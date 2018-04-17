using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
   public class MISVdcmunMapping
    {
       public Decimal? MUNMPNG_CD { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String DefinedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String DescEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String CountryCd { get; set; }
        public String CountryName { get; set; }
        [Required(ErrorMessage = " ")]
        public String DescLoc { get; set; }
        public String ShortName { get; set; }
        public String ShortNameLoc { get; set; }
        public Decimal? OrderNo { get; set; }
        public bool Disabled { get; set; }
        public bool Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        public String EnteredBy { get; set; }
        public DateTime EnteredDt { get; set; }
        public String EnteredDtLoc { get; set; }
        public String editMode { get; set; }
        public Decimal? regStateCodeCheck { get; set; }
        public String regStateNameCheck { get; set; }
        public String regStateLocCheck { get; set; }
        public String IPAddress { get; set; }


        public String leftIndicator { get; set; }
        public String rightIndicator { get; set; }
        [Required(ErrorMessage = " ")]
        public String usrName { get; set; }



        //public int PreviousDistrict_cd { get; set; }
        //public int PreviousVdcmun_cd { get; set; }
        //public int PreviousWard { get; set; }

   
        //public int CurrentVdcmun_cd { get; set; }
        //public int CurrentWard { get; set; }



        public decimal? PreviousDistrict_cd { get; set; }
        public decimal? PreviousVdcmun_cd { get; set; }

        public string PreviousVdcmuncd { get; set; }
        public decimal? PreviousWard { get; set; }

        public decimal? wardFrom { get; set; }
        public decimal? wardTo { get; set; }
        public decimal? CurrentVdcmun_cd { get; set; }

        public string CurrentVdcmuncd { get; set; }
        public decimal? CurrentWard { get; set; }









    }
}
