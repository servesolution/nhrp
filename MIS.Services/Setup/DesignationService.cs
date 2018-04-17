using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Models.Core;
using MIS.Services.Core;
using MIS.Models.Setup;
using System.Web.Mvc;
using System.Web;
using MIS.Models.Security;
using ExceptionHandler;

namespace MIS.Services.Setup
{
    public class DesignationService
    {
       public DataTable GetDesignationList(string initialStr, string orderby, string order)
       {
           DataTable dtbl = null;
           List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
           string cmdText = "";
           string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
           using (ServiceFactory service = new ServiceFactory())
           {

               cmdText = "select DESIGNATION_CD,DEFINED_CD,DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC,APPROVED from MIS_DESIGNATION";
               if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
               {                   
                   cmdText += String.Format(" where " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " like '{0}%'", initialStr);
               }
               if ((orderby != "") && (order != ""))
               {
                    
                       cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                   

               }
               try
               {
                   service.Begin();  
                   dtbl = service.GetDataTable(new
                   {
                       query = cmdText,
                       args = new
                       {

                       }
                   });
               }
               catch (Exception ex)
               {
                   dtbl = null;
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
           return dtbl;
       }


       public DataTable GetPositionList(string initialStr, string orderby, string order)
       {
           DataTable dtbl = null;
           List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
           string cmdText = "";
           string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
           using (ServiceFactory service = new ServiceFactory())
           {

               cmdText = "select POSITION_CD,DEFINED_CD,DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC,APPROVED from MIS_POSITION";
               if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
               {
                   cmdText += String.Format(" where " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " like '{0}%'", initialStr);
               }
               if ((orderby != "") && (order != ""))
               {

                   cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);


               }
               try
               {
                   service.Begin();
                   dtbl = service.GetDataTable(new
                   {
                       query = cmdText,
                       args = new
                       {

                       }
                   });
               }
               catch (Exception ex)
               {
                   dtbl = null;
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
           return dtbl;
       }
      
       /// <summary>
       /// To Approve/DisApprove Designation
       /// </summary>
       /// <param name="objDesignation"></param>
       public void ChangeStatus(MISDesignation objDesignation)
       {
           QueryResult qr = null;
           MisDesignationInfo obj = new MisDesignationInfo();
           using (ServiceFactory service = new ServiceFactory())
           {            
                   
                   service.PackageName = "PKG_SETUP";
                   obj.DesignationCd = objDesignation.designationCd;
                   obj.Approved = objDesignation.approved;
                   obj.ApprovedBy = SessionCheck.getSessionUsername();
                   obj.EnteredBy = SessionCheck.getSessionUsername();
                   obj.Mode = "A";                  
                   try
                   {
                       service.Begin();
                       qr = service.SubmitChanges(obj, true);
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
       }

       /// <summary>
       /// Add Designation
       /// </summary>
       /// <param name="Designation"></param>
       public void AddDesignation(MISDesignation objDesignation)
       {
           MisDesignationInfo obj = new MisDesignationInfo();
           using (ServiceFactory service = new ServiceFactory())
           {               
               service.PackageName = "PKG_SETUP";
               obj.DesignationCd = objDesignation.designationCd;
               obj.DefinedCd = objDesignation.definedCd;
               obj.DescEng = objDesignation.descEng.ToUpper();
               obj.DescLoc = objDesignation.descLoc;
               obj.ShortName = objDesignation.shortName;
               obj.ShortNameLoc = objDesignation.shortNameLoc;
               obj.OrderNo = objDesignation.orderNo;
               obj.Approved = "N";
               obj.Disabled = "N";
               obj.ApprovedBy = SessionCheck.getSessionUsername();
               obj.ApprovedDt = objDesignation.approvedDt;
               obj.ApprovedDtLoc = objDesignation.approvedDtLoc;
               obj.EnteredBy = SessionCheck.getSessionUsername();
               obj.EnteredDt = objDesignation.enteredDt;
               obj.EnteredDtLoc = objDesignation.enteredDtLoc;
               obj.IpAddress = CommonVariables.IPAddress;
               obj.Mode = "I";               
               try
               {
                   service.Begin();
                   QueryResult qr = service.SubmitChanges(obj, true);

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
       }

       /// <summary>
       /// Update Designation
       /// </summary>
       /// <param name="Designation"></param>
       public void UpdateDesignation(MISDesignation objDesignation)
       {
           using (ServiceFactory service = new ServiceFactory())
           {              

                   MisDesignationInfo obj = new MisDesignationInfo();
                   service.PackageName = "PKG_SETUP";
                   obj.DesignationCd = objDesignation.designationCd;
                   obj.DefinedCd = objDesignation.definedCd;
                   obj.DescEng = objDesignation.descEng.ToUpper();
                   obj.DescLoc = objDesignation.descLoc;
                   obj.ShortName = objDesignation.shortName;
                   obj.ShortNameLoc = objDesignation.shortNameLoc;
                   obj.Approved = objDesignation.approved;
                   obj.Disabled = objDesignation.disabled;
                   obj.ApprovedBy = SessionCheck.getSessionUsername();
                   obj.ApprovedDt = objDesignation.approvedDt;
                   obj.ApprovedDtLoc = objDesignation.approvedDtLoc;
                   obj.EnteredBy = SessionCheck.getSessionUsername();
                   obj.EnteredDt = objDesignation.enteredDt;
                   obj.EnteredDtLoc = objDesignation.enteredDtLoc;
                   //obj.IpAddress = CommonVariables.IPAddress;
                   obj.Mode = "U";                   
                   try
                   {
                       service.Begin();
                       QueryResult qr = service.SubmitChanges(obj, true);
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
       }

      /// <summary>
      /// Delete Designation
      /// </summary>
       /// <param name="Designation"></param>
       public void DeleteDesignation(MISDesignation objDesignation)
       {
           using (ServiceFactory service = new ServiceFactory())
           {
               
                   MisDesignationInfo obj = new MisDesignationInfo();
                   obj.DesignationCd = objDesignation.designationCd;
                   obj.DefinedCd = null;
                   obj.DescEng = null;
                   obj.DescLoc = null;
                   obj.ShortName = null;
                   obj.ShortNameLoc = null;
                   obj.Approved = "N";
                   obj.Disabled = "N";
                   obj.ApprovedBy = SessionCheck.getSessionUsername();
                   obj.ApprovedDt = DateTime.Now;
                   obj.ApprovedDtLoc = null;
                   obj.EnteredBy = SessionCheck.getSessionUsername();
                   obj.EnteredDt = DateTime.Now;
                   obj.EnteredDtLoc = null;
                   //obj.IpAddress = CommonVariables.IPAddress;
                   //Delete the group
                   service.PackageName = "PKG_SETUP";                 
                   obj.Mode = "D";
                   try
                   {
                       service.Begin();
                       QueryResult qr = service.SubmitChanges(obj, true);
                   }
                   catch (Exception ex)
                   {

                       ExceptionManager.AppendLog(ex);
                   }
                   finally
                   {
                       service.End();
                   }
             
           }
       }

       // Fill Designation For Edit
       public MISDesignation FillDesignation(string designationCd)
       {
           MISDesignation obj = new MISDesignation();
           DataTable dt = new DataTable();
           dt = null;
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   string CmdText = "select DESIGNATION_CD,DEFINED_CD, DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC from MIS_Designation where Designation_CD = '" + designationCd + "'";
                   dt = service.GetDataTable(new
                   {
                       query = CmdText,
                       args = new
                       {
                       }
                   });

                   if (dt.Rows.Count > 0)
                   {
                       obj.designationCd = dt.Rows[0]["DESIGNATION_CD"].ToString();
                       obj.definedCd = dt.Rows[0]["DEFINED_CD"].ToString();
                       obj.descEng = dt.Rows[0]["DESC_ENG"].ToString();
                       obj.descLoc = dt.Rows[0]["DESC_LOC"].ToString();
                       obj.shortName = dt.Rows[0]["SHORT_NAME"].ToString();
                       obj.shortNameLoc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();


                   }

               }
               catch (Exception ex)
               {
                   ExceptionManager.AppendLog(ex);
                   obj = null;
               }
               finally 
               {
                   service.End();
               }
           }
           return obj;

       }

       public bool CheckDuplicateDefinedCode(string DefinedCode, string DesignationCode)
       {
           string cmdText = string.Empty;
           DataTable dtbl = null;
           using (ServiceFactory service = new ServiceFactory())
           {

               if (DesignationCode == string.Empty)
               {
                   cmdText = "select * from MIS_Designation where DEFINED_CD ='" + DefinedCode + "'";
               }
               else
               {
                   cmdText = "select * from MIS_Designation where DEFINED_CD ='" + DefinedCode + "' and DESIGNATION_CD !='" + DesignationCode + "'";
               }
               try
               {
                   service.Begin();
                   dtbl = service.GetDataTable(new
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
               if (dtbl.Rows.Count > 0)
               {
                   return false;
               }
               return true;
           }
       }
    }
}
