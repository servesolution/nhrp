using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class VW_MEMBER_HUMAN_DISTROY_DTL
    {
        public string HOUSE_OWNER_ID { get; set; }
        public string BUILDING_STRUCTURE_NO { get; set; }
        public string HOUSEHOLD_ID { get; set; }
        public string M_DEFINED_CD { get; set; }
        public decimal? M_SNO { get; set; }
        public string M_FIRST_NAME_ENG { get; set; }
        public string M_MIDDLE_NAME_ENG { get; set; }
        public string M_LAST_NAME_ENG { get; set; }
        public string M_FULL_NAME_ENG { get; set; }
        public string M_FIRST_NAME_LOC { get; set; }
        public string M_MIDDLE_NAME_LOC { get; set; }
        public string M_LAST_NAME_LOC { get; set; }
        public string M_FULL_NAME_LOC { get; set; }
        public string GENDER_CD { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }
        public string DISTRICT_ENG { get; set; }
        public string DISTRICT_LOC { get; set; }
        public string COUNTRY_ENG { get; set; }
        public string COUNTRY_LOC { get; set; }
        public string REGION_ENG { get; set; }
        public string REGION_LOC { get; set; }
        public string ZONE_ENG { get; set; }
        public string ZONE_LOC { get; set; }
        public string VDC_ENG { get; set; }
        public string VDC_LOC { get; set; }
        public decimal? PER_WARD_NO { get; set; }
        public string PER_AREA_ENG { get; set; }
        public string PER_AREA_LOC { get; set; }
        public decimal? AGE { get; set; }
        public bool HUMAN_DESTROY_FLAG { get; set; }
        public decimal? HUMAN_DESTROY_CNT { get; set; }
        public string HUMAN_DESTROY_CD { get; set; }
        public string HUMAN_DESTROY_ENG { get; set; }
        public string HUMAN_DESTROY_LOC { get; set; }


        
    }
}
