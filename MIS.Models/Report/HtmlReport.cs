using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace MIS.Models.Report
{
    public class HtmlReport
    {
       
         public string DISTRICT_CD { get; set; }
         public string BankName { get; set; }
        public string Installment { get; set; }
        public string PA { get; set; }
        public string BenefType { get; set; }
         
        public string FY { get; set; }
        public string Trimester { get; set; }
        public string BankCd { get; set; }
        public string CaseType { get; set; }

        [Required]
        public string District { get; set; }

        public string DonorName { get; set; }
        
        public string Donor { get; set; }
        public string SelectedType { get; set; }
        public string BATCHID { get; set; }

        [Required(ErrorMessage = " ")]
        public string VDC { get; set; }
        [Required(ErrorMessage = " ")]
        public string Ward { get; set; }

        public string User { get; set; }

        public string CurrentVDC { get; set; }
        public string CurrentWard { get; set; }
        public string CaseTypeFlag { get; set; }
        public string LegalOwner { get; set; }
        public string LegalOwnerFlag { get; set; }
        public string OtherHouse { get; set; }
        public string OtherHouseFlag { get; set; }
        public string DamageGrade { get; set; }
        public string TechnicalSolution { get; set; }
        public string BuildingCondition { get; set; }
        public string DocType { get; set; }
        public string DocTypeFlag { get; set; }
        public string SurveyedGrade { get; set; }
        public string PhotoGrade { get; set; }
        public string MatrixGrade { get; set; }
        public string OfficeGrade { get; set; }
        public string SurveyedTS { get; set; }
        public int GradeCount { get; set; }
        public int TSCount { get; set; }
        public string RecommendedTS { get; set; }
        public string Grade1 { get; set; }
        public string Grade2 { get; set; }
        public string Grade3 { get; set; }
        public string Grade4 { get; set; }
        public string Grade5 { get; set; }
        public string Grade6 { get; set; }
        public string TS1 { get; set; }
        public string TS2 { get; set; }
        public string TS3 { get; set; }
        public string TS4 { get; set; }
        public string TS5 { get; set; }

        public int TotalSumEL { get; set; }

        public string HouseOwnerId { get; set; }
        public string SlipNo { get; set; }

        public string gid { get; set; }

        #region properties added for 14 districts dashboard
        public int TOTAL_SURVEYED { get; set; }
        public int TARGETED { get; set; }
        public int RETROFITTING_BENEF { get; set; }
        public int GRIEVANCE_BENEF { get; set; }
        public int CASE_GRIEVANCE { get; set; }
        public int CASE_VERIFIED { get; set; }

        public int totalnew { get; set; }

        #endregion
        public string FileName { get; set; }


        #region properties added for 17 districts dashboard
        public int STOTAL_SURVEYED { get; set; }
        public int STARGETED { get; set; }
        public int SRETROFITTING_BENEF { get; set; }
        public int SGRIEVANCE_BENEF { get; set; }
        public int SCASE_GRIEVANCE { get; set; }
        public int SCASE_VERIFIED { get; set; }

        public int Stotalnew { get; set; }

        #endregion

        public int showing { get; set; }
        public int Index { get; set; }

        public int Total { get; set; }
        public int Remaining { get; set; }
        public string RecommendType { get; set; }

        public string RSRVType { get; set; }

        [Required(ErrorMessage = " ")]
        public string Donorcd { get; set; }

        [Required()]
        public string Pkgcd { get; set; }
        public string Mode { get; set; }
        public string Outrslt { get; set; }


        #region related to training module

        public string status_id { get; set; }
        public string traininglevel_id { get; set; }
        public string Fundingsource_id { get; set; }
        public string implementingpartner_id { get; set; }


        public string GenderId { get; set; }
        public string ExperienceID { get; set; }
        public string ParticipantTypeId { get; set; }
        public string TrainingTypeID { get; set; }

        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public int RE_SURVEY { get; set; }

        public int FIELD_OBS { get; set; }

        public int NON_BENEFECIARY { get; set; }

        public int BENEFICIARY { get; set; }

        public string ProfessionCd { get; set; }

        #endregion

        #region Related to New Training Module
        public string TrainingType { get; set; }
        public string ImplemntPartner { get; set; }
        public string Curriculum { get; set; }
        public string FundingSrc { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string HouseDesignNo { get; set; }

        #endregion

    }
}
