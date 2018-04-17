using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Member
{
    public class MISMemberDoc
    {
        public String memberId { get; set; }
        public Decimal sno { get; set; }
        [Required(ErrorMessage = " ")]
        public String docTypeCd { get; set; }
        public String docId { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
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

        public String memberDefinedId { get; set; }
    }
}
