using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.AuditTrail
{
   public class Login
    {
        public String UsrCd {get;set;}
        public String UsrName { get; set; }
        public Decimal LoginSno {get;set;}
        public String EmployeeCd {get;set;}
        public String GroupName { get; set; }
        public String OfficeCd {get;set;}
        public String OfficeName { get; set; }
        public DateTime LoginDt{get;set;}
        public String LoginFullDt { get; set; }
        public String LoginDtLoc{get;set;}
        public String LoginFullDtLoc { get; set; }
        public Decimal LoginHh{get;set;}
        public Decimal LoginMi{get;set;}
        public Decimal LoginSs{get;set;}
        public Decimal LoginDate { get; set; }
        public DateTime? LogoutDt{get;set;}
        public DateTime? LOGIN_DT_FROM{ get; set; }

        public DateTime? LOGIN_DT_TO{ get; set; }
        public String LogoutFullDt { get; set; }
        public String LogoutDtLoc{get;set;}
        public String LogoutFullDtLoc { get; set; }
        public Decimal? LogoutHh{get;set;}
        public Decimal? LogoutMi{get;set;}
        public Decimal? LogoutSs{get;set;}
        public String LoginRemarks{get;set;}
        public String LogoutRemarks{get;set;}
        public String ModuleCd{get;set;}
        public String ModuleName { get; set; }
        public String MenuCd{get;set;}
        public String MenuUrl{get;set;}
        public String IpAddress{get;set;}
        public string INTERVIEW_DT_FROM = string.Empty;
        public string INTERVIEW_DT_TO = string.Empty;
    }
}
