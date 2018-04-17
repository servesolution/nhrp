using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Setup;
using MIS.Models.Setup.Inspection;
namespace MIS.Models.Inspection
{
    public class InspectionDetailModelClass
    {
        public string mode { get; set; }
        public string modelCd { get; set; }
        public decimal? inspectionCd { get; set; }

        public string district_Cd { get; set; }
        public string vdc_mun_cd { get; set; }
        public string ward_no { get; set; }
        public string definedCd { get; set; }
        public decimal? upperInspectionCodeId { get; set; }
        public string groupFlag { get; set; }
        public string valuetype { get; set; }
        public string descEng { get; set; }
        public string descLoc { get; set; }
        public string shortname { get; set; }
        public string shortNameLoc { get; set; }
        public string orderNo { get; set; }
        public string disabled { get; set; }
        public string approved { get; set; }
        public string approvedBy { get; set; }
        public DateTime? approvedDt { get; set; }
        public string approvedDtLoc { get; set; }
        public string enteredBy { get; set; }
        public DateTime? enteredDt { get; set; }
        public string enteredDtLoc { get; set; }




        //get/set for approval to build house


        public string BenfFnameEng { get; set; }
        public string BenfMnameEng { get; set; }
        public string BenflnameEng { get; set; }
        public string BenfFnameLoc { get; set; }
        public string BenfMnameLoc { get; set; }
        public string BenfLnameLoc { get; set; }
        public string BenfullnameEng { get; set; }
        public string Benfsignature { get; set; }
        public string relationWithbenf { get; set; }
        public DateTime? reg_date { get; set; }


        public string SuprtntfFnameEng { get; set; }
        public string SuprtntMnameEng { get; set; }
        public string SuprtntlnameEng { get; set; }
        public string SuprtntFnameLoc { get; set; }
        public string SuprtntMnameLoc { get; set; }
        public string SuprtntLnameLoc { get; set; }
        public string SuprtntfFullnameEng { get; set; }
        public string designation { get; set; }
        public DateTime? proces_date { get; set; }
        public string Super_signature { get; set; }




        public string EngiFnameEng { get; set; }
        public string EngiMnameEng { get; set; }
        public string EngilnameEng { get; set; }
        public string EngiFnameLoc { get; set; }
        public string EngiMnameLoc { get; set; }
        public string EngiLnameLoc { get; set; }
        public string EngiFullNaEng { get; set; }
        public string EngiDesignation { get; set; }
        public DateTime? ApprDate { get; set; }
        public string engiSignature { get; set; }

        public string ststus { get; set; }
        public string InsPProcessId { get; set; }
        public string InsprocessDefId { get; set; }

        public String HOUSE_MODEL_CD { get; set; }
        public String INSPECTION_PAPER_ID { get; set; }
        public String HOUSE_DESIGN_CD { get; set; }
        public String Passed_map_no { get; set; }
        public String Others { get; set; }



        public String MAP_DESIGN { get; set; }
        public String DESIGN_NUMBER { get; set; }
        public String RC_MATERIAL_CD { get; set; }
        public String FC_MATERIAL_CD { get; set; }
        public String TECHNICAL_ASSIST { get; set; }





        public List<HouseModel> HouseModel { get; set; }
       
       

        public Decimal? countChild { get; set; } //added for table row 
        public virtual IList<InspectionDetailModelClass> Children { get; set; }

        public List<InspectionDetailModelClass> ListFirstParent { get; set; }
        public List<InspectionDetailModelClass> ListSecondParent { get; set; }
        public List<InspectionDetailModelClass> ListThirdParent { get; set; }
        public List<InspectionDetailModelClass> ListFourthParent { get; set; }
        public List<InspectionOwnerDetailModelClass> ListOwner { get; set; }


        public Decimal? inspectionModelDetailId { get; set; }
        public string modelId { get; set; }

    }

    public class InspectionItem
    {
        public List<InspectionComplyModelClass> objComplyModelList = new List<InspectionComplyModelClass>();

        /// <summary>
        /// Gets or sets the Inspection code. 
        /// </summary>
        public string INSPECTION_CODE_ID { get; set; }
        /// <summary>
        /// Gets or sets the upper Inspection code. 
        /// </summary>
        public string UPPER_INSPECTION_CODE_ID { get; set; }
        /// <summary>
        /// Gets or sets the label English. 
        /// </summary>
        public string Group_FLAG { get; set; }
        /// <summary>
        /// Gets or sets the label. 
        /// </summary>
        public string VALUE_TYPE { get; set; }
        /// <summary>
        /// Gets or sets the label Local. 
        /// </summary>
        public string DESC_ENG { get; set; }
        /// <summary>
        /// Gets or sets the Url of the link. 
        /// </summary>
        public string DESC_LOC { get; set; }
        /// <summary>
        /// Gets or sets the level of the menu item. 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the level of the menu item. 
        /// </summary>
        public int LastLevel { get; set; }
        /// <summary>
        /// Gets or sets the parent of the menu item. 
        /// </summary>
        public InspectionItem Parent { get; set; }
        /// <summary>
        /// Gets or sets the List of children of the Inspection item. 
        /// </summary>
        public List<InspectionItem> Children { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether the Inspection item has any children. 
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return (this.Children.Count > 0);
            }
        }

        /// <summary>
        /// Creates a new instance of the SCube.Core.InspectionItem class. 
        /// </summary>
        public InspectionItem()
        {
            Children = new List<InspectionItem>();
        }
    }
}
