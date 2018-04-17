using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class VW_HOUSE_BUILDING_DTL
    {
        public string HOUSE_OWNER_ID { get; set; }    
        public string BUILDING_STRUCTURE_NO { get; set; }
        public decimal? HOUSE_LAND_LEGAL_OWNER { get; set; }
        public string LEGAL_OWNER_ENG { get; set; }
        public string LEGAL_OWNER_LOC { get; set; }
        public decimal? BUILDING_CONDITION_CD { get; set; }      
        public string BUILDING_CONDITION_ENG { get; set; }   
        public string BUILDING_CONDITION_LOC { get; set; }   
        public decimal? STOREYS_CNT_BEFORE { get; set; }       
        public decimal? STOREYS_CNT_AFTER { get; set; }       
        public decimal? HOUSE_AGE { get; set; }      
        public string HOUSE_HEIGHT { get; set; }
        public string HOUSE_HEIGHT_BEFORE_EQ { get; set; }
        public string HOUSE_HEIGHT_AFTER_EQ { get; set; }
        public string PLINTH_AREA { get; set; }
        public decimal? GROUND_SURFACE_CD { get; set; }        
        public string GROUND_SURFACE_ENG { get; set; }     
        public string GROUND_SURFACE_LOC { get; set; }
        public decimal? FOUNDATION_TYPE_CD { get; set; }     
        public string FOUNDATION_TYPE_ENG { get; set; }    
        public string FOUNDATION_TYPE_LOC { get; set; }    
        public decimal? RC_MATERIAL_CD { get; set; }        
        public string RC_MATERIAL_ENG { get; set; }       
        public string RC_MATERIAL_LOC { get; set; }       
        public decimal? FC_MATERIAL_CD { get; set; }    
        public string FC_MATERIAL_ENG { get; set; }
        public string FC_MATERIAL_LOC { get; set; }  
        public decimal? SC_MATERIAL_CD { get; set; }
        public string SC_MATERIAL_ENG { get; set; }      
        public string SC_MATERIAL_LOC { get; set; }    
        public decimal? BUILDING_POSITION_CD { get; set; }   
        public string BUILDING_POSITION_ENG { get; set; }  
        public string BUILDING_POSITION_LOC { get; set; }
        public decimal? BUILDING_PLAN_CD { get; set; }   
        public string BUILDING_PLAN_ENG { get; set; }  
        public string BUILDING_PLAN_LOC { get; set; }
        public string IS_GEOTECHNICAL_RISK { get; set; }   
        public decimal? ASSESSED_AREA_CD { get; set; }  
        public string ASSESSED_AREA_ENG { get; set; }     
        public string ASSESSED_AREA_LOC { get; set; }    
        public decimal? DAMAGE_GRADE_CD { get; set; }       
        public string DAMAGE_GRADE_ENG { get; set; }       
        public string DAMAGE_GRADE_LOC { get; set; }       
        public decimal? TECHSOLUTION_CD { get; set; }     
        public string TECHSOLUTION_ENG { get; set; }       
        public string TECHSOLUTION_LOC { get; set; }       
        public string TECHSOLUTION_COMMENT { get; set; }   
        public string TECHSOLUTION_COMMENT_LOC { get; set; }
        public string RECONSTRUCTION_STARTED { get; set; } 
        public string IS_SECONDARY_USE { get; set; } 
        public string LATITUDE { get; set; }     
        public string LONGITUDE { get; set; }    
        public string ALTITUDE { get; set; }     
        public string ACCURACY { get; set; }     
        public decimal? HOUSEHOLD_CNT_AFTER_EQ { get; set; }
        public string GENERAL_COMMENTS { get; set; }
        public string GENERAL_COMMENTS_LOC { get; set; }
        public string ImagePath { get; set; }
        public string PhotosofHouse { get; set; }
        public string PhotosofFrontdirections { get; set; } 
        public string PhotosofBackdirections { get; set; }
        public string PhotosofRightdirections { get; set; }
        public string PhotosofLeftdirections { get; set; }
       // public string Photosof4directions { get; set; }
        public string Photosofinternaldamageofhouse { get; set; }
        public string Fullydamagedhouseslocationsphoto { get; set; }

        private List<StructureFamilyDetail> objList = new List<StructureFamilyDetail>();        
        public List<StructureFamilyDetail> lstFamily { get { return objList; } set { objList = value; } }

        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail = new List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT>();
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail1 { get { return lstSuperStructureDetail; } set { lstSuperStructureDetail = value; } }

        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalDetail = new List<NHRS_GEOTECHNICAL_RISK_TYPE>();
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalDetail1 { get { return lstGeoTechnicalDetail; } set { lstGeoTechnicalDetail = value; } }

        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseDetail = new List<NHRS_SECONDARY_OCCUPANCY>();
        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseDetail1 { get { return lstsecondaryUseDetail; } set { lstsecondaryUseDetail = value; } }

        public List<VW_DamageExtentHome> lstDamageExtentHome = new List<VW_DamageExtentHome>();
        public List<VW_DamageExtentHome> lstDamageExtentHome1 { get { return lstDamageExtentHome; } set { lstDamageExtentHome = value; } }

        
    }
}
