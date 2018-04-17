using EntityFramework;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using ExceptionHandler;
using System.Data;
using System.Web.Mvc;

namespace MIS.Services.Inspection
{
    public class DeputedEngineersDetailService
    {
        public bool CRUDEngineersInformation(DeputedEngineersModelClass objModel)
        {
            bool result = false;
            QueryResult qr = null;
            string enteredDateLoc  = NepaliDate.getNepaliDate((DateTime.Now ).ConvertToString("MM/dd/yyyy"));
            string updatedDateLoc = NepaliDate.getNepaliDate((DateTime.Now ).ConvertToString("MM/dd/yyyy"));
            using(ServiceFactory sf= new ServiceFactory())
            {
                sf.Begin();
               
                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_DEPUTED_ENGINEER_INFO";
                    qr = sf.SubmitChanges("PR_MANAGE_DEPUTED_ENGINEERS", 
                                         objModel.Mode,
                                         objModel.EngineerId.ToDecimal(),
                                         DBNull.Value,//defined cd  -type varchar
                                         objModel.DistrictCd.ToDecimal(),
                                         objModel.VdcMunCd.ToDecimal(),
                                         objModel.Ward.ToDecimal(),
                                         objModel.FirstNameEng.ConvertToString(),
                                         objModel.MiddleNameEng.ConvertToString(),
                                         objModel.LastNameEng.ConvertToString(),
                                         objModel.MobileNumber.ConvertToString(),
                                         objModel.DesignationCd.ConvertToString(),
                                         objModel.Remarks.ConvertToString(),
                                         "N",//status
                                         "N",//approved
                                         DBNull.Value,//approved by 
                                         DBNull.Value,//approved dt --type Date
                                         DBNull.Value,//approved date loc
                                         SessionCheck.getSessionUsername(),//entered by
                                         DateTime.Now.ToDateTime(),//entered dt - type date
                                         enteredDateLoc,
                                         SessionCheck.getSessionUsername(),//updated by
                                         DateTime.Now.ToDateTime(),//updated dt - type date
                                         updatedDateLoc ,
                                        objModel.OfficeCd.ToDecimal(),
                                        objModel.HasTab.ConvertToString(),
                                        objModel.TabID.ConvertToString(),
                                         objModel.IsTabFunctional.ConvertToString()
                        );
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
            }
            if (qr != null)
            {
                result = qr.IsSuccess;
            }
            
            return result;
        }


