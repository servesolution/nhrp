using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Resurvey
{
    public class ResurveyHouseBuildingDetail
    {
        public string BuildCondiAftrQuakeEng { get; set; }
        //public string BuildingStructureNo { get; set; }
        public string BuildCondiAftrQuakeLoc { get; set; }
        public string StoreyBeforeEarthQuake { get; set; }
        public string StoryAfterEarthQuake { get; set; }
        public string BuildingStructureNumber { get; set; }
        public string GeneralComments { get; set; }
        public string IsGeoTechnicalRisk { get; set; }
        public string DamageGradeEng { get; set; }
        public string DamageGradeLoc { get; set; }
        public string TechnicalSolutionEng { get; set; }
        public string TechnicalSolutionLoc { get; set; }
        public string IsReconstructionStarted { get; set; }
        public string PhotosofFrontdirections { get; set; }
        public string PhotosofBackdirections { get; set; }
        public string PhotosofRightdirections { get; set; }
        public string PhotosofLeftdirections { get; set; }
        public string Photosofinternaldamageofhouse { get; set; }
        public string Fullydamagedhouseslocationsphoto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string GeoRiskEng { get; set; }
        public string GeoRiskLoc { get; set; }    
       
        public List<ResurveyGeoTechnicalRiskType> lstGeoTechnicalDetail = new List<ResurveyGeoTechnicalRiskType>();
        public List<ResurveyGeoTechnicalRiskType> lstGeoTechnicalDetail1 { get { return lstGeoTechnicalDetail; } set { lstGeoTechnicalDetail = value; } }
    }
}
