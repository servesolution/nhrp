using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhAllowanceDtl
    {
        public String householdId { get; set; }
        [Required(ErrorMessage = " ")]
        public String memberId { get; set; }
        public String memberName { get; set; }
        public Decimal sno { get; set; }
        public Decimal? relationTypeCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String allSourceCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String allowanceTypeCd { get; set; }
        public DateTime? allowanceStDt { get; set; }
        public String allowanceStDtLoc { get; set; }
        public Decimal? allowanceYears { get; set; }
        public Decimal? allowanceMonth { get; set; }
        public Decimal? allowanceDay { get; set; }
        public Decimal? allowanceLastYr { get; set; }
        public Decimal? allowanceAmt { get; set; }
        public Decimal? distanceHr { get; set; }
        public Decimal? distanceMin { get; set; }
        public Decimal? handiColorCd { get; set; }
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
        public String editMode { get; set; }
        public String memberCodeCheck { get; set; }
        public String householdCodeCheck { get; set; }
        public Decimal snoCheck { get; set; }
        public String ipaddress { get; set; }

        public bool isHandicaped { get; set; }
    }
}
