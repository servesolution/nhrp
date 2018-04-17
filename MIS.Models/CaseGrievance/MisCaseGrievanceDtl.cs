using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.CaseGrievance
{
    class MisCaseGrievanceDtl
    {
        public String caseId { get; set; }
        public String sno { get; set; }
        public String caseStatus { get; set; }
        public String caseStatusRemarks { get; set; }
        public String caseStatusDt { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String ipAddress { get; set; }
    }

}
