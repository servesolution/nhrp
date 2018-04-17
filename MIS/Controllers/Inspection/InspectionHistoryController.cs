using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Inspection
{
    public class InspectionHistoryController : BaseController
    {
        //
        // GET: /InspectionHistory/
        CommonFunction com = new CommonFunction();
        public ActionResult ManageInspectionHistory()
        {

            string userDistrict = "";
            if (userDistrict != "")
            {
                ViewData["ddl_Districts"] = com.GetDistricts(userDistrict);
            }
            else
            {
                ViewData["ddl_Districts"] = com.GetDistricts("");
            }

            ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_Inspection_number"] = com.GetInspectionLevel("");
            ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
            return View();
        }

        [HttpPost]
        public ActionResult ManageInspectionHistory(FormCollection Fc)
        {


            //string district_Cd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            //string vdc_mun_cd = com.GetCodeFromDataBase(Fc["vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            //string ward_no = Fc["ward"].ConvertToString(); 
            //string InspLevel = Fc["ddl_Inspection_number"].ConvertToString();
            string district_Cd = "";
            string vdc_mun_cd = "";
            string ward_no = "";
            string InspLevel = "";
            string Id = Fc["TB1"].ConvertToString() + "-" + Fc["TB2"].ConvertToString() + "-" + Fc["TB3"].ConvertToString() + "-" + Fc["TB4"].ConvertToString() + "-" + Fc["TB5"].ConvertToString();
            if (Id == "----")
            {
                Id = "";
            }
            if (Session["EngineerApprovePa"].ConvertToString().Trim() != "")
            {
                Id = Session["EngineerApprovePa"].ConvertToString();
                Session["EngineerApprovePa"] = null;
            }


            DataTable dtFinal = new DataTable();
          
            string cmd = "";
            InspectionHistoryService objServ = new InspectionHistoryService();
            DataTable dt = new DataTable();
            dt = objServ.getHistory(district_Cd, vdc_mun_cd, ward_no, InspLevel, Id);
            if(dt.Rows.Count==1)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    dt.Rows.RemoveAt(0);
                    break;
                }
                cmd = "Select INSPECTION_HISTORY_ID ";
                dtFinal = objServ.getUpdatedata(cmd, district_Cd, vdc_mun_cd, ward_no, InspLevel, Id);
                ViewData["UpdatedData"] = dtFinal;

                ViewData["UpdatedData"] = dtFinal; 
                return PartialView("_InspectionUpdatedHistory");
            }
            DataView view = new DataView(dt);
            int count = dt.Rows.Count;
         
            
           
            DataTable distinctValues = view.ToTable(true,
                                                        "MST_INSP_ONE_MOUD_APPROVE" 
                                                        );



            distinctValues = view.ToTable(true, "INSPECTION_HISTORY_ID");
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = "Select INSPECTION_HISTORY_ID, MST_NRA_DEFINED_CD ";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_MST_ID"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_INSPECTION_MST_ID";
                distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_DEFINED_CD"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                //dtt = dt.DefaultView.ToTable(false, "MST_DEFINED_CD");
                //distinctValues = null;
                cmd = cmd + " ,MST_DEFINED_CD";
                distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_INSPECTION_LEVEL0"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_INSPECTION_LEVEL0";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_DATE"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_INSPECTION_DATE";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_STATUS"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_INSPECTION_STATUS";
                 distinctValues = null;
            }



             distinctValues = view.ToTable(true, "MST_ENTERED_BY"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1) 
            {
                cmd = cmd + " ,MST_ENTERED_BY";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_ENTERED_DATE"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_ENTERED_DATE";
                 distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_ENTERED_DATE_LOC"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_ENTERED_DATE_LOC";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_APPROVED"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_APPROVED";
                 distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_APPROVED_BY"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_APPROVED_BY";
                 distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_APPROVED_DATE"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_APPROVED_DATE";
                 distinctValues = null;
            }

            
             distinctValues = view.ToTable(true, "MST_APPROVED_DATE_LOC"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_APPROVED_DATE_LOC";
                 distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_UPDATED_BY"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_UPDATED_BY";
                distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_UPDATED_DATE"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_UPDATED_DATE";
                distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_UPDATED_DATE_LOC"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_UPDATED_DATE_LOC";
                distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_NRA_DEFINED_CD"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_NRA_DEFINED_CD";
                 distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_LEVEL1"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_INSPECTION_LEVEL1";
                 distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_LEVEL2"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_INSPECTION_LEVEL2";
                 distinctValues = null;
            }

             distinctValues = view.ToTable(true, "MST_INSPECTION_LEVEL3"  );
             if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
             {
                 cmd = cmd + " ,MST_INSPECTION_LEVEL3";
                 distinctValues = null;
            }

            distinctValues = view.ToTable(true, "MST_COMPLY_FLAG"  );
            if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
            {
                cmd = cmd + " ,MST_COMPLY_FLAG";
                distinctValues = null;
            }

              distinctValues = view.ToTable(true, "MST_FINAL_DECISION_2_APPROVE"  );
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,MST_FINAL_DECISION_2_APPROVE";
                  distinctValues = null;
            }

              distinctValues = view.ToTable(true, "MST_FINAL_DECISION_APPROVE"  );
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,MST_FINAL_DECISION_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_INSPECTION_DESIGN_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_INSPECTION_DESIGN_CD";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "DEG_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_DEFINED_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_NRA_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_NRA_DEFINED_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_BENF_FULL_NAME");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_BENF_FULL_NAME";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_DISTRICT_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_DISTRICT_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_VDC_MUN_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_VDC_MUN_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_WARD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_WARD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_TOLE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_TOLE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_OWN_DESIGN");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_OWN_DESIGN";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_DESIGN_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_DESIGN_NUMBER";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_CONSTRUCTION_MAT_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_CONSTRUCTION_MAT_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_ROOF_MAT_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_ROOF_MAT_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_OTHER_CONSTRUCTION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_OTHER_CONSTRUCTION";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_OTHER_ROOF");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_OTHER_ROOF";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_ENTERED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd = cmd + " ,DEG_ENTERED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_ENTERED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_ENTERED_DATE";

                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_ENTERED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_ENTERED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_APPROVED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_APPROVED";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_APPROVED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_APPROVED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_APPROVED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_APPROVED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_APPROVED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_APPROVED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_UPDATED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_UPDATED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_UPDATED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_UPDATED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_UPDATED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_UPDATED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_INSPECTION_MST_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_INSPECTION_MST_ID";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "DEG_INSPECTION_STATUS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_INSPECTION_STATUS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "DEG_KITTA_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,DEG_KITTA_NUMBER";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_INSPECTION_PAPER_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_PAPER_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DEFINED_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_INSPECTION_CODE_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_CODE_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_NRA_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_NRA_DEFINED_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_DISTRICT_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DISTRICT_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_VDC_MUN_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_VDC_MUN_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_WARD_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_WARD_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_MAP_DESIGN");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_MAP_DESIGN";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_DESIGN_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DESIGN_NUMBER";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_RC_MATERIAL_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_RC_MATERIAL_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FC_MATERIAL_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FC_MATERIAL_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_TECHNICAL_ASSIST");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_TECHNICAL_ASSIST";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_ORGANIZATION_TYPE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ORGANIZATION_TYPE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_CONSTRUCTOR_TYPE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_CONSTRUCTOR_TYPE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_SOIL_TYPE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_SOIL_TYPE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_HOUSE_MODEL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_HOUSE_MODEL";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_1");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_1";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_2");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_2";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_3");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_3";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_4");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_4";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PHOTO_1");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_1";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_2");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_2";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PHOTO_3");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_3";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_4");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_4";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "PPR_STATUS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_STATUS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_ACTIVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ACTIVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_ENTERED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ENTERED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_ENTERED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ENTERED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_ENTERED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ENTERED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_APPROVED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVED";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_APPROVED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_APPROVED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_APPROVED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_UPDATED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_UPDATED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_UPDATED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_UPDATED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_UPDATED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_UPDATED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_INSPECTION_INSTALLMENT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_INSTALLMENT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_MAP_DESIGNE_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_MAP_DESIGNE_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PASSED_NAKSA_NO");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PASSED_NAKSA_NO";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_OTHERS_INFO");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_OTHERS_INFO";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FILE_BATCH_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FILE_BATCH_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_INSPECTION_TYPE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_TYPE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_INSPECTION_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_BENEFICIARY_NAME");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BENEFICIARY_NAME";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "PPR_TOLE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_TOLE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_LAND_PLOT_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_LAND_PLOT_NUMBER";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_ORGANIZATION_OTHERS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ORGANIZATION_OTHERS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_5");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_5";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_5");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_5";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_PHOTO_CD_6");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_CD_6";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_PHOTO_6");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_PHOTO_6";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FINAL_DECISION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FINAL_DECISION";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_LATITUDE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_LATITUDE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_LONGITUDE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_LONGITUDE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_ALTITUDE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ALTITUDE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_INSPECTION_MST_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_MST_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_BANK_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BANK_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_BANK_ACC_NUM");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BANK_ACC_NUM";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_SERIAL_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_SERIAL_NUMBER";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_HOUSE_OWNER_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_HOUSE_OWNER_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_MOBILE_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_MOBILE_NUMBER";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FORM_PAD_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FORM_PAD_NUMBER";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_DESIGN_DETAILS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DESIGN_DETAILS";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_EDIT_REQUIRED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_EDIT_REQUIRED";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_EDIT_REQUIRED_DETAILS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_EDIT_REQUIRED_DETAILS";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_BANK_SELECT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BANK_SELECT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_BANK_BRANCH");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BANK_BRANCH";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FINAL_REMARKS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FINAL_REMARKS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_ACCEPT_THE_ENTRY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ACCEPT_THE_ENTRY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_GPS_TAKEN");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_GPS_TAKEN";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_FINAL_DECISION_2");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FINAL_DECISION_2";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_BANK_NOT_AVAILABLE_REMARKS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_BANK_NOT_AVAILABLE_REMARKS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_FINAL_DECISION_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FINAL_DECISION_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FINAL_DECISION_2_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FINAL_DECISION_2_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_APPROVE_BATCH");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVE_BATCH";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_APPROVE_BATCH_2");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_APPROVE_BATCH_2";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_INSPECTION_LEVEL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INSPECTION_LEVEL";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_OWN_DESIGN_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_OWN_DESIGN_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_DEFINED_CD";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_INSPECTION_PAPER_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_INSPECTION_PAPER_ID";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_DESIGN_NUMBER");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_DESIGN_NUMBER";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_BASE_CONSTRUCTIONG");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_CONSTRUCTIONG";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_BASE_CONSTRUCTED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_CONSTRUCTED";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_GROUND_ROOF_FINISHED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_GROUND_ROOF_FINISHED";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_GROUND_FLOOR_FINISHED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_GROUND_FLOOR_FINISHED";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "OWN_STOREY_COUNT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_STOREY_COUNT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_BASE_MATERIAL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_MATERIAL";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "OWN_BASE_DEPTH");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_DEPTH";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_BASE_WIDTH");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_WIDTH";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_BASE_HEIGHT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_BASE_HEIGHT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_GROUND_FLOOR_MAT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_GROUND_FLOOR_MAT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_GROUND_FLOOR_PRINCIPAL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_GROUND_FLOOR_PRINCIPAL";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_WALL_DETAIL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_WALL_DETAIL";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_FLOOR_ROOF_DESC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FLOOR_ROOF_DESC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_FLOOR_ROOF_MAT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FLOOR_ROOF_MAT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_FLOOR_ROOF_PRINCIPAL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FLOOR_ROOF_PRINCIPAL";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_FLOOR_ROOF_DTL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FLOOR_ROOF_DTL";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_FIRST_FLOOR_MAT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FIRST_FLOOR_MAT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_FIRST_FLOOR_PRINCIPAL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FIRST_FLOOR_PRINCIPAL";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_FIRST_FLOOR_DTL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_FIRST_FLOOR_DTL";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_ROOF_MAT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ROOF_MAT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_ROOF_PRINCIPAL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ROOF_PRINCIPAL";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_ROOF_DTL");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ROOF_DTL";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "OWN_STATUS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_STATUS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_ACTIVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ACTIVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_ENTERED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ENTERED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_ENTERED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ENTERED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_ENTERED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_ENTERED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_APPROVED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_APPROVED";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_APPROVED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_APPROVED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_APPROVED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_APPROVED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_APPROVED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_APPROVED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_UPDATED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_UPDATED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_UPDATED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_UPDATED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "OWN_UPDATED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_UPDATED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "OWN_CONSTRUCTION_STATUS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,OWN_CONSTRUCTION_STATUS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_INSPECTION_PROCESS_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_INSPECTION_PROCESS_ID";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "PRS_DEFINED_CD");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_DEFINED_CD";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_BENF_FULL_NAME");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_BENF_FULL_NAME";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_RELATN_TO_BENF");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_RELATN_TO_BENF";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_EXAMINAR_FULL_NAME");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_EXAMINAR_FULL_NAME";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_EXAMINAR_DESIGNATION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_EXAMINAR_DESIGNATION";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_ENGINEER_FULL_NAME");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_ENGINEER_FULL_NAME";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_ENGINEER_DESIGNATION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_ENGINEER_DESIGNATION";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_REGISTRATION_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_REGISTRATION_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_PROCEED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_PROCEED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_APPROVAL_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_APPROVAL_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_STATUS");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_STATUS";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_ENTERED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_ENTERED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_ENTERED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_ENTERED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_ENTERED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_ENTERED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_APPROVED");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_APPROVED";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_APPROVED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_APPROVED_BY";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_APPROVED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_APPROVED_DATE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_APPROVED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_APPROVED_DATE_LOC";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_UPDATED_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_UPDATED_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_UPDATED_DATE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_UPDATED_DATE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PRS_UPDATED_DATE_LOC");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_UPDATED_DATE_LOC";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PRS_INSPECTION_PAPER_ID");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PRS_INSPECTION_PAPER_ID";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOUD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOUD_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOUD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOUD_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_THREE_MOUD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THREE_MOUD_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOFALD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOFALD_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOFALD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOFALD_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_THREE_MOFALD_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THREE_MOFALD_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_ONE_ENGI_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_ENGI_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_TWO_ENGI_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_ENGI_APPROVE";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_THREE_ENGI_APPROVE");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THREE_ENGI_APPROVE";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_ONE_FROM_TAB");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_FROM_TAB";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_TWO_FROM_TAB");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_FROM_TAB";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_THREE_FROM_TAB");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THREE_FROM_TAB";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOUD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOUD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOUD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOUD_APPROVE_DT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOUD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOUD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOUD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOUD_APPROVE_DT";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_THREE_MOUD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THREE_MOUD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_THR_MOUD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THR_MOUD_APPROVE_DT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOFALD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOFALD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_ONE_MOFALD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_ONE_MOFALD_APPROVE_DT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOFALD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOFALD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_TWO_MOFALD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_TWO_MOFALD_APPROVE_DT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "MST_INSP_THR_MOFALD_APPROVE_BY");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THR_MOFALD_APPROVE_BY";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "MST_INSP_THR_MOFALD_APPROVE_DT");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,MST_INSP_THR_MOFALD_APPROVE_DT";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_ENGINEERS_APPROVE_FLAG");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_ENGINEERS_APPROVE_FLAG";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_INCOMPLETE_FLAG");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_INCOMPLETE_FLAG";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_FROM_TAB");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_FROM_TAB";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_DATE_OF_APPLICATION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DATE_OF_APPLICATION";
                  distinctValues = null;
              }

              distinctValues = view.ToTable(true, "PPR_YEAR_OF_INSPECTION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_YEAR_OF_INSPECTION";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_MONTH_OF_INSPECTION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_MONTH_OF_INSPECTION";
                  distinctValues = null;
              }


              distinctValues = view.ToTable(true, "PPR_DAY_OF_INSPECTION");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_DAY_OF_INSPECTION";
                  distinctValues = null;
              }
              distinctValues = view.ToTable(true, "PPR_GPS_TAKEN_FLAG");
              if (count == distinctValues.Rows.Count || distinctValues.Rows.Count > 1)
              {
                  cmd =cmd+ " ,PPR_GPS_TAKEN_FLAG";
                  distinctValues = null;
              }



               dtFinal = objServ.getUpdatedata(cmd, district_Cd, vdc_mun_cd, ward_no, InspLevel, Id);
              ViewData["UpdatedData"] = dtFinal;

              return PartialView("_InspectionUpdatedHistory");
        }

    }
}  
   
      