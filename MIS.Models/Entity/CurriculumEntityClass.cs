using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table(Name="NHRS_CURRICULUM")]
    public class CurriculumEntityClass: EntityBase
    {
        private System.Decimal? CURRICULUM_ID = null;
        private System.Decimal? DEFINED_CD = null;
        private System.String DESC_ENG = null;
        private System.String DESC_LOC = null;

        private System.String SHORT_NAME_ENG = null;
        private System.String SHORT_NAME_LOC = null;
        private System.String STATUS = null;
        private System.String ACTIVE = null;

        private System.String ENTERED_BY = null;
        private System.DateTime? ENTERED_DATE = null;
        private System.String ENTERED_DATE_LOC = null;
        private System.String APPROVED = null;

        private System.String APPROVED_BY = null;
        private System.DateTime? APPROVED_DATE = null;
        private System.String APPROVED_DATE_LOC = null;
        private System.String UPDATED_BY = null;

        private System.DateTime? UPDATED_DATE = null;
        private System.String UPDATED_DATE_LOC = null;



        [Column(Name = "CURRICULUM_ID", IsKey = true, SequenceName = "")]
        public System.Decimal? cURRICULUM_ID 
        {
            get { return CURRICULUM_ID; }
            set { CURRICULUM_ID = value; }
        }
        [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
        public System.Decimal? dEFINED_CD 
        {
            get { return DEFINED_CD; }
            set { DEFINED_CD = value; }
        }
        [Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
        public System.String dESC_ENG 
        {
            get { return DESC_ENG; }
            set { DESC_ENG = value; }
        }
        [Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
        public System.String dESC_LOC 
        {
            get { return DESC_LOC; }
            set { DESC_LOC = value; }
        }
        [Column(Name = "SHORT_NAME_ENG", IsKey = false, SequenceName = "")]
        public System.String sHORT_NAME_ENG 
        {
            get { return SHORT_NAME_ENG; }
            set { SHORT_NAME_ENG = value; }
        }
        [Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
        public System.String sHORT_NAME_LOC 
        {
            get { return SHORT_NAME_LOC; }
            set { SHORT_NAME_LOC = value; }
        }
        [Column(Name = "STATUS", IsKey = false, SequenceName = "")]
        public System.String sTATUS 
        {
            get { return STATUS; }
            set { STATUS = value; }
        }
        [Column(Name = "ACTIVE", IsKey = false, SequenceName = "")]
        public System.String aCTIVE 
        {
            get { return ACTIVE; }
            set { ACTIVE = value; }
        }
        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public System.String eNTERED_BY 
        {
            get { return ENTERED_BY; }
            set { ENTERED_BY = value; }
        }
        [Column(Name = "ENTERED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? eNTERED_DATE 
        {
            get { return ENTERED_DATE; }
            set { ENTERED_DATE = value; }
        }
        [Column(Name = "ENTERED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String eNTERED_DATE_LOC 
        {
            get { return ENTERED_DATE_LOC; }
            set { ENTERED_DATE_LOC = value; }
        }
        [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
        public System.String aPPROVED 
        {
            get { return APPROVED; }
            set { APPROVED = value; }
        }
        [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
        public System.String aPPROVED_BY 
        {
            get { return APPROVED_BY; }
            set { APPROVED_BY = value; }
        }
        [Column(Name = "APPROVED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? aPPROVED_DATE 
        {
            get { return APPROVED_DATE; }
            set { APPROVED_DATE = value; }
        }
        [Column(Name = "APPROVED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String aPPROVED_DATE_LOC 
        {
            get { return APPROVED_DATE_LOC; }
            set { APPROVED_DATE_LOC = value; }
        }
        [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
        public System.String uPDATED_BY 
        {
            get { return UPDATED_BY; }
            set { UPDATED_BY = value; }
        }
        [Column(Name = "UPDATED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? uPDATED_DATE 
        {
            get { return UPDATED_DATE; }
            set { UPDATED_DATE = value; }
        }
        [Column(Name = "UPDATED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String uPDATED_DATE_LOC 
        {
            get { return UPDATED_DATE_LOC; }
            set { UPDATED_DATE_LOC = value; }
        }
       
        
    }
}
