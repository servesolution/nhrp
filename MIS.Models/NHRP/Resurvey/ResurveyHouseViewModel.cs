using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Resurvey
{
    public class ResurveyHouseViewModel
    {
        public string HouseOwnerId { get; set; }
        public string OwnerCount { get; set; }
        public string SurveyDate { get; set; }                 //---
        public string SurveyorCd { get; set; }                 //
        public string SurveyorDistrict { get; set; }          //---
        public string SurveyorDistrictCD { get; set; }       //---
        public string  GID_Status { get; set; }
        public string OwnDistrictCd { get; set; }           //
        public string OwnDistrictEng { get; set; }          //
        public string OwnDistrictLoc { get; set; }          //
        public string CurrentVdcMun { get; set; }           //
        public string CurrentVdcMunLoc { get; set; }        //
        public string CurrentVdcMunCD { get; set; }         //
        public string CurrentVdcMunDefCD { get; set; }         //
        public string CurrentWard { get; set; }             //
        public string CurrentTole { get; set; }             //
        public string Previous_Damage_Grade { get; set; }
        public string SurveyType { get; set; }
        public string Super_Structure { get; set; }
        public string OldVdcMun { get; set; }               //
        public string OldVdcMunCD { get; set; }             //
        public string OldVdcMunCDLoc { get; set; }          //
        public string OldWard { get; set; }                 //
        public string OldTole { get; set; }                 //
        public string NewDamageGrade { get; set; }
        public string InstanceUniqueSno { get; set; }      //-
        public string HouseSno { get; set; }              //
        public string GID { get; set; }                    //
        public string Nra_defined_CD { get; set; }
        public string Old_InstanceUniqueSno { get; set; }
        public string owner_sno { get; set; }
        public string GrievantFullName { get; set; }
        public string GrievanvtFirstNameEng { get; set; }
        public string GrievanvtMiddleNameEng { get; set; }
        public string GrievanvtLastNameEng { get; set; }
        public string GrievanvtFirstNameLoc { get; set; }
        public string GrievanvtMiddleNameLoc { get; set; }
        public string GrievanvtLastNameLoc { get; set; }
        public string GrievanvtGenderEng { get; set; }
        public string GrievanvtGenderLoc { get; set; }
        public string Previous_Recommendation { get; set; }
        public string Current_Recommendation { get; set; }
        public string NumberOfOwnerFamily { get; set; }
        public string OwnerFirstNameEng { get; set; }
        public string OwnerMiddleNameEng { get; set; }
        public string OwnerLastNameEng { get; set; }
        public string OwnerFirstNameLoc { get; set; }
        public string OwnerMiddleNameLoc { get; set; }
        public string OwnerLastNameLoc { get; set; }
        public string OwnerFullNameLoc { get; set; }
        public string OwnerFullNameEng { get; set; }
        public string OwnerGenderEng { get; set; }
        public string OwnerGenderLoc { get; set; }
        public string IsRespondantOwner { get; set; }
        public string Eng_Verification_Status { get; set; }
        public string  remarks { get; set; }
        public string RespondantFirstNameEng { get; set; }
        public string RespondantMiddleNameEng { get; set; }
        public string RespondantLastNameEng { get; set; }
        public string RespondantFirstNameLoc { get; set; }
        public string RespondantMiddleNameLoc { get; set; }
        public string RespondantLastNameLoc { get; set; }
        public string RespondantFullNameLoc { get; set; }
        public string RespondantFullNameEng { get; set; }
        public string RespondantGenderEng { get; set; }
        public string RespondantGenderLoc { get; set; }
        public string ResRelationToOwnerEng { get; set; }
        public string ResRelationToOwnerLoc { get; set; }
        public string RespondantOwnerPhotoUrl { get; set; }

        public string UseableHouseOwnedByOwner { get; set; }
        public string HasOtherHouse { get; set; }
        public string OwnerSignedSurveyDate { get; set; }

        public string OwnerFatherFullNameEng { get; set; }
        public string OwnerFatherFullNameLoc { get; set; }
        public string OwnerGFatherFullNameLoc { get; set; }
        public string OwnerGFatherFullNameEng { get; set; }
        public string MunRepreFullNameLoc { get; set; }
        public string MunRepreFullNameEng { get; set; }
        public string WardFullNameLoc { get; set; }
        public string WardFullNameEng { get; set; }

        public string SubmissionDate { get; set; }
        public string KLLDistrictCode { get; set; }
        public string Defined_cd { get; set; }//2
        public string TOTAL_OTHER_HOUSE_CNT_FROM { get; set; }
        public string TOTAL_OTHER_HOUSE_CNT_TO { get; set; }

        public string STRUCTURE_COUNT_FROM { get; set; }
        public string STRUCTURE_COUNT_TO { get; set; }

        public string INTERVIEW_DT_FROM { get; set; }
        public string INTERVIEW_DT_TO { get; set; }
        public string MobileNumber { get; set; }

        private List<ResurveyHouseBuildingDetail> objList = new List<ResurveyHouseBuildingDetail>();
        public List<ResurveyHouseBuildingDetail> HOUSE_BUILDING_DTL_List { get { return objList; } set { objList = null; } }

        public List<GrvDtlToHouseViewModelclass> grievDtlList { get; set; }
        public List<ResurveyGeoTechnicalRiskType> lstGeoTechnicalName { get; set; }
        public List<ResurveyOtherHousesDamagedModel> OtherHousesDamagedList { get; set; }
        public List<ResurveyHouseBuildingDetail> GeoRiskListmain { get; set; }

        public List<ResurveyHouseViewModel> GrievantListMain { get; set; }
        public List<ResurveyHouseViewModel> OwnerListMain { get; set; }
        public List<ResurveyHouseViewModel> RespondentListMain { get; set; }
        public List<SurveyDamageDetail> LstSurveyDmageDetail { get; set; }








































































        //public List<GrvDtlToHouseViewModelclass> grievDtlList { get; set; }
        //public List<OtherLiveableHouseDtlModelClass> LiveablehouseDtlList { get; set; }


        //public List<HouseOwnerNameModel> HouseOwnerNamesList { get; set; }
        //public List<VW_HOUSE_RESPONDENT_DETAIL> lstRespondentDetail { get; set; }
        //public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail { get; set; }
        //public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureName { get; set; }
        //public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalName { get; set; }
        //public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalDetail { get; set; }
        //public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseName { get; set; }
        //public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseDetail { get; set; }
        //public List<VW_DamageExtentHome> lstDamageExtentHome { get; set; }
        //public List<VW_DamageExtentHome> lstDamageExtentHomeName { get; set; }
        //public List<VW_DamageExtentHome> lstDamageLevelName { get; set; }
        //public List<OtherHousesDamagedModel> OtherHousesDamagedList { get; set; }
        //public List<CaseGrievanceHandled> GrievanceHandled { get; set; }
        //public List<GrievanceMemberDetail> GrievanceMemberList { get; set; }
        //public List<GrievantDetail> GrievantDetail { get; set; }


        //private List<VW_HOUSE_BUILDING_DTL> objList = new List<VW_HOUSE_BUILDING_DTL>();
        //public List<VW_HOUSE_BUILDING_DTL> HOUSE_BUILDING_DTL_List { get { return objList; } set { objList = null; } }

        //public List<GrievanceHandling> HouseOwnerDetailList { get; set; }
        //public List<HouseInOtherPlace> OtherHouseListDetail { get; set; }



    }

}
