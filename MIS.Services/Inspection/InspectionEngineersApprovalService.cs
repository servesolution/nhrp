using EntityFramework;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
namespace MIS.Services.Inspection
{
    public class InspectionEngineersApprovalService
    {

        CommonService com = new CommonService();
        public DataTable GetInspectionSearchResults(NameValueCollection paramValues)
        {
          DataTable dt = null;

          Object P_DIST = DBNull.Value;
          Object P_VDC = DBNull.Value;
          Object P_WARD = DBNull.Value;
          Object P_NAME = DBNull.Value;
          Object P_ID = DBNull.Value;
          Object P_INSPECTIONLEVEL = DBNull.Value;
          Object P_USERDISTRICT = DBNull.Value;
            Object P_ENGINEERAPROVE = DBNull.Value;
            Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DIST = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["name"].ConvertToString() != string.Empty)
                P_NAME = paramValues["name"].ConvertToString();
            if (paramValues["id"].ConvertToString() != string.Empty)
                P_ID = paramValues["id"].ConvertToString();
            if (paramValues["inspectionLevel"].ConvertToString() != string.Empty)
                P_INSPECTIONLEVEL = paramValues["inspectionLevel"].ConvertToString();
            if (paramValues["userDistrict"].ConvertToString() != string.Empty)
                P_USERDISTRICT = paramValues["userDistrict"].ConvertToString();
            if (paramValues["engineerApprove"].ConvertToString() != string.Empty)
                P_ENGINEERAPROVE = paramValues["engineerApprove"];
            if (paramValues["pagesize"].ConvertToString() != string.Empty)
                P_SIZE = paramValues["pagesize"].ConvertToString();
            if (paramValues["pageindex"].ConvertToString() != string.Empty)
                P_INDEX = paramValues["pageindex"].ConvertToString();

          ServiceFactory service = new ServiceFactory();
          try
          {

              service.Begin();
              dt = service.GetDataTable(true, "GET_INSPECTION_ENG_APPROV_LIST",
                 P_DIST,
                 P_VDC,
                 P_WARD,
                 P_NAME,
                 P_ID,
                 P_INSPECTIONLEVEL,
                 P_USERDISTRICT,
                 P_ENGINEERAPROVE,
                 P_SIZE,
                 P_INDEX,
                 DBNull.Value)
                  ;
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
        public bool SingleSecondApprove(string paNumber, string InspectionLevel,   string checkvalue)
        {
            bool res = false;
            DataTable dt = new DataTable();
            DataTable dTt = new DataTable();
             QueryResult qrEngineerAgreement = null;
           
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin(); 
                string a = DateTime.Today.ToString("MM/dd/yyyy");
                 
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_APPROVAL";
                    qrEngineerAgreement = service.SubmitChanges("PR_INSPECTION_ENGI_APPROVE",
                                         "U",
                                         paNumber.ConvertToString(),
                                         InspectionLevel.ConvertToString(),
                                        checkvalue.ConvertToString(), 
                                         SessionCheck.getSessionUsername(),
                                     DateTime.Now,
                                                System.DateTime.Now.ConvertToString()
                                     ); 
                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    //exc = oe.Code.ConvertToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
                    //exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qrEngineerAgreement != null)
                {
                    res = qrEngineerAgreement.IsSuccess;
                }
            }

            return res;
        }

         public DataTable VdcWiseData(string distCd, string lang)
        {
            DataTable dt = new DataTable();
             
             using(ServiceFactory sf= new ServiceFactory())
             {
                 sf.Begin();
                 try
                 {
                     //sf.PackageName = "NHRS.PKG_INSPECTION_APPROVAL";
                     //dt = sf.GetDataTable(true,"PR_ENGI_VDC_WISE_DATA", distCd ); 
                     dt = sf.GetDataTable(true, "pr_eng_approve",
                         lang,
                         distCd.ToDecimal(),
                         DBNull.Value
                     ); 
                 }
                 catch(OracleException oe)
                 {
                     ExceptionManager.AppendLog(oe);
                 }
                 catch(Exception ex)
                 {
                     ExceptionManager.AppendLog(ex);
                 }
                 finally
                 {
                     if (sf.Transaction != null)
                     {
                         sf.End();
                     }
                 }
             }

            return dt;
        }
    }
}
