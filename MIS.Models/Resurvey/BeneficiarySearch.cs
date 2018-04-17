using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Resurvey
{
    public class BeneficiarySearch
    {
        public String DistrictCd { get; set; }
        public String VDCMunCd { get; set; }
        public string CurrentVdcMunCD { get; set; }
        public string CurrentVdcMunDefCD { get; set; }
        public String WardNo { get; set; }
        public String FiscalYear { get; set; }
        public String Quota { get; set; }
        public String TargetType { get; set; }
        public String BusinessRule { get; set; }
        public String HouseholdIDFrom { get; set; }
        public String HouseholdIDTo { get; set; }
        public String FormNoFrom { get; set; }
        public String FormNoTo { get; set; }
        public String MemberID { get; set; }
        public String BatchID { get; set; }
        public String PageSize { get; set; }
        public String Order_By { get; set; }
        public String ddlApprove { get; set; }
        public String approve_date { get; set; }
        public Boolean Action { get; set; }
    }
}
