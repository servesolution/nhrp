using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhSchoolDtl
    {
        public string HOUSEHOLD_ID { get; set; }
        public string MEMBER_ID { get; set; }
        [Required(ErrorMessage = " ")]
        public string DEFINED_CD { get; set; }
        public Decimal? SNO { get; set; }
        public String FULL_NAME { get; set; }
        public String FULL_NAME_LOC { get; set; }
        public string SCHOOL_NAME { get; set; }
        public string MSCHOOL_NAME_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal CLASS_TYPE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string CLASS_TYPE_NAME { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal SCHOOL_TYPE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public String SCHOOL_TYPE_NAME { get; set; }
        public Decimal? COUNTRY_CD { get; set; }
        public Decimal? REG_ST_CD { get; set; }
        public Decimal? ZONE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal DISTRICT_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string VDC_MUN_CD { get; set; }
        public Decimal? WARD_NO { get; set; }
        public String AREA_ENG { get; set; }
        public String AREA_LOC { get; set; }
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
        public String EditMode { get; set; }


    }
}
