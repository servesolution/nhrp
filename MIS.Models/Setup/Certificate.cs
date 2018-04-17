using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
    public class Certificate
    {
        public string mode { get; set; }
        public string certificate_cd { get; set; }
        public string defined_cd { get; set; }
        public string desc_eng { get; set; }
        public string desc_loc { get; set; }
        public string description_eng { get; set; }
        public string description_loc { get; set; }
        public string order_no { get; set; }
        public string active { get; set; }
        public string entered_by { get; set; }
        public string entered_dt { get; set; }
        public string entered_dt_loc { get; set; }
        public string approved { get; set; }
        public string approved_by { get; set; }
        public string approved_dt { get; set; }
        public string approved_dt_loc { get; set; }
        public string updated_by { get; set; }
        public string updated_dt { get; set; }
        public string updated_dt_loc { get; set; }
    }
}
