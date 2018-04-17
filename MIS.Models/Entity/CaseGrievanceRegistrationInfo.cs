using System;
using EntityFramework;

[Table(Name = "NHRS_GRIEVANCE_REGISTRATION")]
public class CaseGrievanceRegistrationInfo : EntityBase
{
    private decimal? caseregistartionddl { get; set; }
    private string definedCd { get; set; }
    private string formno { get; set; }
    private string RegistrationNo { get; set; }
    private decimal? RegistrationDistCd { get; set; }
    private decimal? RegistrationVdcMunCd { get; set; }
    private decimal? RegistrationWardNo { get; set; }
    private string RegistrationArea { get; set; }
    private DateTime? RegistrationDateEng { get; set; }
    private string RegistrationDateLoc { get; set; }
    private string FirstNameEng { get; set; }
    private string MiddleNameEng { get; set; }
    private string LastNameEng { get; set; }
    private string FirstNameLoc { get; set; }
    private string MiddleNameLoc { get; set; }
    private string LastNameLoc { get; set; }
    private string FatherFirstNameEng { get; set; }
    private string FatherMiddleNameEng { get; set; }
    private string FatherLastNameEng { get; set; }
    private string FatherFirstNameLoc { get; set; }
    private string FatherMiddleNameLoc { get; set; }
    private string FatherLastNameLoc { get; set; }

    private string HusbandFirstNameEng { get; set; }
    private string HusbandMiddleNameEng { get; set; }
    private string HusbandLastNameEng { get; set; }
    private string HusbandFirstNameLoc { get; set; }
    private string HusbandMiddleNameLoc { get; set; }
    private string HusbandLastNameLoc { get; set; }

    private string GFatherFirstNameEng { get; set; }
    private string GFatherMiddleNameEng { get; set; }
    private string GFatherLastNameEng { get; set; }
    private string GFatherFirstNameLoc { get; set; }
    private string GFatherMiddleNameLoc { get; set; }
    private string GFatherLastNameLoc { get; set; }
    private decimal? HouseholdMemberCount { get; set; }
    private string houselandlegalotherComment { get; set; }
    private string BeneficiaryID { get; set; }
    private string SurveyID { get; set; }
    private string LalpurjaNo { get; set; }
    private DateTime? lalPurjaIssueDateEng { get; set; }
    private string lalPurjaIssueDateLoc { get; set; }
    private string CitizenshipNo { get; set; }
    private DateTime? CitizenshipIssueDateEng { get; set; }
    private string CitizenshipISsueDateLoc { get; set; }
    private decimal? distcd { get; set; }
    private decimal? vdcmuncd { get; set; }
    private decimal? WardNo { get; set; }
    private string Area { get; set; }
    private string ContactPhoneNo { get; set; }
    private decimal? HouseLandLegalOwnerCd { get; set; }
    private string HouseInOtherPlace  {get;set;}
    private decimal? EnumeratorID { get; set; }
    private string EnumeratorFirstNameEng { get; set; }
    private string EnumeratorMiddleNameEng { get; set; }
    private string EnumeratorLastNameEng { get; set; }
    private string EnumeratorFirstNameLoc { get; set; }
    private string EnumeratorMiddleNameLoc { get; set; }
    private string EnumeratorLastNameLoc { get; set; }
    private DateTime? EnumeratorSignedDateEng { get; set; }
    private string EnumeratorSignedDateLoc { get; set; }
    private DateTime? CaseSignatureDateEng { get; set; }
    private string CaseSignatureDateLoc { get; set; }
    private string CaseStatus { get; set; }
    private string CaseAddressedByFname { get; set; }
    private string CaseAddressedByMname { get; set; }
    private string CaseAddressedByLname { get; set; }
    private DateTime? CaseAddressedDateEng { get; set; }
    private string CaseAddressedDateLoc { get; set; }
    private string EnteredBy { get; set; }
    private string EnteredDt { get; set; }
    private string LastUpdatedBy { get; set; }
    private string LastUpdatedDt { get; set; }
    [Column(Name = "CASE_REGISTRATION_ID", IsKey = true, SequenceName = "")]
    public Decimal? CASE_REGISTRATION_ID
    {
        get { return caseregistartionddl; }
        set { caseregistartionddl = value; }
    }

