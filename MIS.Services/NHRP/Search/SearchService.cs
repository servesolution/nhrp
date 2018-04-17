using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Search;
using MIS.Services.Core;
using MIS.Models.NHRP;

namespace MIS.Services.NHRP.Search
{
    public class SearchService
    {
        public DataTable getHouseSearchDetail(HouseSearch objHouseSearch)
        {
            DataTable dt = new DataTable();
            Object district = DBNull.Value;
            Object vdc = DBNull.Value;
            Object ward = DBNull.Value;
            Object imei = DBNull.Value;
            Object instance_unique_sno = DBNull.Value;
            Object SIM_NUMBER = DBNull.Value;
            Object mobile = DBNull.Value;
            Object householddsn = DBNull.Value;
            Object HouseOwnerID = DBNull.Value;
            Object fullname = DBNull.Value;
            Object fullnameloc = DBNull.Value;
            Object enumeratorid = DBNull.Value;
            Object interviewdtfrom = DBNull.Value;
            Object interviewdtto = DBNull.Value;
            Object houseownercount = DBNull.Value;
            Object interviewnotRegion = DBNull.Value;
            Object buildingCntfrm = DBNull.Value;
            Object buildingCntto = DBNull.Value;
            Object householdcntfrm = DBNull.Value;
            Object householdcntto = DBNull.Value;
            Object otherhousecntfrom = DBNull.Value;
            Object otherhousecntto = DBNull.Value;
            Object membercntfrm = DBNull.Value;
            Object membercntto = DBNull.Value;
            Object structurecntfrom = DBNull.Value;
            Object structurecntto = DBNull.Value;
            Object nra_defined_cd= DBNull.Value;

            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;


            if (objHouseSearch.NRA_DEFINED_CD != "")
            {
                nra_defined_cd = objHouseSearch.NRA_DEFINED_CD;
            }

            if (objHouseSearch.StrDistrict != "")
            {
                district = objHouseSearch.StrDistrict;
            }
            if (objHouseSearch.StrVDC != "")
            {
                vdc = objHouseSearch.StrVDC;
            }
            if (objHouseSearch.StrWard != "")
            {
                ward = objHouseSearch.StrWard;
            }



            if (objHouseSearch.Imei != "")
            {
                imei = objHouseSearch.Imei;
            }
            if (objHouseSearch.INSTANCE_UNIQUE_SNO != "")
            {
                instance_unique_sno = objHouseSearch.INSTANCE_UNIQUE_SNO;
            }
            if (objHouseSearch.SIM_NUMBER != "")
            {
                SIM_NUMBER = objHouseSearch.SIM_NUMBER;
            }
            if (objHouseSearch.MOBILE_NUMBER != "")
            {
                mobile = objHouseSearch.MOBILE_NUMBER;
            }


            if (objHouseSearch.FULL_NAME_LOC != "")
            {
                fullnameloc = objHouseSearch.FULL_NAME_LOC;
            }
            if (objHouseSearch.StrHouseHoldSN != "")
            {
                householddsn = (objHouseSearch.StrHouseHoldSN);
            }
            if (objHouseSearch.HOUSE_OWNER_ID != "")
            {
                HouseOwnerID = (objHouseSearch.HOUSE_OWNER_ID);
            }
            if (objHouseSearch.FULL_NAME_ENG != "")
            {
                fullname = objHouseSearch.FULL_NAME_ENG;
            }

            if (objHouseSearch.ENUMERATOR_ID != "")
            {
                enumeratorid = objHouseSearch.ENUMERATOR_ID;
            }
            if (objHouseSearch.INTERVIEW_DT_FROM != "" && objHouseSearch.INTERVIEW_DT_TO != "")
            {
                interviewdtfrom = (objHouseSearch.INTERVIEW_DT_FROM);
                interviewdtto = (objHouseSearch.INTERVIEW_DT_TO);
            }
            if (objHouseSearch.HOUSE_OWNER_CNT != "")
            {
                houseownercount = Decimal.Parse(objHouseSearch.HOUSE_OWNER_CNT);
            }
            if (objHouseSearch.NOT_INTERVIWING_REASON_CD != "")
            {
                interviewnotRegion = objHouseSearch.NOT_INTERVIWING_REASON_CD;
            }
            if (objHouseSearch.StrDistrict != "")
            {
                district = objHouseSearch.StrDistrict;
            }
            if (objHouseSearch.BuildingCntfrm != "")
            {
                buildingCntfrm = Decimal.Parse(objHouseSearch.BuildingCntfrm);
            }

            if (objHouseSearch.BuildingCntto != "")
            {
                buildingCntto = Decimal.Parse(objHouseSearch.BuildingCntto);
            }

            if (objHouseSearch.Householdcntfrm != "")
            {
                householdcntfrm = Decimal.Parse(objHouseSearch.Householdcntfrm);
            }

            if (objHouseSearch.Householdcntto != "")
            {
                householdcntto = Decimal.Parse(objHouseSearch.Householdcntto);
            }

            if (objHouseSearch.Membercntfrm != "")
            {
                membercntfrm = Decimal.Parse(objHouseSearch.Membercntfrm);
            }
            if (objHouseSearch.Membercntto != "")
            {
                membercntto = Decimal.Parse(objHouseSearch.Membercntto);
            }

            if (objHouseSearch.STRUCTURE_COUNT_FROM!="")
            {
                structurecntfrom = (objHouseSearch.STRUCTURE_COUNT_FROM); 
            }
            if(objHouseSearch.STRUCTURE_COUNT_TO!="")
            {
                structurecntto = (objHouseSearch.STRUCTURE_COUNT_TO);
            }
            if (objHouseSearch.TOTAL_OTHER_HOUSE_CNT_FROM != "")
            {
                otherhousecntfrom = (objHouseSearch.TOTAL_OTHER_HOUSE_CNT_FROM);
            }
            if (objHouseSearch.TOTAL_OTHER_HOUSE_CNT_TO != "")
            {
                otherhousecntto = (objHouseSearch.TOTAL_OTHER_HOUSE_CNT_TO);
            }

            if (objHouseSearch.SORT_BY != "")
            {
                SortBy = objHouseSearch.SORT_BY;
            }
            if (objHouseSearch.SORT_ORDER != "")
            {
                SortOrder = objHouseSearch.SORT_ORDER;
            }
            if (objHouseSearch.PAGE_SIZE != "")
            {
                PageSize = objHouseSearch.PAGE_SIZE;
            }
            if (objHouseSearch.PAGE_INDEX != "")
            {
                PageIndex = objHouseSearch.PAGE_INDEX;
            }
            if (objHouseSearch.EXPORT_EXCEL != "")
            {
                ExportExcel = objHouseSearch.EXPORT_EXCEL;
            }
            if (objHouseSearch.LANG != "")
            {
                Lang = objHouseSearch.LANG;
            }
            if (objHouseSearch.FILTER_WORD != "")
            {
                FilterWord = objHouseSearch.FILTER_WORD;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";
                    dt = service.GetDataTable(true, "PR_HOUSEDTL_SEARCH",
                                                district,
                                                vdc,
                                                ward,
                                                instance_unique_sno,
                                                SIM_NUMBER,
                                                imei,
                                                mobile,
                                                HouseOwnerID,
                                                householddsn,
                                                fullname,
                                                fullnameloc,
                                                enumeratorid,
                                                interviewdtfrom,
                                                interviewdtto,
                                                houseownercount,
                                               interviewnotRegion,
                                                structurecntfrom,
                                               structurecntto,
                                               householdcntfrm,
                                               householdcntto,
                                               membercntfrm,
                                               membercntto,
                                              otherhousecntfrom,
                                              otherhousecntto,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
                                                DBNull.Value,
                                                nra_defined_cd
                                                );
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
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
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["VDC_ENG"].ConvertToString() != "" ? dr["VDC_ENG"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_ENG"].ConvertToString() != "" ? dr["DISTRICT_ENG"].ConvertToString() : ""), (dr["VDC_LOC"].ConvertToString() != "" ? dr["VDC_LOC"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_LOC"].ConvertToString() != "" ? dr["DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;

        }
        public DataTable getMemberSearchDetail(MemberSearch objMemberSearch)
        {
            DataTable dt = new DataTable();
            Object sessionid = DBNull.Value;
            Object memberid = DBNull.Value;
            Object definedcd = DBNull.Value;
            Object fullnameeng = DBNull.Value;
            Object fullnameloc = DBNull.Value;
            Object gender = DBNull.Value;
            Object birthDtEngSt = DBNull.Value;
            Object birthDtEngTo = DBNull.Value;
            Object birthdatest = DBNull.Value;
            Object birthdateto = DBNull.Value;
            Object maritalstatus = DBNull.Value;
            Object age = DBNull.Value;
            Object caste = DBNull.Value;
            Object religioncd = DBNull.Value;
            Object educationcd = DBNull.Value;
            Object citizenshipno = DBNull.Value;
            Object citizenshipissuedistrict = DBNull.Value;
            Object citizenshipissuedate = DBNull.Value;
            Object citizenshipissuedateloc = DBNull.Value;
            Object curdistrict = DBNull.Value;
            Object curvdc = DBNull.Value;
            Object curward = DBNull.Value;
            Object perdistrict = DBNull.Value;
            Object pervdc = DBNull.Value;
            Object perward = DBNull.Value;
            Object enteredby = DBNull.Value;
            Object IdentificationType = DBNull.Value;
            Object BankCode = DBNull.Value;
            Object ageFrom = DBNull.Value;
            Object ageTo = DBNull.Value;

            Object InstanceUniqueSno = DBNull.Value;
            Object householdId = DBNull.Value;
            Object O_DEFINED_CD = DBNull.Value;
            Object houseid = DBNull.Value;
            Object ExportToExcel = DBNull.Value;
            Object SearchFromHouseHold = DBNull.Value;
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = DBNull.Value;
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            if (objMemberSearch.SESSION_ID != "")
            {
                sessionid = objMemberSearch.SESSION_ID;
            }
            if (objMemberSearch.MEMBER_ID != "")
            {
                memberid = objMemberSearch.MEMBER_ID;
            }
            if (objMemberSearch.DEFINED_CD != "")
            {
                definedcd = objMemberSearch.DEFINED_CD;
            }
            if (objMemberSearch.StrFullName != "")
            {
                fullnameeng = objMemberSearch.StrFullName;
            }
            if (objMemberSearch.FULL_NAME_LOC != "")
            {
                fullnameloc = objMemberSearch.FULL_NAME_LOC;
            }

            if (objMemberSearch.StrGender != "")
            {
                gender = objMemberSearch.StrGender;
            }
            if (objMemberSearch.BIRTH_DT_ENG_ST != "" && objMemberSearch.BIRTH_DT_ENG_TO != "")
            {
                birthDtEngSt = (objMemberSearch.BIRTH_DT_ENG_ST);
                birthDtEngTo = (objMemberSearch.BIRTH_DT_ENG_TO);
            }
            if (objMemberSearch.BIRTH_DT_ST != "" && objMemberSearch.BIRTH_DT_TO != "")
            {
                birthdatest = (objMemberSearch.BIRTH_DT_ST);
                birthdateto = (objMemberSearch.BIRTH_DT_TO);
            }
            if (objMemberSearch.MARITAL_STATUS_CD != "")
            {
                maritalstatus = Decimal.Parse(objMemberSearch.MARITAL_STATUS_CD);
            }
            if (objMemberSearch.AGE != "")
            {
                age = Decimal.Parse(objMemberSearch.AGE);
            }
            if (objMemberSearch.CASTE_CD != "")
            {
                caste = Decimal.Parse(objMemberSearch.CASTE_CD);
            }
            if (objMemberSearch.RELIGION_CD != "")
            {
                religioncd = Decimal.Parse(objMemberSearch.RELIGION_CD);
            }
            if (objMemberSearch.EDUCATION_CD != "")
            {
                educationcd = Decimal.Parse(objMemberSearch.EDUCATION_CD);
            }
            if (objMemberSearch.CTZ_NO != "")
            {
                citizenshipno = Decimal.Parse(objMemberSearch.CTZ_NO);
            }
            if (objMemberSearch.CTZ_ISSUE_DISTRICT_CD != "")
            {
                citizenshipissuedistrict = objMemberSearch.CTZ_ISSUE_DISTRICT_CD;
            }
            if (objMemberSearch.CTZ_ISSUE_DT != "")
            {
                citizenshipissuedate = objMemberSearch.CTZ_ISSUE_DT;
            }
            if (objMemberSearch.CTZ_ISSUE_DT_LOC != "")
            {
                citizenshipissuedateloc = (objMemberSearch.CTZ_ISSUE_DT_LOC);
            }
            if (objMemberSearch.CUR_DISTRICT_CD != "")
            {
                curdistrict = Decimal.Parse(objMemberSearch.CUR_DISTRICT_CD);
            }
            if (objMemberSearch.CUR_VDC_MUN_CD != "")
            {
                curvdc = Decimal.Parse(objMemberSearch.CUR_VDC_MUN_CD);
            }
            if (objMemberSearch.CUR_WARD_NO != "")
            {
                curward = Decimal.Parse(objMemberSearch.CUR_WARD_NO);
            }
            if (objMemberSearch.PER_DISTRICT_CD != "")
            {
                perdistrict = Decimal.Parse(objMemberSearch.PER_DISTRICT_CD);
            }

            if (objMemberSearch.PER_VDC_MUN_CD != "")
            {
                pervdc = Decimal.Parse(objMemberSearch.PER_VDC_MUN_CD);
            }

            if (objMemberSearch.PER_WARD_NO != "")
            {
                perward = Decimal.Parse(objMemberSearch.PER_WARD_NO);
            }

            if (objMemberSearch.ENTERED_BY != "")
            {
                enteredby = Decimal.Parse(objMemberSearch.ENTERED_BY);
            }
            if (objMemberSearch.IDENTIFICATION_TYPE_CD != "")
            {
                IdentificationType = Decimal.Parse(objMemberSearch.IDENTIFICATION_TYPE_CD);
            }
            if (objMemberSearch.BANK_CD != "")
            {
                BankCode = Decimal.Parse(objMemberSearch.BANK_CD);
            }
            if (objMemberSearch.AgeFrom != "" && objMemberSearch.AgeTo != "")
            {
                ageFrom = Decimal.Parse(objMemberSearch.AgeFrom);
                ageTo = Decimal.Parse(objMemberSearch.AgeTo);
            }
            //house id added
            if (objMemberSearch.HOUSE_ID != "")
            {
                houseid = objMemberSearch.HOUSE_ID;
            }

            if (objMemberSearch.HOUSEHOLD_ID != "" )
            {
                householdId =objMemberSearch.HOUSEHOLD_ID;
                
            }
            if (objMemberSearch.INSTANCE_UNIQUE_SNO != "")
            {
                InstanceUniqueSno = objMemberSearch.INSTANCE_UNIQUE_SNO;

            }

            if (objMemberSearch.SEARCH_FROM_HOUSEHOLD != "")
            {
                SearchFromHouseHold = objMemberSearch.SEARCH_FROM_HOUSEHOLD;
            }
            if (objMemberSearch.EXPORT_EXCEL != "")
            {
                ExportToExcel = objMemberSearch.EXPORT_EXCEL;
            }
            if (objMemberSearch.SORT_BY != "")
            {
                SortBy = objMemberSearch.SORT_BY;
            }
            if (objMemberSearch.SORT_ORDER != "")
            {
                SortOrder = objMemberSearch.SORT_ORDER;
            }
            if (objMemberSearch.PAGE_SIZE != "")
            {
                PageSize = objMemberSearch.PAGE_SIZE;
            }
            if (objMemberSearch.PAGE_INDEX != "")
            {
                PageIndex = objMemberSearch.PAGE_INDEX;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";
                    dt = service.GetDataTable(true, "PR_NHRS_MEMBER_SEARCH", sessionid, memberid, definedcd, O_DEFINED_CD, 
                        InstanceUniqueSno, fullnameeng, fullnameloc, gender, birthDtEngSt, birthDtEngTo, birthdatest, birthdateto, 
                        maritalstatus, age, caste, religioncd, educationcd, citizenshipno, citizenshipissuedistrict, citizenshipissuedate, 
                        citizenshipissuedateloc, curdistrict, curvdc, curward, perdistrict, pervdc, perward, enteredby, IdentificationType,
                        BankCode, ageFrom, ageTo, householdId, ExportToExcel, DBNull.Value, SortBy, SortOrder, PageSize, PageIndex, DBNull.Value);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
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
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                dt.Columns.Add("BIRTH_DATE");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["PER_VDC_ENG"].ConvertToString() != "" ? dr["PER_VDC_ENG"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_ENG"].ConvertToString() != "" ? dr["PER_DISTRICT_ENG"].ConvertToString() : ""), (dr["PER_VDC_LOC"].ConvertToString() != "" ? dr["PER_VDC_LOC"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_LOC"].ConvertToString() != "" ? dr["PER_DISTRICT_LOC"].ConvertToString() : ""));
                    string birthDate = String.Empty;
                    string birthDateLoc = String.Empty;
                    if (dr["BIRTH_DAY"].ConvertToString() != "99" && dr["BIRTH_MONTH"].ConvertToString() != "99" && dr["BIRTH_YEAR"].ConvertToString() != "9999" && dr["BIRTH_DAY"].ConvertToString() != "" && dr["BIRTH_MONTH"].ConvertToString() != "" && dr["BIRTH_YEAR"].ConvertToString() != "")
                    {
                        birthDate = dr["BIRTH_DAY"].ConvertToString() + "-" + dr["BIRTH_MONTH"].ConvertToString() + "-" + dr["BIRTH_YEAR"].ConvertToString();
                    }
                    if (dr["BIRTH_DAY_LOC"].ConvertToString() != "99" && dr["BIRTH_MONTH_LOC"].ConvertToString() != "99" && dr["BIRTH_YEAR_LOC"].ConvertToString() != "9999" && dr["BIRTH_DAY_LOC"].ConvertToString() != "" && dr["BIRTH_MONTH_LOC"].ConvertToString() != "" && dr["BIRTH_YEAR_LOC"].ConvertToString() != "")
                    {
                        birthDateLoc = dr["BIRTH_YEAR_LOC"].ConvertToString() + "-" + dr["BIRTH_MONTH_LOC"].ConvertToString() + "-" + dr["BIRTH_DAY_LOC"].ConvertToString();
                    }
                    dr["BIRTH_DATE"] = Utils.ToggleLanguage(birthDate, birthDateLoc);
                    dr["HOUSEHOLD_HEAD"] = dr["HOUSEHOLD_HEAD"].ConvertToString() == "Y" ? "Yes" : "No";
                }
            }
            return dt;

        }
        public DataTable getHouseholdSearchDetail(HouseholdSearch objHouseholdSearch)
        {
            DataTable dt = new DataTable();
            Object sessionid = DBNull.Value;
            Object memberid = DBNull.Value;
            Object householdid = DBNull.Value;
            Object houseownerid = DBNull.Value;
            Object BuildingStructureNo = DBNull.Value;
            Object definedcd = DBNull.Value;
            Object firstnameeng = DBNull.Value;
            Object middlenameeng = DBNull.Value;
            Object lastnameeng = DBNull.Value;
            Object fullnameeng = DBNull.Value;
            Object firstnameloc = DBNull.Value;
            Object middlenameloc = DBNull.Value;
            Object lastnameloc = DBNull.Value;
            Object fullnameloc = DBNull.Value;
            Object RFnameEng = DBNull.Value;
            Object RMnameEng = DBNull.Value;
            Object RLnameEng = DBNull.Value;
            Object RFullnameEng = DBNull.Value;
            Object RFnameLoc = DBNull.Value;
            Object RMnameLoc = DBNull.Value;
            Object RLnameLoc = DBNull.Value;
            Object RFullnameLoc = DBNull.Value;
            Object perdistrictcd = DBNull.Value;
            Object pervdcmunCd = DBNull.Value;
            Object perwardno = DBNull.Value;
            Object perAreaEng = DBNull.Value;
            Object perAreaLoc = DBNull.Value;
            Object houseNo = DBNull.Value;
            Object curDistrict = DBNull.Value;
            Object curVDCMun = DBNull.Value;
            Object curWardNo = DBNull.Value;
            Object curAreaEng = DBNull.Value;
            Object curAreaLoc = DBNull.Value;
            Object ShelterSinceQuake = DBNull.Value;
            Object ShelterBeforeQuake = DBNull.Value;
            Object CurrentShelter = DBNull.Value;
            Object EQVictimIDCard = DBNull.Value;
            Object MonthlyIncomeCd = DBNull.Value;
            Object DeathCntfrom = DBNull.Value;
            Object DeathCntTo = DBNull.Value;
            Object HumanDestroyCntFrom = DBNull.Value;
            Object HumanDestroyCntTo = DBNull.Value;
            Object SchoolStudentLeftCnt = DBNull.Value;
            Object PregnantCheckupCnt = DBNull.Value;
            Object ChildleftVaccinationCnt = DBNull.Value;
            Object LeftChangeOccupancyCnt = DBNull.Value;
            Object LightSource = DBNull.Value;
            Object FuelSource = DBNull.Value;
            Object ToiletType = DBNull.Value;
            Object WaterSource = DBNull.Value;
            Object MobileNo = DBNull.Value;
            Object BankAccount = DBNull.Value;
            Object EQreliefFund = DBNull.Value;
            Object EnteredBy = DBNull.Value;
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = DBNull.Value;
            Object headgendercd = DBNull.Value;
            Object ho_defined_code = DBNull.Value;
            Object instance_unique_sno = DBNull.Value;
            Object PageIndex = "1";

            if (objHouseholdSearch.SESSION_ID != "")
            {
                sessionid = Decimal.Parse(objHouseholdSearch.SESSION_ID);
            }
            if (objHouseholdSearch.MEMBER_ID != "")
            {
                memberid = objHouseholdSearch.MEMBER_ID;
            }
            if (objHouseholdSearch.HOUSEHOLD_ID != "")
            {
                householdid = Decimal.Parse(objHouseholdSearch.HOUSEHOLD_ID);
            }
            if (objHouseholdSearch.HOUSE_OWNER_ID != "")
            {
                houseownerid = objHouseholdSearch.HOUSE_OWNER_ID;
            }
            if (objHouseholdSearch.BUILDING_STRUCTURE_NO != "")
            {
                BuildingStructureNo = Decimal.Parse(objHouseholdSearch.BUILDING_STRUCTURE_NO);
            }
            if (objHouseholdSearch.DEFINED_CD != "")
            {
                definedcd = Decimal.Parse(objHouseholdSearch.DEFINED_CD);
            }
            if (objHouseholdSearch.FIRST_NAME_ENG != "")
            {
                firstnameeng = objHouseholdSearch.FIRST_NAME_ENG;
            }
            if (objHouseholdSearch.MIDDLE_NAME_ENG != "")
            {
                middlenameeng = objHouseholdSearch.MIDDLE_NAME_ENG;
            }
            if (objHouseholdSearch.LAST_NAME_ENG != "")
            {
                lastnameeng = objHouseholdSearch.LAST_NAME_ENG;
            }
            if (objHouseholdSearch.StrFullName != "")
            {
                fullnameeng = objHouseholdSearch.StrFullName;
            }
            if (objHouseholdSearch.FIRST_NAME_LOC != "")
            {
                firstnameloc = objHouseholdSearch.FIRST_NAME_LOC;
            }
            if (objHouseholdSearch.MIDDLE_NAME_LOC != "")
            {
                middlenameloc = objHouseholdSearch.MIDDLE_NAME_LOC;
            }
            if (objHouseholdSearch.LAST_NAME_LOC != "")
            {
                lastnameloc = objHouseholdSearch.LAST_NAME_LOC;
            }
            if (objHouseholdSearch.FULL_NAME_LOC != "")
            {
                fullnameloc = objHouseholdSearch.FULL_NAME_LOC;
            }

            if (objHouseholdSearch.RESPONDENT_FNAME_ENG != "")
            {
                RFnameEng = objHouseholdSearch.RESPONDENT_FNAME_ENG;
            }
            if (objHouseholdSearch.RESPONDENT_FNAME_LOC != "")
            {
                RFnameLoc = objHouseholdSearch.RESPONDENT_FNAME_LOC;
            }
            if (objHouseholdSearch.RESPONDENT_MNAME_ENG != "")
            {
                RMnameEng = objHouseholdSearch.RESPONDENT_MNAME_ENG;
            }
            if (objHouseholdSearch.RESPONDENT_MNAME_LOC != "")
            {
                RMnameLoc = objHouseholdSearch.RESPONDENT_MNAME_LOC;
            }
            if (objHouseholdSearch.RESPONDENT_LNAME_ENG != "")
            {
                RLnameEng = objHouseholdSearch.RESPONDENT_LNAME_ENG;
            }
            if (objHouseholdSearch.RESPONDENT_LNAME_LOC != "")
            {
                RLnameLoc = objHouseholdSearch.RESPONDENT_LNAME_LOC;
            }
            if (objHouseholdSearch.RESPONDENT_FULLNAME_ENG != "")
            {
                RFullnameEng = objHouseholdSearch.RESPONDENT_FULLNAME_ENG;
            }
            if (objHouseholdSearch.RESPONDENT_FULLNAME_LOC != "")
            {
                RFullnameLoc = objHouseholdSearch.RESPONDENT_FULLNAME_LOC;
            }
            if (objHouseholdSearch.SHELTER_SINCE_QUAKE_CD != "")
            {
                ShelterSinceQuake = objHouseholdSearch.SHELTER_SINCE_QUAKE_CD;
            }
            if (objHouseholdSearch.SHELTER_BEFORE_QUAKE_CD != "")
            {
                ShelterBeforeQuake = objHouseholdSearch.SHELTER_BEFORE_QUAKE_CD;
            }
            if (objHouseholdSearch.CURRENT_SHELTER_CD != "")
            {
                CurrentShelter = objHouseholdSearch.CURRENT_SHELTER_CD;
            }
            if (objHouseholdSearch.EQ_VICTIM_IDENTITY_CARD_CD != "")
            {
                EQVictimIDCard = objHouseholdSearch.EQ_VICTIM_IDENTITY_CARD_CD;
            }
            if (objHouseholdSearch.MONTHLY_INCOME_CD != "")
            {
                MonthlyIncomeCd = objHouseholdSearch.MONTHLY_INCOME_CD;
            }
            if (objHouseholdSearch.DEATH_CNT_FROM != "" && objHouseholdSearch.DEATH_CNT_TO != "")
            {
                DeathCntfrom = (objHouseholdSearch.DEATH_CNT_FROM);
                DeathCntTo = (objHouseholdSearch.DEATH_CNT_TO);
            }
            if (objHouseholdSearch.HUMAN_DESTROY_CNT_FROM != "" && objHouseholdSearch.HUMAN_DESTROY_CNT_TO != "")
            {
                HumanDestroyCntFrom = (objHouseholdSearch.HUMAN_DESTROY_CNT_FROM);
                HumanDestroyCntTo = (objHouseholdSearch.HUMAN_DESTROY_CNT_TO);
            }
            if (objHouseholdSearch.PER_DISTRICT_CD != "")
            {
                perdistrictcd = objHouseholdSearch.PER_DISTRICT_CD;
            }
            if (objHouseholdSearch.PER_VDC_MUN_CD != "")
            {
                pervdcmunCd = (objHouseholdSearch.PER_VDC_MUN_CD);
            }
            if (objHouseholdSearch.PER_WARD_NO != "")
            {
                perwardno = Decimal.Parse(objHouseholdSearch.PER_WARD_NO);
            }
            if (objHouseholdSearch.PER_AREA_ENG != "")
            {
                perAreaEng = objHouseholdSearch.PER_AREA_ENG;
            }
            if (objHouseholdSearch.PER_AREA_LOC != "")
            {
                perAreaLoc = objHouseholdSearch.PER_AREA_LOC;
            }
            if (objHouseholdSearch.HouseNo != "")
            {
                houseNo = Decimal.Parse(objHouseholdSearch.HouseNo);
            }

            if (objHouseholdSearch.CUR_DISTRICT_CD != "")
            {
                curDistrict = Decimal.Parse(objHouseholdSearch.CUR_DISTRICT_CD);
            }

            if (objHouseholdSearch.CUR_VDC_MUN_CD != "")
            {
                 curVDCMun = (objHouseholdSearch.CUR_VDC_MUN_CD);
            }

            if (objHouseholdSearch.CUR_WARD_NO != "")
            {
                curWardNo = Decimal.Parse(objHouseholdSearch.CUR_WARD_NO);
            }

            if (objHouseholdSearch.CUR_AREA_ENG != "")
            {
                curAreaEng = objHouseholdSearch.CUR_AREA_ENG;
            }
            if (objHouseholdSearch.CUR_AREA_LOC != "")
            {
                curAreaLoc = objHouseholdSearch.CUR_AREA_LOC;
            }
            if (objHouseholdSearch.STUDENT_SCHOOL_LEFT_CNT != "")
            {
                SchoolStudentLeftCnt = objHouseholdSearch.STUDENT_SCHOOL_LEFT_CNT;
            }
            if (objHouseholdSearch.PREGNANT_REGULAR_CHECKUP_CNT != "")
            {
                PregnantCheckupCnt = objHouseholdSearch.PREGNANT_REGULAR_CHECKUP_CNT;
            }
            if (objHouseholdSearch.CHILD_LEFT_VACINATION_CNT != "")
            {
                ChildleftVaccinationCnt = objHouseholdSearch.CHILD_LEFT_VACINATION_CNT;
            }
            if (objHouseholdSearch.LEFT_CHANGE_OCCUPANY_CNT != "")
            {
                LeftChangeOccupancyCnt = objHouseholdSearch.LEFT_CHANGE_OCCUPANY_CNT;
            }
            if (objHouseholdSearch.EQ_RELIEF_MONEY_CD != "")
            {
                EQreliefFund = objHouseholdSearch.EQ_RELIEF_MONEY_CD;
            }
            if (objHouseholdSearch.WATER_SOURCE_CD_II != "")
            {
                WaterSource =objHouseholdSearch.WATER_SOURCE_CD_II;
            }
            if (objHouseholdSearch.TOILET_TYPE_CD_I != "")
            {
                ToiletType = objHouseholdSearch.TOILET_TYPE_CD_I;
            }

            if (objHouseholdSearch.LIGHT_SOURCE_CD_II != "")
            {
                LightSource = objHouseholdSearch.LIGHT_SOURCE_CD_II;
            }
            if (objHouseholdSearch.FUEL_SOURCE_CD_II != "")
            {
                FuelSource = objHouseholdSearch.FUEL_SOURCE_CD_II;
            }
            if (objHouseholdSearch.Mobile_Number != "")
            {
                MobileNo = objHouseholdSearch.Mobile_Number;
            }
            if (objHouseholdSearch.Bank_Account != "")
            {
                BankAccount = objHouseholdSearch.Bank_Account;
            }
            if (objHouseholdSearch.SORT_BY != "")
            {
                SortBy = objHouseholdSearch.SORT_BY;
            }
            if (objHouseholdSearch.SORT_ORDER != "")
            {
                SortOrder = objHouseholdSearch.SORT_ORDER;
            }
            if (objHouseholdSearch.PAGE_SIZE != "")
            {
                PageSize = "";
            }
            if (objHouseholdSearch.PAGE_INDEX != "")
            {
                PageIndex = objHouseholdSearch.PAGE_INDEX;
            }
            if (objHouseholdSearch.headgender_cd != "")
            {
                headgendercd = objHouseholdSearch.headgender_cd;
            }
            if (objHouseholdSearch.HO_DEFINED_CD != "")
            {
                ho_defined_code = objHouseholdSearch.HO_DEFINED_CD;
            }
            if (objHouseholdSearch.INSTANCE_UNIQUE_SNO != "")
            {
                instance_unique_sno = objHouseholdSearch.INSTANCE_UNIQUE_SNO;
            }
            
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";

                    dt = service.GetDataTable(true, "PR_NHRS_HOUSEHOLD_SEARCH",
                        sessionid,
                        memberid,
                        householdid,
                        houseownerid,
                        ho_defined_code,
                        instance_unique_sno,
                        BuildingStructureNo,
                        definedcd,
                        // firstnameeng, 
                        // middlenameeng,
                        //lastnameeng, 
                       fullnameeng,
                       fullnameloc,
                        //firstnameloc, 
                       headgendercd,
                        //middlenameloc, 
                        //lastnameloc,  
                        //RFnameEng, 
                        //RMnameEng,
                        //RLnameEng, 
                       RFullnameEng,
                        //RFnameLoc, 
                        //RMnameLoc, 
                        //RLnameLoc, 
                       RFullnameLoc,
                       perdistrictcd,
                       pervdcmunCd,
                        //pervdcmun,
                       perwardno,
                       perAreaEng,
                       perAreaLoc,
                       houseNo,
                       curDistrict,
                       curVDCMun,
                       curWardNo,
                       curAreaEng,
                       curAreaLoc,
                       ShelterSinceQuake,
                       ShelterBeforeQuake,
                       CurrentShelter,
                       EQVictimIDCard,
                       MonthlyIncomeCd,
                       DeathCntfrom,
                       DeathCntTo,
                       HumanDestroyCntFrom,
                       HumanDestroyCntTo,
                       SchoolStudentLeftCnt,
                       PregnantCheckupCnt,
                       ChildleftVaccinationCnt,
                       LeftChangeOccupancyCnt,
                       FuelSource,
                       LightSource,
                       EQreliefFund,
                       ToiletType,
                       WaterSource,
                       MobileNo,
                       BankAccount,
                       EnteredBy,
                       SortBy,
                       SortOrder,
                       PageSize,
                       PageIndex,
                       DBNull.Value);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
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
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_ENG"].ConvertToString() != "" ? dr["PER_DISTRICT_ENG"].ConvertToString() : ""), (dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_LOC"].ConvertToString() != "" ? dr["PER_DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;
        }

