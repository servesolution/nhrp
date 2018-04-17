using EntityFramework;
using ExceptionHandler;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;

namespace MIS.Services.Inspection
{
    public class InspectionHierarchyDesignService
    {
        public bool SaveTreeDesignInfo(HierarchyDesignMapping objHieDesModl)
        {

            bool result = false;
            QueryResult qr = null;

            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();



                    qr = sf.SubmitChanges("PR_NHRS_INSPECTION_TREE_DESIGN",
                                          objHieDesModl.mode,

                                           objHieDesModl.TreeDesignMapCd.ToDecimal(),
                                           objHieDesModl.DefinedCd.ConvertToString(),
                                           objHieDesModl.Hirarchycd.ToDecimal(),
                                           objHieDesModl.DesignCd.ToDecimal(),
                                           

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
                if (qr != null)
                {
                    result = qr.IsSuccess;
                }


            }
            return (result);
        }


       
        public DataTable getAllTreeDesign()
        {
            DataTable dtbl = new DataTable();
            HierarchyDesignMapping objInspectn = new HierarchyDesignMapping();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            //id = "1-2-3-4-55";

            try
            {


                cmd = " SELECT TDM.TREE_DESIGN_MAPCD, TDM.HIERARCHY_CD,TDM.HOUSE_MODEL_ID,"
                    + Utils.ToggleLanguage("IDD.DESC_ENG","IDD.DESC_LOC") + " AS TREE_DESC,"
                     + Utils.ToggleLanguage("IHM.NAME_ENG", "IHM.NAME_LOC") + " AS DESIGN_DESC "
                +"FROM NHRS_INSPECTION_TREE_DESIGN TDM "
                    + "JOIN NHRS_INSPECTION_DESC_DTL IDD ON TDM.HIERARCHY_CD=IDD.INSPECTION_CODE_ID "
                     + "JOIN NHRS_HOUSE_MODEL IHM ON TDM.HOUSE_MODEL_ID=IHM.MODEL_ID WHERE 1=1";

                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }


        public HierarchyDesignMapping getAllTreeDesign(string ID)
        {
            DataTable dtbl = new DataTable();
            HierarchyDesignMapping objInspectn = new HierarchyDesignMapping();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            //id = "1-2-3-4-55";

            try
            {


                cmd = " SELECT TDM.TREE_DESIGN_MAPCD, TDM.HIERARCHY_CD,TDM.HOUSE_MODEL_ID,"
                    + Utils.ToggleLanguage("IDD.DESC_ENG", "IDD.DESC_LOC") + " AS TREE_DESC,"
                     + Utils.ToggleLanguage("IHM.NAME_ENG", "IHM.NAME_LOC") + " AS DESIGN_DESC "
                + "FROM NHRS_INSPECTION_TREE_DESIGN TDM "
                    + "JOIN NHRS_INSPECTION_DESC_DTL IDD ON TDM.HIERARCHY_CD=IDD.INSPECTION_CODE_ID "
                     + "JOIN NHRS_HOUSE_MODEL IHM ON TDM.HOUSE_MODEL_ID=IHM.MODEL_ID WHERE TREE_DESIGN_MAPCD='"+ID+"'";

                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }

            if(dtbl!=null && dtbl.Rows.Count>0)
            {
                foreach(DataRow dr in dtbl.Rows)
                {
                    objInspectn.TreeDesignMapCd = dr["TREE_DESIGN_MAPCD"].ConvertToString();
                    objInspectn.Hirarchycd = dr["HIERARCHY_CD"].ConvertToString();
                    objInspectn.DesignCd = dr["HOUSE_MODEL_ID"].ConvertToString();


                }
            }
            return objInspectn;
        }
    }
}
