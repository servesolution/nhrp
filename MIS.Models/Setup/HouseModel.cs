using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
    public class HouseModel
    {
        public string mode { get; set; }
        public decimal? model_id { get; set; }
        public string defined_cd { get; set; }
        public string name_eng { get; set; }
        public string name_loc { get; set; }
        public string code_eng { get; set; }
        public string code_loc { get; set; }
        public string desc_eng { get; set; }
        public string desc_loc { get; set; }
        public string approved { get; set; }
        public string approved_by { get; set; }
        public DateTime? approved_dt { get; set; }
        public string approved_dt_loc { get; set; }
        public string entered_by { get; set; }
        public DateTime? entered_dt { get; set; }
        public string entered_dt_loc { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_dt { get; set; }
        public string updated_dt_loc { get; set; }

        public string hierarchy_cd { get; set; }
        public string previous_design_cd { get; set; }


        public string design_image { get; set; }
        public string image_url { get; set; }
        public string design_number { get; set; }
    }
}
