using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_HOUSE_OWNER_MST
    {
        public System.String houseOwnerId { get; set; }

        public System.String definedCd { get; set; }

        public System.String enumeratorId { get; set; }

        public String interviewDt { get; set; }

        public System.String interviewDtLoc { get; set; }

        public System.String interviewStart { get; set; }

        public System.String interviewStHh { get; set; }

        public System.String interviewStMm { get; set; }

        public System.String interviewStSs { get; set; }

        public System.String interviewStMs { get; set; }

        public System.String interviewEnd { get; set; }

        public System.String interviewEndHh { get; set; }

        public System.String interviewEndMm { get; set; }

        public System.String interviewEndSs { get; set; }

        public System.String interviewEndMs { get; set; }

        public System.String interviewGmt { get; set; }

        public System.String geopoint { get; set; }

        public System.String latitude { get; set; }

        public System.String longitude { get; set; }

        public System.String altitude { get; set; }

        public System.String accuracy { get; set; }

        public System.String countryCd { get; set; }

        public System.String regStCd { get; set; }

        public System.String zoneCd { get; set; }

        public System.String districtCd { get; set; }

        public System.String vdcMunCd { get; set; }

        public System.String wardNo { get; set; }

        public System.String enumerationArea { get; set; }

        public System.String areaEng { get; set; }

        public System.String areaLoc { get; set; }

        public System.String houseSno { get; set; }

        public System.String houseOwnerCnt { get; set; }

        public System.String isOwner { get; set; }

        public System.String respondentFirstName { get; set; }

        public System.String respondentMiddleName { get; set; }

        public System.String respondentLastName { get; set; }

        public System.String respondentFullName { get; set; }

        public System.String respondentFirstNameLoc { get; set; }

        public System.String respondentMiddleNameLoc { get; set; }

        public System.String respondentLastNameLoc { get; set; }

        public System.String respondentFullNameLoc { get; set; }

        public System.String respondentPhoto { get; set; }

        public System.String respondentGenderCd { get; set; }

        public System.String respondentIsHousehead { get; set; }

        public System.String hhRelationTypeCd { get; set; }

        public System.String notInterviwingReasonCd { get; set; }

        public System.String anyOtherHouse { get; set; }

        public System.Decimal samePlaceOtherHouseCnt { get; set; }

        public System.Decimal otherPlaceOtherHouseCnt { get; set; }

        public System.Decimal nonresidNondamageHCnt { get; set; }

        public System.Decimal nonresidPartialDamageHCnt { get; set; }

        public System.Decimal nonresidFullDamageHCnt { get; set; }

        public System.String socialMobilizerPresentFlag { get; set; }

        public System.String socialMobilizerName { get; set; }

        public System.String socialMobilizerNameLoc { get; set; }

        public System.String imei { get; set; }

        public System.String imsi { get; set; }

        public System.String simNumber { get; set; }

        public System.String mobileNumber { get; set; }

        public System.String houseownerActive { get; set; }

        public System.String nhrsUuid { get; set; }

        public System.String remarks { get; set; }

        public System.String remarksLoc { get; set; }

        public System.String approved { get; set; }

        public System.String approvedBy { get; set; }

        public System.String approvedByLoc { get; set; }

        public System.DateTime? approvedDt { get; set; }

        public System.String approvedDtLoc { get; set; }

        public System.String updatedBy { get; set; }

        public System.String updatedByLoc { get; set; }

        public System.DateTime? updatedDt { get; set; }

        public System.String updatedDtLoc { get; set; }

        public System.String enteredBy { get; set; }

        public System.String enteredByLoc { get; set; }

        public System.DateTime? enteredDt { get; set; }

        public System.String enteredDtLoc { get; set; }

        public System.String ipAddress { get; set; }

        public List<MIG_MIS_HH_ALLOWANCE_DTL> MIG_MIS_HH_ALLOWANCE_DTL { get; set; }
        public List<MIG_MIS_HH_DEATH_DTL> MIG_MIS_HH_DEATH_DTL { get; set; }
        public List<MIG_MIS_HH_DIVORCE_DTL> MIG_MIS_HH_DIVORCE_DTL { get; set; }
        public List<MIG_MIS_HH_DOC_DTL> MIG_MIS_HH_DOC_DTL { get; set; }
        public List<MIG_MIS_HH_FAMILY_DTL> MIG_MIS_HH_FAMILY_DTL { get; set; }
        public List<MIG_MIS_HH_HEAD_DTL> MIG_MIS_HH_HEAD_DTL { get; set; }
        public List<MIG_MIS_HH_MARRIAGE_DTL> MIG_MIS_HH_MARRIAGE_DTL { get; set; }
        public List<MIG_MIS_HH_SCHOOL_DTL> MIG_MIS_HH_SCHOOL_DTL { get; set; }
        public List<MIG_MIS_HOUSEHOLD_INDICATOR> MIG_MIS_HOUSEHOLD_INDICATOR { get; set; }
        public List<MIG_MIS_HOUSEHOLD_INFO> MIG_MIS_HOUSEHOLD_INFO { get; set; }
        public List<MIG_MIS_MEMBER_DOC> MIG_MIS_MEMBER_DOC { get; set; }
        public List<MIG_NHRS_BA_SEC_OCCUPANCY> MIG_NHRS_BA_SEC_OCCUPANCY { get; set; }
        public List<MIG_NHRS_BUILDING_ASS_DTL> MIG_NHRS_BUILDING_ASS_DTL { get; set; }
        public List<MIG_NHRS_BUILDING_ASS_MST> MIG_NHRS_BUILDING_ASS_MST { get; set; }
        public List<MIG_NHRS_BUILDING_ASS_PHOTO> MIG_NHRS_BUILDING_ASS_PHOTO { get; set; }
        public List<MIG_NHRS_FATHER_DTL> MIG_NHRS_FATHER_DTL { get; set; }
        public List<MIG_NHRS_GFATHER_DTL> MIG_NHRS_GFATHER_DTL { get; set; }
        public List<MIG_NHRS_GMOTHER_DTL> MIG_NHRS_GMOTHER_DTL { get; set; }
        public List<MIG_NHRS_HH_HUMAN_DESTROY_DTL> MIG_NHRS_HH_HUMAN_DESTROY_DTL { get; set; }
        public List<MIG_NHRS_HH_OTH_RESIDENCE_DTL> MIG_NHRS_HH_OTH_RESIDENCE_DTL { get; set; }
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> MIG_NHRS_HH_SUPERSTRUCTURE_MAT { get; set; }
        public List<MIG_NHRS_HOUSE_OWNER_DTL> MIG_NHRS_HOUSE_OWNER_DTL { get; set; }
        public List<MIG_NHRS_MOTHER_DTL> MIG_NHRS_MOTHER_DTL { get; set; }
        public List<MIG_NHRS_RESPONDENT_DTL> MIG_NHRS_RESPONDENT_DTL { get; set; }
        public List<MIG_NHRS_SPOUSE_DTL> MIG_NHRS_SPOUSE_DTL { get; set; }
    }
}
