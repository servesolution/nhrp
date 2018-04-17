using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISPmtCalc
    {
        public String PmtCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String PmtSNo { get; set; }
        public String upperPmtCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String ShortName { get; set; }
        public String ShortNameLoc { get; set; }
        public String Weightage { get; set; }
        public Decimal orderNo { get; set; }
        public bool disabled { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredLoc { get; set; }
        public String Remark { get; set; }
        public String RemarkLoc { get; set; }
        public String IPAddress { get; set; }
        public bool Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String groupFlag { get; set; }
        public String tempDesc { get; set; }
        public String Tablename { get; set; }
        public String ColumnName { get; set; }
        public String ColumnValue { get; set; }

    }
}
