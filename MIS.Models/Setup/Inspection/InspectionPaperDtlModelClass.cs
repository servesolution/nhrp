using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup.Inspection
{
    public class InspectionPaperDtlModelClass
    {
       public String INSPECTION_PAPER_ID {get; set;}
       public String DEFINED_CD {get; set;}
       public String INSPECTION_CODE_ID {get; set;}
       public String NRA_DEFINED_CD {get; set;}
       public String DISTRICT_CD {get; set;}
       public String VDC_MUN_CD {get; set;}
       public String WARD_CD {get; set;}
       public String MAP_DESIGN_CD {get; set;}
       public String MAP_DESIGN { get; set; }
       public String DESIGN_NUMBER {get; set;}
       public String RC_MATERIAL_CD {get; set;}
       public String FC_MATERIAL_CD {get; set;}
        public String TECHNICAL_ASSIST {get; set;}
        public String ORGANIZATION_TYPE {get; set;}
        public String CONSTRUCTOR_TYPE {get; set;}
        public String SOIL_TYPE {get; set;}
        public String HOUSE_MODEL {get; set;}
        public String PHOTO_CD_1 {get; set;}
        public String PHOTO_CD_2 {get; set;}
        public String PHOTO_CD_3 {get; set;}
        public String PHOTO_CD_4 {get; set;}
        public String PHOTO_1 {get; set;}
        public String PHOTO_2 {get; set;}
        public String PHOTO_3 {get; set;}
        public String PHOTO_4 {get; set;}

        public String NAKSA { get; set; }

        public String InspectnInstallment { get; set; }

        public String Passed_map_no { get; set; }
        public String Others { get; set; }

        public String mode { get; set; }

       
    }
}
