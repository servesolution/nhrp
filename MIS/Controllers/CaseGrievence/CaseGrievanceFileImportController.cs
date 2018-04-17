using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.IO;
using System.Data;
using CsvHelper;
using System.Data.OleDb;
using System.Configuration;
using MIS.Services.CaseGrievance;

namespace MIS.Controllers.CaseGrievence
{
    public class CaseGrievanceFileImportController : BaseController
    {
        //
        // GET: /CaseGrievanceFileImport/
        CommonFunction commonFC = new CommonFunction();
        public ActionResult Import()
        {
            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "Success")
                {
                    ViewData["FinalMessage"] = "Data imported successfully.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Failed.")
                {
                    ViewData["FinalMessage"] = "Data import failed.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Data import failed due to duplicate file.";
                }
                Session["ImportMessage"] = "";
            }
            return View();
        }
        public ActionResult ImportExcel()
        {
            ViewData["ddl_District"] = commonFC.GetDistricts("");
            ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "Success")
                {
                    ViewData["FinalMessage"] = "Data imported successfully.";
                }
                if (Session["ImportMessage"].ConvertToString() == "Empty")
                {
                    ViewData["FinalMessage"] = "Ward is Empty in some column";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Failed.")
                {
                    ViewData["ErrorMessage"] = Session["ErrorMessage"];
                    ViewData["FinalMessage"] = "Data import failed.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Data import failed due to duplicate file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate Data")
                {
                    ViewData["FinalMessage"] = "Data import failed. These data already exists in the system";
                }
                Session["ImportMessage"] = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult ImportExcel(FormCollection fc, HttpPostedFileBase file)
        //(FormCollection fc)
        {
            string exc = string.Empty;
            CaseGrievanceFileImportService objService = new CaseGrievanceFileImportService();
            string DistrictCode = fc["ddl_District"].ConvertToString();
            string VDCDefinedCode = fc["ddl_VDCMun"].ConvertToString();
            string VDCCode = commonFC.GetCodeFromDataBase(VDCDefinedCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            bool rs = false;
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ExcelGrievance/BrowseImport/";


                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                }

                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName.Replace(" ","_")));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                try
                {
                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objService.JSONFileListInDB();
                    if (!fileListInDB.Contains(file.FileName))
                    {
                        int lnNum = 0;
                        DataTable oDataTable = new DataTable();
                        //DataTable filterTable = new DataTable();
                        //string extension = Path.GetExtension(finalPath);
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, ".xlsx", "Yes", "Sheet1");//, " ");
                           
                            for (int i = (oDataTable.Rows.Count - 1); i >= 0; i--)
                            {
                                if (oDataTable.Rows[i][1].ConvertToString() != "")
                                {
                                    if (objService.CheckDuplicate(oDataTable.Rows[i][1].ConvertToString(), DistrictCode.ConvertToString(), VDCCode.ConvertToString(), oDataTable.Rows[i][7].ConvertToString(),oDataTable.Rows[i][3].ConvertToString(),oDataTable.Rows[i][9].ConvertToString()))
                                    {
                                        oDataTable.Rows.RemoveAt(i);
                                    }
                                }
                                else
                                {
                                    if (oDataTable.Rows[i][1] == DBNull.Value)
                                        oDataTable.Rows[i].Delete();
                                }
                                oDataTable.AcceptChanges();
                            }
                            int columnscount = oDataTable.Rows.Count;
                            bool CheckNull = IsBlankRow(oDataTable, 7, columnscount);
                            //#region split NRA DEFINE CODE
                            //for (int i = 1; i < oDataTable.Rows.Count; i++)
                            //{
                            //    if (oDataTable.Rows[i][5].ToString() != "")
                            //    {
                            //        NRAward = oDataTable.Rows[i][5].ToString().TrimEnd(' ').Split(' ')[2];
                            //    }
                            //    if (oDataTable.Rows[i][4].ToString() != NRAward.ToString())
                            //    {
                            //        ViewData["FinalMessage"] = "Mismatch";
                            //        Session["ImportMessage"] = ViewData["FinalMessage"];
                            //        return RedirectToAction("OpenImport");
                            //    }
                            //}

                            //#endregion
                            if (oDataTable.Rows.Count < 1)
                            {
                                ViewData["FinalMessage"] = "Duplicate Data";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("ImportExcel");
                            }
                            if (CheckNull == true)
                            {
                                ViewData["FinalMessage"] = "Empty";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("ImportExcel");
                            }
                            rs = objService.SaveExcelDataFromFileBrowse(oDataTable, DistrictCode, VDCCode, file.FileName.Replace(" ","_"), out exc);
                        }
                        else if (extension.ToLower() == ".csv")
                        {
                            using (StreamReader reader = System.IO.File.OpenText(finalPath))
                            {
                                var csv = new CsvReader(reader);
                                string[] columnsNames = null;
                                string[] ostreamDataValues = null;
                                while (!reader.EndOfStream)
                                {
                                    var oStreamData = reader.ReadLine().Trim();
                                    if (oStreamData.Length > 0)
                                    {
                                        ostreamDataValues = oStreamData.Split(',');
                                        if (lnNum == 0)
                                        {
                                            lnNum++;
                                            columnsNames = ostreamDataValues;
                                            foreach (string csbHeader in columnsNames)
                                            {
                                                DataColumn dtCol = new DataColumn(csbHeader.ToUpper(), typeof(string));
                                                dtCol.DefaultValue = string.Empty;
                                                oDataTable.Columns.Add(dtCol);

                                            }
                                        }
                                        else
                                        {
                                            DataRow oDataRow = oDataTable.NewRow();
                                            for (int i = 0; i < columnsNames.Length; i++)
                                            {
                                                var colname = columnsNames[i];
                                                var oStreamDataValue = ostreamDataValues;

                                                oDataRow[colname] = string.IsNullOrEmpty(oStreamDataValue[i]) ? string.Empty : oStreamDataValue[i].ToString().TrimStart('0').Length > 0 ? oStreamDataValue[i].TrimStart('0') : "0";
                                            }
                                            oDataTable.Rows.Add(oDataRow);
                                        }
                                    }

                                }
                                reader.Close();
                                reader.Dispose();

                            }
                            rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                        }

                        //rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                        if (rs)
                        {
                            ViewData["FinalMessage"] = "Success";
                        }
                        else
                        {
                            ViewData["FinalMessage"] = "Failed.";
                        }
                        //ViewData["FinalMessage"] = "Data imported successfully.";
                    }
                    else
                    {
                        ViewData["FinalMessage"] = "Duplicate File";
                    }

                }
                catch (Exception)
                {
                    ViewData["FinalMessage"] = "Failed.";
                }
            }
            Session["ImportMessage"] = ViewData["FinalMessage"];
            Session["ErrorMessage"] = ViewData["ErrorMessage"];
            return RedirectToAction("ImportExcel");
            //        return RedirectToAction("OpenImport", new RouteValueDictionary(
            //new { controller = "Enrollment", action = "OpenImport", Import = "Success" }));
        }
        public ActionResult GetGrievanceFileImport(string SearchValueDistrict, string SearchValueVdc)
        {
            CommonFunction commonFC = new CommonFunction();
            CaseGrievanceFileImportService objReportService = new CaseGrievanceFileImportService();
            string id = string.Empty;
            string vdcCD = commonFC.GetCodeFromDataBase(SearchValueVdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["distcode"] = SearchValueDistrict;
            Session["vdccode"] = vdcCD;
            DataTable dtresult = objReportService.GetDataImportRecordByDistrict(SearchValueDistrict, vdcCD);
            ViewData["dtGrievanceFileImportRslt"] = dtresult;

            return PartialView("_GetGrievanceFileImport");
        }
        static bool IsBlankRow(DataTable table, int index, int columns)
        {
            if (table == null) return true;
            for (int i = 1; i < columns; i++)
            {
                var val = table.Rows[i][index].ConvertToString();
                if (val == null || val == string.Empty)
                {
                    return true;
                }
            }
            return false;
        }

        private DataTable Import_To_Grid(string FilePath, string Extension, string isHDR, string sheetName)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string SheetName = dtExcelSchema.Rows[0]["House_Owner"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "$]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            return dt;
        }



        //private DataTable Import_To_Grid(string FilePath, string Extension, string isHDR)//, string sheetName)
        //{
        //    string conStr = "";
        //    switch (Extension)
        //    {
        //        case ".xls": //Excel 97-03
        //            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
        //                     .ConnectionString;
        //            break;
        //        case ".xlsx": //Excel 07
        //            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
        //                      .ConnectionString;
        //            break;
        //    }
        //    conStr = String.Format(conStr, FilePath, isHDR);
        //    OleDbConnection connExcel = new OleDbConnection(conStr);
        //    OleDbCommand cmdExcel = new OleDbCommand();
        //    OleDbDataAdapter oda = new OleDbDataAdapter();
        //    DataTable dt = new DataTable();
        //    cmdExcel.Connection = connExcel;

        //    //Get the name of First Sheet
        //    connExcel.Open();
        //    DataTable dtExcelSchema;
        //    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //    connExcel.Close();

        //    //Read Data from First Sheet
        //    connExcel.Open();

        //    //DataTable dtSheet = new DataTable();
        //    //dtSheet = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //    //sheetName = dtSheet.Rows[0]["TABLE_NAME"].ConvertToString();

        //    //cmdExcel.CommandText = "SELECT * From [" + sheetName + "$]";
        //    //string query = String.Format("select * from [{0}${1}]", "Sheet1", "A2:ZZ");
        //    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        //    //cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]  WHERE  not [SN*] = 0 or not [FORM NO*] = '' or not [REG_NO] = '' or not [GRIEVANT NAME*] = '' or not [FATHER'S NAME*] ='' or not [DIST*] ='' or not [D_CODE*] =''or not [VDC/MUN*] =''or not [V_CODE*] =''or not [FATHER'S NAME*] =''or not [FATHER'S NAME*] =''" + DBNull.Value;
        //    //cmdExcel.CommandText = String.Format("SELECT * From [{0}${1}]", "SheetName", "A2:ZZ");
        //    oda.SelectCommand = cmdExcel;
        //    oda.Fill(dt);
        //    connExcel.Close();
        //    //dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();

        //    return dt;
        //}
    }
}
