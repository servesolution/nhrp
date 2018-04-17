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
using MIS.Services.Payment.HDSP;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Setup;
using System.Globalization;
using System.Web.Routing;
using EntityFramework;
using MIS.Models.Payment.HDSP;
using MIS.Services.Enrollment;
using MIS.Models.Core;
using MIS.Authentication;
using MIS.Models.Security;
using MIS.Services.Security;
using MIS.Services.Setup;
using System.Threading.Tasks;

namespace MIS.Controllers.Payment.HDSP
{
    public class PaymentImportController : BaseController
    {
        CommonFunction common = new CommonFunction();
        NHRSBankMapping objBankMapping = new NHRSBankMapping();
        RouteValueDictionary rvd = new RouteValueDictionary();
        public ActionResult ListFirstTranche()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55") //checks if user is bank/branch user
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode); //get bank by user
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd).Where(x => x.Value != "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            }

            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString())) //success or failed message
            {
                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }

                if (Session["ApprovedMessage"].ConvertToString() == "Update Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "Data Successfully Updated ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Update Failed")
                {
                    ViewData["Message"] = "Update Fialed! Please try again!";
                }

                Session["ApprovedMessage"] = ""; //empty session variable after storing the data in viewdata
            }

            return View(objBankMapping);
        }
        public JsonResult CheckEditPermission(string approvedStatus) //checks approve status of bank data
        {
            rvd = QueryStringEncrypt.DecryptString(approvedStatus);
            if (rvd["status"].ToString() == "ApprovedStatus")
            {
                return Json("Approved");
            }
            else
            {
                return Json("UnApproved");
            }

        }
        [HttpPost]
        public ActionResult ListFirstTranche(FormCollection fc)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.NRA_DEFINED_CD = fc["panum"].ConvertToString();

            result = enrollService.GetFirstTrancheApprovedList(objBankPayment); //get data from service

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55") //checks if user is bank or branch user
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewBag.Tranche = "1";

            ViewData["dtBankListresult"] = result; //sets returned datatable to viewdata

            return PartialView("~/Views/PaymentImport/_ListFirstTrancheData.cshtml"); //returns partial view
        }
        public ActionResult ImportReverseFeedFile()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);

                }

            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                ViewData["ddl_Installment"] = common.GetInstallation("");
                ViewData["ddlbankname"] = common.GetBankCdByDistrictCd("", "");
            }

            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["CONFLICTMESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["CONFLICTMESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";

            if(Session["ErrorFiles"] != null)
            {
                ViewData["ErrorFiles"] = Session["ErrorFiles"];
                Session["ErrorFiles"] = null;
            }

            return View();
        }
        #region Upload form NRA format
        [HttpPost]
        //public ActionResult ImportReverseFeedFile1(HttpPostedFileBase file, FormCollection fc)
        //{
        //    DataTable filteredDt = new DataTable();
        //    string exc = string.Empty;
        //    ReverseBankFileImportService objService = new ReverseBankFileImportService();
        //    bool rs = false;
        //    int batchId;
        //    string bank_cd = common.GetCodeFromDataBase(fc["ddl_Bank"].ConvertToString(), "NHRS_BANK", "BANK_CD");
        //    string branch_standard_cd = fc["ddl_BankBranch"].ConvertToString();
        //    batchId = (objService.GetConflictedBatchId()).ToInt32();
        //    Session["ConflictedRecords"] = null;
        //    Session["CONFLICTED_BATCH_ID"] = null;
        //    Session["PO_CONFLICT"] = "";
        //    List<string> ErrorList = new List<string>();

        //    if (file != null && file.ContentLength > 0)
        //    {
        //        string FilePath = "~/Files/ReverseFeed/";


        //        file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
        //        string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
        //        string File_Name = file.FileName.Replace(" ", "_");
        //        Session["fileName"] = File_Name;
        //        try
        //        {
        //            List<string> fileListInDB = new List<string>();

        //            fileListInDB = objService.JSONFileListInDB();
        //            if (!fileListInDB.Contains(File_Name))
        //            {

        //                DataTable oDataTable = new DataTable();
        //                string extension = Path.GetExtension(finalPath);

        //                if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
        //                {

        //                    oDataTable = Import_To_Grid(finalPath, extension, "Yes");

        //                    if (oDataTable.Rows.Count > 0)
        //                    {
        //                        for (int i = 0; i < oDataTable.Rows.Count; i++)
        //                        {
        //                            #region check PA

        //                            if (oDataTable.Rows[i][1].ToString().Trim() != "")
        //                            {
        //                                if (!objService.CheckIfPAExists(oDataTable.Rows[i][1].ToString().Trim()))
        //                                {
        //                                    ErrorList.Add("PA Number at row position " + (i + 2) + " does not exists");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ErrorList.Add("PA Number at row position " + (i + 2) + " is empty.");
        //                            }
        //                            #endregion

        //                            #region region check branch code exists in file
        //                            if (oDataTable.Rows[i][11].ToString().Trim() == "")
        //                            {
        //                                string reason = "Branch Code is missing at row position " + (i + 2) + " .Please review the file.";
        //                                ErrorList.Add(reason);
        //                            }
        //                            #endregion

        //                            #region check A/C Activation date is empty in file

        //                            if (oDataTable.Rows[i][13].ToString().Trim() == "")
        //                            {
        //                                string reason = "Account activation date is missing at row position " + (i + 2) + " .Please review the file.";
        //                                ErrorList.Add(reason);

        //                            }

        //                            #endregion

        //                            #region check bank name

        //                            if (oDataTable.Rows[i][9].ToString().Trim() == "")
        //                            {
        //                                string reason = "Bank name at position " + (i + 2) + " is empty.";
        //                                ErrorList.Add(reason);
        //                            }

        //                            #endregion

        //                            #region check bank branch

        //                            if (oDataTable.Rows[i][10].ToString().Trim() == "")
        //                            {
        //                                string reason = "Bank branch at position " + (i + 2) + " is empty.";
        //                                ErrorList.Add(reason);
        //                            }

        //                            #endregion

        //                            #region check Recipient Name

        //                            if (oDataTable.Rows[i][7].ToString().Trim() == "")
        //                            {
        //                                string reason = "Recipient Name at position " + (i + 2) + " is empty.";
        //                                ErrorList.Add(reason);
        //                            }

        //                            #endregion

        //                        }
        //                        if (ErrorList.Count > 0)
        //                        {
        //                            Session["PO_CONFLICTList"] = ErrorList;
        //                            return RedirectToAction("ImportReverseFeedFile");
        //                        }
        //                        else if (ErrorList.Count < 1)
        //                        {
        //                            rs = objService.SaveReverseFeedData(oDataTable, file.FileName.Replace(" ", "_"), bank_cd, out exc);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
        //                        return RedirectToAction("ImportReverseFeedFile");
        //                    }

        //                    if (rs)
        //                    {
        //                        Session["PO_CONFLICT"] = "Upload Successful!";

        //                    }
        //                    else
        //                    {

        //                        Session["PO_CONFLICT"] = "Upload Failed!";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Session["PO_CONFLICT"] = "Duplicate File";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Session["PO_CONFLICT"] = "Upload Failed!Please copy all data to another excel sheet and try to upload again!";
        //            ExceptionManager.AppendLog(ex);
        //            return RedirectToAction("ImportReverseFeedFile");

        //        }
        //    }
        //    return RedirectToAction("ImportReverseFeedFile");


        //}
        #endregion
        public ActionResult ImportReverseFeedFile1(HttpPostedFileBase file, FormCollection fc)
        {
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int rs = 0;
            string bank_cd = common.GetCodeFromDataBase(fc["ddl_Bank"].ConvertToString(), "NHRS_BANK", "BANK_CD");
            string branch_standard_cd = fc["ddl_BankBranch"].ConvertToString();
            Session["PO_CONFLICT"] = "";
            string xmlData;

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ReverseFeed/"; // location to save file
                string File_Name = file.FileName.Replace(" ", "_");//replacing white space by _
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //saving file to server
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(File_Name));
                DataTable errorList = null;
                Session["fileName"] = File_Name;
                EnrollmentImportExport objEnroll = new EnrollmentImportExport();
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.JSONFileListInDB(); //getting saved file names from db
                    if (!fileListInDB.Contains(File_Name)) //checks if file already exists in db
                    {
                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath); // gets extension of file

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");// function to convert excel to datatable

                            if (!oDataTable.Columns.Contains("SN"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Please copy data to another excel sheet and upload again!";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                            if (!oDataTable.Columns.Contains("BRANCH_CODE") || !oDataTable.Columns.Contains("PA_NO") ||
                                !oDataTable.Columns.Contains("RECIPIENT_NAME") || !oDataTable.Columns.Contains("AC_NO") ||
                                !oDataTable.Columns.Contains("ACTIVATION_DATE(mm/dd/yyyyAD)") || !oDataTable.Columns.Contains("INSTALLMENT") ||
                                !oDataTable.Columns.Contains("DEPOSIT_AMT") || !oDataTable.Columns.Contains("DEPOSITED_DATE(mm/dd/yyyyAD)") ||
                                !oDataTable.Columns.Contains("CARD_ISSUED_(Y/N)") || !oDataTable.Columns.Contains("REMARKS"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Column name invalid! Please enter column name as provided by NRA";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                            //adding new columns in datatable
                            oDataTable.Columns.Add("FILENAME", typeof(System.String));
                            oDataTable.Columns.Add("BANKCD", typeof(System.String));
                            oDataTable.Columns.Add("ACTIVATION_DATE", typeof(System.String));
                            oDataTable.Columns.Add("DEPOSITED_DATE", typeof(System.String));
                            oDataTable.Columns.Add("IS_CARD_ISSUED", typeof(System.String));
                            oDataTable.Columns.Add("ENTERED_BY", typeof(System.String));

                            foreach (DataRow row in oDataTable.Rows)
                            {
                                //inserting values into new column
                                row["FILENAME"] = File_Name;
                                row["BANKCD"] = bank_cd;
                                row["IS_CARD_ISSUED"] = row["CARD_ISSUED_(Y/N)"].ConvertToString();
                                row["ACTIVATION_DATE"] = row["ACTIVATION_DATE(mm/dd/yyyyAD)"].ConvertToString("MM/dd/yyyy");
                                row["DEPOSITED_DATE"] = row["DEPOSITED_DATE(mm/dd/yyyyAD)"].ConvertToString("MM/dd/yyyy");
                                row["ENTERED_BY"] = SessionCheck.getSessionUsername().ToString();
                            }

                            //removing unwanted columns
                            oDataTable.Columns.Remove("CARD_ISSUED_(Y/N)");
                            oDataTable.Columns.Remove("ACTIVATION_DATE(mm/dd/yyyyAD)");
                            oDataTable.Columns.Remove("DEPOSITED_DATE(mm/dd/yyyyAD)");

                            if (oDataTable.Rows.Count > 0)
                            {
                                using (var ds = new DataSet())
                                {
                                    ds.Tables.Add(oDataTable);
                                    xmlData = ds.GetXml(); // converting datatable to xml
                                }

                                    rs = objService.BulkUploadPSP(xmlData, out exc);

                                if (rs > 0)
                                {
                                    Session["PO_CONFLICT"] = "SUCCESSFUL";
                                    return RedirectToAction("ImportReverseFeedFile");
                                }
                                else
                                {
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                                    Session["PO_CONFLICT"] = exc;
                                }
                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                      
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                        }
                        else
                        {
                            System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                            Session["PO_CONFLICT"] = "Invalid File. Please check file extension";
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                        Session["PO_CONFLICT"] = "Duplicate File";
                    }
                }
                catch (Exception ex)
                {
                    Session["PO_CONFLICT"] = ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                    return RedirectToAction("ImportReverseFeedFile");

                }
            }
            return RedirectToAction("ImportReverseFeedFile");


        }
        private DataTable Import_To_Grid(string FilePaths, string Extension, string isHDR)//, string sheetName)
        {
            string conStr = "";
            Extension = Extension.ToLower();
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
        public ActionResult GetNotExistingData()
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            DataTable dt = new DataTable();
            string filename = Session["fileName"].ConvertToString();
            Session["fileName"] = null;

            try
            {
                dt = objService.GetDumpData(filename);
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
            return PartialView("~/Views/PaymentSearch/_BankDumpData.cshtml");
        }
        public ActionResult GetDuplicateData()
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            DataTable dt = new DataTable();

            string filename = Session["fileName"].ConvertToString();
            string batchId = Session["CONFLICTED_BATCH_ID"].ToString();
            Session["fileName"] = null;

            try
            {
                dt = objService.GetDuplicateData(filename, batchId.ToInt32());
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
            return PartialView("~/Views/PaymentSearch/_BankDumpData.cshtml");
        }
        public ActionResult GetBankImportfile(string SearchValueBank, string installment)
        {
            CommonFunction common = new CommonFunction();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            string id = string.Empty;
            string bankcd = common.GetCodeFromDataBase(SearchValueBank.ConvertToString(), "NHRS_BANK", "BANK_CD");
            Session["distcode"] = bankcd;

            DataTable dtresult = objService.GetBankInfoImportFile(bankcd, installment);
            ViewData["dtBankFileImportRslt"] = dtresult;

            return PartialView("_GetBankFileImport");
        }
        public ActionResult DeleteUploadData(string p)
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            QueryResult success = new QueryResult();
            string id = "";
            string bank_cd = "";
            string payroll_dtl_id = "";
            string pa_number = "";
            string error = "";
            string entered_by = "";
            string username = SessionCheck.getSessionUsername().ToString().Trim();
            RouteValueDictionary rvd = new RouteValueDictionary();

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        id = rvd["id"].ToString();
                        bank_cd = rvd["id1"].ToString();
                        payroll_dtl_id = rvd["PAYROLL_DTL_ID"].ToString();
                        pa_number = rvd["PA_NUMBER"].ToString();
                        error = rvd["ERROR"].ToString();
                        entered_by = rvd["ENTEREDBY"].ToString().Trim();
                        if (!string.IsNullOrEmpty(error))
                        {
                            if (entered_by == username)
                            {
                                success = objService.DeleteErrorData(id, bank_cd, payroll_dtl_id, pa_number);
                            }
                            else
                            {
                                ViewData["Message"] = "NOTAUTH";
                                Session["ImportMessage"] = ViewData["Message"];
                                return RedirectToAction("GetPaymentErrorFiles");
                            }
                        }
                        else
                        {
                            success = objService.DeleteErrorData(id, bank_cd, payroll_dtl_id, pa_number);
                        }
                    }
                }
            }
            if (success.IsSuccess)
            {
                ViewData["Message"] = "deleteSuccess";
            }
            else
            {
                ViewData["Message"] = "deleteFailed";
            }

            Session["ImportMessage"] = ViewData["Message"];

            return RedirectToAction("GetPaymentErrorFiles");
        }
        public ActionResult DeleteExcelUploadFile(string p)
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
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
                        //bank_cd = rvd["id1"].ToString();
                        success = objService.deleteUploadData(id);
                    }
                }
            }
            if (success.IsSuccess)
            {
                ViewData["Message"] = "deleteSuccess";
            }
            else
            {
                ViewData["Message"] = "deleteFailed";
            }

            Session["ImportMessage"] = ViewData["Message"];

            return RedirectToAction("ImportReverseFeedFile");
        }
        public ActionResult ManualUpload()
        {
            CommonFunction common = new CommonFunction();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                }
            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            }

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "Conflict")
                {
                    ViewData["FinalMessage"] = "Conflict";
                }
                if (Session["ImportMessage"].ConvertToString() == "Success")
                {
                    ViewData["FinalMessage"] = "Data imported successfully.";
                }
                if (Session["ImportMessage"].ConvertToString() == "AllConflictedData")
                {
                    ViewData["FinalMessage"] = "All Conflicted data.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FinalMessage"] = "Data import failed.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Data import failed due to duplicate file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "PADoNotExists")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "PA Number at row position " + errorrow + " does not exists.Please review the file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "DuplicateData")
                {
                    ViewData["FinalMessage"] = "Data imported successfully but some data are duplicate in database.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "BranchEmpty")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "There is no branch name  at row position " + errorrow + " .Please review the file.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "ActivationDateEmpty")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Account activation date is missing at row position " + errorrow + " .Please review the file.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "BranchCodeEmpty")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Branch Code is missing at row position " + errorrow + " .Please review the file.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "EmptyTranch")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Tranch at row position " + errorrow + " doesn't contain any value .Please review the file.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "AllDuplicateData")
                {
                    //string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "All data in file are duplicate.Please review the file.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "BranchCodeDoesnotExist")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Branch Code at row position " + errorrow + " does not exists.Please review the file.";

                }

                else if (Session["ImportMessage"].ConvertToString() == "deleteSuccess")
                {

                    ViewData["FinalMessage"] = "Data deleted successfully.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "deleteFailed")
                {

                    ViewData["FinalMessage"] = "Data Delete Failed";

                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongUser")
                {

                    ViewData["FinalMessage"] = "This user is not authorized to delete the data.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongSelectionDropDown")
                {
                    ViewData["FinalMessage"] = "Please select correct installment code in DropDown Menu.";
                }

                Session["ImportMessage"] = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult ManualUpload(FormCollection fc)
        {
            BankClaim objBankClaim = new BankClaim();
            EnrollmentImportExport objBankService = new EnrollmentImportExport();
            CommonFunction common = new CommonFunction();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int batchId = (objService.GetConflictedBatchId()).ToInt32();
            Session["ConflictedRecords"] = null;
            Session["CONFLICTED_BATCH_ID"] = null;

            bool checkUpdate = false;

            objBankClaim.PA_NO = fc["PA_NO"].ConvertToString();
            objBankClaim.Dis_Cd = fc["Dis_Cd"].ToInt32();
            string vdcCode = fc["ddl_VDCMun"].ToString();
            objBankClaim.Ward_Num = fc["Ward_Num"].ToInt32();
            objBankClaim.Reciepient_Name = fc["Reciepient_Name"].ConvertToString();
            objBankClaim.AccountNo = fc["AccountNo"].ConvertToString();
            objBankClaim.Branch_Std_Cd = fc["ddl_BankBranch"].ToString();
            objBankClaim.Activation_Date = (fc["Activation_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Activation_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Activation_Date.ToString());
            objBankClaim.Tranche = "1";
            objBankClaim.Deposited_Date = (fc["Deposited_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Deposited_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Deposited_Date.ToString());
            objBankClaim.Remarks = fc["Remarks"].ConvertToString();
            objBankClaim.IsCard_Issued = fc["ddl_Approved"].ToString();
            objBankClaim.Batch = fc["Batch"].ToInt32();
            objBankClaim.Deposite = 50000;
            objBankClaim.Bank_cd = common.GetCodeFromDataBase(fc["Bank_cd"].ConvertToString(), "NHRS_BANK", "BANK_CD").ToInt32();
            objBankClaim.Card_Iss_Date = NepaliDate.getNepaliDate(Convert.ToString(fc["Card_Iss_Date"]));
            objBankClaim.Branch_Cd = Convert.ToInt32(common.GetBankBranchCdByStdCd((objBankClaim.Branch_Std_Cd).ToString(), (objBankClaim.Bank_cd).ToString()));
            objBankClaim.Bank_Name = common.GetCodeFromDataBase((objBankClaim.Bank_cd).ToString(), "NHRS_BANK", "DESC_ENG");
            objBankClaim.Vdc_Mun_Cd = Convert.ToInt32(common.GetCodeFromDataBase(vdcCode.ToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            objBankClaim.Branch = common.GetCodeFromDataBase(objBankClaim.Branch_Cd.ToString(), "NHRS_BANK_BRANCH", "DESC_ENG");
            objBankClaim.TRANSACTON_ID = fc["TRANSACTON_ID"].ToInt32();
            objBankClaim.Payroll_ID = fc["Payroll_ID"].ToInt32();

            #region check if PA Exists in MST
            if (objBankClaim.PA_NO.ToString().Trim() != "")
            {
                if (!objService.CheckIfPAExists(objBankClaim.PA_NO.ToString()))
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "PA Number does not exists.Please review the file.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
            }
            else
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "PA Number at row position is empty.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }
            #endregion

            #region check bank deposit and tranche.
            string tranchamt = objBankClaim.Tranche.ToString().Trim();
            string deposit = objBankClaim.Deposite.ToString().Trim();

            if (tranchamt.Trim() == "" || objBankClaim.Tranche.ToDecimal() < 0 || objBankClaim.Tranche.ToDecimal() > 3)
            {

                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Tranch at row position is invalid or empty. Please review the file.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");

            }
            else
            {


                if (tranchamt == "1" && deposit.ToDecimal() != 50000)
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Bank deposit and the tranch doesnot match.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
                if (tranchamt == "2")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Bank deposit and the tranch doesnot match.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
                if (tranchamt == "3")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Bank deposit and the tranch doesnot match.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }


            }
            #endregion

            #region check bank account

            if (objBankClaim.AccountNo.ToString().Trim() == "")
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Bank account number is empty.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }
            else
            {
                string accountNo1 = objBankClaim.AccountNo.ToString();
                char[] characters = accountNo1.ToCharArray();
                if (characters[0].ConvertToString() == "'")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Bank account number consist invalid character.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
                if (characters[characters.Length - 1].ConvertToString() == "'")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Bank account number consist invalid character.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }

            }

            #endregion

            #region check duplicate

            //string nra_district = "";
            //string nra_vdc = "";
            //string nra_ward = "";
            //string Pa_no = objBankClaim.PA_NO.ToString();
            //string[] splits = Pa_no.Split('-');
            //int cnt = splits.Count();
            //if (cnt == 5)
            //{
            //    nra_district = splits[splits.Length - 5].ToString();
            //    nra_vdc = splits[splits.Length - 4].ToString();

            //    nra_ward = splits[splits.Length - 3].ToString();
            //}
            //else if (cnt == 6)
            //{
            //    nra_district = splits[splits.Length - 5].ToString();
            //    nra_vdc = splits[splits.Length - 4].ToString();
            //    nra_ward = splits[splits.Length - 3].ToString();
            //}
            //else if (cnt == 7)
            //{
            //    nra_district = splits[splits.Length - 6].ToString();
            //    nra_vdc = splits[splits.Length - 5].ToString();
            //    nra_ward = splits[splits.Length - 4].ToString();
            //}

            //if (objService.CheckDuplicate(objBankClaim.PA_NO.ToString(), objBankClaim.AccountNo.ToString(), objBankClaim.Tranche.ToString()))
            //{
            //    Session["ImportMessage"] = "Conflict";
            //    Session["CONFLICTED_BATCH_ID"] = batchId;
            //    string reason = "Data already exists in the Database.";
            //    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
            //    return RedirectToAction("ManualUpload");

            //}
            #endregion

            #region check duplicate Account Number


            //if (objService.CheckDuplicate(objBankClaim.PA_NO.ToString(), "", ""))
            //{
            //    if (objService.CheckDuplicate(objBankClaim.PA_NO.ToString(), objBankClaim.AccountNo.ToString(), ""))
            //    {
            //        Session["ImportMessage"] = "Conflict";
            //        Session["CONFLICTED_BATCH_ID"] = batchId;
            //        string reason = "PA Number already exists with same Account Number.";
            //        objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
            //        return RedirectToAction("ManualUpload");
            //    }
            //    else
            //    {
            //        ViewBag.TwoPa = objBankClaim.PA_NO.ToString().ToString();
            //    }
            //}
            #endregion

            #region region check branch code exists in file
            if (objBankClaim.Branch_Std_Cd.ToString().Trim() != "")
            {
                if (!objService.ChkIfBranchCd(objBankClaim.Branch_Std_Cd.ToString(), objBankClaim.Bank_cd.ToString()))
                {

                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Branch Code does not exists.Please review the data.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
            }
            else
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Branch Code is missing. Please review the data.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }
            #endregion

            #region check branch exists in file

            if (objBankClaim.Branch.ToString().Trim() == "")
            {

                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "There is no branch name.Please review the file.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }

            #endregion

            #region check A/C Activation date is empty in file

            if (objBankClaim.Activation_Date_LOC.ToString().Trim() == "")
            {

                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Account activation date.Please review the file.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }

            #endregion

            #region check activated date

            string nepali_activate_Date = objBankClaim.Activation_Date_LOC.ConvertToString("MM/dd/yyyy");
            if (nepali_activate_Date == null || nepali_activate_Date == "")
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Activiation date is invalid.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }
            #endregion

            #region check deposited date
            if (objBankClaim.Deposited_Date_LOC.ConvertToString("MM/dd/yyyy").Trim() != "")
            {
                string depositedDt = objBankClaim.Deposited_Date_LOC.ConvertToString("MM/dd/yyyy");

                if (depositedDt == null || depositedDt == "")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Deposited date is invalid.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
            }

            #endregion

            #region check Card issue date
            if (objBankClaim.Card_Iss_Date.ConvertToString("MM/dd/yyyy").Trim() != "")
            {
                string depositedDt = objBankClaim.Deposited_Date_LOC.ConvertToString("MM/dd/yyyy");
                if (depositedDt == null || depositedDt == "")
                {
                    Session["ImportMessage"] = "Conflict";
                    Session["CONFLICTED_BATCH_ID"] = batchId;
                    string reason = "Card issue date is invalid.";
                    objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                    return RedirectToAction("ManualUpload");
                }
            }

            #endregion

            #region check bank name

            if (objBankClaim.Bank_Name.ToString().Trim() == "")
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Bank name is empty.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }

            #endregion

            #region check bank branch

            if (objBankClaim.Branch.ToString().Trim() == "")
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Bank branch is empty.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }

            #endregion

            #region check is card issued

            if (objBankClaim.IsCard_Issued.ToString().Trim() == "" || (objBankClaim.IsCard_Issued.ToString().ToUpper() != "Y" && objBankClaim.IsCard_Issued.ToString().ToUpper() != "N"))
            {
                Session["ImportMessage"] = "Conflict";
                Session["CONFLICTED_BATCH_ID"] = batchId;
                string reason = "Card issued is empty.";
                objService.SaveConflictedRecord(objBankClaim.PA_NO.ToString(), "", reason, batchId);
                return RedirectToAction("ManualUpload");
            }

            #endregion

            if (fc["btn_Submit"].ToString() == "Upload" || fc["btn_Submit"].ToString() == "??????")
            {
                checkUpdate = objBankService.UploadBankClaimDetail(objBankClaim);
            }
            if (checkUpdate == true)
            {
                ViewData["Message"] = "Update Success";

            }
            else
            {
                ViewData["Message"] = "Update Failed";

            }

            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("ListFirstTranche");
        }
        public ActionResult AddSecondTrancheData()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                    //ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Upload Successful!!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Upload Fialed!!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult AddSecondTrancheData(FormCollection fc)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.IsUploaded = "1";
            objBankPayment.NRA_DEFINED_CD = fc["panum"].ToString();
            result = enrollService.GetFirstTrancheApprovedList(objBankPayment);

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewBag.Tranche = "2";

            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/PaymentImport/_AddSecondTrancheData.cshtml");
        }
        public ActionResult ListThirdTrancheData()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                    //ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }
                //if (Session["ApprovedMessage"].ConvertToString() == "WrongUser")
                //{
                //    ViewData["Message"] = "This user is not Authorized to approve data.";
                //}

                if (Session["ApprovedMessage"].ConvertToString() == "Update Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "Data Successfully Updated ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Update Failed")
                {
                    ViewData["Message"] = "Update Fialed! Please try again!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult ListThirdTrancheData(FormCollection fc)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            //objBankPayment.BANK_BRANCH_CD = common.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.IsUploaded = "3";
            objBankPayment.NRA_DEFINED_CD = fc["panum"].ToString();

            result = enrollService.GetThirdTrancheApprovedList(objBankPayment);

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewBag.Tranche = "3";
            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/PaymentImport/_ListThirdTrancheData.cshtml");
        }
        public ActionResult AddThirdTrancheData()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                    //ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Upload Successful!!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Upload Fialed!!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult AddThirdTrancheData(FormCollection fc)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            //objBankPayment.BANK_BRANCH_CD = common.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.IsUploaded = "2";
            objBankPayment.NRA_DEFINED_CD = fc["panum"].ToString();

            result = enrollService.GetSecondTrancheApprovedList(objBankPayment);

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewBag.Tranche = "3";
            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/PaymentImport/_AddThirdTrancheData.cshtml");
        }
        public JsonResult SaveTrancheData(List<BankTrancheData> pa_num)
        {
            QueryResult q = null;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            q = objService.AddTrancheData(pa_num);

            if (q.IsSuccess)
            {
                Session["ApprovedMessage"] = "Success";
                return Json("T");
            }
            else
            {
                Session["ApprovedMessage"] = "Failed";
                return Json("F");
            }

        }
        public JsonResult UpdateTrancheData(string PA_NUM, string DEPOSITE_DATE, string Tranche)
        {
            QueryResult q = null;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            q = objService.UpdateTrancheData(PA_NUM, DEPOSITE_DATE, Tranche);

            if (q.IsSuccess)
                return Json("T");
            return Json("F");
        }
        public ActionResult CheckPaNum(string pa_number)
        {
            DataTable dt = new DataTable();

            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            var result = objService.GetDuplicatePaDetail(pa_number);
            if (result.Rows.Count > 0)
            {
                ViewData["DupPANumberDtl"] = result;
                return PartialView("~/Views/PaymentImport/_DuplicatePADetail.cshtml");
            }
            else
            {
                return Json("EMPTY");
            }

        }
        public ActionResult UpdateClaimVerification(FormCollection fc)
        {
            BankClaim objBankClaim = new BankClaim();
            EnrollmentImportExport objBankService = new EnrollmentImportExport();
            CommonFunction common = new CommonFunction();
            string bank_payroll_cd = "";
            string payroll_installment_cd = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string value = "";
            if (fc != null)
            {
                value = fc["editForm"].ToString();
                rvd = QueryStringEncrypt.DecryptString(value);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        bank_payroll_cd = rvd["id"].ToString();
                        payroll_installment_cd = rvd["id1"].ToString();
                        objBankClaim = objBankService.GetBankClaimbyId(bank_payroll_cd, payroll_installment_cd);

                        ViewData["ddl_District"] = common.GetDistricts(objBankClaim.Dis_Cd.ConvertToString());
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objBankClaim.Vdc_Mun_Cd.ToString(), objBankClaim.Dis_Cd.ToString());
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankClaim.Ward_Num.ConvertToString(), objBankClaim.Vdc_Mun_Cd.ToString());
                        ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1(objBankClaim.IsCard_Issued).Data;

                        if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
                        {
                            ViewData["ddl_Bank"] = common.getBankbyUser((objBankClaim.Bank_cd).ToString());
                            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objBankClaim.Bank_cd).ToString(), "");
                        }
                        else
                        {
                            ViewData["ddl_Bank"] = common.GetBankName("");
                            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objBankClaim.Bank_cd).ToString(), "");
                        }

                        return View(objBankClaim);
                    }
                }
            }

            return View(objBankClaim);
        }
        [HttpPost]
        public ActionResult UpdateClaimVerificationDtl(FormCollection fc)
        {
            CommonFunction common = new CommonFunction();
            BankClaim objBankClaim = new BankClaim();
            EnrollmentImportExport objBankService = new EnrollmentImportExport();
            bool checkUpdate = false;


            objBankClaim.PA_NO = fc["PA_NO"].ConvertToString();
            objBankClaim.Bank_Payroll_Id = Convert.ToInt32(fc["Bank_Payroll_Id"].ToString());
            objBankClaim.Dis_Cd = fc["Dis_Cd"].ToInt32();
            string vdcCode = fc["ddl_VDCMun"].ToString();
            objBankClaim.Ward_Num = fc["Ward_Num"].ToInt32();
            objBankClaim.Reciepient_Name = fc["Reciepient_Name"].ConvertToString();

            objBankClaim.AccountNo = fc["AccountNo"].ConvertToString();
            objBankClaim.Branch_Std_Cd = fc["ddl_BankBranch"].ToString();
            objBankClaim.Activation_Date = (fc["Activation_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Activation_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Activation_Date.ConvertToString());
            objBankClaim.Tranche = "1";
            objBankClaim.Deposited_Date = (fc["Deposited_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Deposited_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Deposited_Date.ConvertToString());
            objBankClaim.Remarks = fc["Remarks"].ConvertToString();
            objBankClaim.IsCard_Issued = fc["ddl_Approved"].ToString();
            objBankClaim.Batch = fc["Batch"].ToInt32();

            objBankClaim.Deposite = 50000;

            objBankClaim.Bank_cd = Convert.ToInt32(fc["Bank_cd"]);
            objBankClaim.Card_Iss_Date = Convert.ToString(fc["Card_Iss_Date"]);
            objBankClaim.Branch_Cd = Convert.ToInt32(common.GetBankBranchCdByStdCd((objBankClaim.Branch_Std_Cd).ToString(), (objBankClaim.Bank_cd).ToString()));

            objBankClaim.Bank_Name = common.GetCodeFromDataBase((objBankClaim.Bank_cd).ToString(), "NHRS_BANK", "DESC_ENG");
            objBankClaim.Vdc_Mun_Cd = Convert.ToInt32(common.GetCodeFromDataBase(vdcCode.ToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            objBankClaim.Branch = common.GetCodeFromDataBase(objBankClaim.Branch_Cd.ToString(), "NHRS_BANK_BRANCH", "DESC_ENG");
            objBankClaim.TRANSACTON_ID = fc["TRANSACTON_ID"].ToInt32();
            objBankClaim.Payroll_ID = fc["Payroll_ID"].ToInt32();



            if (fc["btn_Submit"].ToString() == "Update" || fc["btn_Submit"].ToString() == "?????")
            {
                checkUpdate = objBankService.UpdateBankClaimDetail(objBankClaim);
            }
            if (checkUpdate == true)
            {
                ViewData["Message"] = "Update Success";

            }
            else
            {
                ViewData["Message"] = "Update Failed";

            }

            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("ListFirstTranche");
        }
        public ActionResult ListSecondtranche()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                    //ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }
                //if (Session["ApprovedMessage"].ConvertToString() == "WrongUser")
                //{
                //    ViewData["Message"] = "This user is not Authorized to approve data.";
                //}

                if (Session["ApprovedMessage"].ConvertToString() == "Update Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "Data Successfully Updated ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Update Failed")
                {
                    ViewData["Message"] = "Update Fialed! Please try again!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult ListSecondTranche(FormCollection fc)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            //objBankPayment.BANK_BRANCH_CD = common.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.IsUploaded = "";
            objBankPayment.NRA_DEFINED_CD = fc["panum"].ToString();
            result = enrollService.GetSecondTrancheApprovedList(objBankPayment);

            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewBag.Tranche = "2";

            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/PaymentImport/_ListSecondTrancheData.cshtml");
        }
        public ActionResult UpdateDespositeDate(string EditId, string tranche)
        {
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            EnrollmentImportExport objBankService = new EnrollmentImportExport();
            CommonFunction common = new CommonFunction();
            string bank_payroll_id = "";
            string payroll_installment_cd = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            DataTable result = new DataTable();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }

            if (EditId != null)
            {
                rvd = QueryStringEncrypt.DecryptString(EditId);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        bank_payroll_id = rvd["id"].ToString();
                        payroll_installment_cd = rvd["id1"].ToString();
                        objBankPayment.BANK_NEW_PAYROLL_ID = Convert.ToInt32(bank_payroll_id);
                        objBankPayment.PAYROLL_INSTALL_CD = payroll_installment_cd;
                        objBankPayment.IsUploaded = tranche;
                        if (tranche == "2")
                        {
                            result = objBankService.GetSecondTrancheApprovedList(objBankPayment);
                        }
                        else if (tranche == "3")
                        {
                            result = objBankService.GetThirdTrancheApprovedList(objBankPayment);
                        }


                    }
                }
            }

            ViewData["DepositeDate"] = result;

            return PartialView("~/Views/PaymentImport/_UpdateDepositeDate.cshtml");
        }
        public ActionResult GetPaymentErrorFiles()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.GetBankName(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd.ToString());
                }
            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            }

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "deleteSuccess")
                {

                    ViewData["FinalMessage"] = "Data deleted successfully.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "deleteFailed")
                {

                    ViewData["FailedMessage"] = "Data Delete Failed";

                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongUser")
                {

                    ViewData["FailedMessage"] = "This user is not authorized to delete the data.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "NOTAUTH")
                {

                    ViewData["FailedMessage"] = "You are not Authorized to delete this data! Please contact Bank Admin User.";

                }

            }
            Session["ImportMessage"] = "";

            return View();
        }
        [HttpPost]
        public ActionResult GetPaymentErrorFiles(FormCollection fc)
        {
            DataTable dt = new DataTable();
            string bank_cd;
            string branch_cd;
            EnrollmentImportExport objService = new EnrollmentImportExport();

            if (fc != null)
            {
                bank_cd = fc["ddl_Bank"].ConvertToString();
                branch_cd = fc["ddl_BankBranch"].ConvertToString();
                dt = objService.GetPaymentErrorList(bank_cd, branch_cd, 0, "1");
            }

            Session["ErrorFiles"] = dt;
            ViewData["ErrorFiles"] = dt;
            return PartialView("~/Views/PaymentImport/_ListPaymentError.cshtml");
        }
        public ActionResult ListBranchUsers(FormCollection fc, string p)
        {
            CommonFunction commonFun = new CommonFunction();
            EnrollmentImportExport objEn = new EnrollmentImportExport();
            JsonResult UserStatus = IsAdminBankUser();
            if (UserStatus.Data.ToString() == "T")
            {
                ViewBag.UserStatus = "VALID";
            }
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "usr_cd";
            string order = "asc";
            IDictionary<string, object> dictionUsrCode = new Dictionary<string, object>();
            IDictionary<string, object> dictionUsrName = new Dictionary<string, object>();
            IDictionary<string, object> dictionEmailAdd = new Dictionary<string, object>();
            IDictionary<string, object> dictionExpDate = new Dictionary<string, object>();
            IDictionary<string, object> dictionGroupName = new Dictionary<string, object>();
            IDictionary<string, object> dictionApproved = new Dictionary<string, object>();
            RouteValueDictionary rvd = new RouteValueDictionary();
            UsersService userService = new UsersService();
            DataTable dt = new DataTable();
            ViewBag.initial = "";
            ViewBag.actionName = "Search";
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["initial"] != null)
                        {
                            initial = rvd["initial"].ToString();
                        }
                        if (rvd["orderby"] != null)
                        {
                            orderby = rvd["orderby"].ToString();
                        }
                        if (rvd["order"] != null)
                        {
                            order = rvd["order"].ToString();
                        }
                    }
                }
                string nextorder = "desc";
                if (initial == null)
                {
                    initial = "";
                }
                else
                {
                    dictionUsrCode.Add("initial", initial);
                    dictionUsrName.Add("initial", initial);
                    dictionEmailAdd.Add("initial", initial);
                    dictionExpDate.Add("initial", initial);
                    dictionGroupName.Add("initial", initial);
                    dictionApproved.Add("initial", initial);
                }
                if (orderby == null)
                {
                    orderby = "usr_name";
                }
                dictionUsrCode.Add("orderby", "usr_cd");
                dictionUsrName.Add("orderby", "usr_name");
                dictionEmailAdd.Add("orderby", "email");
                dictionExpDate.Add("orderby", "expiry_dt");
                dictionGroupName.Add("orderby", "grp_name");
                dictionApproved.Add("orderby", "approved");
                if (order == null)
                {
                    order = "asc";
                }
                ViewBag.initial = initial;
                string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                dt = objEn.SearchUsers(initial, CommonVariables.SearchLoginName, CommonVariables.SearchUserName, CommonVariables.SearchGroupCode, orderby, order, bank_cd);
                ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(CommonVariables.SearchGroupCode);
                ViewData["gv_FillUser"] = dt;
                ViewData["txt_Email"] = CommonVariables.SearchLoginName;
                ViewData["txt_UserName"] = CommonVariables.SearchUserName;
                ViewBag.actionName = "ListBranchUsers";
                ViewData["orderby"] = orderby;
                ViewBag.order = order;
                ViewBag.controllerName = "PaymentImport";
                if (order == "desc")
                {
                    nextorder = "asc";
                }
                ViewBag.nextorder = nextorder;
                dictionUsrCode.Add("order", nextorder);
                dictionUsrName.Add("order", nextorder);
                dictionExpDate.Add("order", nextorder);
                dictionGroupName.Add("order", nextorder);
                dictionApproved.Add("order", nextorder);
                RouteValueDictionary routeDictionaryUsrCode = new RouteValueDictionary(dictionUsrCode);
                RouteValueDictionary routeDictionaryUsrName = new RouteValueDictionary(dictionUsrName);
                RouteValueDictionary routeDictionaryExpDate = new RouteValueDictionary(dictionExpDate);
                RouteValueDictionary routeDictionaryGroupName = new RouteValueDictionary(dictionGroupName);
                RouteValueDictionary routeDictionaryApproved = new RouteValueDictionary(dictionApproved);
                ViewBag.RouteUsrCode = QueryStringEncrypt.EncryptString(routeDictionaryUsrCode);
                ViewBag.RouteUsrName = QueryStringEncrypt.EncryptString(routeDictionaryUsrName);
                ViewBag.RouteExpDate = QueryStringEncrypt.EncryptString(routeDictionaryExpDate);
                ViewBag.RouteGroupName = QueryStringEncrypt.EncryptString(routeDictionaryGroupName);
                ViewBag.RouteApproved = QueryStringEncrypt.EncryptString(routeDictionaryApproved);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            #region display message
            if (!string.IsNullOrEmpty(Session["Status"].ConvertToString()))
            {

                if (Session["Status"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["Status"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult ListBranchUsers(FormCollection fc)
        {
            CommonFunction commonFun = new CommonFunction();
            EnrollmentImportExport objEn = new EnrollmentImportExport();
            JsonResult UserStatus = IsAdminBankUser();
            if (UserStatus.Data.ConvertToString() == "T")
            {
                ViewBag.UserStatus = "VALID";
            }
            try
            {
                if (fc["btn_Submit"] == (Utils.GetLabel("Search")))
                {
                    CommonVariables.SearchUserName = Convert.ToString(fc["txt_UserName"]);
                    CommonVariables.SearchLoginName = Convert.ToString(fc["txt_Email"]);
                    CommonVariables.SearchGroupCode = Convert.ToString(fc["ddl_UserInGroup"]);
                    UsersService userService = new UsersService();
                    DataTable dt = new DataTable();
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    dt = objEn.SearchUsers("", HttpUtility.HtmlEncode(CommonVariables.SearchLoginName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchUserName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchGroupCode.Trim()), "", "", bank_cd);
                    ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]);
                    ViewData["gv_FillUser"] = dt;
                    ViewData["txt_Email"] = CommonVariables.SearchLoginName;
                    ViewData["groupCode"] = CommonVariables.SearchGroupCode;
                    ViewData["txt_UserName"] = CommonVariables.SearchUserName;
                    ViewBag.actionName = "ListBranchUsers";
                    ViewBag.initial = "";
                }
                else
                {
                    CommonVariables.SearchLoginName = "";
                    CommonVariables.SearchGroupCode = "";
                    CommonVariables.SearchUserName = "";
                    return RedirectToAction("ListBranchUsers");
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View();
        }
        public ActionResult CreateBankBranchUsers(string p, FormCollection fc)
        {
            CommonFunction common = new CommonFunction();
            Users users = new Users();
            Utils.setUrl(this.Url);
            DataTable dt = new DataTable();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();

            try
            {
                ViewData["gv_ManageUserList"] = dt;
                ViewData["ddl_Districts"] = GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_RegState"] = new List<SelectListItem>();
                ViewData["ddl_Zone"] = new List<SelectListItem>();
                ViewData["ddl_Office"] = GetData.AllOffices(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_Position"] = GetData.AllPositions(Utils.ToggleLanguage("english", "nepali"));

                if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                }
                else
                {

                    ViewData["ddl_bank"] = common.GetBankName("");
                }
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Verification"] = (List<SelectListItem>)common.GetMigration("");
                ViewData["ddl_Donor"] = common.getDonorList("");

                if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                }
                else
                {
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                }

                users.expiryDtLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today.AddDays(364)));
                users.expiryDt = DateTime.Today.AddDays(364);
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }
                }
                if (id != null && id != "")
                {
                    UsersService userService = new UsersService();
                    users = userService.PopulateUserDetails(id);
                    string password = Utils.DecryptString(users.password.ConvertToString());
                    users.password = password;
                    ViewData["ddl_Verification"] = (List<SelectListItem>)common.GetYesNo(users.VerificationRequired).Data;
                    users.editMode = "Edit";
                    ViewData["ddl_bank"] = common.GetBankName(users.bankCode);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(users.bankCode);
                    ViewBag.EditMode = 'Y';
                    users.userCodeCheck = users.usrCd;
                    users.empCodeCheck = users.empCd;
                    return View("CreateBankBranchUsers", users);
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult CreateBankBranchUsers(Users users, FormCollection fc)
        {
            CommonFunction commonFun = new CommonFunction();
            EnrollmentImportExport objService = new EnrollmentImportExport();

            try
            {
                if (fc["btn_Submit"] == (Utils.GetLabel("Submit")))
                {
                    Group group = new Group();
                    string strUserName = string.Empty;
                    string nepexpdt;
                    Users objUsers = new Users();
                    UsersService userService = new UsersService();
                    ViewData["ddl_Districts"] = GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_RegState"] = new List<SelectListItem>();
                    ViewData["ddl_Zone"] = new List<SelectListItem>();
                    ViewData["ddl_Office"] = GetData.AllOffices(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_Position"] = GetData.AllPositions(Utils.ToggleLanguage("english", "nepali"));
                    //ViewData["ddl_PositionSubClass"] = GetData.AllSubClasses(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_VDCMun"] = new List<SelectListItem>();
                    ViewData["ddl_Ward"] = new List<SelectListItem>();
                    ViewData["ddl_Verification"] = (List<SelectListItem>)commonFun.GetMigration("");
                    ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]);
                    //ViewData["ddl_Donor"] = commonFun.getDonorList("");
                    if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
                    {
                        string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                        ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);
                    }
                    else
                    {
                        ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                    }

                    if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
                    {
                        string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                        ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    }
                    else
                    {

                        ViewData["ddl_bank"] = common.GetBankName("");
                    }

                    DataTable dt = new DataTable();
                    ViewData["gv_ManageUserList"] = dt;
                    DateTime expDt = users.expiryDt;
                    nepexpdt = users.expiryDtLoc;
                    if (expDt <= DateTime.Today)
                    {
                        ModelState.AddModelError("ClientName", "Expiry date should be greater than today's date!!");
                    }
                    else
                    {
                        if (Session[SessionCheck.sessionName] != null)
                        {
                            objUsers = (Users)Session[SessionCheck.sessionName];
                            strUserName = objUsers.usrName;
                        }
                        TryUpdateModel(users);
                        //users.password = Utils.EncryptString(users.password);
                        if (users.editMode != "Edit")
                        {
                            if (userService.CheckDuplicateEmail(users.email, users.userCodeCheck))
                            {
                                if (userService.CheckDuplicateUsers(users.usrCd.ConvertToString().ToUpper(), users.empCd))
                                {
                                    //string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                                    users.bankCode = users.bankCode;
                                    users.approved = true;
                                    group.EnterBy = strUserName;
                                    users.password = Utils.EncryptString(users.password);
                                    users.status = "E";
                                    users.enteredBy = strUserName;
                                    users.mobilenumber = fc["mobilenumber"].ConvertToString();
                                    string[] mobl = fc["ddl_Verification"].ConvertToString().Split(',');
                                    users.VerificationRequired = mobl[0];
                                    objService.UserUID(users, group, "I");
                                    Session["Status"] = "Success";
                                    return RedirectToAction("ListBranchUsers");
                                }
                                else
                                {
                                    ModelState.AddModelError("ClientName", "User already exists!!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ClientName", "The email you entered is already taken!!");
                            }
                        }
                        else
                        {
                            if (users.userCodeCheck == users.usrCd && users.empCodeCheck == users.empCd)
                            {
                                if (userService.CheckDuplicateEmail(users.email, users.userCodeCheck))
                                {
                                    if (string.IsNullOrEmpty(users.password))
                                    {
                                        string pss = userService.GetUserPassword(users.email);
                                        users.password = pss;
                                    }
                                    else
                                    {
                                        users.password = Utils.EncryptString(users.password);
                                    }
                                    users.bankCode = users.bankCode;
                                    users.enteredBy = strUserName;
                                    users.VerificationRequired = fc["ddl_Verification"].ConvertToString();
                                    objService.UserUID(users, group, "U");
                                    Session["Status"] = "Success";
                                    return RedirectToAction("ListBranchUsers");
                                }
                                else
                                {
                                    ModelState.AddModelError("ClientName", "The email you entered is already taken!!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ClientName", "User already exists!!");
                                ViewBag.EditMode = 'Y';
                            }
                        }
                    }
                    return View("CreateBankBranchUsers");
                }
                else if (fc["btn_Cancel"] == "Cancel")
                {
                    return RedirectToAction("CreateBankBranchUsers");
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            Session["Status"] = "Success";
            return View("ListBranchUsers");
        }
        public ActionResult Edit(string p)
        {
            return RedirectToAction("CreateBankBranchUsers", new { p = p });
        }
        public JsonResult IsAdminBankUser()
        {
            EnrollmentImportExport objService = new EnrollmentImportExport();

            string user_cd = CommonVariables.UserCode.ToString();
            string grp_cd = CommonVariables.GroupCD.ToString();
            bool result = false;

            if (grp_cd == "1" || grp_cd == "33" || grp_cd == "35")
            {
                result = true;
            }
            else
            {
                result = objService.CheckBankAdminUser(user_cd);
            }

            if (result)
            {
                return Json("T");
            }
            else
            {
                return Json("F");
            }

        }
        public ActionResult GetBeneficiariesList()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            return View();
        }
        [HttpPost]
        public ActionResult GetBeneficiariesList(FormCollection fc)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            try
            {
                if (fc != null && fc.Count > 1)
                {
                    mapBank.DISTRICT_CD = (!string.IsNullOrEmpty(fc["ddl_District"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") : null;
                    mapBank.VDC_MUN_CD = (!string.IsNullOrEmpty(fc["ddl_VDCMun"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") : null;
                    mapBank.WARD_NO = (!string.IsNullOrEmpty(fc["ddl_Ward"].ToString())) ? fc["ddl_Ward"].ToString() : null;
                    mapBank.PA_NUMBER = (!string.IsNullOrEmpty(fc["PA_NUMBER"].ToString())) ? fc["PA_NUMBER"].ToString() : null;
                    mapBank.BeneficiaryType = (!string.IsNullOrEmpty(fc["ddl_benef_type"].ToString())) ? fc["ddl_benef_type"].ToString() : null;
                    dt = objService.GetBeneficiaries(mapBank);

                    ViewData["BenefList"] = dt;

                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            return PartialView("~/Views/PaymentImport/_ListBeneficiaries.cshtml");
        }
        public ActionResult UserDelete(string p)
        {
            Users users = new Users();
            EnrollmentImportExport objService = new EnrollmentImportExport();
            UsersService userService = new UsersService();
            QueryResult qr = new QueryResult();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }
                }
                if (id != "")
                {
                    users.usrCd = id;
                    qr = objService.DeleteBranchUser(users);
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            return RedirectToAction("ListBranchUsers");
        }
        #region Upload form NRA format
        [HttpPost]
        public ActionResult TestExcel(HttpPostedFileBase file, FormCollection fc)
        {
            Helper.ConvertExcelToXml xcel = new Helper.ConvertExcelToXml();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int rs = 0;
            string bank_cd = common.GetCodeFromDataBase(fc["ddl_Bank"].ConvertToString(), "NHRS_BANK", "BANK_CD");
            string xmlData;
            string exc = string.Empty;

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ReverseFeed/";
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string file_name = file.FileName.Replace(" ", "_");

                DataTable oDataTable = new DataTable();
                string extension = Path.GetExtension(finalPath);

                oDataTable = Import_To_Grid(finalPath, extension, "Yes");

                using (var ds = new DataSet())
                {
                    ds.Tables.Add(oDataTable);
                    xmlData = ds.GetXml();
                }

                rs = objService.BulkUploadPSP(xmlData, out exc);

                if (rs > 0)
                {
                    Session["PO_CONFLICT"] = "SUCCESSFUL";
                }
                else
                {

                }
            }

            return View();
        }
        #endregion
        //second tranche bulk upload
        public ActionResult ImportSecondTranche()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);

                }

            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                ViewData["ddl_Installment"] = common.GetInstallation("");
                ViewData["ddlbankname"] = common.GetBankCdByDistrictCd("", "");
            }

            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["MESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["MESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";

            if (Session["ErrorFiles"] != null)
            {
                ViewData["ErrorFiles"] = Session["ErrorFiles"];
                Session["ErrorFiles"] = null;
            }

            return View();
        }
        [HttpPost]
        public ActionResult ImportSecondTranche(HttpPostedFileBase file, FormCollection fc)
        {
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int rs = 0;
            string bank_cd = common.GetCodeFromDataBase(fc["ddl_Bank"].ConvertToString(), "NHRS_BANK", "BANK_CD");
            string branch_standard_cd = fc["ddl_BankBranch"].ConvertToString();
            Session["PO_CONFLICT"] = "";
            string xmlData;
            DataTable errorList = new DataTable();


            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ReverseFeed/"; // location to save file
                string File_Name = file.FileName.Replace(" ", "_");//replacing white space by _
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //saving file to server
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(File_Name));

                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.JSONFileListInDB(); //getting saved file names from db
                    if (!fileListInDB.Contains(File_Name)) //checks if file already exists in db
                    {
                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath); // gets extension of file

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");// function to convert excel to datatable

                            if (!oDataTable.Columns.Contains("SN"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Please copy data to another excel sheet and upload again!";
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                            if (!oDataTable.Columns.Contains("BRANCH_CODE") || !oDataTable.Columns.Contains("PA_NO") ||
                                !oDataTable.Columns.Contains("RECIPIENT_NAME") || !oDataTable.Columns.Contains("AC_NO") ||
                                !oDataTable.Columns.Contains("INSTALLMENT") || !oDataTable.Columns.Contains("DEPOSIT_AMT") 
                                || !oDataTable.Columns.Contains("DEPOSITED_DATE(mm/dd/yyyyAD)") || !oDataTable.Columns.Contains("REMARKS"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Column name invalid! Please enter column name as provided by NRA";
                                return RedirectToAction("ImportReverseFeedFile");
                            }


                            //adding new columns in datatable
                            oDataTable.Columns.Add("FILENAME", typeof(System.String));
                            oDataTable.Columns.Add("BANKCD", typeof(System.String));
                            oDataTable.Columns.Add("DEPOSITED_DATE", typeof(System.String));
                            oDataTable.Columns.Add("ENTERED_BY", typeof(System.String));

                            foreach (DataRow row in oDataTable.Rows)
                            {
                               if (row["INSTALLMENT"].ConvertToString() != "2")
                                {
                                    Session["PO_CONFLICT"] = "Installment Invalid! Please review file!";
                                    return RedirectToAction("ImportSecondTranche");
                                }
                                //inserting values into new column
                                row["FILENAME"] = File_Name;
                                row["BANKCD"] = bank_cd;
                                row["DEPOSITED_DATE"] = row["DEPOSITED_DATE(mm/dd/yyyyAD)"].ConvertToString("MM/dd/yyyy");
                                row["ENTERED_BY"] = SessionCheck.getSessionUsername().ToString();
                            }

                            //removing unwanted columns
                            oDataTable.Columns.Remove("DEPOSITED_DATE(mm/dd/yyyyAD)");

                            if (oDataTable.Rows.Count > 0)
                            {
                                using (var ds = new DataSet())
                                {
                                    ds.Tables.Add(oDataTable);
                                    xmlData = ds.GetXml(); // converting datatable to xml
                                }

                                rs = objService.BulkUploadOtherTranche(xmlData, out exc);

                                if (rs > 0)
                                {
                                    Session["PO_CONFLICT"] = "SUCCESSFUL";
                                    errorList = objService.GetSecondTrancheError(bank_cd,"",rs,"2");
                                    Session["ErrorFiles"] = errorList;
                                    return RedirectToAction("ImportSecondTranche");

                                }
                                else
                                {
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                                    Session["PO_CONFLICT"] = exc;
                                }
                            }
                            else
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                return RedirectToAction("ImportSecondTranche");
                            }

                        }
                        else
                        {
                            Session["PO_CONFLICT"] = "Invalid File. Please check file extension";
                        }
                    }
                    else
                    {
                        Session["PO_CONFLICT"] = "Duplicate File";
                    }
                }
                catch (Exception ex)
                {
                    Session["PO_CONFLICT"] = ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                    return RedirectToAction("ImportSecondTranche");

                }
            }
            return RedirectToAction("ImportSecondTranche");
        }
        public ActionResult ImportThirdTranche()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd);

                }

            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                ViewData["ddl_Installment"] = common.GetInstallation("");
                ViewData["ddlbankname"] = common.GetBankCdByDistrictCd("", "");
            }

            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["MESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["MESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";

            if (Session["ErrorFiles"] != null)
            {
                ViewData["ErrorFiles"] = Session["ErrorFiles"];
                Session["ErrorFiles"] = null;
            }


            return View();
        }
        [HttpPost]
        public ActionResult ImportThirdTranche(HttpPostedFileBase file, FormCollection fc)
        {
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int rs = 0;
            string bank_cd = common.GetCodeFromDataBase(fc["ddl_Bank"].ConvertToString(), "NHRS_BANK", "BANK_CD");
            string branch_standard_cd = fc["ddl_BankBranch"].ConvertToString();
            Session["PO_CONFLICT"] = "";
            string xmlData;
            DataTable errorList = new DataTable();


            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/ReverseFeed/"; // location to save file
                string File_Name = file.FileName.Replace(" ", "_");//replacing white space by _
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //saving file to server
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(File_Name));

                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.JSONFileListInDB(); //getting saved file names from db
                    if (!fileListInDB.Contains(File_Name)) //checks if file already exists in db
                    {
                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath); // gets extension of file

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");// function to convert excel to datatable

                            if (!oDataTable.Columns.Contains("SN"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Please copy data to another excel sheet and upload again!";
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                            if (!oDataTable.Columns.Contains("BRANCH_CODE") || !oDataTable.Columns.Contains("PA_NO") ||
                                !oDataTable.Columns.Contains("RECIPIENT_NAME") || !oDataTable.Columns.Contains("AC_NO") ||
                                !oDataTable.Columns.Contains("INSTALLMENT") || !oDataTable.Columns.Contains("DEPOSIT_AMT")
                                || !oDataTable.Columns.Contains("DEPOSITED_DATE(mm/dd/yyyyAD)") || !oDataTable.Columns.Contains("REMARKS"))
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! Column name invalid! Please enter column name as provided by NRA";
                                return RedirectToAction("ImportReverseFeedFile");
                            }

                            //adding new columns in datatable
                            oDataTable.Columns.Add("FILENAME", typeof(System.String));
                            oDataTable.Columns.Add("BANKCD", typeof(System.String));
                            oDataTable.Columns.Add("DEPOSITED_DATE", typeof(System.String));
                            oDataTable.Columns.Add("ENTERED_BY", typeof(System.String));

                            foreach (DataRow row in oDataTable.Rows)
                            {
                                if (row["INSTALLMENT"].ConvertToString() != "3")
                                {
                                    Session["PO_CONFLICT"] = "Installment Invalid! Please review file!";
                                    return RedirectToAction("ImportThirdTranche");
                                }
                                //inserting values into new column
                                row["FILENAME"] = File_Name;
                                row["BANKCD"] = bank_cd;
                                row["DEPOSITED_DATE"] = row["DEPOSITED_DATE(mm/dd/yyyyAD)"].ConvertToString("MM/dd/yyyy");
                                row["ENTERED_BY"] = SessionCheck.getSessionUsername().ToString();
                            }

                            //removing unwanted columns
                            oDataTable.Columns.Remove("DEPOSITED_DATE(mm/dd/yyyyAD)");

                            if (oDataTable.Rows.Count > 0)
                            {
                                using (var ds = new DataSet())
                                {
                                    ds.Tables.Add(oDataTable);
                                    xmlData = ds.GetXml(); // converting datatable to xml
                                }

                                rs = objService.BulkUploadOtherTranche(xmlData, out exc);

                                if (rs > 0)
                                {
                                    Session["PO_CONFLICT"] = "SUCCESSFUL";
                                    errorList = objService.GetSecondTrancheError(bank_cd, "", rs, "3");
                                    Session["ErrorFiles"] = errorList;
                                    return RedirectToAction("ImportThirdTranche");

                                }
                                else
                                {
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name)); //delete file from if not uploaded
                                    Session["PO_CONFLICT"] = exc;
                                }
                            }
                            else
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                return RedirectToAction("ImportThirdTranche");
                            }

                        }
                        else
                        {
                            Session["PO_CONFLICT"] = "Invalid File. Please check file extension";
                        }
                    }
                    else
                    {
                        Session["PO_CONFLICT"] = "Duplicate File";
                    }
                }
                catch (Exception ex)
                {
                    Session["PO_CONFLICT"] = ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                    return RedirectToAction("ImportThirdTranche");

                }
            }
            return RedirectToAction("ImportThirdTranche");

        }
        public ActionResult GetSecondTrancheError()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.GetBankName(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd.ToString());
                }
            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            }

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "deleteSuccess")
                {

                    ViewData["FinalMessage"] = "Data deleted successfully.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "deleteFailed")
                {

                    ViewData["FailedMessage"] = "Data Delete Failed";

                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongUser")
                {

                    ViewData["FailedMessage"] = "This user is not authorized to delete the data.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "NOTAUTH")
                {

                    ViewData["FailedMessage"] = "You are not Authorized to delete this data! Please contact Bank Admin User.";

                }

            }
            Session["ImportMessage"] = "";

            return View();
        }
        [HttpPost]
        public ActionResult GetSecondTrancheError(FormCollection fc)
        {
            DataTable dt = new DataTable();
            string bank_cd;
            string branch_cd;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            if (fc != null)
            {
                bank_cd = fc["ddl_Bank"].ConvertToString();
                branch_cd = fc["ddl_BankBranch"].ConvertToString();
                dt = objService.GetSecondTrancheError(bank_cd, branch_cd, 0, "2");
            }

            Session["ErrorFiles"] = dt;
            ViewData["ErrorFiles"] = dt;

            return PartialView("~/Views/PaymentImport/_ListPaymentError.cshtml");
        }
        public ActionResult GetThirdTrancheError()
        {
            if (CommonVariables.GroupCD == "50" || CommonVariables.GroupCD == "55")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.GetBankName(bank_cd);
                    ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID(bank_cd.ToString());
                }
            }
            else
            {
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            }

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "deleteSuccess")
                {

                    ViewData["FinalMessage"] = "Data deleted successfully.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "deleteFailed")
                {

                    ViewData["FailedMessage"] = "Data Delete Failed";

                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongUser")
                {

                    ViewData["FailedMessage"] = "This user is not authorized to delete the data.";

                }
                else if (Session["ImportMessage"].ConvertToString() == "NOTAUTH")
                {

                    ViewData["FailedMessage"] = "You are not Authorized to delete this data! Please contact Bank Admin User.";

                }

            }
            Session["ImportMessage"] = "";

            return View();
        }
        [HttpPost]
        public ActionResult GetThirdTrancheError(FormCollection fc)
        {
            DataTable dt = new DataTable();
            string bank_cd;
            string branch_cd;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            if (fc != null)
            {
                bank_cd = fc["ddl_Bank"].ConvertToString();
                branch_cd = fc["ddl_BankBranch"].ConvertToString();
                dt = objService.GetSecondTrancheError(bank_cd, branch_cd, 0, "3");
            }


            Session["ErrorFiles"] = dt;
            ViewData["ErrorFiles"] = dt;

            return PartialView("~/Views/PaymentImport/_ListPaymentError.cshtml");
        }

        public JsonResult CheckIfDuplicateDataExists(string bankid)
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            int duplicateValue = objService.CheckDuplicateData(bankid);
            if(duplicateValue > 0)
            {
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult DeleteDuplicatePaymentData(string bankid)
        {
           ReverseBankFileImportService objService = new ReverseBankFileImportService();
           QueryResult status = null;

            status = objService.DeletePaymentDuplicateData(bankid);
            if (status.IsSuccess)
            {
                return Json(true);
            }
            return Json(false);
        }
    }

}


