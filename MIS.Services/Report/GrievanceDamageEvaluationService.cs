using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Entity;
using EntityFramework;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Models.Setup;
using System.Data;
using MIS.Services.Core;
using MIS.Models.Training;
using System.Configuration;
using System.Data.OleDb;
using System.Collections;
using System.Web.Mvc;


namespace MIS.Services.Report
{
    public class GrievanceDamageEvaluationService
    {
        CommonFunction commFunction = new CommonFunction();

        public DataTable GetDetailDamageGradeEvaluationReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_CurrVDC = DBNull.Value;
            Object P_CurrWard = DBNull.Value;
            Object P_HouseOwner = DBNull.Value;
            Object p_SlipNo = DBNull.Value;
            Object P_gid = DBNull.Value;
            Object P_RS_RV = DBNull.Value;
            Object P_REC = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["HouseOwner"].ConvertToString() != string.Empty)
                P_HouseOwner = paramValues["HouseOwner"].ConvertToString();
            if (paramValues["SlipNo"].ConvertToString() != string.Empty)
                p_SlipNo = paramValues["SlipNo"].ConvertToString();
            if (paramValues["Gid"].ConvertToString() != string.Empty)
                P_gid = paramValues["Gid"].ConvertToString();
            if (paramValues["currentVdc"].ConvertToString() != string.Empty)
                P_CurrVDC = paramValues["currentVdc"].ConvertToString();
            if (paramValues["currentWard"].ConvertToString() != string.Empty)
                P_CurrWard = paramValues["currentWard"].ConvertToString();
            if (paramValues["rs_rv_type"].ConvertToString() != string.Empty)
                P_RS_RV = paramValues["rs_rv_type"].ConvertToString();
            if (paramValues["rec"].ConvertToString() != string.Empty)
                P_REC = paramValues["rec"].ConvertToString();


            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_ENG_VERIFICATION_RPT",
                                        P_Dist,
                                        P_CurrVDC,
                                        P_CurrWard,
                                        P_HouseOwner,
                                        p_SlipNo,
                                        P_gid,
                                        DBNull.Value,
                                        P_RS_RV,
                                        P_REC,
                                        DBNull.Value);
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
                    sf.End();
                }
            }

            return dt;

        }

        public DataTable GetDetailDamageGradeEvaluationSumReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_CurrVDC = DBNull.Value;
            Object P_CurrWard = DBNull.Value;
            Object P_RS_RV = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["currentVdc"].ConvertToString() != string.Empty)
                P_CurrVDC = paramValues["currentVdc"].ConvertToString();
            if (paramValues["currentWard"].ConvertToString() != string.Empty)
                P_CurrWard = paramValues["currentWard"].ConvertToString();
            if (paramValues["rs_rv_type"].ConvertToString() != string.Empty)
                P_RS_RV = paramValues["rs_rv_type"].ConvertToString();



            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_ENG_VERIFICATION_SUM_RPT",
                                        P_Dist,
                                        P_CurrVDC,
                                        P_CurrWard,
                                        P_RS_RV,
                                        DBNull.Value);
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
                    sf.End();
                }
            }

            return dt;

        }
    }
}
