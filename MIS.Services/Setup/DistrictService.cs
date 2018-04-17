using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using System.Data;
using System.Web;
using MIS.Models.Setup;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;
namespace MIS.Services.Setup
{
    public class DistrictService
    {
        #region Insert/Update/Delete District Information
        /// <summary>
        /// Function to Insert/Update/Delete District
        /// </summary>
        /// <param name="objMenu"></param>
        /// <param name="mode"></param>
        /// <returns>Bool</returns>
        public bool DistrictUID(MISDistrict objDistrict, string mode, out string exc)
        {
            QueryResult qResult = null;
            MisDistrictInfo obj = new MisDistrictInfo();
            bool res = false;
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";
                obj.DistrictCd = objDistrict.DistrictCd;
                obj.DefinedCd = Utils.ConvertToString(objDistrict.DefinedCd);
                obj.DescEng = Utils.ConvertToString(objDistrict.DescEng);
                obj.DescLoc = Utils.ConvertToString(objDistrict.DescLoc);
                obj.ShortName = Utils.ConvertToString(objDistrict.ShortName);
                obj.ShortNameLoc = Utils.ConvertToString(objDistrict.ShortNameLoc);
                if (objDistrict.ZoneCd.ConvertToString() != "")
                {
                    obj.ZoneCd = Convert.ToDecimal(GetData.GetCodeFor(DataType.Zone, Convert.ToString(objDistrict.ZoneCd)));
                }
                obj.Disabled = objDistrict.Disabled;
                obj.Approved = objDistrict.Approved;
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
        public bool CheckDuplicateDistrict(Decimal? districtCode)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = @"select DISTRICT_CD FROM MIS_DISTRICT WHERE DISTRICT_CD='" + districtCode + "'";
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
        public bool CheckDuplicateDefinedCode(string DefinedCode, string districtCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (districtCode == string.Empty)
                {
                    cmdText = "select * from MIS_DISTRICT where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_DISTRICT where DEFINED_CD ='" + DefinedCode + "' and DISTRICT_CD !='" + districtCode + "'";
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

        #region Get District List
        public DataTable GetDistricts(string initialStr, string zoneCode, string orderby, string order)
        {
            DataTable dt = null;
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
               
                cmdText = @"select T1.DEFINED_CD, T1.DISTRICT_CD," + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " DESCRIPTION," + Utils.ToggleLanguage("T1.SHORT_NAME", "T1.SHORT_NAME_LOC") + " SHORT_NAME," + Utils.ToggleLanguage("T2.DESC_ENG", "T2.DESC_LOC") + " ZONE, T1.APPROVED FROM MIS_DISTRICT T1 INNER JOIN MIS_ZONE T2 ON T1.ZONE_CD=T2.ZONE_CD WHERE 1=1";
                if (zoneCode != "")
                {
                    cmdText += " AND T1.ZONE_CD ='" + Convert.ToDecimal(zoneCode) + "'";
                }
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" AND " + Utils.ToggleLanguage("Upper(T1.DESC_ENG)", "T1.DESC_LOC") + " like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "T1.DEFINED_CD")
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
                    cmdText += String.Format(" order by " + Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC") + " ASC");
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
        }
        #endregion

        #region Populate Data For Updates
        public MISDistrict PopulateDistrictDetails(Decimal? DistrictCode)
        {
            MISDistrict obj = new MISDistrict();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = String.Format("select * from MIS_DISTRICT where DISTRICT_CD='" + DistrictCode + "'");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
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
                    obj.DescLoc = dtbl.Rows[0]["DESC_LOC"].ToString();
                    obj.DescEng = dtbl.Rows[0]["DESC_ENG"].ToString();
                    obj.ShortNameLoc = dtbl.Rows[0]["SHORT_NAME_LOC"].ToString();
                    obj.ShortName = dtbl.Rows[0]["SHORT_NAME"].ToString();
                    obj.ZoneCd = Convert.ToDecimal(GetData.GetDefinedCodeFor(DataType.Zone, dtbl.Rows[0]["ZONE_CD"].ToString()));
                    obj.DefinedCd = dtbl.Rows[0]["DEFINED_CD"].ToString();
                    if (obj.OrderNo != null)
                    {
                        obj.OrderNo = Convert.ToDecimal(dtbl.Rows[0]["ORDER_NO"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                obj = null;
            }
            return obj;
        }
        #endregion

        #region Fill DropDownLists
        public List<MISDistrict> GetZone(string id, string desc)
        {
            DataTable dtbl = null;
            List<MISDistrict> lstZone = new List<MISDistrict>();
            try
            {
                string cmdText = string.Empty;
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select DEFINED_CD,ZONE_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_ZONE where 1=1";
                    if (id != null && id != "")
                    {
                        cmdText += " AND DEFINED_CD LIKE '%" + Convert.ToDecimal(id) + "%'";
                    }
                    if (desc != null && desc != "")
                    {
                        cmdText += " AND " + Utils.ToggleLanguage("UPPER(DESC_ENG)", "DESC_LOC") + " LIKE '%" + desc.ToUpper() + "%'";
                    }
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
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstZone.Add(new MISDistrict { ZoneCd = (Convert.ToDecimal((drow["DEFINED_CD"]).ToString())), ZoneName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception ex)
            {
                lstZone = null;
                ExceptionManager.AppendLog(ex);
            }
            return lstZone;
        }


        #endregion
    }
}