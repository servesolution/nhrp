using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using System.Data;
using ExceptionHandler;
namespace MIS.Services.Core
{
   public static class NepaliDate
    {
       public static string nepdate = "";
       public static string getForamttedNepaliDate()
       {
           string retDate = "";
           string ndate = getNepaliDate(System.DateTime.Now.ToString("dd-MMM-yyyy"));
           if (ndate != "")
           {
               string[] datePart = ndate.Split('-');
               string monthName = getNepaliMonthName(datePart[1].ToString());
               retDate = datePart[2].ToString() + " " + monthName + datePart[0].ToString();
           }
           return retDate;
       }
       public static string getForamttedNepaliDateTime()
       {
           string retDate = "";
           DateTime currentDate=System.DateTime.Now;
           string ndate = getNepaliDate(currentDate.ToString("dd-MMM-yyyy"));
           if (ndate != "")
           {
               string[] datePart = ndate.Split('-');
               string monthName = getNepaliMonthName(datePart[1].ToString());
               retDate = currentDate.ToString("ddd") + "," + datePart[2].ToString() + " " + monthName+" " + datePart[0].ToString() + " " + currentDate.ToString("hh:mm tt");
           }
           return retDate;
       }
       public static string getNepaliDate(string pDate)
       {
           DataTable dtbl = null;
           string retDate = "";
           try
           {
               using (ServiceFactory service = new ServiceFactory())
               {
                   service.Begin();
                   string cmdText = String.Format("select com_pkg_date.FN_EngToNep('" + Convert.ToDateTime(pDate).ToString("dd-MMM-yyyy") + "') as nepalidate from dual");
                   try
                   {
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
               }
               if (dtbl != null)
               {
                   if (dtbl.Rows.Count > 0)
                   {
                       retDate = dtbl.Rows[0]["nepalidate"].ToString();

                   }
               }
           }
           catch (Exception ex)
           {
               ExceptionManager.AppendLog(ex);
               retDate = ""; 
           }
           return retDate;
       }
       //Modified By Chandra Prakash:6 June 2013
       public static string getEnglishDate(string pDate)
       {
           DataTable dtbl = null;
           string retDate = "";
           try
           {
               using (ServiceFactory service = new ServiceFactory())
               {
                   service.Begin();
                   if (pDate != null)
                   {
                       pDate = Convert.ToDateTime(pDate).ToString("yyyy-MM-dd").ToString();
                   }
                   string cmdText = String.Format("select com_pkg_date.FN_NepToEng('" + pDate + "') as englishdate from dual");
                   try
                   {
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
               }
               if (dtbl.Rows.Count > 0)
               {
                   retDate = dtbl.Rows[0]["englishdate"].ToString();

               }
           }
           catch (Exception ex)
           {
               ExceptionManager.AppendLog(ex);
               retDate = "";
           }
           return retDate;
       }
       public static string getNepaliMonthName(string monthVal)
       {
           string retMonth = "";
           if (monthVal != "")
           {
               switch (monthVal)
               {
                   case "01":
                       retMonth = "Baishak";
                       break;
                   case "02":
                       retMonth = "Jestha";
                       break;
                   case "03":
                       retMonth = "Ashad";
                       break;
                   case "04":
                       retMonth = "Shrawan";
                       break;
                   case "05":
                       retMonth = "Bhadra";
                       break;
                   case "06":
                       retMonth = "Ashwin";
                       break;
                   case "07":
                       retMonth = "Kartik";
                       break;
                   case "08":
                       retMonth = "Mangsir";
                       break;                     
                   case "09":
                       retMonth = "Poush";
                       break;
                   case "10":
                       retMonth = "Magh";
                       break;
                   case "11":
                       retMonth = "Falgun";
                       break;
                   case "12":
                       retMonth = "Chaitra";
                       break;
                   default:                       
                       break;
               }
           }
           return retMonth;
       }
       public static string getFormattedTime(string pHour, string pMin, string pSec)
       {
           string retTime = "";
           int intHour=(pHour==""?0:Int32.Parse (pHour));
           int intMin = (pMin == "" ? 0 : Int32.Parse(pMin));
           int intSec = (pSec == "" ? 0 : Int32.Parse(pSec));
           if (pHour != "" && pMin != "" && pSec != "")
           {
               if (intHour > 12)
               {
                   retTime += (intHour - 12).ToString();
               }
               else
               {
                   retTime += intHour.ToString();
               }
               retTime += ":";
               if (intMin < 10)
               {
                   retTime += "0" + intMin.ToString();
               }
               else
               {
                   retTime += intMin.ToString();
               }
               retTime += ":";
               if (intMin < 10)
               {
                   retTime += "0" + intSec.ToString();
               }
               else
               {
                   retTime += intSec.ToString();
               }
               if (intHour > 12)
               {
                   retTime += " PM";
               }
               else
               { retTime += " AM"; }
           }
           return retTime;
       }

    }
}
