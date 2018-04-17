using ExceptionHandler;
using MIS.Models.Inspection;
using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using MIS.Models.Setup.Inspection;
using CsvHelper;
using MIS.Models.Core;
using System.Data.OracleClient;
using MIS.Models.Security;
using System.Configuration;
using System.Data.OleDb;

namespace MIS.Models.Inspection
{
    public class InspectionImportController : BaseController
    {
        //
        // GET: /InspectionImport/

        // Import inspection 
        CommonFunction com = new CommonFunction();
        public ActionResult ImportInspection()
        {
           
            ViewData["ddl_District"] = com.GetDistricts("");
            ViewData["ddl_VDCMun"] = com.GetVDCMunByDistrict("", "");
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
                else if (Session["ImportMessage"].ConvertToString() == "Found Duplicate Records")
                {
                    ViewData["FinalMessage"] = "Imported successfully and found some duplicate data!!!";
                }
                else if (Session["ImportMessage"].ConvertToString() == "b")
                {
                    ViewData["FinalMessage"] = "Imported successfully without duplicate data";
                }
                else if (Session["ImportMessage"].ConvertToString() == "NoPA")
                {
                    ViewData["FinalMessage"] = Session["ErrorMessage"].ConvertToString();
                }
                else if (Session["ImportMessage"].ConvertToString() == "NoDist")
                {
                    ViewData["FinalMessage"] = Session["ErrorMessage"].ConvertToString();
                }
                ViewData["FileNameDup"] = Session["FileNameDup"].ConvertToString();
                Session["FileNameDup"] = null;
                Session["ImportMessage"] = "";
            }
            return View();
        }


        public ActionResult InspectionImport()
        {
            return View();
        }
        [HttpPost]
      
        public ActionResult ImportInspection(FormCollection fc, HttpPostedFileBase file) 
        //(FormCollection fc)
        {
            InspectionImportService objService = new InspectionImportService();
            CommonFunction commonFC = new CommonFunction(); 
            string exc = string.Empty;
            bool rs = false;
            string DistCdFile = "";
            string VdcCdFile = "";

            string District = fc["ddl_District"].ConvertToString();
            string VDC = fc["ddl_VDCMun"].ConvertToString();
            string DistrictCode = Session["distcode"].ConvertToString();
            string VDCDefinedCode = Session["VDC"].ConvertToString();
            string VDCCode = commonFC.GetCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/InspectionImport/";


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
                                    if (objService.CheckUpdate(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), oDataTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString(), oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString()))
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
                   
                            if (oDataTable.Rows.Count < 1)
                            {
                                ViewData["FinalMessage"] = "Duplicate Data";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("ImportInspection");
                            }
                            if (CheckNull == true)
                            {
                                ViewData["FinalMessage"] = "Empty";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("ImportInspection");
                            }
                            rs = objService.SaveExcelDataFromFileBrowse(oDataTable, file.FileName, out exc, DistCdFile, VdcCdFile);
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


