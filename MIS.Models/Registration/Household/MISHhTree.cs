using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class MISHhTree
    {
        public String householdId { get; set; }
        public string houseHoldDefinedCode { get; set; }
        public string householdFullNameEng { get; set; }
        public string householdFullNameLoc { get; set; }
        public string GenderLoc { get; set; }
        public string GenderEng { get; set; }
        public string HeadEthnicityLoc { get; set; }
        public string HeadEthnicityEng { get; set; }
        public string HeadCasteLoc { get; set; }
        public string HeadCasteEng { get; set; }
        public string HeadReligionLoc { get; set; }
        public string HeadReligionEng { get; set; }
        public string MemberCnt { get; set; }
        public string MaleCnt { get; set; }
        public string Femalecnt { get; set; }


        public string Othercnt { get; set; } 
    }
}
