using MIS.Models.NHRP.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class NHRS_HOUSE_BUILDING_DTL
    {
        public string HOUSE_ID { get; set; }
        public string HOUSE_OWNER_ID { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_STRUCTURE_NO { get; set; }
        [Required(ErrorMessage = " ")]
        public string HOUSE_LAND_LEGAL_OWNER_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string HOUSE_LAND_LEGAL_OWNER { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_CONDITION_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_CONDITION_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_CONDITION_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? STOREYS_CNT_BEFORE { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? STOREYS_CNT_AFTER { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? HOUSE_AGE { get; set; }
        [Required(ErrorMessage = " ")]
        public string PLINTH_AREA { get; set; }
        [Required(ErrorMessage = " ")]
        public string HOUSE_HEIGHT_AFTER_EQ { get; set; }
        [Required(ErrorMessage = " ")]
        public string HOUSE_HEIGHT_BEFORE_EQ { get; set; }
        [Required(ErrorMessage = " ")]
        public string GROUND_SURFACE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string GROUND_SURFACE_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string GROUND_SURFACE_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string FOUNDATION_TYPE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string FOUNDATION_TYPE_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string FOUNDATION_TYPE_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string RC_MATERIAL_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string RC_MATERIAL_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string RC_MATERIAL_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string FC_MATERIAL_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string FC_MATERIAL_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string FC_MATERIAL_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string SC_MATERIAL_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string SC_MATERIAL_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string SC_MATERIAL_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_POSITION_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_POSITION_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_POSITION_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_PLAN_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_PLAN_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string BUILDING_PLAN_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string IS_GEOTECHNICAL_RISK { get; set; }
        [Required(ErrorMessage = " ")]
        public string ASSESSED_AREA_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string ASSESSED_AREA_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string ASSESSED_AREA_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string DAMAGE_GRADE_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string DAMAGE_GRADE_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string DAMAGE_GRADE_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string TECHSOLUTION_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string TECHSOLUTION_ENG { get; set; }
        [Required(ErrorMessage = " ")]
        public string TECHSOLUTION_LOC { get; set; }       
        public string TECHSOLUTION_COMMENT { get; set; }   
        public string TECHSOLUTION_COMMENT_LOC { get; set; }
        [Required(ErrorMessage = " ")]
        public string RECONSTRUCTION_STARTED { get; set; }
        [Required(ErrorMessage = " ")]
        public string IS_SECONDARY_USE { get; set; }
        [Required(ErrorMessage = " ")]
        public string LATITUDE { get; set; }
        [Required(ErrorMessage = " ")]
        public string LONGITUDE { get; set; }
        [Required(ErrorMessage = " ")]
        public string ALTITUDE { get; set; }
        [Required(ErrorMessage = " ")]
        public string ACCURACY { get; set; }
        public string DISTRICT_CD { get; set; }
        [Required(ErrorMessage = " ")]
        public string GENERAL_COMMENTS { get; set; }
        public string GENERAL_COMMENTS_LOC { get; set; }
        public string HO_DEFINED_CD { get; set; }
        public string INSTANCE_UNIQUE_SNO { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? HOUSEHOLD_CNT_AFTER_EQ { get; set; }
        public System.String NHRS_UUID { get; set; }
        public System.Decimal? BATCH_ID { get; set; }
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureDetail { get; set; }
        public List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> lstSuperStructureName { get; set; }
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalName { get; set; }
        public List<NHRS_GEOTECHNICAL_RISK_TYPE> lstGeoTechnicalDetail { get; set; }
        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseName { get; set; }
        public List<NHRS_SECONDARY_OCCUPANCY> lstsecondaryUseDetail { get; set; }
        public string editMode { get; set; } 
        
    }
}