        //parameter do nt match
        public DataTable getStructureSearchDetail(StructureSearch objStructureSearch)
        {
            DataTable dt = new DataTable();
            Object district = DBNull.Value;
            Object vdc = DBNull.Value;
            Object ward = DBNull.Value;
            Object instanceuniquesno = DBNull.Value;
            Object imei = DBNull.Value;
            Object mobile = DBNull.Value;
            Object householddsn = DBNull.Value;
            Object fullnameeng = DBNull.Value;
            Object fullnameloc = DBNull.Value;
            Object Grade = DBNull.Value;
            Object techsolution = DBNull.Value;
            Object emumenatorid = DBNull.Value;
            Object interviewdtfrom = DBNull.Value;
            Object interviewdtto = DBNull.Value;
            Object houseownercnt = DBNull.Value;
            Object nircd = DBNull.Value;
            Object houselegalowner = DBNull.Value;
            Object buildingcondition = DBNull.Value;
            Object groundsurfacecd = DBNull.Value;
            Object foundationtypecd = DBNull.Value;
            Object rccd = DBNull.Value;
            Object fccd = DBNull.Value;
            Object sccd = DBNull.Value;
            Object buildingpositioncd = DBNull.Value;
            Object buildingplancd = DBNull.Value;
            Object assessedareacd = DBNull.Value;
            Object reconstructionstarted = DBNull.Value;
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = DBNull.Value;
            Object exportexcel = DBNull.Value;
            Object lang = DBNull.Value;
            Object filterword = DBNull.Value;
            Object houseagefrom = DBNull.Value;
            Object houseageto = DBNull.Value;
            Object geotechnicalrisk = DBNull.Value;
            //Object houseLegelOwnerCd = DBNull.Value;
            Object legalowner_flag = DBNull.Value;
            Object secondaryuse = DBNull.Value;
            Object houseid = DBNull.Value;

            Object PageIndex = "1";

            if (objStructureSearch.StrDistrict != "")
            {
                district = objStructureSearch.StrDistrict;
            }
            if (objStructureSearch.StrVDC != "")
            {
                vdc = objStructureSearch.StrVDC;
            }
            if (objStructureSearch.StrWard != "")
            {
                ward = objStructureSearch.StrWard;
            }
            if (objStructureSearch.INSTANCE_UNIQUE_SNO != "")
            {
                instanceuniquesno = objStructureSearch.INSTANCE_UNIQUE_SNO;
            }
            if (objStructureSearch.Imei != "")
            {
                imei = objStructureSearch.Imei;
            }
            if (objStructureSearch.DAMAGEGRADE != "")
            {
                Grade = objStructureSearch.DAMAGEGRADE;
            }

            if (objStructureSearch.TECHSOLUTION_CD != "")
            {
                techsolution = objStructureSearch.TECHSOLUTION_CD;
            }
            if (objStructureSearch.ENUMERATOR_ID != "")
            {
                emumenatorid = objStructureSearch.ENUMERATOR_ID;
            }
            //if (objStructureSearch.INTERVIEW_DT_FROM != "")
            //{
            //    interviewdtfrom = objStructureSearch.INTERVIEW_DT_FROM;
            //}
            //if (objStructureSearch.INTERVIEW_DT_TO != "")
            //{
            //    interviewdtto = objStructureSearch.INTERVIEW_DT_TO;
            //}
            if (objStructureSearch.HOUSE_OWNER_CNT != "")
            {
                houseownercnt = objStructureSearch.HOUSE_OWNER_CNT;
            }

            if (objStructureSearch.NOT_INTERVIWING_REASON_CD != "")
            {
                nircd = objStructureSearch.NOT_INTERVIWING_REASON_CD;
            }
            if (objStructureSearch.BUILDING_CONDITION_CD != "")
            {
                buildingcondition = objStructureSearch.BUILDING_CONDITION_CD;
            }
            if (objStructureSearch.GROUND_SURFACE_CD != "")
            {
                groundsurfacecd = objStructureSearch.GROUND_SURFACE_CD;
            }
            if (objStructureSearch.FOUNDATION_TYPE_CD != "")
            {
                foundationtypecd = objStructureSearch.FOUNDATION_TYPE_CD;
            }


            if (objStructureSearch.RC_MATERIAL_CD != "")
            {
                rccd = objStructureSearch.RC_MATERIAL_CD;
            }
            if (objStructureSearch.FC_MATERIAL_CD != "")
            {
                fccd = objStructureSearch.FC_MATERIAL_CD;
            }
            if (objStructureSearch.SC_MATERIAL_CD != "")
            {
                sccd = objStructureSearch.SC_MATERIAL_CD;
            }
            if (objStructureSearch.BUILDING_POSITION_CD != "")
            {
                buildingpositioncd = objStructureSearch.BUILDING_POSITION_CD;
            }
            if (objStructureSearch.BUILDING_PLAN_CD != "")
            {
                buildingplancd = objStructureSearch.BUILDING_PLAN_CD;
            }
            if (objStructureSearch.ASSESSED_AREA_CD != "")
            {
                assessedareacd = objStructureSearch.ASSESSED_AREA_CD;
            }
            if (objStructureSearch.Reconstruction != "")
            {
                reconstructionstarted = objStructureSearch.Reconstruction;
            }

            if (objStructureSearch.MOBILE_NUMBER != "")
            {
                mobile = objStructureSearch.MOBILE_NUMBER;
            }
            if (objStructureSearch.StrHouseHoldSN != "")
            {
                householddsn = (objStructureSearch.StrHouseHoldSN);
            }
            if (objStructureSearch.FULL_NAME != "")
            {
                fullnameeng = objStructureSearch.FULL_NAME;
            }
            if (objStructureSearch.FULL_NAME_LOC != "")
            {
                fullnameloc = Decimal.Parse(objStructureSearch.FULL_NAME_LOC);
            }
            if (objStructureSearch.SORT_BY != "")
            {
                SortBy = objStructureSearch.SORT_BY;
            }
            if (objStructureSearch.SORT_ORDER != "")
            {
                SortOrder = objStructureSearch.SORT_ORDER;
            }
            if (objStructureSearch.PAGE_SIZE != "")
            {
                PageSize = "";
            }
            if (objStructureSearch.PAGE_INDEX != "")
            {
                PageIndex = objStructureSearch.PAGE_INDEX;
            }
            if (objStructureSearch.EXPORT_EXCEL != "")
            {
                exportexcel = objStructureSearch.EXPORT_EXCEL;
            }
            if (objStructureSearch.LANG != "")
            {
                lang = objStructureSearch.LANG;
            }
            if (objStructureSearch.FILTER_WORD != "")
            {
                filterword = objStructureSearch.FILTER_WORD;
            }

            if (objStructureSearch.HOUSE_LEGALOWNER != "")
            {
                legalowner_flag = objStructureSearch.HOUSE_LEGALOWNER;
            }


            if (objStructureSearch.HOUSE_AGE_FROM != "")
            {
                houseagefrom = objStructureSearch.HOUSE_AGE_FROM;
            }
            if (objStructureSearch.HOUSE_AGE_TO != "")
            {
                houseageto = objStructureSearch.HOUSE_AGE_TO;
            }


            if (objStructureSearch.SECONDARY_USE != "")
            {
                secondaryuse = objStructureSearch.SECONDARY_USE;
            }
            if (objStructureSearch.GEOTECHNICAL_RISK != "")
            {
                geotechnicalrisk = (objStructureSearch.GEOTECHNICAL_RISK);
            }
            if (objStructureSearch.HOUSE_ID != "")
            {
                houseid = objStructureSearch.HOUSE_ID;
            }


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";

                    dt = service.GetDataTable(true, "PR_BUILDING_STRUCTURE_SEARCH", district, vdc, ward,instanceuniquesno, houseid,imei, mobile, householddsn, fullnameeng, fullnameloc, Grade, techsolution, emumenatorid, interviewdtfrom, interviewdtto, houseownercnt, nircd, houselegalowner, buildingcondition, groundsurfacecd, foundationtypecd, rccd, fccd, sccd, buildingpositioncd, buildingplancd, assessedareacd, reconstructionstarted, secondaryuse, houseagefrom, houseageto, geotechnicalrisk,legalowner_flag, SortBy, SortOrder, PageSize, PageIndex, exportexcel, lang, filterword, DBNull.Value);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
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
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["VDC_ENG"].ConvertToString() != "" ? dr["VDC_ENG"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_ENG"].ConvertToString() != "" ? dr["DISTRICT_ENG"].ConvertToString() : ""), (dr["VDC_LOC"].ConvertToString() != "" ? dr["VDC_LOC"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_LOC"].ConvertToString() != "" ? dr["DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;
        }
        public DataTable PopulateHouseDetail(string HouseOwnerID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_HOUSE_OWNER_DTL WHERE HOUSE_OWNER_ID = '" + HouseOwnerID + "'";
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
        public NHRSSOCIODEMO PupulateSocioDemo(String HouseholdID)
        {
            NHRSSOCIODEMO obj = new NHRSSOCIODEMO();
            DataTable dtbl = null;
            //DataTable dtblVDCSecretary = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = String.Format("select * from VW_HOUSEHOLD_MEMBER where HouseholdID ='" + HouseholdID + "'");
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
                if (dtbl.Rows.Count > 0)
                {



                    //obj.HOUSE_OWNER_ID = dtbl.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                    //obj.DEFINED_CD = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    //obj.ENUMERATOR_ID = dtbl.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                    ////obj.INTERVIEW_DT = dtbl.Rows[0]["INTERVIEW_DT"].ToDateTime();
                    //obj.INTERVIEW_DT_LOC = dtbl.Rows[0]["INTERVIEW_DT_LOC"].ConvertToString();
                    //obj.INTERVIEW_START = dtbl.Rows[0]["INTERVIEW_START"].ConvertToString();
                    //obj.INTERVIEW_ST_HH = dtbl.Rows[0]["INTERVIEW_ST_HH"].ConvertToString();
                    //obj.INTERVIEW_ST_MM = dtbl.Rows[0]["INTERVIEW_ST_MM"].ConvertToString();
                    //obj.INTERVIEW_ST_SS = dtbl.Rows[0]["INTERVIEW_ST_SS"].ConvertToString();
                    //obj.INTERVIEW_ST_MS = dtbl.Rows[0]["INTERVIEW_ST_MS"].ConvertToString();
                    //obj.INTERVIEW_END = dtbl.Rows[0]["INTERVIEW_END"].ConvertToString();
                    //obj.INTERVIEW_END_HH = dtbl.Rows[0]["INTERVIEW_END_HH"].ConvertToString();
                    //obj.INTERVIEW_END_MM = dtbl.Rows[0]["INTERVIEW_END_MM"].ConvertToString();
                    //obj.INTERVIEW_END_SS = dtbl.Rows[0]["INTERVIEW_END_SS"].ConvertToString();
                    //obj.INTERVIEW_END_MS = dtbl.Rows[0]["INTERVIEW_END_MS"].ConvertToString();
                    //obj.INTERVIEW_GMT = dtbl.Rows[0]["INTERVIEW_GMT"].ConvertToString();
                    //obj.COUNTRY_CD = dtbl.Rows[0]["COUNTRY_CD"].ToString();
                    //obj.REG_ST_CD = dtbl.Rows[0]["REG_ST_CD"].ToString();
                    //obj.ZONE_CD = dtbl.Rows[0]["ZONE_CD"].ToString();
                    //obj.DISTRICT_CD = dtbl.Rows[0]["DISTRICT_CD"].ToString();
                    //obj.VDC_MUN_CD = dtbl.Rows[0]["VDC_MUN_CD"].ToString();
                    //obj.WARD_NO = dtbl.Rows[0]["WARD_NO"].ToString();
                    //obj.ENUMERATION_AREA = dtbl.Rows[0]["ENUMERATION_AREA"].ConvertToString();
                    //obj.AREA_ENG = dtbl.Rows[0]["AREA_ENG"].ConvertToString();
                    //obj.AREA_LOC = dtbl.Rows[0]["AREA_LOC"].ConvertToString();
                    //obj.DISTRICT_ENG = dtbl.Rows[0]["DISTRICT_ENG"].ConvertToString();
                    //obj.DISTRICT_LOC = dtbl.Rows[0]["DISTRICT_LOC"].ConvertToString();
                    ////obj.COUNTRY_ENG = dtbl.Rows[0]["COUNTRY_ENG"].ConvertToString();
                    ////obj.COUNTRY_LOC = dtbl.Rows[0]["COUNTRY_LOC"].ConvertToString();
                    ////obj.REGION_LOC = dtbl.Rows[0]["REGION_LOC"].ConvertToString();
                    ////obj.ZONE_ENG = dtbl.Rows[0]["ZONE_ENG"].ConvertToString();
                    ////obj.ZONE_LOC = dtbl.Rows[0]["ZONE_LOC"].ConvertToString();
                    //obj.VDC_ENG = dtbl.Rows[0]["VDC_ENG"].ConvertToString();
                    //obj.VDC_LOC = dtbl.Rows[0]["VDC_LOC"].ConvertToString();
                    //obj.HOUSE_FAMILY_OWNER_CNT = dtbl.Rows[0]["HOUSE_FAMILY_OWNER_CNT"].ToString();
                    //obj.RESPONDENT_IS_HOUSE_OWNER = dtbl.Rows[0]["RESPONDENT_IS_HOUSE_OWNER"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "??") : Utils.ToggleLanguage("No", "????");
                    ////obj.RESPONDENT_GENDER = dtbl.Rows[0]["RESPONDENT_GENDER"].ToString();

                    //obj.NOT_INTERVIWING_REASON_CD = dtbl.Rows[0]["NOT_INTERVIWING_REASON_CD"].ToString();
                    //obj.NOT_INTERVIWING_REASON = dtbl.Rows[0]["NOT_INTERVIWING_REASON"].ConvertToString();
                    //obj.NOT_INTERVIWING_REASON_LOC = dtbl.Rows[0]["NOT_INTERVIWING_REASON_LOC"].ConvertToString();
                    //obj.ELECTIONCENTER_OHOUSE_CNT = dtbl.Rows[0]["ELECTIONCENTER_OHOUSE_CNT"].ToString();
                    //obj.NONELECTIONCENTER_FHOUSE_CNT = dtbl.Rows[0]["NONELECTIONCENTER_FHOUSE_CNT"].ToString();
                    //obj.NONRESID_NONDAMAGE_H_CNT = dtbl.Rows[0]["NONRESID_NONDAMAGE_H_CNT"].ToString();
                    //obj.NONRESID_PARTIAL_DAMAGE_H_CNT = dtbl.Rows[0]["NONRESID_PARTIAL_DAMAGE_H_CNT"].ToString();
                    //obj.NONRESID_FULL_DAMAGE_H_CNT = dtbl.Rows[0]["NONRESID_FULL_DAMAGE_H_CNT"].ToString();
                    //obj.SOCIAL_MOBILIZER_PRESENT_FLAG = dtbl.Rows[0]["SOCIAL_MOBILIZER_PRESENT_FLAG"].ConvertToString();
                    //obj.SOCIAL_MOBILIZER_NAME = dtbl.Rows[0]["SOCIAL_MOBILIZER_NAME"].ConvertToString();
                    //obj.SOCIAL_MOBILIZER_NAME_LOC = dtbl.Rows[0]["SOCIAL_MOBILIZER_NAME_LOC"].ConvertToString();
                    //obj.IMEI = dtbl.Rows[0]["IMEI"].ConvertToString();
                    //obj.IMSI = dtbl.Rows[0]["IMSI"].ConvertToString();
                    //obj.SIM_NUMBER = dtbl.Rows[0]["SIM_NUMBER"].ConvertToString();
                    //obj.MOBILE_NUMBER = dtbl.Rows[0]["MOBILE_NUMBER"].ConvertToString();
                    //obj.HOUSEOWNER_ACTIVE = dtbl.Rows[0]["HOUSEOWNER_ACTIVE"].ConvertToString();
                    //obj.REMARKS = dtbl.Rows[0]["REMARKS"].ConvertToString();
                    //obj.REMARKS_LOC = dtbl.Rows[0]["REMARKS_LOC"].ConvertToString();
                    //obj.FIRST_NAME_ENG = dtbl.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                    //obj.MIDDLE_NAME_ENG = dtbl.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                    //obj.LAST_NAME_ENG = dtbl.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    //obj.FULL_NAME_ENG = dtbl.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                    //obj.FIRST_NAME_LOC = dtbl.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                    //obj.MIDDLE_NAME_LOC = dtbl.Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                    //obj.LAST_NAME_LOC = dtbl.Rows[0]["LAST_NAME_LOC"].ConvertToString();
                    //obj.FULL_NAME_LOC = dtbl.Rows[0]["FULL_NAME_LOC"].ConvertToString();

                    ////obj.ENUMERATOR_ID = dtbl.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                    ////obj.DEFINED_CD = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    ////obj.DISTRICT_CD = dtbl.Rows[0]["DISTRICT_CD"].ConvertToString();
                    ////obj.VDC_MUN_CD = dtbl.Rows[0]["VDC_MUN_CD"].ConvertToString();
                    ////obj.WARD_NO = dtbl.Rows[0]["WARD_NO"].ConvertToString();
                    ////obj.AREA_ENG = dtbl.Rows[0]["AREA_ENG"].ConvertToString();
                    ////obj.HOUSE_FAMILY_OWNER_CNT = dtbl.Rows[0]["HOUSE_FAMILY_OWNER_CNT"].ConvertToString();
                    ////obj.FIRST_NAME_ENG = dtbl.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                    ////obj.MIDDLE_NAME_ENG = dtbl.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                    ////obj.LAST_NAME_ENG = dtbl.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    ////obj.OWNER_GENDER_CD = dtbl.Rows[0]["GENDER_CD"].ConvertToString();
                    ////obj.RESPONDENT_IS_HOUSE_OWNER = dtbl.Rows[0]["RESPONDENT_IS_HOUSE_OWNER"].ConvertToString();


                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;

        }

    }
}