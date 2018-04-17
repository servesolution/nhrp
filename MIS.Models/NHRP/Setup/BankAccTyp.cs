using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Setup
{
    public class BankAccTyp
    {
        public Decimal? Bank_Acc_Typ_cd { get; set; }
        public string Defined_cd { get; set; }
        public string Desc_Eng { get; set; }
        public string Desc_Loc { get; set; }
        public string Short_Name { get; set; }
        public string Short_Name_Loc { get; set; }
        public Decimal? Order_no { get; set; }
        public string Disabled { get; set; }
        public string  Approved{ get; set; }
        public string Approved_by { get; set; }
        public string Approved_dt { get; set; }
        public string Approved_dt_loc { get; set; }
        public string Entered_by { get; set; }
        public string Entered_dt { get; set; }
        public string Entered_dt_loc { get; set; }
        public string Mode { get; set; }
    }
}
