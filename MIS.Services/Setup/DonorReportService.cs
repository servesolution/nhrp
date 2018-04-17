using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Core;
using System.Data;
using EntityFramework;
using MIS.Services.Core;
using MIS.Models.Setup;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Setup
{
    public class DonorReportService
    {
        public List<TreeItems> GetReportHierarchy(string SearchText)
        {
            #region Database Data Fetch
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "";

                cmdText = "select REPORT_CD CD,UPPER_REPORT_CD UpperCd, DESC_ENG,ORDER_NO,DESC_LOC,REPORT_LINK from MIS_DONOR_REPORT where DISABLED='N'"
                         + "START with (UPPER_REPORT_CD is null OR UPPER_REPORT_CD=NULL)" +
                         "CONNECT by prior REPORT_CD=UPPER_REPORT_CD ORDER BY ORDER_NO";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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

            #endregion
            var treestr = new BuildTreeStructure().GetTreeHierarch(dt, SearchText);
            List<TreeItems> treeItem = new List<TreeItems>(); //custKeyVal.Value;
            treeItem.Add(new TreeItems()
            {
                Children = treestr.Children,
                Cd = (""),
                UpperCd = (null),
                Label = Utils.GetLabel("Reports"),
                Order = (0)
            });

            return treeItem;
        }

        /// <summary>
        /// Get detail for the selected Report
        /// </summary>
        /// <param name="PositionCd"></param>
        /// <returns>datatable</returns>
        public DataTable GetReportDetail(string RCd)
        {
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select * from MIS_DONOR_REPORT T1";
                    if (RCd.ConvertToString() != "")
                    {
                        cmdText = cmdText + " WHERE T1.REPORT_CD=" + RCd;

                    }
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
            }
            catch (Exception ex)
            {
                dtbl = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }

        /// <summary>
        /// Function to Insert/Update/Delete Report
        /// </summary>
        /// <param name="objMenu"></param>
        /// <param name="mode"></param>
        /// <returns>Bool</returns>
        public bool ReportUID(MISReport objRep, string mode)
        {

            QueryResult qResult = null;
            MisReportInfo objRepEntity = new MisReportInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";
                objRepEntity.ReportCd = objRep.reportCd;
                objRepEntity.DefinedCd = objRep.DefinedCD;
                objRepEntity.UpperReportCd = objRep.upperReportCd;
                objRepEntity.DescEng = objRep.descEng;
                objRepEntity.DescLoc = objRep.descLoc;
                objRepEntity.ReportLink = objRep.reportLink;
                objRepEntity.ReportTitle = objRep.reportTitle;
                objRepEntity.ReportTitleLoc = objRep.reportTitleLoc;
                objRepEntity.OrderNo = objRep.orderNo;
                objRepEntity.Disabled = objRep.disabled;
                objRepEntity.Approved = objRep.Approved;
                objRepEntity.ApprovedBy = objRep.ApprovedBy;
                objRepEntity.ApprovedDt = DateTime.Now.ToString("dd-MMM-yyyy");
                objRepEntity.ApprovedDtLoc = objRep.ApprovedDtLoc;
                objRepEntity.EnteredBy = SessionCheck.getSessionUsername();//objRep.enteredBy;
                objRepEntity.EnteredDt = DateTime.Now.ToString("dd-MMM-yyyy");
                objRepEntity.EnteredDtLoc = objRep.enteredLoc;
                objRepEntity.GroupFlag = objRep.groupFlag;
                objRepEntity.Ipaddress = CommonVariables.IPAddress;
                objRepEntity.Mode = mode;

                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objRepEntity, true);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
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
            return qResult.IsSuccess;

        }

        //Check Duplicate
        public bool CheckDuplicateDefinedCode(string DefinedCode, string ReportCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (ReportCode == string.Empty)
                {
                    cmdText = "select * from MIS_DONOR_REPORT where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_DONOR_REPORT where DEFINED_CD ='" + DefinedCode + "' and REPORT_CD !='" + ReportCode + "'";
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
                    return true;
                }
                return false;
            }
        }
        public DataTable DamageGradeName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_DAMAGE_GRADE WHERE 1=1";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
            return dt;
        }
        public DataTable TechnicalSolutionName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_TECHNICAL_SOLUTION WHERE 1=1";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
            return dt;
        }
    }
}
