using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Services.CaseGrievance
{
    public class CaseGrievanceHandlingDropdownService
    {
        public List<SelectListItem> GetDamageGrade(string selectedValue)
        {
            List<SelectListItem> listDamageGrade = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DAMAGE_GRADE T1 where T1.DISABLED='N' order by DAMAGE_GRADE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listDamageGrade.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Grade") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DAMAGE_GRADE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listDamageGrade.Add(li);
                        if ((dr["DAMAGE_GRADE_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return listDamageGrade;
        }
        public List<SelectListItem> GetPhotoGrade(string selectedValue)
        {
            List<SelectListItem> listDamageGrade = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DAMAGE_GRADE T1 where T1.DISABLED='N' order by DAMAGE_GRADE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listDamageGrade.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Grade") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DAMAGE_GRADE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listDamageGrade.Add(li);
                        if ((dr["DAMAGE_GRADE_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return listDamageGrade;
        }
        public List<SelectListItem> GetaftGrievGrade(string selectedValue)
        {
            List<SelectListItem> listDamageGrade = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DAMAGE_GRADE T1 where T1.DISABLED='N' order by DAMAGE_GRADE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listDamageGrade.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Grade") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DAMAGE_GRADE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listDamageGrade.Add(li);
                        if ((dr["DAMAGE_GRADE_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return listDamageGrade;
        }
        public List<SelectListItem> GetTechnicalSolution(string selectedValue)
        {
            List<SelectListItem> listHousePosition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_TECHNICAL_SOLUTION T1 where T1.DISABLED='N' order by TECHSOLUTION_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHousePosition.Add(li1);
                    listHousePosition.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Technical Solution") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["TECHSOLUTION_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHousePosition.Add(li);
                        if ((dr["TECHSOLUTION_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return listHousePosition;
        }
        public JsonResult GetYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("SurveyNo");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("SurveyYes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
