using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Enrollment.EnrollmentPopup
{
    public class HouseOwner
    {
        public String HOUSE_OWNER_ID { get; set; }
        public Decimal? SNO { get; set; }

        public String  MEMBER_ID {get; set;}
        public String   FIRST_NAME_ENG{ get; set; }
        public String MIDDLE_NAME_ENG { get; set; }
        public String LAST_NAME_ENG { get; set; }
        public String FULL_NAME_ENG { get; set; }
        public String FIRST_NAME_LOC { get; set; }
        public String MIDDLE_NAME_LOC { get; set; }
        public String LAST_NAME_LOC { get; set; }
        public String FULL_NAME_LOC { get; set; }
        public String IDENTIFICATION_ISSUE_DT { get; set; }

        public String MEMBER_PHOTO_ID { get; set; }
        public Decimal? GENDER_CD { get; set; }
        public String GENDER_ENG { get; set; }
        public String GENDER_LOC { get; set; }
        public Decimal? MARITAL_STATUS_CD { get; set; }
        public String MARITAL_STATUS_ENG { get; set; }
        public String MARITAL_STATUS_LOC { get; set; }
        public String HOUSEHOLD_HEAD { get; set; }
        public String HOUSEHOLD_ID { get; set; }
        public String HOUSEHOLD_DEFINED_CD { get; set; }










    }
}
