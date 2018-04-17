using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Models.Setup.Inspection
{
    public class MISInspection
    {
        public String InspectionCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String DefinedCD { get; set; }
        public String upperInspectionCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        //[Required(ErrorMessage = " ")]
        public String Shortname { get; set; }

        public String ShortnameLoc { get; set; }
        public String ValueType { get; set; }
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
        public String houseModelId { get; set; }
        //public String houseModelIdChecked { get; set; }

        public String houseModelEng { get; set; }
        public String houseModelLoc { get; set; }
        

        public String ruleFlag { get; set; }


        public IEnumerable<SelectListItem> Continents { set; get; }
        public List<MISInspection> houseModelList { get; set; }
        public List<MISInspection> houseModelListChecked { get; set; }


    }
}
