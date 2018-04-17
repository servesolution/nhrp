using EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Report
{
    public class HouseholdHtmlReportService
    {
        public DataTable HouseholdSummaryReportByMonthlyIncome(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
         
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            if (paramValues["MonthlyIncome"].ConvertToString() != string.Empty && paramValues["MonthlyIncome"].ConvertToString() != "undefined")
                P_MONTHLY_INCOME = paramValues["MonthlyIncome"].ConvertToString();
          
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_HOUSEHOLD_SUMMARY",
                    paramValues["lang"].ConvertToString(),
                    P_MONTHLY_INCOME,
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
                   
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
        public DataTable HouseholdDetailReportByMonthlyIncome(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            Object P_NISSA_NO = DBNull.Value;
            Object P_HOUSE_SERIAL = DBNull.Value;
            Object P_HOUSE_OWNER = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            if (paramValues["MonthlyIncome"].ConvertToString() != string.Empty && paramValues["MonthlyIncome"].ConvertToString() != "undefined")
                P_MONTHLY_INCOME = paramValues["MonthlyIncome"].ConvertToString();
            if (paramValues["NissaNo"].ConvertToString() != string.Empty && paramValues["NissaNo"].ConvertToString() != "undefined")
                P_NISSA_NO = paramValues["NissaNo"].ConvertToString();
            if (paramValues["HouseSerialNo"].ConvertToString() != string.Empty && paramValues["HouseSerialNo"].ConvertToString() != "undefined")
                P_HOUSE_SERIAL = paramValues["HouseSerialNo"].ConvertToString();
            if (paramValues["HouseOwner"].ConvertToString() != string.Empty && paramValues["HouseOwner"].ConvertToString() != "undefined")
                P_HOUSE_OWNER = paramValues["HouseOwner"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_HOUSEHOLD_DETAILS",
                    paramValues["lang"].ConvertToString(),
                    P_MONTHLY_INCOME,
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
                    P_NISSA_NO,
                    P_HOUSE_OWNER,
                    P_HOUSE_SERIAL,                   
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
    }
}
