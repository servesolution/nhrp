using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Security
{
    public class Module
    {
        public String ModuleCd {get;set;}
        public String ModuleName {get;set;}
        public String ModuleDesc {get;set;}
        public String Status {get;set;}
        public String EnteredBy {get;set;}
        public DateTime EnteredDt {get;set;}
        public String LastUpdatedBy {get;set;}
        public DateTime LastUpdatedDt {get;set;}
        public String IpAddress {get;set;}
    }
}
