using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class OtherHousesDamagedModel
    {
        public string FULL_NAME_ENG { get; set; }
        public string FULL_NAME_LOC { get; set; }
        public decimal? OTHER_DISTRICT_CD { get; set; }
        public string OTHER_DISTRICT_CD_Defined { get; set; }
        public string district { get; set; }
        public decimal? OTHER_VDC_MUN_CD { get; set; }
        public string OTHER_VDC_MUN_CD_defined { get; set; }
        public string vdc_mun { get; set; }
        public string OTHER_RESIDENCE_ID { get;set; }
        public string HOUSE_OWNER_ID { get; set; }
        public decimal? OTHER_WARD_NO { get; set; }
        public string BUILDING_CONDITION_CD { get; set; }
        public string BUILDING_CONDITION_CD_Defined { get; set; }
        public string BUILDING_CONDITION_ENG { get; set; }
        public string BUILDING_CONDITION_LOC { get; set; }
        public string buildingCondition { get; set; }
    }
}