        public bool checkDuplicateEnginers(DeputedEngineersModelClass objModel)
        {
            bool result = false;
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_DEPUTED_ENGINEER_INFO";
                    dt = sf.GetDataTable(true,"PR_CHK_DUPLICATE_ENGINEER",
                                         
                                         objModel.DistrictCd.ToDecimal(),
                                         objModel.VdcMunCd.ToDecimal(),
                                         objModel.Ward.ToDecimal(),
                                         objModel.FirstNameEng.ConvertToString(),
                                         objModel.MiddleNameEng.ConvertToString(),
                                         objModel.LastNameEng.ConvertToString(),
                                         objModel.MobileNumber.ConvertToString(),
                                         objModel.DesignationCd.ConvertToString() , 
                                         DBNull.Value,
                                          objModel.OfficeCd.ToDecimal()
                        );
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
            }
            if(objModel.Mode=="I")
            {
                if (dt.Rows.Count == 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            if (objModel.Mode == "U")
            {
                if (dt.Rows.Count == 0 ||  dt.Rows.Count == 1)
                {
                    result = true;
                } 
                else
                {
                    result = false;
                }
            }
            return result;
        } 

        public DeputedEngineersModelClass getEngineersById(string EngineerId)
        {
            DataTable dt = new DataTable();
            DeputedEngineersModelClass objModel = new DeputedEngineersModelClass();
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();

                try
                {
                    string cmdText = "SELECT * FROM NHRS_ENGINEERS_INFO WHERE ENGINEER_ID='" + EngineerId + "'";
                    dt = sf.GetDataTable(cmdText, null);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
            }
            if(dt!=null && dt.Rows.Count>0)
            {
                objModel.EngineerId = dt.Rows[0]["ENGINEER_ID"].ConvertToString();
                objModel.DistrictCd = dt.Rows[0]["DISTRICT_CD"].ConvertToString();
                objModel.VdcMunCd = dt.Rows[0]["VDC_MUN_CD"].ConvertToString();
                objModel.Ward = dt.Rows[0]["WARD"].ConvertToString();
                objModel.FirstNameEng = dt.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                objModel.MiddleNameEng = dt.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                objModel.LastNameEng = dt.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                objModel.DesignationCd = dt.Rows[0]["DESIGNATION"].ConvertToString();
                objModel.MobileNumber = dt.Rows[0]["MOBILE_NUM"].ConvertToString();
                objModel.Remarks = dt.Rows[0]["REMARKS"].ConvertToString();
                objModel.OfficeCd = dt.Rows[0]["OFFICE_CD"].ConvertToString();

                objModel.HasTab = string.Empty;//dt.Rows[0]["HAS_TAB"].ConvertToString();
                objModel.TabID = string.Empty;// dt.Rows[0]["TAB_ID"].ConvertToString();
                objModel.IsTabFunctional = string.Empty; //dt.Rows[0]["TAB_FUNCTIONAL"].ConvertToString();
            }
            return objModel;
        }

        public DataTable getAllEngineerList( DeputedEngineersModelClass objModel)
        {
            DataTable dt = new DataTable();
             using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();

                try
                {
                    //string cmdText = "SELECT nei.*, "+ Utils.ToggleLanguage("md.desc_eng","md.desc_loc") +" as district ,"
                    //                 + Utils.ToggleLanguage("mvm.desc_eng", "mvm.desc_loc") + " as vdc"
                    //                 + " FROM NHRS_ENGINEERS_INFO nei"
                    //                 + "  INNER join MIS_DISTRICT md On nei.DISTRICT_CD = md.DISTRICT_CD "
                    //                 + " inner join NHRS_NMUNICIPALITY mvm ON nei.VDC_MUN_CD = mvm.NMUNICIPALITY_CD " 
                    //                 + "  WHERE 1=1";
                    sf.PackageName = "PKG_NHRS_DEPUTED_ENGINEER_INFO";
                    dt = sf.GetDataTable(true,"PR_DEPU_ENGI_SEARCH",
                        objModel.FirstNameEng,
                        objModel.MiddleNameEng,
                        objModel.LastNameEng,
                        objModel.DistrictCd.ToDecimal(),
                        objModel.VdcMunCd.ToDecimal(),
                        objModel.Ward.ToDecimal(),
                        objModel.DesignationCd.ToDecimal(),
                        objModel.MobileNumber,
                        objModel.OfficeCd.ToDecimal(),
                        DBNull.Value
                        );
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
            }
            
            return dt;
        }



        public List<SelectListItem> GetInspectionEngineerDesignation(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liiii = new SelectListItem();
            liiii.Text = Utils.GetLabel("---Select Designation---");
            liiii.Value = "";
            if (liiii.Value == selectdYesNo)
                liiii.Selected = true;
            liSelect.Add(liiii);

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("Engineer");
            liii.Value = "1";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Sub Engineer");
            lii.Value = "2";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Assistant Sub Engineer");
            li.Value = "3";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);


            return liSelect;
        }


        public List<SelectListItem> GetInspectionEngineerOffice(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liiii = new SelectListItem();
            liiii.Text = Utils.GetLabel("---Select Office---");
            liiii.Value = "";
            if (liiii.Value == selectdYesNo)
                liiii.Selected = true;
            liSelect.Add(liiii);

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("MOUD/DLPIU");
            liii.Value = "1";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Urban Municipality");
            lii.Value = "2";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Rural Municipality");
            li.Value = "3";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);


            SelectListItem iv = new SelectListItem();
            iv.Text = Utils.GetLabel("Ward ");
            iv.Value = "4";
            if (iv.Value == selectdYesNo)
                iv.Selected = true;
            liSelect.Add(iv);


            return liSelect;
        }


    }
}
