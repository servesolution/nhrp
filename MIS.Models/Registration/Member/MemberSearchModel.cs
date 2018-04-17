using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration
{
    public class MemberSearchModel
    {
        #region Properties
        public string MEMBER_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string HOUSEHOLD_FORM_NO { get; set; }
        public string FIRST_NAME_ENG { get; set; }
        public string MIDDLE_NAME_ENG { get; set; }
        public string LAST_NAME_ENG { get; set; }
        public string FULL_NAME_ENG { get; set; }
        public string FIRST_NAME_LOC { get; set; }
        public string MIDDLE_NAME_LOC { get; set; }
        public string LAST_NAME_LOC { get; set; }
        public string FULL_NAME_LOC { get; set; }
        public string FATHER_FNAME_ENG { get; set; }
        public string FATHER_MNAME_ENG { get; set; }
        public string FATHER_LNAME_ENG { get; set; }
        public string FATHER_FULL_NAME_ENG { get; set; }
        public string FATHER_FNAME_LOC { get; set; }
        public string FATHER_MNAME_LOC { get; set; }
        public string FATHER_LNAME_LOC { get; set; }
        public string FATHER_FULLNAME_LOC { get; set; }
        public string MOTHER_FNAME_ENG { get; set; }
        public string MOTHER_MNAME_ENG { get; set; }
        public string MOTHER_LNAME_ENG { get; set; }
        public string MOTHER_FULL_NAME_ENG { get; set; }
        public string MOTHER_FNAME_LOC { get; set; }
        public string MOTHER_MNAME_LOC { get; set; }
        public string MOTHER_LNAME_LOC { get; set; }
        public string MOTHER_FULLNAME_LOC { get; set; }
        public string GFATHER_FNAME_ENG { get; set; }
        public string GFATHER_MNAME_ENG { get; set; }
        public string GFATHER_LNAME_ENG { get; set; }
        public string GFATHER_FULL_NAME_ENG { get; set; }
        public string GFATHER_FNAME_LOC { get; set; }
        public string GFATHER_MNAME_LOC { get; set; }
        public string GFATHER_LNAME_LOC { get; set; }
        public string GFATHER_FULLNAME_LOC { get; set; }
        public string SPOUSE_FNAME_ENG { get; set; }
        public string SPOUSE_MNAME_ENG { get; set; }
        public string SPOUSE_LNAME_ENG { get; set; }
        public string SPOUSE_FULL_NAME_ENG { get; set; }
        public string SPOUSE_FNAME_LOC { get; set; }
        public string SPOUSE_MNAME_LOC { get; set; }
        public string SPOUSE_LNAME_LOC { get; set; }
        public string SPOUSE_FULLNAME_LOC { get; set; }
        public string GENDER_CD { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }
        public string MARITAL_STATUS_CD { get; set; }
        public string DOB_TO { get; set; }
        public string DOB_FROM { get; set; }
        public string DOB_TO_LOC { get; set; }
        public string DOB_FROM_LOC { get; set; }
        public string BIRTH_YEAR { get; set; }
        public string BIRTH_MONTH { get; set; }
        public string BIRTH_DAY { get; set; }
        public string BIRTH_YEAR_LOC { get; set; }
        public string BIRTH_MONTH_LOC { get; set; }
        public string BIRTH_DAY_LOC { get; set; }
        public string BIRTH_DT { get; set; }
        public string BIRTH_DT_LOC { get; set; }
        public string AGE { get; set; }
        public string AGE_FROM { get; set; }
        public string AGE_TO { get; set; }
        public string CASTE_CD { get; set; }
        public string RELIGION_CD { get; set; }
        public string LITERATE_CD { get; set; }
        public string EDUCATION_CD { get; set; }
        public string BIRTH_CERTIFICATE { get; set; }
        public string BIRTH_CERTIFICATE_NO { get; set; }
        public string BIRTH_CER_DISTRICT { get; set; }
        public string CTZ_ISSUE { get; set; }
        public string CTZ_NO { get; set; }
        public string CTZ_ISSUE_DISTRICT { get; set; }
        public string CTZ_ISSUE_YEAR { get; set; }
        public string CTZ_ISSUE_MONTH { get; set; }
        public string CTZ_ISSUE_DAY { get; set; }
        public string CTZ_ISSUE_YEAR_LOC { get; set; }
        public string CTZ_ISSUE_MONTH_LOC { get; set; }
        public string CTZ_ISSUE_DAY_LOC { get; set; }
        public string CTZ_ISSUE_DT { get; set; }
        public string CTZ_ISSUE_DT_LOC { get; set; }
        public string VOTER_ID { get; set; }
        public string VOTERID_NO { get; set; }
        public string VOTERID_DISTRICT { get; set; }
        public string VOTERID_ISSUE_DT { get; set; }
        public string VOTERID_ISSUE_DT_LOC { get; set; }
        public string NID_NO { get; set; }
        public string NID_DISTRICT { get; set; }
        public string NID_ISSUE_DT { get; set; }
        public string NID_ISSUE_DT_LOC { get; set; }
        public string DISTRICT_ENG { get; set; }
        public string COUNTRY_ENG { get; set; }
        public string REGION_ST_ENG { get; set; }
        public string ZONE_ENG { get; set; }
        public string VDC_ENG { get; set; }
        public string MARI_ENG { get; set; }
        public string PER_DISTRICT_CD { get; set; }
        public string PER_VDC_CD { get; set; }
        public string PER_WARD_NO { get; set; }
        public string PER_AREA_ENG { get; set; }
        public string PER_AREA_LOC { get; set; }
        public string CUR_DISTRICT_ENG { get; set; }
        public string CUR_COUNTRY_ENG { get; set; }
        public string CUR_REGION_ST_ENG { get; set; }
        public string CUR_ZONE_ENG { get; set; }
        public string CUR_VDC_ENG { get; set; }
        public string CUR_WARD_NO { get; set; }
        public string CUR_AREA_ENG { get; set; }
        public string CUR_AREA_LOC { get; set; }
        public string TEL_NO { get; set; }
        public string MOBILE_NO { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string URL { get; set; }
        public string PO_BOX_NO { get; set; }
        public string PASSPORT_NO { get; set; }
        public string PASSPORT_ISSUE_DISTRICT { get; set; }
        public string PRO_FUND_NO { get; set; }
        public string CIT_NO { get; set; }
        public string PAN_NO { get; set; }
        public string DRIVING_LICENSE_NO { get; set; }
        public string DEATH { get; set; }
        public string PER_ADD_MIGRATE { get; set; }
        public string CUR_ADD_MIGRATE { get; set; }
        public string REMARKS { get; set; }
        public string REMARKS_LOC { get; set; }
        public string HOUSEHOLD_ID { get; set; }
        public string HOUSEHOLD_DEFINED_CD { get; set; }
        public string HOUSEHOLD_HEAD { get; set; }
        public string NID_ISSUE { get; set; }
        public string EXTRA_V { get; set; }
        public string MST_APPROVED { get; set; }
        public string APPROVED_BY { get; set; }
        public string APPROVED_BY_LOC { get; set; }
        public string MST_APPROVED_DT { get; set; }
        public string MST_APPROVED_DT_LOC { get; set; }
        public string HOUSEHOLD_ID_FROM { get; set; }
        public string HOUSEHOLD_ID_TO { get; set; }
        public string SEARCH_FROM_HOUSEHOLD { get; set; }
        public string IS_HOUSEHOLD_HEAD { get; set; }
        public string HOUSEHOLD_FORM_NO_FROM { get; set; }
        public string HOUSEHOLD_FORM_NO_TO { get; set; }
        public string Caste_Group_Cd { get; set; }
        public string CtzCertFlag { get; set; }
        public string BirthCertFlag { get; set; }
        public string VIDCertFlag { get; set; }
        public string NIDCertFlag { get; set; }
        public string MarriageCertFlag { get; set; }
        public string DeathCertFlag { get; set; }
        public string DivorceCertFlag { get; set; }
        public string SocialSecurityAllowanceFlag { get; set; }
        public string ReadWriteFlag { get; set; }
        public string WidowCertFlag { get; set; }
        public static List<string> DropdownKeys()
        {
            return new List<string> { 
                    "ddl_Cur_Districts", "ddl_Per_Districts", "ddl_Ctz_Districts",
                    "ddl_Cur_VDCMun", "ddl_Per_VDCMun", "ddl_Ctz_VDCMun", 
                    "ddl_Cur_Ward", "ddl_Per_Ward", "ddl_Ctz_Ward", 
                    "ddl_Gender", "ddl_MaritalStatus", "ddl_Caste", 
                    "ddl_Religion", "ddl_Education","ddl_Caste_Group","CtzCertFlag","BirthCertFlag","VIDCertFlag","NIDCertFlag",
                    "MarriageCertFlag","DeathCertFlag","DivorceCertFlag","SocialSecurityAllowanceFlag","ReadWriteFlag","WidowCertFlag"
                };
        }
        private static Dictionary<string, string> UI_Mappings = new Dictionary<string, string>() { 
            {"DEFINED_CD","MEMBER ID"}        
        };
        public string GetDisplayName(string propertyName)
        {
            if (UI_Mappings.ContainsKey(propertyName.ToUpper()))
                return UI_Mappings[propertyName.ToUpper()];
            else
                return propertyName.ToUpper().Replace("_", " ");
        }


        #endregion




    }
}
