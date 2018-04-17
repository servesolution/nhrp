using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Inspection
{
    public class InspectionJsonImportModelClass
    {
    }

    public class JsonInspectionMst
    {
        public string INSPECTION_MST_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string INSPECTION_LEVEL0 { get; set; }
        public string INSPECTION_DATE { get; set; }
        public string INSPECTION_STATUS { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DATE { get; set; }
        public string ENTERED_DATE_LOC { get; set; }
        public string APPROVED { get; set; }
        public string APPROVED_BY { get; set; }
        public string APPROVED_DATE { get; set; }
        public string APPROVED_DATE_LOC { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string UPDATED_DATE_LOC { get; set; }
        public string NRA_DEFINED_CD { get; set; }
        public string INSPECTION_LEVEL1 { get; set; }
        public string INSPECTION_LEVEL2 { get; set; }
        public string INSPECTION_LEVEL3 { get; set; }
        public string COMPLY_FLAG { get; set; }
        public string FINAL_DECISION_2_APPROVE { get; set; }
        public string FINAL_DECISION_APPROVE { get; set; }
    }


    public class JsonInspectionDesign
    {
        public string INSPECTION_DESIGN_CD { get; set; }
        public string DEFINED_CD { get; set; }
        public string NRA_DEFINED_CD { get; set; }
        public string BENF_FULL_NAME { get; set; }
        public string DISTRICT_CD { get; set; }
        public string VDC_MUN_CD { get; set; }
        public string WARD { get; set; }
        public string TOLE { get; set; }
        public string OWN_DESIGN { get; set; }
        public string DESIGN_NUMBER { get; set; }
        public string CONSTRUCTION_MAT_CD { get; set; }
        public string ROOF_MAT_CD { get; set; }
        public string OTHER_CONSTRUCTION { get; set; }
        public string OTHER_ROOF { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DATE { get; set; }
        public string ENTERED_DATE_LOC { get; set; }
        public string APPROVED { get; set; }
        public string APPROVED_BY { get; set; }
        public string APPROVED_DATE { get; set; }
        public string APPROVED_DATE_LOC { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string UPDATED_DATE_LOC { get; set; }
        public string INSPECTION_MST_ID { get; set; }
        public string INSPECTION_STATUS { get; set; }
        public string KITTA_NUMBER { get; set; }
    }

    public class JsonInspectionApplication
    {
            public string INSPECTION_APPLICATION_CD  { get; set; }
            public string DEFINED_CD { get; set; }
            public string DISTRICT_CD { get; set; }
            public string VDC_MUN_CD { get; set; }
            public string WARD_CD { get; set; }
            public string NRA_DEFINED_CD { get; set; }
            public string APPLICANT_NAME { get; set; }
            public string TRANCHE { get; set; }
            public string INSPECTION_LEVEL { get; set; }
            public string FILE_BATCH { get; set; }
            public string REMARKS { get; set; }
            public string APPROVED { get; set; }
            public string APPROVED_BY { get; set; }
            public string APPROVED_DATE { get; set; }
            public string APPROVED_DATE_LOC { get; set; }
            public string ENTERED_BY { get; set; }
            public string ENTERED_DATE { get; set; }
            public string ENTERED_DATE_LOC { get; set; }
            public string UPDATED_BY { get; set; }
            public string UPDATED_DATE { get; set; }
            public string UPDATED_DATE_LOC { get; set; }
            public string REG_NUM { get; set; }
            public string REG_DATE { get; set; }
            public string SCHEDULE { get; set; }
            public string APP_FOR_INSP1 { get; set; }
            public string APP_FOR_INSP2 { get; set; }
            public string APP_FOR_INSP3 { get; set; }
    }


    public class JsonInspectionPaper
    {
                   public string  INSPECTION_PAPER_ID { get; set; }
                   public string  DEFINED_CD { get; set; }
                   public string  INSPECTION_CODE_ID { get; set; }
                   public string  NRA_DEFINED_CD { get; set; }
                   public string  DISTRICT_CD { get; set; }
                   public string  VDC_MUN_CD { get; set; }
                   public string  WARD_CD { get; set; }
                   public string  MAP_DESIGN { get; set; }
                   public string  DESIGN_NUMBER { get; set; }
                   public string  RC_MATERIAL_CD { get; set; }
                   public string  FC_MATERIAL_CD { get; set; }
                   public string  TECHNICAL_ASSIST { get; set; }
                   public string  ORGANIZATION_TYPE { get; set; }
                   public string  CONSTRUCTOR_TYPE { get; set; }
                   public string  SOIL_TYPE { get; set; }
                   public string  HOUSE_MODEL { get; set; }
                   public string  PHOTO_CD_1 { get; set; }
                   public string  PHOTO_CD_2 { get; set; }
                   public string  PHOTO_CD_3 { get; set; }
                   public string  PHOTO_CD_4 { get; set; }
                   public string  PHOTO_1 { get; set; }
                   public string  PHOTO_2 { get; set; }
                   public string  PHOTO_3 { get; set; }
                   public string  PHOTO_4 { get; set; }
                   public string  STATUS { get; set; }
                   public string  ACTIVE { get; set; }
                   public string  ENTERED_BY { get; set; }
                   public string  ENTERED_DATE { get; set; }
                   public string  ENTERED_DATE_LOC { get; set; }
                   public string  APPROVED { get; set; }
                   public string  APPROVED_BY { get; set; }
                   public string  APPROVED_DATE { get; set; }
                   public string  APPROVED_DATE_LOC { get; set; }
                   public string  UPDATED_BY { get; set; }
                   public string  UPDATED_DATE { get; set; }
                   public string  UPDATED_DATE_LOC { get; set; }
                   public string  INSPECTION_INSTALLMENT { get; set; }
                   public string  MAP_DESIGNE_CD { get; set; }
                   public string  PASSED_NAKSA_NO { get; set; }
                   public string  OTHERS_INFO { get; set; }
                   public string  FILE_BATCH_ID { get; set; }
                   public string  INSPECTION_TYPE { get; set; }
                   public string  INSPECTION_DATE { get; set; }
                   public string  BENEFICIARY_NAME { get; set; }
                   public string  TOLE { get; set; }
                   public string  LAND_PLOT_NUMBER { get; set; }
                   public string  ORGANIZATION_OTHERS { get; set; }
                   public string  PHOTO_CD_5 { get; set; }
                   public string  PHOTO_5 { get; set; }
                   public string  PHOTO_CD_6 { get; set; }
                   public string  PHOTO_6 { get; set; }
                   public string  FINAL_DECISION { get; set; }
                   public string  LATITUDE { get; set; }
                   public string  LONGITUDE { get; set; }
                   public string  ALTITUDE { get; set; }
                   public string  INSPECTION_MST_ID { get; set; }
                   public string  BANK_CD { get; set; }
                   public string  BANK_ACC_NUM { get; set; }
                   public string  SERIAL_NUMBER { get; set; }
                   public string  HOUSE_OWNER_ID { get; set; }
                   public string  FINAL_DECISION_APPROVE { get; set; }
                   public string  FINAL_DECISION_2_APPROVE { get; set; }
                   public string  APPROVE_BATCH_1 { get; set; }
                   public string  APPROVE_BATCH_2 { get; set; }
                   public string  FORM_PAD_NUMBER { get; set; }
                   public string  DESIGN_DETAILS { get; set; }
                   public string  EDIT_REQUIRED { get; set; }
                   public string  EDIT_REQUIRED_DETAILS { get; set; }
                   public string  BANK_SELECT { get; set; }
                   public string  BANK_BRANCH { get; set; }
                   public string  FINAL_REMARKS { get; set; }
                   public string  ACCEPT_THE_ENTRY { get; set; }
                   public string  GPS_TAKEN { get; set; }
                   public string  FINAL_DECISION_2 { get; set; }
                   public string  BANK_NOT_AVAILABLE_REMARKS { get; set; }
                   public string  MOBILE_NUMBER { get; set; }
                   public string  INSPECTION_LEVEL { get; set; }
                   public string APPROVE_BATCH { get; set; }
    }

    public class JsonInspectionDetail
    {
        public string INSPECTION_DETAIL_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string INSPECTION_PAPER_ID { get; set; }
        public string INSPECTION_ID { get; set; }
        public string INSPECTION_ELEMENT_ID { get; set; }
        public string VALUE_TYPE { get; set; }
        public string REMARKS { get; set; }
        public string HOUSE_MODEL { get; set; }
        public string COMPLY_ID { get; set; }
        public string COMPLY_FLAG { get; set; }
    }

    public class JonInspectionOwnDesign
    {
           public string  OWN_DESIGN_CD { get; set; }
           public string  DEFINED_CD { get; set; }
           public string  INSPECTION_PAPER_ID { get; set; }
           public string  DESIGN_NUMBER { get; set; }
           public string  BASE_CONSTRUCTIONG { get; set; }
           public string  BASE_CONSTRUCTED { get; set; }
           public string  GROUND_ROOF_FINISHED { get; set; }
           public string  GROUND_FLOOR_FINISHED { get; set; }
           public string  STOREY_COUNT { get; set; }
           public string  BASE_MATERIAL { get; set; }
           public string  BASE_DEPTH { get; set; }
           public string  BASE_WIDTH { get; set; }
           public string  BASE_HEIGHT { get; set; }
           public string  GROUND_FLOOR_MAT { get; set; }
           public string  GROUND_FLOOR_PRINCIPAL { get; set; }
           public string  WALL_DETAIL { get; set; }
           public string  FLOOR_ROOF_DESC { get; set; }
           public string  FLOOR_ROOF_MAT { get; set; }
           public string  FLOOR_ROOF_PRINCIPAL { get; set; }
           public string  FLOOR_ROOF_DTL { get; set; }
           public string  FIRST_FLOOR_MAT { get; set; }
           public string  FIRST_FLOOR_PRINCIPAL { get; set; }
           public string  FIRST_FLOOR_DTL { get; set; }
           public string  ROOF_MAT { get; set; }
           public string  ROOF_PRINCIPAL { get; set; }
           public string  ROOF_DTL { get; set; }
           public string  STATUS { get; set; }
           public string  ACTIVE { get; set; }
           public string  ENTERED_BY { get; set; }
           public string  ENTERED_DATE { get; set; }
           public string  ENTERED_DATE_LOC { get; set; }
           public string  APPROVED { get; set; }
           public string  APPROVED_BY { get; set; }
           public string  APPROVED_DATE { get; set; }
           public string  APPROVED_DATE_LOC { get; set; }
           public string  UPDATED_BY { get; set; }
           public string  UPDATED_DATE { get; set; }
           public string UPDATED_DATE_LOC { get; set; }
           public string CONSTRUCTION_STATUS { get; set; }
    }

    public class JsonInspectionProcess
    {
        public string INSPECTION_PROCESS_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string BENF_FULL_NAME { get; set; }
        public string RELATN_TO_BENF { get; set; }
        public string EXAMINAR_FULL_NAME { get; set; }
        public string EXAMINAR_DESIGNATION { get; set; }
        public string ENGINEER_FULL_NAME { get; set; }
        public string ENGINEER_DESIGNATION { get; set; }
        public string REGISTRATION_DATE { get; set; }
        public string PROCEED_DATE { get; set; }
        public string APPROVAL_DATE { get; set; }
        public string STATUS { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DATE { get; set; }
        public string ENTERED_DATE_LOC { get; set; }
        public string APPROVED { get; set; }
        public string APPROVED_BY { get; set; }
        public string APPROVED_DATE { get; set; }
        public string APPROVED_DATE_LOC { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DATE { get; set; }
        public string UPDATED_DATE_LOC { get; set; }
        public string INSPECTION_PAPER_ID { get; set; }
    }
}
