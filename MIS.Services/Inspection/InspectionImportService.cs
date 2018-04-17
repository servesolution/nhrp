using EntityFramework;
using ExceptionHandler;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Web.Mvc;
using MIS.Models.Core;
using System.Web;
using MIS.Models.Entity;
using MIS.Models.Setup.Inspection;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;

namespace MIS.Services.Inspection
{
    public class InspectionImportService
    {


        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME  FROM NHRS_INSPECTION_FILE_BATCH WHERE STATUS ='Completed'";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }


       


        public bool CheckDuplicate(string PaNumber, string FormNumber, string inspectionLevel)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
             bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_INSPECTION_PAPER_DTL where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND SERIAL_NUMBER='" + FormNumber.ToString() + "' AND INSPECTION_LEVEL='" + inspectionLevel.ConvertToString() + "'  ";


                
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }


        //check pa 
        public bool CheckPa(string PaNumber )
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_ENROLLMENT_MST where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "'  ";



                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }

        //check vdc
        public bool Checkvdc(string vdccode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT KLL_VDC_CD FROM MLD_KLL_VDC_MAP where KLL_VDC_CD='" + vdccode.ToUpper() + "'  ";



                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }

        //check duplicate inspection application registration
        public bool CheckDuplicateApplication(string PaNumber, string InspectionLevel)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();



                

              
                try
                {
                    if (InspectionLevel == "1")
                    {
                        cmdText = "SELECT * FROM NHRS_INSPECTION_MST where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND (INSPECTION_LEVEL0 IS NOT NULL OR INSPECTION_LEVEL1 IS NOT NULL) ";

                    }
                    if (InspectionLevel == "2")
                    {
                        cmdText = "SELECT * FROM NHRS_INSPECTION_MST where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSPECTION_LEVEL2 IS NOT NULL";

                    }
                    if (InspectionLevel == "3")
                    {
                        cmdText = "SELECT * FROM NHRS_INSPECTION_MST where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSPECTION_LEVEL3 IS NOT NULL ";

                    }

                    dt = service.GetDataTable(cmdText, null);
                   if(dt== null || dt.Rows.Count==0)
                   {
                       if (InspectionLevel == "1")
                       {
                           cmdText = "SELECT * FROM NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND APP_FOR_INSP1 ='" + InspectionLevel.ToString() + "' ";

                       }
                       if (InspectionLevel == "2")
                       {
                           cmdText = "SELECT * FROM NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND APP_FOR_INSP2 ='" + InspectionLevel.ToString() + "' ";

                       }
                       if (InspectionLevel == "3")
                       {
                           cmdText = "SELECT * FROM NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND APP_FOR_INSP3 ='" + InspectionLevel.ToString() + "' ";

                       }
                       dt = service.GetDataTable(cmdText, null);
                   }

                    

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }


        //CHECK PREVIOUS NSPECTION APPROVED OR NOT 
        //check duplicate inspection application registration
        public bool CheckPrevInspApproved(string PaNumber, string InspectionLevel)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            if(InspectionLevel!="1")
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    //if (InspectionLevel == "1")
                    //{
                    //    cmdText = "SELECT * FROM NHRS_INSPECTION_MST where (NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND FINAL_DECISION_2_APPROVE ='Y' AND INSPECTION_LEVEL1='" + InspectionLevel + "') OR"
                    //              + "(NRA_DEFINED_CD='" + PaNumber.ToUpper() + "'   AND INSPECTION_LEVEL2 IS NO NULL)";

                    //}
                    if (InspectionLevel == "2")
                    {
                        cmdText = "SELECT * FROM NHRS_INSPECTION_MST where (NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSP_ONE_MOFALD_APPROVE ='Y' AND INSPECTION_LEVEL1='1')";

                    }
                    if (InspectionLevel == "3")
                    {
                        cmdText = "SELECT * FROM NHRS_INSPECTION_MST where (NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSP_TWO_MOFALD_APPROVE ='Y' AND INSPECTION_LEVEL2='2') ";


                    }


                    try
                    {
                        dt = service.GetDataTable(cmdText, null);

                    }
                    catch (Exception ex)
                    {
                        dt = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    res = true;

                }
            }
            else
            {
                dt = null;
            }
            
            

            return res;
        }


        public bool CheckUpdate(string PaNumber, string level, string formNumber)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_INSPECTION_PAPER_DTL where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSPECTION_LEVEL='" + level.ConvertToString() + "' AND SERIAL_NUMBER!='" + formNumber.ToString() +"'";




                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }




        

        public Boolean SaveExcelDataFromFileBrowse(DataTable paramTable, string fileName, out string exc, string district, string vdc)
        {
            CommonFunction common = new CommonFunction();
            QueryResult qrFIleBatch = null;
            QueryResult qrPaperDtl = null;
            QueryResult qrDesign = null;
            QueryResult qreProcess = null;
            QueryResult qrOwnDesign = null;
            bool res = false;
            exc = string.Empty;
            string InspectionLevel = "";

            string batchID = "";
            string InspPprId = "";
            string InspectionMstId = "";
            string DEFINED_CD = "";
            string INSPECTION_CODE_ID = "";

            string InspectionHistoryId = "";

            string bankSelect = "";



            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_IMPORT";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrFIleBatch = service.SubmitChanges("PR_INSPECTION_FILE_BATCH",
                                               "I",
                                                DBNull.Value,
                                                district.ToDecimal(),
                                                vdc.ToDecimal(),
                                                "Completed",
                                                fileName,//filename                                                 
                                                DateTime.Now,
                                                SessionCheck.getSessionUsername(),
                                                DBNull.Value);
                    batchID = qrFIleBatch["v_BATCH_ID"].ConvertToString();



                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        try
                        {



                            




                            //get inspection list from data base  for given info from excel
                            DataTable dtHierarchy = new DataTable();
                            string inspectionType = paramTable.Rows[i]["INSPECTION_TYPE"].ConvertToString();
                            string designNumber = paramTable.Rows[i]["DESIGN_NUMBER"].ConvertToString();
                            string designID = "";    //actual desin id
                            string HierarchyParentID = "";
                            string OwnDesignFlag = "N";
                            string designIdForOwnDesign = "";


                            // check if own design or from available design
                            if (paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() != "" || paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ConvertToString() != "")
                            {
                                if (inspectionType == "7")
                                {
                                    OwnDesignFlag = "Y";

                                }
                            }



                            // get house design id, hierarchy id from house model
                            if (designNumber != "" && designNumber != null)
                            {
                                string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + designNumber + "'and CODE_LOC='" + paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() + "'";
                                dtHierarchy = service.GetDataTable(new
                                {
                                    query = cmdTextDesign,
                                    args = new { }
                                });
                            }
                            // design number empty means own design
                            if (inspectionType != "" && inspectionType != null)
                            {
                                string DesignNumberFromTable = "";
                                if (inspectionType == "1")
                                {
                                    DesignNumberFromTable = "19";
                                }
                                if (inspectionType == "6")
                                {
                                    DesignNumberFromTable = "18";
                                }
                                if (inspectionType == "7")
                                {
                                    DesignNumberFromTable = "20";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "6")
                                {
                                    DesignNumberFromTable = "18";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "1")
                                {
                                    DesignNumberFromTable = "16";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "2")
                                {
                                    DesignNumberFromTable = "1";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "3")
                                {
                                    DesignNumberFromTable = "17";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "4")
                                {
                                    DesignNumberFromTable = "9";
                                }
                                if (DesignNumberFromTable.ConvertToString() != "")
                                {
                                    string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + DesignNumberFromTable + "'";
                                    dtHierarchy = service.GetDataTable(new
                                    {
                                        query = cmdTextDesign,
                                        args = new { }
                                    });
                                }
                            }



                            // set hierarchy code and design_id
                            if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                            {
                                if (OwnDesignFlag == "Y")
                                {
                                    designIdForOwnDesign = "";
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }
                                if (OwnDesignFlag == "N")
                                {
                                    designIdForOwnDesign = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }

                            }


                            //save inspection mst info
                            InspectionLevel = paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString();
                            string level0 = "";
                            string level1 = "";
                            string level2 = "";
                            string level3 = "";
                            string complyFlag = "N";
                            if (InspectionLevel == "1")
                            {
                                level0 = "0";
                                level1 = "1";
                            }
                            if (InspectionLevel == "2")
                            {
                                level0 = "0";
                                level1 = "1";
                                level2 = "2";
                            }
                            if (InspectionLevel == "3")
                            {
                                level0 = "0";
                                level1 = "1";
                                level2 = "2";
                                level2 = "3";
                            }

                            //checking comply
                            if (inspectionType != "7")
                            {
                                for (int j = 29; j <= 161; j = j + 2)
                                {
                                    if (paramTable.Rows[i][j].ConvertToString() == "2")
                                    {
                                        complyFlag = "N";
                                        goto NotComply;

                                    }
                                    else
                                    {
                                        complyFlag = "Y";
                                    }
                                }
                            }


                        NotComply:


                            if (paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString() == "12-55-1-1-54")
                            {

                            }
                            QueryResult qrMst = service.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                                  "I",

                                                   DBNull.Value,                              //mst id
                                                   DBNull.Value,                        // defined id
                                                   level0.ToDecimal(),
                                                   DBNull.Value,                                //inspectionDate
                                                   "R" ,

                                                   SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    DBNull.Value,
                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),
                                                    paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                    level1.ToDecimal(),
                                                    level2.ToDecimal(),
                                                    level3.ToDecimal(),
                                                    complyFlag.ConvertToString(),
                                                    "N" ,  /// final approve flag
                                                    "N" , // final decision 
                                                  InspectionHistoryId

                                              );
                            InspectionMstId = qrMst["v_INSPECTION_MST_ID"].ConvertToString();

                            string[] beneficiaryName = paramTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString().Split(' ');
                            string BeneficiaryFullName = "";
                              string BeneficiaryLastName ="";
                            if (beneficiaryName.Length <=3)
                            {
                                BeneficiaryFullName = paramTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString();
                            }
                            else
                            {
                                for(int k =2 ; k<beneficiaryName.Length;k++)
                                {

                                    BeneficiaryLastName = BeneficiaryLastName + beneficiaryName[k];
                                }
                                BeneficiaryFullName = beneficiaryName[0] + " " + beneficiaryName[1] + " " + BeneficiaryLastName;
                            }


                            //save design part
                            qrDesign = service.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                                                  "I",

                                                   DBNull.Value,                                                 // design id 
                                                   DBNull.Value,                                                  // defined cd
                                                   paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), //nra defined cd
                                                   BeneficiaryFullName,       // beneficiary full name 
                                                   paramTable.Rows[i]["DISTRICT"].ToDecimal(),                     // district
                                                   paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),             // vdc mun cd
                                                   paramTable.Rows[i]["WARD"].ToDecimal(),                         // ward TOLE
                                                   paramTable.Rows[i]["TOLE"].ConvertToString(),                   // tole
                                                   OwnDesignFlag.ConvertToString(),                                // own design/template

                                                   designIdForOwnDesign.ConvertToString(),                         // design number
                                                   paramTable.Rows[i]["BUILD_MATERIAL"].ToDecimal(),               // construction material
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal(),      // roof material  

                                                   paramTable.Rows[i]["BUILD_MATERIAL_OTHER"].ConvertToString(),    // other construction material
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_OTHER"].ConvertToString(),   // other roof material

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    DBNull.Value,
                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                   InspectionMstId.ToDecimal(),
                                                   "R" ,                                          // design status
                                                    paramTable.Rows[i]["LAND_PLOT_NUMBER"].ConvertToString()     ,        // kitta number
                                                   InspectionHistoryId
                                                    );

                            //save inspection paper detail

                            String techniClaAssist = "";
                            String Orgtyp = "";
                            String trainedManson = "";
                            String SoilType = "";
                            string EditRequired = "N";
                            string FinalDecision = "N";
                            string FinalDecision2 = "N";
                            string EngineerPost = "";
                            string AcceptTheEntry = "N";
                            string GpsTaken = "N";
                            string Phone = paramTable.Rows[i]["PHONE_NUMBER"].ConvertToString();
                            if (paramTable.Rows[i]["TECHNICAL_SUPPORT"].ConvertToString() == "1")
                            {
                                techniClaAssist = "Y";
                            }
                            if (paramTable.Rows[i]["TECHNICAL_SUPPORT"].ConvertToString() == "2")
                            {
                                techniClaAssist = "N";
                            }
                            if (paramTable.Rows[i]["ORGANIZATION"].ConvertToString() == "1")
                            {
                                Orgtyp = "G";
                            }
                            if (paramTable.Rows[i]["ORGANIZATION"].ConvertToString() == "2")
                            {
                                Orgtyp = "N";
                            }
                            if (paramTable.Rows[i]["TRAINED_MASONS"].ConvertToString() == "1")
                            {
                                trainedManson = "Y";
                            }
                            if (paramTable.Rows[i]["TRAINED_MASONS"].ConvertToString() == "2")
                            {
                                trainedManson = "N";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "1")
                            {
                                SoilType = "H";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "2")
                            {
                                SoilType = "M";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "3")
                            {
                                SoilType = "S";
                            }
                            if (paramTable.Rows[i]["EDIT_REQUIRED"].ConvertToString() == "1")
                            {
                                EditRequired = "Y";
                            }
                            if (paramTable.Rows[i]["FINAL_DECISION"].ConvertToString() == "1")
                            {
                                FinalDecision = "Y";
                            }
                            if (paramTable.Rows[i]["FINAL_DECISION_2"].ConvertToString() == "1")
                            {
                                FinalDecision2 = "Y";
                            }
                            if (paramTable.Rows[i]["ENGINEER_POST"].ConvertToString() == "1")
                            {
                                EngineerPost = "Engineer";
                            }
                            if (paramTable.Rows[i]["ENGINEER_POST"].ConvertToString() == "2")
                            {
                                EngineerPost = "Sub Engineer";
                            }
                            if (paramTable.Rows[i]["ACCEPT_THE_ENTRY"].ConvertToString() == "1")
                            {
                                AcceptTheEntry = "Y";
                            }
                            if (paramTable.Rows[i]["GPS_TAKEN"].ConvertToString() == "1")
                            {
                                GpsTaken = "Y";
                            }
                            if (paramTable.Rows[i]["PHONE_NUMBER"].ConvertToString() == "0")
                            {
                                Phone = "";
                            }




                            string applicationForOne = "";
                            string applicationForTwo = "";
                            string applicationForThree = "";
                            if(paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString()=="1")
                            {
                                applicationForOne = "1";
                            }
                            if (paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() == "2")
                            {
                                applicationForTwo = "2";
                            }
                            if (paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() == "3")
                            {
                                applicationForThree = "3";
                            }

                            QueryResult qrRegister = service.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                               "I".ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               paramTable.Rows[i]["DISTRICT"].ToDecimal(),                      // district
                                               paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),              // vdc mun cd
                                               paramTable.Rows[i]["WARD"].ToDecimal(),                          // ward
                                                paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), //nra defined cd

                                               BeneficiaryFullName,     //  BENEFICIARY_NAME 
                                               0,
                                               DBNull.Value,
                                               DBNull.Value, //remarks


                                               DBNull.Value,
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),


                                               DBNull.Value, //reg no
                                                DateTime.Now.ConvertToString(), //reg date

                                               DBNull.Value, //schedule
                                               DBNull.Value,
                                              applicationForOne.ToDecimal(),
                                              applicationForTwo.ToDecimal(),
                                              applicationForThree.ToDecimal()



                       );



                            if (paramTable.Rows[i]["BANK_SELECT"].ConvertToString() == "-1" || paramTable.Rows[i]["BANK_SELECT"].ConvertToString() == "0")
                            {
                                bankSelect = null;
                            }
                            else
                            {
                                bankSelect = paramTable.Rows[i]["BANK_SELECT"].ConvertToString();
                            }

                            qrPaperDtl = service.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                                   "I",
                                                   DBNull.Value,
                                                   DEFINED_CD.ToDecimal(),
                                                   INSPECTION_CODE_ID.ToDecimal(),
                                                   paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), //nra defined cd
                                                   paramTable.Rows[i]["DISTRICT"].ToDecimal(),                      // district
                                                   paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),              // vdc mun cd
                                                   paramTable.Rows[i]["WARD"].ToDecimal(),                          // ward
                                                   DBNull.Value,                                            // map design
                                                   designID.ConvertToString(),                                      // design number
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal(),      // roof material  
                                                   paramTable.Rows[i]["BUILD_MATERIAL"].ToDecimal(),               // construction material
                                                   techniClaAssist,                                                // technical assist
                                                   Orgtyp,                                                         // org type
                                                   trainedManson,                                                  //constructor type
                                                   SoilType,                                                       // soil type
                                                   DBNull.Value,                                                  // house model
                                                   paramTable.Rows[i]["PHOTO_1"].ConvertToString(),                 // photo cd 1
                                                   paramTable.Rows[i]["PHOTO_2"].ConvertToString(),                 //  photo cd 2
                                                   paramTable.Rows[i]["PHOTO_3"].ConvertToString(),                 //  photo cd 3
                                                   paramTable.Rows[i]["PHOTO_4"].ConvertToString(),                 //  photo cd 4
                                                    paramTable.Rows[i]["PHOTO_1_NAME"].ConvertToString().ConvertToString(),        // photo  1
                                                    paramTable.Rows[i]["PHOTO_2_NAME"].ConvertToString().ConvertToString(),        //  photo 2
                                                    paramTable.Rows[i]["PHOTO_3_NAME"].ConvertToString().ConvertToString(),        //  photo  3
                                                   paramTable.Rows[i]["PHOTO_4_NAME"].ConvertToString().ConvertToString(),        //photo  4
                                                   "G" ,       //status
                                                   "Y" ,       //active
                                                   SessionCheck.getSessionUsername(), //entered by
                                                   DateTime.Now,                       //entered date
                                                   System.DateTime.Now.ConvertToString(), //entered date loc
                                                   DBNull.Value,                                     // approved
                                                   SessionCheck.getSessionUsername(),      //approved by
                                                   DateTime.Now,                           //approved date 
                                                   System.DateTime.Now.ConvertToString(),  //approved date loc
                                                   SessionCheck.getSessionUsername(),      // updated by
                                                   DateTime.Now,                           //update date
                                                   System.DateTime.Now.ConvertToString(),  //updated date loc
                                                   DBNull.Value,                    // installment
                                                   DBNull.Value,                          // map design code 
                                                   paramTable.Rows[i]["HOUSE_PLAN_NAME"].ConvertToString(),   // passed map no,,gharko mota moti naksa
                                                   DBNull.Value,                    // other info
                                                   batchID.ToDecimal(),                     // file batch id
                                                   paramTable.Rows[i]["INSPECTION_TYPE"].ConvertToString(),      //  Inspection Type/house model
                                                   paramTable.Rows[i]["INSPECTION_DATE"].ToDateTime().ConvertToString(),      //  INSPECTION_DATE 
                                                   BeneficiaryFullName,     //  BENEFICIARY_NAME 
                                                   paramTable.Rows[i]["TOLE"].ConvertToString(),                 //  TOLE   
                                                   paramTable.Rows[i]["LAND_PLOT_NUMBER"].ConvertToString(),     //  LAND_PLOT_NUMBER
                                                   paramTable.Rows[i]["ORGANIZATION_OTHERS"].ConvertToString(),  //ORGANIZATION_OTHERS
                                                   paramTable.Rows[i]["PHOTO_5"].ConvertToString(),              //PHOTO_CD_5 
                                                   paramTable.Rows[i]["PHOTO_5_NAME"].ConvertToString(),         //PHOTO_5 
                                                   DBNull.Value,                                         //PHOTO_CD_6 
                                                   DBNull.Value,                                         // photo 6 
                                                   FinalDecision.ConvertToString(),                              //Final Decision
                                                   paramTable.Rows[i]["LATITUDE"].ConvertToString(),             //LATITUDE
                                                   paramTable.Rows[i]["LONGITUDE"].ConvertToString(),            //LONGITUDE
                                                   paramTable.Rows[i]["ALTITUDE"].ConvertToString(),             //ALTITUDE 
                                                   InspectionMstId.ToDecimal(),                                  // mst id
                                                   bankSelect.ToDecimal(),                //BANK_NAME_cd
                                                   paramTable.Rows[i]["BANK_ACCOUNT_NUMBER"].ConvertToString(),  //BANK_ACCOUNT_NUMBER
                                                   paramTable.Rows[i]["FORM_NUMBER"].ConvertToString(),          //serial number
                                                   DBNull.Value,                                         //House owner id
                                                   Phone.ConvertToString(),                                     //PHONE_NUMBER
                                                   paramTable.Rows[i]["FORM_PAD_NUMBER"].ConvertToString(),     //FORM_PAD_NUMBER
                                                  paramTable.Rows[i]["DESIGN_DETAILS"].ConvertToString(),       //DESIGN_DETAILS
                                                  EditRequired.ConvertToString(),                               //EDIT_REQUIRED
                                                  paramTable.Rows[i]["EDIT_REQUIRED_DETAILS"].ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  bankSelect.ConvertToString(),          // BANK_SELECT
                                                  paramTable.Rows[i]["BANK_BRANCH"].ConvertToString(),          //BANK_BRANCH
                                                  paramTable.Rows[i]["FINAL_REMARKS"].ConvertToString(),        // FINAL_REMARKS
                                                  AcceptTheEntry.ConvertToString(),                             //ACCEPT_THE_ENTRY
                                                  GpsTaken.ConvertToString(),                                   //GPS_TAKEN
                                                  FinalDecision2.ConvertToString(),                       //FINAL_DECISION_2
                                                  paramTable.Rows[i]["BANK_NOT_AVAILABLE_REMARKS"].ConvertToString(),         // bank no remarks
                                                  "N" , // final decision approve
                                                  "N" , // final decision 2 approve
                                                  DBNull.Value,  //approve 1 batch
                                                  DBNull.Value, //approve 2 batch
                                                  paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString(),
                                                  InspectionHistoryId.ToDecimal()
                                               );
                            InspPprId = qrPaperDtl["v_INSPECTION_PAPER_ID"].ConvertToString();

                            string cmdText = "";
                            

                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "1")
                            {
                                inspectionType = "2";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "2")
                            {
                                inspectionType = "3";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "3")
                            {
                                inspectionType = "4";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "4")
                            {
                                inspectionType = "5";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "6")
                            {
                                inspectionType = "6";
                            }

                            if (inspectionType != "7")
                            {
                                cmdText = "Select INSPECTION_CODE_ID,DEFINED_CD, UPPER_INSPECTION_CODE_ID, Group_FLAG,  VALUE_TYPE," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG  from NHRS_INSPECTION_DESC_DTL "

                              + "WHERE  1=1 "
                              + "START with (INSPECTION_CODE_ID LIKE '%"
                              + HierarchyParentID + "%' AND  UPPER_INSPECTION_CODE_ID is null)"
                              + " CONNECT by prior INSPECTION_CODE_ID = UPPER_INSPECTION_CODE_ID ";

                                dtHierarchy = service.GetDataTable(new
                                {
                                    query = cmdText,
                                    args = new { }
                                });
                            }

                            if (inspectionType.ConvertToString() != "7")
                            {


                                if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                                {
                                    string Inspection_code = "";
                                    string value = "";
                                    string Remarks = "";



                                    foreach (DataRow dr in dtHierarchy.Rows)
                                    {



                                        if (inspectionType.ToDecimal() > 0 && inspectionType.ToDecimal() < 7)
                                        {

                                            int j = 31;
                                            do
                                            {


                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 44);
                                        }
                                        if (inspectionType == "1") // for saving A and B type
                                        {

                                            int j = 45;
                                            do
                                            {


                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 87);
                                        }

                                        if (inspectionType == "6")  // for saving rcc type
                                        {
                                            int j = 131;
                                            do
                                            {

                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();

                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 177);
                                        }
                                        if (inspectionType.ToDecimal() >= 2 && inspectionType.ToDecimal() <= 5)  //saving bmc smc,bmm smm
                                        {
                                            int j = 89;
                                            do
                                            {
                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value
                                                    );

                                                }
                                                j = j + 2;
                                            }
                                            while (j <= 129);
                                        }
                                    }

                                }
                            }



                            if (inspectionType == "7")//for own design
                            {
                                string ConstructionStatus = "";

                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "1")
                                {
                                    ConstructionStatus = "Foundation Under Construction";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "2")
                                {
                                    ConstructionStatus = "Foundation Construction Completed";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "3")
                                {
                                    ConstructionStatus = "Upto Roof Band in Ground Floor";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "4")
                                {
                                    ConstructionStatus = "Ground Floor Construction Completed (Including Roof)";
                                }



                                qrOwnDesign = service.SubmitChanges("PR_NHRS_INSPECTION_OWN_DESIGN",
                                        "I".ConvertToString(),

                                          DBNull.Value,
                                          DBNull.Value,

                                          InspPprId.ToDecimal(),                            // inspection paper id 
                                          designID.ConvertToString(),                       // design number FOUR_NO1

                                          DBNull.Value,       //base constructing 
                                          DBNull.Value,            //base constructed
                                          DBNull.Value,                    // ground roof finished
                                          DBNull.Value,   // ground floor completed 

                                           paramTable.Rows[i]["FOUR_NO2"].ConvertToString(), //floor count

                                          paramTable.Rows[i]["FOUR_NO3_A"].ConvertToString(),//base/foundation material
                                          paramTable.Rows[i]["FOUR_NO3_B"].ConvertToString(), // foundation depth below ground
                                          paramTable.Rows[i]["FOUR_NO3_C"].ConvertToString(), //foundation width
                                          paramTable.Rows[i]["FOUR_NO3_D"].ConvertToString(),  // foundation height above ground level

                                          paramTable.Rows[i]["FOUR_NO4_A"].ConvertToString(),  // ground floor constructionmaterial
                                          paramTable.Rows[i]["FOUR_NO4_B"].ConvertToString(),   // ground floor construction technique
                                          paramTable.Rows[i]["FOUR_NO4_C"].ConvertToString(),  // detail description wall type


                                          DBNull.Value,   //extra column
                                          paramTable.Rows[i]["FOUR_NO5_A"].ConvertToString(),  // ground floor roof construction material
                                          paramTable.Rows[i]["FOUR_NO5_B"].ConvertToString(),  // ground floor roof construction technique
                                          paramTable.Rows[i]["FOUR_NO5_C"].ConvertToString(),   // ground floor roof detail description

                                          paramTable.Rows[i]["FOUR_NO6_A"].ConvertToString(),  // first floor construction material
                                          paramTable.Rows[i]["FOUR_NO6_B"].ConvertToString(),  // first floor construction technique
                                           paramTable.Rows[i]["FOUR_NO6_C"].ConvertToString(),  // first floor detail description

                                          paramTable.Rows[i]["FOUR_NO7_A"].ConvertToString(),  // roof construction material
                                          paramTable.Rows[i]["FOUR_NO7_B"].ConvertToString(),   // roof construction technique
                                         paramTable.Rows[i]["FOUR_NO7_C"].ConvertToString(),   // roof dettail description

                                          DBNull.Value,
                                          DBNull.Value,
                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),

                                          DBNull.Value,
                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),

                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),
                                          ConstructionStatus.ConvertToString(),
                                           InspectionHistoryId.ToDecimal()
                                        );






                            }

                            // inspection process detail import
                            qreProcess = service.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                        "I",

                                         DBNull.Value,
                                         DBNull.Value,
                                          paramTable.Rows[i]["HOUSE_OWNER_S_NAME"].ConvertToString(),          //BenfullnameEng
                                          paramTable.Rows[i]["RELATIONSHIP_TO_HOUSE_OWNER"].ConvertToString(), //relationWithbenf


                                         paramTable.Rows[i]["MOUD_DLPIU_ENGINEER"].ConvertToString(),  //engineer name
                                         EngineerPost.ConvertToString(),                               //engineer designation 

                                         DBNull.Value,            //engineer name
                                         DBNull.Value,           //engineer designation 



                                         DBNull.Value,// register date
                                         DBNull.Value,//proceed date 
                                         DBNull.Value,//approved date 
                                         DBNull.Value,

                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),

                                         DBNull.Value,
                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),

                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),
                                         InspPprId.ToDecimal(),
                                           InspectionHistoryId.ToDecimal()


                                    );




                        }
                        catch (OracleException oe)
                        {
                            if (batchID != "")
                            {
                                res = true;
                                exc += oe.Message.ToString();
                                ExceptionManager.AppendLog(oe);


                                string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID  IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID  IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);

                                //string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                                //SaveErrorMessgae(cmdText);

                                //cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                                //SaveErrorMessgae(cmdText);

                                //cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspPprId + "'");
                                //SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where FILE_BATCH_ID='" + batchID + "'AND INSPECTION_LEVEL='" + InspectionLevel + "'");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID NOT IN (SELECT INSPECTION_MST_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE 1=1)");
                                SaveErrorMessgae(cmdText);

                                //cmdText = String.Format("delete from nhrs_inspection_mst where INSPECTION_MST_ID='" + paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString() + "'");
                                //SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from nhrs_inspection_mst where INSPECTION_MST_ID NOT IN (SELECT INSPECTION_MST_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE 1=1)");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                                SaveErrorMessgae(cmdText);
                                break;

                            }
                        }
                        catch (Exception ex)
                        {
                            if (batchID != "")
                            {
                                res = true;
                                exc += ex.Message.ToString();
                                ExceptionManager.AppendLog(ex);

                                string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID  IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID  IN (SELECT INSPECTION_PAPER_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE FILE_BATCH_ID='" + batchID + "')");
                                SaveErrorMessgae(cmdText);
 
                                cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where FILE_BATCH_ID='" + batchID + "'AND INSPECTION_LEVEL='" + InspectionLevel + "'");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID NOT IN (SELECT INSPECTION_MST_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE 1=1)");
                                SaveErrorMessgae(cmdText);

                          

                                cmdText = String.Format("delete from nhrs_inspection_mst where INSPECTION_MST_ID NOT IN (SELECT INSPECTION_MST_ID FROM NHRS_INSPECTION_PAPER_DTL WHERE 1=1)");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                                SaveErrorMessgae(cmdText);

                            }
                        }
                    }

                }
                catch (OracleException oe)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                        string cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);

                    }
                }
                catch (Exception ex)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);


                    }
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrFIleBatch != null)
                    {
                        res = qrFIleBatch.IsSuccess;
                    }
                }

                return res;

            }
        }


        //update from import 

        public Boolean UpdateExcelDataFromFileBrowse(DataTable paramTable, string fileName, out string exc, string district, string vdc)
        {
            CommonFunction common = new CommonFunction();
            QueryResult qrFIleBatch = null;
            QueryResult qrPaperDtl = null;
            QueryResult qrDesign = null;
            QueryResult qreProcess = null;
            QueryResult qrOwnDesign = null;
            bool res = false;
            exc = string.Empty;
            string InspectionLevel = "";

            string batchID = "";
            string InspPprId = "";
            string InspectionMstId = "";
            string DEFINED_CD = "";
            string INSPECTION_CODE_ID = "";






            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_IMPORT";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    //qrFIleBatch = service.SubmitChanges("PR_INSPECTION_FILE_BATCH",
                    //                           "I",
                    //                            DBNull.Value,
                    //                            district.ToDecimal(),
                    //                            vdc.ToDecimal(),
                    //                            "Completed",
                    //                            fileName,//filename                                                 
                    //                            DateTime.Now,
                    //                            SessionCheck.getSessionUsername(),
                    //                            DBNull.Value);
                    //batchID = qrFIleBatch["v_BATCH_ID"].ConvertToString();



                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        try
                        {


                            //get inspection list from data base  for given info from excel
                            DataTable dtHierarchy = new DataTable();
                            string inspectionType = paramTable.Rows[i]["INSPECTION_TYPE"].ConvertToString();
                            string designNumber = paramTable.Rows[i]["DESIGN_NUMBER"].ConvertToString();
                            string designID = "";    //actual desin id
                            string HierarchyParentID = "";
                            string OwnDesignFlag = "N";
                            string designIdForOwnDesign = "";


                            // check if own design or from available design
                            if (paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() != "" || paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ConvertToString() != "")
                            {
                                if (inspectionType == "7")
                                {
                                    OwnDesignFlag = "Y";

                                }
                            }



                            // get house design id, hierarchy id from house model
                            if (designNumber != "" && designNumber != null)
                            {
                                string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + designNumber + "'";
                                dtHierarchy = service.GetDataTable(new
                                {
                                    query = cmdTextDesign,
                                    args = new { }
                                });
                            }
                            // design number empty means own design
                            if (inspectionType != "" && inspectionType != null)
                            {
                                string DesignNumberFromTable = "";
                                if (inspectionType == "1")
                                {
                                    DesignNumberFromTable = "19";
                                }
                                if (inspectionType == "6")
                                {
                                    DesignNumberFromTable = "18";
                                }
                                if (inspectionType == "7")
                                {
                                    DesignNumberFromTable = "20";
                                }
                               

                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "6")
                                {
                                    DesignNumberFromTable = "18";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "1")
                                {
                                    DesignNumberFromTable = "16";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "2")
                                {
                                    DesignNumberFromTable = "1";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "3")
                                {
                                    DesignNumberFromTable = "17";
                                }
                                if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "4")
                                {
                                    DesignNumberFromTable = "9";
                                }
                                if(DesignNumberFromTable.ConvertToString()!="")
                                {
                                    string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + DesignNumberFromTable + "'";
                                    dtHierarchy = service.GetDataTable(new
                                    {
                                        query = cmdTextDesign,
                                        args = new { }
                                    });
                                }
                               
                            }



                            // set hierarchy code and design_id
                            if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                            {
                                if (OwnDesignFlag == "Y")
                                {
                                    designIdForOwnDesign = "";
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }
                                if (OwnDesignFlag == "N")
                                {
                                    designIdForOwnDesign = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }

                            }


                            //save inspection mst info
                            InspectionLevel = paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString();
                            string level0 = "";
                            string level1 = "";
                            string level2 = "";
                            string level3 = "";
                            string complyFlag = "N";
                            if (InspectionLevel == "1")
                            {
                                level0 = "0";
                                level1 = "1";
                            }
                            if (InspectionLevel == "2")
                            {
                                level0 = "0";
                                level1 = "1";
                                level2 = "2";
                            }
                            if (InspectionLevel == "3")
                            {
                                level0 = "0";
                                level1 = "1";
                                level2 = "2";
                                level2 = "3";
                            }

                            //checking comply
                            if (inspectionType != "7")
                            {
                                for (int j = 29; j <= 161; j = j + 2)
                                {
                                    if (paramTable.Rows[i][j].ConvertToString() == "2")
                                    {
                                        complyFlag = "N";
                                        goto NotComply;

                                    }
                                    else
                                    {
                                        complyFlag = "Y";
                                    }
                                }
                            }


                        NotComply:

                            DataTable dtHistory = new DataTable();
                            string InspectionHistoryId = ""; 
                            string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                            dtHistory = service.GetDataTable(new
                            {
                                query = cmdHistoryid,
                                args = new { }
                            });

                            if(dtHistory!=null && dtHistory.Rows.Count>0)
                            {
                                InspectionHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                            }
                            string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + InspectionHistoryId.ConvertToString() + "')";
                            QueryResult InsertHistoryId = service.SubmitChanges(insertCmd, null);

                            QueryResult qrMst = service.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                                  "U",

                                                   DBNull.Value,                              //mst id
                                                   DBNull.Value,                        // defined id
                                                   level0.ToDecimal(),
                                                   DBNull.Value,                                //inspectionDate
                                                   "R" ,

                                                   SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    DBNull.Value,
                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),
                                                    paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(),
                                                    level1.ToDecimal(),
                                                    level2.ToDecimal(),
                                                    level3.ToDecimal(),
                                                    complyFlag.ConvertToString(),
                                                  "N" ,  /// final approve flag
                                                  "N" ,  // final decision 
                                                  InspectionHistoryId.ToDecimal()

                                              );



                            string[] beneficiaryName = paramTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString().Split(' ');
                            string BeneficiaryFullName = "";
                            string BeneficiaryLastName = "";
                            if (beneficiaryName.Length <= 3)
                            {
                                BeneficiaryFullName = paramTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString();
                            }
                            else
                            {
                                for (int k = 2; k < beneficiaryName.Length; k++)
                                {

                                    BeneficiaryLastName = BeneficiaryLastName + beneficiaryName[k];
                                }
                                BeneficiaryFullName = beneficiaryName[0] + " " + beneficiaryName[1] + " " + BeneficiaryLastName;
                            }


                            //save design part
                            qrDesign = service.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                                                  "U",

                                                   DBNull.Value,                                                 // design id 
                                                   DBNull.Value,                                                  // defined cd
                                                   paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), //nra defined cd
                                                   BeneficiaryFullName,       // beneficiary full name 
                                                   paramTable.Rows[i]["DISTRICT"].ToDecimal(),                     // district
                                                   paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),             // vdc mun cd
                                                   paramTable.Rows[i]["WARD"].ToDecimal(),                         // ward TOLE
                                                   paramTable.Rows[i]["TOLE"].ConvertToString(),                   // tole
                                                   OwnDesignFlag.ConvertToString(),                                // own design/template

                                                   designIdForOwnDesign.ConvertToString(),                         // design number
                                                   paramTable.Rows[i]["BUILD_MATERIAL"].ToDecimal(),               // construction material
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal(),      // roof material  

                                                   paramTable.Rows[i]["BUILD_MATERIAL_OTHER"].ConvertToString(),    // other construction material
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_OTHER"].ConvertToString(),   // other roof material

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    DBNull.Value,
                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                   InspectionMstId.ToDecimal(),
                                                   "R" ,                                          // design status
                                                    paramTable.Rows[i]["LAND_PLOT_NUMBER"].ConvertToString()    ,         // kitta number
                                                    InspectionHistoryId
                                                    );

                            //save inspection paper detail

                            String techniClaAssist = "";
                            String Orgtyp = "";
                            String trainedManson = "";
                            String SoilType = "";
                            string EditRequired = "N";
                            string FinalDecision = "N";
                            string FinalDecision2 = "N";
                            string EngineerPost = "";
                            string AcceptTheEntry = "N";
                            string GpsTaken = "N";
                            string Phone = paramTable.Rows[i]["PHONE_NUMBER"].ConvertToString();
                            if (paramTable.Rows[i]["TECHNICAL_SUPPORT"].ConvertToString() == "1")
                            {
                                techniClaAssist = "Y";
                            }
                            if (paramTable.Rows[i]["TECHNICAL_SUPPORT"].ConvertToString() == "2")
                            {
                                techniClaAssist = "N";
                            }
                            if (paramTable.Rows[i]["ORGANIZATION"].ConvertToString() == "1")
                            {
                                Orgtyp = "G";
                            }
                            if (paramTable.Rows[i]["ORGANIZATION"].ConvertToString() == "2")
                            {
                                Orgtyp = "N";
                            }
                            if (paramTable.Rows[i]["TRAINED_MASONS"].ConvertToString() == "1")
                            {
                                trainedManson = "Y";
                            }
                            if (paramTable.Rows[i]["TRAINED_MASONS"].ConvertToString() == "2")
                            {
                                trainedManson = "N";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "1")
                            {
                                SoilType = "H";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "2")
                            {
                                SoilType = "M";
                            }
                            if (paramTable.Rows[i]["TYPE_OF_SOIL"].ConvertToString() == "3")
                            {
                                SoilType = "S";
                            }
                            if (paramTable.Rows[i]["EDIT_REQUIRED"].ConvertToString() == "1")
                            {
                                EditRequired = "Y";
                            }
                            if (paramTable.Rows[i]["FINAL_DECISION"].ConvertToString() == "1")
                            {
                                FinalDecision = "Y";
                            }
                            if (paramTable.Rows[i]["FINAL_DECISION_2"].ConvertToString() == "1")
                            {
                                FinalDecision2 = "Y";
                            }
                            if (paramTable.Rows[i]["ENGINEER_POST"].ConvertToString() == "1")
                            {
                                EngineerPost = "Engineer";
                            }
                            if (paramTable.Rows[i]["ENGINEER_POST"].ConvertToString() == "2")
                            {
                                EngineerPost = "Sub Engineer";
                            }
                            if (paramTable.Rows[i]["ACCEPT_THE_ENTRY"].ConvertToString() == "1")
                            {
                                AcceptTheEntry = "Y";
                            }
                            if (paramTable.Rows[i]["GPS_TAKEN"].ConvertToString() == "1")
                            {
                                GpsTaken = "Y";
                            }
                            if (paramTable.Rows[i]["PHONE_NUMBER"].ConvertToString() == "0")
                            {
                                Phone = "";
                            }












                            string applicationForOne = "";
                            string applicationForTwo = "";
                            string applicationForThree = "";
                            if (paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() == "1")
                            {
                                applicationForOne = "1";
                            }
                            if (paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() == "2")
                            {
                                applicationForTwo = "2";
                            }
                            if (paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() == "3")
                            {
                                applicationForThree = "3";
                            }

                            QueryResult qrRegister = service.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                               "U".ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               paramTable.Rows[i]["DISTRICT"].ToDecimal(),                      // district
                                               paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),              // vdc mun cd
                                               paramTable.Rows[i]["WARD"].ToDecimal(),                          // ward
                                                paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString(), //nra defined cd

                                                BeneficiaryFullName,     //  BENEFICIARY_NAME 
                                               0,
                                               DBNull.Value,
                                               DBNull.Value, //remarks


                                               DBNull.Value,
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),


                                               DBNull.Value, //reg no
                                                DateTime.Now.ConvertToString(), //reg date

                                               DBNull.Value, //schedule
                                               DBNull.Value,
                                              applicationForOne.ToDecimal(),
                                              applicationForTwo.ToDecimal(),
                                              applicationForThree.ToDecimal()



                       );
                            string bankSelect = "";
                            if (paramTable.Rows[i]["BANK_SELECT"].ConvertToString() == "-1" || paramTable.Rows[i]["BANK_SELECT"].ConvertToString() == "0")
                            {
                                bankSelect = null;
                            }
                            else
                            {
                                bankSelect = paramTable.Rows[i]["BANK_SELECT"].ConvertToString();
                            }

                            qrPaperDtl = service.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                                   "U",
                                                   DBNull.Value,
                                                   DEFINED_CD.ToDecimal(),
                                                   INSPECTION_CODE_ID.ToDecimal(),
                                                   paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString().Trim(), //nra defined cd
                                                   paramTable.Rows[i]["DISTRICT"].ToDecimal(),                      // district
                                                   paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),              // vdc mun cd
                                                   paramTable.Rows[i]["WARD"].ToDecimal(),                          // ward
                                                   DBNull.Value,                                            // map design
                                                   designID.ConvertToString(),                                      // design number
                                                   paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal(),      // roof material  
                                                   paramTable.Rows[i]["BUILD_MATERIAL"].ToDecimal(),               // construction material
                                                   techniClaAssist,                                                // technical assist
                                                   Orgtyp,                                                         // org type
                                                   trainedManson,                                                  //constructor type
                                                   SoilType,                                                       // soil type
                                                   DBNull.Value,                                                  // house model
                                                   paramTable.Rows[i]["PHOTO_1"].ConvertToString(),                 // photo cd 1
                                                   paramTable.Rows[i]["PHOTO_2"].ConvertToString(),                 //  photo cd 2
                                                   paramTable.Rows[i]["PHOTO_3"].ConvertToString(),                 //  photo cd 3
                                                   paramTable.Rows[i]["PHOTO_4"].ConvertToString(),                 //  photo cd 4
                                                    paramTable.Rows[i]["PHOTO_1_NAME"].ConvertToString().ConvertToString(),        // photo  1
                                                    paramTable.Rows[i]["PHOTO_2_NAME"].ConvertToString().ConvertToString(),        //  photo 2
                                                    paramTable.Rows[i]["PHOTO_3_NAME"].ConvertToString().ConvertToString(),        //  photo  3
                                                   paramTable.Rows[i]["PHOTO_4_NAME"].ConvertToString().ConvertToString(),        //photo  4
                                                   "G" ,       //status
                                                   "Y" ,       //active
                                                   SessionCheck.getSessionUsername(), //entered by
                                                   DateTime.Now,                       //entered date
                                                   System.DateTime.Now.ConvertToString(), //entered date loc
                                                   DBNull.Value,                                     // approved
                                                   SessionCheck.getSessionUsername(),      //approved by
                                                   DateTime.Now,                           //approved date 
                                                   System.DateTime.Now.ConvertToString(),  //approved date loc
                                                   SessionCheck.getSessionUsername(),      // updated by
                                                   DateTime.Now,                           //update date
                                                   System.DateTime.Now.ConvertToString(),  //updated date loc
                                                   DBNull.Value,                    // installment
                                                   DBNull.Value,                          // map design code 
                                                   paramTable.Rows[i]["HOUSE_PLAN_NAME"].ConvertToString(),   // passed map no,,gharko mota moti naksa
                                                   DBNull.Value,                    // other info
                                                   batchID.ToDecimal(),                     // file batch id
                                                   paramTable.Rows[i]["INSPECTION_TYPE"].ConvertToString(),      //  Inspection Type/house model
                                                   paramTable.Rows[i]["INSPECTION_DATE"].ConvertToString(),      //  INSPECTION_DATE 
                                                   BeneficiaryFullName,     //  BENEFICIARY_NAME 
                                                   paramTable.Rows[i]["TOLE"].ConvertToString(),                 //  TOLE   
                                                   paramTable.Rows[i]["LAND_PLOT_NUMBER"].ConvertToString(),     //  LAND_PLOT_NUMBER
                                                   paramTable.Rows[i]["ORGANIZATION_OTHERS"].ConvertToString(),  //ORGANIZATION_OTHERS
                                                   paramTable.Rows[i]["PHOTO_5"].ConvertToString(),              //PHOTO_CD_5 
                                                   paramTable.Rows[i]["PHOTO_5_NAME"].ConvertToString(),         //PHOTO_5 
                                                   DBNull.Value,                                         //PHOTO_CD_6 
                                                   DBNull.Value,                                         // photo 6 
                                                   FinalDecision.ConvertToString(),                              //Final Decision
                                                   paramTable.Rows[i]["LATITUDE"].ConvertToString(),             //LATITUDE
                                                   paramTable.Rows[i]["LONGITUDE"].ConvertToString(),            //LONGITUDE
                                                   paramTable.Rows[i]["ALTITUDE"].ConvertToString(),             //ALTITUDE 
                                                   InspectionMstId.ToDecimal(),                                  // mst id
                                                   bankSelect.ToDecimal(),                //BANK_NAME_cd
                                                   paramTable.Rows[i]["BANK_ACCOUNT_NUMBER"].ConvertToString(),  //BANK_ACCOUNT_NUMBER
                                                   paramTable.Rows[i]["FORM_NUMBER"].ConvertToString(),          //serial number
                                                   DBNull.Value,                                         //House owner id
                                                   Phone.ConvertToString(),                                     //PHONE_NUMBER
                                                   paramTable.Rows[i]["FORM_PAD_NUMBER"].ConvertToString(),     //FORM_PAD_NUMBER
                                                  paramTable.Rows[i]["DESIGN_DETAILS"].ConvertToString(),       //DESIGN_DETAILS
                                                  EditRequired.ConvertToString(),                               //EDIT_REQUIRED
                                                  paramTable.Rows[i]["EDIT_REQUIRED_DETAILS"].ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  bankSelect.ConvertToString(),          // BANK_SELECT
                                                  paramTable.Rows[i]["BANK_BRANCH"].ConvertToString(),          //BANK_BRANCH
                                                  paramTable.Rows[i]["FINAL_REMARKS"].ConvertToString(),        // FINAL_REMARKS
                                                  AcceptTheEntry.ConvertToString(),                             //ACCEPT_THE_ENTRY
                                                  GpsTaken.ConvertToString(),                                   //GPS_TAKEN
                                                  FinalDecision2.ConvertToString(),                       //FINAL_DECISION_2
                                                  paramTable.Rows[i]["BANK_NOT_AVAILABLE_REMARKS"].ConvertToString(),         // bank no remarks
                                                  "N" , // final decision approve
                                                  "N" , // final decision 2 approve
                                                  DBNull.Value,  //approve 1 batch
                                                  DBNull.Value, //approve 2 batch
                                                  paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString(),
                                                  InspectionHistoryId
                                               );

                            string cmdText = "";

                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "1")
                            {
                                inspectionType = "2";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "2")
                            {
                                inspectionType = "3";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "3")
                            {
                                inspectionType = "4";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "4")
                            {
                                inspectionType = "5";
                            }
                            if (inspectionType == "7" && paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() == "6")
                            {
                                inspectionType = "6";
                            }

                            if (inspectionType != "7")
                            {
                                cmdText = "Select INSPECTION_CODE_ID,DEFINED_CD, UPPER_INSPECTION_CODE_ID, Group_FLAG,  VALUE_TYPE," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG  from NHRS_INSPECTION_DESC_DTL "

                              + "WHERE  1=1 "
                              + "START with (INSPECTION_CODE_ID LIKE '%"
                              + HierarchyParentID + "%' AND  UPPER_INSPECTION_CODE_ID is null)"
                              + " CONNECT by prior INSPECTION_CODE_ID = UPPER_INSPECTION_CODE_ID ";

                                dtHierarchy = service.GetDataTable(new
                                {
                                    query = cmdText,
                                    args = new { }
                                });
                            }
                            DataTable dtPaperId = new DataTable();
                            string cmdTextUpdate = "select INSPECTION_PAPER_ID from NHRS_INSPECTION_PAPER_DTL WHERE INSPECTION_LEVEL='" + paramTable.Rows[i]["INSPECTION_NUMBER"].ConvertToString() + "' AND NRA_DEFINED_CD='" + paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString() + "'";

                            dtPaperId = service.GetDataTable(new
                            {
                                query = cmdTextUpdate,
                                args = new { }
                            });
                            InspPprId = dtPaperId.Rows[0][0].ConvertToString();

                            //insert inspection detail history
                            DataTable inspectionDetail = new DataTable();
                            string cmdSelDetl = "select * from NHRS_INSPECTION_DETAIL WHERE INSPECTION_PAPER_ID='" + dtPaperId.Rows[0][0].ConvertToString() + "'";

                            inspectionDetail = service.GetDataTable(new
                            {
                                query = cmdSelDetl,
                                args = new { }
                            });
                            if (inspectionDetail != null && inspectionDetail.Rows.Count>0)
                            {
                                foreach(DataRow  dr in inspectionDetail.Rows)
                                {
                                    QueryResult qrr = service.SubmitChanges("PR_INSPECTION_DTL_HISTORY",
                                                  

                                                   DBNull.Value,
                                                   InspectionHistoryId.ToDecimal(),
                                                   dr["INSPECTION_DETAIL_ID"].ToDecimal(),
                                                   dr["DEFINED_CD"].ToDecimal(),
                                                   dr["INSPECTION_PAPER_ID"].ToDecimal(),
                                                   dr["INSPECTION_ID"].ToDecimal(),
                                                   dr["INSPECTION_ELEMENT_ID"].ConvertToString(),
                                                   dr["VALUE_TYPE"].ConvertToString(),
                                                   dr["REMARKS"].ConvertToString(),             //  Inspection Type/house model
                                                   dr["HOUSE_MODEL"].ToDecimal(),
                                                   dr["COMPLY_FLAG"].ConvertToString(),
                                                   dr["COMPLY_ID"].ToDecimal()
                                                   );
                                }
                            }





                            if (dtPaperId.Rows.Count > 0)
                            {
                                string deleteInspectionDetail = "DELETE FROM NHRS_INSPECTION_DETAIL WHERE INSPECTION_PAPER_ID='" + dtPaperId.Rows[0][0].ConvertToString() + "'";

                                service.SubmitChanges(deleteInspectionDetail, null);
                            }



                      

                            if (inspectionType.ConvertToString() != "7")
                            {


                                if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                                {
                                    string Inspection_code = "";
                                    string value = "";
                                    string Remarks = "";



                                    foreach (DataRow dr in dtHierarchy.Rows)
                                    {



                                        if (inspectionType.ToDecimal() > 0 && inspectionType.ToDecimal() < 7)
                                        {

                                            int j = 31;
                                            do
                                            {


                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value 
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 44);
                                        }
                                        if (inspectionType == "1") // for saving A and B type
                                        {

                                            int j = 45;
                                            do
                                            {


                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value 
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 87);
                                        }

                                        if (inspectionType == "6")  // for saving rcc type
                                        {
                                            int j = 131;
                                            do
                                            {

                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();

                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value 
                                                    );

                                                }
                                                j = j + 2;


                                            }
                                            while (j <= 177);
                                        }
                                        if (inspectionType.ToDecimal() >= 2 && inspectionType.ToDecimal() <= 5)  //saving bmc smc,bmm smm
                                        {
                                            int j = 89;
                                            do
                                            {
                                                if (paramTable.Columns[j].ColumnName.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                                    value = paramTable.Rows[i][j].ConvertToString();//FINAL_DECISION 
                                                    Remarks = paramTable.Rows[i][j + 1].ConvertToString();
                                                    if (value == "2")
                                                    {
                                                        value = "N";
                                                    }
                                                    if (value == "1")
                                                    {
                                                        value = "Y";
                                                    }

                                                    QueryResult qrr = service.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                                   "I",

                                                    DBNull.Value,
                                                    DBNull.Value,
                                                    InspPprId.ToDecimal(),
                                                    DBNull.Value,
                                                    Inspection_code.ToDecimal(),
                                                    value.ConvertToString(),
                                                    Remarks.ConvertToString(),
                                                    DBNull.Value,             //  Inspection Type/house model
                                                    DBNull.Value,
                                                    DBNull.Value 
                                                    );

                                                }
                                                j = j + 2;
                                            }
                                            while (j <= 129);
                                        }
                                    }

                                }
                            }



                            if (inspectionType == "7")//for own design
                            {
                                
                                string ConstructionStatus = "";

                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "1")
                                {
                                    ConstructionStatus = "Foundation Under Construction";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "2")
                                {
                                    ConstructionStatus = "Foundation Construction Completed";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "3")
                                {
                                    ConstructionStatus = "Upto Roof Band in Ground Floor";
                                }
                                if (paramTable.Rows[i]["FOUR_NO1"].ConvertToString() == "4")
                                {
                                    ConstructionStatus = "Ground Floor Construction Completed (Including Roof)";
                                }



                                qrOwnDesign = service.SubmitChanges("PR_NHRS_INSPECTION_OWN_DESIGN",
                                        "U".ConvertToString(),

                                          DBNull.Value,
                                          DBNull.Value,

                                          InspPprId.ToDecimal(),                            // inspection paper id 
                                          designID.ConvertToString(),                       // design number FOUR_NO1

                                          DBNull.Value,       //base constructing 
                                          DBNull.Value,            //base constructed
                                          DBNull.Value,                    // ground roof finished
                                          DBNull.Value,   // ground floor completed 

                                           paramTable.Rows[i]["FOUR_NO2"].ConvertToString(), //floor count

                                          paramTable.Rows[i]["FOUR_NO3_A"].ConvertToString(),//base/foundation material
                                          paramTable.Rows[i]["FOUR_NO3_B"].ConvertToString(), // foundation depth below ground
                                          paramTable.Rows[i]["FOUR_NO3_C"].ConvertToString(), //foundation width
                                          paramTable.Rows[i]["FOUR_NO3_D"].ConvertToString(),  // foundation height above ground level

                                          paramTable.Rows[i]["FOUR_NO4_A"].ConvertToString(),  // ground floor constructionmaterial
                                          paramTable.Rows[i]["FOUR_NO4_B"].ConvertToString(),   // ground floor construction technique
                                          paramTable.Rows[i]["FOUR_NO4_C"].ConvertToString(),  // detail description wall type

                                          DBNull.Value,   //extra column
                                          paramTable.Rows[i]["FOUR_NO5_A"].ConvertToString(),  // ground floor roof construction material
                                          paramTable.Rows[i]["FOUR_NO5_B"].ConvertToString(),  // ground floor roof construction technique
                                          paramTable.Rows[i]["FOUR_NO5_C"].ConvertToString(),   // ground floor roof detail description

                                          paramTable.Rows[i]["FOUR_NO6_A"].ConvertToString(),  // first floor construction material
                                          paramTable.Rows[i]["FOUR_NO6_B"].ConvertToString(),  // first floor construction technique
                                           paramTable.Rows[i]["FOUR_NO6_C"].ConvertToString(),  // first floor detail description

                                          paramTable.Rows[i]["FOUR_NO7_A"].ConvertToString(),  // roof construction material
                                          paramTable.Rows[i]["FOUR_NO7_B"].ConvertToString(),   // roof construction technique
                                         paramTable.Rows[i]["FOUR_NO7_C"].ConvertToString(),   // roof dettail description

                                          DBNull.Value,
                                          DBNull.Value,
                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),

                                          DBNull.Value,
                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),

                                          SessionCheck.getSessionUsername(),
                                          DateTime.Now,
                                          System.DateTime.Now.ConvertToString(),
                                          ConstructionStatus.ConvertToString() ,
                                          InspectionHistoryId.ToDecimal()
                                        );






                            }
                           
                            // inspection process detail import
                            qreProcess = service.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                        "U",

                                         DBNull.Value,
                                         DBNull.Value,
                                          paramTable.Rows[i]["HOUSE_OWNER_S_NAME"].ConvertToString(),          //BenfullnameEng
                                          paramTable.Rows[i]["RELATIONSHIP_TO_HOUSE_OWNER"].ConvertToString(), //relationWithbenf


                                         paramTable.Rows[i]["MOUD_DLPIU_ENGINEER"].ConvertToString(),  //engineer name
                                         EngineerPost.ConvertToString(),                               //engineer designation 

                                         DBNull.Value,            //engineer name
                                         DBNull.Value,           //engineer designation 



                                         DBNull.Value,// register date
                                         DBNull.Value,//proceed date 
                                         DBNull.Value,//approved date 
                                         DBNull.Value,

                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),

                                         DBNull.Value,
                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),

                                         SessionCheck.getSessionUsername(),
                                         DateTime.Now,
                                         System.DateTime.Now.ConvertToString(),
                                         InspPprId.ToDecimal() ,
                                         InspectionHistoryId.ToDecimal()


                                    );




                        }
                        catch (OracleException oe)
                        {
                            //if (batchID != "")
                            //{
                            //    res = true;
                            //    exc += oe.Message.ToString();
                            //    ExceptionManager.AppendLog(oe);

                            //    string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + InspectionMstId + "'AND INSPECTION_LEVEL='" + InspectionLevel + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from nhrs_inspection_mst where nra_defined_cd='" + paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString().Trim() + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                            //    SaveErrorMessgae(cmdText);


                            //}
                        }
                        catch (Exception ex)
                        {
                            //if (batchID != "")
                            //{
                            //    res = true;
                            //    exc += ex.Message.ToString();
                            //    ExceptionManager.AppendLog(ex);

                            //    string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + InspectionMstId + "'AND INSPECTION_LEVEL='" + InspectionLevel + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("delete from nhrs_inspection_mst where nra_defined_cd='" + paramTable.Rows[i]["GRANT_AGREEMENT_NUMBER"].ConvertToString().Trim() + "'");
                            //    SaveErrorMessgae(cmdText);

                            //    cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                            //    SaveErrorMessgae(cmdText);

                            //}
                        }
                    }

                }
                catch (OracleException oe)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                        string cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);

                    }
                }
                catch (Exception ex)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);


                    }
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrFIleBatch != null)
                    {
                        res = qrFIleBatch.IsSuccess;
                    }
                }

                return res;

            }
        }

        // save duplicate data 
        public Boolean saveDuplicateInspectionData(string paNumber, string formNumber, string benfname, string district, string vdc, string ward, string fileName, string reason)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qrSaveDuplicateInspection = null;
            bool res = false;
            string duplicateId = "";
            string fileBatch = "";
          

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrSaveDuplicateInspection = service.SubmitChanges("PR_NHRS_INSPECTION_DUPLI_DATA",
                                               "I",
                                              duplicateId.ToDecimal(),
                                               DBNull.Value,
                                               DateTime.Now,                    // inspection date 
                                               paNumber.ConvertToString(),
                                               formNumber.ConvertToString(),
                                               benfname.ConvertToString(),
                                               district.ToDecimal(),                     // district
                                               vdc.ToDecimal(),             // vdc mun cd
                                               ward.ToDecimal(),                         // ward TOLE

                                               DBNull.Value,
                                               DBNull.Value,
                                               DBNull.Value,
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),


                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),
                                                fileName.ConvertToString(),
                                                DBNull.Value,
                                                reason.ConvertToString()


                                                   );









                }


                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrSaveDuplicateInspection != null)
                    {
                        res = qrSaveDuplicateInspection.IsSuccess;
                    }
                }

                return res;

            }
        }

        //save duplicate applicant
        public Boolean saveDuplicateApplicant(string paNumber, string Inspection, string benfname, string fileName, string reasonToSave)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qrSaveDuplicateInspection = null;
            bool res = false;
            string duplicateId = "";
            string definedId = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_IMPORT";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrSaveDuplicateInspection = service.SubmitChanges("PR_NHRS_APPLICANT_DUPLI",
                                               "I",
                                               DBNull.Value,
                                               paNumber.ConvertToString(),
                                               fileName.ConvertToString(),
                                               Inspection.ToDecimal(),
                                               benfname.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString() ,
                                               reasonToSave.ConvertToString()
                                                
                                          );









                }


                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrSaveDuplicateInspection != null)
                    {
                        res = qrSaveDuplicateInspection.IsSuccess;
                    }
                }

                return res;

            }
        }


        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }

        //grt duplicate import
        public DataTable GetDuplicateImport(string dist, string vdc, string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT IDD.IMPORT_DATE,IDD.REASON,IDD.NRA_DEFINED_CD,IDD.FORM_NUMBER,IDD.BENEFICIARY_NAME,IDD.DISTRICT_CD,IDD.VDC_MUN_CD,IDD.WARD_CD,IDD.FILE_NAME,"
                + "DIS.DESC_ENG DISTRICT_ENG, DIS.DESC_LOC DISTRICT_LOC,   VDC.DESC_ENG VDC_MUNICIPALITY_ENG,  VDC.DESC_LOC VDC_MUNICIPALITY_LOC "
                + "FROM NHRS_INSPECTION_DUPLICATE_DATA IDD "
                + "left outer JOIN MIS_DISTRICT DIS ON IDD.DISTRICT_CD = DIS.DISTRICT_CD "
                + "left outer JOIN MIS_VDC_MUNICIPALITY VDC ON IDD.VDC_MUN_CD = VDC.VDC_MUN_CD WHERE 1=1";

                if (dist != null && dist != "")
                {
                    cmd = cmd + "AND IDD.DISTRICT_CD='" + dist.ConvertToString() + "'";
                }
                if (dist != null && dist != "")
                {
                    cmd = cmd + "AND IDD.VDC_MUN_CD='" + vdc.ConvertToString() + "'";
                }
                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND IDD.FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY IDD.IMPORT_DATE";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }

        //get conflicted applicant
        public DataTable GetConflictedApplicant( string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * from NHRS_INSP_APPLICANT_DUPLICATE WHERE FILE_NAME ='" + fileName .ConvertToString()+ "'"; 
                 
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }


        public System.Data.DataTable GetDataImportRecordByDistrict(string district,string vdc)
        {
            DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_INSPECTION_IMPORT";
                    dtbl = service.GetDataTable(true, "PR_GET_IMPORTED_FILE",
                         district.ToDecimal(),//DistrictID
                          vdc.ToDecimal(),

                        DBNull.Value);
                    // dtbl = service.GetDataTable(cmdTxt, null);

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }

         
        public bool ImportInspectionRegistration(DataTable paramTable, string fileName, out string exc, string district, string vdc)
        {
            bool result = false;
            string batchID = "";
            exc = string.Empty;
            CommonFunction common = new CommonFunction();
            QueryResult qrFIleBatch = null;
            QueryResult qrRegister = null;

            string InspectionHistoryId = "";
            string InspectionMstId = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_IMPORT";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    //qrFIleBatch = service.SubmitChanges("PR_INSPECTION_FILE_BATCH",
                    //                           "I",
                    //                            DBNull.Value,
                    //                            district.ToDecimal(),
                    //                            vdc.ToDecimal(),
                    //                            "Completed",
                    //                            fileName,//filename                                                 
                    //                            DateTime.Now,
                    //                            SessionCheck.getSessionUsername(),
                    //                            DBNull.Value);
                    //batchID = qrFIleBatch["v_BATCH_ID"].ConvertToString();

                  

                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {




                        //info from applicant excel will be saved to inspection_mst and inspection_design only when application is for inspection 1
                        //after application for one , applicant table will be updated
                        if( paramTable.Rows[i]["INSPECTION"].ConvertToString()=="1")
                        {
                            string inspectionType = paramTable.Rows[i]["INSPECTION_TYPE"].ConvertToString();
                            string designNumber = paramTable.Rows[i]["DESIGN_NUMBER"].ConvertToString();
                            string designID = "";    //actual desin id
                            string HierarchyParentID = "";
                            string OwnDesignFlag = "N";
                            string designIdForOwnDesign = "";


                            // check if own design or from available design
                            if (paramTable.Rows[i]["BUILD_MATERIAL"].ConvertToString() != "" || paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ConvertToString() != "")
                            {
                                if (inspectionType == "7")
                                {
                                    OwnDesignFlag = "Y";

                                }
                            }

                            if (designNumber == "" || designNumber == null)
                            {

                                if (inspectionType == "1")
                                {
                                    designNumber = "19";
                                }
                                if (inspectionType == "6")
                                {
                                    designNumber = "18";
                                }
                                if (inspectionType == "7")
                                {
                                    designNumber = "20";
                                }
                            }

                            DataTable dtHierarchy = new DataTable();
                            if (designNumber != "" && designNumber != null)
                            {
                                string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + designNumber + "'";
                                dtHierarchy = service.GetDataTable(new
                                {
                                    query = cmdTextDesign,
                                    args = new { }
                                });
                            }

                            // set hierarchy code and design_id
                            if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                            {
                                if (OwnDesignFlag == "Y")
                                {
                                    designIdForOwnDesign = "";
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }
                                if (OwnDesignFlag == "N")
                                {
                                    designIdForOwnDesign = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                    HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                }

                            }

                            
                            string complyFlag = "N";
                             

                            //QueryResult qrMst = service.SubmitChanges("PR_NHRS_INSPECTION_MST",
                            //                          "I",

                            //                           DBNull.Value,                              //mst id
                            //                           DBNull.Value,                        // defined id
                            //                           0,
                            //                           DBNull.Value,                                //inspectionDate
                            //                           "R" ,

                            //                           SessionCheck.getSessionUsername(),
                            //                            DateTime.Now,
                            //                            System.DateTime.Now.ConvertToString(),

                            //                            DBNull.Value,
                            //                            SessionCheck.getSessionUsername(),
                            //                            DateTime.Now,
                            //                            System.DateTime.Now.ConvertToString(),

                            //                            SessionCheck.getSessionUsername(),
                            //                            DateTime.Now,
                            //                            System.DateTime.Now.ConvertToString(),
                            //                            paramTable.Rows[i]["PA NO"].ConvertToString(),
                            //                            DBNull.Value,
                            //                            DBNull.Value,
                            //                            DBNull.Value,
                            //                            complyFlag.ConvertToString(),
                            //                          "N" ,  /// final approve flag
                            //                          "N" , // final decision 
                            //                          InspectionHistoryId.ToDecimal()

                            //                      );
                            //InspectionMstId = qrMst["v_INSPECTION_MST_ID"].ConvertToString();




                            //save design part
                            //QueryResult qrDesign = service.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                            //                      "I",

                            //                       DBNull.Value,                                                 // design id 
                            //                       DBNull.Value,                                                  // defined cd
                            //                       paramTable.Rows[i]["PA NO"].ConvertToString(), //nra defined cd
                            //                       paramTable.Rows[i]["Applicant"].ConvertToString(),       // beneficiary full name 
                            //                       paramTable.Rows[i]["DISTRICT"].ToDecimal(),                     // district
                            //                       paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),             // vdc mun cd
                            //                       paramTable.Rows[i]["WARD"].ToDecimal(),                         // ward TOLE
                            //                       DBNull.Value,                                            // tole
                            //                       OwnDesignFlag.ConvertToString(),                                // own design/template

                            //                       designIdForOwnDesign.ConvertToString(),                         // design number
                            //                       paramTable.Rows[i]["BUILD_MATERIAL"].ToDecimal(),               // construction material
                            //                       paramTable.Rows[i]["ROOF_AND_MATERIALS_USED"].ToDecimal(),      // roof material  

                            //                       paramTable.Rows[i]["BUILD_MATERIAL_OTHER"].ConvertToString(),    // other construction material
                            //                       paramTable.Rows[i]["ROOF_AND_MATERIALS_OTHER"].ConvertToString(),   // other roof material

                            //                        SessionCheck.getSessionUsername(),
                            //                        DateTime.Now,
                            //                        System.DateTime.Now.ConvertToString(),

                            //                        DBNull.Value,
                            //                        SessionCheck.getSessionUsername(),
                            //                        DateTime.Now,
                            //                        System.DateTime.Now.ConvertToString(),

                            //                        SessionCheck.getSessionUsername(),
                            //                        DateTime.Now,
                            //                        System.DateTime.Now.ConvertToString(),

                            //                       InspectionMstId.ToDecimal(),
                            //                       "R" ,                                          // design status
                            //                        paramTable.Rows[i]["LAND_PLOT_NUMBER"].ConvertToString(),        // kitta number
                            //                       InspectionHistoryId.ToDecimal()
                            //                        );

                        }



                        string mode = "";
                        string appForInspOne   = "";
                        string AppForInspTwo   = "";
                        string AppForInspThree = "";
                       

                        //if (paramTable.Rows[i]["INSPECTION"].ConvertToString() == "1")
                        //{
                        //    mode = "I";
                        //    appForInspOne = "1";

                        //}
                        if (paramTable.Rows[i]["INSPECTION"].ConvertToString() == "2"  )
                        {
                            mode = "U";
                            appForInspOne = "1";
                            AppForInspTwo = "2";
                        }
                        if (paramTable.Rows[i]["INSPECTION"].ConvertToString() == "3")
                        {
                            mode = "U";
                            appForInspOne = "1";
                            AppForInspTwo = "2";
                            AppForInspThree = "3";
                        }

                        qrRegister = service.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                               mode.ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               paramTable.Rows[i]["DISTRICT"].ToDecimal(),
                                               paramTable.Rows[i]["VDC_MUNICIPALITY"].ToDecimal(),
                                               paramTable.Rows[i]["WARD"].ToDecimal(),
                                               paramTable.Rows[i]["PA NO"].ConvertToString(),
                                           
                                               paramTable.Rows[i]["Applicant"].ConvertToString(),
                                               paramTable.Rows[i]["Inspection"].ToDecimal(),
                                               batchID.ToDecimal(),
                                               paramTable.Rows[i]["Remarks"].ToDecimal(),


                                               DBNull.Value,
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),

                                             
                                               paramTable.Rows[i]["Reg_No"].ToDecimal(),
                                               //paramTable.Rows[i]["Reg_Date"].ConvertToString(),
                                              DBNull.Value,
                                               paramTable.Rows[i]["Schedule"].ToDecimal(),
                                               DBNull.Value,
                                               appForInspOne.ToDecimal(),
                                               AppForInspTwo.ToDecimal(),
                                               AppForInspThree.ToDecimal()
                                              


                       );








                    }

                }
                 catch (OracleException oe)
                        {
                            if (batchID != "")
                            {
                                result = true;
                                exc += oe.Message.ToString();
                                ExceptionManager.AppendLog(oe);






                                string cmdText = String.Format("delete from NHRS_INSPECTION_MST where INSPECTION_MST_ID='" + InspectionMstId + "'");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_DESIGN where INSPECTION_MST_ID='" + batchID + "'");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("delete from NHRS_INSPECTION_APPLICATION where FILE_BATCH='" + batchID + "'");
                                SaveErrorMessgae(cmdText);

                                cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                                SaveErrorMessgae(cmdText);

                            }
                        }
                        catch (Exception ex)
                        {
                            if (batchID != "")
                            {
                                result = true;
                                exc += ex.Message.ToString();
                                ExceptionManager.AppendLog(ex);
 

                                string cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                                SaveErrorMessgae(cmdText);

                            }
                        }
                    
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qrFIleBatch != null)
                    {
                        result = qrFIleBatch.IsSuccess;
                    }
                }
                return result;
            }
        }
    }
}
