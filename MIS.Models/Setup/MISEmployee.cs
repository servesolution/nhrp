using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Setup
{
    public class MISEmployee
    {
        public String To { get; set; }
        [Required(ErrorMessage = " ")]
        public String From { get; set; }
        [Required(ErrorMessage = " ")]
        public String Subject { get; set; }
        [Required(ErrorMessage = " ")]
        public String Body { get; set; }
        [Required(ErrorMessage = " ")]
        public String EmployeeCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String DefinedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String FirstNameEng { get; set; }
        public String MiddleNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String LastNameEng { get; set; }
        public String FullNameEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String FirstNameLoc { get; set; }
        public String MiddleNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String LastNameLoc { get; set; }
        public String FullNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        //[RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|February|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public String BirthDateLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String MaritalStatusCd { get; set; }
        public String MaritalName { get; set; }
        [Required(ErrorMessage = " ")]
        public String GenderCd { get; set; }

        public String FatherFirstaNameEng { get; set; }
        public String FatherMiddleNameEng { get; set; }
        public String FatherLastNameEng { get; set; }
        public String FatherFullNameEng { get; set; }
        public String FatherFirstaNameLoc { get; set; }
        public String FatherMiddleNameLoc { get; set; }
        public String FatherLastNameLoc { get; set; }
        public String FatherFullNameLoc { get; set; }

        public String MotherFirstaNameEng { get; set; }
        public String MotherMiddleNameEng { get; set; }
        public String MotherLastNameEng { get; set; }
        public String MotherFullNameEng { get; set; }
        public String MotherFirstaNameLoc { get; set; }
        public String MotherMiddleNameLoc { get; set; }
        public String MotherLastNameLoc { get; set; }
        public String MotherFullNameLoc { get; set; }

        public String GFatherFirstaNameEng { get; set; }
        public String GFatherMiddleNameEng { get; set; }
        public String GFatherLastNameEng { get; set; }
        public String GFatherFullNameEng { get; set; }
        public String GFatherFirstaNameLoc { get; set; }
        public String GFatherMiddleNameLoc { get; set; }
        public String GFatherLastNameLoc { get; set; }
        public String GFatherFullNameLoc { get; set; }

        public String SpouseFirstaNameEng { get; set; }
        public String SpouseMiddleNameEng { get; set; }
        public String SpouseLastNameEng { get; set; }
        public String SpouseFullNameEng { get; set; }
        public String SpouseFirstaNameLoc { get; set; }
        public String SpouseMiddleNameLoc { get; set; }
        public String SpouseLastNameLoc { get; set; }
        public String SpouseFullNameLoc { get; set; }
        
        public String CitizenshipNo { get; set; }       
        public String CitizenshipDistrictCode { get; set; }        
        public DateTime CitizenshipIssueDate { get; set; }
        public String CitizenshipIssueDateLoc { get; set; }

        public String PassportNo { get; set; }
        public String PassportDistrictCode { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public String PassportIssueDateLoc { get; set; }

        public String ProvidentFundNo { get; set; }
        public String CitNo { get; set; }
        public String PanNo { get; set; }
        public String DrivingLicenseNo { get; set; }

        public String PerCountryCd { get; set; }
        public String PerRegStCd { get; set; }
        public String PerZoneCd { get; set; }
        public String PerDistrictCd { get; set; }
        public String PerVdcMunCd { get; set; }
        public String PerWardNo { get; set; }
        public String PerStreet { get; set; }
        public String PerStreetLoc { get; set; }

        public String TempCountryCd { get; set; }
        public String TempRegStCd { get; set; }
        public String TempZoneCd { get; set; }
        public String TempDistrictCd { get; set; }
        public String TempVdcMunCd { get; set; }
        public String TempWardNo { get; set; }
        public String TempStreet { get; set; }
        public String TempStreetLoc { get; set; }

        [Required(ErrorMessage = " ")]
        public String OfficeCd { get; set; }
        public String SectionCd { get; set; }
        //[RegularExpression("^(?:((31-(Jan|Mar|May|Jul|Aug|Oct|Dec))|((([0-2]\\d)|30)-(January|February|March|April|May|June|July|August|September|October|November|December))|(([01]\\d|2[0-8])-Feb))|(29-Feb(?=-((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))-((1[6-9]|[2-9]\\d)\\d{2})$", ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public DateTime OfficeJoinedDate { get; set; }
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]
        public String OfficeJoinedDateLoc { get; set; }

        [Required(ErrorMessage = " ")]
        public String PositionCd { get; set; }
        public String PositionSubClCd { get; set; }

        [Required(ErrorMessage = " ")]
        public String DesignationCd { get; set; }
        public String DesignationName { get; set; }
        public bool Retired { get; set; }
        public DateTime RetiredDate { get; set; }
        public String RetiredDateLoc { get; set; }
        public String MobileNumber { get; set; }
        public String TelephoneNum { get; set; }
        public String FaxNum { get; set; }
        public String PoBox { get; set; }
        [RegularExpression("^[_a-z0-9-]+(\\.[_a-z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,4})$", ErrorMessage = " ")]
        public String Email { get; set; }
        public String OtherConInfo { get; set; }
        public String OtherConInfoLoc { get; set; }
        public String Remarks { get; set; }
        public String RemarksLoc { get; set; }
        public String EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public bool Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public String ApprovedDateLoc { get; set; }

        public String Mode { get; set; }

         //[Required(ErrorMessage = " ")]
        public String UserCode { get; set; }
        //[Required(ErrorMessage = " ")]
        [RegularExpression(".{6}.*", ErrorMessage = "Password should be at least 6 characters!!")]
        public String Password { get; set; }
        //[Required(ErrorMessage = " ")]
        [Compare("Password", ErrorMessage = "Password Mismatch!!")]
        public String ReTypePassword { get; set; }
        //[Required(ErrorMessage = " ")]
        public String GroupCode { get; set; }

        public String GenderName { get; set; }

        public String ImageURL { get; set; }

        public bool Verification_Required { get; set; }
    }
}
