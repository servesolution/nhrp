using EntityFramework;
using ExceptionHandler;
using MIS.Models.Payment.HDSP;
using MIS.Services.Core;
using MIS.Services.Payment.HDSP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Payment.HDSP
{
    public class DTCOController : BaseController
    {
        CommonFunction common = new CommonFunction();
        public ActionResult ImportDTCOFile()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                ViewData["FinalMessage"] = Session["ImportMessage"];
                Session["ImportMessage"] = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult ImportDTCOFile(HttpPostedFileBase file, FormCollection fc)
        {
            string exc = string.Empty;
            DTCOImportService objDTCOService = new DTCOImportService();
             bool rs = false;
            string dist_cd = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/DTCOPayment/";
                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                }
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", ""));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string File_Name = file.FileName.Replace(" ", "_");
                Session["fileName"] = File_Name;
                try
                {
                    List<string> dbcolumn = new List<string>();
                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objDTCOService.JSONFileListInDB();
                    if (!fileListInDB.Contains(File_Name))
                    {
                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath);
                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");



                            if (oDataTable.Rows.Count > 0)
                            {
                                rs = objDTCOService.SaveDTCOPaymentData(oDataTable, File_Name, dist_cd, null, out exc);
                            }
                            else
                            {
                                ViewData["FinalMessage"] = exc;
                                Session["ImportMessage"] = exc;
                                Session["fileName"] = file.FileName;
                                return RedirectToAction("ImportDTCOFile");
                            }
                            if (rs)
                            {
                                ViewData["FinalMessage"] = "Success";
                            }
                            else
                            {
                                ViewData["FinalMessage"] = exc;
                            }
                        }
                    }
                    else
                    {
                        ViewData["FinalMessage"] = "Duplicate File";
                    }
                }
                catch (Exception e)
                {
                    ViewData["FinalMessage"] = e.Message.ToString();
                    Session["ImportMessage"] = ViewData["FinalMessage"];
                    return RedirectToAction("ImportDTCOFile");
                }
            }
            Session["ImportMessage"] = ViewData["FinalMessage"];
            return RedirectToAction("ImportDTCOFile");
        }
        private DataTable Import_To_Grid(string FilePaths, string Extension, string isHDR)//, string sheetName)
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
            conStr = String.Format(conStr, FilePaths, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            DataTable newDt = new DataTable();
            for (int i = dt.Rows.Count - 1; i > 0; i--)
            {
                DataRow dw = dt.Rows[i];
                if (dw.ItemArray[0].ToString() == "" || dw.ItemArray[0].ToString() == null)
                {
                    dt.Rows.Remove(dw);
                }
            }

            return dt;
        }
        public ActionResult GetEnrollmentFileImport(string SearchValueDistrict)
        {
            CommonFunction commonFC = new CommonFunction();
            DTCOImportService objDTCOService = new DTCOImportService();
            string id = string.Empty;
            string disCD = commonFC.GetCodeFromDataBase(SearchValueDistrict.ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            //string vdcCD = commonFC.GetCodeFromDataBase(SearchValueVdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["distcode"] = SearchValueDistrict;
            //Session["vdccode"] = vdcCD;
            DataTable dtresult = objDTCOService.GetImportFileByDistrictVDC(SearchValueDistrict);
            ViewData["dtDTCOUploadedFile"] = dtresult;

            return PartialView("_DTCOUploadedFileList");


        }
        public ActionResult GetDuplicateData()
        {
            DTCOImportService objDTCOService = new DTCOImportService();
            DataTable dt = new DataTable();
            string filename = Session["fileName"].ConvertToString();
            Session["fileName"] = null;

            try
            {
                dt = objDTCOService.GetDuplicateData(filename);
                ViewData["Duplicate"] = dt;



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            //return PartialView("~/_BankDumpData");
            return PartialView("~/Views/DTCO/_DTCODupData.cshtml");
        }
        public ActionResult GetDuplicateDataFromFile()
        {
            DTCOImportService objDTCOService = new DTCOImportService();
            DataTable dt = new DataTable();
            string filename = Session["file"].ConvertToString();
            Session["file"] = null;

            try
            {
                dt = objDTCOService.GetDuplicateDataFromFile(filename);
                ViewData["Duplicate"] = dt;



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            //return PartialView("~/_BankDumpData");
            return PartialView("~/Views/DTCO/_DTCODupFileData.cshtml");
        }
        public ActionResult GetDTCOError()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");

            #region display message
            if (!string.IsNullOrEmpty(Session["Message"].ConvertToString()))
            {

                if (Session["Message"].ConvertToString() == "DeleteSuccess")
                {
                    ViewData["DeleteSuccess"] = "Success!";
                }
                if (Session["Message"].ConvertToString() == "DeleteFailed")
                {
                    ViewData["DeleteFailed"] = "Failed!";
                }

                if (Session["Message"].ConvertToString() == "UpdateSuccess")
                {
                    ViewData["UpdateSuccess"] = "Success!";
                }
                if (Session["Message"].ConvertToString() == "UpdateFailed")
                {
                    ViewData["UpdateFailed"] = "Failed!";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult GetDTCOError(FormCollection fc)
        {
            DTCOImportService objDTCOService = new DTCOImportService();
            CommonFunction common = new CommonFunction();

            DataTable errorList = new DataTable();
            string district = fc["ddl_District"].ToString();
            string vdc = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            if (!string.IsNullOrEmpty(district))
            {
                try
                {
                    errorList = objDTCOService.GetErrorList(district, vdc);
                }
                catch (Exception ex)
                {
                    errorList = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }

            }
            Session["DTCOError"] = errorList;
            return PartialView("~/Views/DTCO/_DTCOErrorList.cshtml");
        }
        public ActionResult DeleteUploadData(string p)
        {
            DTCOImportService objService = new DTCOImportService();
            QueryResult success = new QueryResult();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        id = rvd["id"].ToString();
                        success = objService.DeleteErrorData(id.ToInt32());
                    }
                }
            }
            if (success.IsSuccess)
            {
                ViewData["Message"] = "DeleteSuccess";
            }
            else
            {
                ViewData["Message"] = "DeleteFailed";
            }

            Session["Message"] = ViewData["Message"];

            return RedirectToAction("GetDTCOError");
        }
        public ActionResult UpdateDTCO(FormCollection fc)
        {
            DTCOImportService objService = new DTCOImportService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            BankClaim objBankClaim = new BankClaim();
            string value = "";

            if (fc != null)
            {
                value = fc["editForm"].ToString();
                rvd = QueryStringEncrypt.DecryptString(value);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        objBankClaim.DTCO_Payment_Id = rvd["id"].ToInt32();
                        objBankClaim = objService.GetDTCOById(objBankClaim.DTCO_Payment_Id);

                        ViewData["ddl_District"] = common.GetDistricts(objBankClaim.Dis_Cd.ConvertToString());
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objBankClaim.Vdc_Mun_Cd.ToString(), objBankClaim.Dis_Cd.ToString());
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankClaim.Ward_Num.ConvertToString(), objBankClaim.Vdc_Mun_Cd.ToString());
                        ViewData["ddl_Bank"] = common.GetBankName((objBankClaim.Bank_cd).ToString());
                        ViewData["ddl_Installation"] = common.GetInstallation(objBankClaim.Payroll_install_cd).Where(x => x.Value.ToInt32() <= 3);

                        return View(objBankClaim);
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult UpdateDTCODtl(FormCollection fc)
        {
            CommonFunction commonFC = new CommonFunction();
            BankClaim objBankClaim = new BankClaim();
            DTCOImportService objService = new DTCOImportService();
            bool checkUpdate = false;

            objBankClaim.PA_NO = fc["PA_NO"].ConvertToString();
            objBankClaim.Payroll_install_cd = fc["ddl_Installation"].ToString();
            objBankClaim.Dis_Cd = fc["Dis_Cd"].ToInt32();
            string vdcCode = fc["ddl_VDCMun"].ToString();
            objBankClaim.Ward_Num = fc["Ward_Num"].ToInt32();
            objBankClaim.Beneficiary_Name = fc["Beneficiary_Name"].ConvertToString();
            objBankClaim.ChequeNo = fc["ChequeNo"].ConvertToString();
            objBankClaim.Cheque_Tran_EDate = fc["Cheque_Tran_EDate"];
            objBankClaim.Nepali_Cheque_Tran_Date = NepaliDate.getNepaliDate(objBankClaim.Cheque_Tran_EDate.ToString());
            objBankClaim.Bank_cd = Convert.ToInt32(fc["Bank_cd"]);
            objBankClaim.Vdc_Mun_Cd = Convert.ToInt32(commonFC.GetCodeFromDataBase(vdcCode.ToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            objBankClaim.DTCO_Payment_Id = fc["DTCO_Payment_Id"].ToInt32();
            objBankClaim.Updated_By = SessionCheck.getSessionUsername();
            objBankClaim.UpdatedDt = DateTime.Now;

            checkUpdate = objService.UpdateDTCODtl(objBankClaim);

            if (checkUpdate == true)
            {
                ViewData["Message"] = "UpdateSuccess";

            }
            else
            {
                ViewData["Message"] = "UpdateFailed";

            }

            Session["Message"] = ViewData["Message"];
            return RedirectToAction("GetDTCOError");
        }
        public ActionResult DeleteDTCOFile(string p)
        {
            DTCOImportService objService = new DTCOImportService();
            bool success = false;
            string id = "";

            RouteValueDictionary rvd = new RouteValueDictionary();

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        id = rvd["id"].ToString();
                        success = objService.deleteUploadData(id);
                    }
                }
            }
            if (success == true)
            {
                ViewData["Message"] = "Delete Success";
            }
            else
            {
                ViewData["Message"] = "Delete Failed";
            }

            Session["ImportMessage"] = ViewData["Message"];

            return RedirectToAction("ImportDTCOFile");
        }
        public JsonResult DeleteDuplicateData()
        {
            DTCOImportService objService = new DTCOImportService();
            QueryResult status = null;

            status = objService.DeleteDTCODupData();
            if(status.IsSuccess)
            {
                return Json("Success");
            }
            return Json("failed");
        }

    }
}
