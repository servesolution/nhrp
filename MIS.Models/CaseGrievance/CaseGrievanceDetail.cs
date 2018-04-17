using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.CaseGrievance
{
    public class CaseGrievanceDetail
    {
        public Decimal? GRIEVANCE_DETAIL_ID { get; set; }
        public string CASE_REGISTRATION_ID { get; set; }
        public string CASE_GRIEVANCE_TYPE_CD { get; set; }
        public string CASE_STATUS { get; set; }
        public string ADDRESSED_OFFICE { get; set; }
        public string FORWARDED_OFFICE { get; set; }
        public string ADDRESSED_DATE_ENG { get; set; }
        public string ADDRESSED_DATE_LOC { get; set; }
        public string REMARKS { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DT { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public string LAST_UPDATED_DT { get; set; }
        public string Mode { get; set; }
    }
}
