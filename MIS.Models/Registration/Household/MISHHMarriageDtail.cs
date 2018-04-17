using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHHMarriageDtail
    {
        public string HOUSEHOLD_ID { get; set; }       
        public string MEMBER_ID { get; set; }
          
        public string SPOUSE_MEMBER_ID { get; set; }
        public string SPOUSE_FULLNAME { get; set; }
        public string SPOUSE_FULLNAME_LOC { get; set; }
         [Required(ErrorMessage = " ")]
        public string DEFINED_CD { get; set; }
         [Required(ErrorMessage = " ")]
        public string SPOUSE_DEFINED_CD { get; set; }
        public String MEMBER_NAME { get; set; }
        public String MEMBER_NAME_LOC { get; set; }
        public Decimal? SNO { get; set; }
        public Decimal RELATION_TYPE_CD { get; set; }
        public bool MARRIAGE_CERTIFICATE { get; set; }
        public string MARRIAGE_CERTIFICATE_NO { get; set; }
        public String MARRIAGE_YEAR { get; set; }
        public String MARRIAGE_MONTH { get; set; }
        public String MARRIAGE_DAY { get; set; }
        public String MARRIAGE_YEAR_LOC { get; set; }
        public String MARRIAGE_MONTH_LOC { get; set; }
        public String MARRIAGE_DAY_LOC { get; set; }
        public String MARRIAGE_DT { get; set; }
        public String MARRIAGE_DT_LOC { get; set; }
        public String MARRIAGE_DOC { get; set; }
        public String REMARKS { get; set; }
        public String REMARKS_LOC { get; set; }
        public String APPROVED { get; set; }
        public String APPROVED_BY { get; set; }
        public String APPROVED_BY_LOC { get; set; }
        public DateTime APPROVED_DT { get; set; }
        public String APPROVED_DT_LOC { get; set; }
        public String UPDATED_BY { get; set; }
        public String UPDATED_BY_LOC { get; set; }
        public DateTime UPDATED_DT { get; set; }
        public String UPDATED_DT_LOC { get; set; }
        public String ENTERED_BY { get; set; }
        public String ENTERED_BY_LOC { get; set; }
        public DateTime ENTERED_DT { get; set; }
        public String ENTERED_DT_LOC { get; set; }
        public String editMode { get; set; }

    }
}
