using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MIS.Models.Core
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string ContactCd { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OrganizationCd { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string OfficePhone { get; set; }
        public string PhoneExt { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CountryCd { get; set; }
        public string Country { get; set; }
        public string StateCd { get; set; }
        public string State { get; set; }
        public string DistrictCd { get; set; }
        public string District { get; set; }
        public string PinZip { get; set; }
        public string UserType { get; set; }
        public bool IsSDP { get; set; }
        public bool IsSYS { get; set; }
        public bool IsPV { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsCCB { get; set; }
        public bool IsPOP { get; set; }
        public string GroupName { get; set; }
        public string OrganizationName { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool ReadOnly { get; set; }
    }

    
}
