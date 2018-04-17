using EntityFramework;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MIS.Models.Entity
//{
    [Table(Name = "NHRS_INSPECTION_DESC_DTL")]
    public class InspectionDetailEntityClass: EntityBase
    {
        public System.Decimal? INSPECTION_CODE_ID = null;
        public System.String DEFINED_CD = null;
        public System.Decimal? UPPER_INSPECTION_CODE_ID = null;
        public System.String GROUP_FLAG = null;
        public System.String VALUE_TYPE = null;
        public System.String DESC_ENG = null;
        public System.String DESC_LOC = null;
        public System.String SHORT_NAME = null;
        public System.String SHORT_NAME_LOC = null;

        public System.Decimal? ORDER_NO = null;
        public System.String DISABLED = null;
        public System.String APPROVED = null;
        public System.String APPROVED_BY = null;
        public System.DateTime? APPROVED_DT = null;
        public System.String APPROVED_DT_LOC = null;
        public System.String ENTERED_BY = null;
        public System.DateTime? ENTERED_DT = null;
        public System.String ENTERED_DT_LOC = null;

        public System.String ipAddress = null;

        [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
        public System.String IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        [Column(Name = "INSPECTION_CODE_ID", IsKey = true, SequenceName = "")]
        public System.Decimal? iNSPECTION_CODE_ID
        {
            get { return INSPECTION_CODE_ID; }
            set { INSPECTION_CODE_ID = value; }
        }
        [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
        public System.String dEFINED_CD
        {
            get { return DEFINED_CD; }
            set { DEFINED_CD = value; }
        }
        [Column(Name = "UPPER_INSPECTION_CODE_ID", IsKey = false, SequenceName = "")]
        public System.Decimal? uPPER_INSPECTION_CODE_ID
        {
            get { return UPPER_INSPECTION_CODE_ID; }
            set { UPPER_INSPECTION_CODE_ID = value; }
        }
        [Column(Name = "GROUP_FLAG", IsKey = false, SequenceName = "")]
        public System.String gROUP_FLAG
        {
            get { return GROUP_FLAG; }
            set { GROUP_FLAG = value; }
        }
        [Column(Name = "VALUE_TYPE", IsKey = false, SequenceName = "")]
        public System.String vALUE_TYPE
        {
            get { return VALUE_TYPE; }
            set { VALUE_TYPE = value; }
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
        [Column(Name = "SHORT_NAME", IsKey = false, SequenceName = "")]
        public System.String sHORT_NAME
        {
            get { return SHORT_NAME; }
            set { SHORT_NAME = value; }
        }
        [Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
        public System.String sHORT_NAME_LOC
        {
            get { return SHORT_NAME_LOC; }
            set { SHORT_NAME_LOC = value; }
        }
        [Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
        public System.Decimal? oRDER_NO
        {
            get { return ORDER_NO; }
            set { ORDER_NO = value; }
        }
        [Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
        public System.String dISABLED
        {
            get { return DISABLED; }
            set { DISABLED = value; }
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
        [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
        public System.DateTime? aPPROVED_DT
        {
            get { return APPROVED_DT; }
            set { APPROVED_DT = value; }
        }
        [Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
        public System.String aPPROVED_DT_LOC
        {
            get { return APPROVED_DT_LOC; }
            set { APPROVED_DT_LOC = value; }
        }
        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public System.String eNTERED_BY
        {
            get { return ENTERED_BY; }
            set { ENTERED_BY = value; }
        }
        [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
        public System.DateTime? eNTERED_DT
        {
            get { return ENTERED_DT; }
            set { ENTERED_DT = value; }
        }
        [Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
        public System.String eNTERED_DT_LOC
        {
            get { return ENTERED_DT_LOC; }
            set { ENTERED_DT_LOC = value; }
        }



    }
//}
