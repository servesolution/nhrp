using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace MIS.Models.Security
{
    public class Permission
    {

        public String PermCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String PermName { get; set; }
        [Required(ErrorMessage = " ")]
        public String PermDesc { get; set; }
        public String EnteredBy { get; set; }
        public DateTime EnteredDt { get; set; }
        public String LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDt { get; set; }
        public String IPAddress { get; set; }


        public Permission()
        { }

    }
}
