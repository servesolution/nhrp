using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Setup
{
    public class MisOffice
    {
        public String officeCd { get; set; }
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        public String upperOfficeCd { get; set; }
        [Required]
        [Display(Name = "Continent")]
        public string groupFlag { set; get; }
        public IEnumerable<SelectListItem> Continents { set; get; }
        public String orderNo { get; set; }
        public String countryCd { get; set; }
        public String regStCd { get; set; }
        public String zoneCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String districtCd { get; set; }
        public String vdcMunCd { get; set; }
        public String telephoneNo { get; set; }
        public String faxNo { get; set; }
        public String url { get; set; }
        public String email { get; set; }
        public String addressEng { get; set; }
        public String addressLoc { get; set; }
        public String wardNo { get; set; }
        public String houseNo { get; set; }
        public String street { get; set; }
        public String poBox { get; set; }
        [Required(ErrorMessage = " ")]
        //[RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        private DateTime OfficeCreateDt = DateTime.Today;
        [Required(ErrorMessage = " ")]
        //[RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public DateTime officeCreateDt { get { return OfficeCreateDt; } set { OfficeCreateDt = value; } }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public String officeCreateDtLoc { get; set; }
        [Required]
        [Display(Name = "OfficeTypeContinents")]
        public string officeType { set; get; }
        public IEnumerable<SelectListItem> OfficeTypeContinents { set; get; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String officeCreatedYearLoc { get; set; }
        public String officeCreatedMonthLoc { get; set; }
        public String officeCreatedDayLoc { get; set; }
        public String officeCreatedeMonth { get; set; }
        public String officeCreatedDay { get; set; }

        public String upperOfficeDefinedCd { get; set; }

        public String officeCreatedYear { get; set; }
    }
}
