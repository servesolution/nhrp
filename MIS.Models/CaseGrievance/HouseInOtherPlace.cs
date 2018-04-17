using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.CaseGrievance
{
    public class HouseInOtherPlace
    {
        public string structure { get; set; }
        public string FIRST_NAME_ENG { get; set; }
        public string FIRST_NAME_LOC { get; set; }

        public string MIDDLE_NAME_ENG { get; set; }
        public string MIDDLE_NAME_LOC { get; set; }
        public string LAST_NAME_ENG { get; set; }
        public string LAST_NAME_LOC { get; set; }

        public string DISTRICT_ENG { get; set; }
        public string DISTRICT_LOC { get; set; }
        public string VDC_ENG { get; set; }

        public string VDC_LOC { get; set; }
        public string ward { get; set; }
        public string buildingCondition { get; set; }
        public string HOUSE_CONDTION_CD { get; set; }

    }
}
