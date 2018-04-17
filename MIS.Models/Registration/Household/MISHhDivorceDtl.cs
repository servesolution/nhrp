using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhDivorceDtl
    {
        public String householdId { get; set; }
        [Required(ErrorMessage = " ")]
        public String memberId { get; set; }
        public String birthDt { get; set; }
        public String memberName { get; set; }
        public String relationType { get; set; }
        public Decimal sno { get; set; }
        public Decimal? relationTypeCd { get; set; }
        public bool divorceCertificate { get; set; }
        public String divorceCertificateNo { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceYear { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceMonth { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceDay { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceYearLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceMonthLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? divorceDayLoc { get; set; }
        public DateTime? divorceDt { get; set; }
        public String divorceDtLoc { get; set; }
        public String divorceDoc { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        private DateTime ApprovedDt = DateTime.Now;
        public DateTime approvedDt
        {
            get
            {
                return ApprovedDt;
            }
            set
            {
                ApprovedDt = value;
            }
        }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        private DateTime UpdatedDt = DateTime.Now;
        public DateTime updatedDt
        {
            get
            {
                return UpdatedDt;
            }
            set
            {
                UpdatedDt = value;
            }
        }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String editMode { get; set; }
        public String memberCodeCheck { get; set; }
        public String householdCodeCheck { get; set; }
        public Decimal snoCheck { get; set; }
        public String enteredByLoc { get; set; }
        private DateTime EnteredDt = DateTime.Now;
        public DateTime enteredDt
        {
            get
            {
                return EnteredDt;
            }
            set
            {
                EnteredDt = value;
            }
        }
        public String enteredDtLoc { get; set; }
        public String ipaddress { get; set; }
    }
}