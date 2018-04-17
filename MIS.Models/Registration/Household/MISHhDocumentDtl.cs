using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhDocumentDtl
    {
        public string HOUSEHOLD_ID { get; set; }
        public Decimal? SNO { get; set; }
        [Required(ErrorMessage = " ")]
        public string DOC_TYPE_CD { get; set; }
        public string DOC_TYPE_NAME { get; set; }
        public string DOC_ID { get; set; }
        public String REMARKS { get; set; }
        public String REMARKS_LOC { get; set; }
        public String APPROVED { get; set; }
        public String APPROVED_BY { get; set; }
        public String APPROVED_BY_LOC { get; set; }
        public String APPROVED_DT { get; set; }
        public String APPROVED_DT_LOC { get; set; }
        public String UPDATED_BY { get; set; }
        public String UPDATED_BY_LOC { get; set; }
        public String UPDATED_DT { get; set; }
        public String UPDATED_DT_LOC { get; set; }
        public String ENTERED_BY { get; set; }
        public String ENTERED_BY_LOC { get; set; }
        public String ENTERED_DT { get; set; }
        public String ENTERED_DT_LOC { get; set; }
        public String OldNewFlag { get; set; }
        public String Edit { get; set; }
        public String IPAdress { get; set; }

    }
}
