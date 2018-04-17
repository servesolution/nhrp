using MIS.Models.Setup.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace MIS.Models.ManageResettlement
{
   public class ResettlementModelClass
    { 
       public string Mode { get; set; }
       public string ResettlementId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string ResFirstName { get; set; }

       [Required]
        [Display(Name = "Middle Name")]
        public string ResMiddleName { get; set; }

       [Required]
       [Display(Name = "Last Name")]
       public string ResLastName { get; set; }

       [Required]
       [Display(Name = "Age")]
       public string ResAge { get; set; }

       [Required]
       [Display(Name = "FMC")]
       public string ResFmc { get; set; }

       [Required]
       [Display(Name = "First Name")]
       public string ResFathersFirstName { get; set; }

       [Required]
       [Display(Name = "Middle Name")]
       public string ResFathersMiddleName { get; set; }

       [Required]
       [Display(Name = "Last Name")]
       public string ResFathersLastName { get; set; }


       [Required]
       [Display(Name = "First Name")]
       public string ResGFathersFirstName { get; set; }

       [Required]
       [Display(Name = "Middle Name")]
       public string ResGFathersMiddleName { get; set; }

       [Required]
       [Display(Name = "Last Name")]
       public string ResGFathersLastName { get; set; }


       [Required]
       [Display(Name = "District")]
       public string ResDistrict { get; set; }

       [Required]
       [Display(Name = "VDC/MUN(P)")]
       public string ResVDCMUN { get; set; }

       [Required]
       [Display(Name = "Ward(P)")]
       public string ResWard { get; set; }

       [Required]
       [Display(Name = "Tole")]
       public string ResTole { get; set; }

       [Required]
       [Display(Name = "CTZ#No")]
       public string ResCtzNo { get; set; }

       [Required]
       [Display(Name = "PA#No")]
       public string ResPaNo { get; set; }

       [Required]
       [Display(Name = "NEWPA#No")]
       public string NewPa { get; set; }

       [Required]
       [Display(Name = "EA")]
       public string ResEa { get; set; }

       [Required]
       [Display(Name = "HH#SN")]
       public string ResHhSn { get; set; }

       [Required]
       [Display(Name = "SLIP#No")]
       public string ResSlipNo { get; set; }

       [Required]
       [Display(Name = "MIS Review")]
       public string ResMisReview { get; set; }

       [Required]
       [Display(Name = "Beneficairy Name")]
       public string ResBeneficairyFName { get; set; }
       [Required]
       [Display(Name = "Beneficairy Name")]
       public string ResBeneficairyMName { get; set; }
       [Required]
       [Display(Name = "Beneficairy Name")]
       public string ResBeneficairyLName { get; set; }

       [Required]
       [Display(Name = "Phone")]
       public string ResPhone { get; set; }


       [Required]
       [Display(Name = "Remarks By Engineers")]
       public string ResRemarks{ get; set; }
       public string RespondantFullName { get; set; }
       public string FathersFullName { get; set; }
       public string GrandFatherFullName { get; set; }
       public string BeneficiaryFullName { get; set; }


    }
}

























                        
                       