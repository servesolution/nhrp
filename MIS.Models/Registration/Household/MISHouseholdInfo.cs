using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHouseholdInfo
    {

        public String householdId { get; set; }
        public String definedCd { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String formNo { get; set; }
        public String memmberDefinedCode { get; set; }
        public String memberId { get; set; }
        [Required] 
        [StringLength(50, MinimumLength=1)]
        public String firstNameEng { get; set; }
         [StringLength(50, MinimumLength = 1)]
        public String middleNameEng { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public String lastNameEng { get; set; }
        public String fullNameEng { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public String firstNameLoc { get; set; }
        [StringLength(500, MinimumLength = 1)]
        public String middleNameLoc { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public String lastNameLoc { get; set; }
        public String fullNameLoc { get; set; }
        public String memberCnt { get; set; }
        [Required]
        public String interviewedBy { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String interviewYear { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String interviewMonth { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String interviewdateDay { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String interviewYearLoc { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String interviewMonthLoc { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String interviewdateDayLoc { get; set; }
        public String interviewDt { get; set; }
        public String interviewDtLoc { get; set; }
        public String interviewDay { get; set; }
        public String interviewStHh { get; set; }
        public String interviewStMm { get; set; }
        public String interviewEndHh { get; set; }
        public String interviewEndMm { get; set; }
        public String perCountryCd { get; set; }
        public String perRegStCd { get; set; }
        public String perZoneCd { get; set; }
        [Required]
        public String perDistrictCd { get; set; }
        [Required]
        public String perVdcMunCd { get; set; }
        [Required]
        public String perWardNo { get; set; }
        public String perAreaEng { get; set; }
        public String perAreaLoc { get; set; }
        public String curCountryCd { get; set; }
        public String curRegStCd { get; set; }
        public String curZoneCd { get; set; }
        public String curDistrictCd { get; set; }
        public String curVdcMunCd { get; set; }
        public String curWardNo { get; set; }
        public String curAreaEng { get; set; }
        public String curAreaLoc { get; set; }
        public String houseNo { get; set; }
        public String telNo { get; set; }
        [RegularExpression(@"^\d{9}[0-9]+$", ErrorMessage = " ")]
        public String mobileNo { get; set; }
        public String fax { get; set; }
        public String email { get; set; }
        public String url { get; set; }
        public String poBoxNo { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String habitantYear { get; set; }
        public String habitantMonth { get; set; }
        public String habitantDay { get; set; }
        public String ancestralHome { get; set; }
        public String hospitalDistanceHr { get; set; }
        public String hospitalDistanceMin { get; set; }
        public String marketDistanceHr { get; set; }
        public String marketDistanceMin { get; set; }
        public String ownDwelling { get; set; }
        public String bedroom { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String bedroomCnt { get; set; }
        [Required]
        public String exWallsMaterialCdI { get; set; }
        public String exWallsMaterialCdIi { get; set; }
        [Required]
        public String roofMaterialCd { get; set; }
        [Required]
        public String waterSourceCdI { get; set; }
        public String waterSourceCdIi { get; set; }
        public String waterDistanceHr { get; set; }
        public String waterDistanceMin { get; set; }
        [Required]
        public String toiletTypeCdI { get; set; }
        public String toiletTypeCdIi { get; set; }
        public String toiletShared { get; set; }
        [Required]
        public String lightSourceCdI { get; set; }
        public String lightSourceCdIi { get; set; }
        [Required]
        public String fuelSourceCdI { get; set; }
        public String fuelSourceCdIi { get; set; }
        public String fan { get; set; }
        public String heater { get; set; }
        public String freeze { get; set; }
        public String tv { get; set; }
        public String computer { get; set; }
        public String internet { get; set; }
        public String landline { get; set; }
        public String tractor { get; set; }
        public String cart { get; set; }
        public String pump { get; set; }
        public String generator { get; set; }
        public String bicycle { get; set; }
        public String bike { get; set; }
        public String car { get; set; }
        public String landOwner { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String landOwnerCnt { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String landInBiga { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String landInRopani { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String landInKattha { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " ")]
        public String landInAana { get; set; }
        public String birds { get; set; }
        public String sheep { get; set; }
        public String deathInAYear { get; set; }
        public String childInSchool { get; set; }
        public String socialAllowance { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public String ruleCalc { get; set; }
        public String ruleFlag { get; set; }
        public String extraI { get; set; }
        public String extraIi { get; set; }
        public String extraIii { get; set; }
        public string transId { get; set; }
        public String extraV { get; set; }
        public String approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        public String updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredByLoc { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String Mode { get; set; }
        // public String FirstName

        public string perDistrict { get; set; }
        public string perVDc { get; set; }
        public string perWard { get; set; }
        public string perDistrictLOC { get; set; }
        public string perVDcLOC { get; set; }
        public string perDistrictENG { get; set; }
        public string perVDcENG { get; set; }
        public string TempDistrict { get; set; }
        public string tempVDCmun { get; set; }
        public string tempWard { get; set; }
        public string exWallMaterial { get; set; }
        public string roofMaterial { get; set; }
        public string sourceDirnkingI { get; set; }
        public string sourceDrinkingII { get; set; }
        public string toiletTypeI { get; set; }
        public string toiletTypeII { get; set; }
        public string lightSourceI { get; set; }
        public string lightSourceII { get; set; }
        public string fuelSourceI { get; set; }
        public string fuelSourceII { get; set; }
        public String perCountry { get; set; }
        public string perReg { get; set; }
        public string perZone { get; set; }
        public string tempCountry { get; set; }
        public string tempReg { get; set; }
        public string tempZone { get; set; }
        public string exWallMaterialII { get; set; }
        public string roofMaterialII { get; set; }
        public string ImageURL { get; set; }
        public string ruleValue { get; set; }
        public String interviewDtto { get; set; }
        [RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        public String interviewYearto { get; set; }
        [RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        public String interviewMonthto { get; set; }
        [RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        public String interviewdateDayto { get; set; }
        public String fuelSourceOthEngI { get; set; }
        public String fuelSourceOthNepI { get; set; }
        public String fuelSourceOthEngII { get; set; }
        public String fuelSourceOthNepII { get; set; }
        public String LightSourceOthEngI { get; set; }
        public String LightSourceOthNepI { get; set; }
        public String LightSourceOthEngII { get; set; }
        public String LightSourceOthNepII { get; set; }
        public String ExMaterialOthEngI { get; set; }
        public String ExMaterialOthNepI { get; set; }
        public String ExMaterialOthEngII { get; set; }
        public String ExMaterialOthNepII { get; set; }
        public String roofMaterialOthEngI { get; set; }
        public String roofMaterialOthNepI { get; set; }
        public String WaterSourceOthEngI { get; set; }
        public String WaterSourceOthNepI { get; set; }
        public String WaterSourceOthEngIi { get; set; }
        public String WaterSourceOthNepIi { get; set; }
        public String ToiletTypeOthEngI { get; set; }
        public String ToiletTypeOthNepI { get; set; }
        public String ToiletTypeOthEngIi { get; set; }
        public String ToiletTypeOthNepIi { get; set; }
    }
}
