using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MIS.Models.Registration.Member
{
    public class MISMember
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
        public String fullName { get; set; }
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
        public Decimal? birthYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? birthMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? birthDay { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? birthYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? birthMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? birthDayLoc { get; set; }
        public DateTime birthDt { get; set; }
        public String strBirthDate { get; set; }
        public String ctzDetails { get; set; }
        public String birthDtLoc { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public Decimal? age { get; set; }
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
        public bool birthCertificate { get; set; }
        public String birthCertificateRadio { get; set; }
        public String birthCertificateNo { get; set; }
        public String birthCerDistrictCd { get; set; }
        public bool ctzIssue { get; set; }
        public String ctzIssueRadio { get; set; }
        public String ctzNo { get; set; }
        public String ctzIssueDistrictCd { get; set; }
        public String ctzIssueDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueDay { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? ctzIssueDayLoc { get; set; }
        public DateTime ctzIssueDt { get; set; }
        public String ctzIssueDtLoc { get; set; }
        public bool voterId { get; set; }
        public String voterIdRadio { get; set; }
        public String voteridNo { get; set; }
        public String voteridDistrictCd { get; set; }
        public String voteridDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueDay { get; set; }
        public DateTime voteridIssueDt { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? voterIssueDayLoc { get; set; }
        public String voteridIssueDtLoc { get; set; }
        public String nidNo { get; set; }
        public String nidDistrictCd { get; set; }
        public String nidDistrict { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueDay { get; set; }
        public DateTime nidIssueDt { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public Decimal? nidIssueDayLoc { get; set; }
        public String nidIssueDtLoc { get; set; }
        public Decimal? perCountryCd { get; set; }
        public Decimal? perRegStCd { get; set; }
        public Decimal? perZoneCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String perDistrictCd { get; set; }
        public String perDistrict { get; set; }
        [Required(ErrorMessage = " ")]
        //public Decimal? perVdcMunCd { get; set; }
        public String perVdcMunCd { get; set; }
        public String perVdcMun { get; set; }
        public Decimal? perWardNo { get; set; }
        public String perAreaEng { get; set; }
        public String perAreaLoc { get; set; }
        public Decimal? curCountryCd { get; set; }
        public Decimal? curRegStCd { get; set; }
        public Decimal? curZoneCd { get; set; }
        public String curDistrictCd { get; set; }
        public String curDistrict { get; set; }
        public String curVdcMunCd { get; set; }
        public String curVdcMun { get; set; }
        public Decimal? curWardNo { get; set; }
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

        public bool householdhead { get; set; }
        public bool nidIssue { get; set; }
        public String nidIssueRadio { get; set; }
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
        public bool disability { get; set; }

        public String Mode { get; set; }
        public String Gender { get; set; }



        //public bool birthCertificateflag { get; set; }
        [Required(ErrorMessage = " ")]
        public String birthCertificateDocument { get; set; }

        //public bool citizenCertificateflag { get; set; }
        [Required(ErrorMessage = " ")]
        public String citizenCertificateDocument { get; set; }

        //public bool voterCertificateflag { get; set; }
        [Required(ErrorMessage = " ")]
        public String voterCertificateDocument { get; set; }

        public bool nidCertificateflag { get; set; }
        [Required(ErrorMessage = " ")]
        public String nidCertificateDocument { get; set; }

        public HttpPostedFileBase fileBirthCrt { get; set; }

        public bool IsbirthCertificate { get; set; }

        public String birthCertIssueYearLoc { get; set; }

        public String birthCertIssueMonthLoc { get; set; }

        public String birthCertIssueDayLoc { get; set; }

        public String birthCertIssueDay { get; set; }

        public String birthCertIssueMonth { get; set; }

        public String birthCertIssueYear { get; set; }

        public String MemAge { get; set; }

        public string MemLink { get; set; }

        public bool DeletePhoto { get; set; }
    }
}
