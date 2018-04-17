using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_MIS_HOUSEHOLD_INDICATOR
    {
        public System.Decimal indicatorCd { get; set; }
        public string HOUSEHOLD_ID { get; set; }  //1
        public string HOUSE_OWNER_ID { get; set; }


        public System.String descEng { get; set; }

        public System.String descLoc { get; set; }

        public System.Decimal? orderNo { get; set; }

        public System.String enteredBy { get; set; }

        public System.DateTime? enteredDt { get; set; }

        public System.String enteredDtLoc { get; set; }

        public System.String definedCd { get; set; }
        public String INDICATOR_BEFORE { get; set; }
        public String INDICATOR_AFTER { get; set; }

        public String ipAddress { get; set; }
    }
}
