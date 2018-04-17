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
using ExceptionHandler;
namespace MIS.Services.Setup
{
    public class ClassTypeService
    {
       
        
        public bool ClassType_Manage(MISClassType objClassType, string mode,out string exc)
        {

            QueryResult qr = null;
            MisClassTypeInfo objClassTypeInfo = new MisClassTypeInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";                
                objClassTypeInfo.ClassTypeCd = objClassType.ClassTypeCd;
                objClassTypeInfo.DefinedCd = Utils.ConvertToString(objClassType.DefinedCd);
                objClassTypeInfo.DescEng = Utils.ConvertToString(objClassType.DescEng);
                objClassTypeInfo.DescLoc = Utils.ConvertToString(objClassType.DescLoc);
                objClassTypeInfo.ShortName = Utils.ConvertToString(objClassType.ShortName);
                objClassTypeInfo.ShortNameLoc = Utils.ConvertToString(objClassType.ShortNameLoc);
                objClassTypeInfo.OrderNo = objClassType.OrderNo;
                objClassTypeInfo.Disabled = objClassType.Disabled.ToYNBoolean(true);
                objClassTypeInfo.Approved = objClassType.Approved.ToYNBoolean(true);
                objClassTypeInfo.EnteredBy = SessionCheck.getSessionUsername();
                objClassTypeInfo.EnteredDt = objClassType.EnteredDt.ToString("dd-MM-yyyy");
                objClassTypeInfo.EnteredDtLoc = objClassType.EnteredDtLoc;
                objClassTypeInfo.ApprovedBy = objClassType.ApprovedBy;
                objClassTypeInfo.ApprovedDt = objClassType.ApprovedDt.ToString("dd-MM-yyyy");
                objClassTypeInfo.ApprovedDtLoc = objClassType.ApprovedDtLoc;
                objClassTypeInfo.IPAddress = CommonVariables.IPAddress;
                objClassTypeInfo.Mode = mode;
               
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(objClassTypeInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                    return false;
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public void ChangeStatus(MISClassType objClassType)
        {
            QueryResult qr = null;
            MisClassTypeInfo objClassTypeInfo = new MisClassTypeInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                
                service.PackageName = "PKG_SETUP";
                objClassTypeInfo.ClassTypeCd = objClassType.ClassTypeCd;
                objClassTypeInfo.Approved = objClassType.Approved.ToYNBoolean(true);
                objClassTypeInfo.ApprovedBy = objClassType.ApprovedBy;
                objClassTypeInfo.EnteredBy = SessionCheck.getSessionUsername();
                objClassTypeInfo.ApprovedDt = objClassTypeInfo.ApprovedDt.ToString();
                objClassTypeInfo.ApprovedDtLoc = objClassTypeInfo.ApprovedDtLoc;
                objClassTypeInfo.Mode = "A";
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(objClassTypeInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    qr = new QueryResult();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string ClassTypeCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {               
                if (ClassTypeCode == string.Empty)
                {
                    cmdText = "select * from MIS_Class_TYPE where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_Class_TYPE where DEFINED_CD ='" + DefinedCode + "' and Class_TYPE_CD !='" + ClassTypeCode + "'";
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
        public DataTable ClassType_GetAllToTable()
        {
            DataTable dtbl = null;            
            using (ServiceFactory service = new ServiceFactory())
            {              
                string cmdText = "select * from MIS_Class_Type";
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

        public DataTable ClassType_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;       
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {              
             
                cmdText = "select Class_type_cd,defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + " description," + Utils.ToggleLanguage("short_name", "short_name_loc") + " shortname,approved from MIS_Class_Type";
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

        public MISClassType ClassType_Get(int ClassTypeCode)
        {
            MISClassType obj = new MISClassType();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                   service.Begin();
                   cmdText = String.Format("select * from MIS_Class_Type where Class_TYPE_CD={0}", ClassTypeCode.ToString());
                   try
                   {
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
                    obj.ClassTypeCd = decimal.Parse(dtbl.Rows[0]["Class_TYPE_CD"].ToString());
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
                ExceptionManager.AppendLog(ex);
                obj = null; 
            }
            return obj;
        }

        public string ClassType_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = String.Format("select nvl(max(to_number(Class_TYPE_CD)),0)+1 as newid from MIS_Class_Type");
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
