using EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Entity
{
    [Table (Name="NHRS_INSPECTION_PAPER_DTL")]
    public class InspectionPaperDetailEntityClass: EntityBase
    {
               private System.Decimal? INSPECTION_PAPER_ID = null;
               private System.Decimal? DEFINED_CD = null;

               private System.Decimal? INSPECTION_CODE_ID = null;
               private System.String NRA_DEFINED_CD = null;
               private System.Decimal? DISTRICT_CD = null;
               private System.Decimal? VDC_MUN_CD = null;
               private System.Decimal? WARD_CD = null;

               private System.String MAP_DESIGN = null;
               private System.String DESIGN_NUMBER = null;
               private System.Decimal? RC_MATERIAL_CD = null;
               private System.Decimal? FC_MATERIAL_CD = null;
               private System.String TECHNICAL_ASSIST = null;
               private System.String ORGANIZATION_TYPE = null;
               private System.String CONSTRUCTOR_TYPE = null;
               private System.String SOIL_TYPE = null;

               private System.String HOUSE_MODEL = null;
               private System.String PHOTO_CD_1 = null;
               private System.String PHOTO_CD_2 = null;
               private System.String PHOTO_CD_3 = null;
               private System.String PHOTO_CD_4 = null;
               private System.String PHOTO_1 = null;
               private System.String PHOTO_2 = null;
              private System.String PHOTO_3 = null;
              private System.String PHOTO_4 = null;

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

              private System.String INSPECTION_INSTALLMENT = null;



              [Column(Name = "INSPECTION_PAPER_ID", IsKey = true, SequenceName = "")]
              public System.Decimal? iNSPECTION_PAPER_ID
              {
                  get { return INSPECTION_PAPER_ID; }
                  set { INSPECTION_PAPER_ID = value; }
              }

              [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? dEFINED_CD
              {
                  get { return DEFINED_CD; }
                  set { DEFINED_CD = value; }
              }
              [Column(Name = "INSPECTION_CODE_ID", IsKey = false, SequenceName = "")]
              public System.Decimal? iNSPECTION_CODE_ID
              {
                  get { return INSPECTION_CODE_ID; }
                  set { INSPECTION_CODE_ID = value; }
              }
              [Column(Name = "NRA_DEFINED_CD", IsKey = false, SequenceName = "")]
              public System.String nRA_DEFINED_CD
              {
                  get { return NRA_DEFINED_CD; }
                  set { NRA_DEFINED_CD = value; }
              }
              [Column(Name = "DISTRICT_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? dISTRICT_CD
              {
                  get { return DISTRICT_CD; }
                  set { DISTRICT_CD = value; }
              }
              [Column(Name = "VDC_MUN_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? vDC_MUN_CD
              {
                  get { return VDC_MUN_CD; }
                  set { VDC_MUN_CD = value; }
              }
              [Column(Name = "WARD_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? wARD_CD
              {
                  get { return WARD_CD; }
                  set { WARD_CD = value; }
              }
              [Column(Name = "MAP_DESIGN", IsKey = false, SequenceName = "")]
              public System.String mAP_DESIGN
              {
                  get { return MAP_DESIGN; }
                  set { MAP_DESIGN = value; }
              }
              [Column(Name = "DESIGN_NUMBER", IsKey = false, SequenceName = "")]
              public System.String dESIGN_NUMBER
              {
                  get { return DESIGN_NUMBER; }
                  set { DESIGN_NUMBER = value; }
              }
              [Column(Name = "RC_MATERIAL_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? rC_MATERIAL_CD
              {
                  get { return RC_MATERIAL_CD ; }
                  set { RC_MATERIAL_CD = value; }
              }
              [Column(Name = "FC_MATERIAL_CD", IsKey = false, SequenceName = "")]
              public System.Decimal? fC_MATERIAL_CD
              {
                  get { return FC_MATERIAL_CD; }
                  set { FC_MATERIAL_CD = value; }
              }
              [Column(Name = "TECHNICAL_ASSIST", IsKey = false, SequenceName = "")]
              public System.String tECHNICAL_ASSIST
              {
                  get { return TECHNICAL_ASSIST; }
                  set { TECHNICAL_ASSIST = value; }
              }
              [Column(Name = "ORGANIZATION_TYPE", IsKey = false, SequenceName = "")]
              public System.String oRGANIZATION_TYPE
              {
                  get { return ORGANIZATION_TYPE; }
                  set { ORGANIZATION_TYPE = value; }
              }
              [Column(Name = "CONSTRUCTOR_TYPE", IsKey = false, SequenceName = "")]
              public System.String cONSTRUCTOR_TYPE
              {
                  get { return CONSTRUCTOR_TYPE; }
                  set { CONSTRUCTOR_TYPE = value; }
              }
              [Column(Name = "SOIL_TYPE", IsKey = false, SequenceName = "")]
              public System.String sOIL_TYPE
              {
                  get { return SOIL_TYPE; }
                  set { SOIL_TYPE = value; }
              }
              [Column(Name = "HOUSE_MODEL", IsKey = false, SequenceName = "")]
              public System.String hOUSE_MODEL
              {
                  get { return HOUSE_MODEL; }
                  set { HOUSE_MODEL = value; }
              }
              [Column(Name = "PHOTO_CD_1", IsKey = false, SequenceName = "")]
              public System.String pHOTO_CD_1
              {
                  get { return PHOTO_CD_1; }
                  set { PHOTO_CD_1 = value; }
              }
              [Column(Name = "PHOTO_CD_2", IsKey = false, SequenceName = "")]
              public System.String pHOTO_CD_2
              {
                  get { return PHOTO_CD_2; }
                  set { PHOTO_CD_2 = value; }
              }
              [Column(Name = "PHOTO_CD_3", IsKey = false, SequenceName = "")]
              public System.String pHOTO_CD_3
              {
                  get { return PHOTO_CD_3; }
                  set { PHOTO_CD_3 = value; }
              }
              [Column(Name = "PHOTO_CD_4", IsKey = false, SequenceName = "")]
              public System.String pHOTO_CD_4
              {
                  get { return PHOTO_CD_4; }
                  set { PHOTO_CD_4 = value; }
              }
              [Column(Name = "PHOTO_1", IsKey = false, SequenceName = "")]
              public System.String pHOTO_1
              {
                  get { return PHOTO_1; }
                  set { PHOTO_1 = value; }
              }
              [Column(Name = "PHOTO_2", IsKey = false, SequenceName = "")]
              public System.String pHOTO_2
              {
                  get { return PHOTO_2; }
                  set { PHOTO_2 = value; }
              }
              [Column(Name = "PHOTO_3", IsKey = false, SequenceName = "")]
              public System.String pHOTO_3
              {
                  get { return PHOTO_3; }
                  set { PHOTO_3 = value; }
              }
              [Column(Name = "PHOTO_4", IsKey = false, SequenceName = "")]
              public System.String pHOTO_4
              {
                  get { return PHOTO_4; }
                  set { PHOTO_4 = value; }
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

              [Column(Name = "INSPECTION_INSTALLMENT", IsKey = false, SequenceName = "")]
              public System.String iNSPECTION_INSTALLMENT
              {
                  get { return INSPECTION_INSTALLMENT; }
                  set { INSPECTION_INSTALLMENT = value; }
              }

         
    }
}
