using EntityFramework;
using ExceptionHandler;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;


namespace MIS.Services.Setup
{
    public class CountrySetupService
    {
        public void insertCountryName(NHRScountrySetup objcountrysetup)
        {
            NHRSCountryInfo objnhrscountryinfo = new NHRSCountryInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    if (objcountrysetup.Mode == "I")
                    {
                        objnhrscountryinfo.COUNTRY_CD = GetMaxvalue().ToDecimal();
                    }
                    if (objcountrysetup.Mode == "U" || objcountrysetup.Mode == "D" || objcountrysetup.Mode == "A")
                    {
                        objnhrscountryinfo.COUNTRY_CD = objcountrysetup.country_cd;
                    }
                    objnhrscountryinfo.COUNTRY_CD = objcountrysetup.country_cd;
                    objnhrscountryinfo.DEFINED_CD = objcountrysetup.defined_cd;
                    objnhrscountryinfo.DESC_ENG = objcountrysetup.desc_eng;
                    objnhrscountryinfo.DESC_LOC = objcountrysetup.desc_loc;
                    objnhrscountryinfo.SHORT_NAME = objcountrysetup.short_name;
                    objnhrscountryinfo.SHORT_NAME_LOC = objcountrysetup.short_name_loc;
                    objnhrscountryinfo.NATIONALITY = objcountrysetup.nationality;
                    objnhrscountryinfo.NATIONALITY_LOC = objcountrysetup.nationality_loc;
                    objnhrscountryinfo.CITIZENSHIP = objcountrysetup.citizenship;
                    objnhrscountryinfo.CITIZENSHIP_LOC = objcountrysetup.citizenship_loc;
                    objnhrscountryinfo.ORDER_NO = 0;
                    objnhrscountryinfo.DISABLED = "N";
                    objnhrscountryinfo.APPROVED = "N";
                    objnhrscountryinfo.APPROVED_BY = SessionCheck.getSessionUsername();
                    objnhrscountryinfo.APPROVED_DT = DateTime.Now.ToDateTime();
                    objnhrscountryinfo.ENTERED_BY = SessionCheck.getSessionUsername();
                    objnhrscountryinfo.ENTERED_DT = DateTime.Now.ToDateTime();
                    objnhrscountryinfo.Mode = objcountrysetup.Mode;
                    service.PackageName = "PKG_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objnhrscountryinfo, true);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
        public NHRScountrySetup FillCountry(string Countrycd)
        {
            NHRScountrySetup obj = new NHRScountrySetup();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from MIS_COUNTRY where COUNTRY_CD = '" + Countrycd + "'";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        obj = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.country_cd = dt.Rows[0]["COUNTRY_CD"].ToDecimal();
                        obj.defined_cd = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.desc_eng = dt.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.desc_loc = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.short_name = dt.Rows[0]["SHORT_NAME"].ToString();
                        obj.short_name_loc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();
                        obj.nationality = dt.Rows[0]["NATIONALITY"].ToString();
                        obj.citizenship = dt.Rows[0]["CITIZENSHIP"].ToString();
                        
                    }
                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return obj;
        }
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(COUNTRY_CD)),0)+1 from MIS_COUNTRY";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
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

            return result;
        }
        public DataTable getallcountryname(string initialStr)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select COUNTRY_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from MIS_COUNTRY";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '{0}%'", initialStr);
                }
                cmdText += String.Format(" order by DEFINED_CD");
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
            }
            return dtbl;
        }
        public DataTable getcountrylist(string initialStr)
        {
            DataTable dtbl = null;
            string cmdText = "";
            NHRScountrySetup objcountry = new NHRScountrySetup();
            string countrycd = objcountry.country_cd.ConvertToString();
            string definedcd = objcountry.defined_cd.ConvertToString();
            string desceng = initialStr.ToUpper();
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select COUNTRY_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from MIS_COUNTRY where 1=1";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" and Upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '{0}%'", desceng);

                }
                //if (definedcd != "")
                //{
                //    cmdText += String.Format(" and DEFINED_CD= '" + definedcd + "'");
                //}
                
                cmdText += String.Format(" order by DEFINED_CD");
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
            }
            return dtbl;
        }
    }
}
