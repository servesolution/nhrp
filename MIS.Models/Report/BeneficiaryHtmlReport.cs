using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Report
{
    public class BeneficiaryHtmlReport
    {
        public string District { get; set; }
        public string VDC { get; set; }
        public string WARD { get; set; }
        public string CurrentVDC { get; set; }
        public string CurrentWard { get; set; }

        public string Beneficiary { get; set; }
        public string NonBeneficiary { get; set; }
        public string ReportType { get; set; }

        public string WardFlag { get; set; }

        public string Batch { get; set; }
        
        public string ByWard { get; set; }
        public string Coordinate { get; set; }


        public int showing { get; set; }
        public int Index { get; set; }

        public int Total { get; set; }
        public int Remaining { get; set; }

    }
}
