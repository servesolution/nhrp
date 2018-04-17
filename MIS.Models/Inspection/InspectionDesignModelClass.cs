using MIS.Models.Setup.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MIS.Models.Inspection
{
    public class InspectionDesignModelClass
    {
        public String DesignCd { get; set; }
        public String DefCd { get; set; }
        public string  BenfFirstName { get; set; }
        public String BenfMiddleName { get; set; }
        public String BenfLastName { get; set; }
        public String BenfFullNameEng { get; set; } 
       

        public String district_Cd { get; set; }
        public String vdc_mun_cd { get; set; }
        public String VdcDefCode { get; set; }
        public String ward_no { get; set; }
        public String tole { get; set; }
        public String NraDefCode { get; set; }

        public String OwnDesign { get; set; }
        public String DesignNumber { get; set; }
        public String ConstructMat { get; set; }
        public String Roofmat { get; set; }
        public String ConstMaterialDesc { get; set; }
        public String RoofMaterialDesc { get; set; }
        public String ConstOther { get; set; }
        public String RoofOther { get; set; }
        


        public String InspectionStatus { get; set; }
        public String InspectionLevel0 { get; set; }
        public String InspectionLevel1 { get; set; }
        public String InspectionLevel2 { get; set; }
        public String InspectionLevel3 { get; set; }
        public String Mode { get; set; }
        public String InspectionMstId { get; set; }
        public String hOwnerId { get; set; }
        public String InspectionLevel { get; set; }
        public String InspectionPaperID { get; set; }


        public String INSP_ONE_FROM_TAB { get; set; }
        public String INSP_TWO_FROM_TAB { get; set; }
        public String INSP_THREE_FROM_TAB { get; set; }


        // for other inspection Information
        public String PHOTO_ID { get; set; }//from photo table
        public String PHOTO_1 { get; set; }
        public String PHOTO_2 { get; set; }
        public String PHOTO_3 { get; set; }
        public String PHOTO_4 { get; set; }
        public String PHOTO_5 { get; set; }
        public String PHOTO_6 { get; set; }
        public String PHOTO_7 { get; set; }
        public String PHOTO_8 { get; set; }


        public String PHOTO_CD_1 { get; set; }
        public String PHOTO_CD_2 { get; set; }
        public String PHOTO_CD_3 { get; set; }
        public String PHOTO_CD_4 { get; set; }
        public String PHOTO_CD_5 { get; set; }
        public String PHOTO_CD_6 { get; set; }
        public String PHOTO_CD_7 { get; set; }
        public String PHOTO_CD_8 { get; set; }

        public String LONGITUDE { get; set; }
        public String LATITUDE { get; set; }
        public String ATTITUDE { get; set; }

        public String PHOTO_HOUSE { get; set; }
        public String finalDecision { get; set; }
        public String finalDecision2 { get; set; }
        public String TECHNICAL_ASSIST { get; set; }
        public String ORGANIZATION_TYPE { get; set; }
        public String ORGANIZATION_OTHERS { get; set; }
        public String CONSTRUCTOR_TYPE { get; set; }
        public String SOIL_TYPE { get; set; }



        public string FurtherProcessStatus { get; set; }
        public string FurtherProcessAccept { get; set; }
        public string ProcessRevision { get; set; }



        public string BeneficiaryFullName { get; set; }
        public string Benfsignature { get; set; }
        public string BenfRelation { get; set; }
        public DateTime? BenfSignDate { get; set; }

        public string SuprtntfFullnameEng { get; set; }
        public string SupertntDesignation { get; set; }
        public DateTime? SupertntSignDate { get; set; }
        public string SuprtntSign { get; set; }


        public string EngineerFullname { get; set; }
        public string EngineerDesignation { get; set; }
        public DateTime? EngineerSignDate { get; set; }
        public String EngineerSignDateLoc { get; set; }
        public String SupertntSignDateLoc { get; set; }
        public string BenfSignDateLoc { get; set; }
        public string EngineerSign { get; set; }

        public string GharkoNaksaUrl { get; set; }
        public string photo1Url { get; set; }
        public string photo2Url { get; set; }
        public string photo3Url { get; set; }
        public string photo4Url { get; set; }
        public string photo5Url { get; set; }
        public string photo6Url { get; set; }
        public string photo7Url { get; set; }
        public string photo8Url { get; set; }


        public string complyCd { get; set; }
        public string complyFlag { get; set; }
        public string ElementRemarks { get; set; }
       // db table variable 
       // public String InspectionPaperId { get; set; }

        public String Bank_ACC_Num { get; set; }
        public String Bank_Name { get; set; }
        public String Serial_Num { get; set; }
        public String Final_Remarks { get; set; }
        public String Final_Remarks_eng { get; set; }
        public String Inspection_date { get; set; }
        public String Inspection_date_loc { get; set; }

        public String MobileNumber { get; set; }
        public String form_pad_number { get; set; }
        public String design_detail { get; set; }
        public String edit_required { get; set; }

        public String edit_required_detail { get; set; }
        public String bank_select { get; set; }
        public String bank_branch { get; set; }
        public String accept_the_entry { get; set; }
        public String gps_taken { get; set; }
        public String final_decision_2 { get; set; }
        public String bank_not_available_remarks { get; set; }
        public String final_decision_approve { get; set; }
        public String final_decision_2_approve { get; set; }
        public String approve_batch { get; set; }
        public String approve_batch_2 { get; set; }


        public String KittaNumber { get; set; }
        public String Inspection { get; set; }
        public String Hierarchy_cd { get; set; }



        public String INSP_ONE_MOUD_APPROVE { get; set; }
        public String INSP_TWO_MOUD_APPROVE { get; set; }
        public String INSP_THREE_MOUD_APPROVE { get; set; }
        public String INSP_ONE_MOFALD_APPROVE { get; set; }
        public String INSP_TWO_MOFALD_APPROVE { get; set; }
        public String INSP_THREE_MOFALD_APPROVE { get; set; }
        public String INSP_ONE_ENGI_APPROVE { get; set; }
        public String INSP_TWO_ENGI_APPROVE { get; set; }
        public String INSP_THREE_ENGI_APPROVE { get; set; }
        public String INSPECTION_ENGINEER_AGREEMENT { get; set; }

        public List<InspectionOwnerDetailModelClass> ListOwner { get; set; }
        public List<InspectionDesignModelClass> RoofMaterialList { get; set; }
        public List<InspectionDesignModelClass> ConstructMaterialList { get; set; }
        public List<InspectionComplyModelClass> InspectionEditList { get; set; }// for editing Inspection 1 

        public List<InspectionItem> InspectionCheckedList { get; set; }

        public HttpPostedFileBase FrontFile { get; set; }
        public HttpPostedFileBase BackFile { get; set; }
        public HttpPostedFileBase TopFile { get; set; }
        public HttpPostedFileBase ButtomtFile { get; set; }
        public HttpPostedFileBase RightFile { get; set; }
        public HttpPostedFileBase LeftFile { get; set; }
        public HttpPostedFileBase MapFile { get; set; }
        public HttpPostedFileBase FormFile { get; set; }
        public IEnumerable<HttpPostedFileBase> MultiFile { get; set; }
        
    }



    

    
}
