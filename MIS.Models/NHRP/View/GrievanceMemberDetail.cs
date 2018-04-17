using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class GrievanceMemberDetail
    {
        public string HOUSE_SNO { get; set; }
        public string HOUSE_OWNER_ID { get; set; }
        public string BUILDING_STRUCTURE_NO { get; set; }
        public string HOUSEHOLD_ID { get; set; }
        public string INSTANCE_UNIQUE_SNO { get; set; }
        public string FULL_NAME_ENG { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }
        public string RELATION_ENG { get; set; }
        public string RELATION_LOC { get; set; }
        public string AGE { get; set; }
        public string EDUCATION_ENG { get; set; }
        public string EDUCATION_LOC { get; set; }
        public string CITIZENSHIP_NO { get; set; }
        public string MOBILE_NUMBER { get; set; }

        public int Family { get; set; }
        public string FamilyType { get; set; }

    }
}
