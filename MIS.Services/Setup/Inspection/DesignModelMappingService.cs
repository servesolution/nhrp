using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Setup.Inspection;
using EntityFramework;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Data;

namespace MIS.Services.Setup.Inspection
{
    public class DesignModelMappingService
    {
        public bool saveModelDesignMapping (ModelDesignMapModelClass objModel)
        {
            String definedCd ="";
            bool result = false;
            QueryResult qr = null;
           using( ServiceFactory sf = new ServiceFactory())

           {
                  try
                    {
                        sf.Begin();
                        sf.PackageName = "PKG_NHRS_INSPECTION";
                        qr = sf.SubmitChanges("PR_NHRS_DESIGN_MODEL_MAP",
                                    objModel.mode.ConvertToString(),
                                    objModel.MapCode.ToDecimal(),
                                    definedCd.ConvertToString(),
                                    objModel.houseDesignCode.ToDecimal(),
                                    objModel.houseModelCode.ToDecimal(),
                                    SessionCheck.getSessionUsername(),
                                    DateTime.Now,
                                    System.DateTime.Now.ConvertToString(),

                                    "",
                                    SessionCheck.getSessionUsername(),
                                    DateTime.Now,
                                    System.DateTime.Now.ConvertToString(),

                                    SessionCheck.getSessionUsername(),
                                    DateTime.Now,
                                    System.DateTime.Now.ConvertToString()
                            );
                    }
                  catch (OracleException oe)
                  {
                      sf.RollBack();
                      ExceptionManager.AppendLog(oe);
                  }
                  catch (Exception ex)
                  {
                      sf.RollBack();
                      ExceptionManager.AppendLog(ex);
                  }
                  finally
                  {
                      if (sf.Transaction != null)
                          sf.End();
                  }
           }
         
            return result;
        }

        //get all design model mapping 
        public ModelDesignMapModelClass getDesignModelMapping(string id)
        {
            DataTable dt = new DataTable();
            QueryResult qr = new QueryResult();
            ModelDesignMapModelClass objModel = new ModelDesignMapModelClass();
            using(ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmdText = "SELECT * FROM NHRS_DESIGN_MODEL_MAP WHERE DESIGN_MODEL_MAP_CD='" + id + "'";
                   sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new { }
                    });
                }
                catch (OracleException oe)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }
                if(dt!=null)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        objModel.MapCode = dr["DESIGN_MODEL_MAP_CD"].ConvertToString();
                        objModel.houseDesignCode = dr["HOUSE_DESIGN_CD"].ConvertToString();
                        objModel.houseModelCode = dr["MODEL_ID"].ConvertToString();
                        
                    }
                }
            }
            return objModel;
        }

        //get mapped record by id 
        public DataTable getAllDesignModelMapping()
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmdText = "SELECT NDMP.DESIGN_MODEL_MAP_CD, NDMP.HOUSE_DESIGN_CD, NDMP.MODEL_ID," + Utils.ToggleLanguage("NHD.DESC_ENG", "NHD.DESC_LOC ") + "AS DESIGN_DESC,"
                    + Utils.ToggleLanguage("NHM.NAME_ENG", "NHM.NAME_LOC ")+ "AS MODEL_DESC "
                    +" FROM NHRS_DESIGN_MODEL_MAP NDMP "
                    + "JOIN NHRS_HOUSE_MODEL NHM ON NDMP.MODEL_ID= NHM.MODEL_ID "
                    + "JOIN NHRS_HOUSE_DESIGN NHD ON NDMP.HOUSE_DESIGN_CD= NHD.HOUSE_DESIGN_CD  WHERE 1=1";
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new { }
                    });
                }
                catch (OracleException oe)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }
                
            }
            return dt;
        }

       
    }
}