                                DataTable dtUpdate = new DataTable();
                                dtUpdate = oDataTable.Clone();
                                Conflicted:
                                for (int i = (oDataTable.Rows.Count - 1); i >= 0; i--)
                                {
                                    if (oDataTable.Rows[i][1].ConvertToString() != "")
                                    {
                                        Session["FileNameDup"] = file.FileName;




                                        //check inspection
                                        if (oDataTable.Rows[i]["INSPECTION_NUMBER"].ToDecimal() !=1  )
                                        {

                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Invalid inspection code at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                null,
                                                null,
                                                null, file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }

                                        //check district cd 
                                        if (oDataTable.Rows[i]["DISTRICT"].ToDecimal() < 0 || oDataTable.Rows[i]["DISTRICT"].ToDecimal() > 76)
                                        {

                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Invalid district code at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                null,
                                                null,
                                                null, file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }

                                        //check vdc cd 

                                        if (!objService.Checkvdc(oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString()))
                                        {
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Invalid Pa numberat row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                 null,
                                                null, file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }
                                        //check pa
                                        if (!objService.CheckPa(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString()))
                                        {
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Invalid Pa numberat row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }
                                        //if (objService.CheckDuplicate(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString()))
                                        if (objService.CheckDuplicate(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(), oDataTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString()))
                                        {
                                            string reason = "Duplicate";
                                            Session["fileName"] = file.FileName.ConvertToString();
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }


                                        // //check if Beneficiary name
                                        if (oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString()=="")
                                        {
                                             ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Beneficiary name null at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }


                                        // //check if INSPECTION_TYPE is null or invalid
                                        if (oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal() < 1 || oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal() > 7 || oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal()==null)
                                        {
                                              ViewData["FinalMessage"] = "Found Duplicate Records";
                                             string reason = "Invalid Inspection type at row position " + (i + 1) ;
                                             DataTable dtttt = new DataTable();
                                             bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                 oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                 oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                 oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                 oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                 oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                             oDataTable.Rows.RemoveAt(i);
                                             goto Conflicted;
                                        }


                                        if (oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal() >0 && oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal() <7)
                                        {
                                              if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal()!= null )
                                              {
                                                  ViewData["FinalMessage"] = "Found Duplicate Records";
                                                  string reason = "Build material code must be null at row position " + (i + 1);
                                                  DataTable dtttt = new DataTable();
                                                  bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                      oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                      oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                      oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                      oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                      oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                                  oDataTable.Rows.RemoveAt(i);
                                                  goto Conflicted;
                                              }
                                              if (oDataTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal() != null)
                                              {
                                                  ViewData["FinalMessage"] = "Found Duplicate Records";
                                                  string reason = "roof material code must be null at row position " + (i + 1);
                                                  DataTable dtttt = new DataTable();
                                                  bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                      oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                      oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                      oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                      oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                      oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                                  oDataTable.Rows.RemoveAt(i);
                                                  goto Conflicted;
                                              }
                                           
                                        }

                                        if (oDataTable.Rows[i]["INSPECTION_TYPE"].ToDecimal() == 7)
                                        {
                                            if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() < 1 || oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() > 8 || oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() == null)
                                            {
                                                ViewData["FinalMessage"] = "Found Duplicate Records";
                                                string reason = "Inspection type and building material does not comply at row position " + (i + 1);
                                                DataTable dtttt = new DataTable();
                                                bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                    oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                    oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                    oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                    oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                    oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                                oDataTable.Rows.RemoveAt(i);
                                                goto Conflicted;
                                            }


                                            if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() >0  || oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() <5 )
                                            {
                                                if(oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() ==1 )
                                                {
                                                    if (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() < 16 || (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() >16))
                                                    {
                                                        goto SaveConflict;
                                                    }
                                                    goto NoBuildMatIssue;
                                                }
                                                else if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() == 2)
                                                {
                                                    if (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() < 1 || oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() > 1 )
                                                    {
                                                        goto SaveConflict;
                                                    }
                                                    goto NoBuildMatIssue;
                                                }
                                                else if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() == 3)
                                                {
                                                    if (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal()<17 || oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() > 17)
                                                    {
                                                        goto SaveConflict;
                                                    }
                                                    goto NoBuildMatIssue;
                                                }
                                                else if (oDataTable.Rows[i]["BUILD_MATERIAL"].ToDecimal() == 4)
                                                {
                                                    if (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() >9 || oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() < 9 )
                                                    {
                                                        goto SaveConflict;
                                                    }
                                                    goto NoBuildMatIssue;
                                                }
                                                else
                                                {
                                                    goto NoBuildMatIssue;
                                                }
                                            SaveConflict:
                                                 
                                                    ViewData["FinalMessage"] = "Found Duplicate Records";
                                                    string reason = "Building material and design number does not comply at row position " + (i + 1);
                                                    DataTable dtttt = new DataTable();
                                                    bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                        oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                        oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                        oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                        oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                        oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                                    oDataTable.Rows.RemoveAt(i);
                                                    goto Conflicted; 
                                               
                                            }
                                        NoBuildMatIssue:
                                            if (oDataTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal() < 1 || oDataTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal() > 8)
                                            {
                                                ViewData["FinalMessage"] = "Found Duplicate Records";
                                                string reason = "Inspection type and roof material does not comply at row position " + (i + 1);
                                                DataTable dtttt = new DataTable();
                                                bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                    oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                    oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                    oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                    oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                    oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                                oDataTable.Rows.RemoveAt(i);
                                                goto Conflicted;
                                            }
                                            
                                        }


                                        //check if INSPECTION_TYPE and DESIGN_NUMBER match 
                                        if (oDataTable.Rows[i]["DESIGN_NUMBER"].ConvertToString() != "" )
                                        {
                                            if (oDataTable.Rows[i]["INSPECTION_TYPE"].ConvertToString() == "2" && oDataTable.Rows[i]["DESIGN_NUMBER"].ConvertToString() != "16")
                                            {
                                                ViewData["FinalMessage"] = "Inspection type and design number doesnot match at row position " + (i + 1) + " .Data Import failed.";
                                            }
                                            else  if (oDataTable.Rows[i]["INSPECTION_TYPE"].ConvertToString() == "3" && (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal()<0 || oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal()>8))
                                            {
                                                ViewData["FinalMessage"] = "Inspection type and design number doesnot match at row position " + (i + 1) + " .Data Import failed.";
                                            }
                                            else if (oDataTable.Rows[i]["INSPECTION_TYPE"].ConvertToString() == "4" && oDataTable.Rows[i]["DESIGN_NUMBER"].ConvertToString() != "17")
                                            {
                                                ViewData["FinalMessage"] = "Inspection type and design number doesnot match at row position " + (i + 1) + " .Data Import failed.";
                                            }
                                            else if (oDataTable.Rows[i]["INSPECTION_TYPE"].ConvertToString() == "5" && (oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() < 9 || oDataTable.Rows[i]["DESIGN_NUMBER"].ToDecimal() > 15))
                                            {
                                                ViewData["FinalMessage"] = "Inspection type and design number doesnot match at row position " + (i + 1) + " .Data Import failed.";
                                            }
                                            else  
                                            {
                                                goto nextCheck;
                                            }
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Inspection type and design number doesnot match at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }
                                    nextCheck:
                                        //check if name of person who accept all inspection data is true
                                        if (oDataTable.Rows[i]["HOUSE_OWNER_S_NAME"].ConvertToString() == "" || oDataTable.Rows[i]["HOUSE_OWNER_S_NAME"].ConvertToString() == null)
                                        {
                                            
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "The persons name field who accept all the inspected data as true is null at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }

                                       //check if inspection level null
                                    if (oDataTable.Rows[i]["INSPECTION_NUMBER"].ToDecimal() < 1 || oDataTable.Rows[i]["INSPECTION_NUMBER"].ToDecimal() > 3 || oDataTable.Rows[i]["INSPECTION_NUMBER"].ToDecimal() == null)
                                        {
                                            
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                            string reason = "Invalid Inspection level at row position " + (i + 1);
                                            DataTable dtttt = new DataTable();
                                            bool saveDuplicate = objService.saveDuplicateInspectionData(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString(),
                                                oDataTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                                                oDataTable.Rows[i]["DISTRICT"].ConvertToString(),
                                                oDataTable.Rows[i]["VDC_MUNICIPALITY"].ConvertToString(),
                                                oDataTable.Rows[i]["WARD"].ConvertToString(), file.FileName, reason);

                                            oDataTable.Rows.RemoveAt(i);
                                            goto Conflicted;
                                        }

                                      

                                        //else if (objService.CheckUpdate(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), oDataTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString()))
                                        else if (objService.CheckUpdate(oDataTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), oDataTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString(), oDataTable.Rows[i]["FORM_NUMBER"].ConvertToString()))
                                        {


                                            dtUpdate.ImportRow(oDataTable.Rows[i]);
                                            oDataTable.Rows.RemoveAt(i);
                                            Session["fileName"] = file.FileName.ConvertToString();
                                            ViewData["FinalMessage"] = "Found Duplicate Records";

                                        }

                                   
                                    }
                                  
                                    else
                                    {
                                        if (oDataTable.Rows[i][1] == DBNull.Value)
                                            oDataTable.Rows[i].Delete();
                                    }
                                    oDataTable.AcceptChanges();
                                }

                                if (dtUpdate.Rows.Count > 0)
                                {
                                    rs = objService.UpdateExcelDataFromFileBrowse(dtUpdate, file.FileName, out exc, District, VDC);
                                }

                            }
                            if(oDataTable.Rows.Count>0)
                            {
                                //rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                                rs = objService.SaveExcelDataFromFileBrowse(oDataTable, file.FileName, out exc, District, VDC);
                            }
                            else
                            {
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                Session["ErrorMessage"] = ViewData["ErrorMessage"];
                                return RedirectToAction("ImportInspection");
                            }
                           
                        }

                        //rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                        if (rs)
                        {
                            if (ViewData["FinalMessage"].ConvertToString() == "Found Duplicate Records")
                            {
                                ViewData["FinalMessage"] = "Found Duplicate Records";
                            }
                            else
                            {
                                ViewData["FinalMessage"] = "b";
                            }
                           
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
            return RedirectToAction("ImportInspection");
            //        return RedirectToAction("OpenImport", new RouteValueDictionary(
            //new { controller = "Enrollment", action = "OpenImport", Import = "Success" }));
        }



        public ActionResult ImportInspectionRegistration()
        {
            ViewData["ddl_District"] = com.GetDistricts("");
            ViewData["ddl_VDCMun"] = com.GetVDCMunByDistrict("", "");
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
                else if (Session["ImportMessage"].ConvertToString() == "Found Duplicate Records")
                {
                    ViewData["FinalMessage"] = "Imported successfully and found some duplicate data!!!";
                }
                else if (Session["ImportMessage"].ConvertToString() == "b")
                {
                    ViewData["FinalMessage"] = "Imported successfully without duplicate data";
                }

                Session["ImportMessage"] = "";
            }
            return View();
        }


        public ActionResult InspectionImportRegistration()
        {
            ViewData["ddl_District"] = com.GetDistricts("");
            ViewData["ddl_VDCMun"] = com.GetVDCMunByDistrict("", "");
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
                if (Session["ImportMessage"].ConvertToString() == "Duplicate Data")
                {
                    ViewData["FinalMessage"] = "Duplicate Data";
                }
                if (Session["ImportMessage"].ConvertToString() == "Empty")
                {
                    ViewData["FinalMessage"] = "Empty";
                }
                    if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Duplicate File";
                }

                if (Session["ImportMessage"].ConvertToString() == "Failed.")
                {
                    ViewData["FinalMessage"] = "Failed.";
                }
                    
                Session["ImportMessage"] = null;
            }
            return View();
        }
        [HttpPost]

        public ActionResult ImportInspectionRegestration(FormCollection fc, HttpPostedFileBase file) 
         {
            InspectionImportService objService = new InspectionImportService();
            CommonFunction commonFC = new CommonFunction();
             string exc = string.Empty;
            bool rs = false;
            
            string District = fc["ddl_District"].ConvertToString();
            string VDC = fc["ddl_VDCMun"].ConvertToString();
            string DistrictCode = Session["distcode"].ConvertToString();
            string VDCDefinedCode = Session["VDC"].ConvertToString();
            string VDCCode = commonFC.GetCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/InspectionImport/InspectionApplicant";


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
                        //DataTable filterTable = new DataTable();
                        //string extension = Path.GetExtension(finalPath);
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            string reasonToSave ="";
                            oDataTable = Import_To_Grid(finalPath, ".xlsx", "Yes", "Sheet1");//, " ");

                            for (int i = (oDataTable.Rows.Count - 1); i >= 0; i--)
                            {
                                if (oDataTable.Rows[i][1].ConvertToString() != "")
                                {
                                    if (objService.CheckDuplicateApplication(oDataTable.Rows[i]["PA NO"].ConvertToString(), oDataTable.Rows[i]["Inspection"].ConvertToString()))
                                    {
                                        reasonToSave="Duplicate";
                                        bool saveDuplicate = objService.saveDuplicateApplicant(oDataTable.Rows[i]["PA NO"].ConvertToString(),
                                                oDataTable.Rows[i]["Inspection"].ConvertToString(),
                                                oDataTable.Rows[i]["Applicant"].ConvertToString(), file.FileName, reasonToSave);

                                        oDataTable.Rows.RemoveAt(i);
                                        ViewData["FinalMessage"] = "Some record not imported";
                                    }

                                    else if (objService.CheckPrevInspApproved(oDataTable.Rows[i]["PA NO"].ConvertToString(), oDataTable.Rows[i]["Inspection"].ConvertToString()))
                                    {
                                        reasonToSave="Previous Inspection Not Approved";
                                        bool savePrevNotApprove = objService.saveDuplicateApplicant(oDataTable.Rows[i]["PA NO"].ConvertToString(),
                                                oDataTable.Rows[i]["Inspection"].ConvertToString(),
                                                oDataTable.Rows[i]["Applicant"].ConvertToString(), file.FileName, reasonToSave);

                                        oDataTable.Rows.RemoveAt(i);
                                        ViewData["FinalMessage"] = "Some record not imported";
                                    }
                                    Session["fileNameApplicant"] = file.FileName;
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

                            if (oDataTable.Rows.Count < 1)
                            {
                                ViewData["FinalMessage"] = "Some record not imported";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("InspectionImportRegistration");
                            }
                            if (CheckNull == true)
                            {
                                ViewData["FinalMessage"] = "Empty";
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                return RedirectToAction("InspectionImportRegistration");
                            }
                            rs = objService.ImportInspectionRegistration(oDataTable, file.FileName, out exc, District, VDCCode);

                            //rs = objService.ExcelImportInspectionRegistrationSave(oDataTable, file.FileName, out exc, DistCdFile, VdcCdFile);
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

                                for (int i = (oDataTable.Rows.Count - 1); i >= 0; i--)
                                {
                                    if (oDataTable.Rows[i][1].ConvertToString() != "")
                                    {
                                        if (objService.CheckDuplicateApplication(oDataTable.Rows[i]["PA_NUMBER"].ConvertToString(), oDataTable.Rows[i]["INSPECTION_LEVEL"].ConvertToString()))
                                        {
                                            DataTable dtttt = new DataTable(); 
                                            oDataTable.Rows.RemoveAt(i);
                                            Session["fileName"] = file.FileName.ConvertToString();
                                            ViewData["FinalMessage"] = "Found Duplicate Records";
                                        }
                                    }
                                    else
                                    {
                                        if (oDataTable.Rows[i][1] == DBNull.Value)
                                            oDataTable.Rows[i].Delete();
                                    }
                                    oDataTable.AcceptChanges();
                                }

                            }
                            if (oDataTable.Rows.Count > 0)
                            {
                                //rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                                rs = objService.ImportInspectionRegistration(oDataTable, file.FileName, out exc, District, VDC);
                            }
                            else
                            {
                                Session["ImportMessage"] = ViewData["FinalMessage"];
                                Session["ErrorMessage"] = ViewData["ErrorMessage"];
                                return RedirectToAction("InspectionImportRegistration");
                            }

                        }

                        //rs = objService.SaveDataFromFileBrowse(oDataTable, file.FileName, out exc);
                        if (rs)
                        {

                            if (ViewData["FinalMessage"].ConvertToString() == "Some record not imported")
                            {
                                ViewData["FinalMessage"] = "Data Imported Successfully also found some conflicted reocord.";
                            }
                            else
                            {
                                ViewData["FinalMessage"] = "Data Imported Successfully.";

                            }
                            

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
            return RedirectToAction("InspectionImportRegistration");
            //        return RedirectToAction("OpenImport", new RouteValueDictionary(
            //new { controller = "Enrollment", action = "OpenImport", Import = "Success" }));
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


        //get duplicate data

        public ActionResult GetDuplicateImport(string district, string VDC, string filename)
        {
              InspectionImportService objService = new InspectionImportService();
            DataTable dt = new DataTable();
             Session["fileName"] = null;
            ViewData["ddl_District"] = com.GetDistricts("");
            ViewData["ddl_VDCMun"] = com.GetVDCMunByDistrict("", "");
            try
            {
                dt = objService.GetDuplicateImport(district, VDC, filename);
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
            return PartialView("_DuplicateImport");
        }

        public ActionResult GetConflictedRegistration(string district, string VDC)
        {
            InspectionImportService objService = new InspectionImportService();
            DataTable dt = new DataTable();
            string filename = Session["fileNameApplicant"].ConvertToString();
            Session["fileName"] = null;
            
            try
            {
                dt = objService.GetConflictedApplicant(filename);
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
            return PartialView("_ConflictedApplicant");
        }

        public DataTable GetDuplicateImportSearch(string district, string VDC)
        {
            InspectionImportService objService = new InspectionImportService();
            DataTable dt = new DataTable();
            ViewData["ddl_District"] = com.GetDistricts("");
            ViewData["ddl_VDCMun"] = com.GetVDCMunByDistrict("", "");
            string filename = Session["fileName"].ConvertToString();
            Session["fileName"] = null;
            try
            {
                dt = objService.GetDuplicateImport(district, VDC, filename);
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
            return dt;
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

        public ActionResult GetInspectionFileByDistrict(string SearchValueDistrict, string SearchValueVdc)
        {
            CommonFunction commonFC = new CommonFunction();
            InspectionImportService objService = new InspectionImportService();
            string id = string.Empty;
            string vdcCD = commonFC.GetCodeFromDataBase(SearchValueVdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["distcode"] = SearchValueDistrict;
            Session["vdccode"] = vdcCD;
            DataTable dtresult = objService.GetDataImportRecordByDistrict(SearchValueDistrict,vdcCD);
            ViewData["dtInspectiontFileImportRslt"] = dtresult;
            return PartialView("_GetInspectionFileByDistrict");
        }

    }
}




