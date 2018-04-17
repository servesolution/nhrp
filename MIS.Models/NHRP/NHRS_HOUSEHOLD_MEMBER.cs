using MIS.Models.NHRP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
   public class NHRS_HOUSEHOLD_MEMBER
    {
       public string HOUSE_ID { get; set; }
        public string HOUSEHOLD_ID { get; set; }  //1
        public string HOUSE_OWNER_ID { get; set; }
        public string HOUSEHOLD_DEFINED_CD { get; set; }  //2
        public string MEMBER_ID { get; set; }  //3
        public string MEMBER_DEFINED_CD { get; set; }  //4
        public string IS_RESPONDENT_HOUSEHEAD { get; set; }
        public string RESPONDENT_FIRST_NAME { get; set; }
        public string RESPONDENT_MIDDLE_NAME { get; set; }
        public string RESPONDENT_LAST_NAME { get; set; }
        public string RESPONDENT_FIRST_NAME_LOC { get; set; }
        public string RESPONDENT_MIDDLE_NAME_LOC { get; set; }
        public string RESPONDENT_LAST_NAME_LOC { get; set; }
        public string RESPONDENT_FULL_NAME_ENG { get; set; }
        public string RESPONDENT_FULL_NAME_LOC { get; set; }
        public string RESPONDENT_RELATION_WITH_HH { get; set; }
        public string RESPONDENT_RELATION_WITH_HH_ENG { get; set; }

        public string RESPONDENT_GENDER_CD { get; set; }
        public string FIRST_NAME_ENG { get; set; }  //5
        public string MIDDLE_NAME_ENG { get; set; }  //6
        public string LAST_NAME_ENG { get; set; }  //7
        public string FULL_NAME_ENG { get; set; }  //8
        public string FIRST_NAME_LOC { get; set; }  //9
        public string MIDDLE_NAME_LOC { get; set; }  //10
        public string LAST_NAME_LOC { get; set; }  //11
        public string FULL_NAME_LOC { get; set; }  //12
        public string PER_COUNTRY_CD { get; set; }  //13
        public string PER_REG_ST_CD { get; set; }  //14
        public string PER_ZONE_CD { get; set; }  //15
        public string PER_DISTRICT_CD { get; set; }  //16
        public string PER_VDC_MUN_CD { get; set; }  //17
        public string PER_WARD_NO { get; set; }  //18
        public string PER_AREA_ENG { get; set; }  //19
        public string PER_AREA_LOC { get; set; }  //20
        public string PER_DISTRICT_ENG { get; set; }
        public string PER_DISTRICT_LOC { get; set; }
        public string PER_COUNTRY_ENG { get; set; }
        public string PER_COUNTRY_LOC { get; set; }
        public string PER_REGION_ENG { get; set; }
        public string PER_REGION_LOC { get; set; }
        public string PER_ZONE_ENG { get; set; }
        public string PER_ZONE_LOC { get; set; }
        public string PER_VDC_ENG { get; set; }
        public string PER_VDC_LOC { get; set; }
        public string CUR_COUNTRY_CD { get; set; }  //21
        public string CUR_REG_ST_CD { get; set; }  //22
        public string CUR_ZONE_CD { get; set; }  //23
        public string CUR_DISTRICT_CD { get; set; }  //24
        public string CUR_VDC_MUN_CD { get; set; }  //25
        public string CUR_WARD_NO { get; set; }  //26
        public string CUR_AREA_ENG { get; set; }  //27
        public string CUR_AREA_LOC { get; set; }  //28
        public string CUR_DISTRICT_DESC_ENG { get; set; }
        public string CUR_DISTRICT_DESC_LOC { get; set; }
        public string CUR_COUNTRY_DESC_ENG { get; set; }
        public string CUR_COUNTRY_DESC_LOC { get; set; }
        public string CUR_REGION_ST_DESC_ENG { get; set; }
        public string CUR_REGION_ST_DESC_LOC { get; set; }
        public string CUR_ZONE_DESC_ENG { get; set; }
        public string CUR_ZONE_DESC_LOC { get; set; }
        public string CUR_VDC_DESC_ENG { get; set; }
        public string CUR_VDC_DESC_LOC { get; set; }
        public string MEMBER_PHOTO_ID { get; set; }  //29
        public string GENDER_CD { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }//30
        public string MARITAL_STATUS_CD { get; set; }
        public string MARITAL_STATUS_ENG { get; set; }
        public string BIRTH_YEAR { get; set; }  //32
        public string BIRTH_MONTH { get; set; }  //33
        public string BIRTH_DAY { get; set; }  //34
        public string BIRTH_YEAR_LOC { get; set; }  //35
        public string BIRTH_MONTH_LOC { get; set; }  //36
        public string BIRTH_DAY_LOC { get; set; }  //37
        public string BIRTH_DT { get; set; }  //38   2. Household Members' Detail:(1st list) table
        public string BIRTH_DT_LOC { get; set; }  //39  2. Household Members' Detail:(1st list) table
        public string AGE { get; set; }  //40       2. Household Members' Detail:(1st list) table
        public string CASTE_CD { get; set; }  //41
        public string CASTE_ENG { get; set; }
        public string CASTE_LOC { get; set; }
        public string RELIGION_CD { get; set; }  //42
        public string RELIGION_ENG { get; set; }
        public string RELIGION_LOC { get; set; }
        public string LITERATE { get; set; }  //43
        public string EDUCATION_CD { get; set; }  //44    2. Household Members' Detail:(1st list) table
        public string EDUCATION_ENG { get; set; }
        public string EDUCATION_LOC { get; set; }
        public string BIRTH_CERTIFICATE { get; set; }  //45     2. Household Members' Detail:(1st list) table
        public string BIRTH_CERTIFICATE_NO { get; set; }  //46
        public string BIRTH_CER_DISTRICT_CD { get; set; }  //47
        public string CTZ_ISSUE { get; set; }  //48
        public string CTZ_NO { get; set; }  //49
        public string CTZ_ISSUE_DISTRICT_CD { get; set; }
        public string CTZ_ISS_DISTRICT_ENG { get; set; }
        public string CTZ_ISSUE_YEAR { get; set; }  //51
        public string CTZ_ISSUE_MONTH { get; set; }  //52
        public string CTZ_ISSUE_DAY { get; set; }  //53
        public string CTZ_ISSUE_YEAR_LOC { get; set; }  //54
        public string CTZ_ISSUE_MONTH_LOC { get; set; }  //55
        public string CTZ_ISSUE_DAY_LOC { get; set; }  //56
        public DateTime? CTZ_ISSUE_DT { get; set; }  //57
        public string CTZ_ISSUE_DT_LOC { get; set; }  //58
        public string VOTER_ID { get; set; }  //59
        public string VOTERID_NO { get; set; }  //60
        public string VOTERID_DISTRICT_CD { get; set; }  //61
        public DateTime? VOTERID_ISSUE_DT { get; set; }  //62
        public string VOTERID_ISSUE_DT_LOC { get; set; }  //63
        public string NID_NO { get; set; }  //64
        public string NID_DISTRICT_CD { get; set; }  //65
        public DateTime? NID_ISSUE_DT { get; set; }  //66
        public string NID_ISSUE_DT_LOC { get; set; }  //67
        public string DISABILITY { get; set; }  //68      2. Household Members' Detail:(1st list) table
        public string TEL_NO { get; set; }  //69
        public string MOBILE_NO { get; set; }  //70
        public string FAX { get; set; }  //71
        public string EMAIL { get; set; }  //72
        public string URL { get; set; }  //73
        public string PO_BOX_NO { get; set; }  //74
        public string PASSPORT_NO { get; set; }  //75
        public string PASSPORT_ISSUE_DISTRICT { get; set; }  //76
        public string PRO_FUND_NO { get; set; }  //77
        public string CIT_NO { get; set; }  //78
        public string PAN_NO { get; set; }  //79
        public string DRIVING_LICENSE_NO { get; set; }
        public string DRIVING_LICENSE_ISSUE_DISTRICT_CD { get; set; }
        public string DEATH { get; set; }  //81
        public string MEMBER_REMARKS { get; set; }  //82
        public string MEMBER_REMARKS_LOC { get; set; }  //83
        public string HOUSEHOLD_HEAD { get; set; }  //84

        public string ELECTION_CENTER_NO { get; set; }
        public string NID_ISSUE { get; set; }  //85
        public string MEMBER_CNT { get; set; }  //86
        public string HOUSE_NO { get; set; }  //87
        public string HOUSEHOLD_TEL_NO { get; set; }  //88
        public string HOUSEHOLD_MOBILE_NO { get; set; }  //89
        public string HOUSEHOLD_FAX { get; set; }  //90
        public string HOUSEHOLD_EMAIL { get; set; }  //91
        public string HOUSEHOLD_URL { get; set; }  //92
        public string HOUSEHOLD_PO_BOX_NO { get; set; }  //93
        public string DEATH_IN_A_YEAR { get; set; }  //94
        public string DEATH_CNT { get; set; }
        public string CHILD_IN_SCHOOL { get; set; }  //95
        public string SOCIAL_ALLOWANCE { get; set; }
        public string SOCIAL_ALLOWANCE_NO { get; set; }
        public string SOCIAL_ALL_ISS_DATE { get; set; }
        public  string ALLOWANCE_DAY { get; set; }
        public string ALLOWANCE_MONTH{ get; set; }
        public string ALLOWANCE_YEARS { get; set; }
        public string ALLOWANCE_TYPE_CD { get; set; }
        public string ALLOWANCE_TYPE_ENG { get; set; }
        public string ALLOWANCE_TYPE_LOC { get; set; }
        public string SOCIAL_ALL_ISS_DISTRICT { get; set; }
        public string IDENTIFICATION_TYPE_CD { get; set; }
        public string IDENTIFICATION_TYPE_ENG { get; set; }
        public string IDENTIFICATION_TYPE_LOC { get; set; }
        public string IDENTIFICATION_OTHERS { get; set; }
        public string IDENTIFICATION_OTHERS_LOC { get; set; }
        public string FOREIGN_HOUSEHEAD_COUNTRY_ENG { get; set; }
        public string FOREIGN_HOUSEHEAD_COUNTRY_LOC { get; set; }
        public string BANK_ACCOUNT_FLAG { get; set; }
        public string BANK_CD { get; set; }
        public string BANK_NAME_ENG { get; set; }
        public string BANK_NAME_LOC { get; set; }
        public string BANK_BRANCH_CD { get; set; }
        public string BANK_BRANCH_NAME_ENG { get; set; }
        public string BANK_BRANCH_NAME_LOC { get; set; }
        public string HH_MEMBER_BANK_ACC_NO { get; set; }
        public string HOUSEHOLD_REMARKS { get; set; }  //97
        public string HOUSEHOLD_REMARKS_LOC { get; set; }  //98
        public string MONTHLY_INCOME_CD { get; set; }
        public string MONTHLY_INCOME_ENG { get; set; }
        public string MONTHLY_INCOME_LOC { get; set; }

        public string SHELTER_SINCE_QUAKE_CD { get; set; }
        public string SHELTER_SINCE_QUAKE_ENG { get; set; }
        public string SHELTER_SINCE_QUAKE_LOC { get; set; }
        public string SHELTER_BEFORE_QUAKE_CD { get; set; }
        public string SHELTER_BEFORE_QUAKE_ENG { get; set; }
        public string SHELTER_BEFORE_QUAKE_LOC { get; set; }
        public string CURRENT_SHELTER_CD { get; set; }
        public string CURRENT_SHELTER_ENG { get; set; }
        public string CURRENT_SHELTER_LOC { get; set; }
        public string EQ_VICTIM_IDENTITY_CARD { get; set; }
        public string EQ_VICTIM_IDENTITY_CARD_CD { get; set; }
        public string EQ_VICTIM_IDENTITY_CARD_ENG { get; set; }
        public string EQ_VICTIM_IDENTITY_CARD_LOC { get; set; }
        public string EQ_VICTIM_IDENTITY_CARD_NO { get; set; }
        public string EDIT_MODE { get; set; }
        public string BUILDING_STRUCTURE_NO { get;set; }
        public string CHILD_LEFT_VACINATION { get; set; }
        public string CHILD_LEFT_VACINATION_CNT { get; set; }
        public string LEFT_CHANGE_OCCUPANY { get; set; }
        public string LEFT_CHANGE_OCCUPANY_CNT { get; set; }
        public string HOUSEHOLD_ACTIVE { get; set; }
        public string PREGNANT_REGULAR_CHECKUP { get; set; }
        public string PREGNANT_REGULAR_CHECKUP_CNT { get; set; }
        public string STUDENT_SCHOOL_LEFT { get; set; }
        public string STUDENT_SCHOOL_LEFT_CNT { get; set; }
        public string RELATION_ENG { get; set; }
        public string RELATION_CD { get; set; }
        public string PRESENCE_STATUS { get; set; }
        public string PRESENCE_STATUS_CD { get; set; }
        public string HANDI_COLOR_ENG { get; set; }
        public string HANDI_COLOR_CD { get; set; }
        public string HUMAN_DESTROY_FLAG { get; set; }
        public string HUMAN_DESTROY_CNT { get; set; }


        public string WATER_SOURCE_CD { get; set; }
        public string WATER_SOURCE_ENG { get; set; }
        public string WATER_SOURCE_LOC { get; set; }
        public string WATER_SOURCE_BEFORE { get; set; }
        public string WATER_SOURCE_AFTER { get; set; }

        public string FUEL_SOURCE_CD { get; set; }
        public string FUEL_SOURCE_ENG { get; set; }
        public string FUEL_SOURCE_LOC { get; set; }
        public string FUEL_SOURCE_BEFORE { get; set; }
        public string FUEL_SOURCE_AFTER { get; set; }

        public string LIGHT_SOURCE_CD { get; set; }
        public string LIGHT_SOURCE_ENG { get; set; }
        public string LIGHT_SOURCE_LOC { get; set; }
        public string LIGHT_SOURCE_BEFORE { get; set; }
        public string LIGHT_SOURCE_AFTER { get; set; }

        public string TOILET_TYPE_CD { get; set; }
        public string TOILET_TYPE_ENG { get; set; }
        public string TOILET_TYPE_LOC { get; set; }
        public string TOILET_TYPE_BEFORE { get; set; }
        public string TOILET_TYPE_AFTER { get; set; }

        public List<VW_MEMBER_FAMILY_DTL> MemberDetailInfoList { get; set; }

        public List<NHRS_HOUSEHOLD_MEMBER> MemberDetailInfo { get; set; }

        public List<VW_MEMBER_DEATH_DTL> MemberDeathInfoList { get; set; }

       public List<VW_MEMBER_HUMAN_DISTROY_DTL> HumanDestroyInfoList { get; set; }
       public List<MIG_MIS_HOUSEHOLD_INDICATOR> HouseholdIndicatorName { get; set; }
       public List<MIG_MIS_HOUSEHOLD_INDICATOR> HouseholdIndicatorDetail { get; set; }
       public List<NHRS_EQ_RELIEF_MONEY> EarthquakeReliefMoneyName { get; set; }
       public List<NHRS_EQ_RELIEF_MONEY> EarthquakeReliefMoneyDetail { get; set; }

             
    }
}
