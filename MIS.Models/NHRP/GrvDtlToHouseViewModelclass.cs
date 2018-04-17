using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class GrvDtlToHouseViewModelclass
    {
        public string houseSN { get; set; }
        public string buildingSn { get; set; }
        public Decimal? familySn { get; set; }
        public string familyType { get; set; }
        public string nissaNo { get; set; }
        public string memFname { get; set; }
        public string memMname { get; set; }
        public string memLname { get; set; }
        public string memFullname { get; set; }
        public string memFnameLoc { get; set; }
        public string memMnameLoc { get; set; }
        public string memLnameLoc { get; set; }
        public string memeFullNameLoc { get; set; }
        public string gender { get; set; }
        public string genderLoc { get; set; }
        public string relationship { get; set; }
        public string relationshipLoc { get; set; }
        public string age { get; set; }
        public string education { get; set; }
        public string educationLoc { get; set; }
        public string citizenshipNo { get; set; }
        public string phoneNo { get; set; }
    }
}
