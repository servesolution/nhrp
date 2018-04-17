using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Resurvey
{
    public class ResurveyRecommendation
    {
        public String mode { get; set; }
        public String gid { get; set; }
        public String hoid { get; set; }
        public String mdg { get; set; }
        public String pdg { get; set; }
        public String rdg { get; set; }
        public String rts { get; set; }
        public String recommendation { get; set; }
        public String remarks { get; set; }
        public string  entered_date { get; set; }
        public string entered_by { get; set; }
        public string stc_cnt { get; set; }
        public List<string> lstGID { get; set; }

        public string Proximity { get; set; }
        public string building_type { get; set; }
        public string major_damage_cd { get; set; }
        public string major_damage_desc { get; set; }
        public string previous_grade { get; set; }
        public string new_grade { get; set; }
        public string reason { get; set; }
    }
}
