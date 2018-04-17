using MIS.Models.NHRP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class NHRPResurveyDetail 
    {

        public string HOUSE_OWNER_ID { get; set; }//1
        public string DEFINED_CD { get; set; }//2
        public decimal? DISTRICT_CD { get; set; }
        public decimal? VDC_MUN_CD { get; set; }
        public decimal? WARD_NO { get; set; }
        public string ENUMERATION_AREA { get; set; }
        public string HOUSE_NO { get; set; }
        public string AREA_ENG { get; set; }
        public string AREA_LOC { get; set; }
        public string ENUMERATOR_ID { get; set; }//3
        public decimal? HOUSE_OWNER_CNT { get; set; }

        public string REASON_FOR_GRIEVANCE { get; set; }
        public string OLD_NISSA_AVAILABLE { get; set; }
        public string OLD_NISSA_PHOTO { get; set; }
        public string REASON_FOR_LEFTOVER { get; set; }
        public string OTHER_REASON_FOR_LEFTOVER { get; set; }
        public string OLD_VDC { get; set; }
        public string OLD_WARD { get; set; }
       
        public string DISTRICT_NAME { get; set; }
        public string VDC_NAME { get; set; }
        public string OLD_VDC_NAME { get; set; }
        public string INTERVIEW_DT { get; set; }//4
        public string INTERVIEW_DT_LOC { get; set; }//5                    
        public string INTERVIEW_START { get; set; }//6
        public string INTERVIEW_ST_HH { get; set; }//7
        public string INTERVIEW_ST_MM { get; set; }//8
        public string INTERVIEW_ST_SS { get; set; }//9
        public string INTERVIEW_ST_MS { get; set; }//10
        public string INTERVIEW_END { get; set; }//11
        public string INTERVIEW_END_HH { get; set; }//12
        public string INTERVIEW_END_MM { get; set; }//13
        public string INTERVIEW_END_SS { get; set; }//14
        public string INTERVIEW_END_MS { get; set; }
        public string INTERVIEW_GMT { get; set; }



        public string RESPONDENT_IS_HOUSE_OWNER { get; set; }
        public string NOT_INTERVIWING_REASON_CD_Defined { get; set; }
        public decimal? NOT_INTERVIWING_REASON_CD { get; set; }
        public string NOT_INTERVIWING_REASON { get; set; }
        public string NOT_INTERVIWING_REASON_LOC { get; set; }
        public decimal? ELECTIONCENTER_OHOUSE_CNT { get; set; }
        public decimal? NONELECTIONCENTER_FHOUSE_CNT { get; set; }
        public decimal? NONRESID_NONDAMAGE_H_CNT { get; set; }
        public decimal? NONRESID_PARTIAL_DAMAGE_H_CNT { get; set; }
        public decimal? NONRESID_FULL_DAMAGE_H_CNT { get; set; }
        public string SOCIAL_MOBILIZER_PRESENT_FLAG { get; set; }
        public string SOCIAL_MOBILIZER_NAME { get; set; }
        public string SOCIAL_MOBILIZER_NAME_LOC { get; set; }
        public string IMEI { get; set; }
        public string IMSI { get; set; }
        public string SIM_NUMBER { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string HOUSEOWNER_ACTIVE { get; set; }
        public string REMARKS { get; set; }
        public string REMARKS_LOC { get; set; }
        public string FIRST_NAME_ENG { get; set; }
        public string MIDDLE_NAME_ENG { get; set; }
        public string LAST_NAME_ENG { get; set; }
        public string FULL_NAME_ENG { get; set; }
        public string FIRST_NAME_LOC { get; set; }
        public string MIDDLE_NAME_LOC { get; set; }
        public string LAST_NAME_LOC { get; set; }
        public string FULL_NAME_LOC { get; set; }
        public string MEMBER_PHOTO_ID { get; set; }
        public decimal? GENDER_CD { get; set; }
        public decimal? MARITAL_STATUS_CD { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }
        public string MARITAL_STATUS_ENG { get; set; }
        public string MARITAL_STATUS_LOC { get; set; }
        public string HOUSEHOLD_HEAD { get; set; }
        public decimal? RESPONDENT_GENDER_CD { get; set; }
        public string RESPONDENT_GENDER { get; set; }

       
        public String editMode { get; set; }
        public List<HouseOwnerNameModel> HouseOwnerNamesList { get; set; }

        public List<GrievanceResurveyModel> GrievanceResurveyList { get; set; }

        public List<VW_HOUSE_RESPONDENT_DETAIL> lstRespondentDetail { get; set; }

        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail { get; set; }
        public List<VW_DamageExtentHome> lstDamageExtentHome { get; set; }
        public List<OtherHousesDamagedModel> OtherHousesDamagedList { get; set; }

        private List<VW_HOUSE_BUILDING_DTL> objList = new List<VW_HOUSE_BUILDING_DTL>();
        public List<VW_HOUSE_BUILDING_DTL> HOUSE_BUILDING_DTL_List { get { return objList; } set { objList = null; } }


    }
}
