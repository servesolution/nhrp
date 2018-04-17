using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
    public class ImportTraining
    {
       
         public String BATCH_CD { get; set; }
        public String DEFINED_CD { get; set; }
        public String BatchName { get; set; }
        public String TRAINING_BATCH_NAME_LOC { get; set; }
        public String TRAINING_DESCRIPTION_ENG { get; set; }
        public String TRAINING_DESCRIPTION_LOC { get; set; }
        public String district { get; set; }
        public String VDC { get; set; }
        public String VENUE_WARD_NO { get; set; }
        public String VENUE_ADDRESS_ENG { get; set; }
        public String VENUE_ADDRESS_LOC { get; set; }
        public String START_DATE_ENG { get; set; }
        public String START_DATE_LOC { get; set; }
        public String END_DATE_ENG { get; set; }
        public String END_DATE_LOC { get; set; }
        public String ACTIVE {get; set;}
           public String ENTERED_BY { get; set; }
        public DateTime? ENTERED_DT { get; set; }
           public String LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATED_DT { get; set; }
        public String Training_LEVEL {get; set;}
        public String FUNDING_SOURCE {get; set;}
        public String TRAINING_IMPLEMENTED { get; set; }
        public String PARTICIPANT_NO { get; set; }
        public String REMARKS { get; set; }
        public String REMARKS_LOC { get; set; }
        public String GRATUATED_PER {get; set;}
        public String TRAINING_STATUS { get; set; }
        public String FileBatchID { get; set; } 
        public String Venu_Name { get; set; }
        public String IMPLEMENTING_PARTNER { get; set; } 
        public String CURRICULUM { get; set; }
        public String PARTICIPANTS_TYPE_CD { get; set; }
        public String TRAINERS_TYPE_CD   { get; set; } 
    }
}
