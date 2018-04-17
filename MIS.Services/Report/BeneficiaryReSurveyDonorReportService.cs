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
    public class BeneficiaryReSurveyDonorReportService
    {
        //Re-survey Reconstruction beneficiary
        public DataTable GetReSurveyBeneficiaryBySummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_BENEFICIARY = DBNull.Value;
            Object P_NON_BENEFICIARY = DBNull.Value;
            Object P_BATCH = DBNull.Value;


            Object p_VDC_CURRENT = DBNull.Value;
            Object P_WARD_CURRNT = DBNull.Value;
            Object P_OUT_RESULT = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();


            if (paramValues["BenfChk"].ConvertToString() != string.Empty && paramValues["BenfChk"].ConvertToString() != "undefined")
                P_BENEFICIARY = paramValues["BenfChk"].ConvertToString();
            if (paramValues["NonBenfChk"].ConvertToString() != string.Empty && paramValues["NonBenfChk"].ConvertToString() != "undefined" && paramValues["NonBenfChk"].ConvertToString() != "null")
                P_NON_BENEFICIARY = paramValues["NonBenfChk"].ConvertToString();
            if (paramValues["Batch"].ConvertToString() != string.Empty)
                P_BATCH = paramValues["Batch"].ConvertToString();

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                p_VDC_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_CURRNT = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_BENEF_SUM_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    p_VDC_CURRENT,
                    P_WARD_CURRNT,
                    P_OUT_RESULT);
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
