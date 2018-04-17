using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
using MIS.Models.Inspection;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;


namespace MIS.Services.Inspection
{
    public class InspectionDetailService
    {
        public DataTable getAllHouseModel( )
        {
            string cmdText = null;
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS_HOUSE_MODEL WHERE 1=1 ";
              
                try
                {
                    service.Begin();
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
            return dt;
        }

        public string GetBaseUrl()
        {
            return  HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public QueryResult insertInspectionDetail(InspectionDetailModelClass objModel)
        {
            InspectionDetailEntityClass objEntity = new InspectionDetailEntityClass();
            QueryResult qr = new QueryResult();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objEntity.Mode = objModel.mode;

                    objEntity.dEFINED_CD = objModel.definedCd.ConvertToString();
                    objEntity.iNSPECTION_CODE_ID = objModel.inspectionCd.ToDecimal();
                    objEntity.uPPER_INSPECTION_CODE_ID = objModel.upperInspectionCodeId.ToDecimal();
                    objEntity.gROUP_FLAG = objModel.groupFlag;
                    objEntity.vALUE_TYPE = objModel.valuetype;
                    objEntity.dESC_ENG = objModel.descEng;
                    objEntity.dESC_LOC = objModel.descLoc;
                    objEntity.sHORT_NAME = objModel.shortname;
                    objEntity.sHORT_NAME_LOC = objModel.shortNameLoc;
                    objEntity.oRDER_NO = objModel.orderNo.ToDecimal();
                    //objEntity.dISABLED = objModel.disabled;
                    objEntity.dISABLED = objModel.disabled;

                    objEntity.eNTERED_BY = SessionCheck.getSessionUsername();
                    objEntity.eNTERED_DT = DateTime.Now.ToDateTime();
                    objEntity.eNTERED_DT_LOC = DateTime.Now.ToDateTime().ToString();
                    objEntity.aPPROVED = "Y";
                    objEntity.aPPROVED_BY = SessionCheck.getSessionUsername();
                    objEntity.aPPROVED_DT = DateTime.Now.ToDateTime();
                    objEntity.aPPROVED_DT_LOC = DateTime.Now.ToDateTime().ToString();
                    service.PackageName = "PKG_NHRS_INSPECTION";
                    service.Begin();
                     qr = service.SubmitChanges(objEntity, true);
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
            return qr;
        }

        public void insertInspectionModelDtl(InspectionDetailModelClass objModel)
        {
            InspectionModelDetailEntityClass objEntity = new InspectionModelDetailEntityClass();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objEntity.Mode = objModel.mode;

                    objEntity.iNSPECTION_MODEL_DTL_ID = objModel.inspectionModelDetailId.ToDecimal();
                    objEntity.iNSPECTION_CODE_ID = objModel.inspectionCd.ToDecimal();
                    objEntity.mODEL_ID = objModel.modelCd.ToDecimal();
                    
                    service.PackageName = "PKG_NHRS_INSPECTION";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objEntity, true);
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
        public InspectionDetailModelClass FillInspectonDetail(decimal? inspectionCd)
        {
            InspectionDetailModelClass obj = new InspectionDetailModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_INSPECTION_DESC_DTL where INSPECTION_CODE_ID = '" + inspectionCd + "'";
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
                        obj = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.inspectionCd = dt.Rows[0]["INSPECTION_CODE_ID"].ToInt32();

                        obj.definedCd = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.upperInspectionCodeId = dt.Rows[0]["UPPER_INSPECTION_CODE_ID"].ToInt32();
                        obj.groupFlag = dt.Rows[0]["GROUP_FLAG"].ToString();
                        obj.valuetype = dt.Rows[0]["VALUE_TYPE"].ToString();
                        obj.descEng = dt.Rows[0]["DESC_ENG"].ToString();

                        obj.descLoc = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.shortname = dt.Rows[0]["SHORT_NAME"].ToString();
                        obj.shortNameLoc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();

                        obj.orderNo = dt.Rows[0]["ORDER_NO"].ConvertToString();
                        obj.disabled = dt.Rows[0]["DISABLED"].ConvertToString();
                      


                    }
                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return obj;
        }
        public DataTable FillInspectonModelDetail(decimal? inspectionCd)
        {
            InspectionDetailModelClass obj = new InspectionDetailModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_INSPECTION_MODEL_DTL where INSPECTION_CODE_ID = '" + inspectionCd + "'";
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
                        obj = null;
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
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return dt;
        }
        public void CleanModelInspection(InspectionDetailModelClass objModel)
        {
            InspectionModelDetailEntityClass objEntity = new InspectionModelDetailEntityClass();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objEntity.Mode = "D";

                    objEntity.iNSPECTION_MODEL_DTL_ID = objModel.inspectionModelDetailId.ToDecimal();
                    objEntity.iNSPECTION_CODE_ID = objModel.inspectionCd.ToDecimal();
                    objEntity.mODEL_ID = objModel.modelCd.ToDecimal();

                    service.PackageName = "PKG_NHRS_INSPECTION";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objEntity, true);
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

        public DataTable GetAllInspectionDetailList()
        {
            InspectionDetailModelClass obj = new InspectionDetailModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_INSPECTION_DESC_DTL where 1=1";
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
                        obj = null;
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
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return dt;
        }
      
    }
}
