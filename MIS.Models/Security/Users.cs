using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace MIS.Models.Security
{
    public class Users
    {

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [RegularExpression(".{6}.*", ErrorMessage = "Password should be at least 6 characters!!")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [RegularExpression(".{6}.*", ErrorMessage = "Password should be at least 6 characters!!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match!")]
        [RegularExpression(".{6}.*", ErrorMessage = "Password should be at least 6 characters!!")]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = " ")]
        public String usrCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String usrName { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression(".{6}.*", ErrorMessage = "Password should be at least 6 characters!!")]
        public String password { get; set; }
        //[Required(ErrorMessage = " ")]
        //[Compare("password", ErrorMessage = "Password Mismatch!!")]
        public String repassword { get; set; }
        public String empCd { get; set; }
        public String status { get; set; }
        public DateTime expiryDt { get; set; }
        public string expiryDtLoc { get; set; }
        public bool internalUsrFlag { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String lastUpdatedBy { get; set; }
        public DateTime lastUpdatedDt { get; set; }
        //[Required(ErrorMessage = " ")]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email ID!!")]
        public String email { get; set; }
        public String userCodeCheck { get; set; }
        public String empCodeCheck { get; set; }
        public String editMode { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int distCode { get; set; }
        public string distName { get; set; }
        public int regStateCode { get; set; }
        public string regStateName { get; set; }
        public int zoneCode { get; set; }
        public string zoneName { get; set; }
        public string officeCode { get; set; }
        public string officeName { get; set; }
        public string positionCode { get; set; }
        public string positionName { get; set; }
        public string positionSubClassCode { get; set; }
        public string positionSubClassName { get; set; }
        public int vdcMunCode { get; set; }
        public string vdcMunName { get; set; }
        public int wardCode { get; set; }
        public string definedCode { get; set; }
        public string fullName { get; set; }
        public string fullNameEng { get; set; }
        public string designationName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string firstNameLoc { get; set; }
        public string middleNameLoc { get; set; }
        public string lastNameLoc { get; set; }
        public String searchLoginName { get; set; }
        public String searchUsrName { get; set; }
        public String searchUsrGroup { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expYear { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expMonth { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expDay { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expYearLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expMonthLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? expDayLoc { get; set; }

        public string VerificationRequired { get; set; }
        public string verificationcode { get; set; }

        [Required(ErrorMessage = " ")]
        public string mobilenumber { get; set; }
        public string verificationvalidity { get; set; }
        public bool isbankUser { get; set; }
        public string bankCode { get; set; }
        public string branchStdCode { get; set; }
        public bool isDonor { get; set; }
        public string donor_cd { get; set; }
        public string Designation { get; set; }

    }
}