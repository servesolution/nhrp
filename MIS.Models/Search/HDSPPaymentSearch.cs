using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class HDSPPaymentSearch
    {
        public string districtCd { get; set; }
        public string districtDCd { get; set; }
        public string vdcCd { get; set; }
        public string vdcDCd { get; set; }
        public string satCd { get; set; }
        public string satDCd { get; set; }
        public string rangeFrom { get; set; }
        public string rangeTo { get; set; }
        public string periodTypeCd { get; set; }
        public string periodTypeDCd { get; set; }
        public string recCount { get; set; }
        public string order { get; set; }
        public string spCd { get; set; }
        public string spDCd { get; set; }
        public string transDateEng { get; set; }
        public string transDateLoc{ get; set; }
        public string accountTypeCd { get; set; }
        public string accountTypeDCd{get;set;}
        public string accountNumber { get; set; }
        
    }
}
