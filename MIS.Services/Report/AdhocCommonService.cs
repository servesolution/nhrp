using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Setup;
using System.Data;
using System.Web;
using MIS.Services.Core;
using System.Data.OracleClient;
using MIS.Models.Core;
using ExceptionHandler;
using MIS.Models.Security;
using MIS.Models.Payment.HDSP;
using System.Windows.Forms;
using MIS.Models.Report;
using MIS.Models.Entity;

namespace MIS.Services.Report
{
    public class AdhocCommonService
    {
        List<MISCommon> lstCommon = null;
        string cmdText = "";
        DataTable dt = new DataTable();
        public List<MISCommon> getTrainingLevelList()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory sf = new ServiceFactory())
                {
                    sf.Begin();
                    cmdText = "SELECT TRAINING_LEVEL_CD, DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") +
                        " AS DESCRIPTION FROM NHRS_TRAINING_LEVEL ORDER BY TRAINING_LEVEL_CD";
                    try
                    {
                        dt = sf.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
                        });
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
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["TRAINING_LEVEL_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            return lstCommon;
        }


        /// <summary>
        /// this function is for Training Type List
        /// </summary>
        /// <returns></returns>
        public List<MISCommon> getTrainingTypeList()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory sf = new ServiceFactory())
                {
                    sf.Begin();
                    cmdText = "SELECT TRAINING_TYPE_CD, DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") +
                        " AS DESCRIPTION FROM NHRS_TRAINING_TYPE ORDER BY TRAINING_TYPE_CD";
                    try
                    {
                        dt = sf.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
                        });
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
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["TRAINING_TYPE_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            return lstCommon;
        }

        public List<MISCommon> GetCertificate()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory sf = new ServiceFactory())
                {
                    sf.Begin();
                    cmdText = "SELECT CERTIFICATE_CD, DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") +
                        " AS DESCRIPTION FROM NHRS_TRAINING_CERTIFICATE ORDER BY CERTIFICATE_CD";
                    try
                    {
                        dt = sf.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
                        });
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
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["CERTIFICATE_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            return lstCommon;
        }

        public List<MISCommon> GetTrainer(string code, string desc)
        {




            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = "SELECT TRAINER_ID, DEFINED_CD, " + Utils.ToggleLanguage("TRAINER_FULL_NAME_ENG", "TRAINER_FULL_NAME_LOC") + " AS DESCRIPTION FROM NHRS_TRAINERS ORDER BY DEFINED_CD";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;




        }
        public List<MISCommon> GetProfession()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory sf = new ServiceFactory())
                {
                    sf.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT PROFESSION_CD, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_TRAINING_PROFESSION";
                    }
                    else
                    {
                        cmdText = "SELECT PROFESSION_CD, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_TRAINING_PROFESSION";
                    }
                    cmdText += " ORDER BY TO_NUMBER(PROFESSION_CD)";
                    try
                    {
                        dt = sf.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
                        });
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
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["PROFESSION_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            return lstCommon;
        }
        public List<MISCommon> GetOrganization()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT ORGANIZATION_ID, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_TRAINING_ORGANIZATION";
                    }
                    else
                    {
                        cmdText = "SELECT ORGANIZATION_ID, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_TRAINING_ORGANIZATION";
                    }
                    cmdText += " ORDER BY TO_NUMBER(ORGANIZATION_ID)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["ORGANIZATION_ID"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetDistrictsbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select DISTRICT_CD, DEFINED_CD, DESC_ENG DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    }
                    else
                    {
                        cmdText = "select DISTRICT_CD,DEFINED_CD, DESC_LOC DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and DISTRICT_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["DISTRICT_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetZonebyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select ZONE_CD,DEFINED_CD, DESC_ENG DESCRIPTION FROM MIS_ZONE where 1=1";
                    }
                    else
                    {
                        cmdText = "select ZONE_CD,DEFINED_CD, DESC_LOC DESCRIPTION FROM MIS_ZONE where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and ZONE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    cmdText += " order by to_number(ZONE_CD)";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["ZONE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetGenderbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select GENDER_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_GENDER where 1=1";
                    if (code != "")
                    {
                        cmdText += " and GENDER_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["GENDER_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }

            return lstCommon;
        }
        public List<MISCommon> GetMarriedbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MARITAL_STATUS_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_MARITAL_STATUS where 1=1";
                    if (code != "")
                    {
                        cmdText += " and MARITAL_STATUS_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MARITAL_STATUS_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetCastebyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select CASTE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_CASTE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and CASTE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["CASTE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetCasteGroupbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select CASTE_GROUP_CD,Defined_cd," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_CASTE_GROUP where 1=1";
                    if (code != "")
                    {
                        cmdText += " and CASTE_GROUP_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["CASTE_GROUP_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetHouseHoldbyZoneDistrictVDCWard(string code, string desc, string regionCode, string zoneCode, string distCode, string vdcCode, string ward, string area)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            if (regionCode != "" && regionCode != null && regionCode != "undefined")
            {
                regionCode = GetData.GetCodeFor(DataType.Region, regionCode);
            }
            if (zoneCode != "" && zoneCode != null && zoneCode != "undefined")
            {
                zoneCode = GetData.GetCodeFor(DataType.Zone, zoneCode);
            }
            if (distCode != "" && distCode != null)
            {
                distCode = GetData.GetCodeFor(DataType.District, distCode);
            }
            if (vdcCode != "" && vdcCode != null)
            {
                vdcCode = GetData.GetCodeFor(DataType.VdcMun, vdcCode);
            }
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select HOUSEHOLD_ID,DEFINED_CD FROM MIS_HOUSEHOLD_INFO where 1=1";
                    if (code != "" && code != null)
                    {
                        cmdText += " and UPPER(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "" && desc != null)
                    {
                        cmdText += " and UPPER(DEFINED_CD) like '%" + desc.ToUpper() + "%'";
                    }
                    if (regionCode != "" && regionCode != null && regionCode != "0" && regionCode != "undefined")
                    {
                        cmdText += " and PER_REG_ST_CD = '" + regionCode + "'";
                    }
                    if (zoneCode != "" && zoneCode != null && zoneCode != "0" && zoneCode != "undefined")
                    {
                        cmdText += " and PER_ZONE_CD = '" + zoneCode + "'";
                    }
                    if (distCode != "" && distCode != null && distCode != "0")
                    {
                        cmdText += " and PER_DISTRICT_CD = '" + distCode + "'";
                    }
                    if (vdcCode != "" && vdcCode != null && vdcCode != "0")
                    {
                        cmdText += " and PER_VDC_MUN_CD = '" + vdcCode + "'";
                    }
                    if (ward != "" && ward != null && ward != "0")
                    {
                        cmdText += " and PER_WARD_NO = '" + ward + "'";
                    }
                    if (area != "" && area != null)
                    {
                        cmdText += " and " + Utils.ToggleLanguage("PER_AREA_ENG", "PER_AREA_LOC") + " = '" + area + "'";
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DEFINED_CD"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetReligionbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select RELIGION_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_RELIGION where 1=1";
                    if (code != "")
                    {
                        cmdText += " and RELIGION_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["RELIGION_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetEducationbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select CLASS_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_class_type where 1=1";
                    if (code != "")
                    {
                        cmdText += " and EDUCATION_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["EDUCATION_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }
        public List<MISCommon> GetVDCMunbyCodeandDescForDistCode(string code, string desc, string districtid)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select VDC_MUN_CD,DEFINED_CD, DESC_ENG DESCRIPTION FROM MIS_VDC_MUNICIPALITY WHERE DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    else
                    {
                        cmdText = "select VDC_MUN_CD,DEFINED_CD, DESC_LOC DESCRIPTION FROM MIS_VDC_MUNICIPALITY WHERE DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and VDC_MUN_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and (DESC_LOC) like '%" + desc + "%'";
                        }
                    }
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                //lstCommon.Add(new MISCommon { Code = "", DefinedCode = "", Description = Utils.GetLabel("Select VDC/Municipality") });
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["VDC_MUN_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }

            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetWardbyCodeandDescForVDCMunCode(string code, string desc, string vdcid)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select WARD_NO FROM MIS_WARD WHERE VDC_MUN_CD='" + Convert.ToInt32(vdcid) + "'";
                    if (code != "")
                    {
                        cmdText += " and WARD_NO like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and WARD_NO like '%" + desc + "%'";
                    }
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                //lstCommon.Add(new MISCommon { Code = "", DefinedCode = "", Description = Utils.GetLabel("Select Ward No") });
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = Utils.GetLabel(drow["WARD_NO"].ConvertToString()), Description = Utils.GetLabel(drow["WARD_NO"].ConvertToString()) });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetRegionStatebyCodeandDescForZoneCode(string code, string desc, string zoneid)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select R.REG_ST_CD,R.DEFINED_CD, R.DESC_ENG DESCRIPTION FROM MIS_REGION_STATE R INNER JOIN MIS_ZONE Z ON R.REG_ST_CD=Z.REG_ST_CD WHERE Z.ZONE_CD='" + Convert.ToInt32(zoneid) + "'";
                    }
                    else
                    {
                        cmdText = "select R.REG_ST_CD,R.DEFINED_CD, R.DESC_LOC DESCRIPTION FROM MIS_REGION_STATE R INNER JOIN MIS_ZONE Z ON R.REG_ST_CD=Z.REG_ST_CD WHERE Z.ZONE_CD='" + Convert.ToInt32(zoneid) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and R.REG_ST_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(R.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(R.DESC_LOC) like '%" + desc + "%'";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["REG_ST_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetZonebyCodeandDescForDistCode(string code, string desc, string districtid)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select Z.ZONE_CD,Z.DEFINED_CD, Z.DESC_ENG DESCRIPTION FROM MIS_ZONE Z INNER JOIN MIS_DISTRICT D ON Z.ZONE_CD=D.ZONE_CD WHERE D.DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    else
                    {
                        cmdText = "select Z.ZONE_CD,Z.DEFINED_CD, Z.DESC_LOC DESCRIPTION FROM MIS_ZONE Z INNER JOIN MIS_DISTRICT D ON Z.ZONE_CD=D.ZONE_CD WHERE D.DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and Z.ZONE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(Z.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(Z.DESC_LOC) like '%" + desc.ToUpper() + "%'";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["ZONE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetZonebyRegionCode(string code, string desc, string RegionID)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            List<MISCommon> lstZone = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select ZONE_CD,DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_ZONE  WHERE 1=1 ";
                    if (RegionID.ConvertToString() != "")
                    {
                        cmdText += " and REGION_CD='" + Convert.ToInt32(RegionID) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and ZONE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and " + Utils.ToggleLanguage("upper(DESC_ENG)", "DESC_LOC") + " like '%" + Utils.ToggleLanguage(desc.ToUpper(), desc) + "%'";
                    }
                    cmdText += " order by to_number(ZONE_CD)";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstZone.Add(new MISCommon { Code = (drow["Zone_CD"].ConvertToString()), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstZone = null;
            }
            return lstZone;
        }

        public List<MISCommon> GetDistrictbyCodeandDescForZoneCode(string code, string desc, string zoneId)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            List<MISCommon> lstDistrict = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DISTRICT_CD,DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_DISTRICT  WHERE 1=1 ";
                    if (zoneId.ConvertToString() != "")
                    {
                        cmdText += " and ZONE_CD='" + Convert.ToInt32(zoneId) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and DISTRICT_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and " + Utils.ToggleLanguage("upper(DESC_ENG)", "DESC_LOC") + " like '%" + Utils.ToggleLanguage(desc.ToUpper(), desc) + "%'";
                    }
                    cmdText += " order by to_number(DISTRICT_CD)";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstDistrict.Add(new MISCommon { Code = (drow["DISTRICT_CD"].ConvertToString()), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstDistrict = null;
            }
            return lstDistrict;
        }
        public List<MISCommon> GetDistrictbyZone(string ZoneCode)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                string cmdText = string.Empty;
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DISTRICT_CD,DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    if (ZoneCode != "")
                    {
                        cmdText += " AND ZONE_CD = '" + ZoneCode + "'";
                    }
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["DISTRICT_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception ex)
            {
                lstCommon = null;
                ExceptionManager.AppendLog(ex);
            }
            return lstCommon;
        }
        public static string GetZone(string districtCode)
        {

            string result = "";
            if (!String.IsNullOrWhiteSpace(districtCode))
            {
                DataTable dt = new DataTable();

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select ZONE_CD from MIS_DISTRICT where DISTRICT_CD='" + districtCode + "'";
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
                        result = dt.Rows[0][0].ConvertToString();
                }
            }
            return result;
        }
        public static string GetRegion(string zoneCode)
        {
            string result = "";
            if (!String.IsNullOrWhiteSpace(zoneCode))
            {
                DataTable dt = new DataTable();

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select REG_ST_CD from MIS_ZONE where ZONE_CD='" + zoneCode + "'";
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
                        result = dt.Rows[0][0].ConvertToString();
                }
            }
            return result;
        }
        public static string GetCountry(string regionCode)
        {
            string result = "";
            if (!String.IsNullOrWhiteSpace(regionCode))
            {
                DataTable dt = new DataTable();

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select COUNTRY_CD from MIS_REGION_STATE where REG_ST_CD='" + regionCode + "'";
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
                        result = dt.Rows[0][0].ConvertToString();
                }
            }
            return result;
        }
        public static List<MISCommon> GetDocumentTypebyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select DOC_TYPE_CD,DEFINED_CD, DESC_ENG DESCRIPTION FROM MIS_DOC_TYPE where 1=1";
                    }
                    else
                    {
                        cmdText = "select DOC_TYPE_CD,DEFINED_CD, DESC_LOC DESCRIPTION FROM MIS_DOC_TYPE where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and DOC_TYPE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public static List<MISCommon> GetRoofTypeByCode(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstRoofType = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    //string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    cmdText = "select MATERIAL_CD, Defined_Cd," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_ROOF_MATERIAL where 1=1";
                    if (code != "")
                    {
                        cmdText += " and MATERIAL_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '%" + desc.ToUpper() + "%'";
                    }
                    cmdText += " order by Defined_Cd ASC";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstRoofType.Add(new MISCommon { Code = drow["MATERIAL_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstRoofType = null;
            }
            return lstRoofType;
        }
        public List<MISCommon> GetRelationTypeByCodeAndDesc1(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstRelType = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    //string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    cmdText = "select RELATION_TYPE_CD, Defined_Cd," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_RELATION_TYPE where 1=1 and RELATION_TYPE_CD between 42 and 43";
                    if (code != "")
                    {
                        cmdText += " and RELATION_TYPE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '%" + desc.ToUpper() + "%'";
                    }
                    cmdText += " order by Defined_Cd ASC";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstRelType.Add(new MISCommon { Code = drow["RELATION_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstRelType = null;
            }
            return lstRelType;
        }
        public List<MISCommon> GetCatalogNumber(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstRelType = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select BUILDING_DEG_CAT_CD," + Utils.ToggleLanguage("BUIDING_DEG_CAT_DES_ENG", "BUILDING_DES_CAT_DES_LOC") + " DESCRIPTION FROM MIS_BUILDING_DESIGN_CATALOG where 1=1";
                    if (code != "")
                    {
                        cmdText += " and BUILDING_DEG_CAT_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and upper(" + Utils.ToggleLanguage("BUIDING_DEG_CAT_DES_ENG", "BUILDING_DES_CAT_DES_LOC") + ") like '%" + desc.ToUpper() + "%'";
                    }
                    cmdText += " order by BUILDING_DEG_CAT_CD ASC";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstRelType.Add(new MISCommon { Code = drow["BUILDING_DEG_CAT_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstRelType = null;
            }
            return lstRelType;
        }
        public List<MISCommon> GetRelationTypeByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstRelType = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    //string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    cmdText = "select RELATION_TYPE_CD, Defined_Cd," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_RELATION_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and RELATION_TYPE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '%" + desc.ToUpper() + "%'";
                    }
                    cmdText += " order by Defined_Cd ASC";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstRelType.Add(new MISCommon { Code = drow["RELATION_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstRelType = null;
            }
            return lstRelType;
        }
        public List<MISCommon> GetSchoolTypebyCodeandDesc()
        {
            DataTable dtbl = null;
            List<MISCommon> lstSchoolType = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select SCHOOL_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_SCHOOL_TYPE where 1=1";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstSchoolType.Add(new MISCommon { Code = drow["SCHOOL_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = (drow["DESCRIPTION"]).ConvertToString() });
                }
            }
            catch (Exception ex)
            {
                lstSchoolType = null;
                ExceptionManager.AppendLog(ex);
            }
            return lstSchoolType;
        }

        public List<MISCommon> GetWallMaterailByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MATERIAL_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_MATERIAL where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and MATERIAL_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MATERIAL_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetRoofMaterialByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MATERIAL_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_ROOF_MATERIAL where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and MATERIAL_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MATERIAL_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetWaterSourceByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select WATER_SOURCE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_WATER_SOURCE where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and WATER_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["WATER_SOURCE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetToiletTypeByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select TOILET_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_TOILET_TYPE where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and WATER_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["TOILET_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetLightSourceByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select LIGHT_SOURCE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_LIGHT_SOURCE where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and WATER_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["LIGHT_SOURCE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetFuelSourceByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select FUEL_SOURCE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_FUEL_SOURCE where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and FUEL_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["FUEL_SOURCE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetSocialIndicatorsIByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select INDICATOR_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_HOUSEHOLD_INDICATOR where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and WATER_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["INDICATOR_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetSocialIndicatorsIIByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select INDICATOR_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_HOUSEHOLD_INDICATOR where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and WATER_SOURCE_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["INDICATOR_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }


            return lstCommon;
        }

        public List<MISCommon> GetCurrencyByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select CURRENCY_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_CURRENCY where 1=1";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and CURRENCY_CD = " + code + "";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["CURRENCY_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetPositionSubClassByDesigByCodeAndDesc(string code, string desc, string desigCode)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select T1.POS_SUB_CLASS_CD,T1.DEFINED_CD," + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " as DESCRIPTION FROM MIS_POSITION_SUB_CLASS_MST T1 INNER JOIN MIS_POSITION_SUB_CLASS T2 ON T1.POS_SUB_CLASS_CD=T2.POS_SUB_CLASS_CD INNER JOIN MIS_DESIGNATION T3 ON T2.DESIGNATION_CD=T3.DESIGNATION_CD where 1=1";
                    cmdText += " and T3.DEFINED_CD = '" + desigCode + "'";
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and T1.DEFINED_CD LIKE '%" + code + "%'";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(T1.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and T1.DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["POS_SUB_CLASS_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetSectionByCodeAndDesc(string code, string desc, string officeCode)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select T1.SECTION_CD, T1.DEFINED_CD," + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " as DESCRIPTION FROM MIS_SECTION T1 INNER JOIN MIS_OFFICE T2 ON T1.OFFICE_CD=T2.OFFICE_CD where 1=1";
                    if (!string.IsNullOrWhiteSpace(officeCode))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and T2.DEFINED_CD = '" + officeCode + "'";
                    }
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        //cmdText += " and MATERIAL_CD like '%" + code + "%'";
                        cmdText += " and T1.DEFINED_CD LIKE '%" + code + "%'";
                    }
                    if (!string.IsNullOrWhiteSpace(desc))
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(T1.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and T1.DESC_LOC like '%" + desc + "%'";
                        }
                    }
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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["SECTION_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        internal List<MISCommon> GetHandiColor()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select Handi_Color_cd,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_HANDI_COLOR where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["Handi_Color_cd"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }


        public string GetCodeForDefinedCdTablenameAndCodeFieldName(string definedCd, string tableName, string codeFieldName)
        {
            string code = string.Empty;
            DataTable dt = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string queryStr = string.Format("SELECT PKG_TRANSACTION.FN_GET_CODFLDNAME('{0}','{1}','{2}') FROM DUAL", tableName, definedCd, codeFieldName);
                    dt = service.GetDataTable(queryStr, null);
                    if (dt != null && dt.Rows.Count > 0) code = dt.Rows[0][0].ConvertToString();

                    service.End();
                }
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return code;
        }

        public string GetFieldNameFromCode(string tableName, string Providefieldname, string reqiredFieldName, string ProvideValue)
        {
            string code = string.Empty;
            DataTable dt = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string queryStr = string.Format("SELECT com_fn_return_desc('{0}','{1}','{2}','{3}') FROM DUAL", tableName, Providefieldname, reqiredFieldName, ProvideValue);
                    dt = service.GetDataTable(queryStr, null);
                    if (dt != null && dt.Rows.Count > 0) code = dt.Rows[0][0].ConvertToString();

                    service.End();
                }
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionHandler.ExceptionManager.AppendLog(ex);
            }
            return code;
        }

        public List<MISCommon> GetClassType()
        {
            DataTable dtbl = null;
            List<MISCommon> lstClassType = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select CLASS_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_CLASS_TYPE where 1=1";
                    try
                    {
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
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstClassType.Add(new MISCommon { Code = drow["CLASS_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = (drow["DESCRIPTION"]).ConvertToString() });
                }
            }
            catch (Exception ex)
            {
                lstClassType = null;
                ExceptionManager.AppendLog(ex);
            }
            return lstClassType;
        }

        public List<MISCommon> GetBudget(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select BUDGET_CD,DEFINED_CD, DESC_ENG DESCRIPTION FROM PAS_BUDGET where 1=1";
                    }
                    else
                    {
                        cmdText = "select BUDGET_CD,DEFINED_CD, DESC_LOC DESCRIPTION FROM PAS_BUDGET where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        public List<MISCommon> GetEducationTargetBatch(string fiscalyear, string DistrictCd = "")
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "SELECT DISTINCT BATCH_ID FROM SAT_EDUCATION_TARGET WHERE 1=1";
                    if (!String.IsNullOrEmpty(fiscalyear))
                    {
                        cmdText += " AND FISCAL_YR='" + fiscalyear + "'";
                    }
                    if (!String.IsNullOrEmpty(DistrictCd))
                    {
                        cmdText += " AND PER_DISTRICT_CD='" + DistrictCd + "'";
                    }
                    cmdText += " ORDER BY BATCH_ID";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["BATCH_ID"].ConvertToString(), Description = drow["BATCH_ID"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetPMTMPICalBatch(string fiscalyear, string DistrictCd = "")
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "SELECT DISTINCT BATCH_ID FROM mis_pmt_mpi_calc_results WHERE 1=1";
                    if (!String.IsNullOrEmpty(fiscalyear))
                    {
                        cmdText += " AND FISCAL_YR='" + fiscalyear + "'";
                    }
                    if (!String.IsNullOrEmpty(DistrictCd))
                    {
                        cmdText += " AND DISTRICT_CD='" + DistrictCd + "'";
                    }
                    cmdText += " ORDER BY BATCH_ID";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["BATCH_ID"].ConvertToString(), Description = drow["BATCH_ID"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetFiscalYear(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select FISCAL_YR,SERIAL_NO FROM COM_FISCAL_YEAR where 1=1";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["FISCAL_YR"].ConvertToString(), Description = drow["FISCAL_YR"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetSAT(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_SOCIAL_ASSIST_TYPE where GROUP_FLAG='D'";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetSATHDSP(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_SOCIAL_ASSIST_TYPE where GROUP_FLAG='D' and ASSIST_TYPE_CD=" + ConstantVariables.EducationGrantSATID;
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetSATSSNP(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_SOCIAL_ASSIST_TYPE where GROUP_FLAG='D' and ASSIST_TYPE_CD!=" + ConstantVariables.EducationGrantSATID;
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetVitalRegistrationTypeByCodeAndDesc(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "SELECT REG_TYPE_CD, DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM VERS_REGISTRATION_TYPE WHERE 1 = 1 ";
                    if (code != "")
                    {
                        cmdText += " AND UPPER(DEFINED_CD) LIKE '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " AND UPPER(DESC_ENG) LIKE '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " AND LOWER(DESC_LOC) LIKE LOWER('%" + desc + "%')";
                        }
                    }
                    cmdText += " ORDER BY ORDER_NO";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["REG_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetServiceProvider(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_SERVICE_PROVIDER where GROUP_FLAG='D'";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetPeriodType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_PERIOD_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetPaymentCycle(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    try
                    {


                        service.Begin();
                        string resPackageName = service.PackageName;
                        service.PackageName = "PKG_NHRS_PAYMENT_SEARCH_GEN";
                        dtbl = service.GetDataTable(true, "PR_NHRS_GET_PAYMENT_CYCLE_LIST",
                            DBNull.Value);


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
                    if (dtbl.Rows.Count > 0)
                    {
                        foreach (DataRow drow in dtbl.Rows)
                        {
                            lstCommon.Add(new MISCommon { Code = drow["PAYROLL_CYCLE_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString() });

                        }
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;

        }


        public List<MISCommon> GetPeriodTypeHDSP(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select T1.DEFINED_CD DEFINED_CD," + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " DESCRIPTION FROM SAT_PERIOD_TYPE T1 JOIN SAT_RULE T2 ON T1.PERIOD_TYPE_CD=T2.PERIOD_TYPE_CD WHERE T2.ASSIST_TYPE_CD=" + ConstantVariables.EducationGrantSATID;
                    if (code != "")
                    {
                        cmdText += " AND upper(T1.DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " AND upper(T1.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " AND lower(T1.DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        public List<MISCommon> GetBankAccountType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION FROM NHRS_BANK_ACC_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public static List<MISCommon> GetDamageGrade(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DAMAGE_GRADE_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_DAMAGE_GRADE Where 1=1 and DAMAGE_GRADE_CD!='0' order by ORDER_NO";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DAMAGE_GRADE_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public static List<MISCommon> GetBuildingCondition(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select BUILDING_CONDITION_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_BUILDING_CONDITION Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BUILDING_CONDITION_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public static List<MISCommon> GetTechnicalSolution(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select TECHSOLUTION_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_TECHNICAL_SOLUTION  Where 1=1 and TECHSOLUTION_CD!='0' order by ORDER_NO";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["TECHSOLUTION_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetRecommendationType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select RECOMMENDATION_TYPE_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_RECOMMENDATION_TYPE  Where 1=1 order by ORDER_NO";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["RECOMMENDATION_TYPE_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        public List<MISCommon> GetTargetID(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select TARGET_BATCH_ID FROM NHRS_TARGET_BATCH where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["TARGET_BATCH_ID"].ConvertToString(), Description = drow["TARGET_BATCH_ID"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetBatchId(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select TARGET_BATCH_ID FROM NHRS_TARGET_BATCH where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = @Utils.GetLabel(drow["TARGET_BATCH_ID"].ConvertToString()), Description = drow["TARGET_BATCH_ID"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetGrievanceBatchId(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "Select DISTINCT BATCH_SERIAL_NO from NHRS_GRIEVANCE_TARGET_BATCH where 1=1 order by BATCH_SERIAL_NO ";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BATCH_SERIAL_NO"].ConvertToString(), Description = drow["BATCH_SERIAL_NO"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetInstallation(string code, string desc)
        {
            DataTable dtbl = null;
            PaymentProcess objPayProcess = new PaymentProcess();
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {

                Object p_payroll_installation_cd = DBNull.Value;
                Object p_desc_eng = DBNull.Value;
                Object p_desc_loc = DBNull.Value;


                if (!string.IsNullOrEmpty(objPayProcess.InstallationCd))
                {
                    p_payroll_installation_cd = objPayProcess.InstallationCd;
                }
                if (!string.IsNullOrEmpty(objPayProcess.Ins_eng))
                {
                    p_desc_eng = objPayProcess.Ins_eng;
                }
                if (!string.IsNullOrEmpty(objPayProcess.Ins_loc))
                {
                    p_desc_loc = objPayProcess.Ins_loc;
                }
                using (ServiceFactory service = new ServiceFactory())
                {
                    //service.Begin();
                    //cmdText = "select PAYROLL_INSTALL_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_PAYROLL_INSTALLMENT where 1=1";
                    //if (code != "")
                    //{
                    //    cmdText += " and upper(PAYROLL_INSTALL_CD) like '%" + code.ToUpper() + "%'";
                    //}
                    //if (desc != "")
                    //{
                    //    if (sessionLanguage == "English")
                    //    {
                    //        cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                    //    }
                    //    else
                    //    {
                    //        cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                    //    }
                    //}


                    try
                    {
                        service.Begin();
                        string resPackageName = service.PackageName;
                        service.PackageName = "PKG_NHRS_PAYMENT_SEARCH_GEN";
                        dtbl = service.GetDataTable(true, "PR_GET_PAYMENT_INSTALLMENT",
                            p_payroll_installation_cd,
                            p_desc_eng,
                            p_desc_loc,
                            DBNull.Value);
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
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["PAYROLL_INSTALL_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetBankName(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_BANK where 1=1";
                    //if (code != "")
                    //{
                    //    cmdText += " and upper(DEFINED_CD)='" + code.ToUpper() + "'";
                    //}
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetBankBranchId(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_BANK_BRANCH where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD)='" + code.ToUpper() + "'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public DataTable getOrderNoByOfficeCd(string OfficeCD)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT ORDER_NO FROM MIS_OFFICE where DEFINED_CD='" + OfficeCD + "'";
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
            }
            return dt;
        }
        public List<MISCommon> GetOfficeByAddressedBankCD(string orderNo, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_OFFICE Where UPPER_OFFICE_CD IS Null AND ORDER_NO <'" + orderNo + "' order by ORDER_NO";

                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetBankBranchIdByBankId(string VDCCode, string Wardno, string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_BANK_BRANCH where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(BANK_CD) ='" + code.ToUpper() + "'";
                    }
                    if (VDCCode != "")
                    {
                        cmdText += " and upper(VDC_MUN_CD)='" + VDCCode.ToUpper() + "'";
                    }
                    if (Wardno != "")
                    {
                        cmdText += " and upper(WARD_NO) ='" + Wardno.ToUpper() + "'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetBankByVDC(string VDC, string Ward, string District, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string cmdText1 = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    if (VDC != "" && VDC != null)
                    {
                        cmdText1 += "and VDC_MUN_CD='" + VDC + "'";
                    }
                    if (Ward != "" && Ward != null)
                    {
                        cmdText1 += "and WARD_NO='" + Ward + "'";
                    }
                    if (District != "" && District != null)
                    {
                        cmdText1 += "and DISTRICT_CD='" + District + "'";
                    }

                    cmdText = "select DISTINCT NB.DESC_ENG BANK_ENG,NB.DESC_LOC BANK_LOC, NB.DEFINED_CD BANK_CD FROM NHRS_BANK_BRANCH BB INNER JOIN NHRS_BANK NB on BB.BANK_CD=NB.BANK_CD where 1=1";
                    if (VDC != "" && VDC != null)
                    {
                        cmdText += " and upper(BB.VDC_MUN_CD) ='" + VDC.ToUpper() + "'";
                    }

                    if (Ward != "" && Ward != null)
                    {
                        cmdText += " and upper(BB.WARD_NO)='" + Ward.ToUpper() + "'";
                    }
                    if (District != "" && District != null)
                    {
                        cmdText += " and upper(BB.DISTRICT_CD) ='" + District.ToUpper() + "'";
                    }

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BANK_CD"].ConvertToString(), Description = @Utils.ToggleLanguage(drow["BANK_ENG"].ConvertToString(), drow["BANK_LOC"].ConvertToString()) });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetBatchByAddress(string Mode, string VDC, string Ward, string District, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string cmdText1 = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "Select DISTINCT Target_batch_id from nhrs_grievance_registration where 1=1 ";
                    if (VDC != "" && VDC != null)
                    {
                        cmdText += "and registration_vdc_mun_cd='" + VDC + "'";
                    }
                    if (District != "" && District != null)
                    {
                        cmdText += "and registration_dist_cd='" + District + "'";
                    }

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["Target_batch_id"].ConvertToString(), Description = @Utils.GetLabel(drow["Target_batch_id"].ConvertToString()) });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetBankByAddress(string Mode, string VDC, string Ward, string District, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string cmdText1 = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();



                    if (Mode == "I")
                    {
                        cmdText1 = "Select BANK_CD from NHRS_BANK_MAPPING where 1=1 ";
                        if (VDC != "" && VDC != null)
                        {
                            cmdText1 += "and VDC_MUN_CD='" + VDC + "'";
                        }
                        if (Ward != "" && Ward != null)
                        {
                            cmdText1 += "and WARD_NO='" + Ward + "'";
                        }
                        if (District != "" && District != null)
                        {
                            cmdText1 += "and DISTRICT_CD='" + District + "'";
                        }
                        cmdText = "select DISTINCT NB.DESC_ENG BANK_ENG,NB.DESC_LOC BANK_LOC, NB.DEFINED_CD BANK_CD FROM NHRS_BANK_BRANCH BB INNER JOIN NHRS_BANK NB on BB.BANK_CD=NB.BANK_CD where 1=1  and  BB.BANK_CD NOT IN (" + cmdText1 + ")";
                    }
                    else
                    {
                        cmdText = "select DISTINCT NB.DESC_ENG BANK_ENG,NB.DESC_LOC BANK_LOC, NB.DEFINED_CD BANK_CD FROM NHRS_BANK_BRANCH BB INNER JOIN NHRS_BANK NB on BB.BANK_CD=NB.BANK_CD where 1=1";
                    }
                    if (VDC != "" && VDC != null)
                    {
                        cmdText += " and upper(BB.VDC_MUN_CD) ='" + VDC.ToUpper() + "'";
                    }

                    if (Ward != "" && Ward != null)
                    {
                        cmdText += " and upper(BB.WARD_NO)='" + Ward.ToUpper() + "'";
                    }
                    if (District != "" && District != null)
                    {
                        cmdText += " and upper(BB.DISTRICT_CD) ='" + District.ToUpper() + "'";
                    }

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BANK_CD"].ConvertToString(), Description = @Utils.ToggleLanguage(drow["BANK_ENG"].ConvertToString(), drow["BANK_LOC"].ConvertToString()) });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetBankBranchIdByBankId(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_BANK_BRANCH where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(BANK_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetDistrictIDByBankBranchId(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DISTRICT_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetGroup(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "SELECT GRP_CD DEFINED_CD, GRP_NAME DESCRIPTION FROM COM_WEB_GRP WHERE 1=1";
                    if (code != "")
                    {
                        cmdText += " AND UPPER(GRP_CD) LIKE '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " AND UPPER(GRP_NAME) LIKE '%" + desc.ToUpper() + "%'";
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetFeedFormat(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_FEED_FORMAT where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetFeedFormatHDSP(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_FEED_FORMAT where FEED_FORMAT_TYPE_CD=2";//for reverse feed only
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetFeedFormatType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_FEED_FORMAT_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetServiceProviderType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM SAT_SERVICE_PROVIDER_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetAllowanceType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_ALLOWANCE_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetHumanLossType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select HUMANLOSS_TYPE_DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_HUMANLOSS_TYPE where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(HUMANLOSS_TYPE_DEFINED_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["HUMANLOSS_TYPE_DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetDeathReasonType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEATH_REASON_DEF_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_DEATH_REASON where 1=1";
                    if (code != "")
                    {
                        cmdText += " and upper(DEATH_REASON_DEF_CD) like '%" + code.ToUpper() + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEATH_REASON_DEF_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetAllTableName(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    //cmdText = "select table_name from user_tables where (table_name not like 'PAS%' AND table_name not like 'JN%' AND table_name not like 'MIS_RPT%' AND table_name not like 'TMP%' AND table_name not like 'COM%') order by table_name";
                    cmdText = "select table_name from user_tables where (table_name like 'MIS_%' and table_name not like 'MIS_RPT_%' and table_name not like 'MISLITE_%') or table_name like 'SAT_%' order by table_name";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["TABLE_NAME"].ConvertToString(), Description = drow["TABLE_NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetAllParticipantsType(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCRIPTION  from NHRS_PARTICIPANTS_TYPE ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetAllHouseOwnerColumns(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select COLUMN_name AS DESCRIPTION  FROM ALL_TAB_COLUMNS  where table_name='NHRS_HOUSE_OWNER_MST' ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DESCRIPTION"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetAllParticipants(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select PARTICIPANT_ID,DEFINED_CD," + Utils.ToggleLanguage("PARTICIPANT_FULL_NAME_ENG", "PARTICIPANT_FULL_NAME_LOC") + " DESCRIPTION from NHRS_TRAINING_PARTICIPANTS ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetAllEducation(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCRIPTION from MIS_CLASS_TYPE ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        public List<MISCommon> GetAllTrainingEducation(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCRIPTION from MIS_TRAINING_CLASS_TYPE ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public static List<MISCommon> GetAllEthnicity(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCRIPTION from MIS_CASTE ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> getTrainingBatch(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select BATCH_CD,DEFINED_CD," + Utils.ToggleLanguage("TRAINING_BATCH_NAME_ENG", "TRAINING_BATCH_NAME_LOC") + " DESCRIPTION from NHRS_TRAINING_BATCH ";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetAllColumnName(string TableName, string ColumnName)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select COLUMN_NAME from dba_tab_cols where owner = 'NHRS' AND TABLE_NAME = '" + TableName + "' AND COLUMN_NAME='" + ColumnName + "' order by COLUMN_NAME asc ";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                lstCommon.Add(new MISCommon { Code = "", Description = "--" + Utils.GetLabel("Select DB Column") + "--" });
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["COLUMN_NAME"].ConvertToString(), Description = drow["COLUMN_NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetAllColumnName(string TableName)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select COLUMN_NAME from dba_tab_cols where owner = 'NHRS' AND TABLE_NAME = '" + TableName + "' order by COLUMN_NAME asc ";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                lstCommon.Add(new MISCommon { Code = "", Description = "--" + Utils.GetLabel("Select DB Column") + "--" });
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["COLUMN_NAME"].ConvertToString(), Description = drow["COLUMN_NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> getDistrictByBatch(string BatchID)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select TBD.DISTRICT_CD,DS.DESC_ENG from NHRS_TARGET_BATCH TBD INNER JOIN MIS_DISTRICT DS ON TBD.DISTRICT_CD =DS.DISTRICT_CD where TBD.TARGET_BATCH_ID = '" + BatchID + "' order by TBD.TARGET_BATCH_ID asc ";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DISTRICT_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> getBatchIDByDistrict(string DistrictID)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select TBD.DISTRICT_CD,TBD.BATCH_SERIAL_NO from NHRS_GRIEVANCE_TARGET_BATCH TBD where TBD.DISTRICT_CD = '" + DistrictID + "' order by TBD.BATCH_SERIAL_NO asc ";

                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BATCH_SERIAL_NO"].ConvertToString(), Description = drow["BATCH_SERIAL_NO"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        #region Vital Registration
        public List<MISCommon> GetRegistrationAddress()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select REGISTRATION_ADD_CD, DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_REGISTRATION_ADDRESS where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["REGISTRATION_ADD_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetSpecialCircumstances()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select SPECIAL_CIRCUMSTANCE_CD, DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_SPECIAL_CIRCUMSTANCE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["SPECIAL_CIRCUMSTANCE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetBirthHelper()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select BIRTH_HELPER_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_BIRTH_HELPER where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["BIRTH_HELPER_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetDeathSite()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select DEATH_PLACE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_DEATH_PLACE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["DEATH_PLACE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetBirthSite()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select BIRTH_SITE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_BIRTH_SITE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["BIRTH_SITE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetBirthType()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select BIRTH_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_BIRTH_TYPE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["BIRTH_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetOccupation()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select OCCUPATION_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM MIS_OCCUPATION where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["OCCUPATION_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetMotherTongue()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MOTHER_TONGUE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_MOTHER_TONGUE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MOTHER_TONGUE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetMarriageType()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MARRIAGE_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_MARRIAGE_TYPE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MARRIAGE_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetCourtType()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select COURT_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_COURT_TYPE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["COURT_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetRegistrationType()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select REG_TYPE_CD, DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_REGISTRATION_TYPE where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["REG_TYPE_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }

        public List<MISCommon> GetMigrationReason()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmdText = "select MIGRANT_REASON_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " as DESCRIPTION FROM VERS_MIGRANT_REASON where 1=1";

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
                    lstCommon = new List<MISCommon>();
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

            foreach (DataRow drow in dtbl.Rows)
            {
                lstCommon.Add(new MISCommon { Code = drow["MIGRANT_REASON_CD"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
            }
            return lstCommon;
        }
        #endregion

        #region Get Names From Code
        public DataTable GetZoneAndRegion(string parameter, string definecode)
        {
            DataTable dt = null;
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "Select '" + parameter + "' from COM_WEB_USR where  ";
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

                return dt;
            }
        }
        //public static List<MISCommon> GetMaritalStatus(string code, string desc)
        //{
        //    DataTable dtbl = null;
        //    List<MISCommon> lstCommon = new List<MISCommon>();
        //    string cmdText = "";
        //    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
        //    try
        //    {
        //        using (ServiceFactory service = new ServiceFactory())
        //        {
        //            service.Begin();
        //            cmdText = "select MARITAL_STATUS_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM MIS_MARITAL_STATUS  Where 1=1  order by ORDER_NO";


        //            try
        //            {
        //                dtbl = service.GetDataTable(new
        //                {
        //                    query = cmdText,
        //                    args = new
        //                    {

        //                    }
        //                });
        //            }
        //            catch (Exception)
        //            {

        //            }
        //            finally
        //            {
        //                if (service.Transaction != null)
        //                {
        //                    service.End();
        //                }
        //            }
        //        }
        //        if (dtbl != null && dtbl.Rows.Count > 0)
        //        {
        //            foreach (DataRow drow in dtbl.Rows)
        //            {
        //                lstCommon.Add(new MISCommon { Code = drow["MARITAL_STATUS_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        lstCommon = null;
        //    }
        //    return lstCommon;
        //}

        //public DataTable GetDistrictName(string strDistrict)
        //{
        //    DataTable dt = null;
        //    string cmdText = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "Select " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " from MIS_DISTRICT where DISTRICT_CD='" + strDistrict + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //        return dt;
        //    }
        //}

        //public DataTable GetVDCMunName(string strVDCMun)
        //{
        //    DataTable dt = null;
        //    string cmdText = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "Select " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " from MIS_VDC_MUNICIPALITY where VDC_MUN_CD='" + strVDCMun + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //        return dt;
        //    }
        //}

        //public DataTable GetCasteName(string strCasteCode)
        //{
        //    DataTable dt = null;
        //    string cmdText = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "Select " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " from MIS_CASTE where CASTE_CD='" + strCasteCode + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //        return dt;
        //    }
        //}



        //public DataTable GetMaritalStatusName(string strMaritalStatusCode)
        //{
        //    DataTable dt = null;
        //    string cmdText = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "Select " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " from MIS_MARITAL_STATUS where MARITAL_STATUS_CD='" + strMaritalStatusCode + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //        return dt;
        //    }
        //}

        //public DataTable GetReligionName(string strReligionCode)
        //{
        //    DataTable dt = null;
        //    string cmdText = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "Select " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " from MIS_RELIGION where RELIGION_CD='" + strReligionCode + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }

        //        return dt;
        //    }
        //}
        #endregion

        #region Adding for BudgetAllocation - Pratik
        public List<MISCommon> GetLineItems()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select LINE_ITEM_CD,DEFINED_CD, DESC_ENG, DESC_LOC FROM PAS_LINE_ITEM where GROUP_FLAG = 'D'";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["LINE_ITEM_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), DescriptionLoc = drow["DESC_LOC"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetDonors()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select DONOR_CD, DEFINED_CD, DESC_ENG, DESC_LOC FROM PAS_DONOR where 1=1";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["DONOR_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), DescriptionLoc = drow["DESC_LOC"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public List<MISCommon> GetSourceTypes()
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select SOURCE_CD, DEFINED_CD, DESC_ENG, DESC_LOC FROM PAS_SOURCE where GROUP_FLAG = 'D'";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }

                foreach (DataRow drow in dtbl.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = drow["SOURCE_CD"].ConvertToString(), Description = drow["DESC_ENG"].ConvertToString(), DefinedCode = drow["DEFINED_CD"].ConvertToString(), DescriptionLoc = drow["DESC_LOC"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        #endregion

        #region Adding for SSNP - Pratik
        public List<MISCommon> GetPeriodTypeSSNP(string satCode)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DES FROM SAT_PERIOD_TYPE T1 where PERIOD_TYPE_CD in ( select PERIOD_TYPE_CD from SAT_RULE where assist_type_cd = '" + satCode + "')";
                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DES"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        #endregion






        public DataTable GetBankCdByDistrictCd(string DistrictCd)
        {
            List<MISCommon> lstCommon = new List<MISCommon>();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_PAYMENT_SEARCH_GEN";
                    dt = service.GetDataTable(true, "PR_NHRS_GET_PAYROLL_BANK_LIST", DistrictCd.ToDecimal(), DBNull.Value);



                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
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

            }
            return dt;
        }

        //get curriculum
        public List<MISCommon> GetCurriculum()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT CURRICULUM_ID, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_CURRICULUM";
                    }
                    else
                    {
                        cmdText = "SELECT CURRICULUM_ID, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_CURRICULUM";
                    }
                    cmdText += " ORDER BY TO_NUMBER(CURRICULUM_ID)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["CURRICULUM_ID"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        //get implementing partner
        public List<MISCommon> GetImplementingPartner()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT IMPLEMENTING_PARTNER_ID, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_IMPLEMENTING_PARTNER";
                    }
                    else
                    {
                        cmdText = "SELECT IMPLEMENTING_PARTNER_ID, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_IMPLEMENTING_PARTNER";
                    }
                    cmdText += " ORDER BY TO_NUMBER(IMPLEMENTING_PARTNER_ID)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["IMPLEMENTING_PARTNER_ID"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        //participantsType
        public List<MISCommon> GetParticipantsType()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT PARTICIPANT_TYPE_CD, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_PARTICIPANTS_TYPE";
                    }
                    else
                    {
                        cmdText = "SELECT PARTICIPANT_TYPE_CD, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_PARTICIPANTS_TYPE";
                    }
                    cmdText += " ORDER BY TO_NUMBER(PARTICIPANT_TYPE_CD)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["PARTICIPANT_TYPE_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        //trainers type
        public List<MISCommon> GetTrainersType()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT TRAINERS_TYPE_CD, DEFINED_CD, DESC_ENG DESCRIPTION FROM NHRS_TRAINERS_TYPE";
                    }
                    else
                    {
                        cmdText = "SELECT TRAINERS_TYPE_CD, DEFINED_CD, DESC_LOC DESCRIPTION FROM NHRS_TRAINERS_TYPE";
                    }
                    cmdText += " ORDER BY TO_NUMBER(TRAINERS_TYPE_CD)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["TRAINERS_TYPE_CD"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        //house model 
        public List<MISCommon> GetHoueModel()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT MODEL_ID, DEFINED_CD, NAME_ENG DESCRIPTION FROM NHRS_HOUSE_MODEL where APPROVED='Y'";
                    }
                    else
                    {
                        cmdText = "SELECT MODEL_ID, DEFINED_CD, NAME_LOC DESCRIPTION FROM NHRS_HOUSE_MODEL where APPROVED='Y'";
                    }
                    cmdText += " ORDER BY TO_NUMBER(MODEL_ID)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["MODEL_ID"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        //house design for inspection 1
        public List<MISCommon> GetHoueModelForInspectionOne()
        {
            lstCommon = new List<MISCommon>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "SELECT MODEL_ID, DEFINED_CD, NAME_ENG DESCRIPTION FROM NHRS_HOUSE_MODEL where APPROVED='Y' AND CODE_LOC='1' AND DESIGN_NUMBER!='20'";
                    }
                    else
                    {
                        cmdText = "SELECT MODEL_ID, DEFINED_CD, NAME_LOC DESCRIPTION FROM NHRS_HOUSE_MODEL where APPROVED='Y'AND CODE_LOC='1' AND DESIGN_NUMBER!='20'";
                    }
                    cmdText += " ORDER BY TO_NUMBER(MODEL_ID)";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new { }
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
                }
                foreach (DataRow dr in dt.Rows)
                {
                    lstCommon.Add(new MISCommon { Code = dr["MODEL_ID"].ConvertToString(), DefinedCode = dr["DEFINED_CD"].ConvertToString(), Description = dr["DESCRIPTION"].ConvertToString() });
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        //base construction material
        public List<MISCommon> GetInspectionBaseMaterial(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select CONSTRUCTION_MAT_CD, " + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + " AS NAME "
                    + "FROM NHRS_CONSTRUCTION_MATERIAL where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["CONSTRUCTION_MAT_CD"].ConvertToString(), Description = drow["NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        //inspection roof material
        public List<MISCommon> GetInspectionRoofMaterial(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select RC_MAT_CD, " + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + " AS NAME "
                    + "FROM NHRS_RC_MATERIAL where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["RC_MAT_CD"].ConvertToString(), Description = drow["NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        //house legal owner 

        public static List<MISCommon> GetHouseLegalOwner(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select HOUSE_LAND_LEGAL_OWNERCD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_HOUSE_LAND_LEGAL_OWNER Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["HOUSE_LAND_LEGAL_OWNERCD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // Foundation type
        public static List<MISCommon> GetFoundationType(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select FOUNDATION_TYPE_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_FOUNDATION_TYPE Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["FOUNDATION_TYPE_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        // Floor material
        public static List<MISCommon> GetFloorConstructionMaterial(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select FC_MATERIAL_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_FLOOR_CONSTRUCT_MATERIAL Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["FC_MATERIAL_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // Building  material
        public static List<MISCommon> GetBuildingmaterial(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select SC_MATERIAL_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_STOREY_CONSRUCT_MATERIAL Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["SC_MATERIAL_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        // Building  Position
        public static List<MISCommon> GetBuildingPosition(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select BUILDING_POSITION_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_BUILDING_POSITION_CONFIG Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BUILDING_POSITION_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // Building  Plan configuation
        public static List<MISCommon> GetBuildinPlanConfig(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select BUILDING_PLAN_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_BUILDING_PLAN_CONFIG Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["BUILDING_PLAN_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // Building  ASSESSED AREA
        public static List<MISCommon> GetBuildingAssessedArea(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select ASSESSED_AREA_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_ASSESSED_AREA Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["ASSESSED_AREA_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // shelter after earth quake
        public static List<MISCommon> getShelterAfterEq(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select SHELTER_SINCE_QUAKE_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_SHELTER_SINCE_QUAKE Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["SHELTER_SINCE_QUAKE_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // shelter before earth quake
        public static List<MISCommon> getShelterBeforeEq(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select SHELTER_BEFORE_QUAKE_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_SHELTER_BEFORE_QUAKE Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["SHELTER_BEFORE_QUAKE_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // monthly income
        public static List<MISCommon> getMonthlyIncome(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select MONTHLY_INCOME_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_MONTHLY_INCOME Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["MONTHLY_INCOME_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        // MOUDB approved inspection batch
        public List<MISCommon> getMOUDBatch(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DISTINCT APPROVE_BATCH    FROM NHRS_INSPECTION_PAPER_DTL Where APPROVE_BATCH IS NOT NULL ORDER BY APPROVE_BATCH";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["APPROVE_BATCH"].ConvertToString(), Description = drow["APPROVE_BATCH"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        // MOFALD approved inspection batch
        public List<MISCommon> getMOFALDBatch(string code, string desc)
        {
            DataTable dtbl = null;
            lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DISTINCT APPROVE_BATCH_2    FROM NHRS_INSPECTION_PAPER_DTL Where APPROVE_BATCH_2 IS NOT NULL ORDER BY APPROVE_BATCH_2";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["APPROVE_BATCH_2"].ConvertToString(), Description = drow["APPROVE_BATCH_2"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public static List<MISCommon> getSuperStructure(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select SUPERSTRUCTURE_MATERIAL_CD ," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + "  DESCRIPTION   FROM NHRS_SUPERSTRUCTURE_MATERIAL Where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["SUPERSTRUCTURE_MATERIAL_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }


        //public List<MISCommon> GetBankCdByDistrictCd(string code, string desc)
        //{
        //    DataSet dtbl = null;
        //    lstCommon = new List<MISCommon>();
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        try
        //        {
        //            service.Begin();
        //            service.PackageName = "PKG_NHRS_PAYMENT_SEARCH_GEN";
        //            dtbl = service.GetDataSetOracle(true, "GET_BANK", code.ToDecimal(), DBNull.Value, DBNull.Value, DBNull.Value);


        //            if (dtbl != null && dtbl.Rows.Count > 0)
        //            {
        //                foreach (DataRow drow in dtbl.Rows)
        //                {
        //                    lstCommon.Add(new MISCommon { Code = drow["DEFINED_CD"].ConvertToString(), Description = drow["DESCRIPTION"].ConvertToString() });
        //                }
        //            }

        //        }
        //        catch (Exception)
        //        {
        //            lstCommon = null;
        //        }
        //        return lstCommon;
        //    }

        //}

        public DataTable SearchResult(AdhocHouseOwnerReportModel objModel)
        {

            DataTable dt = null;
            Adhoc_ReportSearchEntity objReportSearchEntity = new Adhoc_ReportSearchEntity();

            ////object p_lang = DBNull.Value;
            ////Object p_district = DBNull.Value;
            ////Object p_vdcmun = DBNull.Value;
            ////Object p_ward = DBNull.Value;
            ////Object p_gndr = DBNull.Value;
            // string p_topbldngcondtnn;
            // string p_area;
            // string p_benfflag;
            // string p_grievrgstrdflag;
            // string p_otherhousestop;
            // string p_ownercntindicator;
            // string p_ownercnt;
            // string p_gndr;
            // string p_isrespond;
            // string p_housewithinEC_Inidcator;
            // string p_housewithinEC;
            // string p_houseoutofEC_Indicator;
            // string p_houseoutofEC;
            //// string p_phnflag;
            // string p_LglOwner;
            // string p_BldngCondtn;
            // string p_StoreyBeforEQ;
            // string p_StoreyAfterEQ;
            // string p_fundntype;
            // string p_roofmtrl;
            // string p_flrmtrl;
            // string p_bldngmatrl;
            // string p_bldngpstn;
            // string p_plnconfig;
            // string p_geograpcrisk;
            // string p_inspctdpart;
            // string p_dmggrade;
            // string p_tchncalsoln;
            // string p_reconstrctnstrt;
            // string p_othruse;
            // string p_membercntafterEQ_Indicator;
            // string p_membercntafterEQ;
            // string p_idntfcationtyp;
            // string p_idntfcationissudist;
            // string p_ethncty;
            // string p_eductn;
            // string p_havnakacunt;
            // string p_shltraftreq;
            // string p_shltrbfreeq;
            // string p_currntplace;
            // string p_haveeqvctmcard;
            // string p_mnthlyincme;
            // string p_phno;
            // string p_deathCnt_Indicator;
            // string p_deathCnt;





            using (ServiceFactory service = new ServiceFactory())
            {
                //objReportSearchEntity.District_CD = objModel.Districtcd.ToDecimal();
                //objReportSearchEntity.VDC_MUN_CD = objModel.VDCMun.ToDecimal();
                //objReportSearchEntity.Ward = objModel.Ward.ToDecimal();
                objReportSearchEntity.Gender_CD = objModel.gndr.ToString();
                objReportSearchEntity.Damage_Grade = objModel.dmggrade.ToString();
                objReportSearchEntity.SuperStruct_CD = objModel.superstructure.ToString();
                objReportSearchEntity.Technical_soln_CD = objModel.tchncalsoln.ToString();
                objReportSearchEntity.HouseOwner = objModel.houseownerdropdown;
                objReportSearchEntity.BuildingStructure = objModel.buildingstructure;

                ////p_lang = objModel.lang.ToString(); 
                ////p_district = objModel.Districtcd.ToDecimal();
                ////p_vdcmun=objModel.VDCMun.ToDecimal();
                ////p_ward = objModel.Ward.ToDecimal();
                //p_topbldngcondtnn = objModel.topbldngcondtnn;
                //p_area=objModel.area;
                //p_benfflag=objModel.benfflag;
                //p_grievrgstrdflag=objModel.grievrgstrdflag;
                //p_otherhousestop = objModel.othrhousestop;
                //p_ownercntindicator = objModel.ownercount_Indicator;
                //p_ownercnt = objModel.ownercount;
                ////p_gndr = objModel.gndr.ToDecimal();
                // p_isrespond = objModel.isrespond;
                // p_housewithinEC_Inidcator=objModel.housewithinEC_Inidcator;
                // p_housewithinEC=objModel.housewithinEC;
                // p_houseoutofEC_Indicator=objModel.houseoutofEC_Indicator;
                // p_houseoutofEC = objModel.houseoutofEC;
                //// p_phnflag=objModel.phnflag;
                // p_LglOwner=objModel.LglOwner;
                // p_BldngCondtn=objModel.BldngCondtn;
                // p_StoreyBeforEQ=objModel.StoreyBeforEQ;
                // p_StoreyAfterEQ=objModel.StoreyAfterEQ;
                // p_fundntype=objModel.fundntype;
                // p_roofmtrl=objModel.roofmtrl;
                // p_flrmtrl=objModel.flrmtrl;
                // p_bldngmatrl=objModel.bldngmatrl;
                // p_bldngpstn=objModel.bldngpstn;
                // p_plnconfig=objModel.plnconfig;
                // p_geograpcrisk=objModel.geograpcrisk;
                // p_inspctdpart=objModel.inspctdpart;
                // p_dmggrade=objModel.dmggrade;
                // p_tchncalsoln=objModel.tchncalsoln;
                // p_reconstrctnstrt=objModel.reconstrctnstrt;
                // p_othruse=objModel.othruse;
                // p_membercntafterEQ_Indicator=objModel.membercntafterEQ_Indicator;
                // p_membercntafterEQ=objModel.membercntafterEQ;
                // p_idntfcationtyp=objModel.idntfcationtyp;
                // p_idntfcationissudist=objModel.idntfcationissudist;
                // p_ethncty=objModel.ethncty;
                // p_eductn=objModel.eductn;
                // p_havnakacunt=objModel.havnakacunt;
                // p_shltraftreq=objModel.shltraftreq;
                // p_shltrbfreeq=objModel.shltrbfreeq;
                // p_currntplace=objModel.currntplace;
                // p_haveeqvctmcard=objModel.haveeqvctmcard;
                // p_mnthlyincme=objModel.mnthlyincme;
                // p_phno=objModel.phno;
                // p_deathCnt_Indicator=objModel.deathCnt_Indicator;
                // p_deathCnt=objModel.deathCnt;



                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "pr_hhold_dmg_sty_techS_supS",

                         objReportSearchEntity.District_CD,
                         objReportSearchEntity.VDC_MUN_CD,
                          objReportSearchEntity.Ward,
                            objReportSearchEntity.Gender_CD,
                            objReportSearchEntity.SuperStruct_CD,
                             objReportSearchEntity.Damage_Grade,
                          objReportSearchEntity.Technical_soln_CD,
                                DBNull.Value


                    ////p_lang,
                        ////  p_district,
                        ////  p_vdcmun,
                        ////  p_ward,
                        //p_topbldngcondtnn,
                        //p_area,
                        //p_benfflag,
                        //p_grievrgstrdflag,
                        //p_otherhousestop,
                        //p_ownercntindicator,
                        //p_ownercnt,
                        ////p_gndr,
                        ////DBNull.Value
                        //p_isrespond,
                        //p_housewithinEC_Inidcator,
                        //p_housewithinEC,
                        //p_houseoutofEC_Indicator,
                        //p_houseoutofEC,
                        ////p_phnflag,
                        //p_LglOwner,
                        //p_BldngCondtn,
                        //p_StoreyBeforEQ,
                        //p_StoreyAfterEQ,
                        //p_fundntype,
                        //p_roofmtrl,
                        //p_flrmtrl,
                        //p_bldngmatrl,
                        //p_bldngpstn,
                        //p_plnconfig,
                        //p_geograpcrisk,
                        //p_inspctdpart,
                        //p_dmggrade,
                        //p_tchncalsoln,
                        //p_reconstrctnstrt,
                        //p_othruse,
                        //p_membercntafterEQ_Indicator,
                        //p_membercntafterEQ,
                        //p_idntfcationtyp,
                        //p_idntfcationissudist,
                        //p_ethncty,
                        //p_eductn,
                        //p_havnakacunt,
                        //p_shltraftreq,
                        //p_shltrbfreeq,
                        //p_currntplace,
                        //p_haveeqvctmcard,
                        //p_mnthlyincme,
                        //p_phno,
                        //p_deathCnt_Indicator,
                        //p_deathCnt
                   );
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
            }
            return dt;
        }

        public DataTable SearchResults(String finalquery)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                 try
                {
                    service.Begin();
                    DataTable dtbl = service.GetDataTable(new
                    {
                        query = finalquery,
                        args = new
                        {
                        }
                    });
                    dt = dtbl;
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {

                    if (service.Transaction != null && service.Transaction.Connection != null)
                    {
                        service.End();
                    }
                }

            }
            return dt;
        }

    }
}
