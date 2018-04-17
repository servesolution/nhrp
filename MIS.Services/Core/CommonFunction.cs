using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Web.Routing;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Configuration;
using System.Globalization;
using MIS.Models.Core;
using EntityFramework;
using ExceptionHandler;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MIS.Services.Core;
using MIS.Services.AuditTrail;
using MIS.Services.Setup;
using MIS.Models.Setup;
using MIS.Models.Security;
using System.Data.OracleClient;
using MIS.Services.Security;
using System.Data.SqlClient;

namespace MIS.Services.Core
{
    public class CommonFunction
    {
        CommonService commonService = null;
        RegionStateService regStateService = null;
        //Constructor
        public CommonFunction()
        {
            commonService = new CommonService();
            regStateService = new RegionStateService();
        }

        public List<SelectListItem> GetExperienceYears(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstYrs = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstYrs.Add(new SelectListItem { Value = "", Text = "Exp. Yrs" });
            }
            for (int i = 1; i <= 50; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Value = i.ConvertToString();
                item.Text = Utils.GetLabel(i.ConvertToString());
                if (item.Value == selectedValue)
                    item.Selected = true;
                selLstYrs.Add(item);
            }
            selLstYrs.Add(new SelectListItem { Value = "", Text = "50+" });
            return selLstYrs;
        }
        public List<SelectListItem> GetTrainingLevel(string selectedValue, bool isPopup = false)
        {
            List<SelectListItem> selLstTrainLevel = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainingLevelList();
            if (isPopup == false)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Training Level", "तालिम तह छान्नुहोस्") });
            }
            foreach (MISCommon com in lstCommon)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = com.Code, Text = com.Description });
            }
            foreach (SelectListItem item in selLstTrainLevel)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTrainLevel;
        }

        /// <summary>
        /// this function been made for training type
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <param name="isPopup"></param>
        /// <returns></returns>
        public List<SelectListItem> GetTrainingType(string selectedValue, bool isPopup = false)
        {
            List<SelectListItem> selLstTrainLevel = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainingTypeList();
            if (isPopup == false)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = " ", Text = "---" + Utils.ToggleLanguage("Select Training Type", "तालिम तह छान्नुहोस्") + "---" });
                
            }
            var index = lstCommon.FindIndex(x => x.DefinedCode == "18");
            var item = lstCommon[index];
            lstCommon[index] = lstCommon[lstCommon.Count()-1];
            lstCommon[lstCommon.Count() - 1] = item;

            foreach (MISCommon com in lstCommon)
            {
             selLstTrainLevel.Add(new SelectListItem { Value = com.Code, Text = com.Description });
             
            }
            //selLstTrainLevel.Add(new SelectListItem { Value = "-1", Text = "Other (Please specify in comment)" });
            foreach (SelectListItem items in selLstTrainLevel)
            {
                if (items.Value == selectedValue)
                    items.Selected = true;
            }
            return selLstTrainLevel;
        }

        public List<SelectListItem> getTrainingOrganizer(string selectedValue, bool isPopup = false)
        {
            List<SelectListItem> selLstTrainLevel = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainingOrganizerList();
            if (isPopup == false)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = " ", Text = "---" + Utils.ToggleLanguage("Select Training Organizer", "तालिम तह छान्नुहोस्") + "---" });

            }
            var index = lstCommon.FindIndex(x => x.DefinedCode == "4");
            var item = lstCommon[index];
            lstCommon[index] = lstCommon[lstCommon.Count() - 1];
            lstCommon[lstCommon.Count() - 1] = item;

            foreach (MISCommon com in lstCommon)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = com.Code, Text = com.Description });

            }
            //selLstTrainLevel.Add(new SelectListItem { Value = "-1", Text = "Other (Please specify in comment)" });
            foreach (SelectListItem items in selLstTrainLevel)
            {
                if (items.Value == selectedValue)
                    items.Selected = true;
            }
            return selLstTrainLevel;
        }

        //traning batch name
        public List<SelectListItem> GetBatchName(string selectedValue, bool isPopup = false)
        {
            List<SelectListItem> selLstTrainLevel = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainingBatchList();
            if (isPopup == false)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Training Batch", "तालिम ब्याच छान्नुहोस्") + "---" });
            }
            foreach (MISCommon com in lstCommon)
            {
                selLstTrainLevel.Add(new SelectListItem { Value = com.Code, Text = com.Description });
            }
            foreach (SelectListItem item in selLstTrainLevel)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTrainLevel;
        }


        public List<SelectListItem> GetCertificate(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstCertificate = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetCertificate();
            if (ispopup == false)
            {
                selLstCertificate.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Certificate", "प्रमाणपत्र छान्नुहोस्") + "---" });
            }
            foreach (MISCommon com in lstCommon)
            {
                selLstCertificate.Add(new SelectListItem { Value = com.DefinedCode, Text = com.Description });
            }
            foreach (SelectListItem item in selLstCertificate)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstCertificate;
        }


        public List<SelectListItem> GetExperienceNumber(string selectedValue)
        {
            int i;
            List<SelectListItem> selLstCertificate = new List<SelectListItem>();
            //List<MISCommon> lstCommon = commonService.GetCertificate();
            //if (ispopup == false)
            //{
            selLstCertificate.Add(new SelectListItem { Value = "", Text = "--Select Experience--" });
            // }
            for (i = 1; i <= 30; i++)
            {
                selLstCertificate.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            foreach (SelectListItem item in selLstCertificate)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstCertificate;
        }

        public List<SelectListItem> getTrainer(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTrainer = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTrainer(code, desc);
            selLstTrainer.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Trainer", "प्रशिक्षक छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstTrainer.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstTrainer)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return selLstTrainer;
        }


        public List<SelectListItem> getTrainerByBatchFeedback(string selectedValue, string batch, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTrainer = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainerByBatchFeedback(code, desc, batch);
            selLstTrainer.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Trainer", "प्रशिक्षक छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstTrainer.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstTrainer)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return selLstTrainer;
        }
        public List<SelectListItem> getParticipantsByBatchFeedback(string selectedValue, string batch, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTrainer = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getParticipantsByBatchFeedback(code, desc, batch);
            selLstTrainer.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Trainer", "प्रशिक्षक छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstTrainer.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstTrainer)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return selLstTrainer;
        }
        public JsonResult GetTrainerByBatch(string id)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTrainerListByBatch("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetParticipantsByBatch(string id)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetParticipantsListByBatch("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public List<SelectListItem> GetOrganization(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstOrg = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetOrganization();
            if (ispopup == false)
            {
                selLstOrg.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Organization", "संगठन छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstOrg.Add(new SelectListItem { Value = common.DefinedCode, Text = common.Description });
            }
            foreach (SelectListItem item in selLstOrg)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstOrg;
        }
        public List<SelectListItem> GetProfession(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstProf = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetProfession();
            if (ispopup == false)
            {
                selLstProf.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Profession", "पेशा छान्नुहोस्") + "---" });
            }
            foreach (MISCommon com in lstCommon)
            {
                selLstProf.Add(new SelectListItem { Value = com.DefinedCode, Text = com.Description });
            }
            foreach (SelectListItem item in selLstProf)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstProf;
        }
        public static string GetHouseholdRuleFlag(string householdid)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "select RULE_FLAG from MIS_HOUSEHOLD_INFO where HOUSEHOLD_ID='" + householdid + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["RULE_FLAG"].ToString();
                }

            }
            return result;
        }
        public static string GetMLDDistrictCode(string DistrictCD)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "select MLD_DISTRICT_CD from MLD_KLL_DISTRICT_MAP where KLL_DISTRICT_CD='" + DistrictCD + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["MLD_DISTRICT_CD"].ToString();
                }

            }
            return result;
        }

        //Trainingtype Name
        public string GetTrainingTypeName(string code, string tableName, string ColumanName)
        {
            string result = "";
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DESC_ENG from " + tableName + " WHERE " + ColumanName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["DESC_ENG"].ToString();
                    }
                }
            }
            return result;
        }
        public static string GetMLDVDCCode(string VDCCD, string District)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "select MLD_VDC_CD from MLD_KLL_VDC_MAP where KLL_VDC_CD='" + VDCCD + "' and KLL_DISTRICT_CD='" + District + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["MLD_VDC_CD"].ToString();
                }

            }
            return result;
        }
        public List<SelectListItem> GetDistrictsByDistrictCode(string selectedValue, bool ispopup = false, bool isDistrictUser = true)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21" && isDistrictUser)
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                if (ispopup == false)
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd || d.Value == "")).ToList();
                    selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
                }
                else
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
                }
            }
            else
            {

                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
            }
            string District = GetDescriptionFromCode(selectedValue, "MIS_DISTRICT", "DISTRICT_CD");
            selLstDistricts.RemoveAll(i => i.Text != District);
            return selLstDistricts;
        }
        public List<SelectListItem> GetBankByBankCode(string selectedValue, bool ispopup = false, bool isbankUser = true)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string Usrcd = HttpContext.Current.Session[""].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);
            List<SelectListItem> selLstBank = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstBank.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });
            }
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21" && isbankUser)
            {

                selLstBank.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                if (ispopup == false)
                {
                    selLstBank = (selLstBank.Where(d => d.Value == DistDefCd || d.Value == "")).ToList();
                    selLstBank = GetSelectedList(selLstBank, selectedValue).ToList();
                }
                else
                {
                    selLstBank = (selLstBank.Where(d => d.Value == DistDefCd)).ToList();
                }
            }
            else
            {

                selLstBank.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstBank = GetSelectedList(selLstBank, selectedValue).ToList();
            }
            string District = GetDescriptionFromCode(selectedValue, "MIS_DISTRICT", "DISTRICT_CD");
            selLstBank.RemoveAll(i => i.Text != District);
            return selLstBank;
        }
        public static string GetDistrictByEmployeeCode(string employeeCode)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select PER_DISTRICT_CD from COM_EMPLOYEE where EMPLOYEE_CD='" + employeeCode + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["PER_DISTRICT_CD"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    result = "";
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }

            }
            return result;
        }
        public static string GetVDCByEmployeeCode(string employeeCode)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select PER_VDC_MUN_CD from COM_EMPLOYEE where EMPLOYEE_CD='" + employeeCode + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["PER_VDC_MUN_CD"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    result = "";
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }

            }
            return result;
        }
        public static string GetBankByUserCode(string userCode)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select BANK_CD from COM_WEB_USR where USR_CD='" + userCode + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["BANK_CD"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    result = "";
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }

            }
            return result;
        }
        public List<SelectListItem> getBankbyUser(string bankcd)
        {
            List<SelectListItem> ListBankbyUser = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select BANK_CD,DESC_ENG,DESC_LOC from NHRS_BANK where BANK_CD='" + bankcd + "' ORDER BY DESC_ENG DESC";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListBankbyUser.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["BANK_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListBankbyUser.Add(li);
                        if ((dr["BANK_CD"].ConvertToString()) == bankcd)
                        {
                            li.Selected = false;
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
            return ListBankbyUser;
        }
        public List<SelectListItem> getDonorList(string donor_cd = "")
        {
            List<SelectListItem> donorList = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DONOR_DTL";
                if (donor_cd != "")
                {
                    cmdText = "SELECT * FROM NHRS_DONOR_DTL WHERE DONOR_CD= " + donor_cd;
                }

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    donorList.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Donor") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DONOR_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_NEP"].ConvertToString());
                        donorList.Add(li);

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
            return donorList;
        }
        public List<SelectListItem> GetDonorSpecificPkg(string donor_cd)
        {
            List<SelectListItem> pkgList = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select ns.DESC_ENG,nm.pACKAGE_CD,ns.DESC_NEP from nhrs_donor_pkg_mapping nm left join nhrs_donor_pkg_support ns on nm.package_cd = ns.pkg_cd WHERE NM.dONOR_cD = " + donor_cd;

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["PACKAGE_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_NEP"].ConvertToString());
                        pkgList.Add(li);

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
            return pkgList;
        }
        public List<SelectListItem> GetDonorPkgSupportList()
        {
            List<SelectListItem> pkgList = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DONOR_PKG_SUPPORT";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    pkgList.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Support Package") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["PKG_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_NEP"].ConvertToString());
                        pkgList.Add(li);

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
            return pkgList;
        }
        public static string GetDistrictByOfficeCode(string employeeCode)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select com_fn_return_desc('mis_office','office_cd','district_cd',office_cd) PER_DISTRICT_CD  from  COM_EMPLOYEE  where EMPLOYEE_CD='" + employeeCode + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0]["PER_DISTRICT_CD"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    result = "";
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }

            }
            return result;
        }
        public static string GetAge(string pDate)
        {
            DataTable dtbl = null;
            string retDate = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    string cmdText = String.Format("select com_pkg_date.com_fn_get_age('" + Convert.ToDateTime(pDate).ToString("dd-MMM-yyyy") + "','" + DateTime.Now.ToString("dd-MMM-yyyy") + "','Y') as age from dual");
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    retDate = dtbl.Rows[0]["age"].ToString();

                }
            }
            catch (Exception ex)
            {
                retDate = "";
                ExceptionManager.AppendLog(ex);
            }
            return retDate;
        }
        public static string CheclDuplicateCtzNumber(string ctzNumber, string ctzDistrictCd, string ctzIssueDate)
        {
            DataTable dtbl = null;
            string DuplicateBool = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    string cmdText = String.Format("select pkg_member.FN_CHECK_CTZ_NO('" + ctzNumber + "','" + ctzDistrictCd + "','" + ctzIssueDate + "') as DuplicateBool from dual");
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    DuplicateBool = dtbl.Rows[0]["DuplicateBool"].ToString();

                }
            }
            catch (Exception ex)
            {
                DuplicateBool = "";
                ExceptionManager.AppendLog(ex);
            }
            return DuplicateBool;
        }
        public static bool PMTCalculation(string distcode, string householdid, string sessionid)
        {
            bool cal = false;
            QueryResult qr = new QueryResult();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    try
                    {
                        service.Begin();
                        qr = service.SubmitChanges("PKG_PROVERTY_RULE_CALC.PR_PMT_CALC", householdid, distcode, sessionid);
                        if (qr.IsSuccess)
                        {
                            cal = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
            }
            catch (Exception ex)
            {
                cal = false;
                ExceptionManager.AppendLog(ex);
            }
            return cal;
            //return qr["P_SESSION_ID"].ToString();
        }
        public static bool MPICalculation(string distcode, string householdid, string sessionid)
        {
            bool cal = false;
            QueryResult qr = new QueryResult();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    try
                    {
                        service.Begin();
                        qr = service.SubmitChanges("PKG_PROVERTY_RULE_CALC.PR_MPI_CALC", householdid, distcode, sessionid);
                        if (qr.IsSuccess)
                        {
                            cal = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
            }
            catch (Exception ex)
            {
                cal = false;
                ExceptionManager.AppendLog(ex);
            }
            return cal;
        }
        public static bool MPIPMT_CalcApprove(string sessionid, string householdid, string rulflag, string districtCd)
        {
            bool cal = false;
            QueryResult qr = new QueryResult();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    try
                    {
                        service.Begin();
                        qr = service.SubmitChanges("PKG_PROVERTY_RULE_CALC.PR_PMT_MPI_CALC_APPROVE", sessionid, householdid, rulflag, districtCd);
                        if (qr.IsSuccess)
                        {
                            cal = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
            }
            catch (Exception ex)
            {
                cal = false;
                ExceptionManager.AppendLog(ex);
            }
            return cal;
        }
        public static string GetRecentFiscalYear(string englishDate)
        {
            string nepaliDate = NepaliDate.getNepaliDate(englishDate);
            DataTable dtbl = null;
            string retDate = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    string cmdText = String.Format("select com_pkg_date.fn_getnepalifiscalyear('" + nepaliDate + "') as fiscalyear from dual");
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        service.End();
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    retDate = dtbl.Rows[0]["fiscalyear"].ToString();

                }
            }
            catch (Exception ex)
            {
                retDate = "";
                ExceptionManager.AppendLog(ex);
            }
            return retDate;
        }

        #region DropDownList Fills
        public List<SelectListItem> GetDistricts(string selectedValue, bool ispopup = false, bool isDistrictUser = true)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21" && isDistrictUser)
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                if (ispopup == false)
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd || d.Value == "")).ToList();
                    selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
                }
                else
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
                }
            }
            else
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
            }



            return selLstDistricts;
        }
        public List<SelectListItem> GetDistrictsinEnglish(string selectedValue, bool ispopup = false, bool isDistrictUser = true)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string DistDefCd = GetData.GetDefinedCodeFor(DataType.AllDistrict, DistCd);
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21" && isDistrictUser)
            {
                selLstDistricts.AddRange(GetData.Districts("English").ToList());
                if (ispopup == false)
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd || d.Value == "")).ToList();
                    selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
                }
                else
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
                }
            }
            else
            {
                selLstDistricts.AddRange(GetData.AllDistricts("english").ToList());
                selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
            }



            return selLstDistricts;
        }
        public List<SelectListItem> GetAllDistricts(string selectedValue, bool ispopup = false, bool isDistrictUser = true)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21" && isDistrictUser)
            {
                selLstDistricts.AddRange(GetData.Districts(Utils.ToggleLanguage("english", "nepali")).ToList());
                if (ispopup == false)
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd || d.Value == "")).ToList();
                    selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
                }
                else
                {
                    selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
                }
            }
            else
            {
                selLstDistricts.AddRange(GetData.Districts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();
            }



            return selLstDistricts;
        }
        private List<SelectListItem> GetSelectedList(List<SelectListItem> list, string selectedValue)
        {
            var j = list.Where(x => x.Selected);
            if (selectedValue != null && selectedValue != "" && selectedValue != "0")
                ((SelectListItem)(list.Where(s => s.Value == selectedValue).First())).Selected = true;
            return list;
        }
        public List<SelectListItem> GetZonebyRegion(string selectedValue, string RegionCode, bool ispopup = false)
        {
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();
            if (!string.IsNullOrEmpty(RegionCode))
            {
                lstCommon = commonService.GetZonebyRegionCode("", "", RegionCode);
            }
            if (ispopup == false)
            {
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Zone") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstZone.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLstZone)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstZone;
        }
        public List<SelectListItem> GetDistrictsbyZone(string selectedValue, string ZoneCode, bool ispopup = false)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();
            if (!string.IsNullOrEmpty(ZoneCode))
            {
                lstCommon = commonService.GetDistrictbyCodeandDescForZoneCode("", "", ZoneCode);
            }
            if (ispopup == false)
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstDistricts.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLstDistricts)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDistricts;
        }
        public List<SelectListItem> GetVDCMunByDistrict(string selectedValue, string DistDefCd, bool ispopup = false)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string VdcMunCd = HttpContext.Current.Session["UserVdcMunDefCd"].ConvertToString();
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);

            }
            string VdcMunDefCd = GetData.GetDefinedCodeFor(DataType.VdcMun, VdcMunCd);
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false)
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });


            if (!string.IsNullOrWhiteSpace(DistDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.District, DistDefCd), Utils.ToggleLanguage("english", "nepali")));
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                if (!string.IsNullOrWhiteSpace(VdcMunDefCd))
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == VdcMunDefCd).ToList();
            }
            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            string VDCCode = GetCodeFromDataBase(selectedValue, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            string VDC = GetDescriptionFromCode(VDCCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            selLstVDCMun.RemoveAll(i => i.Text != VDC);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetVDCMunByAllDistrict(string selectedValue, string DistDefCd, bool ispopup = false)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string VdcMunCd = HttpContext.Current.Session["UserVdcMunDefCd"].ConvertToString();
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                DistDefCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);

            }
            string VdcMunDefCd = GetData.GetDefinedCodeFor(DataType.VdcMun, VdcMunCd);
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false)
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });


            if (!string.IsNullOrWhiteSpace(DistDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.AllDistrict, DistDefCd), Utils.ToggleLanguage("english", "nepali")));
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                if (!string.IsNullOrWhiteSpace(VdcMunDefCd))
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == VdcMunDefCd).ToList();
            }
            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetVDCMunByDistrictCode(string selectedValue, string DistrictCd, bool ispopup = false)
        {
            string liveGrpCd = HttpContext.Current.Session["liveGrpCd"].ConvertToString();
            string DistCd = HttpContext.Current.Session["UserDistrictCd"].ConvertToString();
            string VdcMunCd = HttpContext.Current.Session["UserVdcMunDefCd"].ConvertToString();
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                DistrictCd = GetData.GetDefinedCodeFor(DataType.District, DistCd);

            }
            else
            {

                DistrictCd = GetData.GetDefinedCodeFor(DataType.District, DistrictCd);
            }
            selectedValue = GetData.GetDefinedCodeFor(DataType.VdcMun, selectedValue);
            string VdcMunDefCd = GetData.GetDefinedCodeFor(DataType.VdcMun, VdcMunCd);
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false)
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });


            if (!string.IsNullOrWhiteSpace(DistrictCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(DistrictCd, Utils.ToggleLanguage("english", "nepali")));
            //grp
            if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            {
                if (!string.IsNullOrWhiteSpace(VdcMunDefCd))
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == VdcMunDefCd).ToList();
            }
            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetBatchType(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("Pending") });
            selBatchType.Add(new SelectListItem { Value = "Y", Text = Utils.GetLabel("Processed") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetWardByVDCMun(string selectedValue, string vdcDefCd, bool ispopup = false)
        {

            List<SelectListItem> selLstWard = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstWard.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Ward No.") + "---" });
            }
            for (int i = 1; i <= 35; i++)
            {
                SelectListItem li = new SelectListItem();
                li.Value = i.ConvertToString();
                li.Text = Utils.GetLabel(i.ConvertToString());
                if (li.Value == selectedValue)
                    li.Selected = true;
                selLstWard.Add(li);
            }

            return selLstWard;
        }
        public List<SelectListItem> GetCasteByCasteGrp(string selectedValue, string casteGrpDefCd, bool ispopup = false)
        {
            // if (!string.IsNullOrEmpty(liveGrpCd) && liveGrpCd == "21")
            //{
            //    casteGrpDefCd = GetData.GetDefinedCodeFor(DataType.CasteGroup, DistCd);

            //}
            string casteGrpCode = GetData.GetCodeFor(DataType.CasteGroup, casteGrpDefCd);
            List<SelectListItem> selLstCaste = new List<SelectListItem>();
            if (ispopup == false)
                selLstCaste.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Caste") + "---" });


            if (!string.IsNullOrWhiteSpace(casteGrpCode))
                selLstCaste.AddRange(GetData.CasteByCasteGrp(casteGrpCode, Utils.ToggleLanguage("english", "nepali")));


            //grp

            selLstCaste = GetData.GetSelectedList(selLstCaste, selectedValue);
            return selLstCaste;
        }
        public List<SelectListItem> GetHouseHoldByDistZoneVDCWard(string selectedValue, string regionid, string zoneid, string districtid, string vdcid, string ward, string area)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetHouseHoldbyZoneDistrictVDCWard("", "", regionid, zoneid, districtid, vdcid, ward, area);
            foreach (MISCommon common in lstCommon)
            {
                selLstDistricts.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLstDistricts)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDistricts;
        }
        public List<SelectListItem> GetRegionState(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstRegionState = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstRegionState.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Region") + "---" });
            }
            selLstRegionState = GetData.AllRegions(Utils.ToggleLanguage("english", "nepali"));
            selLstRegionState = GetData.GetSelectedList(selLstRegionState, selectedValue);


            // List<MISCommon> lstCommon = commonService.GetZonebyCodeandDesc("", "");
            //foreach (MISCommon common in lstCommon)
            //{
            //    selLstZone.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            //}
            //foreach (SelectListItem item in selLstZone)
            //{
            //    if (item.Value == selectedValue)
            //        item.Selected = true;
            //}
            return selLstRegionState;
        }
        public List<SelectListItem> GetZone(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetZonebyCodeandDesc("", "");
            if (ispopup == false)
            {
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Zone") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstZone.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLstZone)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstZone;
        }
        public List<SelectListItem> GetVDCMun(string selectedValue)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            List<SelectListItem> lstVdcs = new List<SelectListItem>();
            foreach (SelectListItem sliDistrict in GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")))
            {
                try
                {
                    if (sliDistrict.Value != "99")
                    {
                        lstVdcs = GetData.VdcByDistrict(GetData.GetCodeFor(DataType.District, sliDistrict.Value), Utils.ToggleLanguage("english", "nepali"));

                        foreach (SelectListItem sli in lstVdcs)
                            lst.Add(new SelectListItem { Value = sli.Value, Text = sli.Text });
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);

                }
            }
            lst = GetData.GetSelectedList(lst, selectedValue);
            return lst;
        }
        public List<SelectListItem> GetDocumentType(string selectedValue)
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetDocumentTypebyCodeandDesc("", "");
            selLstDocument.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Document Type") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstDocument.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetTableNameForEligibilityRule(string selectedValue)
        {
            List<SelectListItem> selLstTable = new List<SelectListItem>();
            selLstTable.Add(new SelectListItem { Value = Convert.ToString(""), Text = "---" + Utils.GetLabel("Select Table Name") + "---" });
            selLstTable.Add(new SelectListItem { Value = Convert.ToString("NHRS_HOUSE_OWNER_MST"), Text = Utils.GetLabel("NHRS_HOUSE_OWNER_MST") });
            selLstTable.Add(new SelectListItem { Value = Convert.ToString("NHRS_HOWNER_OTH_RESIDENCE_DTL"), Text = Utils.GetLabel("NHRS_HOWNER_OTH_RESIDENCE_DTL") });
            selLstTable.Add(new SelectListItem { Value = Convert.ToString("NHRS_BUILDING_ASSESSMENT_MST"), Text = Utils.GetLabel("NHRS_BUILDING_ASSESSMENT_MST") });
            foreach (SelectListItem item in selLstTable)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTable;
        }
        public List<SelectListItem> GetBusinessRule(string selectedValue)
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            selLstDocument.Add(new SelectListItem { Value = Convert.ToString(""), Text = "---" + Utils.GetLabel("Select Business Rule") + "---" });
            selLstDocument.Add(new SelectListItem { Value = Convert.ToString("PMT"), Text = Utils.ToggleLanguage("PMT", "पी यम टी") });
            selLstDocument.Add(new SelectListItem { Value = Convert.ToString("MPI"), Text = Utils.ToggleLanguage("MPI", "एम पी आई") });
            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetAllOperator(string selectedValue)
        {
            List<SelectListItem> selOperator = new List<SelectListItem>();
            selOperator.Add(new SelectListItem { Value = Convert.ToString(""), Text = "---" + Utils.GetLabel("Select Operator Type") + "---" });
            selOperator.Add(new SelectListItem { Value = Convert.ToString("="), Text = Utils.GetLabel("Equal To") });
            selOperator.Add(new SelectListItem { Value = Convert.ToString(">"), Text = Utils.GetLabel("Greater Than") });
            selOperator.Add(new SelectListItem { Value = Convert.ToString("<"), Text = Utils.GetLabel("Less Than") });
            selOperator.Add(new SelectListItem { Value = Convert.ToString("<="), Text = Utils.GetLabel("Less Than Equal To") });
            selOperator.Add(new SelectListItem { Value = Convert.ToString(">="), Text = Utils.GetLabel("Greater Than Equal To") });
            foreach (SelectListItem item in selOperator)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selOperator;
        }
        public List<SelectListItem> GetEligibilityRule(string selectedValue)
        {
            List<SelectListItem> ListEligibilityRule = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ELIGIBILITY_RULE  order by RULE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListEligibilityRule.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Eligibility Rule") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.GetLabel(dr["RULE_NAME"].ConvertToString());
                        ListEligibilityRule.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
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
            return ListEligibilityRule;
        }
        public List<SelectListItem> Trageted(string selectedValue)
        {

            List<SelectListItem> lstTargeted = new List<SelectListItem>();
            SelectListItem selLst = new SelectListItem();
            selLst.Text = Utils.GetLabel("Untargeted");
            selLst.Value = "N";
            if (selLst.Value == selectedValue)
                selLst.Selected = true;
            lstTargeted.Add(selLst);

            SelectListItem selLst1 = new SelectListItem();
            selLst1.Text = Utils.GetLabel("Targeted");
            selLst1.Value = "Y";
            if (selLst1.Value == selectedValue)
                selLst1.Selected = true;
            lstTargeted.Add(selLst1);

            return lstTargeted;
        }
        public List<SelectListItem> GetOperation(string selectedValue)
        {
            List<SelectListItem> selOperation = new List<SelectListItem>();
            selOperation.Add(new SelectListItem { Value = "DELETE", Text = Utils.GetLabel("Delete") });
            selOperation.Add(new SelectListItem { Value = "UPDATE", Text = Utils.GetLabel("Update") });
            foreach (SelectListItem item in selOperation)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selOperation;
        }
        public List<SelectListItem> GetTableType(string selectedValue)
        {
            List<SelectListItem> selTableType = new List<SelectListItem>();
            selTableType.Add(new SelectListItem { Value = "AUDIT_TRAIL_COM", Text = Utils.GetLabel("Common") });
            selTableType.Add(new SelectListItem { Value = "AUDIT_TRAIL_TRANS", Text = Utils.GetLabel("Transaction") });
            foreach (SelectListItem item in selTableType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selTableType;
        }
        public List<SelectListItem> GetTargetType(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("Non-Targeting") });
            selBatchType.Add(new SelectListItem { Value = "Y", Text = Utils.GetLabel("Targeting") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetTargetType1(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("New Applicant") });
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("Non-Eligible") });
            selBatchType.Add(new SelectListItem { Value = "Y", Text = Utils.GetLabel("Eligible") });

            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetTargetType2(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("New Applicant") });
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("Processed") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetTargetType3(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("New Applicant") });
            selBatchType.Add(new SelectListItem { Value = "P", Text = Utils.GetLabel("Processed") });
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("Approved") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetTargetType4(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("New Applicant") });
            selBatchType.Add(new SelectListItem { Value = "T", Text = Utils.GetLabel("Targeted") });
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("Approved") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetMobileNo(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("Please Select") });
            selBatchType.Add(new SelectListItem { Value = "Y", Text = Utils.GetLabel("Yes") });
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("No") });

            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetBankAccountNo(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "A", Text = Utils.GetLabel("All") });
            selBatchType.Add(new SelectListItem { Value = "Y", Text = Utils.GetLabel("Yes") });
            selBatchType.Add(new SelectListItem { Value = "N", Text = Utils.GetLabel("No") });

            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetExcelType(string selectedValue)
        {
            List<SelectListItem> selBatchType = new List<SelectListItem>();
            selBatchType.Add(new SelectListItem { Value = "Master", Text = Utils.GetLabel("Master") });
            selBatchType.Add(new SelectListItem { Value = "User", Text = Utils.GetLabel("User") });
            foreach (SelectListItem item in selBatchType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selBatchType;
        }
        public List<SelectListItem> GetTableName(string selectedValue, string TableType)
        {
            List<SelectListItem> selTableName = new List<SelectListItem>();
            DataTable dt = null;
            AuditTrailService objService = new AuditTrailService();
            dt = objService.GetAuditTrailTable(TableType);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    selTableName.Add(new SelectListItem { Value = dr["USER_TABLE_DESC"].ToString(), Text = dr["USER_TABLE_DESC"].ToString() });
                }

            }
            foreach (SelectListItem item in selTableName)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selTableName;
        }
        public List<SelectListItem> GetRuleCalculated(string selectedValue)
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            //selLstDocument.Add(new SelectListItem { Value = Convert.ToString("All"), Text = Utils.ToggleLanguage("All", "सबै") });
            selLstDocument.Add(new SelectListItem { Value = Convert.ToString("Not Calculated"), Text = Utils.ToggleLanguage("Not Calculated", "नगरिएको") });
            selLstDocument.Add(new SelectListItem { Value = Convert.ToString("Calculated"), Text = Utils.ToggleLanguage("Calculated", "गरिएको") });

            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetOrderValue(string selectedValue)
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            selLstDocument.Add(new SelectListItem { Value = "ASC", Text = Utils.ToggleLanguage("Ascending", "अस्सेन्डिंग") });
            selLstDocument.Add(new SelectListItem { Value = "DESC", Text = Utils.ToggleLanguage("Descending", "डिसेन्डिंग") });

            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetGender(string selectedValue)
        {
            List<SelectListItem> selLstGender = new List<SelectListItem>();
            selLstGender.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Gender") + "---" });
            selLstGender.AddRange(GetData.AllGenders(Utils.ToggleLanguage("english", "nepali")));
            selLstGender = GetData.GetSelectedList(selLstGender, selectedValue);
            return selLstGender;
        }
        public List<SelectListItem> GetMaritalStatus(string selectedValue)
        {
            List<SelectListItem> selLstMarried = new List<SelectListItem>();
            selLstMarried.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Marital Status") + "---" });
            selLstMarried.AddRange(GetData.AllMaritalStatus(Utils.ToggleLanguage("english", "nepali")));
            selLstMarried = GetData.GetSelectedList(selLstMarried, selectedValue);
            return selLstMarried;
        }
        public List<SelectListItem> GetCaste(string selectedValue)
        {
            List<SelectListItem> selLstCaste = new List<SelectListItem>();
            selLstCaste.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Caste") + "---" });
            selLstCaste.AddRange(GetData.AllCastes(Utils.ToggleLanguage("english", "nepali")));
            selLstCaste = GetData.GetSelectedList(selLstCaste, selectedValue);
            return selLstCaste;
        }
        public List<SelectListItem> GetCasteGroup(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstCasteGroup = new List<SelectListItem>();
            if (ispopup == false)
            {
                selLstCasteGroup.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Caste Group") + "---" });
            }
            selLstCasteGroup.AddRange(GetData.AllCasteGroups(Utils.ToggleLanguage("english", "nepali")));
            selLstCasteGroup = GetData.GetSelectedList(selLstCasteGroup, selectedValue);
            return selLstCasteGroup;
        }
        public List<SelectListItem> GetReligion(string selectedValue)
        {
            List<SelectListItem> selLstReligion = new List<SelectListItem>();
            selLstReligion.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Religion") + "---" });
            selLstReligion.AddRange(GetData.AllReligions(Utils.ToggleLanguage("english", "nepali")));
            selLstReligion = GetData.GetSelectedList(selLstReligion, selectedValue);
            return selLstReligion;
        }
        public List<SelectListItem> GetEducation(string selectedValue)
        {
            List<SelectListItem> selLstEducation = new List<SelectListItem>();
            selLstEducation.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Education") + "---" });
            selLstEducation.AddRange(GetData.AllEducations(Utils.ToggleLanguage("english", "nepali")));
            selLstEducation = GetData.GetSelectedList(selLstEducation, selectedValue);
            return selLstEducation;
        }
        public List<SelectListItem> getReportLayerHH(string id, string regionid, string districtid, string zoneid, string vdcid, string ward, string area)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();
            if (id != null)
            {
                if (id == "Permanent District" || id == "Temporary District" || id == "Citizen District" || id == "Voter District" || id == "NID District" || id == "District")
                {
                    if (zoneid != "" && zoneid != null)
                    {
                        listLayer = GetDistrictsbyZone("", zoneid);
                    }
                    else
                    {
                        listLayer = GetDistrictsbyZone("", "");
                    }
                }
                if (id == "VDC/Municipality")
                {
                    if (districtid != "" && districtid != null)
                    {
                        listLayer = GetVDCMunByDistrict("", districtid, true);
                    }
                }
                if (id == "Ward")
                {
                    if (vdcid != "" && vdcid != null)
                    {
                        listLayer = GetWardByVDCMun("", vdcid, true);
                    }
                }
                if (id == "Household")
                {
                    listLayer = GetHouseHoldByDistZoneVDCWard("", regionid, zoneid, districtid, vdcid, ward, area);
                }
            }
            return listLayer;
        }
        public List<SelectListItem> getcaseGrievanceType(string selectedValue)
        {
            List<SelectListItem> listCaseGrievanceType = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_CASE_GRIEVIENCE_TYPE T1 order by CASE_GRIEVIENCE_TYPE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    listCaseGrievanceType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Case Grievance Type") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["CASE_GRIEVIENCE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listCaseGrievanceType.Add(li);
                        if ((dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString()) == selectedValue)
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
            return listCaseGrievanceType;
        }
        public List<SelectListItem> getDocumentTYpe(string selectedValue)
        {
            List<SelectListItem> lstDocumentType = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_GRIEVANCE_DOC_TYPE T1 order by DOC_TYPE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    lstDocumentType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Document Type") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        lstDocumentType.Add(li);
                        if ((dr["DOC_TYPE_CD"].ConvertToString()) == selectedValue)
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
            return lstDocumentType;
        }
        public List<SelectListItem> GetAddressedOfficeForCaseGrievance(string selectedValue)
        {
            List<SelectListItem> listOffice = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_OFFICE T1 where UPPER_OFFICE_CD IS Null AND DEFINED_CD='" + selectedValue + "' order by ORDER_NO";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    listOffice.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Office") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listOffice.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
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
            return listOffice;
        }
        public List<SelectListItem> GetForwadedOffice(string selectedValue, string AddressedOffice)
        {
            List<SelectListItem> selLstOffice = new List<SelectListItem>();
            DataTable dt = commonService.getOrderNoByOfficeCd(AddressedOffice);
            string OrderNo = dt.Rows[0][0].ConvertToString();
            if (!string.IsNullOrWhiteSpace(AddressedOffice))
                selLstOffice.AddRange(GetForwadedOfficeForCaseGrievance(OrderNo, AddressedOffice));
            //grp

            //if (!string.IsNullOrWhiteSpace(selectedValue))
            //    selLstOffice = selLstOffice.Where(v => v.Value == selectedValue).ToList();

            //selLstOffice = GetData.GetSelectedList(selLstOffice, selectedValue);
            return selLstOffice;
        }
        public List<SelectListItem> GetForwadedOfficeForCaseGrievance(string OrderNo, string selectedValue)
        {
            List<SelectListItem> listOffice = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_OFFICE T1 where UPPER_OFFICE_CD IS Null AND ORDER_NO<'" + OrderNo + "' order by ORDER_NO";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    listOffice.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Office") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listOffice.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
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
            return listOffice;
        }
        public List<SelectListItem> GetUserGroup(string selectedValue)
        {
            List<SelectListItem> selLstUserGroup = new List<SelectListItem>();
            GroupService userGroupService = new GroupService();
            try
            {
                List<Group> lstUserGroup = userGroupService.GetUserGroup();
                foreach (Group userGroup in lstUserGroup)
                {
                    selLstUserGroup.Add(new SelectListItem { Value = userGroup.GrpCode, Text = Utils.GetLabel(userGroup.GrpName) });
                }
                foreach (SelectListItem item in selLstUserGroup)
                {
                    if (item.Value == selectedValue)
                        item.Selected = true;
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return selLstUserGroup;
        }
        public List<SelectListItem> getReportLayer(string id, string districtid, string zoneid, string vdcid)
        {
            return getReportLayerHH(id, null, districtid, zoneid, vdcid, null, null);
        }
        public List<SelectListItem> getLayer(string id, string districtid, string zoneid, string vdcid)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();
            if (id != null)
            {

                if (id == "Permanent District" || id == "Temporary District" || id == "Citizen District" || id == "Voter District" || id == "NID District" || id == "District")
                {
                    listLayer = GetDistricts("");


                }
                else if (id == "Region/State")
                {
                    if (zoneid == "")
                    {
                        //listLayer = GetRegionState("");
                    }
                    else
                    {
                        //List<MISCommon> lstCommon = commonService.GetRegStatebyCodeandDescForZoneCode("", "", zoneid);
                        //foreach (Users user in lstUser)
                        //{
                        //    listLayer.Add(new SelectListItem { Value = Convert.ToString(user.regStateCode), Text = user.regStateName });
                        //}
                    }
                }
                else if (id == "Zone")
                {
                    if (districtid == "")
                    {
                        listLayer = GetZone("");
                    }
                    else
                    {
                        //List<MISCommon> lstCommon = commonService.GetZonebyCodeandDescForDistCode("", "", districtid);
                        //foreach (Users user in lstUser)
                        //{
                        //    listLayer.Add(new SelectListItem { Value = Convert.ToString(user.zoneCode), Text = user.zoneName });
                        //}
                    }
                }
                else if (id == "Permanent VDC/Municipality" || id == "Temporary VDC/Municipality")
                {
                    if (districtid == "")
                    {
                        listLayer = GetVDCMunByDistrict("", "", true);
                    }
                    else
                    {
                        lstCommon = commonService.GetVDCMunbyCodeandDescForDistCode("", "", districtid);
                        foreach (MISCommon common in lstCommon)
                        {
                            listLayer.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
                        }
                    }


                }
                else if (id == "Permanent Ward" || id == "Temporary Ward")
                {
                    if (vdcid == "")
                    {
                        listLayer = GetWardByVDCMun("", "", true);
                    }
                    else
                    {
                        lstCommon = commonService.GetWardbyCodeandDescForVDCMunCode("", "", vdcid);
                        foreach (MISCommon common in lstCommon)
                        {
                            listLayer.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Code.ToString() });
                        }
                    }
                }
                else if (id == "Gender")
                {
                    listLayer = GetGender("");
                }
                else if (id == "Married")
                {
                    listLayer = GetMaritalStatus("");
                }
                else if (id == "Caste")
                {
                    listLayer = GetCaste("");
                }
                //else if (id == "Household")
                //{
                //    listLayer = GetHouseHold("");
                //}
                else if (id == "Religion")
                {
                    listLayer = GetReligion("");
                }
                else if (id == "Education")
                {
                    listLayer = GetEducation("");
                }
                else if (id == "Office")
                {
                    //listLayer = GetOffice("");
                }
                else if (id == "Position")
                {
                    //listLayer = GetPosition("");
                }
                else if (id == "Position Sub Class")
                {
                    //listLayer = GetPositionSubClass("");
                }
                else if (id == "Document Type")
                {
                    listLayer = GetDocumentType("");

                }
            }
            return listLayer;
        }
        public List<SelectListItem> getFilteredData(string id, string desc, string layertype, string zoneid, string districtid, string vdcid)
        {
            return getFilteredDataHH(id, desc, layertype, null, zoneid, districtid, vdcid, null, null);
        }
        public List<SelectListItem> getFilteredDataHH(string id, string desc, string layertype, string regionid, string zoneid, string districtid, string vdcid, string ward, string area)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();
            if (layertype != null)
            {

                if (layertype == "Permanent District" || layertype == "Temporary District" || layertype == "Citizen District" || layertype == "Voter District" || layertype == "NID District" || layertype == "District")
                {
                    if (zoneid == null || zoneid == "")
                    {
                        lstCommon = commonService.GetDistrictsbyCodeandDesc(id, desc);
                    }
                    else
                    {
                        lstCommon = commonService.GetDistrictbyCodeandDescForZoneCode(id, desc, zoneid);
                    }
                }
                else if (layertype == "Zone")
                {
                    lstCommon = commonService.GetZonebyCodeandDesc(id, desc);
                }
                else if (layertype == "Region/State")
                {

                }
                else if (layertype == "Permanent VDC/Municipality" || layertype == "Temporary VDC/Municipality" || layertype == "VDC/Municipality")
                {
                    lstCommon = commonService.GetVDCMunbyCodeandDescForDistCode(id, desc, districtid);

                }
                else if (layertype == "Permanent Ward" || layertype == "Temporary Ward" || layertype == "Ward")
                {
                    lstCommon = commonService.GetWardbyCodeandDescForVDCMunCode(id, desc, vdcid);
                }
                else if (layertype == "Gender")
                {
                    lstCommon = commonService.GetGenderbyCodeandDesc(id, desc);
                }
                else if (layertype == "Married")
                {
                    lstCommon = commonService.GetMarriedbyCodeandDesc(id, desc);
                }
                else if (layertype == "Caste")
                {
                    lstCommon = commonService.GetCastebyCodeandDesc(id, desc);
                }
                else if (layertype == "Religion")
                {
                    lstCommon = commonService.GetReligionbyCodeandDesc(id, desc);
                }
                else if (layertype == "Education")
                {
                    lstCommon = commonService.GetEducationbyCodeandDesc(id, desc);
                }
                else if (layertype == "Household")
                {
                    lstCommon = commonService.GetHouseHoldbyZoneDistrictVDCWard(id, desc, regionid, zoneid, districtid, vdcid, ward, area);
                }
                else if (layertype == "Office")
                {
                    //lstCommon = commonService.GetOfficebyCodeandDesc(id, desc);                    
                }
                else if (layertype == "Position")
                {
                    //lstCommon = commonService.GetPositionbyCodeandDesc(id, desc);                    
                }
                else if (layertype == "Position Sub Class")
                {
                    //lstCommon = commonService.GetPositionSubClassbyCodeandDesc(id, desc);                    
                }
                else if (layertype == "Document Type")
                {
                    lstCommon = commonService.GetDocumentTypebyCodeandDesc(id, desc);
                }
                if (lstCommon != null)
                {
                    foreach (MISCommon common in lstCommon)
                    {
                        listLayer.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
                    }
                }
            }
            return listLayer;
        }
        public List<SelectListItem> GetRelation1(string selectedValue)
        {
            List<SelectListItem> selLstRelType = new List<SelectListItem>();
            selLstRelType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Relation Type") + "---" });
            List<MISCommon> lstRelType = commonService.GetRelationTypeByCodeAndDesc1("", "");
            foreach (MISCommon relType in lstRelType)
            {
                selLstRelType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
            }
            foreach (SelectListItem item in selLstRelType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRelType;
        }
        public List<SelectListItem> GetRelation(string selectedValue)
        {
            List<SelectListItem> selLstRelType = new List<SelectListItem>();
            selLstRelType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Relation Type") + "---" });
            List<MISCommon> lstRelType = commonService.GetRelationTypeByCodeAndDesc("", "");
            foreach (MISCommon relType in lstRelType)
            {
                selLstRelType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
            }
            foreach (SelectListItem item in selLstRelType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRelType;
        }
        public List<SelectListItem> GetFatherRelation(string selectedValue)
        {
            List<SelectListItem> selLstRelType = new List<SelectListItem>();
            selLstRelType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Relation Type") + "---" });
            List<MISCommon> lstRelType = commonService.GetRelationTypeByCodeAndDesc("", "");
            foreach (MISCommon relType in lstRelType)
            {
                if (relType.Description == "Daughter" || relType.Description == "Son" || relType.Description == "छोरा" || relType.Description == "छोरी")
                {
                    selLstRelType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
                }

            }
            foreach (SelectListItem item in selLstRelType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRelType;
        }
        public List<SelectListItem> GetCatalogNumber(string selectedValue)
        {
            List<SelectListItem> selLstRelType = new List<SelectListItem>();
            selLstRelType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Catalog No.") + "---" });
            List<MISCommon> lstRelType = commonService.GetCatalogNumber("", "");
            foreach (MISCommon relType in lstRelType)
            {
                selLstRelType.Add(new SelectListItem { Value = relType.Code, Text = relType.Description });
            }
            foreach (SelectListItem item in selLstRelType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRelType;
        }
        public List<SelectListItem> GetGrandFatherRelation(string selectedValue)
        {
            List<SelectListItem> selLstRelType = new List<SelectListItem>();
            selLstRelType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Relation Type") + "---" });
            List<MISCommon> lstRelType = commonService.GetRelationTypeByCodeAndDesc("", "");
            foreach (MISCommon relType in lstRelType)
            {
                if (relType.Description == "Grand Daughter" || relType.Description == "Grand Son" || relType.Description == "नाति" || relType.Description == "नातिनी")
                {
                    selLstRelType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
                }

            }
            foreach (SelectListItem item in selLstRelType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRelType;
        }
        public List<SelectListItem> GetSchoolType(string selectedvalue)
        {
            List<SelectListItem> selListSchool = new List<SelectListItem>();
            selListSchool.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select School Type") + "---" });
            selListSchool.AddRange(GetData.AllSchoolTypes(Utils.ToggleLanguage("english", "nepali")));
            return selListSchool;
        }
        public List<SelectListItem> GetRoofType(string selectedValue)
        {
            List<SelectListItem> selLstRoofType = new List<SelectListItem>();
            selLstRoofType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Roof Construction Type") + "---" });
            List<MISCommon> lstRelType = commonService.GetRoofTypeByCode("", "");
            foreach (MISCommon relType in lstRelType)
            {
                selLstRoofType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
            }
            foreach (SelectListItem item in selLstRoofType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRoofType;
        }
        public List<SelectListItem> GetHumanLoss(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            selLst.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Human Loss Type") + "---" });
            List<MISCommon> lstCommon = commonService.GetHumanLossType(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetDeathReason(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            selLst.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Death Reason Type") + "---" });
            List<MISCommon> lstCommon = commonService.GetDeathReasonType(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetAllowanceType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            selLst.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Allowance Type") + "---" });
            List<MISCommon> lstCommon = commonService.GetAllowanceType(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.Code), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetClassType(string selectedvalue)
        {
            List<SelectListItem> selListClass = new List<SelectListItem>();
            selListClass.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Class Type") + "---" });
            selListClass.AddRange(GetData.AllClassTypes(Utils.ToggleLanguage("english", "nepali")));
            selListClass = GetData.GetSelectedList(selListClass, selectedvalue);
            return selListClass;
        }
        public List<SelectListItem> GetOffice(string selectedValue)
        {
            List<SelectListItem> selLstOffice = new List<SelectListItem>();
            selLstOffice.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Office") + "---" });
            selLstOffice.AddRange(GetData.AllOffices(Utils.ToggleLanguage("english", "nepali")));
            selLstOffice = GetData.GetSelectedList(selLstOffice, selectedValue);
            return selLstOffice;
        }
        public List<SelectListItem> GetPosition(string selectedValue)
        {
            List<SelectListItem> selLstPosition = new List<SelectListItem>();
            selLstPosition.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Position") + "---" });
            selLstPosition.AddRange(GetData.AllPositions(Utils.ToggleLanguage("english", "nepali")));
            selLstPosition = GetData.GetSelectedList(selLstPosition, selectedValue);
            return selLstPosition;
        }
        public List<SelectListItem> GetPositionSubClass(string selectedValue)
        {
            List<SelectListItem> selLstPositionSubClass = new List<SelectListItem>();
            selLstPositionSubClass.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Position Sub Class") + "---" });
            selLstPositionSubClass.AddRange(GetData.AllSubClasses(Utils.ToggleLanguage("english", "nepali")));
            selLstPositionSubClass = GetData.GetSelectedList(selLstPositionSubClass, selectedValue);
            return selLstPositionSubClass;
        }
        public List<SelectListItem> GetDesignationByPosition(string selectedValue, string positionCd)
        {
            List<SelectListItem> selLstDesignation = new List<SelectListItem>();
            selLstDesignation.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Designation") + "---" });
            if (!string.IsNullOrWhiteSpace(positionCd))
                selLstDesignation.AddRange(GetData.AllDesignationByPosition(positionCd, Utils.ToggleLanguage("english", "nepali")));

            selLstDesignation = GetData.GetSelectedList(selLstDesignation, selectedValue);
            return selLstDesignation;
        }
        public List<SelectListItem> GetCountry(string selectedValue)
        {
            List<SelectListItem> selLstCountry = new List<SelectListItem>();
            List<MISRegionState> lstCountry = null;
            try
            {
                lstCountry = regStateService.GetCountry("", "");
                selLstCountry.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Country") + "---" });
                foreach (MISRegionState objRegState in lstCountry)
                {
                    selLstCountry.Add(new SelectListItem { Value = Convert.ToString(objRegState.CountryCd), Text = objRegState.CountryName });
                }
                foreach (SelectListItem item in selLstCountry)
                {
                    if (item.Value == selectedValue)
                        item.Selected = true;
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return selLstCountry;
        }
        public List<SelectListItem> GetPositionSubClassbyDesignation(string selectedValue, string desigCd, string code = "", string desc = "")
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetPositionSubClassByDesigByCodeAndDesc(code, desc, desigCd);
            selLst.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Position Sub-Class") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.DefinedCode), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetSection(string selectedValue, string officeCd, string code = "", string desc = "")
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetSectionByCodeAndDesc(code, desc, officeCd);
            selLst.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Section") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.DefinedCode), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetHandiColor(string selectedValue)
        {
            List<SelectListItem> selLst = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetHandiColor();
            selLst.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Handi Color") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLst.Add(new SelectListItem { Value = Convert.ToString(common.DefinedCode), Text = common.Description });
            }
            foreach (SelectListItem item in selLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLst;
        }
        public List<SelectListItem> GetEligibilityRuleName(string selectedValue)
        {
            List<SelectListItem> lstRuleName = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ELIGIBILITY_RULE T1 order by RULE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    lstRuleName.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Eligibility Rule Name", "गरिबी मापन रुल छान्नुहोस्") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.GetLabel(dr["RULE_NAME"].ConvertToString());
                        lstRuleName.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
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
            return lstRuleName;
        }
        #endregion

        #region JsonResult
        public JsonResult FillVDCMun(string id)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetVDCMunbyCodeandDescForDistCode("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FillRuralUrbanMunicipality(string id)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetRuralUrbanCodeandDescForDistCode("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult FillRuralUrbanWardNo(string id)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetRuralUrbanCodeandwardnoForRUMC("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillBranchBybank(string id)
        {
            CommonService comser = new CommonService();

            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = comser.GetbranchformbankCode("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public string GetPkgNamefromPkgCd(string pkg_cd)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "select DESC_ENG from NHRS_DONOR_PKG_SUPPORT where PKG_CD='" + pkg_cd + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0][0].ToString();
                }

            }
            return result;
        }
        public JsonResult FillWard(string id)
        {
            List<SelectListItem> selLstWard = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetWardbyCodeandDescForVDCMunCode("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillRegionState(string id)
        {
            List<SelectListItem> selLstRegState = new List<SelectListItem>();
            List<MISCommon> lstRegState = commonService.GetRegionStatebyCodeandDescForZoneCode("", "", id);
            return new JsonResult { Data = lstRegState, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillDistrictbyZone(string zoneCode)
        {
            List<SelectListItem> selLstRegState = new List<SelectListItem>();
            List<MISCommon> lstzone = new List<MISCommon>();
            if (zoneCode != "")
            {
                lstzone = commonService.GetDistrictbyZone(zoneCode);
            }
            return new JsonResult { Data = lstzone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillHouseHoldbyZoneDistrictVDCWard(string regionCode, string zoneCode, string distCode, string vdcCode, string ward, string area)
        {
            List<SelectListItem> selLstRegState = new List<SelectListItem>();
            List<MISCommon> lstzone = new List<MISCommon>();
            lstzone = commonService.GetHouseHoldbyZoneDistrictVDCWard("", "", regionCode, zoneCode, distCode, vdcCode, ward, area);
            return new JsonResult { Data = lstzone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillTableName(string TableType)
        {

            List<SelectListItem> lstTable = new List<SelectListItem>();
            lstTable = GetTableName("", TableType);
            return new JsonResult { Data = lstTable, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillZone(string zoneCode)
        {
            List<SelectListItem> selLstRegState = new List<SelectListItem>();
            List<MISCommon> lstzone = commonService.GetZonebyCodeandDescForDistCode("", "", zoneCode);
            return new JsonResult { Data = lstzone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GetHours(string selectdHours)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Hr.");
            lii.Value = "";
            liSelect.Add(lii);
            for (int i = 0; i <= 23; i++)
            {
                SelectListItem li = new SelectListItem();
                string txtVal = string.Format("{0:00}", i);
                li.Text = Utils.GetNumber(txtVal);
                li.Value = i.ToString();
                if (li.Value == selectdHours)
                    li.Selected = true;
                liSelect.Add(li);
            }
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetMinutes(string selectdMinutes)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Min.");
            lii.Value = "";
            liSelect.Add(lii);
            for (int i = 0; i < 60; i++)
            {
                SelectListItem li = new SelectListItem();
                string txtVal = string.Format("{0:00}", i);
                li.Text = Utils.GetNumber(txtVal);
                li.Value = i.ToString();
                if (li.Value == selectdMinutes)
                    li.Selected = true;
                liSelect.Add(li);

            }
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetEnrollmentStatus(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("All");
            liii.Value = "All";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);


            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Enrolled");
            lii.Value = "Enrolled";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Not Enrolled");
            li.Value = "NotEnrolled";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public List<SelectListItem> GetBeneficiary(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();


            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);


            return liSelect;
        }

        public List<SelectListItem> getInspectionYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();


            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Yes");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("No");
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);


            return liSelect;
        }
        public List<SelectListItem> GetApprove(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();



            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return liSelect;
        }

        public List<SelectListItem> GetMigration(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();



            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return liSelect;
        }

        public List<SelectListItem> GetApproveAll(string selectdYesNoAll)
        {
            List<SelectListItem> liSelectAll = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNoAll)
                lii.Selected = true;
            liSelectAll.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNoAll)
                li.Selected = true;
            liSelectAll.Add(li);

            SelectListItem liA = new SelectListItem();
            liA.Text = Utils.GetLabel("All");
            liA.Value = "A";
            if (li.Value == selectdYesNoAll)
                li.Selected = true;
            liSelectAll.Add(liA);

            return liSelectAll;
        }
        public JsonResult GetMOUYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("All");
            liii.Value = "All";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);


            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("All");
            liii.Value = "";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNoAll(String selectYesNoAll)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectYesNoAll)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectYesNoAll)
                li.Selected = true;
            liSelect.Add(li);

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("All");
            liii.Value = "A";
            if (liii.Value == selectYesNoAll)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem liiii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("None", "कुनै पनि");
            liii.Value = "Nn";
            if (liii.Value == selectYesNoAll)
                liii.Selected = true;
            liSelect.Add(liii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public List<SelectListItem> CaseGrievanceStatus(string selecetdVal)
        {
            List<SelectListItem> lstLetterType = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Value = "O";
            li.Text = Utils.GetLabel("Open");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);
            li = new SelectListItem();
            li.Value = "P";
            li.Text = Utils.GetLabel("Pending");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);
            li = new SelectListItem();
            li.Value = "R";
            li.Text = Utils.GetLabel("Resolved");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);
            li = new SelectListItem();
            li.Value = "C";
            li.Text = Utils.GetLabel("Closed");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);
            return lstLetterType;
        }
        public JsonResult CaseStatus(string selecetdVal)
        {
            List<SelectListItem> lstLetterType1 = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Value = "O";
            li.Text = Utils.GetLabel("Open");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType1.Add(li);
            li = new SelectListItem();
            li.Value = "P";
            li.Text = Utils.GetLabel("Pending");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType1.Add(li);
            li = new SelectListItem();
            li.Value = "R";
            li.Text = Utils.GetLabel("Resolved");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType1.Add(li);
            li = new SelectListItem();
            li.Value = "C";
            li.Text = Utils.GetLabel("Closed");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType1.Add(li);

            return new JsonResult { Data = lstLetterType1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetYesNo5(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "छैन");
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "छ");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            //SelectListItem liiii = new SelectListItem();
            //liiii.Text = Utils.ToggleLanguage("None", "कुनै पनि");
            //liiii.Value = "Nn";
            //if (liiii.Value == selectdYesNo)
            //    liiii.Selected = true;
            //liSelect.Add(liiii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo1(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुहोस्");
            liii.Value = "";
            liSelect.Add(liii);
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "छैन");
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "छ");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetPaUploadYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुहोस्");
            liii.Value = "";
            liSelect.Add(liii);
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "होइन");
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "हो");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            //SelectListItem liiii = new SelectListItem();
            //liiii.Text = Utils.ToggleLanguage("None", "कुनै पनि");
            //liiii.Value = "Nn";
            //if (liiii.Value == selectdYesNo)
            //    liiii.Selected = true;
            //liSelect.Add(liiii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetValueType(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुस!!");
            liii.Value = "";
            liSelect.Add(liii);
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("Text", "लेख");
            li.Value = "T";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("true/false", "हो/होइन");
            lii.Value = "B";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo4(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "पाउँदैन"); //
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "पाउँछन्"); //Utils.GetLabel("No");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo2(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "गर्दैनन"); //
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "गर्छन्"); //
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo3(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "जादैनन्"); //
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "जान्छन्"); //
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNoSearch(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुस!!"); //Utils.GetLabel("No");
            liii.Value = "";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo1Search(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुस!!"); //Utils.GetLabel("No");
            liii.Value = "";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "छैन"); //
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);


            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "छ"); //Utils.GetLabel("No");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYesNo2Search(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुस!!"); //Utils.GetLabel("No");
            liii.Value = "";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "गर्दैनन"); //
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "गरछन"); //
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion
        #region "Common"

        public List<SelectListItem> GetGroup(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetGroup(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLstDocument.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetGenderByCodeAndDesc(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetGenderbyCodeandDesc(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLstDocument.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        public List<SelectListItem> GetFiscalYear(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstFiscalYear = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetFiscalYear(code, desc);
            selLstFiscalYear.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Fiscal Year") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstFiscalYear.Add(new SelectListItem { Value = common.Code, Text = Utils.GetNumber(common.Description) });
            }
            foreach (SelectListItem item in selLstFiscalYear)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstFiscalYear;
        }
        public List<SelectListItem> GetDamageGrade(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetDamageGrade(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Damage Grade") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public List<SelectListItem> GetTechnicalSolution(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTechnicalSolution(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Tech Solution") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public List<SelectListItem> getRecommendationType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstRecommendationType = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetRecommendationType(code, desc);
            selLstRecommendationType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Recommendation") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstRecommendationType.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstRecommendationType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRecommendationType;
        }

        public List<SelectListItem> getThreeRecommendationType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstRecommendationType = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getThreeRecommendationType(code, desc);
            selLstRecommendationType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Recommendation") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstRecommendationType.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstRecommendationType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRecommendationType;
        }
        public List<SelectListItem> GetBuildingCondition(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBuildingCondition(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Building Condition") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public List<SelectListItem> GetGrievanceBatchId(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetGrievanceBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetBatchId(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetRetrofittingBatchId(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetRetroFittingBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetRetrofittingBeneficiaryBatchId(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetRetroFittingBeneficiaryBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "0", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                if (common.Description == string.Empty)
                { common.Description = "Null";
                    common.Code = "5";
                }
                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetTargetLot(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstLotId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTargetLotId(code, desc);
            selLstLotId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Target Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstLotId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstLotId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstLotId;
        }
        public List<SelectListItem> GetResurveyTargetLot(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstLotId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetResurveyTargetLotId(code, desc);
            selLstLotId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Target Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstLotId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstLotId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstLotId;
        }
        public List<SelectListItem> GetGrievanceBatchID(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetGrievanceBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetGrievanceTargetBatchID(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetGrievanceTargetBatchId(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetTargetID(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTargetID(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Target ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public List<SelectListItem> GetResurveyTargetID(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.ResurveyTargetBatch(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Target ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public List<SelectListItem> GetInstallation(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstInstallation = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetInstallation(code, desc);
            selLstInstallation.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Installment") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstInstallation.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstInstallation)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstInstallation;
        }
        public List<SelectListItem> GetBankNameByVDC(string selectedValue, string District, string VDC, string Ward, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBankName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankByVDC(VDC, Ward, District, desc);
            selLstBankName.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBankName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBankName)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBankName;
        }
        public List<SelectListItem> GetBankName(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBankName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankName(selectedValue, desc);
            selLstBankName.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBankName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBankName)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBankName;
        }
        public List<SelectListItem> GetBankAccountType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBankAccountType = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankAccountType(code, desc);
            selLstBankAccountType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank Account Type") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBankAccountType.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstBankAccountType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBankAccountType;
        }
        public List<SelectListItem> GetVDCSecretaryOffice(string empCode, string selectedValue)
        {
            List<SelectListItem> ListVdcSecretaryOffice = new List<SelectListItem>();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_VDC_SECRETARY";
                    dt = service.GetDataTable(true,
                                                "PR_SECRETARY_OFFICE",
                                                empCode.ToDecimal(),
                                                DBNull.Value
                                               );

                    SelectListItem li1 = new SelectListItem();
                    ListVdcSecretaryOffice.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC Secretary Office") + "---" });
                    foreach (DataRow dr in dt.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListVdcSecretaryOffice.Add(li);

                    }
                    foreach (SelectListItem item in ListVdcSecretaryOffice)
                    {
                        if (item.Value == selectedValue)
                            item.Selected = true;
                    }
                }

                catch (Exception ex)
                {
                    dt = null;
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
            return ListVdcSecretaryOffice;
        }
        public List<SelectListItem> GetBankBranchId(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBankBranchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankBranchId(selectedValue, desc);
            selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBankBranchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstBankBranchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBankBranchId;
        }
        public string GetBankBranchCdByStdCd(string BranchStdCd, string BankCd)
        {

            string Branch_CD = commonService.GetBankBranchCdByStdCd(BranchStdCd, BankCd);
            return Branch_CD;
        }
        public List<SelectListItem> GetBankBranchIdByBankID(string VDCCode, string WarNo, string BankCode, string desc = "")
        {
            List<SelectListItem> selLstBankBranchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankBranchIdByBankId(VDCCode, WarNo, BankCode, desc);
            //selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBankBranchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstBankBranchId;
        }
        public List<SelectListItem> GetBankByAddress(string Mode, string VDCCode, string WardNo, string DistrictCd, string desc = "")
        {
            List<SelectListItem> selLstBank = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankByAddress(Mode, VDCCode, WardNo, DistrictCd, desc);
            //selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBank.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstBank;
        }
        public List<SelectListItem> GetBatchIDByAddress(string VDCCode, string WardNo, string DistrictCd, string desc = "")
        {
            List<SelectListItem> selLstBatch = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBatchByAddress("", VDCCode, WardNo, DistrictCd, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLstBatch.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstBatch;
        }
        public List<SelectListItem> GetBankBranchIdByBankID(string BankCode, string desc = "")
        {
            List<SelectListItem> selLstBankBranchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBankBranchIdByBankId(BankCode, desc);

            selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank Branch") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBankBranchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstBankBranchId;
        }
        public List<SelectListItem> GetDistrictByBankBranchID(string DistrictCode, string desc = "")
        {
            List<SelectListItem> selLstBankBranchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetDistrictIDByBankBranchId(DistrictCode, desc);
            //selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstBankBranchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstBankBranchId;
        }
        public List<SelectListItem> GetOfficeByAddressedBank(string OfficeCode, string desc = "")
        {
            List<SelectListItem> selLstOfficeCd = new List<SelectListItem>();
            string DefinedOfficeCD = GetDefinedCodeFromDataBase(OfficeCode, "MIS_OFFICE", "OFFICE_CD");
            DataTable dt = commonService.getOrderNoByOfficeCd(DefinedOfficeCD);
            string OrderNo = dt.Rows[0][0].ConvertToString();
            List<MISCommon> lstCommon = commonService.GetOfficeByAddressedBankCD(OrderNo, desc);
            //selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstOfficeCd.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            return selLstOfficeCd;
        }
        public List<SelectListItem> GetAllTableName(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTableName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllTableName(code, desc);
            foreach (MISCommon common in lstCommon)
            {
                selLstTableName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstTableName)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTableName;
        }
        public List<SelectListItem> GetAllColumnNameOfTable(string tableName, string ColumnName)
        {
            List<SelectListItem> LstColumnName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllColumnName(tableName, ColumnName);
            foreach (MISCommon common in lstCommon)
            {
                LstColumnName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in LstColumnName)
            {
                if (item.Value == ColumnName)
                    item.Selected = true;
            }
            return LstColumnName;
        }
        public List<SelectListItem> GetAllColumnNameOfTable(string tableName)
        {
            List<SelectListItem> LstColumnName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllColumnName(tableName);
            foreach (MISCommon common in lstCommon)
            {
                LstColumnName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            //foreach (SelectListItem item in selLstTableName)
            //{
            //    if (item.Value == selectedValue)
            //        item.Selected = true;
            //}
            return LstColumnName;
        }
        public List<SelectListItem> GetDistrictByBatchID(string BatchID)
        {
            List<SelectListItem> LstColumnName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getDistrictByBatch(BatchID);
            foreach (MISCommon common in lstCommon)
            {
                LstColumnName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            //foreach (SelectListItem item in selLstTableName)
            //{
            //    if (item.Value == selectedValue)
            //        item.Selected = true;
            //}
            return LstColumnName;
        }
        public List<SelectListItem> GetDistrictByRetroBatchID(string BatchID)
        {
            List<SelectListItem> LstColumnName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getDistrictByRetroBatch(BatchID);
            foreach (MISCommon common in lstCommon)
            {
                LstColumnName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            //foreach (SelectListItem item in selLstTableName)
            //{
            //    if (item.Value == selectedValue)
            //        item.Selected = true;
            //}
            return LstColumnName;
        }
        public List<SelectListItem> getBatchIDByDistrict(string DistrictID)
        {
            List<SelectListItem> LstColumnName = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getBatchIDByDistrict(DistrictID);
            foreach (MISCommon common in lstCommon)
            {
                LstColumnName.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            //foreach (SelectListItem item in selLstTableName)
            //{
            //    if (item.Value == selectedValue)
            //        item.Selected = true;
            //}
            return LstColumnName;
        }

        #endregion

   
        public DataTable GetDescriptionForMapping(string TableName)
        {
            DataTable dTable = new DataTable();
            string CmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                CmdText = "SELECT DESC_ENG,DESC_LOC FROM " + TableName + "";
                try
                {
                    dTable = service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }

                    });
                }
                catch (Exception ex)
                {
                    dTable = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
            return dTable;

        }
        public string GetVdcMunCd(string dist_cd, string vdc_cd)
        {
            DataTable dTable = new DataTable();
            string CmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                CmdText = "select MLD_VDC_CD from mld_kll_vdc_map where kll_district_cd = " + dist_cd + " and kll_vdc_cd = " + vdc_cd;
                try
                {
                    dTable = service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }

                    });
                }
                catch (Exception ex)
                {
                    dTable = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
            return dTable.Rows[0][0].ToString();

        }
        public string GetMLDDistCd(string DistrictCD)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "select MLD_DISTRICT_CD from MLD_KLL_DISTRICT_MAP where KLL_DISTRICT_CD='" + DistrictCD + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["MLD_DISTRICT_CD"].ToString();
                }

            }
            return result;

        }
        public string GetVDCCdFromDatabase(decimal? code, string tableName, string ColumnName)
        {
            string result = "";
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string cmdText = "SELECT DEFINED_CD from " + tableName + " WHERE " + ColumnName + " ='" + code + "'";
                try
                {
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["DEFINED_CD"].ToString();
                }

            }
            return result;

        }
        public string GetCodeFromDataBase(string definedCd, string tableName, string codeColumnName)
        {
            string val = "";
            try
            {
                val = commonService.GetCodeForDefinedCdTablenameAndCodeFieldName(definedCd, tableName, codeColumnName);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return val;
        }
        public bool checkDuplicateDefinedCode(string code, string tableName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dtbl = new DataTable();
                string query = "SELECT DEFINED_CD FROM " + tableName + " WHERE DEFINED_CD = '" + code + "'";
                using (ServiceFactory service = new ServiceFactory())
                {
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction.Connection != null)
                        {
                            service.End();
                        }
                    }
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string GetDefinedCodeFromDataBase(string code, string tableName, string codeColumnName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DEFINED_CD from " + tableName + " WHERE " + codeColumnName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ConvertToString();
                    }
                }
            }
            return "";
        }

        public string GetDistrictCodeFromDataBase(string dist, string tableName, string ColumnName)
        {
            if (!string.IsNullOrWhiteSpace(dist))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DEFINED_CD from " + tableName + " WHERE " + ColumnName + " ='" + dist + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ConvertToString();
                    }
                }
            }
            return "";
        }

        public string GetMLDVDCCodeFromDataBase(string name, string tableName, string ColumnName)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                DataTable dt = new DataTable();
                string query = "SELECT MLD_VDC_CD from " + tableName + " WHERE " + ColumnName + " ='" + name + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ConvertToString();
                    }
                }
            }
            return "";
        }
        public string GetValueFromDataBase(string code, string tableName, string givenColumnName, string requiredColumnName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT " + requiredColumnName + " from " + tableName + " WHERE " + givenColumnName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {
                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dt.Rows[0][0].ConvertToString();
                    }
                }
            }
            return "";
        }
        public string GetDescriptionFromCode(string code, string tableName, string ColumanName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT DESC_ENG,DESC_LOC from " + tableName + " WHERE " + ColumanName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                        return Utils.ToggleLanguage(dt.Rows[0][0].ConvertToString(), dt.Rows[0][1].ConvertToString());
                }
            }
            return "";
        }
        public string GetDonorCd(string UserCode)
        {
            if (!string.IsNullOrWhiteSpace(UserCode))
            {
                DataTable dt = new DataTable();
                string query = "SELECT Donor_Cd from COM_WEB_USR where USR_CD = " + UserCode;
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                        return dt.Rows[0][0].ConvertToString();
                }
            }
            return "";
        }
        public DataTable GetUploadDbFields(string UploadFormatTable, string UploadFormatField, string UploadFormatCode)
        {
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    string query = "SELECT * FROM {0} WHERE 1 = 1 AND {1} = {2}";
                    query = string.Format(query, UploadFormatTable, UploadFormatField, UploadFormatCode);
                    service.Begin();
                    dtbl = service.GetDataTable(query, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }
        public bool CheckPODisVDC(string dis, string convertedDis)
        {
            DataTable dtbl1 = null;
            //DataTable dtbl2 = null;

            bool isValid = true;
            int n;
            var isNumeric = int.TryParse(dis, out n);

            string query1 = "SELECT MLD_DISTRICT_CD from MLD_KLL_DISTRICT_MAP where KLL_DISTRICT_CD = " + n;
            //string query2 = "SELECT MLD_VDC_CD FROM MLD_KLL_VDC_MAP WHERE KLL_DISTRICT_CD = " + dis + " AND KLL_VDC_CD = " + vdc;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dtbl1 = service.GetDataTable(query1, null);
                    //dtbl2 = service.GetDataTable(query2, null);
                }
                catch (Exception ex)
                {
                    dtbl1 = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            if (dtbl1.Rows[0][0].ToString() != convertedDis)
            {
                isValid = false;
            }

            return isValid;
        }
        public List<SelectListItem> LetterType(string selecetdVal)
        {
            List<SelectListItem> lstLetterType = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Value = "ME";
            li.Text = Utils.GetLabel("MoUEnglish");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);
            li = new SelectListItem();
            li.Value = "MN";
            li.Text = Utils.GetLabel("MoUNepali");
            if (selecetdVal == li.Value)
                li.Selected = true;
            lstLetterType.Add(li);

            return lstLetterType;
        }
        // for user specific lists
        #region User Specfic
        public List<SelectListItem> GetWardByVdcForUser(string selectedValue, bool ispopup = false)
        {

            string WardCd = HttpContext.Current.Session["UserWard"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false && (UserGroupFlag == "C" || UserGroupFlag == "D"))
            {
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Ward No.") + "---" });
            }
            for (int i = 1; i <= 35; i++)
            {
                SelectListItem li = new SelectListItem();
                li.Value = i.ConvertToString();
                li.Text = i.ConvertToString();
                if (li.Value == selectedValue)
                    li.Selected = true;
                selLstVDCMun.Add(li);
            }

            if (!string.IsNullOrWhiteSpace(WardCd))
                if (UserGroupFlag == "V")
                {
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == WardCd).ToList();
                }


            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetVDCMunByDistrictForUser(string selectedValue, bool ispopup = false)
        {
            string DistDefCd = HttpContext.Current.Session["UserDistrictDefCd"].ConvertToString();
            string VdcMunDefCd = HttpContext.Current.Session["UserVdcMunDefCd"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false && (UserGroupFlag == "D" || UserGroupFlag == "C"))
            {
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });
            }


            if (!string.IsNullOrWhiteSpace(DistDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.District, DistDefCd), Utils.ToggleLanguage("english", "nepali")));

            if (!string.IsNullOrWhiteSpace(VdcMunDefCd))
                if (UserGroupFlag == "V")
                {
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == VdcMunDefCd).ToList();
                }


            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetNewVDCMunByDistrictForUser(string selectedValue, bool ispopup = false)
        {
            string DistDefCd = HttpContext.Current.Session["UserDistrictDefCd"].ConvertToString();
            string VdcMunDefCd = HttpContext.Current.Session["UserVdcMunDefCd"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (ispopup == false && (UserGroupFlag == "D" || UserGroupFlag == "C"))
            {
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Rural/Urban Municipality") + "---" });
            }


            if (!string.IsNullOrWhiteSpace(DistDefCd))
                selLstVDCMun.AddRange(GetData.NewVdcByDistrict(GetData.GetCodeFor(DataType.District, DistDefCd), Utils.ToggleLanguage("english", "nepali")));

            if (!string.IsNullOrWhiteSpace(VdcMunDefCd))
                if (UserGroupFlag == "V")
                {
                    selLstVDCMun = selLstVDCMun.Where(v => v.Value == VdcMunDefCd).ToList();
                }


            selLstVDCMun = GetData.GetSelectedList(selLstVDCMun, selectedValue);
            return selLstVDCMun;
        }
        public List<SelectListItem> GetDistrictsForUser(string selectedValue, bool ispopup = false)
        {
            string DistDefCd = HttpContext.Current.Session["UserDistrictDefCd"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            string Zonencd = HttpContext.Current.Session["UserZoneCD"].ConvertToString();
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false && UserGroupFlag == "C")
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }


            if (UserGroupFlag == "V" || UserGroupFlag == "D")
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
            }
            else if (UserGroupFlag == "C")
            {
                //selLstDistricts.AddRange(GetData.DistrictbyZone(Zonencd, Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
            }

            selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();

            return selLstDistricts;
        }
        public List<SelectListItem> GetRegionForUser(string selectedValue, bool ispopup = false)
        {

            string RegionDefCd = string.Empty;
            string regioncd = HttpContext.Current.Session["UserRegionCD"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            if (regioncd != null)
            {
                RegionDefCd = GetData.GetDefinedCodeFor(DataType.Region, regioncd);
            }

            List<SelectListItem> selLstRegion = new List<SelectListItem>();

            if (ispopup == false && UserGroupFlag == "C")
            {
                selLstRegion.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Region/State") + "---" });
            }

            selLstRegion.AddRange(GetData.AllRegions(Utils.ToggleLanguage("english", "nepali")).ToList());
            if (UserGroupFlag == "V" || UserGroupFlag == "D")
            {
                selLstRegion = (selLstRegion.Where(r => r.Value == RegionDefCd).ToList());
            }

            selLstRegion = GetSelectedList(selLstRegion, selectedValue).ToList();


            return selLstRegion;
        }
        public List<SelectListItem> GetZoneForUser(string selectedValue, bool ispopup = false)
        {
            string ZoneDefCd = string.Empty;
            string Zonencd = HttpContext.Current.Session["UserZoneCD"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            string UserRegionCd = HttpContext.Current.Session["UserRegionCD"].ConvertToString();
            if (Zonencd != null)
            {
                ZoneDefCd = GetData.GetDefinedCodeFor(DataType.Zone, Zonencd);
            }

            List<SelectListItem> selLstZone = new List<SelectListItem>();
            if (ispopup == false && UserGroupFlag == "C")
            {
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Zone") + "---" });
            }

            if (UserGroupFlag == "V" || UserGroupFlag == "D")
            {
                selLstZone.AddRange(GetData.AllZones(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstZone = (selLstZone.Where(z => z.Value == ZoneDefCd).ToList());
            }
            else if (UserGroupFlag == "C")
            {
                selLstZone.AddRange(GetData.ZoneByRegion(UserRegionCd, Utils.ToggleLanguage("english", "nepali")).ToList());
            }
            if (ispopup == false)
            {
                selLstZone = GetSelectedList(selLstZone, selectedValue).ToList();
            }
            return selLstZone;
        }
        #endregion
        #region Report
        public List<SelectListItem> ReportType()
        {
            List<SelectListItem> selLstReport = new List<SelectListItem>();

            //selLstReport.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Report Type") + "---" });
            selLstReport.Add(new SelectListItem { Value = "W", Text = "Beneficiary Report By Ward" });
            // selLstReport.Add(new SelectListItem { Value = "G", Text = "House Damage Summary" });
            selLstReport.Add(new SelectListItem { Value = "EL", Text = "Enrolled Beneficiary Report" });
            selLstReport.Add(new SelectListItem { Value = "EI", Text = "Beneficiary Report By Identification" });
            selLstReport.Add(new SelectListItem { Value = "EO", Text = "Beneficiary Report with All Owner" }); 
            return selLstReport; 
        }
        public List<SelectListItem> ReportTypeForTargetedBenificiary()
        {
            List<SelectListItem> selLstReport = new List<SelectListItem>();

            //selLstReport.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Report Type") + "---" });
            selLstReport.Add(new SelectListItem { Value = "EL", Text = "Enrolled Beneficiary Report" });
            selLstReport.Add(new SelectListItem { Value = "EI", Text = "Beneficiary Report By Identification" });
            selLstReport.Add(new SelectListItem { Value = "EO", Text = "Beneficiary Report with All Owner" });

            return selLstReport;
        }
        public List<SelectListItem> GetDataBatch()
        {
            List<SelectListItem> sellstBatch = new List<SelectListItem>();
            sellstBatch.Add(new SelectListItem { Value = "", Text = Utils.GetLabel("Please Select") });
            sellstBatch.Add(new SelectListItem { Value = "1", Text = "1" });
            sellstBatch.Add(new SelectListItem { Value = "2", Text = "2" });
            sellstBatch.Add(new SelectListItem { Value = "3", Text = "3" });
            sellstBatch.Add(new SelectListItem { Value = "4", Text = "4" });
            sellstBatch.Add(new SelectListItem { Value = "5", Text = "5" });
            sellstBatch.Add(new SelectListItem { Value = "6", Text = "6" });
            sellstBatch.Add(new SelectListItem { Value = "7", Text = "7" });
            sellstBatch.Add(new SelectListItem { Value = "8", Text = "8" });
            return sellstBatch;

        }
        public List<SelectListItem> ReportTypeForCaseGrievance()
        {
            List<SelectListItem> selLstReport = new List<SelectListItem>();

            //selLstReport.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Report Type") + "---" });
            selLstReport.Add(new SelectListItem { Value = "GS", Text = "Grievance Summary By Case Type" });
            selLstReport.Add(new SelectListItem { Value = "GS1", Text = "Grievance Summary By House Legal Owner" });
            selLstReport.Add(new SelectListItem { Value = "GD", Text = "Grievance By District" });
            selLstReport.Add(new SelectListItem { Value = "GDD", Text = "Grievance By Doc Detail" });

            return selLstReport;
        }
        public List<SelectListItem> ReportTypeForMemberInfo()
        {
            List<SelectListItem> selLstReport = new List<SelectListItem>();

            //selLstReport.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Report Type") + "---" });
            selLstReport.Add(new SelectListItem { Value = "S", Text = "Survey" });
            selLstReport.Add(new SelectListItem { Value = "HO", Text = "House Owner" });
            selLstReport.Add(new SelectListItem { Value = "F", Text = "Family" });
            selLstReport.Add(new SelectListItem { Value = "M", Text = "Member" });

            return selLstReport;
        }
        #endregion
        public List<SelectListItem> GetBankCdByDistrictCd(string DisCd, string desc = "")
        {
            List<SelectListItem> selListBankCd = new List<SelectListItem>();
            List<MISCommon> lstCommon = new List<MISCommon>();

            //List<MISCommon> lstCommon = commonService.GetBankCdByDistrictCd(DisCd, desc);
            //selLstBankBranchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });
            if (DisCd != "")
            {
                DataTable dtbl = commonService.GetBankCdByDistrictCd(DisCd);

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["BANK_CD"].ConvertToString(), Description = drow["BANK_NAME_ENG"].ConvertToString() });
                    foreach (MISCommon common in lstCommon)
                    {
                        selListBankCd.Add(new SelectListItem { Value = common.Code, Text = common.Description });
                    }

                }

            }

            return selListBankCd;
        }
        public List<SelectListItem> getAllParticipantsType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> ParticipantList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllParticipantsType(code, desc);
            ParticipantList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Participants Type", "सहभागी प्रकार छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                ParticipantList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in ParticipantList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return ParticipantList;
        }
        public List<SelectListItem> GetHouseownerTable(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> ParticipantList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllHouseOwnerColumns(code, desc);
            ParticipantList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Columns Name", "Select Columns Name") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                ParticipantList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in ParticipantList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return ParticipantList.Where(a => a.Text == "HOUSE_FAMILY_OWNER_CNT").ToList();
        }
        public List<SelectListItem> getAllParticipants(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> ParticipantList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllParticipants(code, desc);
            ParticipantList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Participants", "सहभागी छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                ParticipantList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in ParticipantList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return ParticipantList;
        }
        public List<SelectListItem> getEducationLevel(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> EdLvlList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllEducation(code, desc);
            EdLvlList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Education Level", "शैक्षिक तह छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                EdLvlList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in EdLvlList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return EdLvlList;
        }
        public List<SelectListItem> getTrainingEducationLevel(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> EdLvlList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllTrainingEducation(code, desc);
            EdLvlList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Education Level", "शैक्षिक तह छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                EdLvlList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in EdLvlList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return EdLvlList;
        }
        public List<SelectListItem> getEthnicity(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> EthnicityList = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetAllEthnicity(code, desc);
            EthnicityList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Ethnicity", "जाति समूह छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                EthnicityList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in EthnicityList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return EthnicityList;
        }
        public List<SelectListItem> getTrainingBatch(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> batchLst = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getTrainingBatch(code, desc);
            batchLst.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Training Batch", "तालिम समूह छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                batchLst.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in batchLst)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return batchLst;
        }
        //getCurriculum
        public List<SelectListItem> GetCurriculum(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstCur = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetCurriculum();
            if (ispopup == false)
            {
                selLstCur.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Curriculum", "पाठ्यक्रम छान्नुहोस्") + "---" });
            }
            var index = lstCommon.FindIndex(x => x.DefinedCode == "5");
            var item = lstCommon[index];
            lstCommon[index] = lstCommon[lstCommon.Count() - 1];
            lstCommon[lstCommon.Count() - 1] = item;

            foreach (MISCommon common in lstCommon)
            {
                selLstCur.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem items in selLstCur)
            {
                if (items.Value == selectedValue)
                    items.Selected = true;
            }
            return selLstCur;
        }
        //getImplementing Partner
        public List<SelectListItem> GetImplementingPartner(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstImplemntPartnr = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetImplementingPartner();
            if (ispopup == false)
            {
                selLstImplemntPartnr.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Implementing Partner", "कार्यान्वयन साझेदार छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstImplemntPartnr.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstImplemntPartnr)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstImplemntPartnr;
        }
        // participants type
        public List<SelectListItem> GetParticipantsType(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstPtype = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetParticipantsType();
            if (ispopup == false)
            {
                selLstPtype.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Participants Type", "सहभागी प्रकार छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstPtype.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstPtype)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstPtype;
        }
        //trainers type
        public List<SelectListItem> GetTrainersType(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstTrainType = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTrainersType();
            if (ispopup == false)
            {
                selLstTrainType.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Trainers Type", "प्रशिक्षक प्रकार छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstTrainType.Add(new SelectListItem { Value = common.DefinedCode, Text = common.Description });
            }
            foreach (SelectListItem item in selLstTrainType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTrainType;
        }
        public List<SelectListItem> GetallHouseModel(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstHoModl = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetHoueModel();
            if (ispopup == false)
            {
                selLstHoModl.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select House Design", "घरको डिजाईन छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstHoModl.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstHoModl)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstHoModl;
        }
        public List<SelectListItem> GetallHouseModelForInspectionOne(string selectedValue, bool ispopup = false)
        {
            List<SelectListItem> selLstHoModl = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetHoueModelForInspectionOne();
            if (ispopup == false)
            {
                selLstHoModl.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select House Design", "घरको डिजाईन छान्नुहोस्") + "---" });
            }
            foreach (MISCommon common in lstCommon)
            {
                selLstHoModl.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstHoModl)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstHoModl;
        }
        //get inspection pillar/base construction material 
        public List<SelectListItem> GetInspectionPillarMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selHouseDesign = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetInspectionBaseMaterial(code, desc);
            selHouseDesign.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select base/pillar material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selHouseDesign.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selHouseDesign)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selHouseDesign;
        }
        //get inspection roof material
        public List<SelectListItem> GetInspectionRoofMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selHouseDesign = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetInspectionRoofMaterial(code, desc);
            selHouseDesign.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select roof material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selHouseDesign.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selHouseDesign)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selHouseDesign;
        }
        // inspection No/level
        public List<SelectListItem> GetInspectionNo(string selectedValue)
        {
            List<SelectListItem> selInspNo = new List<SelectListItem>();
            selInspNo.Add(new SelectListItem { Value = "", Text = Utils.GetLabel("Please Select") });
            selInspNo.Add(new SelectListItem { Value = "D", Text = Utils.GetLabel("Design") });
            selInspNo.Add(new SelectListItem { Value = "1", Text = Utils.GetLabel("First Inspection") });
            selInspNo.Add(new SelectListItem { Value = "2", Text = Utils.GetLabel("Second Inspection") });
            selInspNo.Add(new SelectListItem { Value = "3", Text = Utils.GetLabel("Third inspection") });

            foreach (SelectListItem item in selInspNo)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selInspNo;
        }
        public List<SelectListItem> GetInspectionLevel(string selectedValue)
        {
            List<SelectListItem> selInspNo = new List<SelectListItem>();
            selInspNo.Add(new SelectListItem { Value = "", Text = Utils.GetLabel("Please Select") });
            selInspNo.Add(new SelectListItem { Value = "1", Text = Utils.GetLabel("First Inspection") });
            selInspNo.Add(new SelectListItem { Value = "2", Text = Utils.GetLabel("Second Inspection") });
            selInspNo.Add(new SelectListItem { Value = "3", Text = Utils.GetLabel("Third inspection") });

            foreach (SelectListItem item in selInspNo)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selInspNo;
        }
        // inspection No/level withou design
        public List<SelectListItem> GetInspectionLevelWithoutDesign(string selectedValue)
        {
            List<SelectListItem> selInspNo = new List<SelectListItem>();
            selInspNo.Add(new SelectListItem { Value = "", Text = Utils.GetLabel("Please Select") });
            selInspNo.Add(new SelectListItem { Value = "1", Text = Utils.GetLabel("First Inspection") });
            selInspNo.Add(new SelectListItem { Value = "2", Text = Utils.GetLabel("Second Inspection") });
            selInspNo.Add(new SelectListItem { Value = "3", Text = Utils.GetLabel("Third inspection") });

            foreach (SelectListItem item in selInspNo)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selInspNo;
        }
        // get mathematical operator indicator
        public JsonResult GetIndicator(string selectdIndicator)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem lvi = new SelectListItem();
            lvi.Text = Utils.GetLabel("--- Select Indicator ---");
            lvi.Value = "";
            if (lvi.Value == selectdIndicator)
                lvi.Selected = true;
            liSelect.Add(lvi);

            SelectListItem lv = new SelectListItem();
            lv.Text = "=";
            lv.Value = "=";
            if (lv.Value == selectdIndicator)
                lv.Selected = true;
            liSelect.Add(lv);

            SelectListItem liv = new SelectListItem();
            liv.Text = "<=";
            liv.Value = "<=";
            if (liv.Value == selectdIndicator)
                liv.Selected = true;
            liSelect.Add(liv);

            SelectListItem liii = new SelectListItem();
            liii.Text = ">=";
            liii.Value = ">=";
            if (liii.Value == selectdIndicator)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = "<";
            lii.Value = "<";
            if (lii.Value == selectdIndicator)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = ">";
            li.Value = ">";
            if (li.Value == selectdIndicator)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // get benf type
        public JsonResult GetBenfType(string selectdIndicator)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            //ParticipantList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Participants", "सहभागी छान्नुहोस्") + "---" });


            SelectListItem liiii = new SelectListItem();
            liiii.Text = Utils.GetLabel("Please Select");
            liiii.Value = "";
            if (liiii.Value == selectdIndicator)
                liiii.Selected = true;
            liSelect.Add(liiii);

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("All");
            liii.Value = "A";
            if (liii.Value == selectdIndicator)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Retro");
            lii.Value = "RTF";
            if (lii.Value == selectdIndicator)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Reconstruction");
            li.Value = "RTC";
            if (li.Value == selectdIndicator)
                li.Selected = true;
            liSelect.Add(li);

            SelectListItem liiiii = new SelectListItem();
            liiiii.Text = Utils.GetLabel("Grievance");
            liiiii.Value = "GRC";
            if (liiiii.Value == selectdIndicator)
                liiiii.Selected = true;
            liSelect.Add(liiiii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public List<SelectListItem> GetOthhousecondth(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetOthrhousecondth(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Othrhouse Condition") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //house legal owner 
        public List<SelectListItem> LegalOwner(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetHouseLegalOwner(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select House Legal Owner") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //foundation type
        public List<SelectListItem> getFoundationType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetFoundationType(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Foundation Type") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //floor material
        public List<SelectListItem> GetFloorMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetFloorConstructionMaterial(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Floor Material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //Building material
        public List<SelectListItem> getBuildingMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBuildingmaterial(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Storey Material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //Building Position
        public List<SelectListItem> getBuildingPosition(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBuildingPosition(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Building Position") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //Building Plan
        public List<SelectListItem> getPlanConfiguration(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBuildinPlanConfig(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Plan Configuration") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //Asssess area
        public List<SelectListItem> GetInspectedHousepart(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetBuildingAssessedArea(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Monitored Part") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //shelter after earthquake
        public List<SelectListItem> ShelterAfterEq(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getShelterAfterEq(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //shelter before earthquake
        public List<SelectListItem> ShelterBeforeEq(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getShelterBeforeEq(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //get monthly income level
        public List<SelectListItem> GetMonthlyIncome(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getMonthlyIncome(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public string GetDescriptionFromCodeByName(string code, string tableName, string ColumanName)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                DataTable dt = new DataTable();
                string query = "SELECT NAME_ENG,NAME_LOC from " + tableName + " WHERE " + ColumanName + " ='" + code + "'";
                using (ServiceFactory sf = new ServiceFactory())
                {

                    try
                    {
                        sf.Begin();
                        dt = sf.GetDataTable(query, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (sf.Transaction.Connection != null)
                        {
                            sf.End();
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                        return Utils.ToggleLanguage(dt.Rows[0][0].ConvertToString(), dt.Rows[0][1].ConvertToString());
                }
            }
            return "";
        }
        //get inspection own design construction status 
        public JsonResult InspectionConstructionStatus(string selectedValue)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem i = new SelectListItem();
            i.Text = Utils.GetLabel("---Please Select---");
            i.Value = "";
            if (i.Value == selectedValue)
                i.Selected = true;
            liSelect.Add(i);

            SelectListItem ii = new SelectListItem();
            ii.Text = Utils.GetLabel("Foundation Under Construction");
            ii.Value = "Foundation Under Construction";
            if (ii.Value == selectedValue)
                ii.Selected = true;
            liSelect.Add(ii);

            SelectListItem iii = new SelectListItem();
            iii.Text = Utils.GetLabel("Foundation Construction Completed");
            iii.Value = "Foundation Construction Completed";
            if (iii.Value == selectedValue)
                iii.Selected = true;
            liSelect.Add(iii);

            SelectListItem iv = new SelectListItem();
            iv.Text = Utils.GetLabel("Upto Roof Band in Ground Floor");
            iv.Value = "Upto Roof Band in Ground Floor";
            if (iv.Value == selectedValue)
                iv.Selected = true;
            liSelect.Add(iv);

            SelectListItem v = new SelectListItem();
            v.Text = Utils.GetLabel("Ground Floor Construction Completed (Including Roof)");
            v.Value = "Ground Floor Construction Completed (Including Roof)";
            if (v.Value == selectedValue)
                v.Selected = true;
            liSelect.Add(v);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //get inspection MOUDB approved batch 
        public List<SelectListItem> GetMOUDApprovedbatch(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getMOUDBatch(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Batch", "ब्याच छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //get inspection MOFALD approved batch 
        public List<SelectListItem> GetMOFALDpprovedbatch(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.getMOFALDBatch(code, desc);
            selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Batch", "ब्याच छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //inspection application level
        public JsonResult GetInspectionApplication(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुहोस्");
            liii.Value = "";
            liSelect.Add(liii);

            //SelectListItem iv = new SelectListItem();
            //iv.Text = Utils.ToggleLanguage("Inspection 1", "निरीक्षण १");
            //iv.Value = "1";
            //if (iv.Value == selectdYesNo)
            //    iv.Selected = true;
            //liSelect.Add(iv);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("Inspection 2", "निरीक्षण २");
            li.Value = "2";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Inspection 3", "निरीक्षण ३");
            lii.Value = "3";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            //SelectListItem liiii = new SelectListItem();
            //liiii.Text = Utils.ToggleLanguage("None", "कुनै पनि");
            //liiii.Value = "Nn";
            //if (liiii.Value == selectdYesNo)
            //    liiii.Selected = true;
            //liSelect.Add(liiii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //inspection with remarks or without remarks
        public JsonResult InspectionRemarksType(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुहोस्");
            liii.Value = "";
            liSelect.Add(liii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("With Remarks", "कैफियत भएको");
            li.Value = "1";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Without Remarks", "कैफियत नभएको");
            lii.Value = "2";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //if inspection is uploaded from tab or manually
        public JsonResult InspectionTabManual(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("Please Select", "कृपया छान्नुहोस्");
            liii.Value = "";
            liSelect.Add(liii);


            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("From Tab", "ट्याब बाट");
            li.Value = "1";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Manually", "एप्लिकेसन फम बाट");
            lii.Value = "2";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public List<SelectListItem> GetUrbanMunicipality(string selectedValue, string munciplaity_Cd)
        {
            List<SelectListItem> ListIdentificationType = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select NMUNICIPALITY_CD,DEFINED_CD, DESC_ENG,DESC_LOC FROM nhrs_nmunicipality WHERE DISTRICT_CD='" + Convert.ToInt32(selectedValue) + "'";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListIdentificationType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Rural/Urban Municipality") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListIdentificationType.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == munciplaity_Cd)
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
            return ListIdentificationType;
        }
        public JsonResult GetResettlementReview(string value)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();


            SelectListItem iv = new SelectListItem();
            iv.Text = "---" + Utils.GetLabel("Please Select") + "---";
            iv.Value = "";
            if (iv.Value == value)
                iv.Selected = true;
            liSelect.Add(iv);


            SelectListItem vii = new SelectListItem();
            vii.Text = Utils.GetLabel("Non Beneficiary");
            vii.Value = "NB";
            if (vii.Value == value)
                vii.Selected = true;
            liSelect.Add(vii);

            SelectListItem v = new SelectListItem();
            v.Text = Utils.GetLabel("Retrofitting Beneficiary");
            v.Value = "RTB";
            if (v.Value == value)
                v.Selected = true;
            liSelect.Add(v);

            SelectListItem vi = new SelectListItem();
            vi.Text = Utils.GetLabel("Grievance Beneficiary");
            vi.Value = "GB";
            if (vi.Value == value)
                vi.Selected = true;
            liSelect.Add(vi);

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("Not Found");
            liii.Value = "NF";
            if (liii.Value == value)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("Not Surveyed");
            lii.Value = "NS";
            if (lii.Value == value)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Reconstruction Beneficiary");
            li.Value = "RCB";
            if (li.Value == value)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public List<SelectListItem> GetTargetBatch(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstLotId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetTargetBatch(code, desc);
            selLstLotId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Target Id") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstLotId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstLotId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstLotId;
        }
        public List<SelectListItem> GetEnrollmentPAUser(string selectedValue)
        {
            List<SelectListItem> lstUser = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select distinct t1.ENTERED_BY  from nhrs_Enrollment_pa t1 where t1.entered_by is not null order by entered_by asc";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    lstUser.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select User") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["ENTERED_BY"].ConvertToString());
                        li.Text = dr["ENTERED_BY"].ConvertToString();
                        lstUser.Add(li);
                        if ((dr["ENTERED_BY"].ConvertToString()) == selectedValue)
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
            return lstUser;
        }
        public List<SelectListItem> GetNewVDCMunByDistrict(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstLotId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetNewVdcMunByDistCd(selectedValue);
            selLstLotId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select GP/NP") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstLotId.Add(new SelectListItem { Value = common.DefinedCode, Text = common.Description });
            }

            foreach (SelectListItem item in selLstLotId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstLotId;
        }
        public List<SelectListItem> GetNewVDCMunByDistrictVdc(string vdcCd, string distCd, string code = "", string desc = "")
        {
            List<SelectListItem> selLstLotId = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonService.GetNewVdcMunByDistVdc(distCd, vdcCd);
            selLstLotId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select GP/NP") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstLotId.Add(new SelectListItem { Value = common.DefinedCode, Text = common.Description });
            }

            foreach (SelectListItem item in selLstLotId)
            {
                if (item.Value == vdcCd)
                    item.Selected = true;
            }
            return selLstLotId;
        }
        //get new vdc/ward by old vdc/ward
        public List<MyValue> GetNewVDCWardByOldVdcWard(string distCd, string oldVdc, string oldWard, string code = "", string desc = "")
        {
            List<MyValue> selLstLotId = new List<MyValue>();
            List<MISCommonDistVdcWard> lstCommon = commonService.GetNewVdcWardByOldVdcWard(distCd, oldVdc, oldWard);
            foreach (MISCommonDistVdcWard common in lstCommon)
            {

                selLstLotId.Add(new MyValue { VdcValue = common.DefinedCodeVdc, VdcText = common.DescriptionVdc, WardValue = common.CodeWard });
            }
            return selLstLotId;
        }
        //get old vdc/ward by new vdc/ward
        public List<MyValue> GetOldVdcWardByNewVdcWard(string distCd, string oldVdc, string oldWard, string code = "", string desc = "")
        {
            List<MyValue> selLstLotId = new List<MyValue>();
            List<MISCommonDistVdcWard> lstCommon = commonService.GetOldVdcWardByNewVdcWard(distCd, oldVdc, oldWard);
            foreach (MISCommonDistVdcWard common in lstCommon)
            {
                selLstLotId.Add(new MyValue { VdcValue = common.DefinedCodeVdc, VdcText = common.DescriptionVdc, WardValue = common.CodeWard });
            }
            return selLstLotId;
        }
        //get old vdc by new vdc 
        public List<MyValue> GetOldVdcByNew(string distCd, string oldVdc, string code = "", string desc = "")
        {
            List<MyValue> selLstLotId = new List<MyValue>();
            List<MISCommonDistVdcWard> lstCommon = commonService.GetOldVdcByNewVdc(distCd, oldVdc);
            foreach (MISCommonDistVdcWard common in lstCommon)
            {
                selLstLotId.Add(new MyValue { VdcValue = common.DefinedCodeVdc, VdcText = common.DescriptionVdc });
            }
            return selLstLotId;
        }

        public List<SelectListItem> GetInspectionManageUser(string selectedValue)
        {
            List<SelectListItem> lstUser = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select distinct t1.ENTERED_BY  from NHRS_INSPECTION_MST t1 where t1.ENTERED_BY is not null order by ENTERED_BY asc";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    lstUser.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select User") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["ENTERED_BY"].ConvertToString());
                        li.Text = dr["ENTERED_BY"].ConvertToString();
                        lstUser.Add(li);
                        if ((dr["ENTERED_BY"].ConvertToString()) == selectedValue)
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
            return lstUser;
        }

    }

}
