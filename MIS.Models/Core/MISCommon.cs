using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
    public class MISCommon
    {
        public String Code { get; set; }
        public String DefinedCode { get; set; }
        public String Description { get; set; }
        public String DescriptionLoc { get; set; }

        public class MyValue
        {
            public string Value1;
            public string Value2;
        }
    }
}
