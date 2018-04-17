using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Inspection
{
    public class MiniInspectionModelClass
    {
        public string MODE { get; set; }
       public string  MINI_INSPECTION_ID  {get;set;}
       public string NRA_DEFINED_CD { get; set; }
       public string BENEFICIARY_NAME { get; set; }
       public string BENEFICIARY_FNAME { get; set; }
       public string BENEFICIARY_MNAME { get; set; }
       public string BENEFICIARY_LNAME { get; set; }
       public string RECIEPENT_NAME { get; set; }
       public string FILLED_RECIEPENT { get; set; }
       public string DISTRICT_CD { get; set; }
       public string VDC_MUN_CD { get; set; }
       public string WARD_NO { get; set; }
       public string HOUSE_DESIGN_NO { get; set; }
       public string TRANCHE { get; set; }
       public string FIRST_TRANCHE { get; set; }
       public string SECOND_TRANCHE { get; set; }
       public string THIRD_TRANCHE { get; set; }
       public string BANK_CD { get; set; }
       public string BANK_ACC_NO { get; set; }
       public string MOBILE_NO { get; set; }
       public string REMARKS { get; set; }
       public string STATUS { get; set; }
       public string ACTIVE { get; set; }
       public string ENTERED_BY { get; set; }
       public string ENTERED_DATE { get; set; }
       public string ENTERED_DATE_LOC { get; set; }
       public string APPROVED { get; set; }
       public string APPROVED_BY { get; set; }
       public string APPROVED_DATE { get; set; }
       public string APPROVED_DATE_LOC { get; set; }
       public string UPDATED_BY { get; set; }
       public string UPDATED_DATE { get; set; }
       public string UPDATED_DATE_LOC { get; set; }

       public string REMARKS_TWO { get; set; }
       public string REMARKS_THREE { get; set; }
       public List<InspectionOwnerDetailModelClass> ListOwner { get; set; }
    }
}
