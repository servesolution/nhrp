using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using MIS.Models.Inspection;
using System.Data;
using System.Data.OracleClient;
using ExceptionHandler;
namespace MIS.Services.Inspection
{
    public class InspectionHigherLevelUploadService
    {


        public bool saveHigherInspection(List<SortedDictionary<dynamic, dynamic>> InspectionInfo, string filename)
        {

            QueryResult qrFIleBatch = null;
            bool result = false;
            string InspPprId = "";
            String InspectionMstId = "";
            string inspectionLevel = "";

            string batchID = "";
            string InspectionHistoryId = "";
            string designNumber = "";
            string inspectionNumber = "";
            string inspectionType = "";
            string designIdForOwnDesign = "";
            string designID = "";
            string HierarchyParentID = "";
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
            string Phone = "";

            DataTable dtHierarchy = new DataTable();
            var CommonInspectionDetail = new List<KeyValuePair<string, string>>();
            var InspectionDetail = new List<KeyValuePair<string, string>>();
            var InspectionRemarks = new List<KeyValuePair<string, string>>();



            List<SortedDictionary<dynamic, dynamic>> InspectionList = new List<SortedDictionary<dynamic, dynamic>>();
            JsonInspectionMst objMst = new JsonInspectionMst();
            JsonInspectionDesign objDesign = new JsonInspectionDesign();
            JsonInspectionApplication objApplication = new JsonInspectionApplication();
            JsonInspectionPaper objPapr = new JsonInspectionPaper();
            JonInspectionOwnDesign objOwnDesign = new JonInspectionOwnDesign();
            JsonInspectionProcess objProcess = new JsonInspectionProcess();
            InspectionJsonAdditionalService objAdditionalService = new InspectionJsonAdditionalService();
            InspectionUpdateJsonService objUpdateService = new InspectionUpdateJsonService();
            InspectionList = InspectionInfo.ToList();



            if (InspectionList.Count > 0)
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    try
                    {

                        service.PackageName = "NHRS.PKG_INSPECTION_IMPORT";
                        service.Begin();


                        //SAVE File info
                        //qrFIleBatch = service.SubmitChanges("PR_INSPECTION_FILE_BATCH",
                        //                           "I",
                        //                            DBNull.Value,   //file batch
                        //                            DBNull.Value,  //district
                        //                            DBNull.Value, //vdc
                        //                            "Completed",
                        //                            filename,       //filename                                                 
                        //                            DateTime.Now,
                        //                            SessionCheck.getSessionUsername(),
                        //                            DBNull.Value);
                        //batchID = qrFIleBatch["v_BATCH_ID"].ConvertToString();

                        //if (qrFIleBatch.IsSuccess)
                        //{
                        foreach (var InspectionItem in InspectionList)
                        {


                            foreach (SortedDictionary<dynamic, dynamic> PaInfo in InspectionItem.First(s => s.Key == "PArecord").Value)
                            {

                                objMst.NRA_DEFINED_CD = PaInfo.ContainsKey("PA_Record") ? Utils.ConvertToString(PaInfo.First(s => s.Key == "PA_Record").Value) : null;
                                objDesign.NRA_DEFINED_CD = PaInfo.ContainsKey("PA_Record") ? Utils.ConvertToString(PaInfo.First(s => s.Key == "PA_Record").Value) : null;
                                objApplication.NRA_DEFINED_CD = PaInfo.ContainsKey("PA_Record") ? Utils.ConvertToString(PaInfo.First(s => s.Key == "PA_Record").Value) : null;
                                objPapr.NRA_DEFINED_CD = PaInfo.ContainsKey("PA_Record") ? Utils.ConvertToString(PaInfo.First(s => s.Key == "PA_Record").Value) : null;


                                objMst.NRA_DEFINED_CD = "12-1-1-0-11";
                                objDesign.NRA_DEFINED_CD = "12-1-1-0-11";
                                objApplication.NRA_DEFINED_CD = "12-1-1-0-11";
                                objPapr.NRA_DEFINED_CD = "12-1-1-0-11";

                                goto PaInfoExit;
                            }
                        PaInfoExit:
                            foreach (SortedDictionary<dynamic, dynamic> ApplicationInfo in InspectionItem.First(s => s.Key == "ApplicationRecord").Value)
                            {
                                //mst info 
                                inspectionNumber = ApplicationInfo.ContainsKey("WHICH_INSPECTION") ? Utils.ConvertToString(ApplicationInfo.First(s => s.Key == "WHICH_INSPECTION").Value) : null;
                                inspectionLevel = ApplicationInfo.ContainsKey("WHICH_INSPECTION") ? Utils.ConvertToString(ApplicationInfo.First(s => s.Key == "WHICH_INSPECTION").Value) : null;
                                if (inspectionLevel == "3")
                                {
                                    objMst.INSPECTION_LEVEL1 = "1";
                                    objMst.INSPECTION_LEVEL2 = "2";
                                    objMst.INSPECTION_LEVEL3 = "3";
                                }
                                else
                                {
                                    if (inspectionLevel == "2")
                                    {
                                        objMst.INSPECTION_LEVEL1 = "1";
                                        objMst.INSPECTION_LEVEL2 = "2";
                                        objMst.INSPECTION_LEVEL3 = "";

                                    }
                                    else
                                    {
                                        if (inspectionLevel == "1")
                                        {
                                            objMst.INSPECTION_LEVEL1 = "1";
                                            objMst.INSPECTION_LEVEL2 = "";
                                            objMst.INSPECTION_LEVEL3 = "";
                                        }
                                    }
                                }



                                //paper dtl info 
                                objPapr.FORM_PAD_NUMBER = ApplicationInfo.ContainsKey("FORM_PAD_NUMBER") ? Utils.ConvertToString(ApplicationInfo.First(s => s.Key == "FORM_PAD_NUMBER").Value) : null;
                                objPapr.INSPECTION_LEVEL = ApplicationInfo.ContainsKey("WHICH_INSPECTION") ? Utils.ConvertToString(ApplicationInfo.First(s => s.Key == "WHICH_INSPECTION").Value) : null;

                                goto ApplicationInfoExit;
                            }
                        ApplicationInfoExit:

                            foreach (SortedDictionary<dynamic, dynamic> beneficiaryInfo in InspectionItem.First(s => s.Key == "BeneficiaryReport").Value)
                            {
                                Phone = beneficiaryInfo.ContainsKey("PHONE_NUMBER") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "PHONE_NUMBER").Value) : null;
                                if (Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "EDIT_REQUIRED").Value) == "1")
                                {
                                    EditRequired = "Y";
                                }

