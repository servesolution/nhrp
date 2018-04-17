using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.CaseGrievance
{
    public class CaseGrievanceOTHDetail
    {
        public string GRIEVANCE_OTH_DETAIL_ID { get; set; }
        public string CASE_REGISTRATION_ID { get; set; }
        public string HOUSEHOLD_HEAD_FNAME_ENG { get; set; }
        public string HOUSEHOLD_HEAD_MNAME_ENG { get; set; }
        public string HOUSEHOLD_HEAD_LNAME_ENG { get; set; }
        public string HOUSEHOLD_HEAD_FNAME_LOC { get; set; }
        public string HOUSEHOLD_HEAD_MNAME_LOC { get; set; }
        public string HOUSEHOLD_HEAD_LNAME_LOC { get; set; }
        public string HOUSEHOLD_HEAD_FULL_NAME { get; set; }
        public Decimal? HOUSE_DIST_CD { get; set; }
        public String HOUSE_DIST_ENG { get; set; }
        public String HOUSE_DIST_LOC { get; set; }
        public string HOUSE_VDC_MUN_CD { get; set; }
        public String HOUSE_VDC_ENG { get; set; }
        public String HOUSE_VDC_LOC { get; set; }
        public decimal? HOUSE_WARD_NO { get; set; }
        public string HOUSE_AREA { get; set; }
        public decimal? HOUSE_CONDITION_CD { get; set; }
        public string HOUSE_CONDITION_ENG { get; set; }
        public string HOUSE_CONDITION_LOC { get; set; }
        public string Mode { get; set; }
    }
}
