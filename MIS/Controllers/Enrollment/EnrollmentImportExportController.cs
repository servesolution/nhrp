using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.Enrollment;
using MIS.Services.Core;
using MIS.Services.Enrollment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Enrollment
{
    public class EnrollmentImportExportController : BaseController
    {
        //
        // GET: /EnrollmentImportExport/

        public ActionResult EnrollmentBankList()
        {
            CheckPermission();
            EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            CommonFunction common = new CommonFunction();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            ViewData["ResearchEnrollment"] = TempData["ResearchEnrollment"];

            return View(objEnrollmentSearch);
        }
        [HttpPost]
        public ActionResult EnrollmentBankList(EnrollmentSearch objEnrollment, FormCollection fc)
        {
            CheckPermission();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            objEnrollment = new EnrollmentSearch();

            objEnrollment.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            objEnrollment.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
            if (fc["chkReExport"].Contains("true"))
            {
                List<string> exportedFiles = Directory.GetFiles(Server.MapPath("~/Excel/EnrollmentImportExport/"), "*.xls")
                                         .Select(path => Path.GetFileName(path))
                                         .ToList();
                DataTable dtbl = new DataTable();
                DataRow row;
                dtbl.Columns.Add("FILE_NAME", typeof(String));
                foreach (var i in exportedFiles)
                {
                    if (i.Contains(fc["hdDistrictVDC"]))
                    {
                        row = dtbl.NewRow();
                        row["FILE_NAME"] = i.ToString();
                        dtbl.Rows.Add(row);
                    }
                }
                ViewData["dtexportedFiles"] = dtbl;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();
                return PartialView("~/Views/EnrollmentImportExport/_FileList.cshtml", objEnrollment);
            }
            else
            {
                result = enrollService.GetBankList(objEnrollment.DistrictCd, objEnrollment.VDCMun);
                ViewData["dtBankListresult"] = result;

                Session["dtBankList"] = result;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();

                return PartialView("~/Views/EnrollmentImportExport/_BankList.cshtml", objEnrollment);
            }
        }

        public ActionResult EnrollmentBankAccountList()
        {
            CheckPermission();
            EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            CommonFunction common = new CommonFunction();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Bank"] = common.GetBankNameByVDC("", "", "","");// common.get("", "");
            ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
            ViewData["ResearchEnrollment"] = TempData["ResearchEnrollment"];

            return View(objEnrollmentSearch);
        }
        [HttpPost]
        public ActionResult EnrollmentBankAccountList(EnrollmentSearch objEnrollment, FormCollection fc)
        {
            CheckPermission();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            objEnrollment = new EnrollmentSearch();

            objEnrollment.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            objEnrollment.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
            if (fc["chkReExport"].Contains("true"))
            {
                List<string> exportedFiles = Directory.GetFiles(Server.MapPath("~/Excel/EnrollmentImportExport/"), "*.xls")
                                         .Select(path => Path.GetFileName(path))
                                         .ToList();
                DataTable dtbl = new DataTable();
                DataRow row;
                dtbl.Columns.Add("FILE_NAME", typeof(String));
                foreach (var i in exportedFiles)
                {
                    if (i.Contains(fc["hdDistrictVDC"]))
                    {
                        row = dtbl.NewRow();
                        row["FILE_NAME"] = i.ToString();
                        dtbl.Rows.Add(row);
                    }
                }
                ViewData["dtexportedFiles"] = dtbl;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();
                return PartialView("~/Views/EnrollmentImportExport/_FileList.cshtml", objEnrollment);
            }
            else
            {
                result = enrollService.GetBankList(objEnrollment.DistrictCd, objEnrollment.VDCMun);
                ViewData["dtBankListresult"] = result;

                Session["dtBankList"] = result;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();

                return PartialView("~/Views/EnrollmentImportExport/_BankList.cshtml", objEnrollment);
            }
        }
        public ActionResult UpdateEnrollmentBankList()
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable dtFileBatch = new DataTable();
            dtFileBatch = enrollService.GetBankBatchStatus("", "");
            ViewData["dtBankListresult"] = dtFileBatch;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateEnrollmentBankList(FormCollection fc)
        {
            string batchID = "";
            string filename = "";
            bool fileexist = false;
            bool res = false;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.ContentLength == 0)
                    continue;

                string FilePath = "~/Excel/EnrollmentImportExport/ImportFile/";
                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                }

                string[] splitName = Path.GetFileName(file.FileName).Split('_');
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName));
                //DataTable dtCollectionList = GetDataFromExcel(finalPath, ".xlsx", "Yes", splitName[0] + "_" + splitName[1] + "_Enrollment");

                int num_rows = 0;
                int num_cols = 0;
                string[,] valuesOwners = LoadCsvTab(finalPath);
                num_rows = valuesOwners.GetUpperBound(0) + 1;
                num_cols = valuesOwners.GetUpperBound(1) + 1;
                DataTable dtEnrollBankList = new DataTable();
                for (int c = 0; c < num_cols; c++)
                {
                    dtEnrollBankList.Columns.Add(valuesOwners[0, c].Replace("\"", ""));
                }
                for (int r = 1; r < num_rows; r++)
                {
                    dtEnrollBankList.Rows.Add();
                    for (int c = 0; c < num_cols; c++)
                    {
                        dtEnrollBankList.Rows[r - 1][c] = valuesOwners[r, c].Replace("\"", "");
                    }
                }
                EnrollmentImportExport enrollService = new EnrollmentImportExport();
                DataTable dtFileBatch = new DataTable();
                filename = Path.GetFileName(file.FileName);
                dtFileBatch = enrollService.GetBankBatchStatus(filename, "N");
                if (dtFileBatch != null)
                {
                    if (dtFileBatch.Rows.Count > 0)
                    {
                        batchID = dtFileBatch.Rows[0]["BATCH_ID"].ConvertToString();
                        fileexist = true;
                        foreach (DataRow dr in dtEnrollBankList.Rows)
                        {
                            enrollService = new EnrollmentImportExport();
                            res = enrollService.UpdateEnrollmentBankDetail(dr["AGREEMENT"].ConvertToString(), dr["BANK_NAME"].ConvertToString(), dr["ACCOUNT_NO"].ConvertToString(), dr["ACCOUNT_TYPE"].ConvertToString(), dr["REMARKS"].ConvertToString(), batchID);
                        }
                    }
                }

            }
            if (res)
            {
                EnrollmentImportExport enrollService = new EnrollmentImportExport();
                enrollService.UpdateEnrollmentBankBatch(filename, "", "Y");
                ViewData["FinalMessage"] = "File uploaded successfully.";
            }
            else
            {
                if (fileexist)
                {
                    EnrollmentImportExport enrollService = new EnrollmentImportExport();
                    enrollService.RollBackEnrollmentBankBatch(batchID);
                    ViewData["FinalMessage"] = "Error on upload.";
                }
                else
                {
                    ViewData["FinalMessage"] = "This File is already uploaded or the file name doesn't match.";
                }
            }

            return View();
        }
        private string[,] LoadCsvTab(string filename)
        {
            string whole_file = System.IO.File.ReadAllText(filename);

            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            int num_rows = lines.Length;
            int num_cols = lines[0].Split('\t').Length;

            string[,] values = new string[num_rows, num_cols];
            //string[] values = Regex.Split(value, "\r\n");
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            for (int r = 0; r < num_rows; r++)
            {
                //if(csvSplit.Match(lines[r]))
                //{

                //}
                string[] line_r = SplitCSVTab(lines[r]); //lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }

            return values;
        }
        public static string[] SplitCSVTab(string input)
        {
            Regex csvSplit = new Regex("(?:^|\t)(\"(?:[^\"]+|\"\")*\"|[^\t]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart('\t'));
            }

            return list.ToArray<string>();
        }


        #region Export To Excel

        public FileResult DownLoadExistingFile(string id)
        {
            string filepath = Server.MapPath("~/Excel/EnrollmentImportExport/" + id);
            return File(filepath, "application/vnd.ms-excel", Path.GetFileName(filepath));
        }

        public ActionResult ExportExcel(string searchType, string distcode, string vdccode)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtBankList"];

            //dt = ColumnsPreparation(dt, searchType);
            //dt = SetOrdinals(dt, searchType);
            string filepath = Server.MapPath("~/Excel/EnrollmentImportExport/EnrollmentBankList.xls");
            string Uniquefilename = dt.Rows[0]["DistrictName"] + "_" + dt.Rows[0]["VDCName"] + "_" + "EnrollmentBankList" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".xls";
            ExcelExport(dt, Uniquefilename);

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string btcID = enrollService.AddEnrollmentBankBatch(Uniquefilename, GetData.GetCodeFor(DataType.District, distcode), GetData.GetCodeFor(DataType.VdcMun, vdccode), "");
            foreach (DataRow dr in dt.Rows)
            {
                enrollService = new EnrollmentImportExport();
                enrollService.UpdateEnrollmentDownloadFlag(dr["AGREEMENT"].ToString(), btcID);
            }

            return File(filepath, "application/xlsx", "EnrollmentBankList.xls");
        }

        public DataTable ColumnsPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();

            requiredCols.Add("TARGET_BATCH_ID");
            requiredCols.Add("INSTANCE_UNIQUE_SNO");
            requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            requiredCols.Add("ADDRESS");
            requiredCols.Add("MOU SIGNED STATUS");

            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();

            dtModified.Columns["TARGET_BATCH_ID"].SetOrdinal(0);
            dtModified.Columns["INSTANCE_UNIQUE_SNO"].SetOrdinal(1);
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(2);
            dtModified.Columns["ADDRESS"].SetOrdinal(3);
            dtModified.Columns["MOU SIGNED STATUS"].SetOrdinal(4);
            dtModified.Columns["TARGET_BATCH_ID"].ColumnName = Utils.GetLabel("Batch ID");
            dtModified.Columns["INSTANCE_UNIQUE_SNO"].ColumnName = Utils.GetLabel("House ID");
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name");
            dtModified.Columns["ADDRESS"].ColumnName = Utils.GetLabel("Address");
            dtModified.Columns["MOU SIGNED STATUS"].ColumnName = Utils.GetLabel("MOU Signed Status");

            return dtModified;
        }

        protected void ExcelExport(DataTable dtRecords, string Uniquefilename)
        {
            FileStream fs = new FileStream(Server.MapPath(@"~/Excel/EnrollmentImportExport/" + Uniquefilename), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter m_streamWriter = new StreamWriter(fs);

            string finalval = "";

            string XlsPath = Server.MapPath(@"~/Excel/EnrollmentImportExport/" + Uniquefilename);
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("S NO."));
                finalval = tab + Utils.GetLabel("S NO.");
                tab = "\t";

                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {
                    Response.Write(tab + datacol.ColumnName);
                    finalval = finalval + tab + datacol.ColumnName;
                    tab = "\t";

                }
                Response.Write("\n");
                finalval = finalval + "\n";

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;
                    finalval = finalval + tab + Utils.GetLabel(Convert.ToString(rowCount));
                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";

                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        finalval = finalval + tab + Utils.GetLabel(Convert.ToString(dr[j]));
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }

                    Response.Write("\n");
                    finalval = finalval + "\n";
                }


                m_streamWriter.WriteLine(finalval);
                m_streamWriter.Flush();


                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        #endregion
        #region Check Permission
        public void CheckPermission()
        {
            PermissionParamService objPermissionParamService = new PermissionParamService();
            PermissionParam objPermissionParam = new PermissionParam();
            ViewBag.EnableEdit = "false";
            ViewBag.EnableDelete = "false";
            ViewBag.EnableAdd = "false";
            ViewBag.EnableApprove = "false";
            try
            {
                objPermissionParam = objPermissionParamService.GetPermissionValue();
                if (objPermissionParam != null)
                {

                    if (objPermissionParam.EnableAdd == "true")
                    {
                        ViewBag.EnableAdd = "true";
                    }
                    if (objPermissionParam.EnableEdit == "true")
                    {
                        ViewBag.EnableEdit = "true";
                    }
                    if (objPermissionParam.EnableDelete == "true")
                    {
                        ViewBag.EnableDelete = "true";
                    }
                    if (objPermissionParam.EnableApprove == "true")
                    {
                        ViewBag.EnableApprove = "true";
                    }
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


        }
        #endregion

    }
}