    [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
    public String DEFINED_CD
    {
        get { return definedCd; }
        set { definedCd = value; }
    }

    [Column(Name = "FORM_NO", IsKey = false, SequenceName = "")]
    public String FORM_NO
    {
        get { return formno; }
        set { formno = value; }
    }

    [Column(Name = "REGISTRATION_NO", IsKey = false, SequenceName = "")]
    public string REGISTRATION_NO
    {
        get { return RegistrationNo; }
        set { RegistrationNo = value; }
    }

    [Column(Name = "REGISTRATION_DIST_CD", IsKey = false, SequenceName = "")]
    public Decimal? REGISTRATION_DIST_CD
    {
        get { return RegistrationDistCd; }
        set { RegistrationDistCd = value; }
    }

    [Column(Name = "REGISTRATION_VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public Decimal? REGISTRATION_VDC_MUN_CD
    {
        get { return RegistrationVdcMunCd; }
        set { RegistrationVdcMunCd = value; }
    }

    [Column(Name = "REGISTRATION_WARD_NO", IsKey = false, SequenceName = "")]
    public Decimal? REGISTRATION_WARD_NO
    {
        get { return RegistrationWardNo; }
        set { RegistrationWardNo = value; }
    }

    [Column(Name = "REGISTRATION_AREA", IsKey = false, SequenceName = "")]
    public String REGISTRATION_AREA
    {
        get { return RegistrationArea; }
        set { RegistrationArea = value; }
    }

    [Column(Name = "REGISTRATION_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? REGISTRATION_DATE_ENG
    {
        get { return RegistrationDateEng; }
        set { RegistrationDateEng = value; }
    }

    [Column(Name = "REGISTRATION_DATE_LOC", IsKey = false, SequenceName = "")]
    public String REGISTRATION_DATE_LOC
    {
        get { return RegistrationDateLoc; }
        set { RegistrationDateLoc = value; }
    }

    [Column(Name = "FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String FIRST_NAME_ENG
    {
        get { return FirstNameEng; }
        set { FirstNameEng = value; }
    }

    [Column(Name = "MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public String MIDDLE_NAME_ENG
    {
        get { return MiddleNameEng; }
        set { MiddleNameEng = value; }
    }

    [Column(Name = "LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String LAST_NAME_ENG
    {
        get { return LastNameEng; }
        set { LastNameEng = value; }
    }
    [Column(Name = "FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String FIRST_NAME_LOC
    {
        get { return FirstNameLoc; }
        set { FirstNameLoc = value; }
    }

    [Column(Name = "MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public String MIDDLE_NAME_LOC
    {
        get { return MiddleNameLoc; }
        set { MiddleNameLoc = value; }
    }

    [Column(Name = "LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String LAST_NAME_LOC
    {
        get { return LastNameLoc; }
        set { LastNameLoc = value; }
    }


    [Column(Name = "HUSBAND_FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String HUSBAND_FIRST_NAME_ENG
    {
        get { return HusbandFirstNameEng; }
        set { HusbandFirstNameEng = value; }
    }

    [Column(Name = "HUSBAND_MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public String HUSBAND_MIDDLE_NAME_ENG
    {
        get { return HusbandMiddleNameEng; }
        set { HusbandMiddleNameEng = value; }
    }

    [Column(Name = "HUSBAND_LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String HUSBAND_LAST_NAME_ENG
    {
        get { return HusbandLastNameEng; }
        set { HusbandLastNameEng = value; }
    }
    [Column(Name = "HUSBAND_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String HUSBAND_FIRST_NAME_LOC
    {
        get { return FirstNameLoc; }
        set { FirstNameLoc = value; }
    }

    [Column(Name = "HUSBAND_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public String HUSBAND_MIDDLE_NAME_LOC
    {
        get { return HusbandMiddleNameLoc; }
        set { HusbandMiddleNameLoc = value; }
    }

    [Column(Name = "HUSBAND_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String HUSBAND_LAST_NAME_LOC
    {
        get { return HusbandLastNameLoc; }
        set { HusbandLastNameLoc = value; }
    }

    [Column(Name = "FATHER_FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String FATHER_FIRST_NAME_ENG
    {
        get { return FatherFirstNameEng; }
        set { FatherFirstNameEng = value; }
    }

    [Column(Name = "FATHER_MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public String FATHER_MIDDLE_NAME_ENG
    {
        get { return FatherMiddleNameEng; }
        set { FatherMiddleNameEng = value; }
    }

    [Column(Name = "FATHER_LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String FATHER_LAST_NAME_ENG
    {
        get { return FatherLastNameEng; }
        set { FatherLastNameEng = value; }
    }
    [Column(Name = "FATHER_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String FATHER_FIRST_NAME_LOC
    {
        get { return FatherLastNameLoc; }
        set { FatherLastNameLoc = value; }
    }

    [Column(Name = "FATHER_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public String FATHER_MIDDLE_NAME_LOC
    {
        get { return FatherMiddleNameLoc; }
        set { FatherMiddleNameLoc = value; }
    }

    [Column(Name = "FATHER_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String FATHER_LAST_NAME_LOC
    {
        get { return FatherLastNameLoc; }
        set { FatherLastNameLoc = value; }
    }

    [Column(Name = "GFATHER_FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String GFATHER_FIRST_NAME_ENG
    {
        get { return GFatherFirstNameEng; }
        set { GFatherFirstNameEng = value; }
    }

    [Column(Name = "GFATHER_MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public String GFATHER_MIDDLE_NAME_ENG
    {
        get { return GFatherMiddleNameEng; }
        set { GFatherMiddleNameEng = value; }
    }

    [Column(Name = "GFATHER_LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String GFATHER_LAST_NAME_ENG
    {
        get { return GFatherLastNameEng; }
        set { GFatherLastNameEng = value; }
    }

    [Column(Name = "GFATHER_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String GFATHER_FIRST_NAME_LOC
    {
        get { return GFatherLastNameLoc; }
        set { GFatherLastNameLoc = value; }
    }

    [Column(Name = "GFATHER_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public String GFATHER_MIDDLE_NAME_LOC
    {
        get { return GFatherMiddleNameLoc; }
        set { GFatherMiddleNameLoc = value; }
    }

    [Column(Name = "GFATHER_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String GFATHER_LAST_NAME_LOC
    {
        get { return GFatherLastNameLoc; }
        set { GFatherLastNameLoc = value; }
    }

    [Column(Name = "HOUSEHOLD_MEMBER_COUNT", IsKey = false, SequenceName = "")]
    public Decimal? HOUSEHOLD_MEMBER_COUNT
    {
        get { return HouseholdMemberCount; }
        set { HouseholdMemberCount = value; }
    }

    [Column(Name = "BENEFICIARY_ID", IsKey = false, SequenceName = "")]
    public String BENEFICIARY_ID
    {
        get { return BeneficiaryID; }
        set { BeneficiaryID = value; }
    }

    [Column(Name = "SURVEY_ID", IsKey = false, SequenceName = "")]
    public String SURVEY_ID
    {
        get { return SurveyID; }
        set { SurveyID = value; }
    }

    [Column(Name = "LALPURJA_NO", IsKey = false, SequenceName = "")]
    public string LALPURJA_NO
    {
        get { return LalpurjaNo; }
        set { LalpurjaNo = value; }
    }

    [Column(Name = "LALPURJA_ISSUE_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? LALPURJA_ISSUE_DATE_ENG
    {
        get { return lalPurjaIssueDateEng; }
        set { lalPurjaIssueDateEng = value; }
    }

    [Column(Name = "LALPURJA_ISSUE_DATE_LOC", IsKey = false, SequenceName = "")]
    public String LALPURJA_ISSUE_DATE_LOC
    {
        get { return lalPurjaIssueDateLoc; }
        set { lalPurjaIssueDateLoc = value; }
    }

    [Column(Name = "CITIZENSHIP_NO", IsKey = false, SequenceName = "")]
    public string CITIZENSHIP_NO
    {
        get { return CitizenshipNo; }
        set { CitizenshipNo = value; }
    }

    [Column(Name = "CITIZENSHIP_ISSUE_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? CITIZENSHIP_ISSUE_DATE_ENG
    {
        get { return CitizenshipIssueDateEng; }
        set { CitizenshipIssueDateEng = value; }
    }

    [Column(Name = "CITIZENSHIP_ISSUE_DATE_LOC", IsKey = false, SequenceName = "")]
    public String CITIZENSHIP_ISSUE_DATE_LOC
    {
        get { return CitizenshipISsueDateLoc; }
        set { CitizenshipISsueDateLoc = value; }
    }

    [Column(Name = "DIST_CD", IsKey = false, SequenceName = "")]
    public Decimal? DIST_CD
    {
        get { return distcd; }
        set { distcd = value; }
    }

    [Column(Name = "VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public decimal? VDC_MUN_CD
    {
        get { return vdcmuncd; }
        set { vdcmuncd = value; }
    }

    [Column(Name = "WARD_NO", IsKey = false, SequenceName = "")]
    public Decimal? WARD_NO
    {
        get { return WardNo; }
        set { WardNo = value; }
    }

    [Column(Name = "AREA", IsKey = false, SequenceName = "")]
    public String AREA
    {
        get { return Area; }
        set { Area = value; }
    }

    [Column(Name = "CONTACT_PHONE_NO", IsKey = false, SequenceName = "")]
    public string CONTACT_PHONE_NO
    {
        get { return ContactPhoneNo; }
        set { ContactPhoneNo = value; }
    }

    [Column(Name = "HOUSE_LAND_LEGAL_OWNERCD", IsKey = false, SequenceName = "")]
    public Decimal? HOUSE_LAND_LEGAL_OWNERCD
    {
        get { return HouseLandLegalOwnerCd; }
        set { HouseLandLegalOwnerCd = value; }
    }

    [Column(Name = "HOUSE_IN_OTHER_PLACE", IsKey = false, SequenceName = "")]
    public String HOUSE_IN_OTHER_PLACE
    {
        get { return HouseInOtherPlace; }
        set { HouseInOtherPlace = value; }
    }

    [Column(Name = "CASE_SIGNATURE_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? CASE_SIGNATURE_DATE_ENG
    {
        get { return CaseSignatureDateEng; }
        set { CaseSignatureDateEng = value; }
    }

    [Column(Name = "CASE_SIGNATURE_DATE_LOC", IsKey = false, SequenceName = "")]
    public String CASE_SIGNATURE_DATE_LOC
    {
        get { return CaseSignatureDateLoc; }
        set { CaseSignatureDateLoc = value; }
    }

    [Column(Name = "ENUMENATOR_ID", IsKey = false, SequenceName = "")]
    public Decimal? ENUMENATOR_ID
    {
        get { return EnumeratorID; }
        set { EnumeratorID = value; }
    }

    [Column(Name = "ENUMENATOR_FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_FIRST_NAME_ENG
    {
        get { return EnumeratorFirstNameEng; }
        set { EnumeratorFirstNameEng = value; }
    }

    [Column(Name = "ENUMENATOR_MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_MIDDLE_NAME_ENG
    {
        get { return EnumeratorMiddleNameEng; }
        set { EnumeratorMiddleNameEng = value; }
    }

    [Column(Name = "ENUMENATOR_LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_LAST_NAME_ENG
    {
        get { return EnumeratorLastNameEng; }
        set { EnumeratorLastNameEng = value; }
    }

    [Column(Name = "ENUMENATOR_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_FIRST_NAME_LOC
    {
        get { return EnumeratorFirstNameLoc; }
        set { EnumeratorFirstNameLoc = value; }
    }

    [Column(Name = "ENUMENATOR_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_MIDDLE_NAME_LOC
    {
        get { return EnumeratorMiddleNameLoc; }
        set { EnumeratorMiddleNameLoc = value; }
    }

    [Column(Name = "ENUMENATOR_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_LAST_NAME_LOC
    {
        get { return EnumeratorLastNameLoc; }
        set { EnumeratorLastNameLoc = value; }
    }

    [Column(Name = "ENUMENATOR_SIGNED_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? ENUMENATOR_SIGNED_DATE_ENG
    {
        get { return EnumeratorSignedDateEng; }
        set { EnumeratorSignedDateEng = value; }
    }

    [Column(Name = "ENUMENATOR_SIGNED_DATE_LOC", IsKey = false, SequenceName = "")]
    public String ENUMENATOR_SIGNED_DATE_LOC
    {
        get { return EnumeratorSignedDateLoc; }
        set { EnumeratorSignedDateLoc = value; }
    }

    [Column(Name = "CASE_STATUS", IsKey = false, SequenceName = "")]
    public String CASE_STATUS
    {
        get { return CaseStatus; }
        set { CaseStatus = value; }
    }

    [Column(Name = "CASE_ADDRESSED_BY_FNAME_ENG", IsKey = false, SequenceName = "")]
    public String CASE_ADDRESSED_BY_FNAME_ENG
    {
        get { return CaseAddressedByFname; }
        set { CaseAddressedByFname = value; }
    }
    [Column(Name = "CASE_ADDRESSED_BY_MNAME_ENG", IsKey = false, SequenceName = "")]
    public String CASE_ADDRESSED_BY_MNAME_ENG
    {
        get { return CaseAddressedByMname; }
        set { CaseAddressedByMname = value; }
    }
    [Column(Name = "CASE_ADDRESSED_BY_LNAME_ENG", IsKey = false, SequenceName = "")]
    public String CASE_ADDRESSED_BY_LNAME_ENG
    {
        get { return CaseAddressedByLname; }
        set { CaseAddressedByLname = value; }
    }
    [Column(Name = "HOUSE_LAND_LEGAL_OTH_COMMENT", IsKey = false, SequenceName = "")]
    public String HOUSE_LAND_LEGAL_OTH_COMMENT
    {
        get { return houselandlegalotherComment; }
        set { houselandlegalotherComment = value; }
    }
    [Column(Name = "CASE_ADDRESSED_DATE_ENG", IsKey = false, SequenceName = "")]
    public DateTime? CASE_ADDRESSED_DATE_ENG
    {
        get { return CaseAddressedDateEng; }
        set { CaseAddressedDateEng = value; }
    }

    [Column(Name = "CASE_ADDRESSED_DATE_LOC", IsKey = false, SequenceName = "")]
    public String CASE_ADDRESSED_DATE_LOC
    {
        get { return CaseAddressedDateLoc; }
        set { CaseAddressedDateLoc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public String ENTERED_BY
    {
        get { return EnteredBy; }
        set { EnteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public String ENTERED_DT
    {
        get { return EnteredDt; }
        set { EnteredDt = value; }
    }


    [Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
    public String LAST_UPDATED_BY
    {
        get { return LastUpdatedBy; }
        set { LastUpdatedBy = value; }
    }

    [Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
    public String LAST_UPDATED_DT
    {
        get { return LastUpdatedDt; }
        set { LastUpdatedDt = value; }
    }
    public CaseGrievanceRegistrationInfo()
    { }

}
