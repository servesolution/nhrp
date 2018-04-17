using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
  public  class MISGender
    {
      public  int GenderCD{get;set;}
      public string DefinedCD{ get; set; }
      [Required(ErrorMessage = " ")]
      public string DescEng{ get; set; }
      [Required(ErrorMessage = " ")]
      public string DescLoc { get; set; }
      public string ShortName { get; set; }
      public string ShortNameLoc { get; set; }
      public string ApprovedBy { get; set; }
      public Boolean Approved { get; set; }
      public DateTime ApprovedDt { get; set; }
      public string ApproveDtLoc { get; set; }
      public Boolean Disabled { get; set; }
      public string OrderNo { get; set; }
      public string EnteredBy { get; set; }
      public DateTime EnteredDt{get;set;}
      public string EnteredDtLoc{get;set;}

    }
}
