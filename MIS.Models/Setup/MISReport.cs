using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Setup
{
    public class MISReport
    {
       

         public String DISTRICT_CD { get; set; }
        public String VDC_MUN_CD { get; set; }
        public String MAPPING_ID { get; set; }

        //
        public String reportCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String DefinedCD { get; set; }

        public String PA_NUMBER { get; set; }
        public String upperReportCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        //[Required(ErrorMessage = " ")]
        public String reportLink { get; set; }
        [Required(ErrorMessage = " ")]
        public String reportTitle { get; set; }
        public String reportTitleLoc { get; set; }
        public Decimal orderNo { get; set; }
        public bool disabled { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredLoc { get; set; }
        public DateTime lastUpdatedDt { get; set; }
        public String lastUpdatedBy { get; set; }
        public String IPAddress { get; set; }
        public bool Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String groupFlag { get; set; }
        public String tempDesc { get; set; }
        [Required(ErrorMessage = " ")]
        public String houseHoldId { get; set; }
        public String ruleFlag { get; set; }
        public IEnumerable<SelectListItem> Continents { set; get; }

    }
}
