using EntityFramework;
using ExceptionHandler;
using MIS.Models.NHRP;
using MIS.Models.NHRP.View;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;

namespace MIS.Services.NHRP.Edit
{
    public class NHRSInterviewService
    {
        public DataTable GetBuildingStructureCondition(string HouseOwnerID)
        {
            NhrsHouseDetail obj = new NhrsHouseDetail();
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = String.Format("select BUILDING_STRUCTURE_NO,HOUSE_OWNER_ID,HO_DEFINED_CD,BUILDING_CONDITION_ENG from VW_HOUSE_BUILDING_DTL where HOUSE_OWNER_ID ='" + HouseOwnerID + "'");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
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
            return dtbl ;
        }
        public DataTable OtherHousesDamaged(string HouseOwnerID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT HOUSE_OWNER_ID,   OTHER_RESIDENCE_ID,OTHER_DISTRICT_CD,OTHER_VDC_MUN_CD,OTHER_WARD_NO,BUILDING_CONDITION_CD FROM NHRS_HOWNER_OTH_RESIDENCE_DTL WHERE HOUSE_OWNER_ID='" + HouseOwnerID + "'";
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
        public DataTable HouseOwnerDetail(string HouseOwnerID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT HOUSE_OWNER_ID,SNO,FIRST_NAME_ENG,MIDDLE_NAME_ENG,LAST_NAME_ENG,FULL_NAME_ENG,FIRST_NAME_LOC,MIDDLE_NAME_LOC,LAST_NAME_LOC,FULL_NAME_LOC,GENDER_CD,GENDER_ENG,GENDER_LOC FROM VW_HOUSE_OWNER_DTL WHERE HOUSE_OWNER_ID='" + HouseOwnerID + "'";
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
                cmdText = "SELECT RESPONDENT_FIRST_NAME,RESPONDENT_MIDDLE_NAME,RESPONDENT_LAST_NAME,RESPONDENT_FULL_NAME,RESPONDENT_FIRST_NAME_LOC,RESPONDENT_MIDDLE_NAME_LOC,RESPONDENT_LAST_NAME_LOC,RESPONDENT_FULL_NAME_LOC,RESPONDENT_GENDER_CD,GENDER_ENG,GENDER_LOC,RELATION_ENG,HH_RELATION_TYPE_CD,RELATION_LOC,OTHER_RELATION_TYPE,OTHER_RELATION_TYPE_LOC,RESPONDENT_SNO FROM VW_RESPONDENT_DTL WHERE HOUSE_OWNER_ID='" + a + "'";
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
        public DataSet GetHouseOwnerDetail(string HouseOWnerID)
        {
            //NHRS_HOUSEHOLD_MEMBER obj = new NHRS_HOUSEHOLD_MEMBER();
            DataSet ds = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_VIEWS";
                    ds = service.GetDataSetOracle(true, "PR_HOUSEOWNER_VIEW", HouseOWnerID.ToDecimal(), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,DBNull.Value);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
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
            return ds;
        }
        public DataTable getHouseID(string HouseOwnerID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DEFINED_CD FROM NHRS_HOUSE_OWNER_MST where HOUSE_OWNER_ID='" + HouseOwnerID + "'";
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
        public void UpdateNHRSInterviewMaster(NhrsHouseDetail nhrshousedet)
        {
            NhrsHouseOwnerMstInfo hsOwner = new NhrsHouseOwnerMstInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    hsOwner.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                    hsOwner.EnumeratorId = nhrshousedet.ENUMERATOR_ID;
                    hsOwner.InterviewDt =  nhrshousedet.INTERVIEW_DT.ToDateTime();

                    hsOwner.InterviewDtLoc = nhrshousedet.INTERVIEW_DT_LOC;
                    hsOwner.AreaEng = nhrshousedet.AREA_ENG;
                    hsOwner.AreaLoc = nhrshousedet.AREA_ENG;
                    hsOwner.DefinedCd = nhrshousedet.DEFINED_CD;
                    hsOwner.RespondentIsHouseOwner = nhrshousedet.RESPONDENT_IS_HOUSE_OWNER;

                    hsOwner.DistrictCd = nhrshousedet.DISTRICT_CD;
                    hsOwner.VdcMunCd = nhrshousedet.VDC_MUN_CD;
                    hsOwner.WardNo = nhrshousedet.WARD_NO;
                    hsOwner.EnumerationArea = nhrshousedet.ENUMERATION_AREA;
                    hsOwner.MobileNumber = nhrshousedet.MOBILE_NUMBER;
                    hsOwner.HouseFamilyOwnerCnt = nhrshousedet.HOUSE_FAMILY_OWNER_CNT;
                    hsOwner.NotInterviwingReasonCd = nhrshousedet.NOT_INTERVIWING_REASON_CD;
                    hsOwner.ElectionCenterOHouseCnt = nhrshousedet.ELECTIONCENTER_OHOUSE_CNT;
                    hsOwner.NonElectionCenterFHouseCnt = nhrshousedet.NONELECTIONCENTER_FHOUSE_CNT;
                    hsOwner.NonresidNondamageHCnt = nhrshousedet.NONRESID_NONDAMAGE_H_CNT;
                    hsOwner.NonresidPartialDamageHCnt = nhrshousedet.NONRESID_PARTIAL_DAMAGE_H_CNT;
                    hsOwner.NonresidFullDamageHCnt = nhrshousedet.NONRESID_FULL_DAMAGE_H_CNT;
                    hsOwner.UpdatedBy = SessionCheck.getSessionUsername();
                    hsOwner.UpdatedByLoc = SessionCheck.getSessionUsername();
                    hsOwner.Mode = "U";
                    service.PackageName = "PKG_HOUSEOWNER";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(hsOwner, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public void UpdateNHRSInterviewDetail(HouseOwnerNameModel OwnerName)
        {
            NhrsHouseOwnerDtlInfo houseownerdtl = new NhrsHouseOwnerDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    houseownerdtl.Sno = OwnerName.SN;
                    houseownerdtl.HouseOwnerId = OwnerName.HOUSE_OWNER_ID;
                    houseownerdtl.FirstNameEng = OwnerName.FIRST_NAME_ENG;
                    houseownerdtl.MiddleNameEng = OwnerName.MIDDLE_NAME_ENG;
                    houseownerdtl.LastNameEng = OwnerName.LAST_NAME_ENG;

                    houseownerdtl.FullNameEng = OwnerName.FIRST_NAME_ENG.ConvertToString() + (OwnerName.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + OwnerName.MIDDLE_NAME_ENG) + " ") + OwnerName.LAST_NAME_ENG.ConvertToString();
                    
                    
                    houseownerdtl.FirstNameLoc = OwnerName.FIRST_NAME_ENG;
                    houseownerdtl.MiddleNameLoc = OwnerName.MIDDLE_NAME_ENG;
                    houseownerdtl.LastNameLoc = OwnerName.LAST_NAME_ENG;

                    houseownerdtl.FullNameLoc = OwnerName.FIRST_NAME_ENG.ConvertToString() + (OwnerName.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + OwnerName.MIDDLE_NAME_ENG) + " ") + OwnerName.LAST_NAME_ENG.ConvertToString();

                    houseownerdtl.GenderCd = OwnerName.GENDER_CD.ToDecimal();
                    houseownerdtl.Mode = "U";
                    houseownerdtl.UpdatedBy = SessionCheck.getSessionUsername();
                    //houseownerdtl.UpdatedByLoc = SessionCheck.getSessionUsername();
                    service.PackageName = "PKG_HOUSEOWNER";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(houseownerdtl, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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

        public void UpdateNHRSOtherHouseDamaged(OtherHousesDamagedModel otherElecHouse)
        {
            OtherHousesDamagedInfo housedamagedtl = new OtherHousesDamagedInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housedamagedtl.OTHER_HOUSE_ID = otherElecHouse.OTHER_RESIDENCE_ID;
                    housedamagedtl.HOUSE_OWNER_ID = otherElecHouse.HOUSE_OWNER_ID;
                    housedamagedtl.OTHER_DISTRICT_CD = otherElecHouse.OTHER_DISTRICT_CD;
                    housedamagedtl.OTHER_VDC_MUN_CD = otherElecHouse.OTHER_VDC_MUN_CD;
                    housedamagedtl.OTHER_WARD_NO = otherElecHouse.OTHER_WARD_NO;
                    housedamagedtl.BUILDING_CONDITION_CD = otherElecHouse.BUILDING_CONDITION_CD;
                    housedamagedtl.Mode = "U";
                    housedamagedtl.UpdatedBy = SessionCheck.getSessionUsername();

                    service.PackageName = "PKG_HOUSEOWNER";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housedamagedtl, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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

        public void UpdateNHRSRespondentDetail(VW_HOUSE_RESPONDENT_DETAIL resp)
        {
            NhrsRespondentDtlInfo housedamagedtl = new NhrsRespondentDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housedamagedtl.HouseOwnerId = resp.HOUSE_OWNER_ID;
                    housedamagedtl.RespondentSno = resp.RESPONDENT_SNO;
                    housedamagedtl.RespondentFirstName = resp.FIRST_NAME_ENG;
                    housedamagedtl.RespondentFirstNameLoc = resp.FIRST_NAME_LOC;
                    housedamagedtl.RespondentMiddleName = resp.MIDDLE_NAME_ENG;
                    housedamagedtl.RespondentMiddleNameLoc = resp.MIDDLE_NAME_LOC;
                    housedamagedtl.RespondentLastName = resp.LAST_NAME_ENG;
                    housedamagedtl.RespondentLastNameLoc = resp.LAST_NAME_LOC;

                    housedamagedtl.RespondentFullName = resp.FIRST_NAME_ENG.ConvertToString() + (resp.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + resp.MIDDLE_NAME_ENG) + " ") + resp.LAST_NAME_ENG.ConvertToString();

                    housedamagedtl.RespondentFullNameLoc = resp.FIRST_NAME_ENG.ConvertToString() + (resp.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + resp.MIDDLE_NAME_ENG) + " ") + resp.LAST_NAME_ENG.ConvertToString();

                    housedamagedtl.RespondentGenderCd = resp.GENDER_CD;
                    housedamagedtl.HhRelationTypeCd = resp.RELATION_CD;

                    housedamagedtl.OtherRelationType = resp.OTHER_RELATION_TYPE;
                    housedamagedtl.OtherRelationTypeLoc = resp.OTHER_RELATION_TYPE_LOC;

                    housedamagedtl.Mode = "U";
                    

                    service.PackageName = "PKG_HOUSEOWNER";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housedamagedtl, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
    }
}
