using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISFiscalYear
    {
        [Required(ErrorMessage = " ")]
        //[RegularExpression("^\\d{4}/\\d{2}", ErrorMessage = "Please enter valid format.")]
        public String FiscalYr { get; set; }
        public Decimal? SerialNo { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|February|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        public DateTime FiscalStartDt { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[02])$", ErrorMessage = " ")]
        public String FiscalStartDtLoc { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|February|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        public DateTime FiscalEndDt { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[02])$", ErrorMessage = " ")]
        public String FiscalEndDtLoc { get; set; }
        public String Status { get; set; }
        public String ProvisionCloseBy { get; set; }
        public DateTime ProvisionCloseDt { get; set; }
        public String ProvisionCloseDtLoc { get; set; }
        public String EnteredBy { get; set; }
        public String EnteredDtLoc { get; set; }
        public DateTime EnteredDt { get; set; }
        public String FinalCloseBy { get; set; }
        public DateTime FinalCloseDt { get; set; }
        public String FinalCloseDtLoc { get; set; }
        public String IPAddress { get; set; }
        public String Mode { get; set; }

        public bool IsEditMode { get; set; }
    }
}
