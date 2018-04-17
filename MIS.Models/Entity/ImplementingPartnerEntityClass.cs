using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table(Name = "NHRS_IMPLEMENTING_PARTNER")]
    public class ImplementingPartnerEntityClass: EntityBase
    {
        private System.Decimal? ImplementingPrtnerId = null;
        private System.Decimal? DefinedCd = null;
        private System.String DescEng = null;
        private System.String DescLoc = null;
        private System.String ShortNameEng = null;

        private System.String ShortNameLoc = null;
        private System.String Status = null;
        private System.String Active = null;
        private System.DateTime? StartDate = null;
        private System.String StartDateLoc = null;

        private System.DateTime? EndDate = null;
        private System.String EndDateLoc = null;
        private System.String EnteredBy = null;
        private System.DateTime? EnteredDate = null;
        private System.String EnteredDateLoc = null;

        private System.String Approvedd = null;
        private System.String ApprovedBy = null;
        private System.DateTime? ApprovedDate = null;
        private System.String ApprovedDateLoc = null;
        private System.String UpdatedBy = null;

        private System.DateTime? UpdatedDate = null;
        private System.String UpdatedDateLoc = null;
        private System.String IsTrinOrg = null;
        private System.String IsDonor = null;


        [Column(Name = "IMPLEMENTING_PARTNER_ID", IsKey = true, SequenceName = "")]
        public System.Decimal? implementingPrtnerId
        {
            get { return ImplementingPrtnerId; }
            set { ImplementingPrtnerId = value; }
        }


        [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
        public System.Decimal? definedCd
        {
            get { return DefinedCd; }
            set { DefinedCd = value; }
        }


        [Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
        public System.String descEng
        {
            get { return DescEng; }
            set { DescEng = value; }
        }

        [Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
        public System.String descLoc
        {
            get { return DescLoc; }
            set { DescLoc = value; }
        }


        [Column(Name = "SHORT_NAME_ENG", IsKey = false, SequenceName = "")]
        public System.String shortNameEng
        {
            get { return ShortNameEng; }
            set { ShortNameEng = value; }
        }

        [Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
        public System.String shortNameLoc
        {
            get { return ShortNameLoc; }
            set { ShortNameLoc = value; }
        }

        [Column(Name = "STATUS", IsKey = false, SequenceName = "")]
        public System.String status
        {
            get { return Status; }
            set { Status = value; }
        }


        [Column(Name = "ACTIVE", IsKey = false, SequenceName = "")]
        public System.String active
        {
            get { return Active; }
            set { Active = value; }
        }

        [Column(Name = "START_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? startDate
        {
            get { return StartDate; }
            set { StartDate = value; }
        }


        [Column(Name = "START_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String startDateLoc
        {
            get { return StartDateLoc; }
            set { StartDateLoc = value; }
        }

        [Column(Name = "END_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? endDate
        {
            get { return EndDate; }
            set { EndDate = value; }
        }

        [Column(Name = "END_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String endDateLoc
        {
            get { return EndDateLoc; }
            set { EndDateLoc = value; }
        }


        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public System.String enteredBy
        {
            get { return EnteredBy; }
            set { EnteredBy = value; }
        }

        [Column(Name = "ENTERED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? enteredDate
        {
            get { return EnteredDate; }
            set { EnteredDate = value; }
        }


        [Column(Name = "ENTERED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String enteredDateLoc
        {
            get { return EnteredDateLoc; }
            set { EnteredDateLoc = value; }
        }

        [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
        public System.String approvedd
        {
            get { return Approvedd; }
            set { Approvedd = value; }
        }

        [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
        public System.String approvedBy
        {
            get { return ApprovedBy; }
            set { ApprovedBy = value; }
        }


        [Column(Name = "APPROVED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? approvedDate
        {
            get { return ApprovedDate; }
            set { ApprovedDate = value; }
        }

        [Column(Name = "APPROVED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String approvedDateLoc
        {
            get { return ApprovedDateLoc; }
            set { ApprovedDateLoc = value; }
        }


        [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
        public System.String updatedBy
        {
            get { return UpdatedBy; }
            set { UpdatedBy = value; }
        }

        [Column(Name = "UPDATED_DATE", IsKey = false, SequenceName = "")]
        public System.DateTime? updatedDate
        {
            get { return UpdatedDate; }
            set { UpdatedDate = value; }
        }

        [Column(Name = "UPDATED_DATE_LOC", IsKey = false, SequenceName = "")]
        public System.String updatedDateLoc
        {
            get { return UpdatedDateLoc; }
            set { UpdatedDateLoc = value; }
        }
        [Column(Name = "DONOR", IsKey = false, SequenceName = "")]
        public System.String isDonor
        {
            get { return IsDonor; }
            set { IsDonor = value; }
        }
        [Column(Name = "TRAINING_ORG", IsKey = false, SequenceName = "")]
        public System.String isTrinOrg
        {
            get { return IsTrinOrg; }
            set { IsTrinOrg = value; }
        }
    }
}
