using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MIS.Services.Setup
{
    public class CurriculumService
    {
        public bool CurriculumInsert(CurriculumModelClass objModel)
        {
            CurriculumEntityClass objentity = new CurriculumEntityClass();

            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                service.PackageName = "PKG_NHRS_TRAINING";
                objentity.cURRICULUM_ID = objModel.CurriculumCd.ToDecimal();
                objentity.Mode = objModel.MODE;
                if (objModel.MODE == "I" || objModel.MODE == "U")
                {
                    objentity.dEFINED_CD = objModel.DefinedCd.ToDecimal();
                    objentity.dESC_ENG = objModel.DescriptionEng;
                    objentity.dESC_LOC = objModel.DescriptionNepali;
                    objentity.sHORT_NAME_ENG = objModel.ShortNameEng;
                    objentity.sHORT_NAME_LOC = objModel.ShortNameLoc;

               

                    objentity.sTATUS = "G";
                    objentity.aCTIVE = "Y";
                    objentity.eNTERED_BY = SessionCheck.getSessionUsername();
                    objentity.eNTERED_DATE = System.DateTime.Now;
                    objentity.eNTERED_DATE_LOC = System.DateTime.Now.ConvertToString();
                    objentity.aPPROVED = "Y";

                    objentity.aPPROVED_BY = SessionCheck.getSessionUsername();
                    objentity.aPPROVED_DATE = System.DateTime.Now;
                    objentity.aPPROVED_DATE_LOC = System.DateTime.Now.ConvertToString();

                    objentity.uPDATED_BY = SessionCheck.getSessionUsername();
                    objentity.uPDATED_DATE = System.DateTime.Now;
                    objentity.uPDATED_DATE_LOC = System.DateTime.Now.ConvertToString();

                }




                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(objentity, true);
                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    //exc = oe.Code.ConvertToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
                    //exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }

        }
        public DataTable GetAllCurriculum()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select * from NHRS_CURRICULUM WHERE 1=1 ";


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
        public CurriculumModelClass FillCurriculum(string ImpCd)
        {
            CurriculumModelClass obj = new CurriculumModelClass();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_CURRICULUM where CURRICULUM_ID  = '" + ImpCd + "'";
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

                        obj.CurriculumCd = dt.Rows[0]["CURRICULUM_ID"].ToString();
                        obj.DefinedCd = dt.Rows[0]["DEFINED_CD"].ToString();
                        obj.DescriptionEng = dt.Rows[0]["DESC_ENG"].ToString();
                        obj.DescriptionNepali = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.ShortNameEng = dt.Rows[0]["SHORT_NAME_ENG"].ToString();
                        obj.ShortNameLoc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();


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
    }
}
