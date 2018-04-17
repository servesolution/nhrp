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
    public class CaseRegistrationTypeService
    {

        public bool CaseRegistrationType_Manage(CaseRegistrationType objCaseRegistrationType, string mode, out string exc)
        {

            QueryResult qr = null;
            CaseRegistrationTypeInfo objCaseRegistrationTypeInfo = new CaseRegistrationTypeInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objCaseRegistrationTypeInfo.RegTypeCd = objCaseRegistrationType.RegTypeCd;
                objCaseRegistrationTypeInfo.DefinedCd = Utils.ConvertToString(objCaseRegistrationType.DefinedCd);
                objCaseRegistrationTypeInfo.DescEng = Utils.ConvertToString(objCaseRegistrationType.DescEng);
                objCaseRegistrationTypeInfo.DescLoc = Utils.ConvertToString(objCaseRegistrationType.DescLoc);
                objCaseRegistrationTypeInfo.ShortName = Utils.ConvertToString(objCaseRegistrationType.ShortName);
                objCaseRegistrationTypeInfo.ShortNameLoc = Utils.ConvertToString(objCaseRegistrationType.ShortNameLoc);
                objCaseRegistrationTypeInfo.OrderNo = objCaseRegistrationType.OrderNo;
                objCaseRegistrationTypeInfo.Disabled = objCaseRegistrationType.Disabled;

                if (mode.EqualsTo("I"))
                {
                    objCaseRegistrationTypeInfo.EnteredBy = SessionCheck.getSessionUsername();//objCaseRegistrationType.EnteredBy;
                    objCaseRegistrationTypeInfo.EnteredDt = objCaseRegistrationType.EnteredDt;
                    objCaseRegistrationTypeInfo.EnteredDtLoc = objCaseRegistrationType.EnteredDtLoc;
                }
                if (mode.EqualsTo("U"))
                {
                    objCaseRegistrationTypeInfo.UpdatedBy = SessionCheck.getSessionUsername();//objCaseRegistrationType.EnteredBy;
                    objCaseRegistrationTypeInfo.UpdatedDt = objCaseRegistrationType.UpdatedDt;
                    objCaseRegistrationTypeInfo.UpdatedDtLoc = objCaseRegistrationType.UpdatedDtLoc.ToString();
                }
                if (mode.EqualsTo("A"))
                {
                    objCaseRegistrationTypeInfo.Approved = objCaseRegistrationType.Approved;
                    objCaseRegistrationTypeInfo.ApprovedBy = objCaseRegistrationType.ApprovedBy;
                    objCaseRegistrationTypeInfo.ApprovedDt = objCaseRegistrationType.ApprovedDt;
                    objCaseRegistrationTypeInfo.ApprovedDtLoc = objCaseRegistrationType.ApprovedDtLoc;
                }
                objCaseRegistrationTypeInfo.Ipaddress = CommonVariables.IPAddress;
                objCaseRegistrationTypeInfo.Mode = mode;
                service.Begin();
                try
                {
                    qr = service.SubmitChanges(objCaseRegistrationTypeInfo, true);
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
        public void ChangeStatus(CaseRegistrationType objCaseRegistrationType)
        {
            QueryResult qr = null;
            CaseRegistrationTypeInfo objCaseRegistrationTypeInfo = new CaseRegistrationTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_CASE_GREIVANCES";
                objCaseRegistrationTypeInfo.RegTypeCd = objCaseRegistrationType.RegTypeCd;
                objCaseRegistrationTypeInfo.Approved = objCaseRegistrationType.Approved;
                objCaseRegistrationTypeInfo.ApprovedBy = objCaseRegistrationType.ApprovedBy;
                //objCaseRegistrationTypeInfo.EnteredBy = SessionCheck.getSessionUsername();//objCaseRegistrationType.EnteredBy;
                objCaseRegistrationTypeInfo.ApprovedDt = objCaseRegistrationType.ApprovedDt.ToString();
                objCaseRegistrationTypeInfo.ApprovedDtLoc = objCaseRegistrationType.ApprovedDtLoc;
                objCaseRegistrationTypeInfo.Mode = "A";
                try
                {

                    service.Begin();
                    qr = service.SubmitChanges(objCaseRegistrationTypeInfo, true);
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string CaseRegistrationTypeCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (CaseRegistrationTypeCode == string.Empty)
                {
                    cmdText = "select * from CASE_REGISTRATION_TYPE where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from CASE_REGISTRATION_TYPE where DEFINED_CD ='" + DefinedCode + "' and REG_TYPE_CD !='" + CaseRegistrationTypeCode + "'";
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
        public DataTable CaseRegistrationType_GetAllToTable()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from CASE_REGISTRATION_TYPE";
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

        public DataTable CaseRegistrationType_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select REG_TYPE_CD,defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + " description," + Utils.ToggleLanguage("short_name", "short_name_loc") + " shortname,approved from CASE_REGISTRATION_TYPE";
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

        public CaseRegistrationType CaseRegistrationType_Get(int CaseRegistrationTypeCode)
        {
            CaseRegistrationType obj = new CaseRegistrationType();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    cmdText = String.Format("select * from CASE_REGISTRATION_TYPE where REG_TYPE_CD ={0}", CaseRegistrationTypeCode.ToString());
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
                    obj.RegTypeCd = decimal.Parse(dtbl.Rows[0]["REG_TYPE_CD"].ConvertToString());
                    obj.DefinedCd = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    obj.DescEng = dtbl.Rows[0]["DESC_ENG"].ConvertToString();
                    obj.DescLoc = dtbl.Rows[0]["DESC_LOC"].ConvertToString();
                    obj.ShortName = dtbl.Rows[0]["SHORT_NAME"].ConvertToString();
                    obj.ShortNameLoc = dtbl.Rows[0]["SHORT_NAME_LOC"].ConvertToString();
                    obj.OrderNo = dtbl.Rows[0]["ORDER_NO"].ToDecimal();
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
                    obj.ApprovedBy = dtbl.Rows[0]["APPROVED_BY"].ConvertToString();
                    obj.ApprovedDt = dtbl.Rows[0]["APPROVED_DT"].ToDateTime().ConvertToString("dd-MM-yyyy");
                    obj.ApprovedDtLoc = dtbl.Rows[0]["APPROVED_DT_LOC"].ConvertToString();
                    obj.UpdatedBy = dtbl.Rows[0]["UPDATED_BY"].ConvertToString();
                    obj.UpdatedDt = dtbl.Rows[0]["UPDATED_DT"].ToDateTime().ConvertToString("dd-MM-yyyy");
                    obj.UpdatedDtLoc = dtbl.Rows[0]["UPDATED_DT_LOC"].ConvertToString();
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

        public string CaseRegistrationType_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = String.Format("select nvl(max(to_number(REG_TYPE_CD)),0)+1 as newid from CASE_REGISTRATION_TYPE");
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
