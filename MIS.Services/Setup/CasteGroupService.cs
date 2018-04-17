using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data;
using EntityFramework;
using ExceptionHandler;
using MIS.Models.Setup;
using System.Data.OracleClient;
using System.Web;

namespace MIS.Services.Setup
{
    public class CasteGroupService
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
                    string cmdText = "select nvl(max(to_number(CASTE_GROUP_CD)),0)+1 from MIS_CASTE_GROUP";
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

        public void ManageCasteGroup(MISCasteGroup CasteGroup)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                MisCasteGroupInfo obj = new MisCasteGroupInfo();
                if (CasteGroup.Mode == "D")
                {
                    obj.CasteGroupCd = CasteGroup.casteGroupCd;
                    obj.DefinedCd = null;
                    obj.DescEng = null;
                    obj.DescLoc = null;
                    obj.ShortName = null;
                    obj.ShortNameLoc = null;
                    obj.Approved = false;
                    obj.Disabled = false;
                    obj.ApprovedBy = null;
                    obj.ApprovedDt = DateTime.Now.ToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = null;
                    obj.EnteredBy =SessionCheck.getSessionUsername();
                    obj.EnteredDt = DateTime.Now.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = null;
                    obj.Mode = CasteGroup.Mode;
                    obj.IPAddress = CommonVariables.IPAddress;
                }
                else if (CasteGroup.Mode == "A")
                {
                    obj.CasteGroupCd = CasteGroup.casteGroupCd;
                    obj.Approved = CasteGroup.approved;
                    obj.ApprovedBy = CasteGroup.approvedBy;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.Mode = CasteGroup.Mode;
                }
                else
                {
                    obj.CasteGroupCd = CasteGroup.casteGroupCd;
                    obj.DefinedCd = CasteGroup.definedCd;
                    obj.DescEng = Utils.ConvertToString(CasteGroup.descEng.ToUpper());
                    obj.DescLoc = Utils.ConvertToString(CasteGroup.descLoc);
                    if (CasteGroup.shortName == null)
                    {
                        obj.ShortName = CasteGroup.shortName;
                    }
                    else
                    {
                        obj.ShortName = CasteGroup.shortName.ToUpper();
                    }
                    obj.ShortNameLoc = Utils.ConvertToString(CasteGroup.shortNameLoc);
                    obj.CasteGroupCd = CasteGroup.casteGroupCd;
                    obj.Approved = CasteGroup.approved;
                    obj.Disabled = CasteGroup.disabled;
                    obj.ApprovedBy = CasteGroup.approvedBy;
                    obj.ApprovedDt = CasteGroup.approvedDt.ToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = CasteGroup.approvedDtLoc;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDt = CasteGroup.enteredDt.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = CasteGroup.enteredDtLoc;
                    obj.IPAddress = CommonVariables.IPAddress;
                    obj.Mode = CasteGroup.Mode;
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
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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

        public DataTable CasteGroup_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {
               
                if (sessionLanguage == "English")
                {
                    cmdText = "select CASTE_GROUP_CD,DEFINED_CD,DESC_ENG DESCRIPTION,SHORT_NAME,ORDER_NO,DISABLED,APPROVED,APPROVED_BY,APPROVED_DT,ENTERED_BY,ENTERED_DT from MIS_CASTE_GROUP";
                }
                else
                {
                    cmdText = "select CASTE_GROUP_CD,DEFINED_CD,DESC_LOC DESCRIPTION,SHORT_NAME_LOC SHORT_NAME,ORDER_NO,DISABLED,APPROVED,APPROVED_BY,APPROVED_DT,ENTERED_BY,ENTERED_DT from MIS_CASTE_GROUP";
                }
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    if (sessionLanguage == "English")
                    {
                        cmdText += String.Format(" where Upper(DESC_ENG) like '{0}%'", initialStr);
                    }
                    else
                    {
                        cmdText += String.Format(" where Upper(DESC_LOC) like '{0}%'", initialStr);
                    }
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "DEFINED_CD")
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
                    cmdText = "select * from MIS_CASTE_GROUP where DEFINED_CD ='" + DefinedCd + "'";
                }
                else
                {
                    cmdText = "select * from MIS_CASTE_GROUP where DEFINED_CD ='" + DefinedCd + "' and CASTE_GROUP_CD !='" + code + "'";
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

        public MISCasteGroup FillCasteGroup(string CasteGroupCd)
        {
            MISCasteGroup obj = new MISCasteGroup();
            DataTable dTable = new DataTable();
            DataTable dt = new DataTable();
            dTable = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select CASTE_GROUP_CD, DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC,DEFINED_CD from MIS_CASTE_GROUP where CASTE_GROUP_CD = '" + CasteGroupCd + "'";
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
                        obj.casteGroupCd = Convert.ToInt32(dTable.Rows[0]["CASTE_GROUP_CD"]);
                        obj.descEng = dTable.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.descLoc = dTable.Rows[0]["DESC_LOC"].ConvertToString();
                        obj.shortName = dTable.Rows[0]["SHORT_NAME"].ConvertToString();
                        obj.shortNameLoc = dTable.Rows[0]["SHORT_NAME_LOC"].ConvertToString();
                        obj.definedCd = dTable.Rows[0]["DEFINED_CD"].ConvertToString();
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
