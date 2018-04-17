using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Setup;
using EntityFramework;
using System.Data;
using System.Web;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;

namespace MIS.Services.Setup
{
    public class CountryService
    {
        //ServiceFactory service = null;
        #region Insert/Update/Delete Country Information
        public bool CountryUID(MISCountry objCntry, string mode, out string exc)
        {
            QueryResult qResult = null;
            MisCountryInfo obj = new MisCountryInfo();
            bool res = false;
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";
                obj.CountryCd = objCntry.countryCd;
                obj.DescEng = Utils.ConvertToString(objCntry.descEng);
                obj.DescLoc = Utils.ConvertToString(objCntry.descLoc);
                obj.DefinedCd = Utils.ConvertToString(objCntry.definedCd);
                obj.OrderNo = objCntry.orderNo;
                obj.ShortName = Utils.ConvertToString(objCntry.shortName);
                obj.ShortNameLoc = Utils.ConvertToString(objCntry.shortNameLoc);
                obj.Nationality = Utils.ConvertToString(objCntry.nationality);
                obj.NationalityLoc = Utils.ConvertToString(objCntry.nationalityLoc);
                obj.Citizenship = Utils.ConvertToString(objCntry.citizenship);
                obj.CitizenshipLoc = Utils.ConvertToString(objCntry.citizenshipLoc);
                obj.Disabled = objCntry.disabled;
                obj.Approved = objCntry.approved;
                obj.ApprovedBy = SessionCheck.getSessionUsername();
                obj.EnteredBy = SessionCheck.getSessionUsername();
                obj.IPAddress = CommonVariables.IPAddress;
                obj.Mode = mode;                
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    exc = ex.ToString();
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
            if (qResult != null)
            {
                res = qResult.IsSuccess;
            }
            return res;
        }
        #endregion

        #region Check for Uniqueness while inserting and updating
        public bool CheckDuplicateCountry(Decimal? cntryCode, string cntryNameEng, string cntryNameLoc)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {                
                cmdText = @"select COUNTRY_CD FROM MIS_COUNTRY WHERE COUNTRY_CD='" + cntryCode + "' OR UPPER(DESC_ENG)='" + cntryNameEng + "' OR DESC_LOC='" + cntryNameLoc + "'";
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string CountryCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (CountryCode == string.Empty)
                {
                    cmdText = "select * from MIS_COUNTRY where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_COUNTRY where DEFINED_CD ='" + DefinedCode + "' and COUNTRY_CD !='" + CountryCode + "'";
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
        #endregion

        #region Get Country List
        public DataTable GetCountries(string initialStr, string orderby, string order)
        {
            DataTable dt = null;
            string cmdText = string.Empty;          
            using (ServiceFactory service = new ServiceFactory())
            {                
               
                cmdText = @"select DEFINED_CD, COUNTRY_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " SHORT_NAME," + Utils.ToggleLanguage("NATIONALITY", "NATIONALITY_LOC") + " NATIONALITY," + Utils.ToggleLanguage("CITIZENSHIP", "CITIZENSHIP_LOC") + " CITIZENSHIP, APPROVED FROM MIS_COUNTRY WHERE 1=1";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" AND " + Utils.ToggleLanguage("Upper(DESC_ENG)", "DESC_LOC") + " like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "DEFINED_CD")
                    {
                        cmdText += String.Format(" order by to_number({0}) {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
                }
                else
                {
                    cmdText += String.Format(" order by " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " ASC");
                }
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
                return dt;
            }
        }
        #endregion

        #region Populate Data For Updates
        public MISCountry PopulateCountryDetails(decimal CountryCode)
        {
            MISCountry obj = new MISCountry();
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmdText = String.Format("select * from MIS_COUNTRY where COUNTRY_CD='" + CountryCode + "'");
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
                    if (dtbl.Rows.Count > 0)
                    {
                        obj.descLoc = dtbl.Rows[0]["DESC_LOC"].ToString();
                        obj.descEng = dtbl.Rows[0]["DESC_ENG"].ToString();
                        obj.shortNameLoc = dtbl.Rows[0]["SHORT_NAME_LOC"].ToString();
                        obj.shortName = dtbl.Rows[0]["SHORT_NAME"].ToString();
                        obj.nationalityLoc = dtbl.Rows[0]["NATIONALITY_LOC"].ToString();
                        obj.nationality = dtbl.Rows[0]["NATIONALITY"].ToString();
                        obj.citizenshipLoc = dtbl.Rows[0]["CITIZENSHIP_LOC"].ToString();
                        obj.citizenship = dtbl.Rows[0]["CITIZENSHIP"].ToString();
                        obj.definedCd = dtbl.Rows[0]["DEFINED_CD"].ToString();
                        obj.orderNo = Convert.ToDecimal(dtbl.Rows[0]["ORDER_NO"].ToString());
                    }
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
            }
            return obj;
        }
        #endregion
    }
}
