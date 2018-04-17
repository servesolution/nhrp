using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using EntityFramework;
using System.Reflection;
//using Oracle.ManagedDataAccess.Client;
using ExceptionHandler;
using System.Data.OracleClient;

namespace MIS.Services.Report
{
    public class STEPCSummaryReportService
    {
        public DataSet getSTEPCCountForDashboard()
        {
            DataSet dtbl = new DataSet();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataSet(true, "PR_DASHBOARD_SUMMARY_REPORT", 
                        Utils.ToggleLanguage("E", "N"),
                        DBNull.Value,
                        DBNull.Value);

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataSet GetGrievanceForReport()
        {
            DataSet dtbl = new DataSet();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataSet(true, "PR_DASHBOARD_GRIEVANCE_REPORT", 
                        Utils.ToggleLanguage("E", "N") ,
                         DBNull.Value
                        );

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataSet GetTrainingReport()
        {
            DataSet dtbl = new DataSet();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataSet(true, "pr_training_report",
                        Utils.ToggleLanguage("E", "N"),
                         DBNull.Value
                        );

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable getDTCOGranctSummaryReporTbl()
        {
            DataTable dt = null;
          
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_dtco_SUM_RPT",
                    "E",
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;

        }
        public DataSet GetDonorSupportDetail()
        {
            DataSet dtbl = new DataSet();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataSet(true, "PR_DONOR_SUPPORT_DASHBOARD",
                        Utils.ToggleLanguage("E", "N"),
                         DBNull.Value
                        );

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable GetPaymentSumReport()
        {
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    dtbl = service.GetDataTable(true, "PR_GRANT_SUM_RPT",
                        Utils.ToggleLanguage("E", "N"),
                         DBNull.Value,
                          DBNull.Value,
                           DBNull.Value,
                            DBNull.Value,
                             DBNull.Value
                        );

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable getBenefSummaryReport()
        {
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_BENEF_SUMMARY_REPORT",
                        Utils.ToggleLanguage("E", "N"),
                        DBNull.Value);

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable GetMOUDDataForGraph(string District)
        {
            DataTable dtbl = new DataTable();
            if (District == null)
            {
                District = DBNull.Value.ConvertToString();
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    dtbl = service.GetDataTable(true, "PR_GET_MOUD_DATA",
                        Utils.ToggleLanguage("E", "N"),
                        District,
                        DBNull.Value);

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable getDTCOGranctSummaryReport()
        {
            DataTable dt = null;
          
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_DTCO_Dashboard_Gant_Chart",
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;

        }
        public DataTable getDTCOGranctDetailReport(string year)
        {
            DataTable dt = null;
            Object Year = DBNull.Value;

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                if (!string.IsNullOrEmpty(year))
                    Year = year;
                dt = service.GetDataTable(true, "PR_DTCO_Dashboard_Chart",
                    Year,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;

        }
        public DataTable getGrievanceSummaryReport()
        {
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_GRIEVANCE_SUMMARY_REPORT",
                        Utils.ToggleLanguage("E", "N"),
                        DBNull.Value);

                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
  
    }
}
