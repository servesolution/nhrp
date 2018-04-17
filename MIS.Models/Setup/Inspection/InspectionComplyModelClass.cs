using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup.Inspection
{
    public class InspectionComplyModelClass
    {
        public String Mode { get; set; }
        public String InspectionComplyCode { get; set; }
        public String DefinedCd { get; set; }
        public String InspectionLevel { get; set; }
        public String InspectionDesign { get; set; }
        public String InspectionElementId { get; set; }
        public String InspectionComplyFlag { get; set; }
        public String InspectionRemarks { get; set; }
        public String status { get; set; }

        public String hierarchyCd { get; set; }


        public List<InspectionComplyModelClass> objComplyModelList = new List<InspectionComplyModelClass>();

    }
}
