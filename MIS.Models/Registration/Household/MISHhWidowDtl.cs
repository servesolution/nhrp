using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhWidowDtl
    {
        public string HOUSEHOLD_ID { get; set; }      
        public string MEMBER_ID { get; set; }
          [Required(ErrorMessage = " ")]
        public string DEFINED_CD { get; set; }
        public string MEMBER_NAME { get; set; }
        public string MEMBER_NAME_LOC { get; set; }
        public Decimal? SNO { get; set; }       
        public String DEATH_CERTIFICATE { get; set; }
        public string DEATH_CERTIFICATE_NO { get; set; }
        public Decimal DEATH_YEAR { get; set; }
        public Decimal DEATH_MONTH { get; set; }
        public Decimal DEATH_DAY { get; set; }
         [Required(ErrorMessage = " ")]
        public Decimal DEATH_YEAR_LOC { get; set; }
         [Required(ErrorMessage = " ")]
        public Decimal DEATH_MONTH_LOC { get; set; }
         [Required(ErrorMessage = " ")]
        public Decimal DEATH_DAY_LOC { get; set; }
        public DateTime DEATH_DT { get; set; }
        [Required(ErrorMessage = " ")]
        public String DEATH_DT_LOC { get; set; }
        public String DEATH_DOC { get; set; }
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
        public String ENTERED_DT { get; set; }
        public String ENTERED_DT_LOC { get; set; }
        public String ipaddress { get; set; }
        public String editMode { get; set; }
        public String memIDCheck { get; set; }
    }
}
