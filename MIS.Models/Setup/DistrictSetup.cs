using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
    public class DistrictSetup
    {
        public string mode { get; set; }
        public decimal? district_cd { get; set; }
        public string defined_cd { get; set; }
        public string desc_eng { get; set; }
        public string desc_loc { get; set; }
        public string short_name { get; set; }
        public string short_name_loc { get; set; }
        public decimal? zone_cd { get; set; }
        public decimal? order_no { get; set; }
        public string approved { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }
    }
}
