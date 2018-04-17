using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MIS.Models.Setup;
using MIS.Models.CaseGrievence;

namespace MIS.Models.CaseGrievance
{
    public class CaseGrievanceModel
    {
        public String grievanceCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String householdId { get; set; }

        public String householdIdSrch { get; set; }

        public String regionCd { get; set; }
        public String regionCdName { get; set; }

        public String ZoneCd { get; set; }
        public String ZoneCdName { get; set; }

        public String DistrictCd { get; set; }
        public String District { get; set; }
        public String VDCMunCd { get; set; }
        public String VDCMun { get; set; }
        public String WardCd { get; set; }
        public String WardCdName { get; set; }
        public String Ward { get; set; }
        public String BatchID { get; set; }
        public String ddlApprove { get; set; }
        public String hhFullName { get; set; }

        public String caseStatus { get; set; }
        public String pinNo { get; set; }

        public String address { get; set; }


        public String phoneNo { get; set; }
        [Required(ErrorMessage = " ")]
        public String listCase { get; set; }
        [Required(ErrorMessage = " ")]
        public String caseDesc { get; set; }

        [Required(ErrorMessage = " ")]
        public String signApplicant { get; set; }
        [Required(ErrorMessage = " ")]
        public String signAppDate { get; set; }

        public String signDswOfficer { get; set; }
        public String signDswDate { get; set; }
        public String signSeniorOfficer { get; set; }
        public String signSeniorDate { get; set; }
        public String processRemark { get; set; }
        public String finalRemark { get; set; }

        public String PER_REGION_ENG { get; set; }

        public String PER_Zone_ENG { get; set; }

        public String PER_District_ENG { get; set; }

        public String VDCMun_CD { get; set; }

        public String PER_WARD_NO { get; set; }


        public String fullName { get; set; }

        public String approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedDt { get; set; }
        public String updatedBy { get; set; }
        public String updatedDt { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }

        public String ipAddress { get; set; }
        public String RegTypeCd { get; set; }
        public String CategoryName { get; set; }

       // Added Extra field for case registration detail table
        public String sno { get; set; }
        public String caseStatusRemarks { get; set; }
        public String caseStatusDt { get; set; }

        public string pageIndex { get; set; }
        public string pageSize { get; set; }
        public string sortBy { get; set; }
        public string sortOrder { get; set; }
        public string filterWord { get; set; }
        [Required(ErrorMessage = " ")]
        public string regTypeGrpCd { get; set; }

        public string OtherHouse { get; set; }
        public string CASE_REGISTRATION_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string FORM_NO { get; set; }
        public string REGISTRATION_NO { get; set; }
        public string Nissa_NO { get; set; }
        public string RETRO_FLAG { get; set; }
        public Decimal? REGISTRATION_DIST_CD { get; set; }
        public string REGISTRATION_DIST_ENG { get; set; }
        public string REGISTRATION_DIST_LOC { get; set; }
        public string REGISTRATION_VDC_MUN_CD { get; set; }
        public string REGISTRATION_VDC_MUN_ENG { get; set; }
        public string REGISTRATION_VDC_MUN_LOC { get; set; }
        public Decimal? REGISTRATION_WARD_NO { get; set; }
        public string REGISTRATION_AREA { get; set; }
        public string REGISTRATION_DATE_ENG { get; set; }
        public string REGISTRATION_DATE_LOC { get; set; }
        public string FIRST_NAME_ENG { get; set; }
        public string MIDDLE_NAME_ENG { get; set; }
        public string LAST_NAME_ENG { get; set; }
        public string FULL_NAME_ENG { get; set; }
        public string FIRST_NAME_LOC { get; set; }
        public string MIDDLE_NAME_LOC { get; set; }
        public string LAST_NAME_LOC { get; set; }
        public string FATHER_FIRST_NAME_ENG { get; set; }
        public string FATHER_MIDDLE_NAME_ENG { get; set; }
        public string FATHER_LAST_NAME_ENG { get; set; }
        public string FATHER_FULL_NAME_ENG { get; set; }
        public string FATHER_FIRST_NAME_LOC { get; set; }
        public string FATHER_MIDDLE_NAME_LOC { get; set; }
        public string FATHER_LAST_NAME_LOC { get; set; }
        public string GFATHER_FIRST_NAME_ENG { get; set; }
        public string GFATHER_MIDDLE_NAME_ENG { get; set; }
        public string GFATHER_LAST_NAME_ENG { get; set; }
        public string GFATHER_FULL_NAME_ENG { get; set; }
        public string GFATHER_FIRST_NAME_LOC { get; set; }
        public string GFATHER_MIDDLE_NAME_LOC { get; set; }
        public string GFATHER_LAST_NAME_LOC { get; set; }

