using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
  
namespace MIS.Services.BusinessCalculation
{
    public class OwnerSummaryService
    {
        CommonFunction commFunction = new CommonFunction();

        public DataTable GetHouseOwnerReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_OwnerCount = DBNull.Value;
            Object P_OwnerCountCd = DBNull.Value;
            Object P_Gender = DBNull.Value;
            Object P_Respondend = DBNull.Value;
            Object P_WithinEc = DBNull.Value;
            Object P_WithinEcCd = DBNull.Value;
            Object P_WithoutEc = DBNull.Value;
            Object P_WithoutEcCd = DBNull.Value;
            Object P_PhoneNo = DBNull.Value;
            Object P_Grievence = DBNull.Value;
            Object P_IsBen = DBNull.Value;
            Object P_Ben = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["OwnerCount"].ConvertToString() != string.Empty)
                P_OwnerCount = paramValues["OwnerCount"].ConvertToString();
            if (paramValues["OwnerCountCd"].ConvertToString() != string.Empty)
                P_OwnerCount = paramValues["OwnerCountCd"].ConvertToString();
            if (paramValues["Gender"].ConvertToString() != string.Empty)
                P_Gender = paramValues["Gender"].ConvertToString();
            if (paramValues["Respondend"].ConvertToString() != string.Empty)
                P_Respondend = paramValues["Respondend"].ConvertToString();
            if (paramValues["WithinEc"].ConvertToString() != string.Empty)
                P_WithinEc = paramValues["WithinEc"].ConvertToString();
            if (paramValues["WithinEcCd"].ConvertToString() != string.Empty)
                P_WithinEc = paramValues["WithinEcCd"].ConvertToString();
            if (paramValues["WithoutEc"].ConvertToString() != string.Empty)
                P_WithoutEc = paramValues["WithoutEc"].ConvertToString();
            if (paramValues["WithoutEcCd"].ConvertToString() != string.Empty)
                P_WithoutEc = paramValues["WithoutEcCd"].ConvertToString();
            if (paramValues["PhoneNo"].ConvertToString() != string.Empty)
                P_PhoneNo = paramValues["PhoneNo"].ConvertToString();
            if (paramValues["Grievence"].ConvertToString() != string.Empty)
                P_Grievence = paramValues["Grievence"].ConvertToString();
            if (paramValues["IsBen"].ConvertToString() != string.Empty)
                P_IsBen = paramValues["IsBen"].ConvertToString();
            if (paramValues["Ben"].ConvertToString() != string.Empty)
                P_Ben = paramValues["Ben"].ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_HOUSE_OWNER_ADHOC",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_Ward,
                    P_OwnerCount,
                    P_OwnerCountCd,
                    P_Gender,
                    P_Respondend,
                    P_WithinEc,
                    P_WithinEcCd,
                    P_WithoutEc,
                    P_WithoutEcCd,
                    P_PhoneNo,
                    P_Grievence,
                    P_IsBen,
                    P_Ben,
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


        public DataTable GetDamageSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();


            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_DAMAGE_SUMMARY",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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

        public DataTable GetInspectionSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_BUILDING_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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
        public DataTable GetInspectionReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_BUILDING_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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


