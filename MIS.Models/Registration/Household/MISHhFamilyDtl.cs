using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhFamilyDtl
    {
        public String householdId { get; set; }
        [Required(ErrorMessage = " ")]
        public String memberId { get; set; }
        public String membID { get; set; }
        public String memberName { get; set; }
        public String firstNameEng { get; set; }
        public String middleNameEng { get; set; }
        public String lastNameEng { get; set; }
        public String firstNameLoc { get; set; }
        public String middleNameLoc { get; set; }
        public String lastNameLoc { get; set; }
        public String distName { get; set; }
        public String birthDt { get; set; }
        public String age { get; set; }
        public String gender { get; set; }
        public String marital_status { get; set; }
        public String citizenshipNo { get; set; }
        public Decimal sno { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? relationTypeCd { get; set; }
        public Decimal? relationTypeCdCheck { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public bool memberActive { get; set; }
        public bool memberDeath { get; set; }
        public bool memberMarriage { get; set; }
        public bool memberSplit { get; set; }
        public String transferHhId { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String editMode { get; set; }
        public String memberCodeCheck { get; set; }
        public String householdCodeCheck { get; set; }
        public Decimal snoCheck { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        public DateTime updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredByLoc { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String ipaddress { get; set; }
        //Date of Birth
        public String birthYrLoc { get; set; }
        public String birthMonLoc { get; set; }
        public String birthDayLoc { get; set; }
        public String birthYrEng { get; set; }
        public String birthMonEng { get; set; }
        public String birthDayEng { get; set; }
        //End Date of Birth
        public String PerCountryCd { get; set; } 
        public String PerDistrictCd { get; set; } 
        public String PerVDCCd { get; set; }
        public String PerWardCd { get; set; }

        public String CtzNo { get; set; }
        public String CtzIssueDistrictCd{ get; set; }
        public String CtzIssueYear { get; set; }
        public String CtzIssueMonth { get; set; }
        public String CtzIssueDay { get; set; }
        public String CtzIssueYearLoc { get; set; }
        public String CtzIssueMonthLoc { get; set; }
        public String CtzIssueDayLoc { get; set; }
        public String Gendercd { get; set; }
        public String MaritalStatusCd { get; set; }
        public String ReligionCd { get; set; }
        public String CasteCd { get; set; }
        public String EducationCd { get; set; }
    }
}