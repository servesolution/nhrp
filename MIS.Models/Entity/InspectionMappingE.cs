using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table(Name = "NHRS_INSPECTION_MDL_MAPPING")]
    public class InspectionMappingE : EntityBase
    {
        public System.Decimal? InspectionMapCd = null;
        public System.String InspectionMapDefCd = null;
        public System.Decimal? InspectionMapConsCd = null;
        public System.Decimal? InspectionMapRoofCd = null;
        public System.Decimal? InspectionMapHouseCd = null;

        public System.String InspectionMapEnterBy = null;
        public System.DateTime? InspectionMapEnterDate = null;
        public System.String InspectionMapEnterDateLoc = null;

        public System.String InspectionMapApproved = null;
        public System.String InspectionMapApprovedBy = null;
        public System.DateTime? InspectionMapApprovedDate = null;
        public System.String InspectionMapApprovedDateLoc = null;

        public System.String InspectionMapUpdatedBy = null;
        public System.DateTime? InspectionMapUpdatedDate = null;
        public System.String InspectionMapUpdatedDateLoc = null;

        public System.String mode = null;

        [Column(Name = "MODEL_MAPPING_CD", IsKey = true, SequenceName = "")]
        public System.Decimal? InspectionmapCd
        {
            get { return InspectionMapCd; }
            set { InspectionMapCd = value; }
        }

        [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
        public System.String InspectionmapDefCd
        {
            get { return InspectionMapDefCd; }
            set { InspectionMapDefCd = value; }
        }

        [Column(Name = "CONSTRUCTION_MAT_CD", IsKey = false, SequenceName = "")]
        public System.Decimal? InspectionmapConsCd
        {
            get { return InspectionMapConsCd; }
            set { InspectionMapConsCd = value; }
        }

        [Column(Name = "ROOF_MAT_CD", IsKey = false, SequenceName = "")]
        public System.Decimal? InspectionmapRoofCd
        {
            get { return InspectionMapRoofCd; }
            set { InspectionMapRoofCd = value; }
        }

        [Column(Name = "HOUSE_MODEL_CD", IsKey = false, SequenceName = "")]
        public System.Decimal? InspectionmapHouseCd
        {
            get { return InspectionMapHouseCd; }
            set { InspectionMapHouseCd = value; }
        }

        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public System.String InspectionmapEnterBy
        {
            get { return InspectionMapEnterBy; }
            set { InspectionMapEnterBy = value; }
        }

        [Column(Name = "ENTERED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? InspectionmapEnterDate
        {
            get { return InspectionMapEnterDate; }
            set { InspectionMapEnterDate = value; }
        }
        [Column(Name = "ENTERED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String InspectionmapEnterDateLoc
        {
            get { return InspectionMapEnterDateLoc; }
            set { InspectionMapEnterDateLoc = value; }
        }

        [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
        public System.String InspectionmapApproved
        {
            get { return InspectionMapApproved; }
            set { InspectionMapApproved = value; }
        }
        [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
        public System.String InspectionmapApprovedBy
        {
            get { return InspectionMapApprovedBy; }
            set { InspectionMapApprovedBy = value; }
        }

        [Column(Name = "APPROVED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? InspectionmapApproveDate
        {
            get { return InspectionMapApprovedDate; }
            set { InspectionMapApprovedDate = value; }
        }
        [Column(Name = "APPROVED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String InspectionmapApprovedDateLoc
        {
            get { return InspectionMapApprovedDateLoc; }
            set { InspectionMapApprovedDateLoc = value; }
        }  
        [Column(Name= "UPDATED_BY",IsKey=false,SequenceName="")]
        public System.String InspectionmapUpdatedBy
        {
            get { return InspectionMapUpdatedBy; }
            set{InspectionMapUpdatedBy=value;}
        }
        [Column(Name = "UPDATED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? InspectionmapUpdatedDate
        {
            get { return InspectionMapUpdatedDate; }
            set { InspectionMapUpdatedDate = value; }
        }
        [Column(Name = "UPDATED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String InspectionmapUpdatedDateLoc
        {
            get { return InspectionMapUpdatedDateLoc; }
            set{InspectionMapUpdatedDateLoc=value;}
        }
    }
}
    

