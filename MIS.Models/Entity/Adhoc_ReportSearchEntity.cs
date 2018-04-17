using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table(Name = "hhold_dmg_sty_techS_supS")]

    public class Adhoc_ReportSearchEntity : EntityBase
    {
        private System.String district_cd = null;
        private System.String mun_vdc_cd = null;
        private System.String ward_cd = null;
        private System.String grievance = null;
        private System.String beneficiary = null;
        private System.String beneficiary_type = null;

        private System.String superstructure = null;
        private System.String deathcount =null;
        private System.String storycountbeforeearthquake = null;
        private System.String respondentishouseowner = null;
        private System.String storycountafterearthquake = null;
        private System.String membercountafterearthquake = null;
        private System.String gender = null;


        private System.String houseowner = null;
        private System.String ownercount_Indicator = null;
        private System.String ownercount = null;
        private System.String gender_cd = null;
        private System.String isrespond = null;
        private System.String otherhouse = null;


        private System.String building_structure = null;
        private System.String LglOwner = null;
        private System.String BldngCondtn = null;
        private System.String StoreyBeforEQ_Indicator = null;
        private System.String StoreyBeforEQ = null;
        private System.String StoreyAfterEQ_Indicator = null;
        private System.String StoreyAfterEQ = null;
        private System.String fundntype = null;
        private System.String RoofMaterial = null;
        private System.String flrmtrl = null;
        private System.String bldngmatrl = null;
        private System.String bldngpstn = null;
        private System.String plnconfig = null;
        private System.String geograpcrisk = null;
        private System.String inspctdpart = null;
        private System.String reconstrctnstrt = null;
        private System.String damage_grade_cd = null;
        private System.String technical_soln_cd = null;
        private System.String othruse = null;
        private System.String membercntafterEQ_Indicator = null;
        private System.String membercntafterEQ = null;
        private System.String super_structure_cd = null;



        private System.String household = null;
        private System.String identification_type = null;
        private System.String identification_issue_district = null;
        private System.String caste = null;
        private System.String education = null;
        private System.String educationquery = null;
        private System.String marital_status = null;

        private System.String havebankacc = null;
        private System.String shelteraftereq = null;
        private System.String shelterbeforeq = null;
        private System.String currentplace = null;
        private System.String have_eqvictim_card = null;

        private System.String monthly_income = null;
        private System.String monthlyincomequery = null;
        private System.String phone_num = null;
        private System.String death_cnt_indicator = null;
        private System.String death_cnt = null;













        [Column(Name = "district_cd", IsKey = true, SequenceName = "")]
        public System.String District_CD
        {
            get { return district_cd; }
            set { district_cd = value; }
        }

        [Column(Name = "VDC_MUN_CD", IsKey = true, SequenceName = "")]
        public System.String VDC_MUN_CD
        {
            get { return mun_vdc_cd; }
            set { mun_vdc_cd = value; }
        }

        [Column(Name = "ward_no ", IsKey = true, SequenceName = "")]
        public System.String Ward
        {
            get { return ward_cd; }
            set { ward_cd = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Grievance
        {
            get { return grievance; }
            set { grievance = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Beneficiary
        {
            get { return beneficiary; }
            set { beneficiary = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String BeneficiaryType
        {
            get { return beneficiary_type; }
            set { beneficiary_type = value; }
        }


        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String HouseOwner
        {
            get { return houseowner; }
            set { houseowner = value; }
        }






        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String SuperStructure
        {
            get { return superstructure; }
            set { superstructure = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String DeathCount
        {
            get { return deathcount; }
            set { deathcount = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String RespondentisHouseowner
        {
            get { return respondentishouseowner; }
            set { respondentishouseowner = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoryCountBeforeEarthQuake
        {
            get { return storycountbeforeearthquake; }
            set { storycountbeforeearthquake = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoryCountAfterEarthQuake
        {
            get { return storycountafterearthquake; }
            set { storycountafterearthquake = value; }
        }


        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String MemberCountAfterEarthQuake
        {
            get { return membercountafterearthquake; }
            set { membercountafterearthquake = value; }
        }



        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String Gender
        {
            get { return gender; }
            set { gender = value; }
        }






















        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String OwnerCountIndicator
        {
            get { return ownercount_Indicator; }
            set { ownercount_Indicator = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String OwnerCount
        {
            get { return ownercount; }
            set { ownercount = value; }
        }


        [Column(Name = "gender_cd", IsKey = true, SequenceName = "")]
        public System.String Gender_CD
        {
            get { return gender_cd; }
            set { gender_cd = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String IsRespond
        {
            get { return isrespond; }
            set { isrespond = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String Otherhouse
        {
            get { return otherhouse; }
            set { otherhouse = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String BuildingStructure
        {
            get { return building_structure; }
            set { building_structure = value; }
        }


        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String HouseLegalOwner
        {
            get { return LglOwner; }
            set { LglOwner = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String BuildingCondition
        {
            get { return BldngCondtn; }
            set { BldngCondtn = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoreyCountBeforeEQ_Indict
        {
            get { return StoreyBeforEQ_Indicator; }
            set { StoreyBeforEQ_Indicator = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoreyCountBefore
        {
            get { return StoreyBeforEQ; }
            set { StoreyBeforEQ = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoreyAfterEQ_Indict
        {
            get { return StoreyAfterEQ_Indicator; }
            set { StoreyAfterEQ_Indicator = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String StoreyCountAfterEQ
        {
            get { return StoreyAfterEQ; }
            set { StoreyAfterEQ = value; }
        }
        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String FoundationType
        {
            get { return fundntype; }
            set { fundntype = value; }
        }


        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String RoofMaterials
        {
            get { return RoofMaterial; }
            set { RoofMaterial = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String FloorMaterial
        {
            get { return flrmtrl; }
            set { flrmtrl = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String BuildingMaterial
        {
            get { return bldngmatrl; }
            set { bldngmatrl = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String BuildingPosition
        {
            get { return bldngpstn; }
            set { bldngpstn = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String PlanConfig
        {
            get { return plnconfig; }
            set { plnconfig = value; }
        }



        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String GeographicalRisk
        {
            get { return geograpcrisk; }
            set { geograpcrisk = value; }
        }



        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String InspectedPart
        {
            get { return inspctdpart; }
            set { inspctdpart = value; }
        }

        [Column(Name = "", IsKey = false, SequenceName = "")]
        public System.String ReconstructionStarted
        {
            get { return reconstrctnstrt; }
            set { reconstrctnstrt = value; }
        }


        [Column(Name = "damage_grade", IsKey = true, SequenceName = "")]
        public System.String Damage_Grade
        {
            get { return damage_grade_cd; }
            set { damage_grade_cd = value; }
        }
        
        [Column(Name = "technical_soln_cd", IsKey = true, SequenceName = "")]
        public System.String Technical_soln_CD
        {
            get { return technical_soln_cd; }
            set { technical_soln_cd = value; }
        }



        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String OtherUseOfHouse
        {
            get { return othruse; }
            set { othruse = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String MemberCntAfterEQ_Indicator
        {
            get { return membercntafterEQ_Indicator; }
            set { membercntafterEQ_Indicator = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String MemberCountAEQ
        {
            get { return membercntafterEQ; }
            set { membercntafterEQ = value; }
        }

        [Column(Name = "super_str_cd", IsKey = true, SequenceName = "")]
        public System.String SuperStruct_CD
        {
            get { return super_structure_cd; }
            set { super_structure_cd = value; } 
        }





        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String HouseHold
        {
            get { return household; }
            set { household = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Identification_Type
        {
            get { return identification_type; }
            set { identification_type = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Identification_Issue_Dis
        {
            get { return identification_issue_district; }
            set { identification_issue_district = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Caste
        {
            get { return caste; }
            set { caste = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Education
        {
            get { return education; }
            set { education = value; }
        }


        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String EducationQuery
        {
            get { return educationquery; }
            set { educationquery = value; }
        }





        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String HaveBankAccount
        {
            get { return havebankacc; }
            set { havebankacc = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String ShelterAfterEQ
        {
            get { return shelteraftereq; }
            set { shelteraftereq = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String ShelterBeforeEQ
        {
            get { return shelterbeforeq; }
            set { shelterbeforeq = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String CurrentPlace
        {
            get { return currentplace; }
            set { currentplace = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String HaveEQvictimCard
        {
            get { return have_eqvictim_card; }
            set { have_eqvictim_card = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String MonthlyIncome
        {
            get { return monthly_income; }
            set { monthly_income = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String MonthlyIncomeQuery
        {
            get { return monthlyincomequery; }
            set { monthlyincomequery = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Phone_Number
        {
            get { return phone_num; }
            set { phone_num = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Death_Cnt_Indicator
        {
            get { return death_cnt_indicator; }
            set { death_cnt_indicator = value; }
        }
        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Death_Count
        {
            get { return death_cnt; }
            set { death_cnt = value; }
        }

        [Column(Name = "", IsKey = true, SequenceName = "")]
        public System.String Marital_Status
        {
            get { return marital_status; }
            set { marital_status = value; }
        }






    }
}
