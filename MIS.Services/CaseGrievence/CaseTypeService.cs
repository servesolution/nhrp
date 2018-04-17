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
    public class CaseTypeService
    {

        public bool CaseType_Manage(CaseType objCaseType, string mode, out string exc)
        {

            QueryResult qr = null;
            CaseIdTypeInfo objCaseTypeInfo = new CaseIdTypeInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objCaseTypeInfo.IdTypeCd = objCaseType.IdTypeCd;
                objCaseTypeInfo.DefinedCd = Utils.ConvertToString(objCaseType.DefinedCd);
                objCaseTypeInfo.DescEng = Utils.ConvertToString(objCaseType.DescEng);
                objCaseTypeInfo.DescLoc = Utils.ConvertToString(objCaseType.DescLoc);
                objCaseTypeInfo.ShortName = Utils.ConvertToString(objCaseType.ShortName);
                objCaseTypeInfo.ShortNameLoc = Utils.ConvertToString(objCaseType.ShortNameLoc);
                objCaseTypeInfo.OrderNo = objCaseTypeInfo.OrderNo;
                objCaseTypeInfo.Disabled = objCaseTypeInfo.Disabled;

                if (mode.EqualsTo("I"))
                {
                    objCaseTypeInfo.EnteredBy = objCaseType.EnteredBy;
                    objCaseTypeInfo.EnteredDt = objCaseType.EnteredDt;
                    objCaseTypeInfo.EnteredDtLoc = objCaseType.EnteredDtLoc;
                }
                if (mode.EqualsTo("U"))
                {
                    objCaseTypeInfo.UpdatedBy = objCaseType.UpdatedBy;
                    objCaseTypeInfo.UpdatedDt = objCaseType.UpdatedDt;
                    objCaseTypeInfo.UpdatedDtLoc = objCaseType.UpdatedDtLoc;
                }
                if (mode.EqualsTo("A"))
                {
                    objCaseTypeInfo.Approved = objCaseTypeInfo.Approved;
                    objCaseTypeInfo.ApprovedBy = objCaseType.ApprovedBy;
                    objCaseTypeInfo.ApprovedDt = objCaseType.ApprovedDt;
                    objCaseTypeInfo.ApprovedDtLoc = objCaseType.ApprovedDtLoc;
                }
                objCaseTypeInfo.Ipaddress = CommonVariables.IPAddress;
                objCaseTypeInfo.Mode = mode;
                service.Begin();
                try
                {
                    qr = service.SubmitChanges(objCaseTypeInfo, true);
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
        public void ChangeStatus(CaseType objCaseType)
        {
            QueryResult qr = null;
            CaseIdTypeInfo objCaseTypeInfo = new CaseIdTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objCaseTypeInfo.IdTypeCd = objCaseType.IdTypeCd;
                objCaseTypeInfo.Approved = objCaseType.Approved;
                objCaseTypeInfo.ApprovedBy = objCaseType.ApprovedBy;
                //objCaseTypeInfo.EnteredBy = SessionCheck.getSessionUsername();//objCaseType.EnteredBy;
                objCaseTypeInfo.ApprovedDt = objCaseType.ApprovedDt;
                objCaseTypeInfo.ApprovedDtLoc = objCaseType.ApprovedDtLoc;
                objCaseTypeInfo.Mode = "A";
                try
                {

                    service.Begin();
                    qr = service.SubmitChanges(objCaseTypeInfo, true);
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string CaseTypeCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (CaseTypeCode == string.Empty)
                {
                    cmdText = "select * from CASE_ID_TYPE where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from CASE_ID_TYPE where DEFINED_CD ='" + DefinedCode + "' and ID_TYPE_CD !='" + CaseTypeCode + "'";
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
        public DataTable CaseType_GetAllToTable()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from CASE_ID_TYPE";
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

        public DataTable CaseType_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select ID_TYPE_CD,defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + " description," + Utils.ToggleLanguage("short_name", "short_name_loc") + " shortname,approved from CASE_ID_TYPE";
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

        public CaseType CaseType_Get(int CaseTypeCode)
        {
            CaseType obj = new CaseType();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    cmdText = String.Format("SELECT * FROM CASE_ID_TYPE WHERE ID_TYPE_CD ={0}", CaseTypeCode.ToString());
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
                    obj.IdTypeCd = decimal.Parse(dtbl.Rows[0]["ID_TYPE_CD"].ConvertToString());
                    obj.DefinedCd = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    obj.DescEng = dtbl.Rows[0]["DESC_ENG"].ConvertToString();
                    obj.DescLoc = dtbl.Rows[0]["DESC_LOC"].ConvertToString();
                    obj.ShortName = dtbl.Rows[0]["SHORT_NAME"].ConvertToString();
                    obj.ShortNameLoc = dtbl.Rows[0]["SHORT_NAME_LOC"].ConvertToString();
                    obj.OrderNo = 0;
                    if (dtbl.Rows[0]["DISABLED"].ConvertToString() == "Y")
                    {
                        obj.Disabled = true;
                    }
                    else
                    {
                        obj.Disabled = false;
                    }
                    if (dtbl.Rows[0]["APPROVED"].ConvertToString() == "Y")
                    {
                        obj.Approved = true;
                    }
                    else
                    {
                        obj.Approved = false;

                    }
                    obj.ApprovedDt = dtbl.Rows[0]["APPROVED_DT"].ToDateTime().ConvertToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = dtbl.Rows[0]["APPROVED_DT_LOC"].ConvertToString();
                    obj.EnteredBy = dtbl.Rows[0]["ENTERED_BY"].ConvertToString();
                    obj.EnteredDt = dtbl.Rows[0]["ENTERED_DT"].ToDateTime().ConvertToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = dtbl.Rows[0]["ENTERED_DT_LOc"].ConvertToString();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }

        public string CaseType_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = String.Format("select nvl(max(to_number(ID_TYPE_CD)),0)+1 as newid from CASE_ID_TYPE");
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
