using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.CaseGrievence;
using MIS.Services.Core;
using System.Data;
using ExceptionHandler;
using System.Data.OracleClient;

namespace MIS.Services.CaseGrievence
{
    public class GrievenceNatureService
    {

        public bool GrievenceNature_Manage(GrievenceNature objGrievenceNature, string mode, out string exc)
        {

            QueryResult qr = null;
            CaseGrievanceNatureInfo objGrievenceNatureInfo = new CaseGrievanceNatureInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objGrievenceNatureInfo.GrievanceNatureCd = objGrievenceNature.GrievanceNatureCd;
                objGrievenceNatureInfo.DefinedCd = Utils.ConvertToString(objGrievenceNature.DefinedCd);
                objGrievenceNatureInfo.DescEng = Utils.ConvertToString(objGrievenceNature.DescEng);
                objGrievenceNatureInfo.DescLoc = Utils.ConvertToString(objGrievenceNature.DescLoc);
                objGrievenceNatureInfo.ShortName = Utils.ConvertToString(objGrievenceNature.ShortName);
                objGrievenceNatureInfo.ShortNameLoc = Utils.ConvertToString(objGrievenceNature.ShortNameLoc);
                objGrievenceNatureInfo.OrderNo = objGrievenceNatureInfo.OrderNo;
                objGrievenceNatureInfo.Disabled = objGrievenceNatureInfo.Disabled;
                objGrievenceNatureInfo.Approved = objGrievenceNatureInfo.Approved;
                objGrievenceNatureInfo.EnteredBy = SessionCheck.getSessionUsername();//objGrievenceNature.EnteredBy;
                objGrievenceNatureInfo.EnteredDt = objGrievenceNature.EnteredDt.ToString("dd-MM-yyyy");
                objGrievenceNatureInfo.EnteredDtLoc = objGrievenceNature.EnteredDtLoc;
                objGrievenceNatureInfo.ApprovedBy = objGrievenceNature.ApprovedBy;
                objGrievenceNatureInfo.ApprovedDt = objGrievenceNature.ApprovedDt.ToString("dd-MM-yyyy");
                objGrievenceNatureInfo.ApprovedDtLoc = objGrievenceNature.ApprovedDtLoc;
                objGrievenceNatureInfo.UpdatedBy = objGrievenceNature.ApprovedBy;
                objGrievenceNatureInfo.UpdatedDt = objGrievenceNature.ApprovedDt.ToString("dd-MM-yyyy");
                objGrievenceNatureInfo.UpdatedDtLoc = objGrievenceNature.ApprovedDtLoc;
                objGrievenceNatureInfo.Ipaddress = CommonVariables.IPAddress;
                objGrievenceNatureInfo.Mode = mode;
                service.Begin();
                try
                {
                    qr = service.SubmitChanges(objGrievenceNatureInfo, true);
                }
                catch (OracleException oe)
                {
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                    return false;
                }
                catch (Exception ex)
                {
                    exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                    return false;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return qr.IsSuccess;

        }
        //Change Permission
        public void ChangeStatus(GrievenceNature objGrievenceNature)
        {
            QueryResult qr = null;
            CaseGrievanceNatureInfo objGrievenceNatureInfo = new CaseGrievanceNatureInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objGrievenceNatureInfo.GrievanceNatureCd = objGrievenceNature.GrievanceNatureCd;
                objGrievenceNatureInfo.Approved = objGrievenceNature.Approved;
                objGrievenceNatureInfo.ApprovedBy = objGrievenceNature.ApprovedBy;
                objGrievenceNatureInfo.EnteredBy = SessionCheck.getSessionUsername();//objGrievenceNature.EnteredBy;
                objGrievenceNatureInfo.ApprovedDt = objGrievenceNature.ApprovedDt.ToString("dd-MM-yyyy");
                objGrievenceNatureInfo.ApprovedDtLoc = objGrievenceNature.ApprovedDtLoc;
                objGrievenceNatureInfo.Mode = "A";
                try
                {

                    service.Begin();
                    qr = service.SubmitChanges(objGrievenceNatureInfo, true);
                }
                catch (OracleException oe)
                {
                    qr = new QueryResult();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    qr = new QueryResult();
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

        //Check Duplicate
        public bool CheckDuplicateDefinedCode(string DefinedCode, string GrievenceNatureCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (GrievenceNatureCode == string.Empty)
                {
                    cmdText = "select * from CASE_GRIEVANCE_NATURE where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from CASE_GRIEVANCE_NATURE where DEFINED_CD ='" + DefinedCode + "' and GRIEVANCE_NATURE_CD !='" + GrievenceNatureCode + "'";
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
        public DataTable GrievenceNature_GetAllToTable()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from CASE_GRIEVANCE_NATURE";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
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
            return dtbl;
        }

        public DataTable GrievenceNature_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select GRIEVANCE_NATURE_CD,defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + " description," + Utils.ToggleLanguage("short_name", "short_name_loc") + " shortname,approved from CASE_GRIEVANCE_NATURE";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("desc_eng", "desc_loc") + ") like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "defined_cd")
                    {
                        cmdText += String.Format(" order by to_number({0}) {1}", orderby, order);
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
            return dtbl;
        }

        public GrievenceNature GrievenceNature_Get(int GrievenceNatureCode)
        {
            GrievenceNature obj = new GrievenceNature();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    cmdText = String.Format("SELECT * FROM CASE_GRIEVANCE_NATURE WHERE GRIEVANCE_NATURE_CD ={0}", GrievenceNatureCode.ToString());
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(cmdText, null);
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
                if (dtbl.Rows.Count > 0)
                {
                    obj.GrievanceNatureCd = decimal.Parse(dtbl.Rows[0]["GRIEVANCE_NATURE_CD"].ToString());
                    obj.DefinedCd = dtbl.Rows[0]["DEFINED_CD"].ToString();
                    obj.DescEng = dtbl.Rows[0]["DESC_ENG"].ToString();
                    obj.DescLoc = dtbl.Rows[0]["DESC_LOC"].ToString();
                    obj.ShortName = dtbl.Rows[0]["SHORT_NAME"].ToString();
                    obj.ShortNameLoc = dtbl.Rows[0]["SHORT_NAME_LOC"].ToString();
                    obj.OrderNo = 0;
                    if (dtbl.Rows[0]["DISABLED"].ToString() == "Y")
                    {
                        obj.Disabled = true;
                    }
                    else
                    {
                        obj.Disabled = false;
                    }
                    if (dtbl.Rows[0]["APPROVED"].ToString() == "Y")
                    {
                        obj.Approved = true;
                    }
                    else
                    {
                        obj.Approved = false;

                    }
                    obj.ApprovedDt = DateTime.Parse(dtbl.Rows[0]["APPROVED_DT"].ToString());
                    obj.ApprovedDtLoc = dtbl.Rows[0]["APPROVED_DT_LOC"].ToString();
                    obj.EnteredBy = dtbl.Rows[0]["ENTERED_BY"].ToString();
                    obj.EnteredDt = DateTime.Parse(dtbl.Rows[0]["ENTERED_DT"].ToString());
                    obj.EnteredDtLoc = dtbl.Rows[0]["ENTERED_DT_LOc"].ToString();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }

        public string GrievenceNature_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = String.Format("select nvl(max(to_number(GRIEVANCE_NATURE_CD)),0)+1 as newid from CASE_GRIEVANCE_NATURE");
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
                if (dtbl.Rows.Count > 0)
                {
                    newID = dtbl.Rows[0]["newid"].ToString();

                }
            }
            catch (Exception ex)
            {
                newID = "";
                ExceptionManager.AppendLog(ex);
            }
            return newID;
        }
    }
}
