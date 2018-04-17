using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public  class StructureFamilyDetail
    {
        public string HOUSE_OWNER_ID { get; set; }
        public string BUILDING_STRUCTURE_NO { get; set; }
        public string HOUSEHOLD_ID { get; set; }
        public string H_DEFINED_CD { get; set; }
        public string H_FULL_NAME_ENG { get; set; }
        public string H_FULL_NAME_LOC { get; set; }
        public string M_DEFINED_CD { get; set; }
        public string M_FIRST_NAME_ENG { get; set; }
        public string M_MIDDLE_NAME_ENG { get; set; }
        public string M_LAST_NAME_ENG { get; set; }
        public string M_FULL_NAME_ENG { get; set; }
        public string M_FIRST_NAME_LOC { get; set; }
        public string M_MIDDLE_NAME_LOC { get; set; }
        public string M_LAST_NAME_LOC { get; set; }
        public string M_FULL_NAME_LOC { get; set; }
        public string GENDER_ENG{ get; set; }
        public string GENDER_LOC { get; set; }
        public string MARITAL_STATUS_ENG { get; set; }
        public string MARITAL_STATUS_LOC { get; set; }
        public string RELATION_ENG { get; set; }
        public string RELATION_LOC { get; set; }
    }
}
