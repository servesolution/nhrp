using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Report
{
  public  class GrievanceDetailReport
    {
        public string CASE_REGISTRATION_ID { get; set; }
        public string GRIEVANT_NAME { get; set; }
        public string HOUSE_OWNER_NAME_ENG { get; set; }
        public string HOUSE_OWNER_NAME { get; set; }
        public string HOUSE_OWNER_ID { get; set; }
        public string first_owner_NAME { get; set; }
        public string REGISTRATION_DIST_CD { get; set; }
        public string DISTRICT { get; set; }
        public string REGISTRATION_VDC_MUN_CD { get; set; }
        public string VDC { get; set; }
        public string REGISTRATION_WARD_NO { get; set; }
        public string SLIP_NO { get; set; }
        public string BENEFICIARY_ID { get; set; }
        public string HOUSE_SNO { get; set; }
        public string CASE_STATUS { get; set; }
        public string TARGET_BATCH_ID { get; set; }
        public string NRA_DEFINED_CD { get; set; }
        public string RECOMENDATION_FLAG { get; set; }


        public string CLARIFICATION_FLAG { get; set; }
        public string GRIEVANCE_PA { get; set; }
        public string ORDER_NO { get; set; }
        public string latitude { get; set; }

        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string RETRO_PA { get; set; }
     
    }
}




