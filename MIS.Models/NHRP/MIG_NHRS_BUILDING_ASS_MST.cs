using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_BUILDING_ASS_MST
    {
        public System.String houseOwnerId { get; set; }

        public System.Decimal? districtCd { get; set; }

        public System.String buildingStructureNo { get; set; }

        public System.Decimal? familyCntBefore { get; set; }

        public System.Decimal? familyCntAfter { get; set; }

        public System.Decimal? buildingConditionCd { get; set; }

        public System.Decimal? storeysCntBefore { get; set; }

        public System.Decimal? storeysCntAfter { get; set; }

        public System.Decimal? houseAge { get; set; }

        public System.Decimal? plinthAreaCd { get; set; }

        public System.String houseHeight { get; set; }

        public System.Decimal? groundSurfaceCd { get; set; }

        public System.Decimal? foundationTypeCd { get; set; }

        public System.Decimal? rcMaterialCd { get; set; }

        public System.Decimal? fcMaterialCd { get; set; }

        public System.Decimal? scMaterialCd { get; set; }

        public System.Decimal? superstructureMaterialCd { get; set; }

        public System.Decimal? buildingPositionCd { get; set; }

        public System.Decimal? buildingPlanCd { get; set; }

        public System.Decimal? assessedAreaCd { get; set; }

        public System.Decimal? techsolutionCd { get; set; }

        public System.String techsolutionComment { get; set; }

        public System.String techsolutionCommentLoc { get; set; }

        public System.String reconstructionStarted { get; set; }

        public System.String isGeographicRisk { get; set; }

        public System.Decimal? geographicRiskTypeCd { get; set; }

        public System.Decimal? shelterSinceQuakeCd { get; set; }

        public System.String eqVictimIdentityCard { get; set; }

        public System.String eqVictimIdentityCardNo { get; set; }

        public System.String rcvdEarthquakeReliefMoney { get; set; }

        public System.String humanDestroyFlag { get; set; }

        public System.Decimal? humanDestroyCnt { get; set; }

        public System.String approved { get; set; }

        public System.String approvedBy { get; set; }

        public System.String approvedByLoc { get; set; }

        public System.DateTime? approvedDt { get; set; }

        public System.String approvedDtLoc { get; set; }

        public System.String updatedBy { get; set; }

        public System.String updatedByLoc { get; set; }

        public System.DateTime? updatedDt { get; set; }

        public System.String updatedDtLoc { get; set; }

        public System.String enteredBy { get; set; }

        public System.String enteredByLoc { get; set; }

        public System.DateTime? enteredDt { get; set; }

        public System.String enteredDtLoc { get; set; }

        public System.String nhrsUuid { get; set; }

        public System.String ipAddress { get; set; }
    }
}
