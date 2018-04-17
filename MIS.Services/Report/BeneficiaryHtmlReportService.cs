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
    public class BeneficiaryHtmlReportService
    {
        public DataTable BeneficiaryBySummary(NameValueCollection paramValues)
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

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
       

            if (paramValues["BenfChk"].ConvertToString() != string.Empty && paramValues["BenfChk"].ConvertToString() != "undefined")
                P_BENEFICIARY = paramValues["BenfChk"].ConvertToString();
            if (paramValues["NonBenfChk"].ConvertToString() != string.Empty && paramValues["NonBenfChk"].ConvertToString() != "undefined" && paramValues["NonBenfChk"].ConvertToString() !="null")
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
                dt = service.GetDataTable(true, "PR_GET_BENEF_NON_BENEF_SUMMARY",
                    paramValues["lang"].ConvertToString(),
                    P_BENEFICIARY,
                    P_NON_BENEFICIARY,
                    P_DISTRICT_CD,
                    p_VDC_CURRENT,
                    P_WARD_CURRNT,
                    P_BATCH,
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


        public DataTable SummaryReportByWard(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_BENEFICIARY = DBNull.Value;
            Object P_NON_BENEFICIARY = DBNull.Value;
            Object p_VDC_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();

            if (paramValues["BenfChk"].ConvertToString() != string.Empty)
                P_BENEFICIARY = paramValues["BenfChk"].ConvertToString();
            if (paramValues["NonBenfChk"].ConvertToString() != string.Empty)
                P_NON_BENEFICIARY = paramValues["NonBenfChk"].ConvertToString();

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                p_VDC_CURRENT = paramValues["vdcCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_BENEF_NON_BENEF_BY_WARD",
                    paramValues["lang"].ConvertToString(),
                    P_BENEFICIARY,
                    P_NON_BENEFICIARY,
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
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
        public DataTable BeneficiaryByDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;
             Object P_BATCH = DBNull.Value;
             Object P_SIZE = "100";

             Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
             Object P_WARD_NO_CURRENT = DBNull.Value;
             Object P_exportcheck = DBNull.Value;
             Object P_INDEX = DBNull.Value;

             if (paramValues["checkexport"].ConvertToString() != string.Empty)
                 P_exportcheck = paramValues["checkexport"].ConvertToString();

            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }
             //Object P_INDEX="1";

             //Object P_SIZE = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            if (paramValues["Batch"].ConvertToString() != string.Empty && paramValues["Batch"].ConvertToString() != "undefined")
                P_BATCH = paramValues["Batch"].ConvertToString();

            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
                P_COORDINATES = paramValues["Coordinate"].ConvertToString();



            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_HOUSE_OWNER_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES,
                    P_DISTRICT_CD ,
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
                    P_BATCH,
                      P_SIZE,
                   P_INDEX,
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



        public DataTable BeneficiaryByDetailForExport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;
             Object P_BATCH = DBNull.Value;
            

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            if (paramValues["Batch"].ConvertToString() != string.Empty && paramValues["Batch"].ConvertToString() != "undefined")
                P_BATCH = paramValues["Batch"].ConvertToString();

            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
                P_COORDINATES = paramValues["Coordinate"].ConvertToString();



         

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_HOUSE_OWNER_DTAIL_RPTFEXPT",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES,
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
                    
                    P_BATCH,
                     
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


        public DataTable NonBeneficiaryByDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;
            Object P_SIZE = DBNull.Value;

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object P_exportcheck = DBNull.Value;

            if (paramValues["checkexport"].ConvertToString() != string.Empty)
                P_exportcheck = paramValues["checkexport"].ConvertToString();
            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }



            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();


          
            
            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
                P_COORDINATES = paramValues["Coordinate"].ConvertToString();

            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_NON_BENEFECIARY_DETAIL",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES,
                    P_DISTRICT_CD, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,

                     P_SIZE,
                   P_INDEX,
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






        public DataTable NonBeneficiaryByDetailForExport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;
          

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();


          
            
            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
                P_COORDINATES = paramValues["Coordinate"].ConvertToString();

          

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_NON_BENEFECARY_DTLFEXPT",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES,
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                  
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
        public DataTable BeneficiaryDetailByGender(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_GENDER_CD = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["gender"].ConvertToString() != string.Empty)
                P_GENDER_CD = paramValues["gender"].ConvertToString();         
            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
               P_COORDINATES = paramValues["Coordinate"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_BENEF_DTL_BY_GENDER",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES.ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                    P_GENDER_CD.ToDecimal()  ,                  
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

        public DataTable NonBeneficiaryDetailByGender(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_GENDER_CD = DBNull.Value;
            Object P_COORDINATES = DBNull.Value;


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["gender"].ConvertToString() != string.Empty)
                P_GENDER_CD = paramValues["gender"].ConvertToString();

            if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
            P_COORDINATES = paramValues["Coordinate"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_NONBENEF_DTL_BY_GENDER",
                    paramValues["lang"].ConvertToString(),
                    P_COORDINATES,
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                    P_GENDER_CD.ToDecimal(),                    
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
        public DataTable BenefNonBenefSummaryByGender(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_GENDER_CD = DBNull.Value;
            //Object P_COORDINATES = DBNull.Value;
            Object P_BENEF_CHK = DBNull.Value;
            Object P_NONBENEF_CHK = DBNull.Value;

            if (paramValues["BenfChk"].ConvertToString() != string.Empty)
                P_BENEF_CHK = paramValues["BenfChk"].ConvertToString();
            if (paramValues["NonBenfChk"].ConvertToString() != string.Empty)
                P_NONBENEF_CHK = paramValues["NonBenfChk"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["gender"].ConvertToString() != string.Empty)
                P_GENDER_CD = paramValues["gender"].ConvertToString();
           
            //if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
            //    P_COORDINATES = paramValues["Coordinate"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_BENEFNONBENEF_SUM_BY_GENDER",
                    paramValues["lang"].ConvertToString(),
                    P_BENEF_CHK,
                    P_NONBENEF_CHK,
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                   // P_COORDINATES,
                   
                    P_GENDER_CD.ToDecimal(),
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

        public DataTable GetGrievanceBeneficiaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object p_Batch = DBNull.Value;

             Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;


            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
        
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
         
            if (paramValues["Batch"].ConvertToString() != string.Empty)
                p_Batch = paramValues["Batch"].ConvertToString();


           

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "get_grievance_beneficiary",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD, 
                    p_Batch, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
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

       
        public DataTable GetTotalBeneficiaryDetailReport(NameValueCollection paramValues) //getting datatable for total benef. detail report
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            
            //Object P_NRA_DEFINED_CD = DBNull.Value;

            Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object p_Batch = DBNull.Value;
            
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();

            if (paramValues["Batch"].ConvertToString() != string.Empty)
                p_Batch = paramValues["Batch"].ConvertToString();

            //if (paramValues["dist"].ConvertToString() != string.Empty)
            //    P_NRA_DEFINED_CD = paramValues["nraDefinedCd"].ConvertToString();

            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "pr_benef_all",
                     paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD,
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

        //end
        public DataTable GetTotalBeneficiarySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object p_dist = DBNull.Value;                      
            Object p_gp_np = DBNull.Value;
            Object p_ward = DBNull.Value;
            Object p_Batch = DBNull.Value;
            
           


            if (paramValues["dist"].ConvertToString() != string.Empty)
                p_dist = paramValues["dist"].ConvertToString();

            if (paramValues["Batch"].ConvertToString() != string.Empty)
                p_Batch = paramValues["Batch"].ConvertToString();


          


            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                p_gp_np = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                p_ward = paramValues["wardCurr"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_PA_location_summary",
                    paramValues["lang"].ConvertToString(),
                    p_dist,       
                    p_gp_np,
                    p_ward,                 
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

        

        public DataTable GetGrievanceBeneficiarySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object p_Batch = DBNull.Value;

            Object P_WARD_CURRNT  = DBNull.Value;
            Object p_VDC_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
             
            if (paramValues["Batch"].ConvertToString() != string.Empty)
                p_Batch = paramValues["Batch"].ConvertToString();


            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                p_VDC_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_CURRNT = paramValues["wardCurr"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GR_BENEF_SUMMARY_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD, 
                    p_VDC_CURRENT,
                    P_WARD_CURRNT, 
                    p_Batch,
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
        //public DataTable NonBeneficiarySummaryByGender(NameValueCollection paramValues)
        //{
        //    DataTable dt = null;
        //    ServiceFactory service = new ServiceFactory();
        //    Object P_DISTRICT_CD = DBNull.Value;
        //    Object P_VDC_CD = DBNull.Value;
        //    Object P_WARD = DBNull.Value;
        //    Object P_GENDER_CD = DBNull.Value;
        //    Object P_COORDINATES = DBNull.Value;


        //    if (paramValues["dist"].ConvertToString() != string.Empty)
        //        P_DISTRICT_CD = paramValues["dist"].ConvertToString();
        //    if (paramValues["vdc"].ConvertToString() != string.Empty)
        //        P_VDC_CD = paramValues["vdc"].ConvertToString();
        //    if (paramValues["ward"].ConvertToString() != string.Empty)
        //        P_WARD = paramValues["ward"].ConvertToString();
        //    if (paramValues["gender"].ConvertToString() != string.Empty)
        //        P_GENDER_CD = paramValues["gender"].ConvertToString();

        //    if (paramValues["Coordinate"].ConvertToString() != string.Empty && paramValues["Coordinate"].ConvertToString() != "undefined")
        //        P_COORDINATES = paramValues["Coordinate"].ConvertToString();

        //    try
        //    {
        //        service.Begin();
        //        dt = service.GetDataTable(true, "PR_GET_NONBENEF_SUM_BY_GENDER",
        //            paramValues["lang"].ConvertToString(),
        //            P_COORDINATES,
        //            P_DISTRICT_CD,
        //            P_VDC_CD,
        //            P_WARD,
        //            P_GENDER_CD.ToDecimal(),
        //            DBNull.Value);
        //    }
        //    catch (OracleException oe)
        //    {
        //        dt = null;
        //        ExceptionManager.AppendLog(oe);
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = null;
        //        ExceptionManager.AppendLog(ex);
        //    }
        //    finally
        //    {
        //        if (service.Transaction != null)
        //        {
        //            service.End();
        //        }
        //    }
        //    return dt;
        //}
    }
}
