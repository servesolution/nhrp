using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;




[Table(Name = "NHRS_ENROLLMENT_PA")]

public class EnrollmentMou : EntityBase
    {
    private System.String FiscalYear = null;
    private System.Decimal? TargtBtchId = null;
    private System.Decimal? TargtingId = null;

    private System.Decimal? EnrollmentId = null;
    private System.String MOUID = null;
    private System.String  DefinedMouId= null;
    private System.String SurveyNo = null;

    private System.Decimal? EnrollMouDay = null;
    private System.Decimal? EnrollMouMnth = null;
    private System.Decimal? EnrollMouYr = null;
    private System.String EnrollMouDt = null;
    private System.String EnrollMouDayLoc = null;
    private System.String EnrollMouMnthLoc = null;
    private System.String EnrollMouYrLoc = null;
    private System.String EnrollMouDtLoc = null;

    private System.String HouseOwnerId = null;
    private System.String HoMemId = null;
    private System.String HoHldId = null;
    private System.String HOHldDefCd = null;
    private System.String BeneficiaryTypCd = null;
    private System.String MemId = null;
   
    private System.String BeneficiaryFNmEng = null;
    private System.String BeneficiaryMNmEng = null;
    private System.String BeneficiaryLNmEng = null;
    private System.String BeneficiaryFullNmEng = null;

    private System.String BeneficiaryFNmLoc = null;
    private System.String BeneficiaryMNmLoc = null;
    private System.String BeneficiaryLNmLoc = null;
    private System.String BeneficiaryFullNmLoc = null;
    private System.Decimal? BeneficiaryRelnTypCd = null;

    private System.Decimal? RegnStateCd = null;
    private System.Decimal? ZoneCd = null;
    private System.Decimal? DistrictCd = null;
    private System.String VdcMunCd = null;
    private System.Decimal? WardNo = null;
    private System.String EnumerationArea = null;
    private System.String AreaEng = null;
    private System.String AreaLoc = null;

    private System.String FatherMemId = null;
    private System.String FatherFnameEng = null;
    private System.String FatherMnameEng = null;
    private System.String FatherLnameEng = null;
    private System.String FatherFullnameEng = null;
    private System.String FatherFnameLoc = null;
    private System.String FatherMnameLoc = null;
    private System.String FatherLnameLoc = null;
    private System.String FatherFullnameLoc = null;

    private System.String GFatherMemId = null;
    private System.String GFatherFnameEng = null;
    private System.String GFatherMnameEng = null;
    private System.String GFatherLnameEng = null;
    private System.String GFatherFullnameEng = null;
    private System.String GFatherFnameLoc = null;
    private System.String GFatherMnameLoc = null;
    private System.String GFatherLnameLoc = null;
    private System.String GFatherFullnameLoc = null;

    private System.String FinlawFnameEng = null;
    private System.String FinlawFnameLoc = null;
    private System.String FinlawMnameEng = null;
    private System.String FinlawMnameLoc = null;
    private System.String FinlawLnameEng = null;
    private System.String FinlawLnameLoc = null;
    private System.String FinLawFullNameEng = null;
    private System.String FinLawFullNameLoc = null;

    private System.String husbandmemberid = null;
    private System.String husbandfnameeng = null;
    private System.String husbandfnameloc = null;
    private System.String husbandMnameeng = null;
    private System.String husbandMnameloc = null;
    private System.String husbandLnameeng = null;
    private System.String husbandLnameloc = null;
    private System.String husbandFullnameEng = null;
    private System.String husbandFullnameLoc = null;
    
    private System.Decimal? IdentificationTypCD = null;
    private System.String IdentificationNo = null;
    private System.String beneficiaryctznoo = null;

    private System.String IdentificationDoc = null;
    private System.Decimal? IdentificationIssDisCd = null;
    private System.Decimal? IdentificationIssDay = null;
    private System.Decimal? IdentificationIssMnth = null;
    private System.Decimal? IdentificationIssYr = null;
    private System.String IdentificationIssDt = null;
    private System.String IdentificationIssDayLoc = null;
    private System.String IdentificationIssMnthLoc = null;
    private System.String IdentificationIssYrLoc = null;
    private System.String IdentificationIssDtLoc = null;

    private System.String EnumeratorId = null;
    private System.String BuildingKittaNo = null;
    private System.String BuildingArea = null;
    private System.Decimal? BuildingDistCd = null;
    private System.Decimal? BuildingVdcMunCd = null;
    private System.Decimal? BuildingWardNo = null;
    private System.String BuildingAreaEng = null;
    private System.String BuildingAreaLoc = null;

    private System.String NomineeMemId = null;
    private System.String NomineeFnamEng = null;
    private System.String NomineeMnamEng = null;
    private System.String NomineeLnamEng = null;
    private System.String NomineeFullnamEng = null;

    private System.String NomineeFnamLoc = null;
    private System.String NomineeMnamLoc = null;
    private System.String NomineeLnamLoc = null;
    private System.String NomineeFullnamLoc = null;
    private System.Decimal? NomineeRelnTypCd = null;

    private System.String EmployCd = null;
    private System.String OfficeCd = null;
    private System.String Remarks = null;
    private System.String RemarksLoc = null;

    private System.Decimal? BankCd = null;
    private System.Decimal? BankBrnchCd = null;
    private System.String BankAccNo = null;
    private System.String AccHoldFNameEng = null;
    private System.String AccHoldMNameEng = null;
    private System.String AccHoldLNameEng = null;

    private System.Decimal? BankAccTypCd = null;

    private System.String IsPaymentReceiverChngd = null;
    private System.String ChangedReasonEng = null;
    private System.String ChangedReasonLoc = null;

    private System.String Approved = null;
    private System.String ApprovedBy = null;
    private System.String ApprovedDt = null;
    private System.String ApprovedDtLoc = null;

    private System.String UpdatedBy = null;
    private System.String UpdatedDt = null;
    private System.String UpdatedDtLoc = null;

    private System.String EnteredBy = null;
    private System.DateTime? EnteredDt = null;
    private System.String EnteredDtLoc = null;
    private System.String ipaddress = null;

    private System.String PayrollGenerationFlag = null;
    private System.String Payrollbatchid = null;
    private System.String PayrollDtlID = null;

    private System.String nradefinedcd = null;

    //private System.String Mode = null;
    private System.String GuardianFullNameEng = null;
    private System.String GuardianFullNameLoc = null;
    private System.String GuardianIdentityNo = null;
    private System.Decimal ? GuardianDistrictCd = null;
    private System.Decimal ? GuardianVdcCd = null;
    private System.Decimal ? GuardianWardNo = null;
    private System.Decimal ? GuardianRelTypCd = null;
    private System.String IsBeneficiaryMigrated = null;
    private System.String BenfMigrationDate = null;
    private System.String BenfMigratnDateLoc = null;
    private System.Decimal? BenfMigrationNo = null;
    private System.String IsBulDesignFrmCatalog = null;
    private System.Decimal ? BulDesignCatNo = null;
    private System.String BuildingPilarTyp = null;
    private System.String BulFloorRoofTyp = null;
    private System.String BulOtherTyp = null;
    private System.Decimal? fatherRelationtypecd = null;
    private System.Decimal? gfatherRelationtypecd = null;
    private System.String Benfdobeng = null;
    private System.String Benfdobloc = null;
    private System.String BenfPhone = null;
    private System.String BenfPhoto = null;
    private System.String isManjurinamaAvail = null;
    private System.String ManjurinamaFNameEng = null;
    private System.String ManjurinamaMNameEng = null;
    private System.String ManjurinamaLNameEng = null;
    private System.String ManjurinamaFullNameEng = null;
    private System.String ManjurinamaFNameLoc = null;
    private System.String ManjurinamaMNameLoc = null;
    private System.String ManjurinamaLNameLoc = null;
    private System.String ManjurinamaFullNameLoc = null;
    private System.Decimal? ManjurinamaDistrictCd = null;
    private System.String ManjurinamaVdcMunCd = null;
    private System.String ManjurinamaWard = null;
    private System.String ManjurinamaAreaEng = null;
    private System.String ManjurinamaAreaLoc = null;
    private System.Decimal? ManjurinamaIdentityTypeCd = null;
    private System.String ManjurinamaIdentityNo = null;
    private System.Decimal? ManjurinamaIdentityIssueDisCd = null;
    private System.String ManjurinamaIdentityIssueDate = null;
    private System.String ManjurinamaIdentityIssueDateLoc = null;
    private System.String ManjurinamaBirthDate = null;
    private System.String ManjurinamaBirthDateLoc = null;
    private System.String ManjurinamaFatherFNameEng = null;
    private System.String ManjurinamaFatherMNameEng = null;
    private System.String ManjurinamaFatherLNameEng = null;
    private System.String ManjurinamaFatherFNameLoc = null;
    private System.String ManjurinamaFatherMNameLoc = null;
    private System.String ManjurinamaFatherLNameLoc = null;
    private System.String ManjurinamaFatherFullNameEng = null;
    private System.String ManjurinamaFatherFullNameLoc = null;
    private System.String ManjurinamaGFatherFNameEng = null;
    private System.String ManjurinamaGFatherMNameEng = null;
    private System.String ManjurinamaGFatherLNameEng = null;
    private System.String ManjurinamaGFatherFNameLoc = null;
    private System.String ManjurinamaGFatherMNameLoc = null;
    private System.String ManjurinamaGFatherLNameLoc = null;
    private System.String ManjurinamaGFatherFullNameEng = null;
    private System.String ManjurinamaGFatherFullNameLoc = null;
    private System.Decimal? ManjurinamaRelationTypeCd = null;
    private System.String ManjurinamaPhone = null;
    private System.String IsBuildingToConstruct = null;
    private System.String IsBulOwnDesign = null;
    private System.String BuildingStrNo = null;

    private System.String SakshiFNameEng = null;
    private System.String SakshiMNameEng = null;
    private System.String SakshiLNameEng = null;
    private System.String CitizenshipsPicName = null;

    private System.String MapapprovedNo1 = null;


    [Column(Name = "IS_BUILDING_TOBE_CONSTRUCTED", IsKey = true, SequenceName = "")]
    public System.String IS_BUILDING_TOBE_CONSTRUCTED
    {
        get { return IsBuildingToConstruct; }
        set { IsBuildingToConstruct = value; }
    }
    [Column(Name = "MANJURINAMA_PHONE", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_PHONE
    {
        get { return ManjurinamaPhone; }
        set { ManjurinamaPhone = value; }
    }
    [Column(Name = "MANJURINAMA_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? MANJURINAMA_RELATION_TYPE_CD
    {
        get { return ManjurinamaRelationTypeCd; }
        set { ManjurinamaRelationTypeCd = value; }
    }
    [Column(Name = "MANJURINAMA_GF_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_FULLNAME_LOC
    {
        get { return ManjurinamaGFatherFullNameLoc; }
        set { ManjurinamaGFatherFullNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_GF_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_FULLNAME_ENG
    {
        get { return ManjurinamaGFatherFullNameEng; }
        set { ManjurinamaGFatherFullNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_GF_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_LNAME_LOC
    {
        get { return ManjurinamaGFatherLNameLoc; }
        set { ManjurinamaGFatherLNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_GF_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_MNAME_LOC
    {
        get { return ManjurinamaGFatherMNameLoc; }
        set { ManjurinamaGFatherMNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_GF_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_FNAME_LOC
    {
        get { return ManjurinamaGFatherFNameLoc; }
        set { ManjurinamaGFatherFNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_GF_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_LNAME_ENG
    {
        get { return ManjurinamaGFatherLNameEng; }
        set { ManjurinamaGFatherLNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_GF_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_MNAME_ENG
    {
        get { return ManjurinamaGFatherMNameEng; }
        set { ManjurinamaGFatherMNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_GF_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_GFATHER_FNAME_ENG
    {
        get { return ManjurinamaGFatherFNameEng; }
        set { ManjurinamaGFatherFNameEng = value; }
    }

    [Column(Name = "MANJURINAMA_F_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_FULLNAME_LOC
    {
        get { return ManjurinamaFatherFullNameLoc; }
        set { ManjurinamaFatherFullNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_F_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_FULLNAME_ENG
    {
        get { return ManjurinamaFatherFullNameEng; }
        set { ManjurinamaFatherFullNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_LNAME_LOC
    {
        get { return ManjurinamaFatherLNameLoc; }
        set { ManjurinamaFatherLNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_MNAME_LOC
    {
        get { return ManjurinamaFatherMNameLoc; }
        set { ManjurinamaFatherMNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_FNAME_LOC
    {
        get { return ManjurinamaFatherFNameLoc; }
        set { ManjurinamaFatherFNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_LNAME_ENG
    {
        get { return ManjurinamaFatherLNameEng; }
        set { ManjurinamaFatherLNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_MNAME_ENG
    {
        get { return ManjurinamaFatherMNameEng; }
        set { ManjurinamaFatherMNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_FATHER_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FATHER_FNAME_ENG
    {
        get { return ManjurinamaFatherFNameEng; }
        set { ManjurinamaFatherFNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_BIRTH_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_BIRTH_DT_LOC
    {
        get { return ManjurinamaBirthDateLoc; }
        set { ManjurinamaBirthDateLoc = value; }
    }
    [Column(Name = "MANJURINAMA_BIRTH_DT", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_BIRTH_DT
    {
        get { return ManjurinamaBirthDate; }
        set { ManjurinamaBirthDate = value; }
    }
    [Column(Name = "MANJURINAMA_IDENT_ISS_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_IDENTIFICATION_ISS_DT_LOC
    {
        get { return ManjurinamaIdentityIssueDateLoc; }
        set { ManjurinamaIdentityIssueDateLoc = value; }
    }
    [Column(Name = "MANJURINAMA_IDENT_ISSUE_DT", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_IDENTIFICATION_ISSUE_DT
    {
        get { return ManjurinamaIdentityIssueDate; }
        set { ManjurinamaIdentityIssueDate = value; }
    }
    [Column(Name = "MANJURINAMA_IDENT_ISS_DIS_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? MANJURINAMA_IDENTIFICATION_ISSUE_DIS_CD
    {
        get { return ManjurinamaIdentityIssueDisCd; }
        set { ManjurinamaIdentityIssueDisCd = value; }
    }
    [Column(Name = "MANJURINAMA_IDENTITY_NO", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_IDENTITY_NO
    {
        get { return ManjurinamaIdentityNo; }
        set { ManjurinamaIdentityNo = value; }
    }
    [Column(Name = "MANJURINAMA_IDENTITY_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? MANJURINAMA_IDENTITY_TYPE_CD
    {
        get { return ManjurinamaIdentityTypeCd; }
        set { ManjurinamaIdentityTypeCd = value; }
    }
    [Column(Name = "MANJURINAMA_AREA_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_AREA_LOC
    {
        get { return ManjurinamaAreaLoc; }
        set { ManjurinamaAreaLoc = value; }
    }
    [Column(Name = "MANJURINAMA_AREA_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_AREA_ENG
    {
        get { return ManjurinamaAreaEng; }
        set { ManjurinamaAreaEng = value; }
    }
    [Column(Name = "MANJURINAMA_WARD_NO", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_WARD_NO
    {
        get { return ManjurinamaWard; }
        set { ManjurinamaWard = value; }
    }
    [Column(Name = "MANJURINAMA_VDC_CD", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_VDC_CD
    {
        get { return ManjurinamaVdcMunCd; }
        set { ManjurinamaVdcMunCd = value; }
    }
    [Column(Name = "MANJURINAMA_DISTRICT_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? MANJURINAMA_DISTRICT_CD
    {
        get { return ManjurinamaDistrictCd; }
        set { ManjurinamaDistrictCd = value; }
    }
    [Column(Name = "MANJURINAMA_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FULLNAME_LOC
    {
        get { return ManjurinamaFullNameLoc; }
        set { ManjurinamaFullNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_LNAME_LOC
    {
        get { return ManjurinamaLNameLoc; }
        set { ManjurinamaLNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_MNAME_LOC
    {
        get { return ManjurinamaMNameLoc; }
        set { ManjurinamaMNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FNAME_LOC
    {
        get { return ManjurinamaFNameLoc; }
        set { ManjurinamaFNameLoc = value; }
    }
    [Column(Name = "MANJURINAMA_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FULLNAME_ENG
    {
        get { return ManjurinamaFullNameEng; }
        set { ManjurinamaFullNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_LNAME_ENG
    {
        get { return ManjurinamaLNameEng; }
        set { ManjurinamaLNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_MNAME_ENG
    {
        get { return ManjurinamaMNameEng; }
        set { ManjurinamaMNameEng = value; }
    }
    [Column(Name = "MANJURINAMA_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MANJURINAMA_FNAME_ENG
    {
        get { return ManjurinamaFNameEng; }
        set { ManjurinamaFNameEng = value; }
    }

    [Column(Name = "ISMANJURINAMA_AVAIL", IsKey = true, SequenceName = "")]
    public System.String ISMANJURINAMA_AVAIL
    {
        get { return isManjurinamaAvail; }
        set { isManjurinamaAvail = value; }
    }

    [Column(Name = "BUILDING_OTHER_DEG", IsKey = true, SequenceName = "")]
    public System.String bulOtherTyp
    {
        get { return BulOtherTyp; }
        set { BulOtherTyp = value; }
    }

    [Column(Name = "BUILDING_FLOOR_ROOF_TYPE", IsKey = true, SequenceName = "")]
    public System.String bulFloorRoofTyp
    {
        get { return BulFloorRoofTyp; }
        set { BulFloorRoofTyp = value; }
    }



      [Column(Name = "MAP_APROVED_NO", IsKey = true, SequenceName = "")]
    public System.String MapapprovedNo
    {
        get { return MapapprovedNo1; }
        set { MapapprovedNo1 = value; }
    }


    [Column(Name = "BUILDING_PILER_TYPE", IsKey = true, SequenceName = "")]
    public System.String buildingPilarTyp
    {
        get { return BuildingPilarTyp; }
        set { BuildingPilarTyp = value; }
    }


    [Column(Name = "BUILDING_DEG_CAT_NO", IsKey = true, SequenceName = "")]
    public System.Decimal ? bulDesignCatNo
    {
        get { return BulDesignCatNo; }
        set { BulDesignCatNo = value; }
    }

    [Column(Name = "IS_BUILDING_DEG_FROM_CAT", IsKey = true, SequenceName = "")]
    public System.String isBulDesignFrmCatalog
    {
        get { return IsBulDesignFrmCatalog; }
        set { IsBulDesignFrmCatalog = value; }
    }
    [Column(Name = "IS_BUILDING_OWN_DESIGN", IsKey = true, SequenceName = "")]
    public System.String IS_BUILDING_OWN_DESIGN
    {
        get { return IsBulOwnDesign; }
        set { IsBulOwnDesign = value; }
    }


    [Column(Name = "BENEFICIARY_MIGRATION_NO", IsKey = true, SequenceName = "")]
    public System.Decimal? benfMigrationNo
    {
        get { return BenfMigrationNo; }
        set { BenfMigrationNo = value; }
    }
    [Column(Name = "BENEFICIARY_MIGRATION_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String benfMigratnDateLoc
    {
        get { return BenfMigratnDateLoc; }
        set { BenfMigratnDateLoc = value; }
    }
    [Column(Name = "BENEFICIARY_MIGRATTION_DT", IsKey = true, SequenceName = "")]
    public System.String benfMigrationDate
    {
        get { return BenfMigrationDate; }
        set { BenfMigrationDate = value; }
    }

    [Column(Name = "BENEFICIARY_DOB_ENG", IsKey = true, SequenceName = "")]
    public System.String BENEFICIARY_DOB_ENG
    {
        get { return Benfdobeng; }
        set { Benfdobeng = value; }
    }
    [Column(Name = "BENEFICIARY_DOB_LOC", IsKey = true, SequenceName = "")]
    public System.String BENEFICIARY_DOB_LOC
    {
        get { return Benfdobloc; }
        set { Benfdobloc = value; }
    }
    [Column(Name = "BENEFICIARY_PHONE", IsKey = true, SequenceName = "")]
    public System.String BENEFICIARY_PHONE
    {
        get { return BenfPhone; }
        set { BenfPhone = value; }
    }
    [Column(Name = "BENEFICIARY_PHOTO", IsKey = true, SequenceName = "")]
    public System.String BENEFICIARY_PHOTO
    {
        get { return BenfPhoto; }
        set { BenfPhoto = value; }
    }

    [Column(Name = "IS_BENEFICIARY_MIGRATED", IsKey = true, SequenceName = "")]
    public System.String isBeneficiaryMigrated
    {
        get { return IsBeneficiaryMigrated; }
        set { IsBeneficiaryMigrated = value; }
    }

    //[Column(Name = "GUARDIAN_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    //public System.Decimal ? guardianRelTypCd
    //{
    //    get { return GuardianRelTypCd; }
    //    set { GuardianRelTypCd = value; }
    //}

    //[Column(Name = "GUARDIAN_WARD_NO", IsKey = true, SequenceName = "")]
    //public System.Decimal ? guardianWardNo
    //{
    //    get { return GuardianWardNo; }
    //    set { GuardianWardNo = value; }
    //}


    //[Column(Name = "GUARDIAN_VDC_CD", IsKey = true, SequenceName = "")]
    //public System.Decimal ? guardianVdcCd
    //{
    //    get { return GuardianVdcCd; }
    //    set { GuardianVdcCd = value; }
    //}


    //[Column(Name = "GUARDIAN_DISTRICT_CD", IsKey = true, SequenceName = "")]
    //public System.Decimal? guardianDistrictCd
    //{
    //    get { return GuardianDistrictCd; }
    //    set { GuardianDistrictCd = value; }
    //}

    //[Column(Name = "GUARDIAN_IDENTITY_NO", IsKey = true, SequenceName = "")]
    //public System.String guardianIdentityNo
    //{
    //    get { return GuardianIdentityNo; }
    //    set { GuardianIdentityNo = value; }
    //}

    //[Column(Name = "GUARDIAN_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    //public System.String guardianFullNameLoc
    //{
    //    get { return GuardianFullNameLoc; }
    //    set { GuardianFullNameLoc = value; }
    //}

    //[Column(Name = "GUARDIAN_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    //public System.String guardianFullNameEng
    //{
    //    get { return GuardianFullNameEng; }
    //    set { GuardianFullNameEng = value; }
    //}


    //[Column(Name = "mode", IsKey= true, SequenceName="")]
    //public System.String Modee
    //{
    //    get { return Mode; }
    //    set { Mode = value; }
    //}

    [Column(Name = "FISCAL_YR", IsKey = true, SequenceName = "")]
    public System.String FiscalYr
    {
        get { return FiscalYear; }
        set { FiscalYear = value; }
    }

    [Column(Name="TARGET_BATCH_ID",IsKey=true,SequenceName="")]
    public System.Decimal? TargtBatchId
    {
        get{return TargtBtchId ;}
        set{TargtBtchId=value;}
    }

    [Column(Name = "TARGETING_ID", IsKey = true, SequenceName = "")]
    public System.Decimal? TargetingId
    {
        get { return TargtingId; }
        set { TargtingId = value; }
    }

    [Column(Name = "ENROLLMENT_ID", IsKey = true, SequenceName = "")]
    public System.Decimal? EnrollmntId
    {
        get { return EnrollmentId; }
        set { EnrollmentId = value; }
    }

    [Column(Name = "NRA_DEFINED_CD", IsKey = true, SequenceName = "")]
    public System.String NRA_DEFINED_CD
    {
        get { return nradefinedcd; }
        set { nradefinedcd = value; }
    }

    [Column(Name = "MOU_ID", IsKey = true, SequenceName = "")]
    public System.String MOUId
    {
        get { return MOUID; }
        set { MOUID = value; }
    }

    [Column(Name = "DEFINED_MOU_ID", IsKey = true, SequenceName = "")]
    public System.String DefinedMouID
    {
        get { return DefinedMouId; }
        set { DefinedMouId = value; }
    }

    [Column(Name = "SURVEY_NO", IsKey = true, SequenceName = "")]
    public System.String SurveyNO
    {
        get { return SurveyNo; }
        set { SurveyNo = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_DAY", IsKey = true, SequenceName = "")]
    public System.Decimal? EnrollmentMouDay
    {
        get { return EnrollMouDay; }
        set { EnrollMouDay = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_MONTH", IsKey = true, SequenceName = "")]
    public System.Decimal? EnrollmentMouMnth
    {
        get { return EnrollMouMnth; }
        set { EnrollMouMnth = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_YEAR", IsKey = true, SequenceName = "")]
    public System.Decimal? EnrollmentMouYr
    {
        get { return EnrollMouYr; }
        set { EnrollMouYr = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_DT", IsKey = true, SequenceName = "")]
    public System.String EnrollmentMouDt
    {
        get { return EnrollMouDt; }
        set { EnrollMouDt = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_DAY_LOC", IsKey = true, SequenceName = "")]
    public System.String EnrollmentMouDayLoc
    {
        get { return EnrollMouDayLoc; }
        set { EnrollMouDayLoc = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_MONTH_LOC", IsKey = true, SequenceName = "")]
    public System.String EnrollmentMouMnthLoc
    {
        get { return EnrollMouMnthLoc; }
        set { EnrollMouMnthLoc = value; }
    }


    [Column(Name = "ENROLLMENT_MOU_YEAR_LOC", IsKey = true, SequenceName = "")]
    public System.String EnrollmentMouYrLoc
    {
        get { return EnrollMouYrLoc; }
        set { EnrollMouYrLoc = value; }
    }

    [Column(Name = "ENROLLMENT_MOU_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String EnrollmentMouDtLoc
    {
        get { return EnrollMouDtLoc; }
        set { EnrollMouDtLoc = value; }
    }

    [Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
    public System.String HouseOwnerID
    {
        get { return HouseOwnerId; }
        set { HouseOwnerId = value; }
    }

    [Column(Name = "HO_MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String HoMemID
    {
        get { return HoMemId; }
        set { HoMemId = value; }
    }


    [Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
    public System.String HoHldID
    {
        get { return HoHldId; }
        set { HoHldId = value; }
    }

    [Column(Name = "HOUSEHOLD_DEFINED_CD", IsKey = true, SequenceName = "")]
    public System.String HOHldDefCD
    {
        get { return HOHldDefCd; }
        set { HOHldDefCd = value; }
    }

    [Column(Name = "BENEFICIARY_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryTypCD
    {
        get { return BeneficiaryTypCd; }
        set { BeneficiaryTypCd = value; }
    }

    [Column(Name = "MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String MemID
    {
        get { return MemId; }
        set { MemId = value; }
    }

    [Column(Name = "BENEFICIARY_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryFirstNmEng
    {
        get { return BeneficiaryFNmEng; }
        set { BeneficiaryFNmEng = value; }
    }

    [Column(Name = "BENEFICIARY_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryMiddleNmEng
    {
        get { return BeneficiaryMNmEng; }
        set { BeneficiaryMNmEng = value; }
    }


    [Column(Name = "BENEFICIARY_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryLastNmEng
    {
        get { return BeneficiaryLNmEng; }
        set { BeneficiaryLNmEng = value; }
    }
    [Column(Name = "BENEFICIARY_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryFullNameEng
    {
        get { return BeneficiaryFullNmEng; }
        set { BeneficiaryFullNmEng = value; }
    }




    [Column(Name = "BENEFICIARY_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryFistNmLoc
    {
        get { return BeneficiaryFNmLoc; }
        set { BeneficiaryFNmLoc = value; }
    }

    [Column(Name = "BENEFICIARY_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryMiddleNmLoc
    {
        get { return BeneficiaryMNmLoc; }
        set { BeneficiaryMNmLoc = value; }
    }

    [Column(Name = "BENEFICIARY_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryLastNmLoc
    {
        get { return BeneficiaryLNmLoc; }
        set { BeneficiaryLNmLoc = value; }
    }

    [Column(Name = "BENEFICIARY_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryFullNameLoc
    {
        get { return BeneficiaryFullNmLoc; }
        set { BeneficiaryFullNmLoc = value; }
    }

    [Column(Name = "BENEFICIARY_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BeneficiaryRelnTypCD
    {
        get { return BeneficiaryRelnTypCd; }
        set { BeneficiaryRelnTypCd = value; }
    }

    [Column(Name = "REGION_STATE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? RegnStateCD
    {
        get { return RegnStateCd; }
        set { RegnStateCd = value; }
    }

    [Column(Name = "ZONE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? ZoneCD
    {
        get { return ZoneCd; }
        set { ZoneCd = value; }
    }

    [Column(Name = "DISTRICT_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? DistrictCD
    {
        get { return DistrictCd; }
        set { DistrictCd = value; }
    }


    [Column(Name = "VDC_MUN_CD", IsKey = true, SequenceName = "")]
    public System.String VdcMunCD 
    {
        get { return VdcMunCd; }
        set { VdcMunCd = value; }
    }

    [Column(Name = "WARD_NO", IsKey = true, SequenceName = "")]
    public System.Decimal? WardNO
    {
        get { return WardNo; }
        set { WardNo = value; }
    }

    [Column(Name = "ENUMERATION_AREA", IsKey = true, SequenceName = "")]
    public System.String EnumArea
    {
        get { return EnumerationArea; }
        set { EnumerationArea = value; }
    }

    [Column(Name = "AREA_ENG", IsKey = true, SequenceName = "")]
    public System.String AreaENG
    {
        get { return AreaEng; }
        set { AreaEng = value; }
    }

    [Column(Name = "AREA_LOC", IsKey = true, SequenceName = "")]
    public System.String AreaLOC
    {
        get { return AreaLoc; }
        set { AreaLoc = value; }
    }

    [Column(Name = "FATHER_MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String FatherMemID
    {
        get { return FatherMemId; }
        set { FatherMemId = value; }
    }

    [Column(Name = "FATHER_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FatherFirstnameEng
    {
        get { return FatherFnameEng; }
        set { FatherFnameEng = value; }
    }

    [Column(Name = "FATHER_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FatherMiddlenameEng
    {
        get { return FatherMnameEng; }
        set { FatherMnameEng = value; }
    }

    [Column(Name = "FATHER_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FatherLastnameEng
    {
        get { return FatherLnameEng; }
        set { FatherLnameEng = value; }
    }

    [Column(Name = "FATHER_FullNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FatherFullNameEng
    {
        get { return FatherFullnameEng; }
        set { FatherFullnameEng = value; }
    }



    [Column(Name = "FATHER_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FatherFirstnameLoc
    {
        get { return FatherFnameLoc; }
        set { FatherFnameLoc = value; }
    }

    [Column(Name = "FATHER_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FatherMiddlenameLoc
    {
        get { return FatherMnameLoc; }
        set { FatherMnameLoc = value; }
    }

    [Column(Name = "FATHER_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FatherLastnameLoc
    {
        get { return FatherLnameLoc; }
        set { FatherLnameLoc = value; }
    }

    [Column(Name = "FATHER_FullNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FatherFullNameLoc
    {
        get { return FatherFullnameLoc; }
        set { FatherFullnameLoc = value; }
    }

    [Column(Name="GFATHER_MEMBER_ID",IsKey=true,SequenceName="")]
    public System.String GFatherMemID
    {
        get { return GFatherMemId; }
        set { GFatherMemId = value; }
    }

    [Column(Name = "GFATHER_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String GFatherFirstnameEng
    {
        get { return GFatherFnameEng; }
        set { GFatherFnameEng = value; }
    }

    [Column(Name = "GFATHER_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String GFatherMiddlenameEng
    {
        get { return GFatherMnameEng; }
        set { GFatherMnameEng = value; }
    }

    [Column(Name = "GFATHER_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String GFatherLastnameEng
    {
        get { return GFatherLnameEng; }
        set { GFatherLnameEng = value; }
    }

    [Column(Name = "GFATHER_FullNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String GFatherFullNameEng
    {
        get { return GFatherFullnameEng; }
        set { GFatherFullnameEng = value; }
    }



    [Column(Name = "GFATHER_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String GFatherFirstnameLoc
    {
        get { return GFatherFnameLoc; }
        set { GFatherFnameLoc = value; }
    }

    [Column(Name = "GFATHER_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String GFatherMiddlenameLoc
    {
        get { return GFatherMnameLoc; }
        set { GFatherMnameLoc = value; }
    }

    [Column(Name = "GFATHER_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String GFatherLastnameLoc
    {
        get { return GFatherLnameLoc; }
        set { GFatherLnameLoc = value; }
    }

    [Column(Name = "GFATHER_FullNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String GFatherFullNameLoc
    {
        get { return GFatherFullnameLoc; }
        set { GFatherFullnameLoc = value; }
    }

    [Column(Name = "FATHER_INLAW_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_FNAME_ENG
    {
        get { return FinlawFnameEng; }
        set { FinlawFnameEng = value; }
    }
    [Column(Name = "FATHER_INLAW_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_FNAME_LOC
    {
        get { return FinlawFnameLoc; }
        set { FinlawFnameLoc = value; }
    }
    [Column(Name = "FATHER_INLAW_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_MNAME_ENG
    {
        get { return FinlawMnameEng; }
        set { FinlawMnameEng = value; }
    }
    [Column(Name = "FATHER_INLAW_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_MNAME_LOC
    {
        get { return FinlawMnameLoc; }
        set { FinlawMnameLoc = value; }
    }
    [Column(Name = "FATHER_INLAW_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_LNAME_ENG
    {
        get { return FinlawLnameEng; }
        set { FinlawLnameEng = value; }
    }
    [Column(Name = "FATHER_INLAW_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_LNAME_LOC
    {
        get { return FinlawLnameLoc; }
        set { FinlawLnameLoc = value; }
    }
    [Column(Name = "FATHER_INLAW_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_FULLNAME_ENG
    {
        get { return FinLawFullNameEng; }
        set { FinLawFullNameEng = value; }
    }
    [Column(Name = "FATHER_INLAW_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FATHER_INLAW_FULLNAME_LOC
    {
        get { return FinLawFullNameLoc; }
        set { FinLawFullNameLoc = value; }
    }
    [Column(Name = "SPOUSE_FIRST_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_FIRST_NAME_ENG
    {
        get { return husbandfnameeng; }
        set { husbandfnameeng = value; }
    }
    [Column(Name = "SPOUSE_MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_MEMBER_ID
    {
        get { return husbandmemberid; }
        set { husbandmemberid = value; }
    }
    [Column(Name = "SPOUSE_FIRST_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_FIRST_NAME_LOC
    {
        get { return husbandfnameloc; }
        set { husbandfnameloc = value; }
    }
    [Column(Name = "SPOUSE_MIDDLE_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_MIDDLE_NAME_ENG
    {
        get { return husbandMnameeng; }
        set { husbandMnameeng = value; }
    }
    [Column(Name = "SPOUSE_MIDDLE_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_MIDDLE_NAME_LOC
    {
        get { return husbandMnameloc; }
        set { husbandMnameloc = value; }
    }
    [Column(Name = "SPOUSE_LAST_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_LAST_NAME_ENG
    {
        get { return husbandLnameeng; }
        set { husbandLnameeng = value; }
    }
    [Column(Name = "SPOUSE_LAST_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_LAST_NAME_LOC
    {
        get { return husbandLnameloc; }
        set { husbandLnameloc = value; }
    }
    
    [Column(Name = "SPOUSE_FULL_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_FULL_NAME_ENG
    {
        get { return husbandFullnameEng; }
        set { husbandFullnameEng = value; }
    }
    [Column(Name = "SPOUSE_FULL_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String SPOUSE_FULL_NAME_LOC
    {
        get { return husbandFullnameLoc; }
        set { husbandFullnameLoc = value; }
    }
    [Column(Name = "FATHER_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? FATHER_RELATION_TYPE_CD
    {
        get { return fatherRelationtypecd; }
        set { fatherRelationtypecd = value; }
    }

    [Column(Name = "GFATHER_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? GFATHER_RELATION_TYPE_CD
    {
        get { return gfatherRelationtypecd; }
        set { gfatherRelationtypecd = value; }
    }


    [Column(Name = "IDENTIFICATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? IdentificationTypeCD
    {
        get { return IdentificationTypCD; }
        set { IdentificationTypCD = value; }
    }

    [Column(Name = "IDENTIFICATION_NO", IsKey = true, SequenceName = "")]
    public System.String IdentificationNO
    {
        get { return IdentificationNo; }
        set { IdentificationNo = value; }
    }
     [Column(Name = "BENEFICIARY_CTZ_NO", IsKey = true, SequenceName = "")]
    public System.String BeneficiaryctzNO
    {
        get { return beneficiaryctznoo; }
        set { beneficiaryctznoo = value; }
    }
    

    [Column(Name = "IDENTIFICATION_DOCUMENT", IsKey = true, SequenceName = "")]
    public System.String IdentificationDOC
    {
        get { return IdentificationDoc; }
        set { IdentificationDoc = value; }
    }

    [Column(Name = "IDENTIFICATION_ISSUE_DIS_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? IdentificationIssDisCD
    {
        get { return IdentificationIssDisCd; }
        set { IdentificationIssDisCd = value; }
    }

    [Column(Name = "IDENTIFICATION_ISSUE_DAY", IsKey = true, SequenceName = "")]
    public System.Decimal? IdentificationIssueDay
    {
        get { return IdentificationIssDay; }
        set { IdentificationIssDay = value; }
    }

    [Column(Name = "IDENTIFICATION_ISSUE_MONTH", IsKey = true, SequenceName = "")]
    public System.Decimal? IdentificationIssueMnth
    {
        get { return IdentificationIssMnth; }
        set { IdentificationIssMnth = value; }
    }

    [Column(Name = "IDENTIFICATION_ISSUE_YEAR", IsKey = true, SequenceName = "")]
    public System.Decimal? IdentificationIssueYr
    {
        get { return IdentificationIssYr; }
        set { IdentificationIssYr = value; }
    }

    [Column(Name = "IDENTIFICATION_ISSUE_DT", IsKey = true, SequenceName = "")]
    public System.String IdentificationIssueDt
    {
        get { return IdentificationIssDt; }
        set { IdentificationIssDt = value; }
    }
    [Column(Name = "IDENTIFICATION_ISS_DAY_LOC", IsKey = true, SequenceName = "")]
    public System.String IdentificationIssueDayLoc
    {
        get { return IdentificationIssDayLoc; }
        set { IdentificationIssDayLoc = value; }
    }

    [Column(Name = "IDENTIFICATION_ISS_MNTH_LOC", IsKey = true, SequenceName = "")]
    public System.String IdentificationIssueMnthLoc
    {
        get { return IdentificationIssMnthLoc; }
        set { IdentificationIssMnthLoc = value; }
    }

    [Column(Name = "IDENTIFICATION_ISS_YEAR_LOC", IsKey = true, SequenceName = "")]
    public System.String IdentificationIssueYrLoc
    {
        get { return IdentificationIssYrLoc; }
        set { IdentificationIssYrLoc = value; }
    }

    [Column(Name = "IDENTIFICATION_ISS_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String IdentificationIssueDtLoc
    {
        get { return IdentificationIssDtLoc; }
        set { IdentificationIssDtLoc = value; }
    }

    [Column(Name = "ENUMERATOR_ID", IsKey = true, SequenceName = "")]
    public System.String EnumeratorID
    {
        get { return EnumeratorId; }
        set { EnumeratorId = value; }
    }

    [Column(Name = "BUILDING_KITTA_NUMBER", IsKey = true, SequenceName = "")]
    public System.String BuildingKittaNO
    {
        get { return BuildingKittaNo; }
        set { BuildingKittaNo = value; }
    }

    [Column(Name = "BUILDING_AREA", IsKey = true, SequenceName = "")]
    public System.String BuildingAREA
    {
        get { return BuildingArea; }
        set { BuildingArea = value; }
    }

    [Column(Name = "BUILDING_DISTRICT_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BuildingDistCD
    {
        get { return BuildingDistCd; }
        set { BuildingDistCd = value; }
    }


    [Column(Name = "BUILDING_VDC_MUN_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BuildingVdcMunCD 
    {
        get { return BuildingVdcMunCd; }
        set { BuildingVdcMunCd = value; }
    }

    [Column(Name = "BUILDING_WARD_NO", IsKey = true, SequenceName = "")]
    public System.Decimal? BuildingWardNO
    {
        get { return BuildingWardNo; }
        set { BuildingWardNo = value; }
    }

    [Column(Name = "BUILDING_AREA_ENG", IsKey = true, SequenceName = "")]
    public System.String BuildingAreaENG
    {
        get { return BuildingAreaEng; }
        set { BuildingAreaEng = value; }
    }

    [Column(Name = "BUILDING_AREA_LOC", IsKey = true, SequenceName = "")]
    public System.String BuildingAreaLOC
    {
        get { return BuildingAreaLoc; }
        set { BuildingAreaLoc = value; }
    }

    [Column(Name = "NOMINEE_MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String NomineeMemID
    {
        get { return NomineeMemId; }
        set { NomineeMemId = value; }
    }

    [Column(Name = "NOMINEE_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String NomineeFirstnamEng
    {
        get { return NomineeFnamEng; }
        set { NomineeFnamEng = value; }
    }

    [Column(Name = "NOMINEE_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String NomineeMiddlenamEng
    {
        get { return NomineeMnamEng; }
        set { NomineeMnamEng = value; }
    }

    [Column(Name = "NOMINEE_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String NomineeLastnamEng
    {
        get { return NomineeLnamEng; }
        set { NomineeLnamEng = value; }
    }

    [Column(Name = "NOMINEE_FULLNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String NomineeFullnameEng
    {
        get { return NomineeFullnamEng; }
        set { NomineeFullnamEng = value; }
    }

    
    [Column(Name = "NOMINEE_FNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String NomineeFirstnamLoc
    {
        get { return NomineeFnamLoc; }
        set { NomineeFnamLoc = value; }
    }

    [Column(Name = "NOMINEE_MNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String NomineeMiddlenamLoc
    {
        get { return NomineeMnamLoc; }
        set { NomineeMnamLoc = value; }
    }

    [Column(Name = "NOMINEE_LNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String NomineeLastnamLoc
    {
        get { return NomineeLnamLoc; }
        set { NomineeLnamLoc = value; }
    }

    [Column(Name = "NOMINEE_FULLNAME_LOC", IsKey = true, SequenceName = "")]
    public System.String NomineeFullnameLoc
    {
        get { return NomineeFullnamLoc; }
        set { NomineeFullnamLoc = value; }
    }

    [Column(Name = "NOMINEE_RELATION_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? NomineeRelnTypCD
    {
        get { return NomineeRelnTypCd; }
        set { NomineeRelnTypCd = value; }
    }

    [Column(Name = "EMPLOYEE_CD", IsKey = true, SequenceName = "")]
    public System.String EmployCD
    {
        get { return EmployCd; }
        set { EmployCd = value; }
    }

    [Column(Name = "OFFICE_CD", IsKey = true, SequenceName = "")]
    public System.String OfficeCD
    {
        get { return OfficeCd; }
        set { OfficeCd = value; }
    }

    [Column(Name = "REMARKS", IsKey = true, SequenceName = "")]
    public System.String Remark
    {
        get { return Remarks; }
        set { Remarks = value; }
    }

    [Column(Name = "REMARKS_LOC", IsKey = true, SequenceName = "")]
    public System.String RemarkLoc
    {
        get { return RemarksLoc; }
        set { RemarksLoc = value; }
    }

    [Column(Name = "BANK_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BankCD
    {
        get { return BankCd; }
        set { BankCd = value; }
    }

    [Column(Name = "BANK_BRANCH_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BankBrnchCD
    {
        get { return BankBrnchCd; }
        set { BankBrnchCd = value; }
    }

    [Column(Name = "BANK_ACC_NO", IsKey = true, SequenceName = "")]
    public System.String BankAccNO
    {
        get { return BankAccNo; }
        set { BankAccNo = value; }
    }

    [Column(Name = "BANK_ACC_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? BankAccTypCD 
    {
        get { return BankAccTypCd; }
        set { BankAccTypCd = value; }
    }

    [Column(Name = "IS_PAYMENT_RECEIVER_CHANGED", IsKey = true, SequenceName = "")]
    public System.String IsPaymentReceiverChanged 
    {
        get { return IsPaymentReceiverChngd; }
        set { IsPaymentReceiverChngd = value; }
    }

    [Column(Name = "CHANGED_REASON_ENG", IsKey = true, SequenceName = "")]
    public System.String ChngedReasonEng
    {
        get { return ChangedReasonEng; }
        set { ChangedReasonEng = value; }
    }

    [Column(Name = "CHANGED_REASON_LOC", IsKey = true, SequenceName = "")]
    public System.String ChngedReasonLoc
    {
        get { return ChangedReasonLoc; }
        set { ChangedReasonLoc = value; }
    }

    [Column(Name = "APPROVED", IsKey = true, SequenceName = "")]
    public System.String ApproveD
    {
        get { return Approved; }
        set { Approved = value; }
    }
    [Column(Name = "APPROVED_BY", IsKey = true, SequenceName = "")]
    public System.String ApprovedBY
    {
        get { return ApprovedBy; }
        set { ApprovedBy = value; }
    }

    [Column(Name = "APPROVED_DT", IsKey = true, SequenceName = "")]
    public System.String ApprovedDT
    {
        get { return ApprovedDt; }
        set { ApprovedDt = value; }
    }

    [Column(Name = "APPROVED_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String ApprovedDTLOC
    {
        get { return ApprovedDtLoc; }
        set { ApprovedDtLoc = value; }
    }

    [Column(Name = "UPDATED_BY", IsKey = true, SequenceName = "")]
    public System.String UpdatedBY
    {
        get { return UpdatedBy; }
        set { UpdatedBy = value; }
    }

    [Column(Name = "UPDATED_DT", IsKey = true, SequenceName = "")]
    public System.String UpdatedDT
    {
        get { return UpdatedDt; }
        set { UpdatedDt = value; }
    }

    [Column(Name = "UPDATED_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String UpdatedDTLOC
    {
        get { return UpdatedDtLoc; }
        set { UpdatedDtLoc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = true, SequenceName = "")]
    public System.String EnteredBY
    {
        get { return EnteredBy; }
        set { EnteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = true, SequenceName = "")]
    public System.DateTime? EnteredDT
    {
        get { return EnteredDt; }
        set { EnteredDt = value; }
    }

    [Column(Name = "ENTERED_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String EnteredDTLOC
    {
        get { return EnteredDtLoc; }
        set { EnteredDtLoc = value; }
    }
    [Column(Name = "ip_address", IsKey = true, SequenceName = "")]
    public System.String IpAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

    [Column(Name = "PAYROLL_GENERATION_FLAG", IsKey = true, SequenceName = "")]
    public System.String PayrollGenerationFlg
    {
        get { return PayrollGenerationFlag; }
        set { PayrollGenerationFlag = value; }
    }

    [Column(Name = "PAYROLL_BATCH_ID", IsKey = true, SequenceName = "")]
    public System.String PayrollbatchId
    {
        get { return Payrollbatchid; }
        set { Payrollbatchid = value; }
    }

    [Column(Name = "PAYROLL_DTL_ID", IsKey = true, SequenceName = "")]
    public System.String PayrollDtlid
    {
        get { return PayrollDtlID; }
        set { PayrollDtlID = value; }
    }

    [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = true, SequenceName = "")]
    public System.String BUILDING_STRUCTURE_NO
    {
        get { return BuildingStrNo; }
        set { BuildingStrNo = value; }
    }
    [Column(Name = "ACC_HOLD_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String ACC_HOLD_FNAME_ENG
    {

        get { return AccHoldFNameEng; }
        set { AccHoldFNameEng = value; }
    }
    [Column(Name = "ACC_HOLD_MNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String ACC_HOLD_MNAME_ENG
    {
        get { return AccHoldMNameEng; }
        set { AccHoldMNameEng = value; }
    }
    [Column(Name = "ACC_HOLD_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String ACC_HOLD_LNAME_ENG
    {
        get { return AccHoldLNameEng; }
        set { AccHoldLNameEng = value; }
    }

     [Column(Name = "SHAKSHI_FNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SHAKSHI_FNAME_ENG
    {

        get { return SakshiFNameEng; }
        set { SakshiFNameEng = value; }
    }
    [Column(Name = "SHAKSHI_MNAME_ENG", IsKey = true, SequenceName = "")]
     public System.String SHAKSHI_MNAME_ENG
    {
        get { return SakshiMNameEng; }
        set { SakshiMNameEng = value; }
    }
    [Column(Name = "SHAKSHI_LNAME_ENG", IsKey = true, SequenceName = "")]
    public System.String SHAKSHI_LNAME_ENG
    {
        get { return SakshiLNameEng; }
        set { SakshiLNameEng = value; }
    }

    [Column(Name = "CTZN_PIC_NAME", IsKey = true, SequenceName = "")]
    public System.String CTZN_PIC_NAME
    {
        get { return CitizenshipsPicName; }
        set { CitizenshipsPicName = value; }
    }

    public EnrollmentMou()
    {

    }
}

