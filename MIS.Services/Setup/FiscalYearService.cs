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
    public class FiscalYearService
    {
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = new DataTable();
            DataTable MaxCode = new DataTable();
            dt = null;
            try
            {
                using (ServiceFactory CasteService = new ServiceFactory())
                {
                   
                    string cmdText = "select nvl(max(to_number(SERIAL_NO)),0)+1 from COM_FISCAL_YEAR";
                    try
                    {
                        CasteService.Begin();
                        MaxCode = CasteService.GetDataTable(new
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
                        CasteService.End();
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

        public void ManageFiscalYear(MISFiscalYear FiscalYear)
        {
            using (ServiceFactory FiscalYearservice = new ServiceFactory())
            {
                ComFiscalYearInfo obj = new ComFiscalYearInfo();
                if (FiscalYear.Mode == "D")
                {
                    obj.FiscalYr = FiscalYear.FiscalYr;
                    obj.SerialNo = null;
                    obj.FiscalStartDt = null;
                    obj.FiscalStartDtLoc = null;
                    obj.FiscalEndDt = null;
                    obj.FiscalEndDtLoc = null;
                    obj.Status = null;
                    obj.ProvisionCloseBy = null;
                    obj.ProvisionCloseDt = null;
                    obj.ProvisionCloseDtLoc = null;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDtLoc = null;
                    obj.EnteredDt = DateTime.Now.ToString("dd-MM-yyyy");
                    obj.FinalCloseBy = null;
                    obj.FinalCloseDt = null;
                    obj.FinalCloseDtLoc = null;
                    obj.IPAddress = CommonVariables.IPAddress;
                    obj.Mode = FiscalYear.Mode;
                }
                else
                {
                    obj.FiscalYr = FiscalYear.FiscalYr;
                    obj.SerialNo = FiscalYear.SerialNo.ToInt32(); //Convert.ToInt32(GetMaxvalue());
                    obj.FiscalStartDt = FiscalYear.FiscalStartDt.ToString("dd-MM-yyyy");
                    obj.FiscalStartDtLoc =Utils.ConvertToString(FiscalYear.FiscalStartDtLoc);
                    obj.FiscalEndDt = FiscalYear.FiscalEndDt.ToString("dd-MM-yyyy");
                    obj.FiscalEndDtLoc = Utils.ConvertToString(FiscalYear.FiscalEndDtLoc);
                    obj.Status = "O";
                    obj.ProvisionCloseBy = null;
                    obj.ProvisionCloseDt = null;
                    obj.ProvisionCloseDtLoc = null;
                    obj.EnteredBy = SessionCheck.getSessionUsername();
                    obj.EnteredDt = FiscalYear.EnteredDt.ToString("dd-MM-yyyy");
                    obj.EnteredDtLoc = NepaliDate.getNepaliDate(DateTime.Today.ToString("dd-MMM-yyyy"));
                    obj.Mode = FiscalYear.Mode;
                    obj.IPAddress = CommonVariables.IPAddress;
                }
                FiscalYearservice.PackageName = "PKG_SETUP";               
                try
                {
                    FiscalYearservice.Begin();
                    QueryResult qr = FiscalYearservice.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    FiscalYearservice.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    FiscalYearservice.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    FiscalYearservice.End();
                }
            }
        }
        public DataTable FiscalYear_GetFiscalYear()
        {
            DataTable dtbl = null;           
            string cmdText = "";           
            using (ServiceFactory service = new ServiceFactory())
            {                
                cmdText = "select FISCAL_YR,FISCAL_START_DT,FISCAL_END_DT from COM_FISCAL_YEAR";                
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
                    service.End();
                }
            }
            return dtbl;
        }

        public DataTable FiscalYear_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = "select FISCAL_YR," + Utils.ToggleLanguage("FISCAL_START_DT", "FISCAL_START_DT_LOC") + " AS FISCAL_START_DT," + Utils.ToggleLanguage("FISCAL_END_DT", "FISCAL_END_DT_LOC") + " as FISCAL_END_DT,STATUS,SERIAL_NO,ENTERED_BY,ENTERED_DT from COM_FISCAL_YEAR";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    if (sessionLanguage == "English")
                    {
                        cmdText += String.Format(" where Upper(FISCAL_YR) like '{0}%'", initialStr);
                    }
                    else
                    {
                        cmdText += String.Format(" where Upper(FISCAL_YR) like '{0}%'", initialStr);
                    }
                }
                if ((orderby != "") && (order != ""))
                {
                    cmdText += String.Format(" order by {0} {1}", orderby, order);
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
                    service.End();
                }
            }
            return dtbl;
        }

        public bool CheckDuplicateFiscalYear(string fiscalYear, string serialno)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {               
               
                if (serialno == string.Empty)
                {
                    cmdText = "select * from COM_FISCAL_YEAR where FISCAL_YR ='" + fiscalYear + "'";
                }
                else
                {
                    cmdText = "select * from COM_FISCAL_YEAR where FISCAL_YR ='" + fiscalYear + "' and SERIAL_NO !='" + serialno + "'";
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
                    service.End();
                }
                if (dtbl.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }

        public DataTable CheckToAddorUpdate(string fiscalYear)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {                
                
                cmdText = "select * from COM_FISCAL_YEAR where FISCAL_YR ='" + fiscalYear + "'";
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
                    service.End();
                }
                return dtbl;
            }
        }

        public MISFiscalYear FillFiscalYear(string FiscalYr) 
        {
            MISFiscalYear obj = new MISFiscalYear();           
            DataTable dt = null;
            using (ServiceFactory FiscalYearService = new ServiceFactory())
            {
                string CmdText = "select FISCAL_YR,FISCAL_START_DT_LOC,FISCAL_START_DT,FISCAL_END_DT_LOC,FISCAL_END_DT,STATUS,SERIAL_NO from COM_FISCAL_YEAR where FISCAL_YR = '" + FiscalYr + "'";
                try
                {
                    FiscalYearService.Begin();
                    dt = FiscalYearService.GetDataTable(new
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
                    obj = null;
                }
                finally
                {
                    FiscalYearService.End();
                }

                if (dt.Rows.Count > 0)
                {
                    obj.FiscalYr = dt.Rows[0]["FISCAL_YR"].ToString();
                    obj.FiscalStartDt = Convert.ToDateTime(dt.Rows[0]["FISCAL_START_DT"]);
                    obj.FiscalStartDtLoc = dt.Rows[0]["FISCAL_START_DT_LOC"].ToString();
                    obj.FiscalEndDt = Convert.ToDateTime(dt.Rows[0]["FISCAL_END_DT"]);
                    obj.FiscalEndDtLoc = dt.Rows[0]["FISCAL_END_DT_LOC"].ToString();
                    obj.Status = dt.Rows[0]["STATUS"].ToString();
                    obj.SerialNo = Convert.ToInt32(dt.Rows[0]["SERIAL_NO"]);
                }

            }
            return obj;
        }
    }
}