        public string HUSBAND_FIRST_NAME_ENG { get; set; }
        public string HUSBAND_MIDDLE_NAME_ENG { get; set; }
        public string HUSBAND_LAST_NAME_ENG { get; set; }
        public string HUSBAND_FULL_NAME_ENG { get; set; }
        public string HUSBAND_FIRST_NAME_LOC { get; set; }
        public string HUSBAND_MIDDLE_NAME_LOC { get; set; }
        public string HUSBAND_LAST_NAME_LOC { get; set; }

        public Decimal? HOUSEHOLD_MEMER_COUNT { get; set; }
        public string BENEFICIARY_ID { get; set; }
        public string SURVEY_ID { get; set; }
        public string LALPURJA_NO { get; set; }
        public string LALPURJA_ISSUE_DATE_ENG { get; set; }
        public string LALPURJA_ISSUE_DATE_LOC { get; set; }
        public Decimal? DIST_CD { get; set; }
        public string VDC_MUN_CD { get; set; }
        public Decimal? WARD_NO { get; set; }
        public string AREA { get; set; }
        public string CONTACT_PHONE_NO { get; set; }
        public string HOUSE_LAND_LEGAL_OWNERCD { get; set; }
        public string LAND_LEGAL_ENG { get; set; }
        public string LAND_LEGAL_LOC { get; set; }
        public string HOUSE_LAND_LEGAL_OTH_COMMENT { get; set; }
        public string HOUSE_IN_OTHER_PLACE { get; set; }
        public string CASE_SIGNATURE_DATE_ENG { get; set; }
        public string CASE_SIGNATURE_DATE_LOC { get; set; }
        public string ENUMENATOR_ID { get; set; }
        public string ENUMENATOR_FIRST_NAME_ENG { get; set; }
        public string ENUMENATOR_MIDDLE_NAME_ENG { get; set; }
        public string ENUMENATOR_LAST_NAME_ENG { get; set; }
        public string ENUMENATOR_FULL_NAME_ENG { get; set; }
        public string ENUMENATOR_FIRST_NAME_LOC { get; set; }
        public string ENUMENATOR_MIDDLE_NAME_LOC { get; set; }
        public string ENUMENATOR_LAST_NAME_LOC { get; set; }
        public string ENUMENATOR_SIGNED_DATE_ENG { get; set; }
        public string ENUMENATOR_SIGNED_DATE_LOC { get; set; }
        public string CITIZENSHIP_NO { get; set; }
        public string CITIZENSHIP_ISSUE_DATE_ENG { get; set; }
        public string CITIZENSHIP_ISSUE_DATE_LOC { get; set; }
        public string CASE_STATUS { get; set; }
        public string CASE_ADDRESSED_BY_FNAME_ENG { get; set; }
        public string CASE_ADDRESSED_BY_MNAME_ENG { get; set; }
        public string CASE_ADDRESSED_BY_LNAME_ENG { get; set; }
        public string CASE_ADDRESSED_BY_FullNAME_ENG { get; set; }
        public string CASE_ADDRESSED_DATE_ENG { get; set; }
        public string CASE_ADDRESSED_DATE_LOC { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DT { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public string LAST_UPDATED_DT { get; set; }
        public string Mode { get; set; }
        public string ViewMode { get; set; }
        public List<CaseGrievanceDetail> CaseGrievanceDtl { get; set; }
        public List<CaseGrievanceOTHDetail> CaseGrievanceOthDtl { get; set; }
        public List<NHRSGrievanceDocType> DocumentTypeDetail { get; set; }
        public List<NHRSGrievanceDocType> DocumentTypeName { get; set; }
        public List<CaseType> CaseRegistrationList { get; set; }
        public List<CaseType> CaseRegistrationName { get; set; }
    }
}
