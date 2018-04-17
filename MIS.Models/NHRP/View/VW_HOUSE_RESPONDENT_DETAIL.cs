using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
   public class VW_HOUSE_RESPONDENT_DETAIL
    {
        public string FIRST_NAME_ENG { get; set; }
        public string MIDDLE_NAME_ENG { get; set; }
        public string LAST_NAME_ENG { get; set; }
        public string FULL_NAME_ENG { get; set; }
        public string FIRST_NAME_LOC { get; set; }
        public string MIDDLE_NAME_LOC { get; set; }
        public string LAST_NAME_LOC { get; set; }
        public string FULL_NAME_LOC { get; set; }

        public decimal? GENDER_CD { get; set; }
        public string GENDER_CD_Defined { get; set; }
        public string GENDER_ENG { get; set; }
        public string GENDER_LOC { get; set; }

        public string RELATION_CD_Defined { get; set; }
        public decimal? RELATION_CD { get; set; }
        public string RELATION_LOC { get; set; }
        public string RELATION_ENG { get; set; }
        public string OTHER_RELATION_TYPE { get; set; }
        public string OTHER_RELATION_TYPE_LOC { get; set; }

       

        public decimal? NOT_INTERVIWING_REASON_CD { get; set; }
        public decimal? RESPONDENT_SNO { get; set; }

       

        public string HOUSE_OWNER_ID { get; set; }

    }
}
