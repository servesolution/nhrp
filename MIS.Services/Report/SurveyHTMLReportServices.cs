using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using EntityFramework;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Report
{
   public class SurveyHTMLReportServices
    {
       public DataTable SurveyReportByCaste(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_CASTE_TYPE = DBNull.Value;
           Object P_MARITAL_STATUS = DBNull.Value;
           Object P_GENDER = DBNull.Value;
           
           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["caste"].ConvertToString() != string.Empty && paramValues["caste"].ConvertToString() != "undefined")
               P_CASTE_TYPE = paramValues["caste"].ToDecimal();
           if (paramValues["maritalstatus"].ConvertToString() != string.Empty && paramValues["maritalstatus"].ConvertToString() != "undefined")
               P_MARITAL_STATUS = paramValues["maritalstatus"].ConvertToString();
           if (paramValues["gender"].ConvertToString() != string.Empty && paramValues["gender"].ConvertToString() != "undefined")
               P_GENDER = paramValues["gender"].ConvertToString();
          
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GET_CASTE_SURVEY_REPORT",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_CASTE_TYPE.ToDecimal(),
                   P_MARITAL_STATUS.ToDecimal(),
                   P_GENDER.ToDecimal(),
                  DBNull.Value
                  );

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
       public DataTable SurveySummaryReportByCaste(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_CASTE_TYPE = DBNull.Value;
           Object P_MARITAL_STATUS = DBNull.Value;
           Object P_GENDER = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["caste"].ConvertToString() != string.Empty && paramValues["caste"].ConvertToString() != "undefined")
               P_CASTE_TYPE = paramValues["caste"].ToDecimal();
           if (paramValues["maritalstatus"].ConvertToString() != string.Empty && paramValues["maritalstatus"].ConvertToString() != "undefined")
               P_MARITAL_STATUS = paramValues["maritalstatus"].ConvertToString();
           if (paramValues["gender"].ConvertToString() != string.Empty && paramValues["gender"].ConvertToString() != "undefined")
               P_GENDER = paramValues["gender"].ConvertToString();

           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GET_CASTE_SUM_SURVEY_REPORT",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_CASTE_TYPE.ToDecimal(),
                   P_MARITAL_STATUS.ToDecimal(),
                   P_GENDER.ToDecimal(),
                  DBNull.Value
                  );

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
       public DataTable SurveyDetailReportByEducation(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_LITERATE_TYPE = DBNull.Value;
           Object P_EDUCATION_CD = DBNull.Value;
           Object P_GENDER = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["literatechk"].ConvertToString() != string.Empty && paramValues["literatechk"].ConvertToString() != "undefined")
               P_LITERATE_TYPE = paramValues["literatechk"].ConvertToString();
           if (paramValues["educationcd"].ConvertToString() != string.Empty && paramValues["educationcd"].ConvertToString() != "undefined")
               P_EDUCATION_CD = paramValues["educationcd"].ConvertToString();
           if (paramValues["gender"].ConvertToString() != string.Empty && paramValues["gender"].ConvertToString() != "undefined")
               P_GENDER = paramValues["gender"].ConvertToString();

           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GET_EDUCATIONAL_SURVEY_REPORT",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_LITERATE_TYPE.ConvertToString(),
                   P_EDUCATION_CD.ToDecimal(),
                   P_GENDER.ToDecimal(),
                  DBNull.Value
                  );

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
       public DataTable SurveySummaryReportByEducation(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_LITERATE_TYPE = DBNull.Value;
           Object P_EDUCATION_CD = DBNull.Value;
           Object P_GENDER = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["literatechk"].ConvertToString() != string.Empty && paramValues["literatechk"].ConvertToString() != "undefined")
               P_LITERATE_TYPE = paramValues["literatechk"].ConvertToString();
           if (paramValues["educationcd"].ConvertToString() != string.Empty && paramValues["educationcd"].ConvertToString() != "undefined")
               P_EDUCATION_CD = paramValues["educationcd"].ConvertToString();
           if (paramValues["gender"].ConvertToString() != string.Empty && paramValues["gender"].ConvertToString() != "undefined")
               P_GENDER = paramValues["gender"].ConvertToString();

           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GET_EDU_SURVEY_SUM_REPORT",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_LITERATE_TYPE.ConvertToString(),
                   P_EDUCATION_CD.ToDecimal(),
                   P_GENDER.ToDecimal(),
                  DBNull.Value
                  );

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

       public DataTable SurveySumReportByNonEducation(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_LITERATE_TYPE = DBNull.Value;
           Object P_EDUCATION_CD = DBNull.Value;
           Object P_GENDER = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["literatechk"].ConvertToString() != string.Empty && paramValues["literatechk"].ConvertToString() != "undefined")
               P_LITERATE_TYPE = paramValues["literatechk"].ConvertToString();
           if (paramValues["educationcd"].ConvertToString() != string.Empty && paramValues["educationcd"].ConvertToString() != "undefined")
               P_EDUCATION_CD = paramValues["educationcd"].ConvertToString();
           if (paramValues["gender"].ConvertToString() != string.Empty && paramValues["gender"].ConvertToString() != "undefined")
               P_GENDER = paramValues["gender"].ConvertToString();

           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GET_EDU_SURVEY_SUM_ILLT_REPORT",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_LITERATE_TYPE.ConvertToString(),
                   P_EDUCATION_CD.ToDecimal(),
                   P_GENDER.ToDecimal(),
                  DBNull.Value
                  );

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
    }
}
