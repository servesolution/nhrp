using EntityFramework;
using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace MIS.Controllers.Report
{
    public class NHRSReportService
    {
        public List<TreeItems> GetReportHierarchy(string SearchText)
        {
            #region Database Data Fetch

            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {


                cmdText = "select REPORT_CD CD, UPPER_REPORT_CD UpperCd, Desc_ENG,DESC_LOC, defined_cd , ORDER_NO from MIS_REPORT " +
                         " START with (UPPER_REPORT_CD is null OR UPPER_REPORT_CD='ROOT')   " +
                         " CONNECT by prior REPORT_CD=UPPER_REPORT_CD";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {
                            // USER_CD = userCd
                        }
                    });
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
            #endregion
            var treestr = new BuildTreeStructure().GetTreeHierarch(dtbl, SearchText);

            List<TreeItems> treeItem = new List<TreeItems>(); //custKeyVal.Value;

            treeItem.Add(new TreeItems()
            {
                Children = treestr.Children,
                Cd = (""),
                UpperCd = (null),
                Label = Utils.GetLabel("Report"),
                Order = (0)

            });

            return treeItem;
        }
        public DataTable GetReportDetail(string ReportCd)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from MIS_REPORT T1";
                if (ReportCd != null)
                {
                    cmdText = cmdText + " WHERE T1.REPORT_CD=" + ReportCd;

                }
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
        public QueryResult ReportUID(MISReport objReport, string mode, out string exc)
        {

            QueryResult qResult = null;
            MisReportInfo objReportEntity = new MisReportInfo();
            exc = "";
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.PackageName = "PKG_SETUP";
                objReportEntity.ReportCd = objReport.reportCd;
                objReportEntity.UpperReportCd = objReport.upperReportCd;
                objReportEntity.DefinedCd = objReport.DefinedCD;
                objReportEntity.DescEng = objReport.descEng;
                objReportEntity.DescLoc = objReport.descLoc;
                objReportEntity.ReportTitle = objReport.descEng;
                objReportEntity.GroupFlag = objReport.groupFlag;
                objReportEntity.OrderNo = objReport.orderNo;
                objReportEntity.Disabled = objReport.disabled;
                objReportEntity.Approved = objReport.Approved;
                objReportEntity.ApprovedBy = objReport.ApprovedBy;
                objReportEntity.ApprovedDt = objReport.ApprovedDt.ToString("dd-MMM-yyyy");
                objReportEntity.ApprovedDtLoc = objReport.ApprovedDtLoc;
                objReportEntity.EnteredBy = SessionCheck.getSessionUsername();//objReport.enteredBy;
                objReportEntity.EnteredDt = objReport.enteredDt.ToString("dd-MMM-yyyy");
                objReportEntity.Ipaddress = CommonVariables.IPAddress;
                objReportEntity.Mode = mode;
                service.Begin();
                qResult = service.SubmitChanges(objReportEntity, true);
            }
            catch (OracleException oe)
            {
                service.RollBack();
                exc = oe.Code.ToString();
                ExceptionManager.AppendLog(oe);

            }
            catch (Exception ex)
            {
                service.RollBack();
                exc = ex.ToString();
                ExceptionManager.AppendLog(ex);

            }
            finally
            {

                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return qResult;

        }
        public bool CheckPresenceOfChildren(string code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from MIS_REPORT where UPPER_REPORT_CD ='" + code + "'";
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string ReportCD)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (ReportCD == string.Empty)
                {
                    cmdText = "select * from MIS_REPORT where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_REPORT where DEFINED_CD ='" + DefinedCode + "' and REPORT_CD !='" + ReportCD + "'";
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
    }
}