                                //design info 
                                objDesign.BENF_FULL_NAME = beneficiaryInfo.ContainsKey("BENEFICIARY_NAME") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BENEFICIARY_NAME").Value) : null;
                                objDesign.DISTRICT_CD = beneficiaryInfo.ContainsKey("DISTRICT") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "DISTRICT").Value) : null;
                                objDesign.VDC_MUN_CD = beneficiaryInfo.ContainsKey("VDC_MUNICIPALITY") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "VDC_MUNICIPALITY").Value) : null;
                                objDesign.WARD = beneficiaryInfo.ContainsKey("WARD") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "WARD").Value) : null;
                                objDesign.TOLE = beneficiaryInfo.ContainsKey("TOLE") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "TOLE").Value) : null;
                                objDesign.KITTA_NUMBER = beneficiaryInfo.ContainsKey("LAND_PLOT_NUMBER") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "LAND_PLOT_NUMBER").Value) : null;

                                //application info 
                                objApplication.DISTRICT_CD = beneficiaryInfo.ContainsKey("DISTRICT") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "DISTRICT").Value) : null;
                                objApplication.VDC_MUN_CD = beneficiaryInfo.ContainsKey("VDC_MUNICIPALITY") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "VDC_MUNICIPALITY").Value) : null;
                                objApplication.WARD_CD = beneficiaryInfo.ContainsKey("WARD") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "WARD").Value) : null;
                                objApplication.APPLICANT_NAME = beneficiaryInfo.ContainsKey("BENEFICIARY_NAME") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BENEFICIARY_NAME").Value) : null;


                                //paper info
                                objPapr.DISTRICT_CD = beneficiaryInfo.ContainsKey("DISTRICT") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "DISTRICT").Value) : null;
                                objPapr.VDC_MUN_CD = beneficiaryInfo.ContainsKey("VDC_MUNICIPALITY") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "VDC_MUNICIPALITY").Value) : null;
                                objPapr.WARD_CD = beneficiaryInfo.ContainsKey("WARD") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "WARD").Value) : null;
                                objPapr.BENEFICIARY_NAME = beneficiaryInfo.ContainsKey("BENEFICIARY_NAME") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BENEFICIARY_NAME").Value) : null;
                                objPapr.TOLE = beneficiaryInfo.ContainsKey("TOLE") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "TOLE").Value) : null;
                                objPapr.LAND_PLOT_NUMBER = beneficiaryInfo.ContainsKey("LAND_PLOT_NUMBER") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "LAND_PLOT_NUMBER").Value) : null;
                                objPapr.BANK_CD = beneficiaryInfo.ContainsKey("BANK_SELECT") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BANK_SELECT").Value) : null;
                                objPapr.BANK_ACC_NUM = beneficiaryInfo.ContainsKey("BANK_ACCOUNT_NUMBER") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BANK_ACCOUNT_NUMBER").Value) : null;
                                objPapr.MOBILE_NUMBER = beneficiaryInfo.ContainsKey("PHONE_NUMBER") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "PHONE_NUMBER").Value) : null;
                                objPapr.EDIT_REQUIRED = EditRequired;
                                objPapr.EDIT_REQUIRED_DETAILS = beneficiaryInfo.ContainsKey("EDIT_REQUIRED_DETAILS") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "EDIT_REQUIRED_DETAILS").Value) : null;
                                objPapr.BANK_SELECT = beneficiaryInfo.ContainsKey("BANK_SELECT") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BANK_SELECT").Value) : null;
                                if (objPapr.BANK_SELECT.ConvertToString() == "-1")
                                {
                                    objPapr.BANK_SELECT = "N"; //BANK INFORMATION NOT AVAILABLE
                                }
                                if (objPapr.BANK_SELECT.ConvertToString() == "0")
                                {
                                    objPapr.BANK_SELECT = "O"; //OTHER/NOT ON THE LIST
                                }
                                else
                                {
                                    objPapr.BANK_SELECT = "Y"; //Bank
                                }
                                objPapr.BANK_BRANCH = beneficiaryInfo.ContainsKey("BANK_BRANCH") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BANK_BRANCH").Value) : null;
                                objPapr.BANK_NOT_AVAILABLE_REMARKS = beneficiaryInfo.ContainsKey("BANK_NOT_AVAILABLE_REMARKS") ? Utils.ConvertToString(beneficiaryInfo.First(s => s.Key == "BANK_NOT_AVAILABLE_REMARKS").Value) : null;
                                goto beneficiaryInfoExit;
                            }
                        beneficiaryInfoExit:

                            foreach (SortedDictionary<dynamic, dynamic> InspectionRecord in InspectionItem.First(s => s.Key == "InspectionRecord").Value)
                            {
                                inspectionType = InspectionRecord.ContainsKey("INSPECTION_TYPE") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "INSPECTION_TYPE").Value) : null;
                                designNumber = InspectionRecord.ContainsKey("DESIGN_NUMBER") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "DESIGN_NUMBER").Value) : null;




                                //designInfo
                                objDesign.CONSTRUCTION_MAT_CD = InspectionRecord.ContainsKey("BUILD_MATERIAL") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "BUILD_MATERIAL").Value) : null;
                                objDesign.ROOF_MAT_CD = InspectionRecord.ContainsKey("ROOF_AND_MATERIALS_USED") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "ROOF_AND_MATERIALS_USED").Value) : null;
                                objDesign.OTHER_CONSTRUCTION = InspectionRecord.ContainsKey("BUILD_MATERIAL_OTHER") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "BUILD_MATERIAL_OTHER").Value) : null;
                                objDesign.OTHER_ROOF = InspectionRecord.ContainsKey("ROOF_AND_MATERIALS_OTHER") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "ROOF_AND_MATERIALS_OTHER").Value) : null;
                                String InspectionType = InspectionRecord.ContainsKey("INSPECTION_TYPE") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "INSPECTION_TYPE").Value) : null;
                                if (objDesign.CONSTRUCTION_MAT_CD != null || objDesign.ROOF_MAT_CD != null)
                                {
                                    if (InspectionType == "7")
                                    {
                                        objDesign.OWN_DESIGN = "Y";
                                    }

                                }


                                // get house design id, hierarchy id from house model
                                if (designNumber != "" && designNumber != null)
                                {
                                    string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + designNumber + "'and CODE_LOC='" + inspectionNumber.ConvertToString() + "'";
                                    dtHierarchy = service.GetDataTable(new
                                    {
                                        query = cmdTextDesign,
                                        args = new { }
                                    });
                                }

                                // design number empty means own design
                                if (designNumber == "" || designNumber == null)
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
                                    string cmdTextDesign = " SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE DESIGN_NUMBER='" + DesignNumberFromTable + "' and CODE_LOC='" + inspectionNumber.ConvertToString() + "'";
                                    dtHierarchy = service.GetDataTable(new
                                    {
                                        query = cmdTextDesign,
                                        args = new { }
                                    });
                                }

                                // set hierarchy code and design_id
                                if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                                {
                                    if (objDesign.OWN_DESIGN.ConvertToString() == "Y")
                                    {
                                        designIdForOwnDesign = "";
                                        designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                        HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                    }
                                    else
                                    {
                                        designIdForOwnDesign = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                        designID = dtHierarchy.Rows[0]["MODEL_ID"].ConvertToString();
                                        HierarchyParentID = dtHierarchy.Rows[0]["HIERARCHY_PARENT_ID"].ConvertToString();
                                    }

                                }

                                objDesign.DESIGN_NUMBER = designID;
                                // paper dtl info
                                objPapr.RC_MATERIAL_CD = InspectionRecord.ContainsKey("ROOF_AND_MATERIALS_USED") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "ROOF_AND_MATERIALS_USED").Value) : null;
                                objPapr.FC_MATERIAL_CD = InspectionRecord.ContainsKey("BUILD_MATERIAL") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "BUILD_MATERIAL").Value) : null;
                                objPapr.INSPECTION_TYPE = InspectionRecord.ContainsKey("INSPECTION_TYPE") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "INSPECTION_TYPE").Value) : null;
                                objPapr.INSPECTION_DATE = InspectionRecord.ContainsKey("DATE_OF_INSPECTION") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "DATE_OF_INSPECTION").Value) : null;
                                objPapr.DESIGN_DETAILS = InspectionRecord.ContainsKey("DESIGN_DETAILS") ? Utils.ConvertToString(InspectionRecord.First(s => s.Key == "DESIGN_DETAILS").Value) : null;


                                goto InspectionRecordExit;
                            }
                        InspectionRecordExit:

                            foreach (SortedDictionary<dynamic, dynamic> PartOne in InspectionItem.First(s => s.Key == "PartOne").Value)
                            {
                                objPapr.ORGANIZATION_OTHERS = PartOne.ContainsKey("ORGANIZATION_OTHERS") ? Utils.ConvertToString(PartOne.First(s => s.Key == "ORGANIZATION_OTHERS").Value) : null;

                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TECHNICAL_SUPPORT").Value) == "1")
                                {
                                    techniClaAssist = "Y";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TECHNICAL_SUPPORT").Value) == "2")
                                {
                                    techniClaAssist = "N";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "ORGANIZATION").Value) == "1")
                                {
                                    Orgtyp = "G";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "ORGANIZATION").Value) == "2")
                                {
                                    Orgtyp = "N";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TRAINED_MASONS").Value) == "1")
                                {
                                    trainedManson = "Y";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TRAINED_MASONS").Value) == "2")
                                {
                                    trainedManson = "N";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TYPE_OF_SOIL").Value) == "1")
                                {
                                    SoilType = "H";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TYPE_OF_SOIL").Value) == "2")
                                {
                                    SoilType = "M";
                                }
                                if (Utils.ConvertToString(PartOne.First(s => s.Key == "TYPE_OF_SOIL").Value) == "3")
                                {
                                    SoilType = "S";
                                }
                                if (Phone.ConvertToString() == "0")
                                {
                                    Phone = "";
                                }

                                goto PartOneExit;
                            }
                        PartOneExit:

                            foreach (SortedDictionary<dynamic, dynamic> InspProcess in InspectionItem.First(s => s.Key == "InspectionProcess").Value)
                            {
                                if (Utils.ConvertToString(InspProcess.First(s => s.Key == "FINAL_DECISION").Value) == "1")
                                {
                                    FinalDecision = "Y";
                                }

                                if (Utils.ConvertToString(InspProcess.First(s => s.Key == "FINAL_DECISION_2").Value) == "1")
                                {
                                    FinalDecision2 = "Y";
                                }
                                if (Utils.ConvertToString(InspProcess.First(s => s.Key == "ENGINEER_POST").Value) == "1")
                                {
                                    EngineerPost = "Engineer";
                                }
                                if (Utils.ConvertToString(InspProcess.First(s => s.Key == "ENGINEER_POST").Value) == "2")
                                {
                                    EngineerPost = "Sub Engineer";
                                }
                                if (Utils.ConvertToString(InspProcess.First(s => s.Key == "ACCEPT_THE_ENTRY").Value) == "1")
                                {
                                    AcceptTheEntry = "Y";
                                }


                                //paper dtl info
                                objPapr.FINAL_DECISION = FinalDecision;
                                objPapr.FINAL_DECISION_2 = FinalDecision2;
                                objPapr.FINAL_REMARKS = InspProcess.ContainsKey("FINAL_REMARKS") ? Utils.ConvertToString(InspProcess.First(s => s.Key == "FINAL_REMARKS").Value) : null;



                                //process info
                                objProcess.BENF_FULL_NAME = InspProcess.ContainsKey("HOUSE_OWNER_S_NAME") ? Utils.ConvertToString(InspProcess.First(s => s.Key == "HOUSE_OWNER_S_NAME").Value) : null;
                                objProcess.RELATN_TO_BENF = InspProcess.ContainsKey("RELATIONSHIP_TO_HOUSE_OWNER") ? Utils.ConvertToString(InspProcess.First(s => s.Key == "RELATIONSHIP_TO_HOUSE_OWNER").Value) : null;
                                objProcess.ENGINEER_FULL_NAME = InspProcess.ContainsKey("MOUD_DLPIU_ENGINEER") ? Utils.ConvertToString(InspProcess.First(s => s.Key == "MOUD_DLPIU_ENGINEER").Value) : null;
                                objProcess.ENGINEER_DESIGNATION = InspProcess.ContainsKey("ENGINEER_POST") ? Utils.ConvertToString(InspProcess.First(s => s.Key == "ENGINEER_POST").Value) : null;

                                goto InspProcessExit;
                            }
                        InspProcessExit:

                            foreach (SortedDictionary<dynamic, dynamic> GpsData in InspectionItem.First(s => s.Key == "GPSData").Value)
                            {

                                if (Utils.ConvertToString(GpsData.First(s => s.Key == "GPS_TAKEN").Value) == "1")
                                {
                                    GpsTaken = "Y";
                                }

                                //paper dtl info 
                                objPapr.LONGITUDE = GpsData.ContainsKey("LONGITUDE") ? Utils.ConvertToString(GpsData.First(s => s.Key == "LONGITUDE").Value) : null;
                                objPapr.LATITUDE = GpsData.ContainsKey("LATITUDE") ? Utils.ConvertToString(GpsData.First(s => s.Key == "LATITUDE").Value) : null;
                                objPapr.ALTITUDE = GpsData.ContainsKey("ALTITUDE") ? Utils.ConvertToString(GpsData.First(s => s.Key == "ALTITUDE").Value) : null;

                                goto GpsDataExit;
                            }
                        GpsDataExit:

                            foreach (SortedDictionary<dynamic, dynamic> Photos in InspectionItem.First(s => s.Key == "Photos").Value)
                            {
                                objPapr.PHOTO_CD_1 = Photos.ContainsKey("PHOTO_1") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_1").Value) : null;
                                objPapr.PHOTO_CD_2 = Photos.ContainsKey("PHOTO_2") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_2").Value) : null;
                                objPapr.PHOTO_CD_3 = Photos.ContainsKey("PHOTO_3") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_3").Value) : null;
                                objPapr.PHOTO_CD_4 = Photos.ContainsKey("PHOTO_4") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_4").Value) : null;
                                objPapr.PHOTO_CD_5 = Photos.ContainsKey("PHOTO_5") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_5").Value) : null;
                                objPapr.PHOTO_CD_6 = Photos.ContainsKey("PHOTO_OF_HOUSE_PLAN") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_OF_HOUSE_PLAN").Value) : null;

                                objPapr.PHOTO_1 = Photos.ContainsKey("PHOTO_1_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_1_NAME").Value) : null;
                                objPapr.PHOTO_2 = Photos.ContainsKey("PHOTO_2_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_2_NAME").Value) : null;
                                objPapr.PHOTO_3 = Photos.ContainsKey("PHOTO_3_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_3_NAME").Value) : null;
                                objPapr.PHOTO_4 = Photos.ContainsKey("PHOTO_4_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_4_NAME").Value) : null;
                                objPapr.PHOTO_5 = Photos.ContainsKey("PHOTO_5_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "PHOTO_5_NAME").Value) : null;
                                objPapr.PASSED_NAKSA_NO = Photos.ContainsKey("HOUSE_PLAN_NAME") ? Utils.ConvertToString(Photos.First(s => s.Key == "HOUSE_PLAN_NAME").Value) : null;

                                goto PhotosExit;
                            }
                        PhotosExit:

                            //notes information (remarks)
                            if ((inspectionType.ToDecimal() > 0 && inspectionType.ToDecimal() < 7))
                            {
                                foreach (SortedDictionary<dynamic, dynamic> NoteRemarks in InspectionItem.First(s => s.Key == "Notes").Value)
                                {
                                    foreach (var item in NoteRemarks)
                                    {
                                        string RealKey = "";
                                        string ObtainedKey = item.Key;
                                        string[] Keyaray = ObtainedKey.Split('_');
                                        if (Keyaray.Length > 2)
                                        {
                                            RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                        }
                                        else
                                        {
                                            RealKey = ObtainedKey;
                                        }
                                        string value = item.Value;
                                        InspectionRemarks.Add(new KeyValuePair<string, string>(RealKey, value));
                                    }
                                    goto NoteRemarksExit;

                                }

                            }
                        NoteRemarksExit:


                            //Inspection Detail
                            if ((inspectionType.ToDecimal() > 0 && inspectionType.ToDecimal() < 7) && inspectionNumber == "1")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> CommonDetailPart in InspectionItem.First(s => s.Key == "InspectionCommon").Value)
                                {
                                    foreach (var CommonDetailPart1 in CommonDetailPart)
                                    {
                                        string Key = CommonDetailPart1.Key;
                                        string value = CommonDetailPart1.Value;
                                        CommonInspectionDetail.Add(new KeyValuePair<string, string>(Key, value));
                                    }
                                    goto CommonDetailPartExit;

                                }

                            }
                        CommonDetailPartExit:

                            // A anb B type RCC inspection one
                            if (inspectionType == "1" && inspectionNumber == "1")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> ABRCCInspOne in InspectionItem.First(s => s.Key == "AandBTypeRccHouseI1").Value)
                                {
                                    foreach (var item in ABRCCInspOne)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto ABRCCInspOneExit;
                                        }
                                    }
                                    goto ABRCCInspOneExit;
                                }
                            }
                        ABRCCInspOneExit:


                            // A anb B type RCC inspection Second
                            if (inspectionType == "1" && inspectionNumber == "2")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> ABRCCInspTwo in InspectionItem.First(s => s.Key == "AandBTypeRccHouseI2").Value)
                                {
                                    foreach (var item in ABRCCInspTwo)
                                    {
                                        if (item.Value != null)
                                        {

                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto ABRCCInspTwoExit;
                                        }

                                    }
                                    goto ABRCCInspTwoExit;
                                }
                            }
                        ABRCCInspTwoExit:



                            // A anb B type RCC inspection Third
                            if (inspectionType == "1" && inspectionNumber == "3")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> ABRCCInspThree in InspectionItem.First(s => s.Key == "AandBTypeRccHouseI3").Value)
                                {
                                    foreach (var item in ABRCCInspThree)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto ABRCCInspThreeExit;
                                        }
                                    }
                                    goto ABRCCInspThreeExit;
                                }
                            }
                        ABRCCInspThreeExit:



                            // Own Design 
                            if (inspectionType == "7")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> OwnDesign in InspectionItem.First(s => s.Key == "OwnDesign").Value)
                                {
                                    if (OwnDesign.Values != null)
                                    {

                                        string ConstructionStatusCd = OwnDesign.ContainsKey("FOUR_NO1") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO1").Value) : null;

                                        if (ConstructionStatusCd.ConvertToString() == "1")
                                        {
                                            objOwnDesign.CONSTRUCTION_STATUS = "Foundation Under Construction";
                                        }
                                        if (ConstructionStatusCd.ConvertToString() == "2")
                                        {
                                            objOwnDesign.CONSTRUCTION_STATUS = "Foundation Construction Completed";
                                        }
                                        if (ConstructionStatusCd.ConvertToString() == "3")
                                        {
                                            objOwnDesign.CONSTRUCTION_STATUS = "Upto Roof Band in Ground Floor";
                                        }
                                        if (ConstructionStatusCd.ConvertToString() == "4")
                                        {
                                            objOwnDesign.CONSTRUCTION_STATUS = "Ground Floor Construction Completed (Including Roof)";
                                        }


                                        objOwnDesign.STOREY_COUNT = OwnDesign.ContainsKey("FOUR_NO2") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO2").Value) : null;
                                        objOwnDesign.BASE_MATERIAL = OwnDesign.ContainsKey("FOUR_NO3_A") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO3_A").Value) : null;
                                        objOwnDesign.BASE_DEPTH = OwnDesign.ContainsKey("FOUR_NO3_B") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO3_B").Value) : null;
                                        objOwnDesign.BASE_WIDTH = OwnDesign.ContainsKey("FOUR_NO3_C") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO3_C").Value) : null;
                                        objOwnDesign.BASE_HEIGHT = OwnDesign.ContainsKey("FOUR_NO3_D") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO3_D").Value) : null;
                                        objOwnDesign.GROUND_FLOOR_MAT = OwnDesign.ContainsKey("FOUR_NO4_A") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO4_A").Value) : null;
                                        objOwnDesign.GROUND_FLOOR_PRINCIPAL = OwnDesign.ContainsKey("FOUR_NO4_B") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO4_B").Value) : null;
                                        objOwnDesign.WALL_DETAIL = OwnDesign.ContainsKey("FOUR_NO4_C") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO4_C").Value) : null;
                                        objOwnDesign.FLOOR_ROOF_MAT = OwnDesign.ContainsKey("FOUR_NO5_A") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO5_A").Value) : null;
                                        objOwnDesign.FLOOR_ROOF_PRINCIPAL = OwnDesign.ContainsKey("FOUR_NO5_B") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO5_B").Value) : null;
                                        objOwnDesign.FLOOR_ROOF_DTL = OwnDesign.ContainsKey("FOUR_NO5_C") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO5_C").Value) : null;
                                        objOwnDesign.FIRST_FLOOR_MAT = OwnDesign.ContainsKey("FOUR_NO6_A") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO6_A").Value) : null;
                                        objOwnDesign.FIRST_FLOOR_PRINCIPAL = OwnDesign.ContainsKey("FOUR_NO6_B") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO6_B").Value) : null;
                                        objOwnDesign.FIRST_FLOOR_DTL = OwnDesign.ContainsKey("FOUR_NO6_C") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO6_C").Value) : null;
                                        objOwnDesign.ROOF_MAT = OwnDesign.ContainsKey("FOUR_NO7_A") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO7_A").Value) : null;
                                        objOwnDesign.ROOF_PRINCIPAL = OwnDesign.ContainsKey("FOUR_NO7_B") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO7_B").Value) : null;
                                        objOwnDesign.ROOF_DTL = OwnDesign.ContainsKey("FOUR_NO7_C") ? Utils.ConvertToString(OwnDesign.First(s => s.Key == "FOUR_NO7_C").Value) : null;

                                        goto OwnDesignExit;
                                    }
                                }
                            }


                        OwnDesignExit:





                            // smm,bmm,smc,bmc type second inspection
                            if ((inspectionType.ToDecimal() > 1 && inspectionType.ToDecimal() < 6) && inspectionNumber == "2")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> TwoTypeSecondInspection in InspectionItem.First(s => s.Key == "CTypeHouseI2").Value)
                                {
                                    foreach (var item in TwoTypeSecondInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto TwoTypeSecondInspectionExit;
                                        }
                                    }
                                    goto TwoTypeSecondInspectionExit;
                                }
                            }
                        TwoTypeSecondInspectionExit:


                            // Rcc second inspection
                            if (inspectionType == "6" && inspectionNumber == "2")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> RccSecondInspection in InspectionItem.First(s => s.Key == "CTypeRCCHouseI2").Value)
                                {
                                    foreach (var item in RccSecondInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto RccSecondInspectionExit;
                                        }

                                    }
                                    goto RccSecondInspectionExit;
                                }
                            }
                        RccSecondInspectionExit:




                            // smm,bmm,smc,bmc type Third inspection
                            if ((inspectionType.ToDecimal() > 1 && inspectionType.ToDecimal() < 6) && inspectionNumber == "3")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> TwoTypeThirdInspection in InspectionItem.First(s => s.Key == "CTypeHouseI3").Value)
                                {
                                    foreach (var item in TwoTypeThirdInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto TwoTypeThirdInspectionExit;
                                        }
                                    }
                                    goto TwoTypeThirdInspectionExit;
                                }
                            }
                        TwoTypeThirdInspectionExit:




                            // Rcc Third inspection
                            if (inspectionType == "6" && inspectionNumber == "3")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> RccThirdInspection in InspectionItem.First(s => s.Key == "CTypeRCCHouseI3").Value)
                                {
                                    foreach (var item in RccThirdInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto RccThirdInspectionExit;
                                        }
                                    }
                                    goto RccThirdInspectionExit;
                                }
                            }



                        RccThirdInspectionExit:



                            // Rcc First inspection
                            if (inspectionType == "6" && inspectionNumber == "1")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> RccFirstInspection in InspectionItem.First(s => s.Key == "CTypeRCCHouseI1").Value)
                                {
                                    foreach (var item in RccFirstInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto RccFirstInspectionExit;
                                        }
                                    }
                                    goto RccFirstInspectionExit;
                                }
                            }
                        RccFirstInspectionExit:


                            // smm,bmm,smc,bmc type First inspection
                            if ((inspectionType.ToDecimal() > 1 && inspectionType.ToDecimal() < 6) && inspectionNumber == "1")
                            {
                                foreach (SortedDictionary<dynamic, dynamic> TwoTypeFirstInspection in InspectionItem.First(s => s.Key == "CTypeHouseI1").Value)
                                {
                                    foreach (var item in TwoTypeFirstInspection)
                                    {
                                        if (item.Value != null)
                                        {
                                            string ObtainedKey = item.Key;
                                            string[] Keyaray = ObtainedKey.Split('_');
                                            string RealKey = Keyaray[1] + "_" + Keyaray[2] + "_" + Keyaray[3];
                                            string value = item.Value;
                                            InspectionDetail.Add(new KeyValuePair<string, string>(RealKey, value));
                                        }
                                        else
                                        {
                                            goto TwoTypeFirstInspectionExit;
                                        }
                                    }
                                    goto TwoTypeFirstInspectionExit;
                                }
                            }
                        TwoTypeFirstInspectionExit:










                            objMst.COMPLY_FLAG = InspectionItem.ContainsKey("FINAL_DECISION") ? Utils.ConvertToString(InspectionItem.First(s => s.Key == "FINAL_DECISION").Value) : null;


                        }

                        DataTable dtHistory = new DataTable();
                        string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                        dtHistory = service.GetDataTable(new
                        {
                            query = cmdHistoryid,
                            args = new { }
                        });

                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            InspectionHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                        }
                        string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + InspectionHistoryId.ConvertToString() + "')";
                        QueryResult InsertHistoryId = service.SubmitChanges(insertCmd, null);


                        //save inspection mst information
                        QueryResult qrMst = service.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                                 "U",

                                                  objMst.INSPECTION_MST_ID.ToDecimal(),                              //mst id
                                                  objMst.DEFINED_CD.ConvertToString(),                               // defined id
                                                  "0".ToDecimal(),                                                  // level zero
                                                  "".ToDateTime(),                                //inspectionDate
                                                  "R".ConvertToString(),                  //inspection status

                                                  SessionCheck.getSessionUsername(),      //entered by
                                                   DateTime.Now,                          //entered date
                                                   System.DateTime.Now.ConvertToString(), //entered date loc

                                                   "",                                     //approved
                                                   SessionCheck.getSessionUsername(),      //approved by
                                                   DateTime.Now,                            //approved date  
                                                   System.DateTime.Now.ConvertToString(),   //approved date loc

                                                   SessionCheck.getSessionUsername(),       //updated by
                                                   DateTime.Now,                            //updated date 
                                                   System.DateTime.Now.ConvertToString(),   //updated date loc
                                                   objMst.NRA_DEFINED_CD.ConvertToString(), //pa nuber
                                                   objMst.INSPECTION_LEVEL1.ToDecimal(),
                                                   objMst.INSPECTION_LEVEL2.ToDecimal(),
                                                   objMst.INSPECTION_LEVEL3.ToDecimal(),
                                                   objMst.COMPLY_FLAG.ConvertToString(),
                                                 "N".ConvertToString(),  /// final approve flag
                                                 "N".ConvertToString(), // final decision 
                                                 InspectionHistoryId.ToDecimal()

                                             );
                        //   InspectionMstId = qrMst["v_INSPECTION_MST_ID"].ConvertToString();

                        //save design part
                        QueryResult qrDesign = service.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                                                  "U",

                                                   DBNull.Value,   // design id 
                                                   DBNull.Value,                // defined cd
                                                   objDesign.NRA_DEFINED_CD.ConvertToString(),     //nra defined cd
                                                   objDesign.BENF_FULL_NAME.ConvertToString(),       // beneficiary full name 
                                                   objDesign.DISTRICT_CD.ToDecimal(),                   // district
                                                   objDesign.VDC_MUN_CD.ToDecimal(),                     // vdc mun cd
                                                   objDesign.WARD.ToDecimal(),                             // ward TOLE
                                                   objDesign.TOLE.ConvertToString(),                    // tole
                                                   objDesign.OWN_DESIGN.ConvertToString(), //objDesign.OWN_DESIGN.ConvertToString(),            // own design/template flag

                                                   objDesign.DESIGN_NUMBER.ConvertToString(), //objDesign.DESIGN_NUMBER.ConvertToString(),          // design number
                                                   objDesign.CONSTRUCTION_MAT_CD.ToDecimal(),    // construction material
                                                   objDesign.ROOF_MAT_CD.ToDecimal(),            // roof material  

                                                   objDesign.OTHER_CONSTRUCTION.ConvertToString(), // other construction material
                                                   objDesign.OTHER_ROOF.ConvertToString(),           // other roof material

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    "",
                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                    SessionCheck.getSessionUsername(),
                                                    DateTime.Now,
                                                    System.DateTime.Now.ConvertToString(),

                                                   InspectionMstId.ToDecimal(),
                                                   "R".ConvertToString(),                                          // design status
                                                   objDesign.KITTA_NUMBER.ConvertToString(),        // kitta number
                                                   InspectionHistoryId.ToDecimal()
                                                    );





                        //save inspection Application 
                        QueryResult qrRegister = service.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                               "U".ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               objApplication.DISTRICT_CD.ToDecimal(),                      // district
                                               objApplication.VDC_MUN_CD.ToDecimal(),              // vdc mun cd
                                               objApplication.WARD_CD.ToDecimal(),                          // ward
                                               objApplication.NRA_DEFINED_CD.ConvertToString(), //nra defined cd

                                               objApplication.APPLICANT_NAME.ConvertToString(),     //  BENEFICIARY_NAME 
                                               "0".ToDecimal(),
                                               "".ToDecimal(),
                                               "".ToDecimal(), //remarks


                                               "",
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),


                                               "".ToDecimal(), //reg no
                                                DateTime.Now.ConvertToString(), //reg date

                                               "".ToDecimal(), //schedule
                                               "",
                                              objMst.INSPECTION_LEVEL1.ToDecimal(),
                                              objMst.INSPECTION_LEVEL2.ToDecimal(),
                                              objMst.INSPECTION_LEVEL3.ToDecimal()



                       );

                        DataTable dtMstID = new DataTable();
                        string cmdMstId = " SELECT INSPECTION_MST_ID  FROM NHRS_INSPECTION_MST where NRA_DEFINED_CD='" + objPapr.NRA_DEFINED_CD.ConvertToString() + "'";
                        dtMstID = service.GetDataTable(new
                        {
                            query = cmdMstId,
                            args = new { }
                        });

                        if (dtMstID != null && dtMstID.Rows.Count > 0)
                        {
                            InspectionMstId = dtMstID.Rows[0]["INSPECTION_MST_ID"].ConvertToString();
                        }

                        // save inspection paper detail
                        objPapr.DEFINED_CD = "";
                        objPapr.INSPECTION_CODE_ID = "";

                        QueryResult qrPaperDtl = service.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",


                             "I",
                                                   DBNull.Value,
                                                   objPapr.DEFINED_CD.ToDecimal(),
                                                   objPapr.INSPECTION_CODE_ID.ToDecimal(),
                                                   objPapr.NRA_DEFINED_CD.ConvertToString(), //nra defined cd
                                                   objPapr.DISTRICT_CD.ToDecimal(),                      // district
                                                   objPapr.VDC_MUN_CD.ToDecimal(),              // vdc mun cd
                                                   objPapr.WARD_CD.ToDecimal(),                          // ward
                                                   "".ConvertToString(),                                            // map design
                                                   designID.ConvertToString(),                                      // design number
                                                   objPapr.RC_MATERIAL_CD.ToDecimal(),      // roof material  
                                                   objPapr.FC_MATERIAL_CD.ToDecimal(),               // construction material
                                                   techniClaAssist,                                                // technical assist
                                                   Orgtyp,                                                         // org type
                                                   trainedManson,                                                  //constructor type
                                                   SoilType,                                                       // soil type
                                                   "".ToDecimal(),                                                  // house model
                                                   objPapr.PHOTO_CD_1.ConvertToString(),                 // photo cd 1
                                                   objPapr.PHOTO_CD_2.ConvertToString(),                 //  photo cd 2
                                                   objPapr.PHOTO_CD_3.ConvertToString(),                 //  photo cd 3
                                                   objPapr.PHOTO_CD_4.ConvertToString(),                 //  photo cd 4
                                                   objPapr.PHOTO_1.ConvertToString().ConvertToString(),        // photo  1
                                                   objPapr.PHOTO_2.ConvertToString().ConvertToString(),        //  photo 2
                                                   objPapr.PHOTO_3.ConvertToString().ConvertToString(),        //  photo  3
                                                   objPapr.PHOTO_4.ConvertToString().ConvertToString(),        //photo  4
                                                   "G".ConvertToString(),       //status
                                                   "Y".ConvertToString(),       //active
                                                   SessionCheck.getSessionUsername(), //entered by
                                                   DateTime.Now,                       //entered date
                                                   System.DateTime.Now.ConvertToString(), //entered date loc
                                                   "",                                     // approved
                                                   SessionCheck.getSessionUsername(),      //approved by
                                                   DateTime.Now,                           //approved date 
                                                   System.DateTime.Now.ConvertToString(),  //approved date loc
                                                   SessionCheck.getSessionUsername(),      // updated by
                                                   DateTime.Now,                           //update date
                                                   System.DateTime.Now.ConvertToString(),  //updated date loc
                                                   "".ConvertToString(),                    // installment
                                                   "".ToDecimal(),                          // map design code 
                                                   objPapr.PASSED_NAKSA_NO.ConvertToString(),   // passed map no,,gharko mota moti naksa
                                                   "".ConvertToString(),                    // other info
                                                   batchID.ToDecimal(),                     // file batch id
                                                   objPapr.INSPECTION_TYPE.ConvertToString(),      //  Inspection Type/house model
                                                   objPapr.INSPECTION_DATE.ConvertToString(),      //  INSPECTION_DATE 
                                                   objPapr.BENEFICIARY_NAME.ConvertToString(),     //  BENEFICIARY_NAME 
                                                   objPapr.TOLE.ConvertToString(),                 //  TOLE   
                                                   objPapr.LAND_PLOT_NUMBER.ConvertToString(),     //  LAND_PLOT_NUMBER
                                                   objPapr.ORGANIZATION_OTHERS.ConvertToString(),  //ORGANIZATION_OTHERS
                                                   objPapr.PHOTO_CD_5.ConvertToString(),              //PHOTO_CD_5 
                                                   objPapr.PHOTO_5.ConvertToString(),         //PHOTO_5 
                                                   objPapr.PHOTO_CD_6.ConvertToString(),                                         //PHOTO_CD_6 
                                                   "".ConvertToString(),                                         // photo 6 
                                                   objPapr.FINAL_DECISION.ConvertToString(),                              //Final Decision
                                                   objPapr.LATITUDE.ConvertToString(),             //LATITUDE
                                                   objPapr.LONGITUDE.ConvertToString(),            //LONGITUDE
                                                   objPapr.ALTITUDE.ConvertToString(),             //ALTITUDE 
                                                   InspectionMstId.ToDecimal(),                                  // mst id
                                                   objPapr.BANK_CD.ToDecimal(),                //BANK_NAME_cd
                                                   objPapr.BANK_ACC_NUM.ConvertToString(),  //BANK_ACCOUNT_NUMBER
                                                   "".ConvertToString(),          //serial number/form number
                                                   "".ConvertToString(),                                         //House owner id
                                                   objPapr.MOBILE_NUMBER.ConvertToString(),                                     //PHONE_NUMBER
                                                   objPapr.FORM_PAD_NUMBER.ConvertToString(),     //FORM_PAD_NUMBER
                                                  objPapr.DESIGN_DETAILS.ConvertToString(),       //DESIGN_DETAILS
                                                  objPapr.EDIT_REQUIRED.ConvertToString(),                               //EDIT_REQUIRED
                                                  objPapr.EDIT_REQUIRED_DETAILS.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objPapr.BANK_SELECT.ConvertToString(),          // BANK_SELECT
                                                  objPapr.BANK_BRANCH.ConvertToString(),          //BANK_BRANCH
                                                  objPapr.FINAL_REMARKS.ConvertToString(),        // FINAL_REMARKS
                                                  AcceptTheEntry.ConvertToString(),                             //ACCEPT_THE_ENTRY
                                                  GpsTaken.ConvertToString(),                                   //GPS_TAKEN
                                                  FinalDecision2.ConvertToString(),                       //FINAL_DECISION_2
                                                  objPapr.BANK_NOT_AVAILABLE_REMARKS.ConvertToString(),         // bank no remarks
                                                  "N".ConvertToString(), // final decision approve
                                                  "N".ConvertToString(), // final decision 2 approve
                                                  "".ToDecimal(),  //approve 1 batch
                                                  "".ToDecimal(), //approve 2 batch
                                                 objPapr.INSPECTION_LEVEL.ConvertToString(),
                                                  InspectionHistoryId.ToDecimal()




                                                   //"I",
                            // DBNull.Value,
                            // objPapr.DEFINED_CD.ToDecimal(),
                            // "".ToDecimal(),
                            // "".ConvertToString(), //nra defined cd
                            // objPapr.DISTRICT_CD.ToDecimal(),                      // district
                            //  objPapr.VDC_MUN_CD.ToDecimal(),              // vdc mun cd
                            // "".ToDecimal(),                          // ward
                            // "".ConvertToString(),                                            // map design
                            // "".ConvertToString(),                                      // design number
                            // "".ToDecimal(),      // roof material  
                            // "".ToDecimal(),               // construction material
                            // "",                                                // technical assist
                            // "",                                                         // org type
                            // "",                                                  //constructor type
                            // "",                                                       // soil type
                            // "".ToDecimal(),                                                  // house model
                            // "".ConvertToString(),                 // photo cd 1
                            // "".ConvertToString(),                 //  photo cd 2
                            // "".ConvertToString(),                 //  photo cd 3
                            // "".ConvertToString(),                 //  photo cd 4
                            // "".ConvertToString().ConvertToString(),        // photo  1
                            // "".ConvertToString().ConvertToString(),        //  photo 2
                            // "".ConvertToString().ConvertToString(),        //  photo  3
                            // "".ConvertToString().ConvertToString(),        //photo  4
                            // "G".ConvertToString(),       //status
                            // "Y".ConvertToString(),       //active
                            // SessionCheck.getSessionUsername(), //entered by
                            // DateTime.Now,                       //entered date
                            // System.DateTime.Now.ConvertToString(), //entered date loc
                            // "",                                     // approved
                            // SessionCheck.getSessionUsername(),      //approved by
                            // DateTime.Now,                           //approved date 
                            // System.DateTime.Now.ConvertToString(),  //approved date loc
                            // SessionCheck.getSessionUsername(),      // updated by
                            // DateTime.Now,                           //update date
                            // System.DateTime.Now.ConvertToString(),  //updated date loc
                            // "".ConvertToString(),                    // installment
                            // "".ToDecimal(),                          // map design code 
                            // "".ConvertToString(),   // passed map no,,gharko mota moti naksa
                            // "".ConvertToString(),                    // other info
                            // "".ToDecimal(),                     // file batch id
                            // "".ConvertToString(),      //  Inspection Type/house model
                            // "".ConvertToString(),      //  INSPECTION_DATE 
                            // "".ConvertToString(),     //  BENEFICIARY_NAME 
                            // "".ConvertToString(),                 //  TOLE   
                            // "".ConvertToString(),     //  LAND_PLOT_NUMBER
                            // "".ConvertToString(),  //ORGANIZATION_OTHERS
                            // "".ConvertToString(),              //PHOTO_CD_5 
                            // "".ConvertToString(),         //PHOTO_5 
                            //  objPapr.PHOTO_CD_6.ConvertToString(),                                         //PHOTO_CD_6 
                            // "".ConvertToString(),                                         // photo 6 
                            // "".ConvertToString(),                              //Final Decision
                            // "".ConvertToString(),             //LATITUDE
                            // "".ConvertToString(),            //LONGITUDE
                            // "".ConvertToString(),             //ALTITUDE 
                            // "".ToDecimal(),                                  // mst id
                            //  objPapr.BANK_CD.ToDecimal(),                //BANK_NAME_cd
                            // "".ConvertToString(),  //BANK_ACCOUNT_NUMBER
                            // "".ConvertToString(),          //serial number
                            // "".ConvertToString(),                                         //House owner id
                            // "".ConvertToString(),                                     //PHONE_NUMBER
                            //"".ConvertToString(),     //FORM_PAD_NUMBER
                            //"".ConvertToString(),       //DESIGN_DETAILS
                            //   "".ConvertToString(),                            //EDIT_REQUIRED
                            //  "".ConvertToString(),   //EDIT_REQUIRED_DETAILS
                            // objPapr.BANK_SELECT.ConvertToString(),     // BANK_SELECT
                            //"".ConvertToString(),          //BANK_BRANCH
                            //"".ConvertToString(),        // FINAL_REMARKS
                            //"".ConvertToString(),                             //ACCEPT_THE_ENTRY
                            //"".ConvertToString(),                                   //GPS_TAKEN
                            //"".ConvertToString(),                       //FINAL_DECISION_2
                            //"".ConvertToString(),         // bank no remarks
                            //"N".ConvertToString(), // final decision approve
                            //"N".ConvertToString(), // final decision 2 approve
                            //"".ToDecimal(),  //approve 1 batch
                            //"".ToDecimal(), //approve 2 batch
                            //"".ConvertToString(),
                            //InspectionHistoryId

                                               );
                        InspPprId = qrPaperDtl["v_INSPECTION_PAPER_ID"].ConvertToString();



                        string cmdText = "";
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






                        if (inspectionType.ToDecimal() > 0 && inspectionType.ToDecimal() < 7)
                        {
                            if (dtHierarchy != null && dtHierarchy.Rows.Count > 0)
                            {
                                string Inspection_code = "";
                                string value = "";
                                string Remarks = "";



                                foreach (DataRow dr in dtHierarchy.Rows)
                                {
                                    foreach (var element in CommonInspectionDetail)
                                    {
                                        if (element.Key.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                        {
                                            Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                            value = element.Value.ConvertToString();//FINAL_DECISION 
                                            foreach (var element1 in InspectionRemarks)
                                            {
                                                if (element1.Key.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Remarks = element1.Value.ConvertToString();
                                                }
                                            }

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
                                            "".ToDecimal(),
                                            Inspection_code.ToDecimal(),
                                            value.ConvertToString(),
                                            Remarks.ConvertToString(),
                                            "".ToDecimal(),             //  Inspection Type/house model
                                            "".ConvertToString(),
                                            "".ToDecimal()
                                            );
                                        }
                                    }


                                    foreach (var element in InspectionDetail)
                                    {
                                        if (element.Key.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                        {
                                            Inspection_code = dr["INSPECTION_CODE_ID"].ConvertToString();
                                            value = element.Value.ConvertToString();//FINAL_DECISION 
                                            foreach (var element1 in InspectionRemarks)
                                            {
                                                if (element1.Key.ConvertToString() == dr["DEFINED_CD"].ConvertToString())
                                                {
                                                    Remarks = element1.Value.ConvertToString();
                                                }
                                            }
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
                                            "".ToDecimal(),
                                            Inspection_code.ToDecimal(),
                                            value.ConvertToString(),
                                            Remarks.ConvertToString(),
                                            "".ToDecimal(),             //  Inspection Type/house model
                                            "".ConvertToString(),
                                            "".ToDecimal()
                                            );
                                        }
                                    }










                                }
                            }

                        }
                        if (inspectionType == "7")//for own design
                        {
                            QueryResult qrOwnDesign = service.SubmitChanges("PR_NHRS_INSPECTION_OWN_DESIGN",
                                           "I".ConvertToString(),

                                             DBNull.Value,
                                             DBNull.Value,

                                             InspPprId.ToDecimal(),                            // inspection paper id 
                                             designID.ConvertToString(),                       // design number FOUR_NO1

                                             "".ConvertToString(),       //base constructing 
                                             "".ConvertToString(),            //base constructed
                                             "".ConvertToString(),                    // ground roof finished
                                             "".ConvertToString(),   // ground floor completed 

                                           objOwnDesign.STOREY_COUNT.ConvertToString(),
                                           objOwnDesign.BASE_MATERIAL.ConvertToString(),
                                           objOwnDesign.BASE_DEPTH.ConvertToString(),
                                           objOwnDesign.BASE_WIDTH.ConvertToString(),
                                           objOwnDesign.BASE_HEIGHT.ConvertToString(),
                                           objOwnDesign.GROUND_FLOOR_MAT.ConvertToString(),
                                           objOwnDesign.GROUND_FLOOR_PRINCIPAL.ConvertToString(),
                                           objOwnDesign.WALL_DETAIL.ConvertToString(),
                                           "".ConvertToString(),
                                           objOwnDesign.FLOOR_ROOF_MAT.ConvertToString(),
                                           objOwnDesign.FLOOR_ROOF_PRINCIPAL.ConvertToString(),
                                           objOwnDesign.FLOOR_ROOF_DTL.ConvertToString(),
                                           objOwnDesign.FIRST_FLOOR_MAT.ConvertToString(),
                                           objOwnDesign.FIRST_FLOOR_PRINCIPAL.ConvertToString(),
                                           objOwnDesign.FIRST_FLOOR_DTL.ConvertToString(),
                                           objOwnDesign.ROOF_MAT.ConvertToString(),
                                           objOwnDesign.ROOF_PRINCIPAL.ConvertToString(),
                                           objOwnDesign.ROOF_DTL.ConvertToString(),
                                             "",
                                             "",
                                             SessionCheck.getSessionUsername(),
                                             DateTime.Now,
                                             System.DateTime.Now.ConvertToString(),

                                             "",
                                             SessionCheck.getSessionUsername(),
                                             DateTime.Now,
                                             System.DateTime.Now.ConvertToString(),

                                             SessionCheck.getSessionUsername(),
                                             DateTime.Now,
                                             System.DateTime.Now.ConvertToString(),
                                            objOwnDesign.CONSTRUCTION_STATUS.ConvertToString(),
                                              InspectionHistoryId.ToDecimal()
                                           );

                        }

                        // inspection process detail import
                        QueryResult qreProcess = service.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                     "I",

                                      DBNull.Value,
                                      DBNull.Value,
                                     objProcess.BENF_FULL_NAME.ConvertToString(),          //BenfullnameEng
                                      objProcess.RELATN_TO_BENF.ConvertToString(), //relationWithbenf


                                      objProcess.ENGINEER_FULL_NAME.ConvertToString(),  //engineer name
                                      objProcess.ENGINEER_DESIGNATION.ConvertToString(),                               //engineer designation 

                                      "".ConvertToString(),            //engineer name
                                      "".ConvertToString(),           //engineer designation 



                                      "".ToDateTime(),// register date
                                      "".ConvertToString(),//proceed date 
                                      "".ConvertToString(),//approved date 
                                      "".ConvertToString(),

                                      SessionCheck.getSessionUsername(),
                                      DateTime.Now,
                                      System.DateTime.Now.ConvertToString(),

                                      "",
                                      SessionCheck.getSessionUsername(),
                                      DateTime.Now,
                                      System.DateTime.Now.ConvertToString(),

                                      SessionCheck.getSessionUsername(),
                                      DateTime.Now,
                                      System.DateTime.Now.ConvertToString(),
                                      InspPprId.ToDecimal(),
                                      InspectionHistoryId.ToDecimal()


                                 );













                        //}
                    }

                    catch (OracleException oe)
                    {
                        if (batchID != "")
                        {
                            result = true;
                            ExceptionManager.AppendLog(oe);

                            string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + InspectionMstId + "'AND INSPECTION_LEVEL='" + inspectionLevel + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from nhrs_inspection_mst where nra_defined_cd='" + objMst.NRA_DEFINED_CD.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            //cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                            //SaveErrorMessgae(cmdText);


                        }
                    }
                    catch (Exception ex)
                    {
                        if (batchID != "")
                        {
                            result = true;
                            ExceptionManager.AppendLog(ex);

                            string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION where INSPECTION_PAPER_ID='" + InspPprId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + InspectionMstId + "'AND INSPECTION_LEVEL='" + inspectionLevel + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from nhrs_inspection_design where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from nhrs_inspection_mst where nra_defined_cd='" + objMst.NRA_DEFINED_CD.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            //cmdText = String.Format("update NHRS_INSPECTION_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                            //SaveErrorMessgae(cmdText);

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
                }
            } return result;
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
