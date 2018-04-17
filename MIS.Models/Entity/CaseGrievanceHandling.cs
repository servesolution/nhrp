using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table(Name = "NHRS_GRIEVANCE_HANDLED")]
   public class CaseGrievanceHandling : EntityBase
    {
        private Decimal? grievanceId = null;
        private String houseOwnerId = null;
        private String houseownernameeng = null;
        private String buildingStructureNo = null;
        private Decimal? caseRegistrationId = null;
        private Decimal? surveyedDamageGradeCd = null;
        private Decimal? matrixGradeCode = null;
        private Decimal? officerGradeCd = null;

        private Decimal? photoGradeCd = null;
        private String grievanceOfficerCd = null;
        private String officerRecommendation = null;
        private String recommendationcode = null;
        private String clarificationcode = null;
        private String status = null;
        private String retargeted = null;
        private String remarks = null;
        private String remarksLoc = null;

        private String active = null;
        private String enteredBy = null;
        private String enteredDt = null;
        private String lastUpdatedBy = null;
        private String lastUpdatedDt = null;

        private String surveytechSolution = null;
        private String officerstechSolution = null;

        //private string mode = null;

        //[Column(Name = "MODE", IsKey = false, SequenceName = "")]
        //public string Mode
        //{
        //    get { return mode; }
        //    set { mode = value; }
        //}
        [Column(Name = "SURVEYED_TECH_SOLUTION_CD", IsKey = false, SequenceName = "")]
        public string SurveytechSolution
        {
            get { return surveytechSolution; }
            set { surveytechSolution = value; }
        }
        [Column(Name = "OFFICER_TECH_SOLUTION_CD", IsKey = false, SequenceName = "")]
        public string OfficerstechSolution
        {
            get { return officerstechSolution; }
            set { officerstechSolution = value; }
        }

        [Column(Name = "GRIEVANCE_ID", IsKey = false, SequenceName = "")]
        public Decimal? GrievanceId
        {
            get { return grievanceId; }
            set { grievanceId = value; }
        }
        [Column(Name = "HOUSE_OWNER_ID", IsKey = false, SequenceName = "")]
        public String HouseOwnerId
        {
            get { return houseOwnerId; }
            set { houseOwnerId = value; }
        }
        [Column(Name = "HOUSE_OWNER_NAME_ENG", IsKey = false, SequenceName = "")]
        public String HouseOWnerNameEng
        {
            get { return houseownernameeng; }
            set { houseownernameeng = value; }
        }
        [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = false, SequenceName = "")]
        public String BuildingStructureNo
        {
            get { return buildingStructureNo; }
            set { buildingStructureNo = value; }
        }
        [Column(Name = "CASE_REGISTRATION_ID", IsKey = false, SequenceName = "")]
        public Decimal? CaseRegistrationId
        {
            get { return caseRegistrationId; }
            set { caseRegistrationId = value; }
        }
        [Column(Name = "SURVEYED_DAMAGE_GRADE_CD", IsKey = false, SequenceName = "")]
        public Decimal? SurveyedDamageGradeCd
        {
            get { return surveyedDamageGradeCd; }
            set { surveyedDamageGradeCd = value; }
        }
        [Column(Name = "MATRIX_GRADE_CD", IsKey = false, SequenceName = "")]
        public Decimal? MatrixGradeCode
        {
            get { return matrixGradeCode; }
            set { matrixGradeCode = value; }
        }
        [Column(Name = "OFFICER_GRADE_CD", IsKey = false, SequenceName = "")]
        public Decimal? OfficerGradeCd
        {
            get { return officerGradeCd; }
            set { officerGradeCd = value; }
        }
        [Column(Name = "PHOTO_GRADE_CD", IsKey = false, SequenceName = "")]
        public Decimal? PhotoGradeCd
        {
            get { return photoGradeCd; }
            set { photoGradeCd = value; }
        }
        [Column(Name = "GRIEVANCE_OFFICER_CD", IsKey = false, SequenceName = "")]
        public String  GrievanceOfficerCd
        {
            get { return grievanceOfficerCd; }
            set { grievanceOfficerCd = value; }
        }
        [Column(Name = "OFFICER_RECOMMENDATION", IsKey = false, SequenceName = "")]
        public String OfficerRecommendation
        {
            get { return officerRecommendation; }
            set { officerRecommendation = value; }
        }
        [Column(Name = "RECOMMENDATION_CODE", IsKey = false, SequenceName = "")]
        public String RecommendationCode
        {
            get { return recommendationcode; }
            set { recommendationcode = value; }
        }
        [Column(Name = "CLARIFICATION_CODE", IsKey = false, SequenceName = "")]
        public String ClarificationCode
        {
            get { return clarificationcode; }
            set { clarificationcode = value; }
        }
        [Column(Name = "STATUS", IsKey = false, SequenceName = "")]
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        [Column(Name = "RETARGETED", IsKey = false, SequenceName = "")]
        public String Retargeted
        {
            get { return retargeted; }
            set { retargeted = value; }
        }
        [Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
        public String Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        [Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
        public String RemarksLoc
        {
            get { return remarksLoc; }
            set { remarksLoc = value; }
        }
        [Column(Name = "ACTIVE", IsKey = false, SequenceName = "")]
        public String Active
        {
            get { return active; }
            set { active = value; }
        }
        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public String EnteredBy
        {
            get { return enteredBy; }
            set { enteredBy = value; }
        }
        [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
        public String EnteredDt
        {
            get { return enteredDt; }
            set { enteredDt = value; }
        }
        [Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
        public String LastUpdatedBy
        {
            get { return lastUpdatedBy; }
            set { lastUpdatedBy = value; }
        }
        [Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
        public String LastUpdatedDt
        {
            get { return lastUpdatedDt; }
            set { lastUpdatedDt = value; }
        }

    }
}
