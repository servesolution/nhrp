using EntityFramework;
using ExceptionHandler;
using MIS.Models.NHRP;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;

namespace MIS.Services.NHRP.Edit
{
    public class NHRSInterviewHouseDetailService
    {
        public DataTable BuildingStructureDetail(string BuildingStructureNo, string HouseOwnerID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM VW_HOUSE_BUILDING_DTL WHERE BUILDING_STRUCTURE_NO='" + BuildingStructureNo + "' and HOUSE_OWNER_ID = '" + HouseOwnerID + "'"; //+ " AND DEFINED_CD = " + definedCd  169  ;
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
            }
            return dt;
        }
        public DataTable GetHousehold(string StructureNo)
        {
            NhrsHouseDetail obj = new NhrsHouseDetail();
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = String.Format("select * from VW_HOUSEHOLD_MEMBER where BUILDING_STRUCTURE_NO ='" + StructureNo + "'");
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
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }
        public DataTable SuperStructureofHouseName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_SUPERSTRUCTURE_MATERIAL";
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
            }
            return dt;
        }
        public DataTable SuperStructureofHouse(string HouseOwnerID,string StructureNo  )
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_SUPERSTRUCTURE_MATERIAL WHERE HOUSE_OWNER_ID='" + HouseOwnerID + "' AND BUILDING_STRUCTURE_NO='"+StructureNo+"'";
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
            }
            return dt;
        }
        public DataSet GetBuildingAssessmentDetail(string HouseOwnerID, string StructureNo)
        {
            //NHRS_HOUSEHOLD_MEMBER obj = new NHRS_HOUSEHOLD_MEMBER();
            DataSet ds = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_VIEWS";
                    ds = service.GetDataSetOracle(true, "PR_BUILDING_STRUCTURE_VIEW", HouseOwnerID.ToDecimal(), StructureNo.ToDecimal(), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);

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
            return ds;
        }
        public DataTable GeoTechnicalName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_GEOTECHNICAL_RISK_TYPE";
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
            }
            return dt;
        }
        public DataTable GeoTechnicalDetail(string HouseOwnerID,string StructureNo)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_GEOTEHNICAL WHERE HOUSE_OWNER_ID='" + HouseOwnerID + "' AND BUILDING_STRUCTURE_NO='" + StructureNo + "'";
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
            }
            return dt;
        }
        public DataTable SecondaryUseName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_SECONDARY_OCCUPANCY";
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
            }
            return dt;
        }
        public DataTable SecondaryUseDetail(string HouseOwnerID, string StructureNo)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.VW_BA_SECONDARY_OCCUPANCY WHERE HOUSE_OWNER_ID='" + HouseOwnerID + "' AND BUILDING_STRUCTURE_NO='" + StructureNo + "'";
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
            }
            return dt;
        }
        public DataTable GetAllDamageDetail()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_DAMAGE_DETAIL ORDER BY DAMAGE_CD";
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
            }
            return dt;
        }

        public DataTable GetAllDamagestructure()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DESC_ENG FROM NHRS.NHRS_SUPERSTRUCTURE_MATERIAL";
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
            }
            return dt;
        }

      
        public void UpdateNHRSInterviewHouseDetail(NHRS_HOUSE_BUILDING_DTL nhrshousedet, Dictionary<string, string> lstSuperStruct, Dictionary<string, string> lstDamageDet, Dictionary<string, string> lstGeoTech, Dictionary<string, string> lstSecUse)
        {
            NhrsBuildingAssMstInfo buildingAssMst = new NhrsBuildingAssMstInfo();
            NhrsHhSuperstructureMatInfo objNHRSHhSuperStructMatInfo = new NhrsHhSuperstructureMatInfo();
            NhrsBaGeotechnicalRiskInfo objNHRSBaGeotechnicalRiskInfo = new NhrsBaGeotechnicalRiskInfo();
            NhrsBaSecOccupancyInfo objNHRSBaSecOccupancyInfo = new NhrsBaSecOccupancyInfo();
            NhrsBuildingAssDtlInfo objNHRSBuildingAssDtl = new NhrsBuildingAssDtlInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    buildingAssMst.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                    buildingAssMst.BuildingStructureNo = nhrshousedet.BUILDING_STRUCTURE_NO;
                    buildingAssMst.Houselandlegalowner = nhrshousedet.HOUSE_LAND_LEGAL_OWNER_CD;
                    buildingAssMst.BuildingConditionCd = nhrshousedet.BUILDING_CONDITION_CD;
                    buildingAssMst.StoreysCntAfter = nhrshousedet.STOREYS_CNT_AFTER;
                    buildingAssMst.StoreysCntBefore = nhrshousedet.STOREYS_CNT_BEFORE;
                    buildingAssMst.HouseAge = nhrshousedet.HOUSE_AGE;
                    buildingAssMst.PlinthAreaCd = Convert.ToDecimal(nhrshousedet.PLINTH_AREA);
                    buildingAssMst.HouseHeightAfterEQ = nhrshousedet.HOUSE_HEIGHT_AFTER_EQ;
                    buildingAssMst.HouseHeightBeforeEQ = nhrshousedet.HOUSE_HEIGHT_BEFORE_EQ;
                    buildingAssMst.GroundSurfaceCd = nhrshousedet.GROUND_SURFACE_CD;
                    buildingAssMst.FoundationTypeCd = nhrshousedet.FOUNDATION_TYPE_CD;
                    buildingAssMst.RcMaterialCd = nhrshousedet.RC_MATERIAL_CD;
                    buildingAssMst.FcMaterialCd = nhrshousedet.FC_MATERIAL_CD;
                    buildingAssMst.ScMaterialCd = nhrshousedet.SC_MATERIAL_CD;
                    buildingAssMst.BuildingPositionCd = nhrshousedet.BUILDING_POSITION_CD;
                    buildingAssMst.BuildingPlanCd = nhrshousedet.BUILDING_PLAN_CD;
                    buildingAssMst.IsGeotechnicalRisk = nhrshousedet.IS_GEOTECHNICAL_RISK;
                    buildingAssMst.AssessedAreaCd = nhrshousedet.ASSESSED_AREA_CD;
                    buildingAssMst.DamageGradeCd = nhrshousedet.DAMAGE_GRADE_CD;
                    buildingAssMst.TechsolutionCd = nhrshousedet.TECHSOLUTION_CD;
                    buildingAssMst.ReconstructionStarted = nhrshousedet.RECONSTRUCTION_STARTED;
                    buildingAssMst.IsSecondaryUse = nhrshousedet.IS_SECONDARY_USE;
                    buildingAssMst.Latitude = nhrshousedet.LATITUDE;
                    buildingAssMst.Longitude = nhrshousedet.LONGITUDE;
                    buildingAssMst.Altitude = nhrshousedet.ALTITUDE;
                    buildingAssMst.Accuracy = nhrshousedet.ACCURACY;
                    buildingAssMst.GeneralComments = buildingAssMst.GeneralCommentsLoc = nhrshousedet.GENERAL_COMMENTS;
                    buildingAssMst.Householdcntaftereq = nhrshousedet.HOUSEHOLD_CNT_AFTER_EQ;
                    buildingAssMst.Mode = "U";
                    service.PackageName = "PKG_BUILDING_ASSESSMENT";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(buildingAssMst, true);
                    if (qr.IsSuccess)
                    {
                        objNHRSHhSuperStructMatInfo = new NhrsHhSuperstructureMatInfo();
                        objNHRSHhSuperStructMatInfo.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                        objNHRSHhSuperStructMatInfo.BuildingStructureNo = nhrshousedet.BUILDING_STRUCTURE_NO;
                        objNHRSHhSuperStructMatInfo.Mode = "DA";
                        qr = service.SubmitChanges(objNHRSHhSuperStructMatInfo, true);
                        foreach (var item in lstSuperStruct)
                        {
                            if (item.Value == "Y")
                            {
                                objNHRSHhSuperStructMatInfo.SuperstructureMatId = Convert.ToDecimal(item.Key.Split('_')[1].ConvertToString());
                                objNHRSHhSuperStructMatInfo.NhrsUuid = nhrshousedet.NHRS_UUID;
                                objNHRSHhSuperStructMatInfo.Approved = "N";
                                objNHRSHhSuperStructMatInfo.EnteredBy = SessionCheck.getSessionUsername();
                                objNHRSHhSuperStructMatInfo.EnteredDt = DateTime.Now;
                                objNHRSHhSuperStructMatInfo.IPAddress = CommonVariables.IPAddress;
                                objNHRSHhSuperStructMatInfo.BatchId = nhrshousedet.BATCH_ID;
                                objNHRSHhSuperStructMatInfo.Mode = "I";
                                qr = service.SubmitChanges(objNHRSHhSuperStructMatInfo, true);
                            }
                        }
                    }
                    if (qr.IsSuccess)
                    {
                        objNHRSBaGeotechnicalRiskInfo = new NhrsBaGeotechnicalRiskInfo();
                        objNHRSBaGeotechnicalRiskInfo.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                        objNHRSBaGeotechnicalRiskInfo.BuildingStructureNo = nhrshousedet.BUILDING_STRUCTURE_NO;
                        objNHRSBaGeotechnicalRiskInfo.Mode = "DA";
                        qr = service.SubmitChanges(objNHRSBaGeotechnicalRiskInfo, true);
                        foreach (var item in lstGeoTech)
                        {
                            if (item.Value == "Y")
                            {
                                objNHRSBaGeotechnicalRiskInfo.GeotechnicalRiskTypeCd = Convert.ToDecimal(item.Key.Split('_')[1].ConvertToString());
                                //objNHRSBaGeotechnicalRiskInfo.NhrsUuid = nhrshousedet.NHRS_UUID;
                                objNHRSBaGeotechnicalRiskInfo.Approved = "N";
                                objNHRSBaGeotechnicalRiskInfo.EnteredBy = SessionCheck.getSessionUsername();
                                objNHRSBaGeotechnicalRiskInfo.EnteredDt = DateTime.Now;
                                objNHRSBaGeotechnicalRiskInfo.IPAddress = CommonVariables.IPAddress;
                                objNHRSBaGeotechnicalRiskInfo.BatchId = nhrshousedet.BATCH_ID;
                                objNHRSBaGeotechnicalRiskInfo.Mode = "I";
                                qr = service.SubmitChanges(objNHRSBaGeotechnicalRiskInfo, true);
                            }
                        }
                    }
                    if (qr.IsSuccess)
                    {
                        objNHRSBaSecOccupancyInfo = new NhrsBaSecOccupancyInfo();
                        objNHRSBaSecOccupancyInfo.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                        objNHRSBaSecOccupancyInfo.BuildingStructureNo = nhrshousedet.BUILDING_STRUCTURE_NO;
                        objNHRSBaSecOccupancyInfo.Mode = "DA";
                        qr = service.SubmitChanges(objNHRSBaSecOccupancyInfo, true);
                        foreach (var item in lstSecUse)
                        {
                            if (item.Value == "Y")
                            {
                                objNHRSBaSecOccupancyInfo.SecOccupancyCd = Convert.ToDecimal(item.Key.Split('_')[1].ConvertToString());
                                objNHRSBaSecOccupancyInfo.NhrsUuid = nhrshousedet.NHRS_UUID;
                                objNHRSBaSecOccupancyInfo.Approved = "N";
                                objNHRSBaSecOccupancyInfo.EnteredBy = SessionCheck.getSessionUsername();
                                objNHRSBaSecOccupancyInfo.EnteredDt = DateTime.Now;
                                objNHRSBaSecOccupancyInfo.IPAddress = CommonVariables.IPAddress;
                                objNHRSBaSecOccupancyInfo.BatchId = nhrshousedet.BATCH_ID;
                                objNHRSBaSecOccupancyInfo.Mode = "I";
                                qr = service.SubmitChanges(objNHRSBaSecOccupancyInfo, true);
                            }
                        }
                    }
                    if (qr.IsSuccess)
                    {
                        objNHRSBuildingAssDtl = new NhrsBuildingAssDtlInfo();
                        objNHRSBuildingAssDtl.HouseOwnerId = nhrshousedet.HOUSE_OWNER_ID;
                        objNHRSBuildingAssDtl.DistrictCd = nhrshousedet.DISTRICT_CD.ToDecimal();
                        objNHRSBuildingAssDtl.BuildingStructureNo = nhrshousedet.BUILDING_STRUCTURE_NO;
                        objNHRSBuildingAssDtl.Mode = "DA";
                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                        foreach (var item in lstDamageDet)
                        {
                            String damageLevelStr = String.Empty;
                            if (item.Value == "Y")
                            {
                                objNHRSBuildingAssDtl.DamageCd = Convert.ToDecimal(item.Key.Split('_')[1].ConvertToString());
                                damageLevelStr = item.Key.Split('_')[2].ConvertToString() == "SE" ? "1" : damageLevelStr;
                                damageLevelStr = item.Key.Split('_')[2].ConvertToString() == "MH" ? "2" : damageLevelStr;
                                damageLevelStr = item.Key.Split('_')[2].ConvertToString() == "IN" ? "3" : damageLevelStr;
                                damageLevelStr = item.Key.Split('_')[2].ConvertToString() == "NO" ? "10" : damageLevelStr;
                                damageLevelStr = item.Key.Split('_')[2].ConvertToString() == "NA" ? "11" : damageLevelStr;
                                objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelStr);
                                if (item.Key.Split('_').Count() > 3)
                                {
                                    objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item.Key.Split('_')[3].ConvertToString());   
                                }
                                objNHRSBuildingAssDtl.BatchId = nhrshousedet.BATCH_ID;
                                objNHRSBuildingAssDtl.NhrsUuid = nhrshousedet.NHRS_UUID;
                                objNHRSBuildingAssDtl.Approved = "N";
                                objNHRSBuildingAssDtl.EnteredBy = SessionCheck.getSessionUsername();
                                objNHRSBuildingAssDtl.EnteredDt = DateTime.Now;
                                objNHRSBuildingAssDtl.IPAddress = CommonVariables.IPAddress;
                                objNHRSBuildingAssDtl.Mode = "I";
                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                            }
                        }
                    }
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
    }
}
