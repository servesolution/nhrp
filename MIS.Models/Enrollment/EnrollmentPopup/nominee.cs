using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Enrollment.EnrollmentPopup
{
   public class nominee
    {
        public String HOUSE_OWNER_ID { get; set; }
        public String NOMINEE_MEMBER_ID { get; set; }
        public String NOMINEE_FNAME_ENG { get; set; }
        public String NOMINEE_MNAME_ENG { get; set; }
        public String NOMINEE_LNAME_ENG { get; set; }
        public String NOMINEE_FULLNAME_ENG { get; set; }
        public String NOMINEE_FNAME_LOC { get; set; }
        public String NOMINEE_MNAME_LOC { get; set; }
        public String NOMINEE_LNAME_LOC { get; set; }
        public String NOMINEE_FULLNAME_LOC { get; set; }
        public Decimal? NOMINEE_RELATION_TYPE_CD { get; set; }
        public String NOMINEE_RELATION_TYPE_ENG { get; set; }
        public String NOMINEE_RELATION_TYPE_LOC { get; set; }


    }
}