        public DataTable GetBuildingReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_BUILDING_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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
        //public DataTable HouseholdGradeDetail(NameValueCollection paramValues)
        //{
        //    DataTable dt = null;
        //    ServiceFactory service = new ServiceFactory();
        //    Object P_Dist = DBNull.Value;
        //    Object P_VDC = DBNull.Value;
        //    Object P_WARD = DBNull.Value;
        //    Object P_OTHHOUSEFLAG = DBNull.Value;
        //    Object P_DAMAGE_GRADE = DBNull.Value;
        //    Object P_TECHNICAL_SOLUTION = DBNull.Value;
        //    Object P_BUILDING_CONDTION = DBNull.Value;
        //    if (paramValues["dist"].ConvertToString() != string.Empty)
        //        P_Dist = paramValues["dist"].ConvertToString();
        //    if (paramValues["vdc"].ConvertToString() != string.Empty)
        //        P_VDC = paramValues["vdc"].ConvertToString();
        //    if (paramValues["ward"].ConvertToString() != string.Empty)
        //        P_WARD = paramValues["ward"].ConvertToString();
        //    if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
        //        P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
        //    if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
        //        P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
        //    if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
        //        P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
        //    if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
        //        P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();
        //    try
        //    {
        //        service.Begin();
        //        dt = service.GetDataTable(true, "PR_HOUSEHOLD_DAMAGE_REPORT",
        //            paramValues["lang"].ConvertToString(),
        //            P_Dist,
        //            P_VDC,
        //            P_WARD,
        //            P_DAMAGE_GRADE,
        //            P_TECHNICAL_SOLUTION,
        //            P_OTHHOUSEFLAG,
        //            P_BUILDING_CONDTION,
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
        public DataTable GetBeneficiaryByWard(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_BENEFECIARY_BY_WARD",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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

        public DataTable GetNonBeneficiaryDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["DGrade"].ConvertToString() != string.Empty)
                P_WARD = paramValues["DGrade"].ConvertToString();
            if (paramValues["TechSln"].ConvertToString() != string.Empty)
                P_WARD = paramValues["TechSln"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_NON_BENEFECIARY_DETAIL",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
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

        public DataTable GetEnrolledBeneficiary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["batchId"].ConvertToString() != string.Empty)
                P_BATCH = paramValues["batchId"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_HOUSE_OWNER_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_BATCH,
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

        public DataTable GetEnrolledByIdentification(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["batchId"].ConvertToString() != string.Empty)
                P_BATCH = paramValues["batchId"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_HOUSE_OWNER_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_BATCH,
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

        public DataTable GetBeneficiaryOwner(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["batchId"].ConvertToString() != string.Empty)
                P_BATCH = paramValues["batchId"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_HOUSE_OWNER_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_BATCH,
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

        public DataTable GrievanceSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_CASE_TYPE = DBNull.Value;
            Object P_LEGAL_OWNER = DBNull.Value;
            Object P_OTHER_HOUSE = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["CaseType"].ConvertToString() != string.Empty)
                P_CASE_TYPE = paramValues["CaseType"].ConvertToString();
            if (paramValues["LegalOWner"].ConvertToString() != string.Empty)
                P_LEGAL_OWNER = paramValues["LegalOWner"].ConvertToString();
            if (paramValues["OtherHouse"].ConvertToString() != string.Empty)
                P_OTHER_HOUSE = paramValues["OtherHouse"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_GRIEVANCE_HTML_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_LEGAL_OWNER,
                    P_CASE_TYPE,
                    P_OTHER_HOUSE,
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

        public DataTable GetGrievanceSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_CASE_TYPE = DBNull.Value;
            Object P_LEGAL_OWNER = DBNull.Value;
            Object P_OTHER_HOUSE = DBNull.Value;
            Object P_DOC_TYPE = DBNull.Value;
            Object P_CASE_FLAG = DBNull.Value;
            Object P_LEGAL_OWNER_FLAG = DBNull.Value;
            Object P_OTHER_HOUSE_FLAG = DBNull.Value;
            Object P_DOC_TYPE_FLAG = DBNull.Value;

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
                P_Dist = paramValues["dist"].ConvertToString();
             
            if (paramValues["CaseType"].ConvertToString() != string.Empty && paramValues["CaseType"].ConvertToString() != "undefined")
                P_CASE_TYPE = paramValues["CaseType"].ToDecimal();
            if (paramValues["LegalOWner"].ConvertToString() != string.Empty && paramValues["LegalOWner"].ConvertToString() != "undefined")
                P_LEGAL_OWNER = paramValues["LegalOWner"].ConvertToString();
            if (paramValues["OtherHouse"].ConvertToString() != string.Empty && paramValues["OtherHouse"].ConvertToString() != "undefined")
                P_OTHER_HOUSE = paramValues["OtherHouse"].ConvertToString();
            if (paramValues["DocType"].ConvertToString() != string.Empty && paramValues["DocType"].ConvertToString() != "undefined")
                P_DOC_TYPE = paramValues["DocType"].ConvertToString();

            if (paramValues["CaseTypeFlag"].ConvertToString() != string.Empty && paramValues["CaseTypeFlag"].ConvertToString() != "undefined")
                P_CASE_FLAG = paramValues["CaseTypeFlag"].ConvertToString();
            if (paramValues["LegalOwnerFlag"].ConvertToString() != string.Empty && paramValues["LegalOwnerFlag"].ConvertToString() != "undefined")
                P_LEGAL_OWNER_FLAG = paramValues["LegalOwnerFlag"].ConvertToString();
            if (paramValues["OtherHouseFlag"].ConvertToString() != string.Empty && paramValues["OtherHouseFlag"].ConvertToString() != "undefined")
                P_OTHER_HOUSE_FLAG = paramValues["OtherHouseFlag"].ConvertToString();
            if (paramValues["DocTypeFlag"].ConvertToString() != string.Empty && paramValues["DocTypeFlag"].ConvertToString() != "undefined")
                P_DOC_TYPE_FLAG = paramValues["DocTypeFlag"].ConvertToString();

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "GET_CASE_GRIEVANCE_REPORTS",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT, 
                    P_LEGAL_OWNER,
                    P_CASE_FLAG,
                    P_CASE_TYPE,
                    P_DOC_TYPE_FLAG,
                    P_DOC_TYPE,
                    P_LEGAL_OWNER_FLAG,
                    P_LEGAL_OWNER,
                    P_OTHER_HOUSE_FLAG,
                    P_OTHER_HOUSE,
                    DBNull.Value);

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
                        service.End();
                    }
                }

            }
            return dt;
        }

        public DataTable GrievanceDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_CASE_TYPE = DBNull.Value;
            Object P_LEGAL_OWNER = DBNull.Value;
            Object P_OTHER_HOUSE = DBNull.Value;
            Object P_DOC_TYPE = DBNull.Value;
            Object P_CASE_FLAG = DBNull.Value;
            Object P_LEGAL_OWNER_FLAG = DBNull.Value;
            Object P_OTHER_HOUSE_FLAG = DBNull.Value;
            Object P_DOC_TYPE_FLAG = DBNull.Value;

         Object P_SIZE = DBNull.Value;

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;

            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object P_exportcheck = DBNull.Value;

            if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
                P_exportcheck = paramValues["exportcheck"].ConvertToString();

            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }


            if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
                P_Dist = paramValues["dist"].ConvertToString();
            
            if (paramValues["CaseType"].ConvertToString() != string.Empty && paramValues["CaseType"].ConvertToString() != "undefined")
                P_CASE_TYPE = paramValues["CaseType"].ConvertToString();
            if (paramValues["LegalOWner"].ConvertToString() != string.Empty && paramValues["LegalOWner"].ConvertToString() != "undefined")
                P_LEGAL_OWNER = paramValues["LegalOWner"].ConvertToString();
            if (paramValues["OtherHouse"].ConvertToString() != string.Empty && paramValues["OtherHouse"].ConvertToString() != "undefined")
                P_OTHER_HOUSE = paramValues["OtherHouse"].ConvertToString();
            if (paramValues["DocType"].ConvertToString() != string.Empty && paramValues["DocType"].ConvertToString() != "undefined")
            {
                P_DOC_TYPE = paramValues["DocType"].ConvertToString();
                P_DOC_TYPE = commFunction.GetCodeFromDataBase(P_DOC_TYPE.ConvertToString(), "NHRS_GRIEVANCE_DOC_TYPE", "DOC_TYPE_CD");
            }
            if (paramValues["CaseTypeFlag"].ConvertToString() != string.Empty && paramValues["CaseTypeFlag"].ConvertToString() != "undefined")
                P_CASE_FLAG = paramValues["CaseTypeFlag"].ConvertToString();
            if (paramValues["LegalOwnerFlag"].ConvertToString() != string.Empty && paramValues["LegalOwnerFlag"].ConvertToString() != "undefined")
                P_LEGAL_OWNER_FLAG = paramValues["LegalOwnerFlag"].ConvertToString();
            if (paramValues["OtherHouseFlag"].ConvertToString() != string.Empty && paramValues["OtherHouseFlag"].ConvertToString() != "undefined")
                P_OTHER_HOUSE_FLAG = paramValues["OtherHouseFlag"].ConvertToString();
            if (paramValues["DocTypeFlag"].ConvertToString() != string.Empty && paramValues["DocTypeFlag"].ConvertToString() != "undefined")
                P_DOC_TYPE_FLAG = paramValues["DocTypeFlag"].ConvertToString();

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

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "GET_GRIEVANCE_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT, 
                    P_CASE_FLAG,
                    P_CASE_TYPE,
                    P_DOC_TYPE_FLAG,
                    P_DOC_TYPE,
                    P_LEGAL_OWNER_FLAG,
                    P_LEGAL_OWNER,
                    P_OTHER_HOUSE_FLAG,
                    P_OTHER_HOUSE,
                    P_SIZE,
                    P_INDEX,
                    DBNull.Value);

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
                        service.End();
                    }
                }

            }
            return dt;

        }


        public DataTable GrievanceDetailFOREXPORT(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_CASE_TYPE = DBNull.Value;
            Object P_LEGAL_OWNER = DBNull.Value;
            Object P_OTHER_HOUSE = DBNull.Value;
            Object P_DOC_TYPE = DBNull.Value;
            Object P_CASE_FLAG = DBNull.Value;
            Object P_LEGAL_OWNER_FLAG = DBNull.Value;
            Object P_OTHER_HOUSE_FLAG = DBNull.Value;
            Object P_DOC_TYPE_FLAG = DBNull.Value;

          
            if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["CaseType"].ConvertToString() != string.Empty && paramValues["CaseType"].ConvertToString() != "undefined")
                P_CASE_TYPE = paramValues["CaseType"].ConvertToString();
            if (paramValues["LegalOWner"].ConvertToString() != string.Empty && paramValues["LegalOWner"].ConvertToString() != "undefined")
                P_LEGAL_OWNER = paramValues["LegalOWner"].ConvertToString();
            if (paramValues["OtherHouse"].ConvertToString() != string.Empty && paramValues["OtherHouse"].ConvertToString() != "undefined")
                P_OTHER_HOUSE = paramValues["OtherHouse"].ConvertToString();
            if (paramValues["DocType"].ConvertToString() != string.Empty && paramValues["DocType"].ConvertToString() != "undefined")
            {
                P_DOC_TYPE = paramValues["DocType"].ConvertToString();
                P_DOC_TYPE = commFunction.GetCodeFromDataBase(P_DOC_TYPE.ConvertToString(), "NHRS_GRIEVANCE_DOC_TYPE", "DOC_TYPE_CD");
            }
            if (paramValues["CaseTypeFlag"].ConvertToString() != string.Empty && paramValues["CaseTypeFlag"].ConvertToString() != "undefined")
                P_CASE_FLAG = paramValues["CaseTypeFlag"].ConvertToString();
            if (paramValues["LegalOwnerFlag"].ConvertToString() != string.Empty && paramValues["LegalOwnerFlag"].ConvertToString() != "undefined")
                P_LEGAL_OWNER_FLAG = paramValues["LegalOwnerFlag"].ConvertToString();
            if (paramValues["OtherHouseFlag"].ConvertToString() != string.Empty && paramValues["OtherHouseFlag"].ConvertToString() != "undefined")
                P_OTHER_HOUSE_FLAG = paramValues["OtherHouseFlag"].ConvertToString();
            if (paramValues["DocTypeFlag"].ConvertToString() != string.Empty && paramValues["DocTypeFlag"].ConvertToString() != "undefined")
                P_DOC_TYPE_FLAG = paramValues["DocTypeFlag"].ConvertToString();

           

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "GET_GRIEVANCE_DETAIL_RPTFEXPT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_CASE_FLAG,
                    P_CASE_TYPE,
                    P_DOC_TYPE_FLAG,
                    P_DOC_TYPE,
                    P_LEGAL_OWNER_FLAG,
                    P_LEGAL_OWNER,
                    P_OTHER_HOUSE_FLAG,
                    P_OTHER_HOUSE,
                     
                    DBNull.Value);

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
                        service.End();
                    }
                }

            }
            return dt;

        }
        public DataTable GrievanceHandlingSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            //Object P_CASE_TYPE_FLAG = DBNull.Value;
            Object P_DAMAGE_GRADE_FLAG = DBNull.Value;
            //Object P_OTHER_HOUSE_FLAG = DBNull.Value;
            Object P_TECHNICAL_SOLUTION_FLAG = DBNull.Value;
            Object P_SURVEYED_GRADE = DBNull.Value;
            Object P_PHOTO_GRADE = DBNull.Value;
            Object P_MATRIX_GRADE = DBNull.Value;
            Object P_RECOMMENDED_GRADE = DBNull.Value;
            Object P_SURVEYED_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_RECOMMENDED_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_DG1 = DBNull.Value;
            Object P_DG2 = DBNull.Value;
            Object P_DG3 = DBNull.Value;
            Object P_DG4 = DBNull.Value;
            Object P_DG5 = DBNull.Value;
            Object P_DG6 = DBNull.Value;
            Object P_TS1 = DBNull.Value;
            Object P_TS2 = DBNull.Value;
            Object P_TS3 = DBNull.Value;
            Object P_TS4 = DBNull.Value;
            Object P_TS5 = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["SurveyedGrade"].ConvertToString() != string.Empty && paramValues["SurveyedGrade"].ConvertToString() != "undefined")
                P_SURVEYED_GRADE = paramValues["SurveyedGrade"].ConvertToString();
            if (paramValues["PhotoGrade"].ConvertToString() != string.Empty && paramValues["PhotoGrade"].ConvertToString() != "undefined")
                P_PHOTO_GRADE = paramValues["PhotoGrade"].ConvertToString();
            if (paramValues["MatrixGarde"].ConvertToString() != string.Empty && paramValues["MatrixGarde"].ConvertToString() != "undefined")
                P_MATRIX_GRADE = paramValues["MatrixGarde"].ConvertToString();
            if (paramValues["RecommendedGrade"].ConvertToString() != string.Empty && paramValues["RecommendedGrade"].ConvertToString() != "undefined")
                P_RECOMMENDED_GRADE = paramValues["RecommendedGrade"].ConvertToString();
            if (paramValues["SurveyedTechnicalSolution"].ConvertToString() != string.Empty && paramValues["SurveyedTechnicalSolution"].ConvertToString() != "undefined")
                P_SURVEYED_TECHNICAL_SOLUTION = paramValues["SurveyedTechnicalSolution"].ConvertToString();
            if (paramValues["RecommendedTechnicalSolution"].ConvertToString() != string.Empty && paramValues["RecommendedTechnicalSolution"].ConvertToString() != "undefined")
                P_RECOMMENDED_TECHNICAL_SOLUTION = paramValues["RecommendedTechnicalSolution"].ConvertToString();
            if (paramValues["DG1"].ConvertToString() != string.Empty && paramValues["DG1"].ConvertToString() != "undefined")
                P_DG1 = "Y";
            if (paramValues["DG2"].ConvertToString() != string.Empty && paramValues["DG2"].ConvertToString() != "undefined")
                P_DG2 = "Y";
            if (paramValues["DG3"].ConvertToString() != string.Empty && paramValues["DG3"].ConvertToString() != "undefined")
                P_DG3 = "Y";
            if (paramValues["DG4"].ConvertToString() != string.Empty && paramValues["DG4"].ConvertToString() != "undefined")
                P_DG4 = "Y";
            if (paramValues["DG5"].ConvertToString() != string.Empty && paramValues["DG5"].ConvertToString() != "undefined")
                P_DG5 = "Y";
            if (paramValues["DG6"].ConvertToString() != string.Empty && paramValues["DG6"].ConvertToString() != "undefined")
                P_DG6 = "Y";
            if (paramValues["TS1"].ConvertToString() != string.Empty && paramValues["TS1"].ConvertToString() != "undefined")
                P_TS1 = "Y";
            if (paramValues["TS2"].ConvertToString() != string.Empty && paramValues["TS2"].ConvertToString() != "undefined")
                P_TS2 = "Y";
            if (paramValues["TS3"].ConvertToString() != string.Empty && paramValues["TS3"].ConvertToString() != "undefined")
                P_TS3 = "Y";
            if (paramValues["TS4"].ConvertToString() != string.Empty && paramValues["TS4"].ConvertToString() != "undefined")
                P_TS4 = "Y";
            if (paramValues["TS5"].ConvertToString() != string.Empty && paramValues["TS5"].ConvertToString() != "undefined")
                P_TS5 = "Y";
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GRIEVANCE_HANDLING_SUMMARY",
                    paramValues["lang"].ConvertToString(),
                    P_SURVEYED_GRADE,
                    P_MATRIX_GRADE,
                    P_RECOMMENDED_GRADE,
                    P_PHOTO_GRADE,
                   P_DG1,
                    P_DG2,
                    P_DG3,
                    P_DG4,
                    P_DG5,
                    P_DG6,
                    P_SURVEYED_TECHNICAL_SOLUTION,
                    P_RECOMMENDED_TECHNICAL_SOLUTION,
                    P_TS1,
                    P_TS2,
                   P_TS3,
                   P_TS4,
                    P_TS5,
                    P_Dist,
                    P_VDC,
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
        public DataSet GrievanceHandlingSummaryReport(NameValueCollection paramValues)
        {
            DataSet ds = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["BatchID"].ConvertToString() != string.Empty)
                P_BATCH_ID = paramValues["BatchID"].ConvertToString();
            if (paramValues["CurVDC"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["CurVDC"].ConvertToString();
            if (paramValues["CurWard"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["CurWard"].ConvertToString();
            try
            {
                service.Begin();
                ds = service.GetDataSet(true, "GR_MIS_REVIEWED_SUMMARY_REPORT",
                    paramValues["Lang"],
                    P_Dist,
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT, 
                    P_BATCH_ID,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                ds = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ds = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return ds;

        }
        public DataTable GrievanceRetrofittingBeneficiary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GR_RETROFITTING_REPORT",
                    paramValues["lang"],
                    P_Dist,
                    P_VDC,
                    P_WARD,
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



        public DataTable GetRetrofittingBeneficiaryDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
            Object P_SIZE = DBNull.Value;                 
            Object P_checkexport =DBNull.Value;
            
           
            Object P_INDEX = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
                     
            if (paramValues["Batch"].ConvertToString() != string.Empty && paramValues["Batch"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["Batch"].ConvertToString();
             

          
            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();

            if (paramValues["Batch"].ConvertToString() != string.Empty)
                P_BATCH_ID = paramValues["Batch"].ConvertToString();
 

            if (paramValues["Batch"].ConvertToString() == "0")
                P_BATCH_ID = "";

            

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_MAINTENANCE_NONBENEF_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
                     P_BATCH_ID,
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

        public DataTable GetRetrofittingBeneficiaryDetailForExport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
          
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            //if (paramValues["BatchID"].ConvertToString() != string.Empty)
            //    P_Dist = paramValues["BatchID"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
           
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_MAINTENANCE_NONBNF_RPTFEXPT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    // P_BATCH_ID,
                    P_OTHHOUSEFLAG,
                       
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


        public DataTable GetRetrofittingBeneficiarySummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();

            if (paramValues["Batch"].ConvertToString() != string.Empty && paramValues["Batch"].ConvertToString() != "-1")
                P_BATCH_ID = paramValues["Batch"].ConvertToString();

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();

            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();

           
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_Rtrfitng_Bnfsry_SMARY_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist.ToDecimal(), 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
                     P_BATCH_ID.ToDecimal(),
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
        public DataTable GetGrievanceBenefSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_WARD_CURRNT = DBNull.Value;
            Object p_VDC_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GR_BENEF_SUMMARY_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                     
                    P_BATCH_ID,
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



        public DataTable GetGrievanceBenefSummaryByAllRptType(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value; ;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_ALL_TYPE_BENF_NONBENF_RPT",
                    paramValues["lang"].ConvertToString(),
                    "Y",
                    "",
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_WARD.ToDecimal(),
                    "".ToDecimal(),
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
        public DataTable GetSurveyHouseDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
            Object P_SIZE = DBNull.Value;
            Object P_PAGE_DRAW = DBNull.Value;
            
            Object P_exportcheck = DBNull.Value;
            if (paramValues["isexport"].ConvertToString() != string.Empty)
                P_exportcheck = paramValues["isexport"].ConvertToString();

            if(!(P_exportcheck.ConvertToString()=="1"))
            {
               // P_SIZE = "100";
            }
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")

                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();
                if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                    P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();
            if (paramValues["draw"].ConvertToString() != string.Empty && paramValues["draw"].ConvertToString() != "undefined")
                P_PAGE_DRAW = paramValues["draw"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty || paramValues["pageindex"].ConvertToString() == "0" || paramValues["pageindex"] == "undefined")
            {
                P_INDEX = "0";
            }

            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();


            //if (paramValues["pagesize"].ConvertToString() == string.Empty)
            //{
            //   // P_SIZE = "100";
            //}

               
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_SURVEY_HOUSE_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    P_OTHHOUSEFLAG,
                    P_BUILDING_CONDTION,
                      P_SIZE,
                   P_INDEX,
                   P_PAGE_DRAW,
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

        public DataTable HouseholdGradeDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HOUSEHOLD_DAMAGE_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    P_OTHHOUSEFLAG,
                    P_BUILDING_CONDTION,
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
        public DataTable GetSurveyHouseSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD = paramValues["wardCurr"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HH_SURVEY_SUMMARY_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_DAMAGE_GRADE,
                     P_TECHNICAL_SOLUTION,
                    P_Dist,
                    P_VDC,
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

        public DataTable DonorGetSurveyHouseSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HH_SURVEY_SUMMARY_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_WARD.ToDecimal(), 
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


        public DataTable DonorHouseholdSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HH_COUNT_SUMMARY_RPT",
                    paramValues["lang"].ConvertToString(), 
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_WARD.ToDecimal(),
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
        public DataTable getGrievanceStatus(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value; ;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GR_REGISTRATION_SUMMARY_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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

        public DataTable getDaliyGrievanceStatus(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DATE_FROM = DBNull.Value;
            Object P_DATE_TO = DBNull.Value;
            Object P_NAME = DBNull.Value;
            if (paramValues["DateFrom"].ConvertToString() != string.Empty)
                P_DATE_FROM = paramValues["DateFrom"].ConvertToString();
            if (paramValues["DateTo"].ConvertToString() != string.Empty)
                P_DATE_TO = paramValues["DateTo"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_DAILY_GRIEVANCE_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_NAME,
                    P_DATE_FROM,
                    P_DATE_TO,
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
        public DataTable getDaliyGrievanceStatusDtl(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DATE_FROM = DBNull.Value;
            Object P_DATE_TO = DBNull.Value;
            Object P_NAME = DBNull.Value;
         Object P_SIZE = DBNull.Value;
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object P_exportcheck = DBNull.Value;

            if (paramValues["exportcheck"].ConvertToString() != string.Empty)
                P_exportcheck = paramValues["exportcheck"].ConvertToString();

            if(P_exportcheck.ConvertToString()=="1")
            {
                 P_SIZE = "";

            }


            if (paramValues["DateFrom"].ConvertToString() != string.Empty)
                P_DATE_FROM = paramValues["DateFrom"].ConvertToString();
            if (paramValues["DateTo"].ConvertToString() != string.Empty)
                P_DATE_TO = paramValues["DateTo"].ConvertToString();

            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_DAILY_GRIEVANCE_DTL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_NAME,
                    P_DATE_FROM,
                    P_DATE_TO,
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
        public DataTable GrievanceHandlingDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_NISSA_NO = DBNull.Value;
            Object P_GRIEVANT_NAME = DBNull.Value;
            Object P_HOUSE_SERIAL_NO = DBNull.Value;
            Object P_FORM_NO = DBNull.Value;
            Object P_REGISTER_NO = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_CASE_TYPE = DBNull.Value;
            Object P_OTHER_HOUSE = DBNull.Value;
            Object P_OTH_HOUSE_CONDITION = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_GHDATA_FLAG = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_CUR_VDC = DBNull.Value;
            Object P_CUR_WARD = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
             
            if (paramValues["BatchID"].ConvertToString() != string.Empty)
                P_BATCH_ID = paramValues["BatchID"].ConvertToString();
            if (paramValues["CurVDC"].ConvertToString() != string.Empty)
                P_CUR_VDC = paramValues["CurVDC"].ConvertToString();
            if (paramValues["CurWard"].ConvertToString() != string.Empty)
                P_CUR_WARD = paramValues["CurWard"].ConvertToString();
         Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            if (paramValues["GHDataFlag"].ConvertToString() != string.Empty && paramValues["GHDataFlag"].ConvertToString() != "undefined")
                P_GHDATA_FLAG = paramValues["GHDataFlag"].ConvertToString();
            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();

            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
               P_SIZE = paramValues["pagesize"].ConvertToString();

            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GRIEVANCE_HANDLING_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist, 
                    P_CUR_VDC,
                    P_CUR_WARD,
                    P_NISSA_NO,
                    P_GRIEVANT_NAME,
                    P_HOUSE_SERIAL_NO,
                    P_FORM_NO,
                    P_REGISTER_NO,
                    P_CASE_TYPE,
                    P_OTHER_HOUSE,
                    P_OTH_HOUSE_CONDITION,
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    DBNull.Value,
                    P_BATCH_ID,
                    P_GHDATA_FLAG,
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

        public DataTable HouseCoordinates(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty)
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_DAMAGE_GRADE",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_DAMAGE_GRADE,
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
        public DataTable GetHouseHoldDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;
            Object P_checkexport = DBNull.Value;
         Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;


            if (paramValues["checkexport"].ConvertToString() != string.Empty)
                P_checkexport = paramValues["checkexport"].ConvertToString();
            if (P_checkexport.ConvertToString() == "1")
            {
                P_SIZE = "";
            }
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            //if (paramValues["vdc"].ConvertToString() != string.Empty)
            //    P_VDC = paramValues["vdc"].ConvertToString();
            //if (paramValues["ward"].ConvertToString() != string.Empty)
            //    P_WARD = paramValues["ward"].ConvertToString();

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

            //if (paramValues["pagesize"].ConvertToString() == string.Empty)
            //{
            //   // P_SIZE = "100";
            //}


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HOUSEHOLD_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
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


        public DataTable GetHouseHoldDetailForExport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
          

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

          


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_HOUSEHOLD_DETAIL_REPORTEXPT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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
         




        #region service of training participatn report

        public DataTable GetParticipantDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_Gender = DBNull.Value;
            Object P_Experienceyear = DBNull.Value;

            Object p_TrainingTypeID = DBNull.Value;

            Object p_ParticipantTypeId = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;

         Object P_SIZE = DBNull.Value;
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object P_exportcheck = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["GenderId"].ConvertToString() != string.Empty)
                P_Gender = paramValues["GenderId"].ConvertToString();
            if (paramValues["ExperienceID"].ConvertToString() != string.Empty)
                P_Experienceyear = paramValues["ExperienceID"].ConvertToString();
            if (paramValues["ParticipantTypeId"].ConvertToString() != string.Empty)
                p_ParticipantTypeId = paramValues["ParticipantTypeId"].ConvertToString();



            //if (paramValues["TrainingTypeID"].ConvertToString() != string.Empty)
            //    p_TrainingTypeID = paramValues["TrainingTypeID"].ConvertToString();

            //if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
            //    P_INDEX = paramValues["pageindex"].ConvertToString();


            //if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
            //    P_SIZE = paramValues["pagesize"].ConvertToString();


            //if (paramValues["pageindex"].ConvertToString() == string.Empty)
            //{
            //    P_INDEX = "1";
            //}

            string UserId = null;

            if (CommonVariables.GroupCD == "54")
            {
                UserId = CommonVariables.UserCode;
            }
            try
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_participants_DTL_RPT",
                    paramValues["lang"].ConvertToString(), 
                    P_Dist,
                    P_VDC, 
                    P_WARD,
                    p_ParticipantTypeId, 
                    UserId,
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




        public DataTable GetParticipantSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_Gender = DBNull.Value;
            Object P_Experienceyear = DBNull.Value;

            Object p_TrainingTypeID = DBNull.Value;

            Object p_ParticipantTypeId = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
 
            if (paramValues["participantsType"].ConvertToString() != string.Empty)
                p_ParticipantTypeId = paramValues["participantsType"].ConvertToString();

            string UserId = null;

            if (CommonVariables.GroupCD == "54")
            {
                UserId = CommonVariables.UserCode;
            }

            try
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_Participants_SMRY_RPT",
                    paramValues["lang"].ConvertToString(), 
                    P_Dist,
                    P_VDC,
                    P_WARD, 
                    p_ParticipantTypeId, 
                    UserId,
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

        public DataTable GetTrainerDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_PROFESSION_CD = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
         Object P_SIZE = DBNull.Value;
            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            Object P_exportcheck = DBNull.Value;

            if (paramValues["exportcheck"].ConvertToString() != string.Empty)
                P_exportcheck = paramValues["exportcheck"].ConvertToString();

            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["ProfessionCd"].ConvertToString() != string.Empty)
                P_PROFESSION_CD = paramValues["ProfessionCd"].ConvertToString();

            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }
            string UserId = null;

            if (CommonVariables.GroupCD == "54")
            {
                UserId = CommonVariables.UserCode;
            }
            try
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_TRAINERS_DTL_RPT",
                    paramValues["lang"].ConvertToString(), 
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_PROFESSION_CD,
                  //    P_SIZE,
                  //  P_INDEX,
                  UserId,
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

        public DataTable GetTrainerSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_ProfessioId = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["ProfessionCd"].ConvertToString() != string.Empty)
                P_ProfessioId = paramValues["ProfessionCd"].ConvertToString();


            string UserId = null;

            if (CommonVariables.GroupCD == "54")
            {
                UserId = CommonVariables.UserCode;
            }

            try
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_trainers_SMRY_RPT",
                    paramValues["lang"].ConvertToString(), 
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_ProfessioId,
                    UserId,
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


        public DataTable GetTrainingDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_implementingpartner_id = DBNull.Value;
            Object p_traininglevel_id = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            Object P_TRAINING_BATCH_CD = DBNull.Value;
            string UserId = null;

            Object p_FUND_CD = DBNull.Value;
            Object P_IMPL_AGENCY = DBNull.Value;
            Object P_START_DATE = DBNull.Value;
            Object P_END_DATE = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["implementingpartner_id"].ConvertToString() != string.Empty)
                P_implementingpartner_id = paramValues["implementingpartner_id"].ConvertToString();
            if (paramValues["TrainingType"].ConvertToString() != string.Empty)
                p_traininglevel_id = paramValues["TrainingType"].ConvertToString();
            if (paramValues["BatchCD"].ConvertToString() != string.Empty)
                P_TRAINING_BATCH_CD = paramValues["BatchCD"].ConvertToString();


            if (paramValues["Fundingsource_id"].ConvertToString() != string.Empty)
                p_FUND_CD = paramValues["Fundingsource_id"].ConvertToString();
            if (paramValues["implementingAgency"].ConvertToString() != string.Empty)
                P_IMPL_AGENCY = paramValues["implementingAgency"].ConvertToString();
            if (paramValues["StartDate"].ConvertToString() != string.Empty)
                P_START_DATE = paramValues["StartDate"].ConvertToString();
            if (paramValues["EndDate"].ConvertToString() != string.Empty)
                P_END_DATE = paramValues["EndDate"].ConvertToString();

            if(CommonVariables.GroupCD=="54")
            {
                UserId = CommonVariables.UserCode;
            }
            try
            {
                service.Begin();
                
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_TRAINING_DTL_RPT",
                    paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                    P_implementingpartner_id,
                    p_FUND_CD,//funding source
                   p_traininglevel_id,
                   P_TRAINING_BATCH_CD,
                   P_IMPL_AGENCY,
                   P_START_DATE,
                   P_END_DATE,
                   UserId,
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

        public DataTable GetTrainingSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_implementingpartner_id = DBNull.Value;
            Object p_traininglevel_id = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            Object P_TRAINING_BATCH_CD = DBNull.Value;


            Object p_FUND_CD = DBNull.Value;
            Object P_IMPL_AGENCY = DBNull.Value;
            Object P_START_DATE = DBNull.Value;
            Object P_END_DATE = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["implementingpartner_id"].ConvertToString() != string.Empty)
                P_implementingpartner_id = paramValues["implementingpartner_id"].ConvertToString();
            if (paramValues["TrainingType"].ConvertToString() != string.Empty)
                p_traininglevel_id = paramValues["TrainingType"].ConvertToString();
            if (paramValues["BatchCD"].ConvertToString() != string.Empty)
                P_TRAINING_BATCH_CD = paramValues["BatchCD"].ConvertToString();


            if (paramValues["Fundingsource_id"].ConvertToString() != string.Empty)
                p_FUND_CD = paramValues["Fundingsource_id"].ConvertToString();
            if (paramValues["implementingAgency"].ConvertToString() != string.Empty)
                P_IMPL_AGENCY = paramValues["implementingAgency"].ConvertToString();
            if (paramValues["StartDate"].ConvertToString() != string.Empty)
                P_START_DATE = paramValues["StartDate"].ConvertToString();
            if (paramValues["EndDate"].ConvertToString() != string.Empty)
                P_END_DATE = paramValues["EndDate"].ConvertToString();




            string UserId = null;

            if (CommonVariables.GroupCD == "54")
            {
                UserId = CommonVariables.UserCode;
            }
 

           

            
            try
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_TRAINNING_REPORT";
                dt = service.GetDataTable(true, "PR_TRAINING_SUM_NEW",
                    paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                    P_implementingpartner_id,
                    p_FUND_CD,//funding source
                   p_traininglevel_id, 
                   P_TRAINING_BATCH_CD, 
                   P_IMPL_AGENCY,
                   P_START_DATE ,
                   P_END_DATE ,
                   UserId,
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
        #endregion
    }

}


