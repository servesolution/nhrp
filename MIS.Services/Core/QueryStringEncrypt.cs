using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace MIS.Services.Core
{
   public static class QueryStringEncrypt
    {
       public static string EncryptString(object routeValues)
       {
           string strEncrypt="";
           RouteValueDictionary RouteValueDictionary = new RouteValueDictionary(routeValues);
           string routeValuesText = RouteTable.Routes.GetVirtualPath(null, RouteValueDictionary).VirtualPath;
           Encryption64 Encryption64 = new Encryption64();
           strEncrypt = Encryption64.Encrypt(routeValuesText, "ABC123AB");
           return strEncrypt;
       }

       public static string EncryptString(RouteValueDictionary RouteValueDictionary)
       {
           string strEncrypt = "";           
           string routeValuesText = RouteTable.Routes.GetVirtualPath(null, RouteValueDictionary).VirtualPath;
           Encryption64 Encryption64 = new Encryption64();
           strEncrypt = Encryption64.Encrypt(routeValuesText, "ABC123AB");
           return strEncrypt;
       }
       /// <summary>
       /// Encrypt the queryString
       /// </summary>
       /// <param name="qryString">Querystring in form "/?p1=value1&p2=value2"</param>
       /// <returns>Encrypted String</returns>
       public static string EncryptString(string qryString)
       {
           string strEncrypt = "";           
           Encryption64 Encryption64 = new Encryption64();
           strEncrypt = Encryption64.Encrypt(qryString, "ABC123AB");
           return strEncrypt;
       }
       public static RouteValueDictionary DecryptString(string encryptString)
       {
           RouteValueDictionary RouteValueDictionary = new RouteValueDictionary();
           var Encryption64 = new Encryption64();
           
           var query = Encryption64.Decrypt(encryptString.Replace(" ","+"), "ABC123AB");
           if (query.Length > 2)
           {
               query = query.Substring(2);
           }
           var tokens = query.Split(new String[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
           if (tokens.Count() > 0)
           {
               for (int i = 0; i < tokens.Count(); i++)
               {
                   var centerPos = tokens[i].IndexOf("=");
                   RouteValueDictionary.Add(tokens[i].Substring(0, centerPos), tokens[i].Substring(centerPos + 1));
               }
           }
           return RouteValueDictionary;
       }
    }
}
