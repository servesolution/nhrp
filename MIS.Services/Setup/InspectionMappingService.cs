using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
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
    public class InspectionMappingService
    {
        public void ManageInspectionMapping(InspectionMapping objInsMapMod)
        {
            InspectionMappingE obj = new InspectionMappingE();
            QueryResult qr = new QueryResult();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    obj.InspectionmapDefCd = objInsMapMod.InspectionMapDefCd;
                    obj.InspectionmapCd = objInsMapMod.InspectionMapCd;
                    obj.InspectionmapConsCd = objInsMapMod.InspectionMapConsCd;
                    obj.InspectionmapRoofCd = objInsMapMod.InspectionMapRoofCd;
                    obj.InspectionmapHouseCd = objInsMapMod.InspectionMapHouseCd;
                    obj.Mode = objInsMapMod.MODE;
                    if (objInsMapMod.MODE == "I" || objInsMapMod.MODE == "U")
                    {
                        obj.InspectionmapEnterDateLoc = objInsMapMod.InspectionMapEnterDateLoc.ConvertToString();
                        obj.InspectionmapApproved = objInsMapMod.InspectionMapApproved.ConvertToString();
                        obj.InspectionmapApprovedDateLoc = objInsMapMod.InspectionMapApprovedDateLoc.ConvertToString();
                        obj.InspectionmapUpdatedDateLoc = objInsMapMod.InspectionMapUpdatedDateLoc.ConvertToString();

                        obj.InspectionmapEnterBy = SessionCheck.getSessionUsername();
                        obj.InspectionmapEnterDate = System.DateTime.Now;
                        obj.InspectionmapApprovedBy = SessionCheck.getSessionUsername();
                        obj.InspectionMapApprovedDate = System.DateTime.Now;
                        obj.InspectionmapUpdatedBy = SessionCheck.getSessionUsername();
                        obj.InspectionmapUpdatedDate = System.DateTime.Now;
                    }


                    service.PackageName = "PKG_NHRS_INSPECTION";
                    service.Begin();

                    qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    // service.RollBack();

                    //   exc = ex.ToString();
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
        public InspectionMapping FillInspectionMapping(string ImapCd)
        {
            InspectionMapping obj = new InspectionMapping();
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_INSPECTION_MDL_MAPPING where MODEL_MAPPING_CD = '" + ImapCd + "'";
                    try
                    {
                        dtbl = service.GetDataTable(new
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
                    if (dtbl.Rows.Count > 0)
                    {

                        obj.InspectionMapCd = dtbl.Rows[0]["MODEL_MAPPING_CD"].ToDecimal();
                        obj.InspectionMapConsCd = dtbl.Rows[0]["CONSTRUCTION_MAT_CD"].ToDecimal();
                        obj.InspectionMapRoofCd = dtbl.Rows[0]["ROOF_MAT_CD"].ToDecimal();
                        obj.InspectionMapHouseCd = dtbl.Rows[0]["HOUSE_MODEL_CD"].ToDecimal();
                        obj.InspectionMapUpdatedDateLoc = dtbl.Rows[0]["APPROVED_DATE_LOC"].ToString();
                        obj.InspectionMapEnterDateLoc = dtbl.Rows[0]["ENTERED_DATE_LOC"].ToString();
                        obj.InspectionMapApproved = dtbl.Rows[0]["APPROVED"].ToString();
                        obj.InspectionMapApprovedDateLoc = dtbl.Rows[0]["APPROVED_DATE_LOC"].ToString();
                        obj.InspectionMapDefCd = dtbl.Rows[0]["DEFINED_CD"].ToString();
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

        public DataTable InspectionMappingList()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                // cmdText ="select MODEL_MAPPING_CD,CONSTRUCTION_MAT_CD,ROOF_MAT_CD ,HOUSE_MODEL_CD  from NHRS_INSPECTION_MDL_MAPPING WHERE 1=1 ";
                cmdText = "SELECT NIMM.MODEL_MAPPING_CD,NIMM.DEFINED_CD,NIMM.CONSTRUCTION_MAT_CD,NIMM.ROOF_MAT_CD,NIMM.HOUSE_MODEL_CD, " 
                           + Utils.ToggleLanguage("IMM.NAME_ENG", "IMM.NAME_LOC") + " AS PILLAR_TYPE, "
                           + Utils.ToggleLanguage("NRM.NAME_ENG", "NRM.NAME_LOC") + " AS ROOF_MATERIAL, "
                           + Utils.ToggleLanguage("NHM.NAME_ENG", "NHM.NAME_LOC") + " AS DESIGN_NAME FROM NHRS_INSPECTION_MDL_MAPPING NIMM  "
                           +"LEFT OUTER JOIN NHRS_RC_MATERIAL NRM ON NRM.RC_MAT_CD=NIMM.ROOF_MAT_CD  "
                           +"LEFT OUTER JOIN NHRS_CONSTRUCTION_MATERIAL IMM ON NIMM.CONSTRUCTION_MAT_CD=IMM.CONSTRUCTION_MAT_CD "
                           +"LEFT OUTER  JOIN NHRS_HOUSE_MODEL NHM ON NIMM.HOUSE_MODEL_CD=NHM.MODEL_ID WHERE 1=1";
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

        public bool checkDuplicateMapping(string designID, string roofID, string constructID)
        {
            bool value = true;
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                if (roofID=="")
                {
                    cmdText = "SELECT MODEL_MAPPING_CD FROM NHRS_INSPECTION_MDL_MAPPING WHERE CONSTRUCTION_MAT_CD='" + constructID + "'AND HOUSE_MODEL_CD='" + designID + "'";

                }
                if (constructID == "")
                {
                    cmdText = "SELECT MODEL_MAPPING_CD FROM NHRS_INSPECTION_MDL_MAPPING WHERE ROOF_MAT_CD='" + roofID + "'AND HOUSE_MODEL_CD='" + designID + "'";

                }
                if (roofID != "" && constructID != "")
                {
                    cmdText = "SELECT MODEL_MAPPING_CD FROM NHRS_INSPECTION_MDL_MAPPING WHERE CONSTRUCTION_MAT_CD='" + constructID + "'AND ROOF_MAT_CD='" + roofID + "'AND HOUSE_MODEL_CD='" + designID + "'";

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
                if(dtbl!=null && dtbl.Rows.Count>0)
                {
                    value = false;
                }
            }
            return value;
        }



    }


}


