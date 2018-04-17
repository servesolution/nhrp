using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class GrievanceResurveyModel
    {
        public string HOUSE_OWNER_ID { get; set; }
        public string REASON_FOR_GRIEVANCE { get; set; }
        public string LOI_SN { get; set; }
        public string GRIEVANCE_SN { get; set; }
        public string RESPONDENT_FIRST_NAME_ENG { get; set; }
        public string RESPONDENT_MIDDDLE_NAME_ENG { get; set; }
        public string RESPONDENT_LAST_NAME_ENG { get; set; }
        public string RESPONDENT_GENDER_CD { get; set; }
        public string RELATION_CD { get; set; }
        public string RELATION_OTHER { get; set; }
        public string PHOTO  { get; set; }
        public string HOUSE_IN_SAME_PLACE { get; set; }
        public string HOUSE_IN_DIFFERENT_PLACE { get; set; }
        public string NOT_DAMAGE_HOUSE_CNT { get; set; }
        public string PARTIALLY_DAMAGED_CNT { get; set; }
        public string FULY_DAMAGED_CNT { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string SLIP_NUMBER { get; set; }
        public string SLIP_PHOTO { get; set; }
        public string WARD_REPRESENTATIVE { get; set; }
        public string WARD_PERSONNEL { get; set; }
        public string AGGREEMENT_PHOTO { get; set; }
    }
}
