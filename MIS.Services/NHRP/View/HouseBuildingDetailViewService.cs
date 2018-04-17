using EntityFramework;
using ExceptionHandler;
using MIS.Models.NHRP;
using MIS.Models.NHRP.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;

namespace MIS.Services.NHRP.View
{
    public class HouseBuildingDetailViewService
    {
        public DataTable HouseBuildingDetail(string y)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_HOUSE_BUILDING_DTL WHERE HOUSE_OWNER_ID = '" + y + "' order by building_structure_no "; //+ " AND DEFINED_CD = " + definedCd  169  ;
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public DataTable GetGrievantDetail(string HouseOwnerID)
        {
            NhrsHouseDetail obj = new NhrsHouseDetail();
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();


                    try
                    {
                        dtbl = service.GetDataTable(true, "GET_GRIEVANT_DETAIL", HouseOwnerID, DBNull.Value);
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
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }

        public DataTable GetHouseMemberDetail(string HouseOwnerID)
        {
            NhrsHouseDetail obj = new NhrsHouseDetail();
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();


                    try
                    {
                        dtbl = service.GetDataTable(true, "GET_MEMBER_DETAIL", HouseOwnerID, DBNull.Value);
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
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }

        public DataSet HouseDetail(string y)
        {
            DataSet dt = null;
            //string cmdText = "";
            string P_DISTRICT_CD = null;
            string P_BUILDING_STRUCTURE_NO = null;
            string P_VDC_MUN_CD = null;
            string P_WARD_NO = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                //cmdText = "SELECT * FROM VW_HOUSE_BUILDING_DTL WHERE HOUSE_OWNER_ID = '" + y + "'"; //+ " AND DEFINED_CD = " + definedCd  169  ;
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_VIEWS";
                    dt = service.GetDataSet("PR_BUILDING_STRUCTURE_VIEW",
                        y,
                        P_BUILDING_STRUCTURE_NO,
                        P_DISTRICT_CD,
                        P_VDC_MUN_CD,
                        P_WARD_NO,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value
                        );


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



        public DataTable HouseOwnerDetail(string x)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_HOUSE_OWNER_DTL WHERE HOUSE_OWNER_ID = '" + x + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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

        //Change by Prakash

        public DataSet HouseFamilyDetailAll(string householdId, string HouseOwnerId, string StructureNo)
        {
            DataSet ds = null;
            string cmdText = "";
            //OracleType.Cursor aa;
            using (ServiceFactory service = new ServiceFactory("PKG_NHRS_VIEWS"))
            {

                try
                {
                    service.Begin();

                    //DataTable dtbl = service.GetDataTable(true, "PR_HOUSEHOLD_MEMBER_VIEW",
                    //    // p_session_id,
                    // HouseOwnerId,
                    //    StructureNo,
                    //    householdId,
                    //    //DBNull.Value,
                    //    //DBNull.Value,
                    //    DBNull.Value);
                    ds = service.GetDataSetOracle(true, "PR_HOUSEHOLD_MEMBER_VIEW ",

                        HouseOwnerId,
                        StructureNo,
                        householdId,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value
                        );
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
            return ds;
        }
        

        //End Change

        public DataTable GrievMemDetail(string householdId)
        {
            DataTable ds = null;

            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();

                    ds = service.GetDataTable(true, "GET_MEMBER_DETAIL",


                        
                        householdId,
                        DBNull.Value
                        
                        );
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
            return ds;
        }
        public DataTable OtherHousesDamaged(string h)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DTL.FULL_NAME_ENG,DTL.FULL_NAME_LOC,DTL.OTHER_DISTRICT_CD,DTL.OTHER_VDC_MUN_CD,DTL.OTHER_WARD_NO, DTL.BUILDING_CONDITION_CD ,nohc.DESC_ENG TechnicalSol_ENG ,nohc.DESC_LOC TechnicalSol_LOC  FROM NHRS_HOWNER_OTH_RESIDENCE_DTL DTL inner JOIN NHRS_OTH_HOUSE_CONDITION nohc on DTL.BUILDING_CONDITION_CD=nohc.BUILDING_CONDITION_CD WHERE HOUSE_OWNER_ID='" + h + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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

        public DataTable GetOthLivableHouseDtl(string h)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT ngh.*,ngr.RECOMENDATION_FLAG,ngr.CLARIFICATION_FLAG,ngr.FULL_NAME_ENG FROM NHRS_GRIEVANCE_HANDLED ngh inner join nhrs_Grievance_Registration ngr on ngr.case_registration_id=ngh.case_registration_id WHERE ngh.HOUSE_OWNER_ID='" + h + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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

        public string leastDamagedHouseCondtion(string h)
        {
            string Condition = string.Empty;
            string cmdText = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "(SELECT MIN(BUILDING_CONDITION_CD) AS BUILDING_CONDITION_CD FROM NHRS_HOWNER_OTH_RESIDENCE_DTL WHERE 1=1 and house_owner_id='" + h + "')  ";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
                if (dt.Rows.Count > 0 && dt != null)
                {
                    Condition = dt.Rows[0]["BUILDING_CONDITION_CD"].ConvertToString();
                }
            }
            return Condition;
        }
        public DataTable GetPhotoDetail(string ownerId, string structureNo)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = " SELECT T1.HOUSE_PHOTO_ID,T1.PHOTO_PATH,T1.DOC_TYPE_CD,mdt.DESC_ENG DOC_ENG, mdt.DESC_LOC " +
                          " FROM NHRS.NHRS_BUILDING_ASSESSMENT_PHOTO T1 INNER JOIN NHRS.MIS_DOC_TYPE mdt ON T1.DOC_TYPE_CD=mdt.DOC_TYPE_CD " +
                          " WHERE T1.HOUSE_OWNER_ID='" + ownerId + "' AND T1.BUILDING_STRUCTURE_NO='" + structureNo + "'";
                // cmdText = "SELECT nhom.DEFINED_CD ONA_ID,COM_PKG_UTIL.FN_GET_KLL_DISTRICT(nhom.DISTRICT_CD) KLL_DISTRICT_CD,nhom.SUBMISSIONTIME  FROM NHRS_HOUSE_OWNER_MST nhom WHERE nhom.HOUSE_OWNER_ID= '" + ownerId + "'";

                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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

        public List<StructureFamilyDetail> GetStructureFamilyDetail(string HouseOwnerID, string StructureNo)
        {
            List<StructureFamilyDetail> lstStructure = new List<StructureFamilyDetail>();
            DataTable dt = new DataTable();
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = " select BI.HOUSEHOLD_ID,BI.DEFINED_CD,BI.HOUSE_OWNER_ID,BI.BUILDING_STRUCTURE_NO,   BI.FULL_NAME_ENG,BI.FULL_NAME_LOC from MIS_HOUSEHOLD_INFO BI WHERE BI.BUILDING_STRUCTURE_NO='" + StructureNo + "' and BI.HOUSE_OWNER_ID='" + HouseOwnerID + "' ";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            StructureFamilyDetail objStructure = new StructureFamilyDetail();
                            objStructure.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                            objStructure.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                            objStructure.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                            objStructure.H_DEFINED_CD = dr["DEFINED_CD"].ConvertToString();
                            objStructure.H_FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                            objStructure.H_FULL_NAME_LOC = dr["FULL_NAME_LOC"].ConvertToString();
                            //objStructure.M_DEFINED_CD = dr[""].ConvertToString();
                            //objStructure.M_FIRST_NAME_ENG = dr[""].ConvertToString();
                            //objStructure.M_MIDDLE_NAME_ENG = dr[""].ConvertToString();
                            //objStructure.M_LAST_NAME_ENG = dr[""].ConvertToString();
                            //objStructure.M_FULL_NAME_ENG = dr[""].ConvertToString();
                            //objStructure.M_FIRST_NAME_LOC = dr[""].ConvertToString();
                            //objStructure.M_MIDDLE_NAME_LOC = dr[""].ConvertToString();
                            //objStructure.M_LAST_NAME_LOC = dr[""].ConvertToString();
                            //objStructure.M_FULL_NAME_LOC = dr[""].ConvertToString();
                            //objStructure.GENDER_ENG = dr[""].ConvertToString();
                            //objStructure.GENDER_LOC = dr[""].ConvertToString();
                            //objStructure.MARITAL_STATUS_ENG = dr[""].ConvertToString();
                            //objStructure.MARITAL_STATUS_LOC = dr[""].ConvertToString();
                            //objStructure.RELATION_ENG = dr[""].ConvertToString();
                            //objStructure.RELATION_LOC = dr[""].ConvertToString();
                            lstStructure.Add(objStructure);
                        }
                    }
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


            return lstStructure;
        }
        public DataTable GrievanceHouseOwnerNames(string a)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "  SELECT ngh.HOUSE_OWNER_ID, ngr.GID,ngr.FULL_NAME_ENG GRIEVANT_NAME,ngh.HOUSE_OWNER_NAME_ENG,ngem.NRA_DEFINED_CD  "
                            + "   From NHRS_GRIEVANCE_REGISTRATION ngr "
                            + "     LEFT OUTER JOIN NHRS_GRIEVANCE_HANDLED ngh ON ngr.CASE_REGISTRATION_ID = ngh.CASE_REGISTRATION_ID AND ngh.BUILDING_STRUCTURE_NO='1'     "
                             + "    LEFT OUTER JOIN NHRS_GRIEVANCE_ENROLL_MST ngem ON ngem.CASE_GRIEVANCE_ID=ngr.CASE_REGISTRATION_ID       "
                           + " WHERE ngh.HOUSE_OWNER_ID='" + a + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public DataTable HouseOwnerNames(string a)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                //cmdText = "SELECT Distinct VHOD.SNO,VHOD.FIRST_NAME_ENG,VHOD.MIDDLE_NAME_ENG,VHOD.LAST_NAME_ENG,VHOD.FULL_NAME_ENG,"
                //+ " VHOD.FIRST_NAME_LOC,VHOD.MIDDLE_NAME_LOC,VHOD.LAST_NAME_LOC,VHOD.FULL_NAME_LOC,VHOD.GENDER_ENG,VHOD.GENDER_LOC ,"
                //+ " NGEM.NEW_NRA_DEFINED_CD AS NRA_DEFINED_CD3,NREM.NRA_DEFINED_CD AS NRA_DEFINED_CD2,NEMS.NRA_DEFINED_CD AS  NRA_DEFINED_CD1,ngh.HOUSE_OWNER_NAME_ENG, ngr.GID"
                //+ " FROM VW_HOUSE_OWNER_DTL VHOD"
                //+ " LEFT OUTER JOIN nhrs_grievance_handled   ngh On VHOD.HOUSE_OWNER_ID = ngh.HOUSE_OWNER_ID and ngh.BUILDING_STRUCTURE_NO=1"
                // + " LEFT OUTER JOIN nhrs_grievance_registration ngr on  ngh.case_registration_id=ngr.case_registration_id"
                // + " LEFT OUTER JOIN NHRS_GRIEVANCE_ENROLL_MST   NGEM On VHOD.HOUSE_OWNER_ID = NGEM.HOUSE_OWNER_ID and ngh.HOUSE_OWNER_NAME_ENG=VHOD.FULL_NAME_ENG"
                //+ " LEFT OUTER JOIN NHRS_RETRO_ENROLL_MST NREM ON VHOD.HOUSE_OWNER_ID = NREM.HOUSE_OWNER_ID"
                //+ " LEFT OUTER JOIN NHRS_ENROLLMENT_MST NEMS ON VHOD.HOUSE_OWNER_ID= NEMS.HOUSE_OWNER_ID"
                //+ " WHERE VHOD.HOUSE_OWNER_ID='" + a + "' order by VHOD.sno";



                cmdText = "  SELECT  nhod.sno,nhod.full_name_Eng  HOUSE_OWNER_NAME,VBA.NRA_DEFINED_CD, ngr.GID "
                            + "   FROM NHRS_HOUSE_OWNER_MST nhom "
                             + "  left OUTER JOIN  VW_SURVEY_BENEF VBA ON VBA.house_owner_id= NHOM.HOUSE_OWNER_ID       "
                              + "  left OUTER JOIN  nhrs_house_owner_dtl nhod ON nhod.house_owner_id= NHOM.HOUSE_OWNER_ID       "
                            + " JOIN NHRS.MIS_DISTRICT DIS ON DIS.DISTRICT_CD = nhom.DISTRICT_CD                                  "
                            + " JOIN NHRS.MIS_VDC_MUNICIPALITY VDC ON VDC.VDC_MUN_CD = nhom.VDC_MUN_CD                            "
                            + " LEFT OUTER JOIN nhrs_enrollment_mst NHEM  ON VBA.NRA_DEFINED_CD= NHEM.NRA_DEFINED_CD             "
                            + " LEFT OUTER JOIN nhrs_retro_enroll_mst NHRT ON VBA.NRA_DEFINED_CD= NHRT.NRA_DEFINED_CD            "
                            + "LEFT OUTER JOIN nhrs_grievance_registration ngr On nhom.HOUSE_OWNER_ID = ngr.HOUSE_OWNER_ID " 
                           + " WHERE nhom.HOUSE_OWNER_ID='" + a + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public DataTable HouseRespondentNames(string a)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT RESPONDENT_FIRST_NAME,RESPONDENT_MIDDLE_NAME,RESPONDENT_LAST_NAME,RESPONDENT_FULL_NAME,RESPONDENT_FIRST_NAME_LOC,RESPONDENT_MIDDLE_NAME_LOC,RESPONDENT_LAST_NAME_LOC,RESPONDENT_FULL_NAME_LOC,GENDER_ENG,GENDER_LOC,RELATION_ENG,RELATION_LOC,OTHER_RELATION_TYPE,RESPONDENT_PHOTO FROM VW_RESPONDENT_DTL WHERE HOUSE_OWNER_ID='" + a + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public DataTable SuperStructureofHouseName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_SUPERSTRUCTURE_MATERIAL";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> SuperStructureofHouse(string HouseOwnerID, string StructureNo)
        {
            List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> superStructureList = new List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT>();
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_SUPERSTRUCTURE_MATERIAL WHERE BUILDING_STRUCTURE_NO='" + StructureNo + "' and HOUSE_OWNER_ID='" + HouseOwnerID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            MIG_NHRS_HH_SUPERSTRUCTURE_MAT superStructureName = new MIG_NHRS_HH_SUPERSTRUCTURE_MAT();
                            superStructureName.superstructureMatId = Convert.ToDecimal(dr["SUPERSTRUCTURE_MAT_ID"]);
                            superStructureName.superstructureMatEng = dr["SUPERSTRUCTURE_MAT_ENG"].ConvertToString();
                            superStructureName.superstructureMatLoc = dr["SUPERSTRUCTURE_MAT_LOC"].ConvertToString();

                            superStructureList.Add(superStructureName);
                        }
                    }
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
            return superStructureList;
        }
        public DataTable DamageLevelName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM nhrs.NHRS_DAMAGE_LEVEL";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public DataTable DamageExtentofHouseName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_DAMAGE_DETAIL Where DAMAGE_CD!=25";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public List<VW_DamageExtentHome> DamageExtentofHouse(string HouseOwnerID, string StructureNo)
        {
            List<VW_DamageExtentHome> DamageExtentHomeList = new List<VW_DamageExtentHome>();
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_NHRS_BUILDING_ASS_DTL WHERE BUILDING_STRUCTURE_NO='" + StructureNo + "' and HOUSE_OWNER_ID='" + HouseOwnerID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            VW_DamageExtentHome damageExtentHome = new VW_DamageExtentHome();
                            damageExtentHome.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ToDecimal();
                            damageExtentHome.HH_DAMAGE_DTL_ID = dr["HH_DAMAGE_DTL_ID"].ToDecimal();
                            damageExtentHome.DAMAGE_CD = dr["DAMAGE_CD"].ToDecimal();
                            damageExtentHome.DAMAGE_ENG = dr["DAMAGE_ENG"].ConvertToString();
                            damageExtentHome.DAMAGE_LOC = dr["DAMAGE_LOC"].ConvertToString();
                            damageExtentHome.DAMAGE_LEVEL_CD = dr["DAMAGE_LEVEL_CD"].ToDecimal();
                            damageExtentHome.DAMAGE_LEVEL_ENG = dr["DAMAGE_LEVEL_ENG"].ConvertToString();
                            damageExtentHome.DAMAGE_LEVEL_LOC = dr["DAMAGE_LEVEL_LOC"].ConvertToString();
                            damageExtentHome.DAMAGE_LEVEL_DTL_CD = dr["DAMAGE_LEVEL_DTL_CD"].ToDecimal();
                            damageExtentHome.DAMAGE_LEVEL_DTL_ENG = dr["DAMAGE_LEVEL_DTL_ENG"].ConvertToString();
                            damageExtentHome.DAMAGE_LEVEL_DTL_LOC = dr["DAMAGE_LEVEL_DTL_LOC"].ConvertToString();
                            damageExtentHome.COMMENT_ENG = dr["COMMENT_ENG"].ConvertToString();
                            damageExtentHome.COMMENT_LOC = dr["COMMENT_LOC"].ConvertToString();

                            DamageExtentHomeList.Add(damageExtentHome);
                        }
                    }
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
            return DamageExtentHomeList;
        }

        public DataTable GeoTechnicalName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_GEOTECHNICAL_RISK_TYPE";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> GeoTechnicalDetail(string HouseOwnerID, string StructureNo)
        {
            List<NHRS_GEOTECHNICAL_RISK_TYPE> geographicalRiskDetailList = new List<NHRS_GEOTECHNICAL_RISK_TYPE>();
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_GEOTEHNICAL WHERE BUILDING_STRUCTURE_NO='" + StructureNo + "' and HOUSE_OWNER_ID='" + HouseOwnerID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            NHRS_GEOTECHNICAL_RISK_TYPE geotechnicalName = new NHRS_GEOTECHNICAL_RISK_TYPE();
                            geotechnicalName.GEOTECHNICAL_RISK_TYPE_CD = Convert.ToDecimal(dr["GEOTECHNICAL_RISK_TYPE_CD"]);
                            geotechnicalName.GEOTECHNICAL_RISK_ENG = dr["GEOTECHNICAL_RISK_ENG"].ConvertToString();
                            geotechnicalName.GEOTECHNICAL_RISK_LOC = dr["GEOTECHNICAL_RISK_LOC"].ConvertToString();

                            geographicalRiskDetailList.Add(geotechnicalName);
                        }
                    }
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
            return geographicalRiskDetailList;
        }
        public DataTable SecondaryUseName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_SECONDARY_OCCUPANCY";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
        public List<NHRS_SECONDARY_OCCUPANCY> SecondaryUseDetail(string HouseOwnerID, string StructureNo)
        {
            List<NHRS_SECONDARY_OCCUPANCY> SecondaryUseDetailList = new List<NHRS_SECONDARY_OCCUPANCY>();
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_BA_SECONDARY_OCCUPANCY WHERE BUILDING_STRUCTURE_NO='" + StructureNo + "' and HOUSE_OWNER_ID='" + HouseOwnerID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            NHRS_SECONDARY_OCCUPANCY SecondaryUseName = new NHRS_SECONDARY_OCCUPANCY();
                            SecondaryUseName.SEC_OCCUPANCY_CD = Convert.ToDecimal(dr["SEC_OCCUPANCY_CD"]);
                            SecondaryUseName.SECONDARY_OCCUPANCY_ENG = dr["SECONDARY_OCCUPANCY_ENG"].ConvertToString();
                            SecondaryUseName.SECONDARY_OCCUPANCY_LOC = dr["SECONDARY_OCCUPANCY_LOC"].ConvertToString();

                            SecondaryUseDetailList.Add(SecondaryUseName);
                        }
                    }
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
            return SecondaryUseDetailList;
        }

        public DataTable getDamageLavelDetail()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "  SELECT *  FROM NHRS.NHRS_DAMAGE_LEVEL_DTL";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
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
    }
}
