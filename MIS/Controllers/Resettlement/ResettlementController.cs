using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using MIS.Services.Core;
using MIS.Services.Resettlement;
using System.Configuration;
using System.Data.OleDb;
using MIS.Models.ManageResettlement;
using System.Web.Routing;

namespace MIS.Controllers.Resettlement
{
    public class ResettlementController : BaseController
    {
        //

        // GET: /Resettlement/
        CommonFunction com = new CommonFunction();
        public ActionResult ManageResettlement(string p)
        {

            ResettlementModelClass objModel = new ResettlementModelClass();
            ResettlementService objService = new ResettlementService();
            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview("").Data;


            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        objModel.ResettlementId = (rvd["id"]).ConvertToString();
                    }
                    if (rvd["Mode"] != null)
                    {
                        objModel.Mode = (rvd["Mode"]).ConvertToString();
                    }

                }
                if (objModel.Mode == "U")
                {
                    objModel = objService.getResettlementDataById(objModel.ResettlementId.ConvertToString());
                    objModel.ResVDCMUN = com.GetDefinedCodeFromDataBase(objModel.ResVDCMUN.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_Districts"] = com.GetDistrictsByDistrictCode(objModel.ResDistrict.ConvertToString());
                    ViewData["ddl_Vdc"] = com.GetVDCMunByDistrictCode(objModel.ResVDCMUN.ConvertToString(), objModel.ResDistrict.ConvertToString());
                    ViewData["ddl_Wards"] = com.GetWardByVDCMun(objModel.ResWard.ConvertToString(), "");
                    ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview(objModel.ResMisReview.ConvertToString()).Data;
                    objModel.Mode = "U";
                    return View(objModel);
                }
                if (objModel.Mode == "D")
                {
                    bool res = objService.saveResettlement(objModel);
                    return RedirectToAction("ResettlementList");
                }
               
            }
            objModel.Mode = "I";
            return View(objModel);
        }
        [HttpPost]
        public ActionResult ManageResettlement(ResettlementModelClass objModel)
        {
            bool result = false;
            ResettlementService objService = new ResettlementService();
            objModel.ResVDCMUN = com.GetCodeFromDataBase(objModel.ResVDCMUN, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            result = objService.saveResettlement(objModel);
            return RedirectToAction("ResettlementList");
        }


        #region resettlement list
        public ActionResult ResettlementList()
        {

            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview("").Data;
            ViewData["ResettlSurveySave"] = Session["ResettlSurveySave"].ConvertToString();
            Session["ResettlSurveySave"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult ResettlementList(FormCollection fc)
        {
            DataTable dt = new DataTable();
            ResettlementService objService = new ResettlementService();
            ResettlementModelClass objModel = new ResettlementModelClass();
            objModel.ResDistrict = fc["ResDistrict"].ConvertToString();
            objModel.ResVDCMUN = fc["ResVDCMUN"].ConvertToString();
            objModel.ResVDCMUN = com.GetCodeFromDataBase(objModel.ResVDCMUN, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            objModel.ResWard = fc["ward"].ConvertToString();

            objModel.ResMisReview = fc["ddl_MIS_review"].ConvertToString();
            objModel.ResPaNo = fc["ResPaNo"].ConvertToString();
            //objModel.ResFirstName = fc["district"].ConvertToString();

            dt = objService.GetAllResettlement(objModel);
            ViewData["ResettlementData"] = dt;
            return PartialView("_ResettlementLsit");
        }
        #endregion
        public ActionResult UploadResettlement()
        {
            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
            ViewData["DuplicateFileName"] = Session["DuplicateFileName"].ConvertToString();
            Session["DuplicateFileName"] = null;
            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "Data Imported Successfully also found some conflicted reocord.")
                {
                    ViewData["FinalMessage"] = "Data Imported Successfully also found some conflicted reocord.";
                }
                if (Session["ImportMessage"].ConvertToString() == "Data Imported Successfully.")
                {
                    ViewData["FinalMessage"] = "Data Imported Successfully.";
                }
                if (Session["ImportMessage"].ConvertToString() == "All Conflicted data")
                {
                    ViewData["FinalMessage"] = "All Conflicted data";
                }
                if (Session["ImportMessage"].ConvertToString() == "Empty")
                {
                    ViewData["FinalMessage"] = "Empty";
                }
                if (Session["ImportMessage"].ConvertToString() == "Failed.")
                {
                    ViewData["FinalMessage"] = Session["ErrorMessage"].ConvertToString();
                }
                if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Duplicate File.";
                }
                if (Session["ImportMessage"].ConvertToString() == "PA Doesnot exist.")
                {
                    ViewData["FinalMessage"] = "PA Doesnot exist.";
                }
                Session["ImportMessage"] = null;




            }
            return View();
        }
        [HttpPost]
        public ActionResult UploadResettlement(FormCollection fc, HttpPostedFileBase file)
        {
            string exc = string.Empty;
            bool rs = false;
            string results = string.Empty;
            string districtCd = fc["ddl_districts"].ConvertToString();
            string vdcCdDef = fc["VdcCd"].ConvertToString();
            string errormessage = string.Empty;
            string vdcCd = com.GetCodeFromDataBase(vdcCdDef, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["DuplicateFileName"] = file.FileName;

            ResettlementService objService = new ResettlementService();
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ResettlementImport/";

                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                }

                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName));
                try
                {
                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objService.JSONFileListInDB();
                    if (!fileListInDB.Contains(file.FileName))
                    {

                        int lnNum = 0;
                        DataTable oDataTable = new DataTable();

                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, ".xlsx", "Yes", "Sheet1");//, " ");
                            string indexDeleted=null;

                            for (int i = 0; i < (oDataTable.Rows.Count) ; i++)
                            {
                            RowDeleted:
                                if (indexDeleted!=null)
                                {
                                    i = indexDeleted.ToInt32();
                                    indexDeleted = null;
                                    
                                }
                            if (oDataTable.Rows.Count == 0)
                            {
                                break;
                            }
                             
                                if((oDataTable.Rows[i][11].ConvertToString() !="" && oDataTable.Rows[i][11].ConvertToString()!=null)&&
                                   (oDataTable.Rows[i][1].ConvertToString() != "" && oDataTable.Rows[i][1].ConvertToString() != null))
                                {
                                    if (objService.CheckIfPaNameExist(oDataTable.Rows[i][11].ConvertToString(), oDataTable.Rows[i][1].ConvertToString()))
                                    {
                                       
                                        ViewData["FinalMessage"] = "Duplicate";
                                        string reason= "Pa and respondance name matched with saved records.";
                                        bool result = objService.saveDuplicateData(oDataTable.Rows[i][16].ConvertToString(),
                                        oDataTable.Rows[i][1].ConvertToString(), districtCd,
                                        vdcCd, oDataTable.Rows[i][11].ToString(),file.FileName,reason);
                                        //oDataTable.Rows[i].Delete();
                                        oDataTable.Rows.Remove(oDataTable.Rows[i]);
                                        indexDeleted = i.ToString();
                                        goto RowDeleted;
                                        
                                    }
                                }

                                if ((oDataTable.Rows[i][14].ConvertToString() != "" && oDataTable.Rows[i][14].ConvertToString() != null) &&
                                  (oDataTable.Rows[i][1].ConvertToString() != "" && oDataTable.Rows[i][1].ConvertToString() != null))
                                {
                                    if (objService.CheckIfNissaNameExist(oDataTable.Rows[i][14].ConvertToString(), oDataTable.Rows[i][1].ConvertToString()))
                                    {

                                        ViewData["FinalMessage"] = "Duplicate";
                                        string reason = "Nissa and respondance name matched with saved records.";
                                        bool result = objService.saveDuplicateData(oDataTable.Rows[i][16].ConvertToString(),
                                        oDataTable.Rows[i][1].ConvertToString(), districtCd,
                                        vdcCd, oDataTable.Rows[i][11].ToString(), file.FileName, reason);
                                        oDataTable.Rows.Remove(oDataTable.Rows[i]);
                                        indexDeleted = i.ToString();
                                        goto RowDeleted;

                                    } 
                                }
                                
                            }
                           
                            //int columnscount = oDataTable.Rows.Count;
                            //if(oDataTable.Rows.Count>0)
                            //{
                            //    bool CheckNull = IsBlankRow(oDataTable, 1, columnscount);

                            //    if (CheckNull == true)
                            //    {
                            //        ViewData["FinalMessage"] = "Empty";
                            //        Session["ImportMessage"] = ViewData["FinalMessage"];
                            //        return RedirectToAction("UploadResettlement");
                            //    }
                            //}

                            if (oDataTable.Rows.Count < 1)
                            {
                                ViewData["FinalMessage"] = "All Conflicted data";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("UploadResettlement");
                            }
                            

                            if (oDataTable.Rows.Count > 0)
                            {
                                //rs = objService.ImportResettlement(oDataTable, file.FileName, out exc);
                                results = objService.ImportResettlement(oDataTable, file.FileName, out exc, districtCd, vdcCd);
                                if (results == "True")
                                {
                                    rs = true;
                                }
                                else
                                {
                                    rs = false;
                                }
                                errormessage = exc.ConvertToString();
                               
                            }
                            else
                            {
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                Session["ErrorMessage"] = ViewData["ErrorMessage"];
                                //return RedirectToAction("InspectionImportRegistration");
                            }

                            if (rs)
                            {

                                if (ViewData["FinalMessage"].ConvertToString() == "Duplicate")
                                {
                                    Session["ImportMessage"] = "Data Imported Successfully also found some conflicted reocord.";
                                    Session["ErrorMessage"] = ViewData["ErrorMessage"];
                                }
                                else
                                {
                                    Session["ImportMessage"] = "Data Imported Successfully.";
                                    Session["ErrorMessage"] = ViewData["ErrorMessage"];

                                }


                            }
                            else
                            {
                                ViewData["FinalMessage"] = errormessage;
                                Session["ImportMessage"] = "Failed.";
                                Session["ErrorMessage"] = errormessage;
                                return RedirectToAction("UploadResettlement");
                            }

                        }
                    }
                    else
                    {
                        Session["ImportMessage"] = "Duplicate File";
                        Session["ErrorMessage"] = ViewData["ErrorMessage"];
                    }
                }
                catch (Exception)
                {
                    Session["ImportMessage"] = "Failed.";
                    Session["ErrorMessage"] = ViewData["ErrorMessage"];
                }
            }
            return RedirectToAction("UploadResettlement");
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


        [HttpPost]
        public ActionResult GetUploaded(string DistCd, string vdcCd)
        {
            DataTable dt = new DataTable();
            ResettlementService objService = new ResettlementService();
            string VdcCd = com.GetCodeFromDataBase(vdcCd, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            dt = objService.GetUploaded(DistCd, VdcCd);
            ViewData["UploadedFile"] = dt;
            return PartialView("_ResettlementUploaded");
        }



        public ActionResult GetConflictedResettlement(string fileName)
        {
            DataTable dt = new DataTable();
            ResettlementService objService = new ResettlementService();


            dt = objService.GetDuplicate(fileName);
            ViewData["Duplicate"] = dt;
            return PartialView("_ResettlementDuplicateData");
         }

    }
}




