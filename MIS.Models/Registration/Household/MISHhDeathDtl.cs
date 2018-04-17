using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhDeathDtl
    {
        public String householdId { get; set; }
        public Decimal sno { get; set; }
        public Decimal? relationTypeCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String firstNameEng { get; set; }
        public String middleNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String lastNameEng { get; set; }
        public String memberId { get; set; }
        public String birthDt { get; set; }
        public String memberName { get; set; }
        public String fullNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String firstNameLoc { get; set; }
        public String middleNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String lastNameLoc { get; set; }
        public String fullNameLoc { get; set; }
        public Decimal? genderCd { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? age { get; set; }
        public bool deathCertificate { get; set; }
        public String deathCertificateNo { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? deathYear { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? deathMonth { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? deathDay { get; set; }
        [Required(ErrorMessage = " ")]
        public String deathYearLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String deathMonthLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String deathDayLoc { get; set; }
        public String deathDt { get; set; }
        public String deathDtLoc { get; set; }
        public String deathDoc { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        public DateTime? approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        public DateTime? updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String editMode { get; set; }
        public String memberCodeCheck { get; set; }
        public String householdCodeCheck { get; set; }
        public Decimal snoCheck { get; set; }
        public String enteredBy { get; set; }
        public String enteredByLoc { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String ipaddress { get; set; }
    }
}
