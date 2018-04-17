using EntityFramework;
using ExceptionHandler;
using MIS.Models.Donor;
using MIS.Models.Security;
using MIS.Models.Vulnerability;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Services.PartnerOrganization
{
   public class PartnerOrganizationService
    {


        public DataTable GetPartnerOrganizationSupportReport(NameValueCollection paramValues)
        {
            DataTable dt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object pa_number = DBNull.Value;
           
            Object Beneficairy_Type = DBNull.Value;
            Object p_STATUS = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                District = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                Vdc = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                Ward = paramValues["ward"].ConvertToString();
            if (paramValues["pa"].ConvertToString() != string.Empty && paramValues["pa"].ConvertToString() != "undefined")
                pa_number = paramValues["pa"].ConvertToString();

            if(paramValues["benefType"].ConvertToString() != string.Empty && paramValues["benefType"].ConvertToString() != "undefined")
                Beneficairy_Type = paramValues["benefType"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "Pr_po_support",
                        District,
                        Vdc,
                        Ward,
                        pa_number,
                         DBNull.Value,
                         Beneficairy_Type,
                         DBNull.Value
                         
                    );
            }
            catch (OracleException oe)
            {
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

        public DataTable GetOrganizationSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
         
            Object Beneficairy_Type = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                District = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                Vdc = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                Ward = paramValues["ward"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "pr_Benef_Org_Sum_Rpt",
                        District,
                        Vdc,
                        Ward,
                        P_OUT_RESULT
                    );
            }
            catch (OracleException oe)
            {
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

        //

        public DataTable GetPartnerOrganizationReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT = DBNull.Value;
            Object P_NEW_VDC = DBNull.Value;
            Object P_NEW_WARD = DBNull.Value;
            Object P_DONOR_CD = DBNull.Value;
            Object P_PAYROLL_INSTALLMENT_CD = DBNull.Value;
            Object pa_number = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_NEW_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_NEW_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["donor"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_DONOR_CD = paramValues["donor"].ConvertToString();

            if (paramValues["pa"].ConvertToString() != string.Empty && paramValues["pa"].ConvertToString() != "undefined")
                pa_number = paramValues["pa"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_DONOR_SUPPORT_RPT",
                    P_DISTRICT,
                    P_NEW_VDC,
                    P_NEW_WARD,
                     pa_number,
                    P_DONOR_CD,
                    P_PAYROLL_INSTALLMENT_CD,
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



       //summary
       public DataTable GetPartnerOrganizationSummaryReport(NameValueCollection paramValues)
       {
           DataTable dt = null;
           ServiceFactory service = new ServiceFactory();
           Object P_DISTRICT = DBNull.Value;
           Object P_NEW_VDC = DBNull.Value;
           Object P_NEW_WARD = DBNull.Value;
           Object P_DONOR_CD = DBNull.Value;
           Object P_PAYROLL_INSTALLMENT_CD = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty)
               P_DISTRICT = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty)
               P_NEW_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_NEW_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["donor"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_DONOR_CD = paramValues["donor"].ConvertToString();

            try
           {
               service.Begin();
               dt = service.GetDataTable(true, "PR_DONOR_SUPPORT_RPT_SUMMARY",
                   paramValues["lang"].ConvertToString(),
                   P_DISTRICT,
                   P_NEW_VDC,
                   P_NEW_WARD,
                   P_DONOR_CD,
                   P_PAYROLL_INSTALLMENT_CD,
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
