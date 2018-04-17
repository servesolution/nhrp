using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Data;

namespace MIS.Services.Setup
{
    public class ImplementingPartnerService
    {
        public bool ImplementingPartnerInsert(ImplementingPartnerModelClas objModel)
        {
         	ImplementingPartnerEntityClass  objentity = new 	ImplementingPartnerEntityClass ();
           
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                service.PackageName = "PKG_NHRS_TRAINING";
                objentity.implementingPrtnerId = objModel.IMPLEMENTING_PARTNER_CD.ToDecimal();
                objentity.Mode = objModel.MODE;
                if (objModel.MODE == "I" || objModel.MODE == "U")
                {
                    objentity.definedCd = objModel.IMPLEMENTING_PARTNER_DEF_CD.ToDecimal();
                    objentity.descEng = objModel.DESC_ENG;
                    objentity.descLoc = objModel.DESC_LOC;
                    objentity.shortNameEng = objModel.SHORT_NAME_ENG;
                    objentity.shortNameLoc = objModel.SHORT_NAME_LOC;

                    objentity.startDate = objModel.START_DATE.ToDateTime();
                    objentity.startDateLoc = objModel.START_DATE_LOC;
                    objentity.endDate = objModel.END_DATE.ToDateTime(); ;
                    objentity.endDateLoc = objModel.END_DATE_LOC;
                    objentity.isDonor = objModel.DONOR;
                    objentity.isTrinOrg = objModel.TRAIN_ORG;


                    objentity.status = "G";
                    objentity.active = "Y";
                    objentity.enteredBy = SessionCheck.getSessionUsername();
                    objentity.enteredDate = System.DateTime.Now;
                    objentity.enteredDateLoc = System.DateTime.Now.ConvertToString();
                    objentity.approvedd = "Y";

                    objentity.approvedBy = SessionCheck.getSessionUsername();
                    objentity.approvedDate = System.DateTime.Now;
                    objentity.approvedDateLoc = System.DateTime.Now.ConvertToString();

                    objentity.updatedBy = SessionCheck.getSessionUsername();
                    objentity.updatedDate = System.DateTime.Now;
                    objentity.updatedDateLoc = System.DateTime.Now.ConvertToString();
             
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
        public DataTable GetAllImplementingPartner()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select * from NHRS_IMPLEMENTING_PARTNER WHERE 1=1 ";

                
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
        public ImplementingPartnerModelClas FillImplementingPartner(string ImpCd)
        {
            ImplementingPartnerModelClas obj = new ImplementingPartnerModelClas();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_IMPLEMENTING_PARTNER where IMPLEMENTING_PARTNER_ID = '" + ImpCd + "'";
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

                        obj.IMPLEMENTING_PARTNER_CD = dt.Rows[0]["IMPLEMENTING_PARTNER_ID"].ToString();
                        obj.IMPLEMENTING_PARTNER_DEF_CD = dt.Rows[0]["DEFINED_CD"].ToString();
                        obj.DESC_ENG = dt.Rows[0]["DESC_ENG"].ToString();
                        obj.DESC_LOC = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.SHORT_NAME_ENG = dt.Rows[0]["SHORT_NAME_ENG"].ToString();
                        obj.SHORT_NAME_LOC = dt.Rows[0]["SHORT_NAME_LOC"].ToString();

                        obj.START_DATE = dt.Rows[0]["START_DATE"].ConvertToString("yy-MMM-dd");
                        obj.START_DATE_LOC = dt.Rows[0]["START_DATE_LOC"].ToString();
                        obj.END_DATE = dt.Rows[0]["END_DATE"].ConvertToString("yy-MMM-dd");
                        obj.END_DATE_LOC = dt.Rows[0]["END_DATE_LOC"].ToString();

                        obj.DONOR = dt.Rows[0]["DONOR"].ToString();
                        obj.TRAIN_ORG = dt.Rows[0]["TRAINING_ORG"].ToString();
                        



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
