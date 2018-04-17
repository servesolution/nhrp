using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
   public class MISHhAddHistory
    {
        public String HouseholdId {get;set;}
        public String AddressType {get;set;}
        public Decimal Sno {get;set;}
        public Decimal? CountryCd {get;set;}
        public Decimal? RegStCd {get;set;}
        public Decimal? ZoneCd {get;set;}
        public Decimal? DistrictCd {get;set;}
        public Decimal? VdcMunCd {get;set;}
        public Decimal? WardNo {get;set;}
        public String AreaEng {get;set;}
        public String AreaLoc {get;set;}
        public Decimal? HabitantYear {get;set;}
        public Decimal? HabitantMonth {get;set;}
        public Decimal? HabitantDay {get;set;}
        public String Remarks {get;set;}
        public String RemarksLoc {get;set;}
        public bool Approved {get;set;}
        public String ApprovedBy {get;set;}
        public String ApprovedByLoc {get;set;}
        public DateTime? ApprovedDt {get;set;}
        public String ApprovedDtLoc {get;set;}
        public String UpdatedBy {get;set;}
        public String UpdatedByLoc {get;set;}
        public DateTime? UpdatedDt {get;set;}
        public String UpdatedDtLoc {get;set;}
        public String EnteredBy {get;set;}
        public String EnteredByLoc {get;set;}
        public DateTime EnteredDt {get;set;}
        public String EnteredDtLoc {get;set;}
        public String IPAddress { get; set; }
    }
}
