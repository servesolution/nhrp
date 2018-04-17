using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
  public  class PSPUploadLog
    {
      public string Districtcd { get; set; }
      public string VDCcd { get; set; }
      public string Ward { get; set; }
        public string Bankcd { get; set; }
        public string Branchcd { get; set; }
        public string FiscalYr { get; set; }
        public string quarter { get; set; }
        public string UploadDateFrom { get; set; }
        public string UploadDateTo { get; set; }
        public string Tranche { get; set; }
      
    }
}
