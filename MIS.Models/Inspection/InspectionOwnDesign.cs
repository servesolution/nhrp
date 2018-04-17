using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Inspection
{
    public class InspectionOwnDesign
    {
        public string mode { get; set; }
        public string PillarConstructing { get; set; }
        public string PillerConstructed { get; set; }
        public string GroundRoofCompleted { get; set; }
        public string GroundFloorCompleted { get; set; }


        public string FloorCount { get; set; }
        public string ConstructionStatus { get; set; }
        public string BaseConstructMaterial { get; set; }
        public string BaseDepthBelowGrnd { get; set; }
        public string BaseAvgWidth { get; set; }
        public string BseHeightAbvGrnd { get; set; }

        public string gndFloMat { get; set; }
        public string GrndFlorPrncpl { get; set; }
        public string WallStructDescpt { get; set; }

        public string FloorRoofDescrpt { get; set; }
        public string FloorRoofMat { get; set; }
        public string FloorRoofPrncpl { get; set; }
        public string FloorRoofDetl { get; set; }

        public string FirstFlorMat { get; set; }
        public string FirstFLorPrncpl { get; set; }
        public string FirstFlorDtl { get; set; }


        public string Roofmat { get; set; }
        public string RoofPrncpl { get; set; }
        public string RoofDetal { get; set; }
        public List<InspectionOwnDesign> list { get; set; }



    }
}
