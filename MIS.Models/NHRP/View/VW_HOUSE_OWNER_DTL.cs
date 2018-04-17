using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.CaseGrievance;

namespace MIS.Models.NHRP.View
{
    public class VW_HOUSE_OWNER_DTL
    {
        public string HOUSE_OWNER_ID { get; set; }//1
        public string DEFINED_CD { get; set; }//2
        public string ENUMERATOR_ID { get; set; }//3
        public DateTime? INTERVIEW_DT { get; set; }//4
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
        public decimal? COUNTRY_CD { get; set; }
        public decimal? REG_ST_CD { get; set; }
        public decimal? ZONE_CD { get; set; }
        public decimal? DISTRICT_CD { get; set; }
        public decimal? VDC_MUN_CD { get; set; }
        public decimal? WARD_NO { get; set; }
        public string ENUMERATION_AREA { get; set; }
        public string AREA_ENG { get; set; }
        public string AREA_LOC { get; set; }
        public string DISTRICT_ENG { get; set; }
        public string DISTRICT_LOC { get; set; }
        public string COUNTRY_ENG { get; set; }
        public string COUNTRY_LOC { get; set; }
        public string REGION_ENG { get; set; }
        public string REGION_LOC { get; set; }
        public string ZONE_ENG { get; set; }
        public string ZONE_LOC { get; set; }
        public string VDC_ENG { get; set; }
        public string VDC_LOC { get; set; }
        public decimal? HOUSE_FAMILY_OWNER_CNT { get; set; }
        public string RESPONDENT_IS_HOUSE_OWNER { get; set; }
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
        public string HOUSE_SNO { get; set; }
        public string ImagePath { get; set; }
        public string CASE_REGISTRATION_ID { get; set; }
        public string TARGET_LOT { get; set; }
        public string NRA_DEFINED_CD { get; set; }
        public string RETRO_PA { get; set; }
        public string GRIEVANCE_PA { get; set; }

        //public string PhotosofHouse { get; set; }
        //public string PhotosofFrontdirections { get; set; }
        //public string PhotosofBackdirections { get; set; }
        //public string PhotosofRightdirections { get; set; }
        //public string PhotosofLeftdirections { get; set; }
        //public string Photosof4directions { get; set; }
        //public string Photosofinternaldamageofhouse { get; set; }
        //public string Fullydamagedhouseslocationsphoto { get; set; }
        public string KLLDistrictCode { get; set; }
        public string SubmissionDate { get; set; }


        public List<GrvDtlToHouseViewModelclass> grievDtlList { get; set; }
        public List<OtherLiveableHouseDtlModelClass> LiveablehouseDtlList { get; set; }


        public List<HouseOwnerNameModel> HouseOwnerNamesList { get; set; }
        public List<VW_HOUSE_RESPONDENT_DETAIL> lstRespondentDetail { get; set; }
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail { get; set; }
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureName { get; set; }
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalName { get; set; }
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalDetail { get; set; }
        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseName { get; set; }
        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseDetail { get; set; }
        public List<VW_DamageExtentHome> lstDamageExtentHome { get; set; }
        public List<VW_DamageExtentHome> lstDamageExtentHomeName { get; set; }
        public List<VW_DamageExtentHome> lstDamageLevelName { get; set; }
        public List<OtherHousesDamagedModel> OtherHousesDamagedList { get; set; }
        public List<CaseGrievanceHandled> GrievanceHandled { get; set; }
        public List<GrievanceMemberDetail> GrievanceMemberList { get; set; }
        public List<GrievantDetail> GrievantDetail { get; set; }
        

        private List<VW_HOUSE_BUILDING_DTL> objList = new List<VW_HOUSE_BUILDING_DTL>();
        public List<VW_HOUSE_BUILDING_DTL> HOUSE_BUILDING_DTL_List { get { return objList; } set { objList = null; } }

        public List<GrievanceHandling> HouseOwnerDetailList { get; set; }
        public List<HouseInOtherPlace> OtherHouseListDetail { get; set; }

        public List<GrievanceOwner> GrievanceOWner { get; set; }


    }
}
