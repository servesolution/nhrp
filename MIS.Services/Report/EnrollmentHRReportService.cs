﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;


namespace MIS.Services.Report
{
    public class EnrollmentHRReportService
    {
        public DataTable GetEnrollmentHRReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_DATE_FROM = DBNull.Value;
            Object P_DATE_TO = DBNull.Value;



            if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DateFrom"].ConvertToString() != string.Empty)
                P_DATE_FROM = paramValues["DateFrom"].ConvertToString();
            if (paramValues["DateTo"].ConvertToString() != string.Empty)
                P_DATE_TO = paramValues["DateTo"].ConvertToString();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //service.Begin();
                   // dt = service.GetDataTable(true, "GR_HR_ENROLLMENT_REPORT",
                   // paramValues["lang"].ConvertToString(),
                   // P_Dist,
                   // P_VDC,
                   // P_WARD,
                   // P_DATE_FROM,
                   // P_DATE_TO,
                   //DBNull.Value
                   //);
                    dt = null;

                }
                catch (OracleException oe)
                {
                    service.RollBack();
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
                        dt = null;
                        service.End();
                    }
                }

            }
            return dt;
        }
    }
}