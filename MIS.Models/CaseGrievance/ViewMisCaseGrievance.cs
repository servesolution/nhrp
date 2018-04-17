using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.CaseGrievance
{
    public class ViewCaseGrievanceModel
    {
        public String grievanceCd { get; set; }
       
        public String householdId { get; set; }
        
        public String hhFullName { get; set; }
        public String caseStatus { get; set; }
        public String pinNo { get; set; }
        public String address { get; set; }
        public String phoneNo { get; set; }
        public String caseDesc { get; set; }
        public String signApplicant { get; set; }
        public String signAppDate { get; set; }
        public String signDswOfficer { get; set; }
        public String signDswDate { get; set; }
        public String signSeniorOfficer { get; set; }
        public String signSeniorDate { get; set; }
        public String processRemark { get; set; }
        public String finalRemark { get; set; }
        public String PER_REGION_ENG { get; set; }
        public String PER_Zone_ENG { get; set; }
        public String PER_District_ENG { get; set; }
        public String VDCMun_ENG { get; set; }
        public String PER_Ward_ENG { get; set; }
        
        public String approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedDt { get; set; }
        public String updatedBy { get; set; }
        public String updatedDt { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String RegTypeCd { get; set; }
        public String CategoryName { get; set; }
        public String CategoryGroupName { get; set; }

        //Added Extra field for case registration detail table
        //public String sno { get; set; }
        //public String caseStatusRemarks { get; set; }
        //public String caseStatusDt { get; set; }

    }
}
