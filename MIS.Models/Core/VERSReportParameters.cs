using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
    public class VERSReportParameters:ReportParameters
    {
      
        public string RegDateFrom { get; set; }
        public string RegDateTo { get; set; }
        public string BirthDateFrom { get; set; }
        public string BirthDateTo { get; set; }

        public string BirthSite { get; set; }
        public string BirthHelper { get; set; }
        public string BirthType { get; set; }

        public VERSReportParameters() { }

        public VERSReportParameters(string region, string zone, string district, string vdc, string wardno, string regDateFrom, string regDateTo,
            string brithDateFrom, string birthDateTo, string birthSite, string birthHelper, string birthType)
        {
            this.Region = region;
            this.Zone = zone;
            this.District = district;
            this.VDC = vdc;
            this.WardNo = wardno;
            this.RegDateFrom = regDateFrom;
            this.RegDateTo = regDateTo;
            this.BirthDateFrom = brithDateFrom;
            this.BirthDateTo = birthDateTo;
            this.BirthSite = birthSite;
            this.BirthHelper = birthHelper;
            this.BirthType = birthType;
        }

    }
}
