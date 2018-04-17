using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Models.Report
{
    public class AdhocHouseOwnerReportModel
    {
        public string lang { get; set; }
        public string Districtcd { get; set; }
        public string VDCMun { get; set; }
        public string Ward { get; set; }
        public string topbldngcondtnn { get; set; }

        public string houseownerdropdown { get; set; }
        public string buildingstructure { get; set; }
        public string ownercount_Indicator { get; set; }
        public string ownercount { get; set; }
        public string gndr{get; set;}
        public List<SelectListItem> gender { get; set; }
        public string isrespond{get; set;}
        public List<SelectListItem> isrespondend { get; set; }
        public string housewithinEC_Inidcator { get; set; }
        public string housewithinEC { get; set; }
        public string houseoutofEC_Indicator { get; set; }
        public string houseoutofEC { get; set; }
        public string phnflag{get; set;}
        public string othrhousestop { get; set; }
        public List<SelectListItem> phonenumberflag { get; set; }

        public string area { get; set; }
        public string grievrgstrdflag{get; set;}
        public List<SelectListItem> grievanceRegisteredFlag { get; set; }
        public string benfflag{get; set;}

        public List<SelectListItem> BeneficiaryFlag { get; set; }
        public string benftype{get; set;}

        public List<SelectListItem> BeneficiaryType { get; set; }
        public string LglOwner { get; set; }
        public List<SelectListItem> legalOWner { get; set; }
        public string BldngCondtn { get; set; }
        public List<SelectListItem> BuildingCondition { get; set; }
        public string fundntype{get; set;}
        public List<SelectListItem> foundationType { get; set; }
        public string roofmtrl{get; set;}
        public List<SelectListItem> RoofMaterial { get; set; }

        public string StoreyBeforEQ { get; set; }

        public string StoreyAfterEQ { get; set; }
        public string flrmtrl{get; set;}
        public List<SelectListItem> FloorMaterial { get; set; }
        public string bldngmatrl{get; set;}
        public List<SelectListItem> BuildingMaterial { get; set; }
        public string bldngpstn{get; set;}

        public List<SelectListItem> BuildingPosition { get; set; }
        public string plnconfig{get; set;}
        public List<SelectListItem> PlanConfig { get; set; }
        public string geograpcrisk{get; set;}
        public List<SelectListItem> GeographicRisk { get; set; }
        public string inspctdpart{get; set;}
        public List<SelectListItem> InspectedPart { get; set; }
        public string dmggrade {get; set;}
        public List<SelectListItem> DamageGrade { get; set; }
        public string tchncalsoln{get; set;}
        public List<SelectListItem> TechnicalSolution { get; set; }
        public string reconstrctnstrt{get; set;}
        public List<SelectListItem> ReConstructionStart { get; set; }
        public string othruse{get; set;}
        public List<SelectListItem> OtherUse { get; set; }

        public string membercntafterEQ_Indicator { get; set; }
        public string membercntafterEQ { get; set; }

        public string superstructure { get; set; }
        public List<SelectListItem> SuperStruct { get; set; }
        public string idntfcationtyp{get; set;}
        public List<SelectListItem> IdentificationType { get; set; }
        public string idntfcationissudist { get; set; }
        public List<SelectListItem> IdentificationIssuDist { get; set; }
        public string ethncty{get; set;}
        public List<SelectListItem> Ethnicity { get; set; }
        public string eductn{get; set;}
        public List<SelectListItem> Education { get; set; }
        public string havnakacunt{get; set;}
        public List<SelectListItem> HaveBankAccount { get; set; }
        public string shltraftreq{get; set;}
        public List<SelectListItem> ShelterAfterEQ { get; set; }
        public string shltrbfreeq{get; set;}
        public List<SelectListItem> ShelterBeforeEQ { get; set; }
        public string currntplace{get; set;}
        public List<SelectListItem> CurrentPlace { get; set; }
        public string haveeqvctmcard{get; set;}
        public List<SelectListItem> HaveEQVictimCard { get; set; }
        public string mnthlyincme{get; set;}
        public List<SelectListItem> MonthlyIncome { get; set; }
        public string phno{get; set;}
        public List<SelectListItem> PhoneNo { get; set; }

        public string deathCnt_Indicator { get; set; }
        public string deathCnt { get; set; }
        public string maritalstatus { get; set; }
        public List<SelectListItem> MaritalStatussss { get; set; }


    }
}
