using MIS.Models.NHRP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
   public class NHRSSOCIODEMO
    {
        public string HOUSEHOLD_ID { get; set; }  //1
        public string HOUSEHOLD_DEFINED_CD { get; set; }  //2
        public string MEMBER_ID { get; set; }  //3
        public string MEMBER_DEFINED_CD { get; set; }  //4
        public string FIRST_NAME_ENG { get; set; }  //5
        public string MIDDLE_NAME_ENG { get; set; }  //6
        public string LAST_NAME_ENG { get; set; }  //7
        public string FULL_NAME_ENG { get; set; }  //8
        public string FIRST_NAME_LOC { get; set; }  //9
        public string MIDDLE_NAME_LOC { get; set; }  //10
        public string LAST_NAME_LOC { get; set; }  //11
        public string FULL_NAME_LOC { get; set; }  //12
        public decimal PER_COUNTRY_CD { get; set; }  //13
        public decimal PER_REG_ST_CD { get; set; }  //14
        public decimal PER_ZONE_CD { get; set; }  //15
        public decimal PER_DISTRICT_CD { get; set; }  //16
        public decimal PER_VDC_MUN_CD { get; set; }  //17
        public decimal PER_WARD_NO { get; set; }  //18
        public string PER_AREA_ENG { get; set; }  //19
        public string PER_AREA_LOC { get; set; }  //20
        public decimal CUR_COUNTRY_CD { get; set; }  //21
        public decimal CUR_REG_ST_CD { get; set; }  //22
        public decimal CUR_ZONE_CD { get; set; }  //23
        public decimal CUR_DISTRICT_CD { get; set; }  //24
        public decimal CUR_VDC_MUN_CD { get; set; }  //25
        public decimal CUR_WARD_NO { get; set; }  //26
        public string CUR_AREA_ENG { get; set; }  //27
        public string CUR_AREA_LOC { get; set; }  //28
        public string MEMBER_PHOTO_ID { get; set; }  //29
        public decimal GENDER_CD { get; set; }  //30
        public string GenderHouseHoldHead { get; set; }
        public decimal MARITAL_STATUS_CD { get; set; }  //31  2. Household Members' Detail:(1st list) table
        public decimal? BIRTH_YEAR { get; set; }  //32
        public decimal? BIRTH_MONTH { get; set; }  //33
        public decimal? BIRTH_DAY { get; set; }  //34
        public string BIRTH_YEAR_LOC { get; set; }  //35
        public string BIRTH_MONTH_LOC { get; set; }  //36
        public string BIRTH_DAY_LOC { get; set; }  //37
        public DateTime BIRTH_DT { get; set; }  //38   2. Household Members' Detail:(1st list) table
        public string BIRTH_DT_LOC { get; set; }  //39  2. Household Members' Detail:(1st list) table
        public string AGE { get; set; }  //40       2. Household Members' Detail:(1st list) table
        public decimal? CASTE_CD { get; set; }  //41
        public string CasteEthnicityHouseHoldHead { get; set; }
        public decimal? RELIGION_CD { get; set; }  //42
        public string LITERATE { get; set; }  //43
        public decimal? EDUCATION_CD { get; set; }  //44    2. Household Members' Detail:(1st list) table
        public string EduHouseHoldHead { get; set; }
        public string BIRTH_CERTIFICATE { get; set; }  //45     2. Household Members' Detail:(1st list) table
        public string BIRTH_CERTIFICATE_NO { get; set; }  //46
        public decimal? BIRTH_CER_DISTRICT_CD { get; set; }  //47
        public string CTZ_ISSUE { get; set; }  //48
        public string CTZ_NO { get; set; }  //49
        public decimal CTZ_ISSUE_DISTRICT_CD { get; set; }  //50
        public decimal CTZ_ISSUE_YEAR { get; set; }  //51
        public decimal CTZ_ISSUE_MONTH { get; set; }  //52
        public decimal CTZ_ISSUE_DAY { get; set; }  //53
        public string CTZ_ISSUE_YEAR_LOC { get; set; }  //54
        public string CTZ_ISSUE_MONTH_LOC { get; set; }  //55
        public string CTZ_ISSUE_DAY_LOC { get; set; }  //56
        public DateTime CTZ_ISSUE_DT { get; set; }  //57
        public string CTZ_ISSUE_DT_LOC { get; set; }  //58
        public string VOTER_ID { get; set; }  //59
        public string VOTERID_NO { get; set; }  //60
        public decimal VOTERID_DISTRICT_CD { get; set; }  //61
        public DateTime VOTERID_ISSUE_DT { get; set; }  //62
        public string VOTERID_ISSUE_DT_LOC { get; set; }  //63
        public string NID_NO { get; set; }  //64
        public decimal NID_DISTRICT_CD { get; set; }  //65
        public DateTime NID_ISSUE_DT { get; set; }  //66
        public string NID_ISSUE_DT_LOC { get; set; }  //67
        public string DISABILITY { get; set; }  //68      2. Household Members' Detail:(1st list) table
        public string TEL_NO { get; set; }  //69
        public string MOBILE_NO { get; set; }  //70
        public string FAX { get; set; }  //71
        public string EMAIL { get; set; }  //72
        public string URL { get; set; }  //73
        public string PO_BOX_NO { get; set; }  //74
        public string PASSPORT_NO { get; set; }  //75
        public decimal PASSPORT_ISSUE_DISTRICT { get; set; }  //76
        public string PRO_FUND_NO { get; set; }  //77
        public string CIT_NO { get; set; }  //78
        public string PAN_NO { get; set; }  //79
        public string DRIVING_LICENSE_NO { get; set; }  //80
        public string DEATH { get; set; }  //81
        public string MEMBER_REMARKS { get; set; }  //82
        public string MEMBER_REMARKS_LOC { get; set; }  //83
        public string HOUSEHOLD_HEAD { get; set; }  //84
        public string NID_ISSUE { get; set; }  //85
        public decimal? MEMBER_CNT { get; set; }  //86
        public string HOUSE_NO { get; set; }  //87
        public string HOUSEHOLD_TEL_NO { get; set; }  //88
        public string HOUSEHOLD_MOBILE_NO { get; set; }  //89
        public string HOUSEHOLD_FAX { get; set; }  //90
        public string HOUSEHOLD_EMAIL { get; set; }  //91
        public string HOUSEHOLD_URL { get; set; }  //92
        public string HOUSEHOLD_PO_BOX_NO { get; set; }  //93
        public string DEATH_IN_A_YEAR { get; set; }  //94
        public string CHILD_IN_SCHOOL { get; set; }  //95
        public string SOCIAL_ALLOWANCE { get; set; }  //96    2. Household Members' Detail:(1st list) table
        public string HOUSEHOLD_REMARKS { get; set; }  //97
        public string HOUSEHOLD_REMARKS_LOC { get; set; }  //98
        //public List<FamilyMemberDetailsModel> FamilyMembersDetailsList { get; set; }

    }
}
