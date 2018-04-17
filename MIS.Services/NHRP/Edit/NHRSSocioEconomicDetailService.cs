using EntityFramework;
using ExceptionHandler;
using MIS.Models.NHRP;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using MIS.Models.NHRP.View;

namespace MIS.Services.NHRP.Edit
{
    public class NHRSSocioEconomicDetailService
    {
        public DataSet GetHouseholdFamilyDetail(string HouseOWnerID, string BuildingStructureNo,string HouseholdID)
        {
            //NHRS_HOUSEHOLD_MEMBER obj = new NHRS_HOUSEHOLD_MEMBER();
            DataSet ds = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_VIEWS";
                    ds = service.GetDataSetOracle(true, "PR_HOUSEHOLD_MEMBER_VIEW", HouseOWnerID, BuildingStructureNo, HouseholdID, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);

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
     
      
        public DataTable GetStructureFamilyDetail(string StructureNo, string HouseOwnerID)
        {
            NhrsHouseDetail obj = new NhrsHouseDetail();
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = " select BI.HOUSEHOLD_ID,BI.DEFINED_CD,BI.HO_DEFINED_CD,BI.HOUSE_OWNER_ID,BI.BUILDING_STRUCTURE_NO, BI.FULL_NAME_ENG,BI.FULL_NAME_LOC from MIS_HOUSEHOLD_INFO BI WHERE BI.BUILDING_STRUCTURE_NO='" + StructureNo + "' and BI.HOUSE_OWNER_ID='" + HouseOwnerID + "' ";
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
            return dtbl;
        }
        public DataTable HouseIndicatorNames()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM MIS_HOUSEHOLD_INDICATOR";
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
       
        public DataTable ReliefMoneyNames()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS_EQ_RELIEF_MONEY";
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
        public void UpdateHouseholdMember(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NHRS_HOUSEHOLD_INFO housemember = new NHRS_HOUSEHOLD_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housemember.HouseholdID = householdmember.HOUSEHOLD_ID;
                    housemember.HouseHoldDefinedCode = householdmember.HOUSEHOLD_DEFINED_CD;
                    housemember.BuildingStructureNo = householdmember.BUILDING_STRUCTURE_NO;
                    housemember.MemberID = householdmember.MEMBER_ID;
                    housemember.MEMBER_DEFINED_CD = householdmember.MEMBER_DEFINED_CD;
                    housemember.HouseOwnerID = householdmember.HOUSE_OWNER_ID;                   
                   
                    
                    housemember.FirstNameEng = householdmember.FIRST_NAME_ENG;
                    housemember.FirstNameLoc = householdmember.FIRST_NAME_ENG;
                    housemember.MiddleNameEng = householdmember.MIDDLE_NAME_ENG;
                    housemember.MiddleNameLoc = householdmember.MIDDLE_NAME_ENG;
                    housemember.LastNameEng = householdmember.LAST_NAME_ENG;
                    housemember.LastNameLoc = householdmember.LAST_NAME_ENG;
                    housemember.FullNameEng = householdmember.FULL_NAME_ENG;
                    housemember.FullNameLoc = householdmember.FULL_NAME_LOC;
                    
                    housemember.RespondentIsHouseHead = householdmember.IS_RESPONDENT_HOUSEHEAD.ConvertToString();

                    housemember.RespondentFirstName = householdmember.RESPONDENT_FIRST_NAME;
                    housemember.RespondentFirstNameLoc = householdmember.RESPONDENT_FIRST_NAME_LOC;
                    housemember.RespondentMiddleName = householdmember.RESPONDENT_MIDDLE_NAME;
                    housemember.RespondentMiddleNameLoc = householdmember.RESPONDENT_MIDDLE_NAME_LOC;
                    housemember.RespondentLastName = householdmember.RESPONDENT_LAST_NAME;
                    housemember.RespondentLastNameLoc = householdmember.RESPONDENT_LAST_NAME_LOC;
                    housemember.RespondentFullName = householdmember.RESPONDENT_FULL_NAME_ENG;
                    housemember.RespondentFullNameLoc = householdmember.RESPONDENT_FULL_NAME_LOC;
                    housemember.RESPONDENT_PHOTO = null;
                    housemember.RespondedGenderCd = householdmember.RESPONDENT_GENDER_CD;
                    housemember.HhRelationTypeCd = householdmember.RESPONDENT_RELATION_WITH_HH.ToDecimal();
                    housemember.MemberCount = householdmember.MEMBER_CNT.ToDecimal();



                    housemember.ShelterSinceQuakeCD = householdmember.SHELTER_SINCE_QUAKE_CD.ToDecimal();
                    housemember.ShelterBeforeQuakeCD = householdmember.SHELTER_BEFORE_QUAKE_CD.ToDecimal();
                    housemember.CurrentShelterCD = householdmember.CURRENT_SHELTER_CD.ToDecimal();
                    housemember.EQVictimIdentityCardCD = householdmember.EQ_VICTIM_IDENTITY_CARD_CD.ToDecimal();
                    housemember.EQVictimIdentityCardNo = householdmember.EQ_VICTIM_IDENTITY_CARD_NO.ToDecimal();
                    if (!string.IsNullOrEmpty(householdmember.EQ_VICTIM_IDENTITY_CARD_CD))
                    {
                        housemember.EQVictimCard = "Y";
                    }
                    else
                    {
                        housemember.EQVictimCard = "N";
                    }
                    housemember.MonthlyIncomeCd = householdmember.MONTHLY_INCOME_CD.ToDecimal();
                    housemember.TelNo = householdmember.TEL_NO.ToDecimal();
                    housemember.MobileNo = householdmember.MOBILE_NO.ToDecimal();
                    housemember.DeathInYear = householdmember.DEATH_IN_A_YEAR.ConvertToString();
                    housemember.DeathCount = householdmember.DEATH_CNT.ToDecimal();
                    housemember.HumanDestroyFlag = householdmember.HUMAN_DESTROY_FLAG.ConvertToString();
                    housemember.HumanDestroyCount = householdmember.HUMAN_DESTROY_CNT.ToDecimal();
                    housemember.StudentSchoolLeft = householdmember.STUDENT_SCHOOL_LEFT.ConvertToString();
                    housemember.StudentSchoolLeftCount = householdmember.DEATH_CNT.ToDecimal();
                    housemember.PregnantCheckup = householdmember.PREGNANT_REGULAR_CHECKUP.ConvertToString();
                    housemember.PregnantCheckupCount = householdmember.PREGNANT_REGULAR_CHECKUP_CNT.ToDecimal();
                    housemember.ChildLeftVaccination = householdmember.CHILD_LEFT_VACINATION.ConvertToString();
                    housemember.ChildLeftVaccinationCount = householdmember.CHILD_LEFT_VACINATION_CNT.ToDecimal();
                    housemember.LeftChangeOccupancy = householdmember.LEFT_CHANGE_OCCUPANY.ConvertToString();
                    housemember.LeftChangeOccupancyCount = householdmember.LEFT_CHANGE_OCCUPANY_CNT.ToDecimal();
                    housemember.UpdatedBy = SessionCheck.getSessionUsername();
                    housemember.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housemember, true);
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

        public void UpdateMember(NHRS_HOUSEHOLD_MEMBER member)
        {
            NHRS_MEMBER_INFO housemember = new NHRS_MEMBER_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housemember.HouseOwnerID = member.HOUSE_OWNER_ID;
                    housemember.BuildingStructureNo = member.BUILDING_STRUCTURE_NO;
                    housemember.HouseholdID = member.HOUSEHOLD_ID;
                    housemember.HouseHoldDefinedCode = member.HOUSEHOLD_DEFINED_CD;                    
                    housemember.MemberID = member.MEMBER_ID;
                    housemember.DefinedCd = member.MEMBER_DEFINED_CD;
                    housemember.FirstNameEng = member.FIRST_NAME_ENG;
                    housemember.FirstNameLoc = member.FIRST_NAME_ENG;
                    housemember.MiddleNameEng = member.MIDDLE_NAME_ENG;
                    housemember.MiddleNameLoc = member.MIDDLE_NAME_ENG;
                    housemember.LastNameEng = member.LAST_NAME_ENG;
                    housemember.LastNameLoc = member.LAST_NAME_ENG;
                    housemember.FullNameEng = member.FULL_NAME_ENG;
                    housemember.FullNameLoc = member.FULL_NAME_LOC;
                    housemember.GenderCd = member.GENDER_CD.ToDecimal();
                    housemember.MaritalStatusCode = member.MARITAL_STATUS_CD;
                    housemember.PresenceStatusCD = member.PRESENCE_STATUS_CD.ToDecimal();
                    housemember.RelationTypeCD = member.RELATION_CD.ToDecimal();
                    housemember.AllowanceDay = member.ALLOWANCE_DAY;
                    housemember.AllowanceMonth = member.ALLOWANCE_MONTH;
                    housemember.AllowanceYear = member.ALLOWANCE_YEARS;
                    housemember.HouseholdHead = member.HOUSEHOLD_HEAD;
                    housemember.BirthYear = member.BIRTH_YEAR;
                    housemember.BirthMonth = member.BIRTH_MONTH;
                    housemember.BirthDay = member.BIRTH_DAY;
                    housemember.BirthYearLoc = member.BIRTH_YEAR_LOC;
                    housemember.BirthMonthLoc = member.BIRTH_MONTH_LOC;
                    housemember.BirthDayLoc = member.BIRTH_DAY_LOC;
                    if (member.BIRTH_DT == "0/0/0" || member.BIRTH_DT == "//")
                    {
                        member.BIRTH_DT = null;
                    }
                    else
                    {
                        housemember.BirthDt = member.BIRTH_DT;
                    }
                    
                    housemember.BirthDtLoc = member.BIRTH_DT_LOC;
                    housemember.Age = member.AGE.ToDecimal();
                    if (!string.IsNullOrEmpty(member.EDUCATION_CD))
                    {
                        housemember.EducationCd = member.EDUCATION_CD;
                    }
                    else
                    {
                        housemember.EducationCd = null;
                    }
                    
                    housemember.Literate = member.LITERATE;
                    housemember.BirthCertificate = member.BIRTH_CERTIFICATE;
                    housemember.CasteCd = member.CASTE_CD;
                    housemember.TelNo = member.TEL_NO.ToDecimal();
                    housemember.MobileNo = member.MOBILE_NO.ToDecimal();
                    housemember.BankBranchCd = member.BANK_BRANCH_CD.ToDecimal();
                    housemember.BankCd = member.BANK_CD.ToDecimal();
                    housemember.HhMemberBankAccNo = member.HH_MEMBER_BANK_ACC_NO;
                    housemember.BankAccountFlag = member.BANK_ACCOUNT_FLAG;
                    housemember.ALLOWANCE_TYPE_CD = member.ALLOWANCE_TYPE_CD.ToDecimal();
                    housemember.SocialAllowance = member.SOCIAL_ALLOWANCE;
                    housemember.HandiColorCd = member.HANDI_COLOR_CD.ToDecimal();
                    housemember.Disability = member.DISABILITY;
                    housemember.UpdatedBy = SessionCheck.getSessionUsername();
                    housemember.Mode = "U";
                    service.PackageName = "PKG_MEMBER";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housemember, true);
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

        public void UpdateMemberDeath(VW_MEMBER_DEATH_DTL memberDeath)
        {
            MisHhDeathDtlInfo memberDeathInfo = new MisHhDeathDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //memberDeathInfo.Sno = memberDeath.M_SNO.ConvertToString();
                    memberDeathInfo.HouseOwnerID = memberDeath.HOUSE_OWNER_ID;
                    memberDeathInfo.BuildingStructureNum = memberDeath.BUILDING_STRUCTURE_NO;
                    memberDeathInfo.HouseholdId = memberDeath.HOUSEHOLD_ID;
                    memberDeathInfo.MemberID = memberDeath.M_DEFINED_CD;
                    memberDeathInfo.FirstNameEng = memberDeath.M_FIRST_NAME_ENG;
                    memberDeathInfo.MiddleNameEng = memberDeath.M_MIDDLE_NAME_ENG;
                    memberDeathInfo.LastNameEng = memberDeath.M_LAST_NAME_ENG;
                    memberDeathInfo.FirstNameLoc = memberDeath.M_FIRST_NAME_LOC;
                    memberDeathInfo.MiddleNameLoc = memberDeath.M_MIDDLE_NAME_LOC;
                    memberDeathInfo.LastNameLoc = memberDeath.M_LAST_NAME_LOC;
                    memberDeathInfo.FullNameEng = memberDeath.M_FULL_NAME_ENG;
                    memberDeathInfo.GenderCd = memberDeath.GENDER_CD.ToDecimal();
                    memberDeathInfo.FullNameLoc = memberDeath.M_FULL_NAME_LOC;
                    memberDeathInfo.DeathDay = memberDeath.DEATH_DAY.ToDecimal();
                    memberDeathInfo.DeathMonth = memberDeath.DEATH_MONTH.ToDecimal();
                    memberDeathInfo.DeathYear = memberDeath.DEATH_YEAR.ToDecimal();
                    memberDeathInfo.DeathDayLoc = memberDeath.DEATH_DAY_LOC.ToDecimal();
                    memberDeathInfo.DeathMonthLoc = memberDeath.DEATH_MONTH_LOC.ToDecimal();
                    memberDeathInfo.DeathYearLoc = memberDeath.DEATH_YEAR_LOC.ToDecimal();
                    if (!string.IsNullOrEmpty(memberDeath.DEATH_YEAR) && !string.IsNullOrEmpty(memberDeath.DEATH_MONTH.ConvertToString()) && !string.IsNullOrEmpty(memberDeath.DEATH_DAY.ConvertToString()))
                    {
                        memberDeathInfo.DeathDt = memberDeath.DEATH_YEAR + "-" + memberDeath.DEATH_MONTH.ConvertToString() + "-" + memberDeath.DEATH_DAY.ConvertToString();
                        memberDeathInfo.DeathDtLoc = memberDeath.DEATH_YEAR + "-" + memberDeath.DEATH_MONTH.ConvertToString() + "-" + memberDeath.DEATH_DAY.ConvertToString();
                    }
                    memberDeathInfo.Age = memberDeath.AGE.ToDecimal();
                    memberDeathInfo.DeathReasonCD = memberDeath.DEATH_REASON_CD.ToDecimal();
                    memberDeathInfo.DeathCertificate = memberDeath.DEATH_CERTIFICATE;

                    memberDeathInfo.UpdatedBy = SessionCheck.getSessionUsername();
                    memberDeathInfo.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(memberDeathInfo, true);
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

        public void UpdateMemberDistroy(VW_MEMBER_HUMAN_DISTROY_DTL memberDistroy)
        {
            NhrsHhHumanDestroyDtlInfo memberDestroyInfo = new NhrsHhHumanDestroyDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    memberDestroyInfo.HouseOwnerId = memberDistroy.HOUSE_OWNER_ID;
                    memberDestroyInfo.BuildingStructureNo = memberDistroy.BUILDING_STRUCTURE_NO;
                    memberDestroyInfo.HouseholdId = memberDistroy.HOUSEHOLD_ID;
                    memberDestroyInfo.MemberId = memberDistroy.M_DEFINED_CD;
                    memberDestroyInfo.HumandestroyTypeCd = memberDistroy.HUMAN_DESTROY_CD.ConvertToString().ToDecimal();
                    memberDestroyInfo.Sno = memberDistroy.M_SNO;
                    memberDestroyInfo.FirstNameEng = memberDistroy.M_FIRST_NAME_ENG;
                    memberDestroyInfo.MiddleNameEng = memberDistroy.M_MIDDLE_NAME_ENG;
                    memberDestroyInfo.LastNameEng = memberDistroy.M_LAST_NAME_ENG;
                    memberDestroyInfo.FirstNameLoc = memberDistroy.M_FIRST_NAME_LOC;
                    memberDestroyInfo.MiddleNameLoc = memberDistroy.M_MIDDLE_NAME_LOC;
                    memberDestroyInfo.LastNameLoc = memberDistroy.M_LAST_NAME_LOC;
                    memberDestroyInfo.FullNameEng = memberDistroy.M_FULL_NAME_ENG;
                    memberDestroyInfo.FullNameLoc = memberDistroy.M_FULL_NAME_LOC;
                    memberDestroyInfo.GenderCd = memberDistroy.GENDER_CD.ToDecimal();
                    memberDestroyInfo.Age = memberDistroy.AGE;

                    memberDestroyInfo.UpdatedBy = SessionCheck.getSessionUsername();
                    memberDestroyInfo.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(memberDestroyInfo, true);
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

        public void InsertHouseholdWater(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NHRShhWaterSourceDtlInfo housememberWater = new NHRShhWaterSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberWater.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberWater.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberWater.WATER_SOURCE_CD = householdmember.WATER_SOURCE_CD;
                    housememberWater.WATER_SOURCE_BEFORE = householdmember.WATER_SOURCE_BEFORE;
                    housememberWater.WATER_SOURCE_AFTER = householdmember.WATER_SOURCE_AFTER;
                    housememberWater.EnteredBy = SessionCheck.getSessionUsername();
                    housememberWater.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberWater.Approved = "Y";
                    housememberWater.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberWater, true);
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
        public void UpdateHouseholdWater(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NHRShhWaterSourceDtlInfo housememberWater = new NHRShhWaterSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberWater.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberWater.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberWater.WATER_SOURCE_CD = householdmember.WATER_SOURCE_CD;
                    housememberWater.WATER_SOURCE_BEFORE = householdmember.WATER_SOURCE_BEFORE;
                    housememberWater.WATER_SOURCE_AFTER = householdmember.WATER_SOURCE_AFTER;
                    housememberWater.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberWater.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberWater, true);
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
        public void DeleteHouseholdWater(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NHRShhWaterSourceDtlInfo housememberWater = new NHRShhWaterSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberWater.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberWater.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberWater.EnteredBy = SessionCheck.getSessionUsername();
                    housememberWater.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberWater.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberWater, true);
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

        public void InsertHouseholdFuel(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhFuelSourceDtlInfo housememberFuel = new NhrsHhFuelSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberFuel.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberFuel.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberFuel.FUEL_SOURCE_CD = householdmember.FUEL_SOURCE_CD;
                    housememberFuel.FUEL_SOURCE_BEFORE = householdmember.FUEL_SOURCE_BEFORE;
                    housememberFuel.FUEL_SOURCE_AFTER = householdmember.FUEL_SOURCE_AFTER;
                    housememberFuel.EnteredBy = SessionCheck.getSessionUsername();
                    housememberFuel.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberFuel.Approved = "Y";
                    housememberFuel.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberFuel, true);
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
        public void UpdateHouseholdFuel(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhFuelSourceDtlInfo housememberFuel = new NhrsHhFuelSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberFuel.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberFuel.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberFuel.FUEL_SOURCE_CD = householdmember.FUEL_SOURCE_CD;
                    housememberFuel.FUEL_SOURCE_BEFORE = householdmember.FUEL_SOURCE_BEFORE;
                    housememberFuel.FUEL_SOURCE_AFTER = householdmember.FUEL_SOURCE_AFTER;
                    housememberFuel.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberFuel.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberFuel, true);
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
        public void DeleteHouseholdFuel(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhFuelSourceDtlInfo housememberFuel = new NhrsHhFuelSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberFuel.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberFuel.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberFuel.FUEL_SOURCE_CD = householdmember.FUEL_SOURCE_CD;
                    housememberFuel.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberFuel.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberFuel, true);
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

        public void InsertHouseholdLight(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhLightSourceDtlInfo housememberLight = new NhrsHhLightSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberLight.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberLight.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberLight.LIGHT_SOURCE_CD = householdmember.LIGHT_SOURCE_CD;
                    housememberLight.LIGHT_SOURCE_BEFORE = householdmember.LIGHT_SOURCE_BEFORE;
                    housememberLight.LIGHT_SOURCE_AFTER = householdmember.LIGHT_SOURCE_AFTER;
                    housememberLight.EnteredBy = SessionCheck.getSessionUsername();
                    housememberLight.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberLight.Approved = "Y";
                    housememberLight.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberLight, true);
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
        public void UpdateHouseholdLight(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhLightSourceDtlInfo housememberLight = new NhrsHhLightSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberLight.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberLight.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberLight.LIGHT_SOURCE_CD = householdmember.LIGHT_SOURCE_CD;
                    housememberLight.LIGHT_SOURCE_BEFORE = householdmember.LIGHT_SOURCE_BEFORE;
                    housememberLight.LIGHT_SOURCE_AFTER = householdmember.LIGHT_SOURCE_AFTER;
                    housememberLight.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberLight.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberLight, true);
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
        public void DeleteHouseholdLight(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhLightSourceDtlInfo housememberLight = new NhrsHhLightSourceDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberLight.HOUSEHOLD_ID = householdmember.HOUSEHOLD_ID;
                    housememberLight.HOUSE_OWNER_ID = householdmember.HOUSE_OWNER_ID;
                    housememberLight.LIGHT_SOURCE_CD = householdmember.LIGHT_SOURCE_CD;
                    housememberLight.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberLight.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberLight, true);
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

        public void InsertHouseholdToilet(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhToiletTypeInfo housememberToilet = new NhrsHhToiletTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberToilet.HouseholdId = householdmember.HOUSEHOLD_ID;
                    housememberToilet.HouseOwnerId = householdmember.HOUSE_OWNER_ID;
                    housememberToilet.ToiletTypeCd = householdmember.TOILET_TYPE_CD.ToDecimal();
                    housememberToilet.ToiletTypeBefore = householdmember.TOILET_TYPE_BEFORE;
                    housememberToilet.ToiletTypeAfter = householdmember.TOILET_TYPE_AFTER;
                    housememberToilet.EnteredBy = SessionCheck.getSessionUsername();
                    housememberToilet.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberToilet.Approved = "Y";
                    housememberToilet.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberToilet, true);
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
        public void UpdateHouseholdToilet(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhToiletTypeInfo housememberToilet = new NhrsHhToiletTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberToilet.HouseholdId = householdmember.HOUSEHOLD_ID;
                    housememberToilet.HouseOwnerId = householdmember.HOUSE_OWNER_ID;
                    housememberToilet.ToiletTypeCd = householdmember.TOILET_TYPE_CD.ToDecimal();
                    housememberToilet.ToiletTypeBefore = householdmember.TOILET_TYPE_BEFORE;
                    housememberToilet.ToiletTypeAfter = householdmember.TOILET_TYPE_AFTER;
                    housememberToilet.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberToilet.Mode = "U";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberToilet, true);
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
        public void DeleteHouseholdToilet(NHRS_HOUSEHOLD_MEMBER householdmember)
        {
            NhrsHhToiletTypeInfo housememberToilet = new NhrsHhToiletTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberToilet.HouseholdId = householdmember.HOUSEHOLD_ID;
                    housememberToilet.HouseOwnerId = householdmember.HOUSE_OWNER_ID;
                    housememberToilet.ToiletTypeCd = householdmember.TOILET_TYPE_CD.ToDecimal();
                    housememberToilet.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberToilet.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberToilet, true);
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

        public void DeleteHouseholdIndicator(MIG_MIS_HOUSEHOLD_INDICATOR householdIndicator)
        {
            NHRS_HOUSEHOLD_INDICATOR_DTL_INFO housememberIndicator = new NHRS_HOUSEHOLD_INDICATOR_DTL_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberIndicator.HouseholdId = householdIndicator.HOUSEHOLD_ID;
                    housememberIndicator.HouseOwnerId = householdIndicator.HOUSE_OWNER_ID;
                    housememberIndicator.INDICATOR_CD = householdIndicator.indicatorCd;
                    housememberIndicator.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberIndicator.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberIndicator, true);
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
        public void InsertHouseholdIndicator(MIG_MIS_HOUSEHOLD_INDICATOR householdIndicator)
        {
            NHRS_HOUSEHOLD_INDICATOR_DTL_INFO housememberIndicator = new NHRS_HOUSEHOLD_INDICATOR_DTL_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberIndicator.HouseholdId = householdIndicator.HOUSEHOLD_ID;
                    housememberIndicator.HouseOwnerId = householdIndicator.HOUSE_OWNER_ID;
                    housememberIndicator.INDICATOR_CD = householdIndicator.indicatorCd;
                    housememberIndicator.INDICATOR_BEFORE = householdIndicator.INDICATOR_BEFORE;
                    housememberIndicator.INDICATOR_AFTER = householdIndicator.INDICATOR_AFTER;
                    housememberIndicator.EnteredBy = SessionCheck.getSessionUsername();
                    housememberIndicator.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberIndicator.Approved = "Y";
                    housememberIndicator.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberIndicator, true);
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

        public void DeleteHouseholdReliefMoney(NHRS_EQ_RELIEF_MONEY householdReliefMoney)
        {
            NHRS_HH_RCVD_EQ_RELIEF_MONEY_INFO housememberReliefMoney = new NHRS_HH_RCVD_EQ_RELIEF_MONEY_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberReliefMoney.HouseholdId = householdReliefMoney.HOUSEHOLD_ID;
                    housememberReliefMoney.HouseOwnerId = householdReliefMoney.HOUSE_OWNER_ID;
                    housememberReliefMoney.EQ_RELIEF_MONEY_CD = householdReliefMoney.EQ_RELIEF_MONEY_CD;
                    housememberReliefMoney.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberReliefMoney.Mode = "D";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberReliefMoney, true);
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
        public void InsertHouseholdReliefMoney(NHRS_EQ_RELIEF_MONEY householdReliefMoney)
        {
            NHRS_HH_RCVD_EQ_RELIEF_MONEY_INFO housememberReliefMoney = new NHRS_HH_RCVD_EQ_RELIEF_MONEY_INFO();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    housememberReliefMoney.HouseholdId = householdReliefMoney.HOUSEHOLD_ID;
                    housememberReliefMoney.HouseOwnerId = householdReliefMoney.HOUSE_OWNER_ID;
                    housememberReliefMoney.EQ_RELIEF_MONEY_CD = householdReliefMoney.EQ_RELIEF_MONEY_CD;
                    housememberReliefMoney.EnteredBy = SessionCheck.getSessionUsername();
                    housememberReliefMoney.UpdatedBy = SessionCheck.getSessionUsername();
                    housememberReliefMoney.Approved = "Y";
                    housememberReliefMoney.Mode = "I";
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(housememberReliefMoney, true);
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
