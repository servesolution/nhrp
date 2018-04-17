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
    public class DistrictSetupService
    {
        DataTable dtbl = null;
        string cmdText = "";
        public void SaveDistrict(DistrictSetup objDistrictSetup)
        {
            DistrictSetupInfo objDistrictSetupInfo = new DistrictSetupInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    if (objDistrictSetup.mode == "A" || objDistrictSetup.mode == "U" || objDistrictSetup.mode=="D")
                    {
                        objDistrictSetupInfo.DistrictCd = objDistrictSetup.district_cd;
                    }
                    objDistrictSetupInfo.DefinedCd = objDistrictSetup.defined_cd;
                    objDistrictSetupInfo.DescEng = objDistrictSetup.desc_eng;
                    objDistrictSetupInfo.DescLoc = objDistrictSetup.desc_loc;
                    objDistrictSetupInfo.ShortName = objDistrictSetup.short_name;
                    objDistrictSetupInfo.ShortNameLoc = objDistrictSetup.short_name_loc;
                    objDistrictSetupInfo.ZoneCd = objDistrictSetup.zone_cd;
                    objDistrictSetupInfo.disabled = "N";
                    objDistrictSetupInfo.approved = "N";
                    objDistrictSetupInfo.ApprovedBy = SessionCheck.getSessionUsername();
                    objDistrictSetupInfo.ApprovedDt = DateTime.Now.ToDateTime();
                    objDistrictSetupInfo.EnteredBy = SessionCheck.getSessionUsername();
                    objDistrictSetupInfo.EnteredDt = DateTime.Now.ToDateTime();
                    objDistrictSetupInfo.Mode = objDistrictSetup.mode;
                    service.PackageName = "PKG_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objDistrictSetupInfo, true);
                }
                catch(OracleException oe)
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
        
        public DataTable getAllDistrictInfo()
        {            
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select district_cd, defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + "as Desc," + 
                    Utils.ToggleLanguage("short_name", "short_name_loc") + "as Short Name, zone_cd from mis_district";
                cmdText += String.Format(" order by defined_cd");
                try
                {

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

        public DataTable getDistrictList(DistrictSetup objDistrictSetup)
        {
            decimal? districtCd = objDistrictSetup.district_cd;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select district_cd, defined_cd," + Utils.ToggleLanguage("desc_eng", "desc_loc") + " as Description," +
                        Utils.ToggleLanguage("short_name", "short_name_loc") + " as ShortName, zone_cd, approved from mis_district where 1=1";
                if (districtCd != null)
                {
                    cmdText += String.Format(" and district_cd = '" + districtCd + "'");
                }
                cmdText += String.Format(" order by defined_cd");
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(new
                    {
                        query=cmdText,
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

        public DataTable getDetailByDistrictCd(string id)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select * from mis_district where district_cd ='" + id + "'";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(new { 
                        query=cmdText,
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
