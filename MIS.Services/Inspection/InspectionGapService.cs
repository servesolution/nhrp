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
    public class InspectionGapService
    {
        string InspectionUpdateHistoryId = "";
        public bool saveOwnInspectionDesign(InspectionDesignModelClass objInspectn, InspectionOwnDesign objOwnDesign)
        {
            string exc = string.Empty;
            bool res = false; 
            bool result = false;
            QueryResult qre = null;
            string InspectionPaperId = "";
            string InsProcessId = ""; 
            string DEFINED_CD = "";
            string INSPECTION_CODE_ID = "";
            string MAP_DESIGN = "";
            string RC_MATERIAL_CD = "";
            string FC_MATERIAL_CD = "";
            string TECHNICAL_ASSIST = "";
            string ORGANIZATION_TYPE = "";
            string CONSTRUCTOR_TYPE = "";
            string SOIL_TYPE = "";
            string HOUSE_MODEL = "";


            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();

                    DataTable InspLevel = new DataTable();

                    string InspLvl = "select * FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' ";
                    InspLevel = sf.GetDataTable(new
                    {
                        query = InspLvl,
                        args = new { }
                    });
                    if (InspLevel == null || InspLevel.Rows.Count > 0)
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                        objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                        objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();
                    }
                    if (objInspectn.Inspection == "1")
                    {

                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {

                        objInspectn.InspectionLevel2 = "2";
                    }
                    if (objInspectn.Inspection == "3")
                    {
                        objInspectn.InspectionLevel3 = "3";
                    } 
                    DataTable dtHistory = new DataTable();
                    string inspectionHIstoryId = "";
                    

                    string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                    dtHistory = sf.GetDataTable(new
                    {
                        query = cmdHistoryid,
                        args = new { }
                    });

                    if (dtHistory != null && dtHistory.Rows.Count > 0)
                    {
                        inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    }

                    string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + inspectionHIstoryId + "')";
                    QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);


                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

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
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                           objInspectn.complyFlag.ConvertToString(),
                                           "N", // final decision 2
                                           "N",// final decision 
                                           inspectionHIstoryId.ToDecimal()


                                      );
                    //InspectionMstId = objInspectn.InspectionMstId.ConvertToString();

                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                              objInspectn.Mode,
                                               DBNull.Value,
                        //objInspectn.InspectionPaperId.ToDecimal(),
                                                DEFINED_CD.ToDecimal(),

                                                INSPECTION_CODE_ID.ToDecimal(),

                                                objInspectn.NraDefCode,
                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(), 
                                                MAP_DESIGN.ConvertToString(),
                                                objInspectn.DesignNumber.ConvertToString(),//design number
                                                RC_MATERIAL_CD.ToDecimal(),
                                                FC_MATERIAL_CD.ToDecimal(),
                                                TECHNICAL_ASSIST.ConvertToString(), 
                                                ORGANIZATION_TYPE.ConvertToString(),
                                                CONSTRUCTOR_TYPE.ConvertToString(),
                                                SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),
                                                objInspectn.PHOTO_CD_1.ConvertToString(),
                                                objInspectn.PHOTO_CD_2.ConvertToString(),
                                                objInspectn.PHOTO_CD_3.ConvertToString(),
                                                objInspectn.PHOTO_CD_4.ConvertToString(), 
                                                objInspectn.PHOTO_1,
                                                objInspectn.PHOTO_2,
                                                objInspectn.PHOTO_3,
                                                objInspectn.PHOTO_4, 
                                                 "G",
                                                 "Y",
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

                                                 DBNull.Value,                                //InspectnInstallment
                                                 DBNull.Value,                                //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),     //Passed_map_no gharko mota moti naksa
                                                 DBNull.Value,                          //Others
                                                 DBNull.Value,                                //Filebatch

                                                DBNull.Value,                                            //InspectionType
                                                DateTime.Now,                                   //INSPECTION_DATE
                                                objInspectn.BenfFullNameEng.ConvertToString(),  //BENEFICIARY_NAME
                                                objInspectn.tole.ConvertToString(),             //TOLE
                                                objInspectn.KittaNumber.ConvertToString(),      //LAND_PLOT_NUMBER
                                                objInspectn.ORGANIZATION_OTHERS.ConvertToString(),//ORGANIZATION_OTHERS
                                                objInspectn.PHOTO_CD_5,
                                                objInspectn.PHOTO_5.ConvertToString(),
                                                objInspectn.PHOTO_CD_6,
                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),//FINAL_DECISION
                                                objInspectn.LATITUDE.ConvertToString(),
                                                objInspectn.LONGITUDE.ConvertToString(),
                                                objInspectn.ATTITUDE.ConvertToString(),
                                                objInspectn.InspectionMstId.ToDecimal(),
                                                objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num.ConvertToString(),
                                                objInspectn.Serial_Num.ConvertToString(),
                                                objInspectn.hOwnerId.ConvertToString(),
                                                objInspectn.MobileNumber.ConvertToString(),
                                                objInspectn.form_pad_number.ConvertToString(),    //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),   //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),    //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),    // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),         //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),     // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),    //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),   //GPS_TAKEN
                                                  objInspectn.finalDecision2.ConvertToString(),    //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N",                            //FINAL DECISION APPROVE 
                                                  "N",                            // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                  objInspectn.Inspection.ConvertToString(),
                                                   inspectionHIstoryId.ToDecimal()

                    );

                    //Main Table 
                    InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (objInspectn.Mode == "U")
                    {
                        InspectionPaperId = objInspectn.InspectionPaperID.ConvertToString();
                    }

                    QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_OWN_DESIGN",
                              objInspectn.Mode,

                                DBNull.Value,
                                DBNull.Value,

                                InspectionPaperId.ToDecimal(),
                                objInspectn.DesignNumber.ConvertToString(),

                                objOwnDesign.PillarConstructing.ConvertToString(),
                                objOwnDesign.PillerConstructed.ConvertToString(),
                                objOwnDesign.GroundRoofCompleted.ConvertToString(),
                                objOwnDesign.GroundFloorCompleted.ConvertToString(),

                                objOwnDesign.FloorCount.ConvertToString(),

                                objOwnDesign.BaseConstructMaterial.ConvertToString(),
                                objOwnDesign.BaseDepthBelowGrnd.ConvertToString(),
                                objOwnDesign.BaseAvgWidth.ConvertToString(),
                                objOwnDesign.BseHeightAbvGrnd.ConvertToString(),

                                objOwnDesign.gndFloMat.ConvertToString(),
                                objOwnDesign.GrndFlorPrncpl.ConvertToString(),
                                objOwnDesign.WallStructDescpt.ConvertToString(),

                                objOwnDesign.FloorRoofDescrpt.ConvertToString(),
                                objOwnDesign.FloorRoofMat.ConvertToString(),
                                objOwnDesign.FloorRoofPrncpl.ConvertToString(),
                                objOwnDesign.FloorRoofDetl.ConvertToString(),

                                objOwnDesign.FirstFlorMat.ConvertToString(),
                                objOwnDesign.FirstFLorPrncpl.ConvertToString(),
                                objOwnDesign.FirstFlorDtl.ConvertToString(),

                                objOwnDesign.Roofmat.ConvertToString(),
                                objOwnDesign.RoofPrncpl.ConvertToString(),
                                objOwnDesign.RoofDetal.ConvertToString(),

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
                                objOwnDesign.ConstructionStatus.ConvertToString(),
                                inspectionHIstoryId.ToDecimal()

                           ); 
                    qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                      objInspectn.Mode,

                                       DBNull.Value,
                                       DBNull.Value,
                                       objInspectn.BeneficiaryFullName.ConvertToString(),
                                       objInspectn.BenfRelation.ConvertToString(),

                                       objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                        objInspectn.SupertntDesignation.ConvertToString(),

                                       objInspectn.EngineerFullname.ConvertToString(),
                                       objInspectn.EngineerDesignation.ConvertToString(),

                                       objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                       objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                       objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                       "Registered",

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
                                       InspectionPaperId.ToDecimal(),
                                        inspectionHIstoryId.ToDecimal()


                                  );
                    InsProcessId = qre["V_INSPECTION_PROCESS_ID"].ConvertToString();

                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;



                }
                catch (OracleException oe)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);
                        string cmdText = "";
                        if (objInspectn.Mode.ConvertToString() == "I")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (objInspectn.Mode.ConvertToString() == "U")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }




                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";

                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);
                        string cmdText = "";
                        if (objInspectn.Mode.ConvertToString() == "I")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (objInspectn.Mode.ConvertToString() == "U")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "' AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        string a = cmdText;
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";

                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (qre != null)
                {
                    result = qre.IsSuccess;
                }



            }
            return (result);
        }



        public string saveInspectionInfo(Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply, InspectionDesignModelClass objInspectn)
        {
            string exc = string.Empty;
            bool res = false;

            string InspectionPaperId = "";
            string InsProcessId = ""; 
            string DEFINED_CD = "";
            string INSPECTION_CODE_ID = ""; 
            string MAP_DESIGN = "";
            string RC_MATERIAL_CD = "";
            string FC_MATERIAL_CD = ""; 
            string HOUSE_MODEL = "";


            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();



                    DataTable InspLevel = new DataTable();

                    string InspLvl = "select * FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' ";
                    InspLevel = sf.GetDataTable(new
                    {
                        query = InspLvl,
                        args = new { }
                    });

                   // Session["InspectionLevel"] = (DataTable)InspLevel;
                    if (InspLevel == null || InspLevel.Rows.Count  >0)
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                        objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                        objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();
                    }
                    if (objInspectn.Inspection == "1")
                    {
                        
                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {
                         
                        objInspectn.InspectionLevel2 = "2";
                    }
                    if (objInspectn.Inspection == "3")
                    { 
                        objInspectn.InspectionLevel3 = "3";
                    }

                    //DataTable dtHistory = new DataTable();
                    //string cmdText = "Select INSPECTION_HISTORY_ID ,PPR_INSPECTION_LEVEL ,DEG_ENTERED_DATE FROM NHRS_INSPECTION_UPDATE_HISTORY"
                    //+ " WHERE DEG_NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' order  by DEG_ENTERED_DATE desc";
                    //dtHistory = sf.GetDataTable(cmdText, null);
                    //if (dtHistory != null && dtHistory.Rows.Count > 0 && objInspectn.Inspection == "1")
                    //{
                    //    InspectionUpdateHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    //}
                    //else
                    //{

                    DataTable dtHistory = new DataTable();
                    string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                    dtHistory = sf.GetDataTable(new
                    {
                        query = cmdHistoryid,
                        args = new { }
                    });

                    if (dtHistory != null && dtHistory.Rows.Count > 0)
                    {
                        InspectionUpdateHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    }

                    string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + InspectionUpdateHistoryId.ConvertToString() + "')";
                    QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);

                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

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
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                            objInspectn.complyFlag.ConvertToString(),
                                            "N", // final decision 2
                                            "N", // final decision 
                                            InspectionUpdateHistoryId.ToDecimal()


                                      );
                    //InspectionMstId = objInspectn.InspectionMstId.ConvertToString();

                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                              "I",
                                               DBNull.Value,
                        //objInspectn.InspectionPaperId.ToDecimal(),
                                                DEFINED_CD.ToDecimal(),

                                                INSPECTION_CODE_ID.ToDecimal(),

                                                objInspectn.NraDefCode,
                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(),



                                                MAP_DESIGN.ConvertToString(),
                                                objInspectn.DesignNumber.ConvertToString(),//design number
                                                RC_MATERIAL_CD.ToDecimal(),
                                                FC_MATERIAL_CD.ToDecimal(),
                                                objInspectn.TECHNICAL_ASSIST.ConvertToString(),

                                                objInspectn.ORGANIZATION_TYPE.ConvertToString(),
                                                objInspectn.CONSTRUCTOR_TYPE.ConvertToString(),
                                                objInspectn.SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),
                                                objInspectn.PHOTO_CD_1,
                                                objInspectn.PHOTO_CD_2,
                                                objInspectn.PHOTO_CD_3,
                                                objInspectn.PHOTO_CD_4,

                                                objInspectn.PHOTO_1.ConvertToString(),
                                                objInspectn.PHOTO_2.ConvertToString(),
                                                objInspectn.PHOTO_3.ConvertToString(),
                                                objInspectn.PHOTO_4.ConvertToString(),





                                                 "G",
                                                 "Y",
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

                                                 DBNull.Value,                                //InspectnInstallment
                                                 DBNull.Value,                                //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),     //Passed_map_no gharko mota moti naksa 
                                                 DBNull.Value,                          //Others
                                                 DBNull.Value,                                //Filebatch

                                                 DBNull.Value,                                            //InspectionType
                                                DateTime.Now,                                   //INSPECTION_DATE
                                                objInspectn.BenfFullNameEng.ConvertToString(),  //BENEFICIARY_NAME
                                                objInspectn.tole.ConvertToString(),             //TOLE
                                                objInspectn.KittaNumber.ConvertToString(),      //LAND_PLOT_NUMBER
                                                objInspectn.ORGANIZATION_OTHERS.ConvertToString(),
                                                objInspectn.PHOTO_CD_5,
                                                objInspectn.PHOTO_5,
                                                objInspectn.PHOTO_CD_6,
                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),//FINAL_DECISION
                                                objInspectn.LATITUDE,
                                                objInspectn.LONGITUDE,
                                                objInspectn.ATTITUDE,
                                                objInspectn.InspectionMstId.ToDecimal(),
                                                objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num,
                                                 objInspectn.Serial_Num.ConvertToString(),
                                                  objInspectn.hOwnerId.ConvertToString(),
                                                  objInspectn.MobileNumber.ConvertToString(),
                                                  objInspectn.form_pad_number.ConvertToString(),        //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),          //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),          //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),            // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),                  //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),           // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),        //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),              //GPS_TAKEN
                                                  objInspectn.final_decision_2.ConvertToString(),          //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N",                                    //FINAL DECISION APPROVE 
                                                  "N",                                    // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                  objInspectn.Inspection.ConvertToString(),
                                                  InspectionUpdateHistoryId.ToDecimal()
                    );

                    //Main Table 
                    InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (lstComply != null)
                    {
                        //insert inspection element
                        foreach (var item in lstComply)
                        {



                            QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                       "I",

                                        DBNull.Value,
                                        DBNull.Value,
                                        InspectionPaperId.ToDecimal(),
                                        DBNull.Value,
                                        item.Key.ToDecimal(),
                                        item.Value.Value1.ConvertToString(),
                                        item.Value.Value2.ConvertToString(),
                                        objInspectn.DesignNumber.ToDecimal(),
                                         objInspectn.complyFlag,
                                        objInspectn.complyCd.ToDecimal()


                                   );


                        }
                    }

                    QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                       "I",

                                        DBNull.Value,
                                        DBNull.Value,
                                        objInspectn.BeneficiaryFullName.ConvertToString(),
                                        objInspectn.BenfRelation.ConvertToString(),

                                        objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                         objInspectn.SupertntDesignation.ConvertToString(),

                                        objInspectn.EngineerFullname.ConvertToString(),
                                        objInspectn.EngineerDesignation.ConvertToString(),

                                        objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        "Registered",

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
                                        InspectionPaperId.ToDecimal(),
                                        InspectionUpdateHistoryId.ToDecimal()




                                   );
                    InsProcessId = qre["V_INSPECTION_PROCESS_ID"].ConvertToString();

                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;



                }
                catch (OracleException oe)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                             + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        //DataTable InspLevel = (DataTable)Session[""];
                        DataTable InspLevel = new DataTable();
                        if (InspLevel == null || InspLevel.Rows.Count > 0)
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                            objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                            objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();
                        }

                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";
                            objInspectn.InspectionLevel3 = "";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                         + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                         + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }

                if (res)
                {
                    res = false;
                    InspectionPaperId = "false";
                }

            }
            return (InspectionPaperId);
        }





        public string UpdateInspectionInfo(Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply, InspectionDesignModelClass objInspectn)
        {
            string exc = string.Empty;
            bool res = false;


            string InspectionPaperId = "";
            string InsProcessId = "";
            string DEFINED_CD = "";
            string HOUSE_MODEL = "";


            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();

                    DataTable InspLevel = new DataTable();

                    string InspLvl = "select * FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' ";
                    InspLevel = sf.GetDataTable(new
                    {
                        query = InspLvl,
                        args = new { }
                    });
                    if (InspLevel == null || InspLevel.Rows.Count > 0)
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                        objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                        objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();
                    }
                    if (objInspectn.Inspection == "1")
                    {

                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {

                        objInspectn.InspectionLevel2 = "2";
                    }
                    if (objInspectn.Inspection == "3")
                    {
                        objInspectn.InspectionLevel3 = "3";
                    }

                    DataTable dtHistory = new DataTable();
                    string inspectionHIstoryId = "";
                    

                    string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                    dtHistory = sf.GetDataTable(new
                    {
                        query = cmdHistoryid,
                        args = new { }
                    });

                    if (dtHistory != null && dtHistory.Rows.Count > 0)
                    {
                        inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    }

                    string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + inspectionHIstoryId + "')";
                    QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);


                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

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
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                            objInspectn.complyFlag.ConvertToString(),
                                            "N", // final decision 2
                                            "N", // final decision 
                                            inspectionHIstoryId.ToDecimal()


                                      );
                    //updated by pa number and design number
                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",

                                                objInspectn.Mode,
                                                DBNull.Value,
                                                DBNull.Value,//DEFINED_CD
                                                DBNull.Value,//INSPECTION_CODE_ID
                                                objInspectn.NraDefCode,

                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(),

                                                DBNull.Value,   //MAP_DESIGN
                                                objInspectn.DesignNumber.ConvertToString(),
                                                objInspectn.Roofmat.ToDecimal(),
                                                objInspectn.ConstructMat.ToDecimal(),
                                                objInspectn.TECHNICAL_ASSIST.ConvertToString(),

                                                objInspectn.ORGANIZATION_TYPE.ConvertToString(),
                                                objInspectn.CONSTRUCTOR_TYPE.ConvertToString(),
                                                objInspectn.SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),         //HOUSE_MODEL

                                                objInspectn.PHOTO_CD_1.ConvertToString(),
                                                objInspectn.PHOTO_CD_2.ConvertToString(),
                                                objInspectn.PHOTO_CD_3.ConvertToString(),
                                                objInspectn.PHOTO_CD_4.ConvertToString(),

                                                objInspectn.PHOTO_1.ConvertToString(),
                                                objInspectn.PHOTO_2.ConvertToString(),
                                                objInspectn.PHOTO_3.ConvertToString(),
                                                objInspectn.PHOTO_4.ConvertToString(),

                                                 "N",//status
                                                 DBNull.Value,//active
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

                                                 DBNull.Value,      //InspectnInstallment
                                                 DBNull.Value,            //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),//Passed_map_no GHARKO MOTAMOTI NAKSA
                                                 DBNull.Value,               //Others
                                                 DBNull.Value,                    //Filebatch

                                                 DBNull.Value,                                //InspectionType
                                                 objInspectn.Inspection_date_loc.ConvertToString(), //INSPECTION_DATE
                                                 objInspectn.BenfFullNameEng.ConvertToString(), //BENEFICIARY_NAME
                                                 objInspectn.tole.ConvertToString(),            //TOLE
                                                 objInspectn.KittaNumber.ConvertToString(),     //LAND_PLOT_NUMBER
                                                 objInspectn.ORGANIZATION_OTHERS.ConvertToString(),
                                                 objInspectn.PHOTO_CD_5,
                                                 objInspectn.PHOTO_5,
                                                 objInspectn.PHOTO_CD_6,

                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),
                                                objInspectn.LATITUDE,
                                                objInspectn.LONGITUDE,
                                                objInspectn.ATTITUDE,
                                                objInspectn.InspectionMstId.ToDecimal(),//  inspection mst id
                                                objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num,
                                                 objInspectn.Serial_Num.ConvertToString(),
                                                  objInspectn.hOwnerId.ConvertToString(),
                                                    objInspectn.MobileNumber.ConvertToString(),
                                                    objInspectn.form_pad_number.ConvertToString(),    //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),   //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),    //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),    // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),          //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),     // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),    //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),   //GPS_TAKEN
                                                  objInspectn.final_decision_2.ConvertToString(),    //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N",                            //FINAL DECISION APPROVE 
                                                  "N",                            // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                   objInspectn.Inspection.ConvertToString(),
                                                   inspectionHIstoryId.ToDecimal()


                    );

                    //Main Table 
                    //InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (lstComply != null)
                    {


                        //insert inspection detail history
                        DataTable inspectionDetail = new DataTable();
                        string cmdSelDetl = "select * from NHRS_INSPECTION_DETAIL WHERE INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'";

                        inspectionDetail = sf.GetDataTable(new
                        {
                            query = cmdSelDetl,
                            args = new { }
                        });
                        if (inspectionDetail != null && inspectionDetail.Rows.Count > 0)
                        {
                            foreach (DataRow dr in inspectionDetail.Rows)
                            {
                                QueryResult qrr = sf.SubmitChanges("PR_INSPECTION_DTL_HISTORY",


                                               DBNull.Value,
                                               inspectionHIstoryId.ToDecimal(),
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



                        //string cmdTextDel = "DELETE FROM NHRS_INSPECTION_DETAIL WHERE (HOUSE_MODEL=" + objInspectn.DesignNumber + " AND INSPECTION_PAPER_ID=" + objInspectn.InspectionPaperID + ")";
                        string cmdTextDel = "DELETE FROM NHRS_INSPECTION_DETAIL WHERE ( INSPECTION_PAPER_ID=" + objInspectn.InspectionPaperID + ")";

                        sf.Begin();
                        QueryResult qrdel = sf.SubmitChanges(cmdTextDel, null);
                        //insert inspection element
                        foreach (var item in lstComply)
                        {



                            QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                       "I",

                                        DBNull.Value,//INSPECTION_DETAIL_ID
                                        DBNull.Value,//DEFINED_CD
                                        objInspectn.InspectionPaperID.ToDecimal(),
                                        DBNull.Value,//INSPECTION_ID
                                        item.Key.ToDecimal(),//INSPECTION_ELEMENT_ID
                                        item.Value.Value1,//VALUE_TYPE
                                        item.Value.Value2,//REMARKS
                                        objInspectn.DesignNumber.ToDecimal(),//DESIGN/MODEL
                                        objInspectn.complyFlag,
                                        objInspectn.complyCd.ToDecimal()

                                   );


                        }
                    }

                    QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                       "U",

                                        DBNull.Value,
                                        DBNull.Value,
                                        objInspectn.BeneficiaryFullName.ConvertToString(),
                                        objInspectn.BenfRelation.ConvertToString(),

                                        objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                         objInspectn.SupertntDesignation.ConvertToString(),

                                        objInspectn.EngineerFullname.ConvertToString(),
                                        objInspectn.EngineerDesignation.ConvertToString(),

                                        objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        "N",//STATUS

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
                                        objInspectn.InspectionPaperID.ToDecimal(),
                                        inspectionHIstoryId.ToDecimal()



                                   );


                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;



                }
                catch (OracleException oe)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";
                            objInspectn.InspectionLevel3 = "";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                       + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                       + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";
                            objInspectn.InspectionLevel3 = "";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                       + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                       + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }



            }
            return (InspectionPaperId);
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
    
    }
}
