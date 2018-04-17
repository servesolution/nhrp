using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;

namespace MIS.Services.NHRP.View
{
    public class HouseHoldMemberViewService
    {
        public DataSet HouseHoldMemberDetail(string householdid, string ownerId, string strNumber)
        {
            DataSet dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                //cmdText = "SELECT * FROM VW_HOUSEHOLD_MEMBER WHERE HOUSEHOLD_ID = '" + housedoldDetail + "'";//+ " AND DEFINED_CD = " + definedCd  169  ;
                try
                {
                    service.Begin();
                    //dt = service.GetDataTable(new
                    //{
                    //    query = cmdText,
                    //    args = new
                    //    {

                    //    }
                    //});
                    string resPackageName = service.PackageName;
                    service.PackageName = "PKG_NHRS_VIEWS";
                    dt = service.GetDataSetOracle(true, "PR_HOUSEHOLD_MEMBER_VIEW", ownerId.ToDecimal(), strNumber.ToDecimal(), householdid, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
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

        

        public DataTable GetOwnerOtherDetail(string ownerId)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "SELECT nhom.DEFINED_CD ONA_ID,COM_PKG_UTIL.FN_GET_KLL_DISTRICT(nhom.DISTRICT_CD) KLL_DISTRICT_CD,nhom.SUBMISSIONTIME  FROM NHRS_HOUSE_OWNER_MST nhom WHERE nhom.HOUSE_OWNER_ID= '" + ownerId + "'";

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

        public DataTable HouseIndicatorNames()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM MIS_HOUSEHOLD_INDICATOR";
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
        public DataTable ReliefMoneyNames()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS_EQ_RELIEF_MONEY";
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


        public DataTable MemberDetail()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_MEMBER_FAMILY_DTL WHERE HOUSEHOLD_ID = " + "165"; //+ " AND DEFINED_CD = " + definedCd  169  ;
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

        public DataTable MemberDeathDetail()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_MEMBER_DEATH_DTL WHERE HOUSEHOLD_ID = " + "165"; //+ " AND DEFINED_CD = " + definedCd  169  ;
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
        public DataTable MemberDisapperedDisabledDetail()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_MEMBER_HUMAN_DISTROY_DTL WHERE HOUSEHOLD_ID = " + "165"; //+ " AND DEFINED_CD = " + definedCd  169  ;
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
