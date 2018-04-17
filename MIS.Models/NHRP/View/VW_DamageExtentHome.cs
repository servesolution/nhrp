using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class VW_DamageExtentHome
    {
        public decimal? BUILDING_STRUCTURE_NO { get; set; }
        public decimal? HH_DAMAGE_DTL_ID { get; set; }
        public decimal? DAMAGE_CD { get; set; }
        public String DAMAGE_ENG { get; set; }
        public String DAMAGE_LOC { get; set; }
        public decimal? DAMAGE_LEVEL_CD { get; set; }
        public String DAMAGE_LEVEL_ENG { get; set; }
        public String DAMAGE_LEVEL_LOC { get; set; }
        public decimal? DAMAGE_LEVEL_DTL_CD { get; set; }
        public String DAMAGE_LEVEL_DTL_ENG { get; set; }
        public String DAMAGE_LEVEL_DTL_LOC { get; set; }
        public String COMMENT_ENG { get; set; }
        public String COMMENT_LOC { get; set; }

        public decimal? UPPER_DAMAGE_CD { get; set; }
        public String GROUP_FLAG { get; set; }
        public String GROUP_FLAG_VALUE { get; set; }
    }
}
