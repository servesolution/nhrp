using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
   public class HouseholdMemberParameters:ReportParameters
    {

       public string CasteGroup { get; set; }
       public string Caste { get; set; }
       public bool IsGenderWise { get; set; }
       public bool IsHouseholdHeadWise { get; set; }
       public bool IsReportDetail { get; set; }


       public HouseholdMemberParameters() { }


       public HouseholdMemberParameters(string region, string zone, string district, string vdc, string ward, string area, string castGroup, string cast,
           bool isGenderWise, bool isHouseholdHeadwise, bool isReportDetail)
       {
           this.Region = region;
           this.Zone = zone;
           this.District = district;
           this.VDC = vdc;
           this.WardNo = ward;
           this.Area = area;
           this.CasteGroup = castGroup;
           this.Caste = cast;
           this.IsGenderWise = isGenderWise;
           this.IsHouseholdHeadWise = isHouseholdHeadwise;
           this.IsReportDetail = isReportDetail;
       }
    }
}
