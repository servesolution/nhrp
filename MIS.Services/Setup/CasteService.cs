using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Setup;
using System.Data;
using System.Web;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;
namespace MIS.Services.Setup
{
    public class CasteService
    {
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = new DataTable();
            DataTable MaxCode = new DataTable();
            dt = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select nvl(max(to_number(CASTE_CD)),0)+1 from MIS_CASTE";
                    try
                    {
                        MaxCode = service.GetDataTable(new
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
                    if (MaxCode != null)
                    {
                        dt = MaxCode;
                    }
                    result = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }

        public void ManageCaste(MISCaste Caste, out string exc)
        {
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                MisCasteInfo obj = new MisCasteInfo();
                if (Caste.Mode == "D")
                {
                    obj.CasteCd = Caste.CasteCd;
                    obj.DefinedCd = null;
                    obj.DescEng = null;
                    obj.DescLoc = null;
                    obj.ShortName = null;
                    obj.ShortNameLoc = null;
                    obj.Approved = false;
                    obj.Disabled = false;
                    obj.GroupFlag = null;
                    obj.ApprovedBy = null;
                    obj.ApprovedDt = DateTime.Now.ToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = null;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDt = DateTime.Now.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = null;
                    obj.Mode = Caste.Mode;
                    obj.IPAddress = CommonVariables.IPAddress;
                }
                else if (Caste.Mode == "A")
                {
                    obj.CasteCd = Caste.CasteCd;
                    obj.Approved = Caste.Approved;
                    obj.ApprovedBy = Caste.ApprovedBy;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.Mode = Caste.Mode;
                }
                else
                {
                    obj.CasteCd = Caste.CasteCd;
                    obj.DefinedCd = Caste.DefinedCd;
                    obj.DescEng = Utils.ConvertToString(Caste.DescEng.ToUpper());
                    obj.DescLoc = Utils.ConvertToString(Caste.DescLoc);
                    if (Caste.ShortName == null)
                    {
                        obj.ShortName = Caste.ShortName;
                    }
                    else
                    {
                        obj.ShortName = Caste.ShortName.ToUpper();
                    }
                    obj.ShortNameLoc = Utils.ConvertToString(Caste.ShortNameLoc);
                    obj.GroupFlag = Caste.GroupFlag;
                    obj.CasteGroupCd = Caste.CasteGroupCd;
                    obj.Approved = Caste.Approved;
                    obj.Disabled = Caste.Disabled;
                    obj.ApprovedBy = Caste.ApprovedBy;
                    obj.ApprovedDt = Caste.ApprovedDt.ToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = Caste.ApprovedDtLoc;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDt = Caste.EnteredDt.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = Caste.EnteredDtLoc;
                    obj.IPAddress = CommonVariables.IPAddress;
                    obj.Mode = Caste.Mode;
                }
                service.PackageName = "PKG_SETUP";              
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    exc = ex.ToString();
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
        }

        public DataTable Caste_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {                
                if (sessionLanguage == "English")
                {
                    cmdText = "select T1.CASTE_CD,T1.DEFINED_CD,T1.DESC_ENG DESCRIPTION,T1.SHORT_NAME,T2.DESC_ENG CASTE_GROUP,T1.ORDER_NO,T1.GROUP_FLAG,T1.DISABLED,T1.APPROVED,T1.APPROVED_BY,T1.APPROVED_DT,T1.ENTERED_BY,T1.ENTERED_DT from MIS_CASTE T1 LEFT OUTER JOIN MIS_CASTE_GROUP T2 ON T1.CASTE_GROUP_CD=T2.CASTE_GROUP_CD";
                }
                else
                {
                    cmdText = "select T1.CASTE_CD,T1.DEFINED_CD,T1.DESC_LOC DESCRIPTION,T1.SHORT_NAME_LOC SHORT_NAME,T2.DESC_LOC CASTE_GROUP,T1.ORDER_NO,T1.GROUP_FLAG,T1.DISABLED,T1.APPROVED,T1.APPROVED_BY,T1.APPROVED_DT,T1.ENTERED_BY,T1.ENTERED_DT from MIS_CASTE T1 LEFT OUTER JOIN MIS_CASTE_GROUP T2 ON T1.CASTE_GROUP_CD=T2.CASTE_GROUP_CD";
                }
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    if (sessionLanguage == "English")
                    {
                        cmdText += String.Format(" where Upper(T1.DESC_ENG) like '{0}%'", initialStr);
                    }
                    else
                    {
                        cmdText += String.Format(" where Upper(T1.DESC_LOC) like '{0}%'", initialStr);
                    }
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "T1.DEFINED_CD")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }

                }
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dtbl;
        }

        public bool CheckDuplicateDefinedCode(string DefinedCd, string code)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                
                DataTable dtbl = new DataTable();
                string cmdText = string.Empty;
                if (code == string.Empty)
                {
                    cmdText = "select * from MIS_CASTE where DEFINED_CD ='" + DefinedCd + "'";
                }
                else
                {
                    cmdText = "select * from MIS_CASTE where DEFINED_CD ='" + DefinedCd + "' and CASTE_CD !='" + code + "'";
                }
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }

        public MISCaste FillCaste(string CasteCd)
        {
            MISCaste obj = new MISCaste();
            DataTable dTable = new DataTable();
            DataTable dt = new DataTable();
            dTable = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select CASTE_CD, DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC,DEFINED_CD,GROUP_FLAG,CASTE_GROUP_CD from MIS_CASTE where CASTE_CD = '" + CasteCd + "'";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
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
                    dTable = dt;
                    if (dTable.Rows.Count > 0)
                    {
                        obj.CasteCd = Convert.ToInt32(dTable.Rows[0]["CASTE_CD"]);
                        obj.DescEng = dTable.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.DescLoc = dTable.Rows[0]["DESC_LOC"].ConvertToString();
                        obj.ShortName = dTable.Rows[0]["SHORT_NAME"].ConvertToString();
                        obj.ShortNameLoc = dTable.Rows[0]["SHORT_NAME_LOC"].ConvertToString();
                        obj.DefinedCd = dTable.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.GroupFlag = dTable.Rows[0]["GROUP_FLAG"].ConvertToString();
                        if ((dTable.Rows[0]["CASTE_GROUP_CD"].ConvertToString()) != string.Empty)
                        {
                            obj.CasteGroupCd = Convert.ToDecimal(dTable.Rows[0]["CASTE_GROUP_CD"].ConvertToString());
                        }
                    }
                }
                catch (Exception)
                {
                    obj = null;
                }
            }
            return obj;
        }
    }
}