using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Security;
using System.Data;
using EntityFramework;
using ExceptionHandler;
namespace MIS.Services.Security
{
   public class CheckActionPermission
    {

       public DataTable checkActionPermission(MenuSecurity mnu)
       {
           DataTable dt = null;
           using (ServiceFactory service = new ServiceFactory())
           {
              
               string cmdText = " SELECT " +
               " menu_cd " +
               ",grp_cd " +
               ",perm_cd " +
               ",entered_by " +
               ",entered_dt " +
               ",last_updated_by " +
               ",last_updated_dt " +
               " FROM  " +
               " com_menu_security " +
               " where menu_cd='" + mnu.menuCd.ToString().Replace("'","") +
               "' and grp_cd=(select cwug.grp_cd from com_web_usr_grp cwug where cwug.USR_CD='" + mnu.UsrCd + "')" +
               " and perm_cd='" + mnu.permCd + "'";
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(cmdText, null);
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
           }
           return dt;
       }
    }
}
