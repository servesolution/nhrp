using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Registration.Household
{
    public class MISHHMember
    {
        public String memberId { get; set; }
        public String definedCd { get; set; }
        //[Required(ErrorMessage = " ")]
        public String formNo { get; set; }
        [Required(ErrorMessage = " ")]
        public String firstNameEng { get; set; }
        public String middleNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String lastNameEng { get; set; }
        public String fullNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String firstNameLoc { get; set; }
        public String middleNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String lastNameLoc { get; set; }
        public String fullNameLoc { get; set; }
        public String fatherFnameEng { get; set; }
        public String fatherMnameEng { get; set; }
        public String fatherLnameEng { get; set; }
        public String fatherFullNameEng { get; set; }
        public String fatherFnameLoc { get; set; }
        public String fatherMnameLoc { get; set; }
        public String fatherLnameLoc { get; set; }
        public String fatherFullnameLoc { get; set; }
        public String motherFnameEng { get; set; }
        public String motherMnameEng { get; set; }
        public String motherLnameEng { get; set; }
        public String motherFullNameEng { get; set; }
        public String motherFnameLoc { get; set; }
        public String motherMnameLoc { get; set; }
        public String motherLnameLoc { get; set; }
        public String motherFullnameLoc { get; set; }
        public String gfatherFnameEng { get; set; }
        public String gfatherMnameEng { get; set; }
        public String gfatherLnameEng { get; set; }
        public String gfatherFullNameEng { get; set; }
        public String gfatherFnameLoc { get; set; }
        public String gfatherMnameLoc { get; set; }
        public String gfatherLnameLoc { get; set; }
        public String gfatherFullnameLoc { get; set; }
        public String spouseFnameEng { get; set; }
        public String spouseMnameEng { get; set; }
        public String spouseLnameEng { get; set; }
        public String spouseFullNameEng { get; set; }
        public String spouseFnameLoc { get; set; }
        public String spouseMnameLoc { get; set; }
        public String spouseLnameLoc { get; set; }
        public String spouseFullnameLoc { get; set; }
        public String photoId { get; set; }
        [Required(ErrorMessage = " ")]
        public String genderCd { get; set; }
        public String GenderName { get; set; }
        [Required(ErrorMessage = " ")]
        public String maritalStatusCd { get; set; }
        public String Married { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String birthYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String birthMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String birthDay { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String birthYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String birthMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String birthDayLoc { get; set; }
        public DateTime birthDt { get; set; }
        public String birthDtLoc { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String age { get; set; }
        [Required(ErrorMessage = " ")]
        public String casteCd { get; set; }
        public String Caste { get; set; }
        [Required(ErrorMessage = " ")]
        public String religionCd { get; set; }
        public String Religion { get; set; }
        public bool literate { get; set; }
        [Required(ErrorMessage = " ")]
        public String educationCd { get; set; }
        public String Education { get; set; }
        public String birthCertificate { get; set; }
        public String birthCertificateNo { get; set; }
        public String birthCerDistrictCd { get; set; }
        public String ctzIssue { get; set; }
        public String ctzNo { get; set; }
        public String ctzIssueDistrictCd { get; set; }
        public String ctzIssueDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String ctzIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String ctzIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String ctzIssueDay { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String ctzIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String ctzIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String ctzIssueDayLoc { get; set; }
        public DateTime ctzIssueDt { get; set; }
        public String ctzIssueDtLoc { get; set; }
        public String voterId { get; set; }
        public String voteridNo { get; set; }
        public String voteridDistrictCd { get; set; }
        public String voteridDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String voterIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String voterIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String voterIssueDay { get; set; }
        public DateTime voteridIssueDt { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String voterIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String voterIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String voterIssueDayLoc { get; set; }
        public String voteridIssueDtLoc { get; set; }
        public String NidId { get; set; }
        public String nidNo { get; set; }
        public String nidDistrictCd { get; set; }
        public String nidDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String nidIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String nidIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String nidIssueDay { get; set; }
        public DateTime nidIssueDt { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String nidIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String nidIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String nidIssueDayLoc { get; set; }
        public String nidIssueDtLoc { get; set; }
        public String perCountryCd { get; set; }
        public String perRegStCd { get; set; }
        public String perZoneCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String perDistrictCd { get; set; }
        public String perDistrict { get; set; }
        public String perVdcMunCd { get; set; }
        public String perVdcMun { get; set; }
        public String perWardNo { get; set; }
        public String perAreaEng { get; set; }
        public String perAreaLoc { get; set; }
        public String curCountryCd { get; set; }
        public String curRegStCd { get; set; }
        public String curZoneCd { get; set; }
        public String curDistrictCd { get; set; }
        public String curDistrict { get; set; }
        public String curVdcMunCd { get; set; }
        public String curVdcMun { get; set; }
        public String curWardNo { get; set; }
        public String curAreaEng { get; set; }
        public String curAreaLoc { get; set; }
        public String telNo { get; set; }
        public String mobileNo { get; set; }
        public String fax { get; set; }
        [RegularExpression("^[_a-z0-9-]+(\\.[_a-z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,4})$", ErrorMessage = " ")]
        public String email { get; set; }
        public String url { get; set; }
        public String poBoxNo { get; set; }
        public String passportNo { get; set; }
        public String passportIssueDistrict { get; set; }
        public String passportIssueDistrictDescription { get; set; }
        public String proFundNo { get; set; }
        public String citNo { get; set; }
        public String panNo { get; set; }
        public String drivingLicenseNo { get; set; }
        public bool death { get; set; }
        public bool perAddMigrate { get; set; }
        public bool curAddMigrate { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }

        public String householdId { get; set; }
        public String householdDefinedCd { get; set; }

        public String extraIii { get; set; }
        public String extraiv { get; set; }
        public String extrav { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        public String updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredByLoc { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }

        public String Mode { get; set; }
        public String Gender { get; set; }

         [Required(ErrorMessage = " ")]
        public string relationType { set; get; }
        
        //public relationTypeCd relationTypeCd { get; set; }

        public bool birthCertificateflag { get; set; }

        public bool citizenCertificateflag { get; set; }

        public bool voterCertificateflag { get; set; }

        public bool nidCertificateflag { get; set; }

        public String widowDocNo { get; set; }

        public String DEATH_YEAR_LOC { get; set; }

        public String DEATH_DAY_LOC { get; set; }

        public String DEATH_MONTH_LOC { get; set; }

        public String DEATH_DAY { get; set; }

        public String DEATH_YEAR { get; set; }

        public String DEATH_MONTH { get; set; }

        public String deathDocNo { get; set; }

        public String W_DEATH_YEAR_LOC { get; set; }

        public String W_DEATH_DAY_LOC { get; set; }

        public String W_DEATH_MONTH_LOC { get; set; }

        public String W_DEATH_DAY { get; set; }

        public String W_DEATH_MONTH { get; set; }

        public String W_DEATH_YEAR { get; set; }

        public String DivorceDocNo { get; set; }

        public String D_DEATH_YEAR_LOC { get; set; }

        public String D_DEATH_MONTH_LOC { get; set; }

        public String D_DEATH_DAY_LOC { get; set; }

        public String D_DEATH_DAY { get; set; }

        public String D_DEATH_MONTH { get; set; }

        public String D_DEATH_YEAR { get; set; }

        public String marriageDocNo { get; set; }

        public String M_DEATH_YEAR_LOC { get; set; }

        public String Marriage_YEAR_LOC { get; set; }

        public String Marriage_MONTH_LOC { get; set; }

        public String Marriage_DAY_LOC { get; set; }

        public String Marriage_DAY { get; set; }

        public String Marriage_MONTH { get; set; }

        public String Marriage_YEAR { get; set; }

        public String DIVORCE_YEAR_LOC { get; set; }

        public String DIVORCE_MONTH_LOC { get; set; }

        public String DIVORCE_DAY_LOC { get; set; }

        public String DIVORCE_DAY { get; set; }

        public String DIVORCE_MONTH { get; set; }

        public String DIVORCE_YEAR { get; set; }

        public String School_AREA_LOC { get; set; }

        public String School_WARD_NO { get; set; }

        public String School_VDC_MUN_CD { get; set; }

        public String School_DISTRICT_CD { get; set; }

        public String SCHOOL_TYPE_CD { get; set; }

        public String CLASS_TYPE_CD { get; set; }

        public String SCHOOL_NAME { get; set; }

        public String MSCHOOL_NAME_LOC { get; set; }

        public String schoolDocNo { get; set; }

        public String School_AREA_ENG { get; set; }

        public bool Widowflag { get; set; }

        public String DivorceRelation { get; set; }

        public String SPOUSE_MEMBER_ID { get; set; }

        public String SPOUSE_FULLNAME { get; set; }

        public bool Deathflag { get; set; }

        public bool Divorceflag { get; set; }

        public bool Marrigeflag { get; set; }

        public bool Schoolflag { get; set; }

        public String School_Country_Cd { get; set; }

        public String School_Reg_St_Cd { get; set; }

        public String School_Zone_Cd { get; set; }

        public String WidowCerId { get; set; }

        public String MarriageCerId { get; set; }

        public String DivorceCerId { get; set; }

        public String DeathCerId { get; set; }

        public bool DeathCerflag { get; set; }

        public bool DivorceCerflag { get; set; }

        public bool MarrigeCerflag { get; set; }

        public bool WidowCerflag { get; set; }

        public String householdHead { get; set; }

        public bool IsbirthCertificate { get; set; }

        public bool Allowanceflag { get; set; }

        public String AllowannceSourceId { get; set; }

        public String AllowanceTypeId { get; set; }

        public String AllowanceYear { get; set; }

        public String allowanceLastYr { get; set; }

        public String allowanceAmt { get; set; }

        public String AllowancedistanceHr { get; set; }

        public String AllowancedistanceMin { get; set; }

        public bool isHandicaped { get; set; }

        public String HandiColorId { get; set; }

        public String birthCertIssueYearLoc { get; set; }

        public String birthCertIssueMonthLoc { get; set; }

        public String birthCertIssueDayLoc { get; set; }

        public String birthCertIssueDay { get; set; }

        public String birthCertIssueMonth { get; set; }

        public String birthCertIssueYear { get; set; }
    }
}
