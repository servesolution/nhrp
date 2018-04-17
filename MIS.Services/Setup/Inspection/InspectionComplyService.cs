using EntityFramework;
using MIS.Models.Setup.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Data;

namespace MIS.Services.Setup.Inspection
{
    public class InspectionComplyService
    {
        public bool saveInspectionComplied(Dictionary<string, string> lstComply, InspectionComplyModelClass objComplyModel)
        {
            bool result = false;
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {

                    sf.PackageName = "PKG_NHRS_INSPECTION";
                    sf.Begin();
                    foreach (var item in lstComply)
                    {
                        QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_COMPLY",
                                objComplyModel.Mode,
                                 objComplyModel.InspectionComplyCode.ToDecimal(),
                                 objComplyModel.DefinedCd.ConvertToString(),
                                 objComplyModel.InspectionLevel.ToDecimal(),
                                 objComplyModel.InspectionDesign.ToDecimal(),
                                 item.Key, // ID
                                 item.Value, // Yes/No
                                 SessionCheck.getSessionUsername(),
                                 DateTime.Now,
                                 System.DateTime.Now.ConvertToString(),

                                "",
                                SessionCheck.getSessionUsername(),
                                DateTime.Now,
                                System.DateTime.Now.ConvertToString(),

                                SessionCheck.getSessionUsername(),
                                DateTime.Now,
                                System.DateTime.Now.ConvertToString(),
                                objComplyModel.status.ConvertToString(),
                                objComplyModel.InspectionRemarks.ConvertToString()


                            );
                    }

                    //}




                    //if (InspectionYes != null)
                    //{
                    //    foreach (var InspectionElement in InspectionYes)
                    //    {
                    //        QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_COMPLY",
                    //                objComplyModel.Mode,

                    //                 objComplyModel.InspectionComplyCode.ToDecimal(),
                    //                 objComplyModel.DefinedCd.ConvertToString(),
                    //                 objComplyModel.InspectionLevel.ToDecimal(),
                    //                 objComplyModel.InspectionDesign.ToDecimal(),
                    //                 InspectionElement.ToDecimal(),
                    //                 "Y",//comply flag yes

                    //                 SessionCheck.getSessionUsername(),
                    //                 DateTime.Now,
                    //                 System.DateTime.Now.ConvertToString(),

                    //                "",
                    //                SessionCheck.getSessionUsername(),
                    //                DateTime.Now,
                    //                System.DateTime.Now.ConvertToString(),

                    //                SessionCheck.getSessionUsername(),
                    //                DateTime.Now,
                    //                System.DateTime.Now.ConvertToString()


                    //            );
                    //    }
                    //}
                    //if (InspectionNo != null)
                    // {
                    //    foreach (var InspectionElement in InspectionNo)
                    //    {
                    //        QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_COMPLY",
                    //                objComplyModel.Mode,

                    //                 objComplyModel.InspectionComplyCode.ToDecimal(),
                    //                 objComplyModel.DefinedCd.ConvertToString(),
                    //                 objComplyModel.InspectionLevel.ToDecimal(),
                    //                 objComplyModel.InspectionDesign.ToDecimal(),
                    //                 InspectionElement.ToDecimal(),
                    //                 "N",//comply flag no

                    //                 SessionCheck.getSessionUsername(),
                    //                 DateTime.Now,
                    //                 System.DateTime.Now.ConvertToString(),

                    //                "",
                    //                SessionCheck.getSessionUsername(),
                    //                DateTime.Now,
                    //                System.DateTime.Now.ConvertToString(),

                    //                SessionCheck.getSessionUsername(),
                    //                DateTime.Now,
                    //                System.DateTime.Now.ConvertToString()


                    //            );
                    //    }
                    //}
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
        

        //update comply table 
        public bool updateInspectionCompile(Dictionary<string, string> lstComply, InspectionComplyModelClass objComplyModel)
        {
            bool result = false;
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmdTextDel="DELETE FROM NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN="+objComplyModel.InspectionDesign+"";
                    sf.Begin();
                   QueryResult qr = sf.SubmitChanges(cmdTextDel, null);



                   sf.PackageName = "PKG_NHRS_INSPECTION";
                  
                   foreach (var item in lstComply)
                   {
                       QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_COMPLY",
                               "I",
                                objComplyModel.InspectionComplyCode.ToDecimal(),
                                objComplyModel.DefinedCd.ConvertToString(),
                                objComplyModel.InspectionLevel.ToDecimal(),
                                objComplyModel.InspectionDesign.ToDecimal(),
                                item.Key, // ID
                                item.Value, // Yes/No
                                SessionCheck.getSessionUsername(),
                                DateTime.Now,
                                System.DateTime.Now.ConvertToString(),

                               "",
                               SessionCheck.getSessionUsername(),
                               DateTime.Now,
                               System.DateTime.Now.ConvertToString(),

                               SessionCheck.getSessionUsername(),
                               DateTime.Now,
                               System.DateTime.Now.ConvertToString(),
                               objComplyModel.status.ConvertToString(),
                               objComplyModel.InspectionRemarks.ConvertToString()


                           );
                   }

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
        public string getInspectionLevel(string designId)
        {
            string InspectionLevel = "";
            DataTable dtbl = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {

                    string cmdText = "SELECT CODE_LOC FROM NHRS_HOUSE_MODEL WHERE MODEL_ID='" + designId + "'";// there should be inspection level in house_model(design)
                    sf.Begin();
                    dtbl = sf.GetDataTable(new
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
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        InspectionLevel = dr["CODE_LOC"].ConvertToString();
                    }
                }

            }
            return InspectionLevel;
        }


        //get inspection comply list
        public DataTable GetInspectionComplyList()
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmd = "Select DISTINCT NIC.INSPECTION_DESIGN ,NIC.INSPECTION_LEVEL ," + Utils.ToggleLanguage("NHM.NAME_ENG", "NHM.NAME_LOC") + " AS DESIGN_DESCRIPTION,NIC.APPROVE FROM NHRS_INSPECTION_COMPLY NIC"
                    + " JOIN NHRS_HOUSE_MODEL NHM ON (NIC.INSPECTION_DESIGN = NHM.MODEL_ID  )";
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmd,
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


        //Get inspection comply item by id getInspectionComplyById
        public InspectionComplyModelClass getInspectionComplyById(string designId)
        {
            InspectionComplyModelClass objComplyModel = new InspectionComplyModelClass();
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmdText = "SELECT * FROM NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN='" + designId + "' ";

                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new { }
                    });

                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null && sf.Transaction.Connection != null)
                    {
                        sf.End();
                    }
                }
              
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<InspectionComplyModelClass> objComplyList = new List<InspectionComplyModelClass>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        InspectionComplyModelClass objComplyModel2 = new InspectionComplyModelClass();
                        objComplyModel2.InspectionComplyCode = dr["COMPLY_CD"].ConvertToString();
                        objComplyModel2.InspectionDesign = dr["INSPECTION_DESIGN"].ConvertToString();
                        objComplyModel2.InspectionLevel = dr["INSPECTION_LEVEL"].ConvertToString();
                        objComplyModel2.InspectionElementId = dr["INSPECTION_ELEMENT_ID"].ConvertToString();
                        objComplyModel2.InspectionComplyFlag = dr["COMPLY_FLAG"].ConvertToString();
                        objComplyList.Add(objComplyModel2);
                    }
                    objComplyModel.objComplyModelList = objComplyList;
                }
            }
            return objComplyModel;
        }

        //delete inspection comply llist
        public bool DeleteComplyListByDesign(string designId)
        {
            DataTable dt = new DataTable();
            bool result = false;
            QueryResult qr = null;
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    string cmdText = "DELETE FROM NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN=" + designId + " ";

                    sf.Begin();
                    qr = sf.SubmitChanges(cmdText, null);

                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qr != null)
                    {
                        result = qr.IsSuccess;
                    }
                }

                return result;
            }
        }

        //approve inspection Comply
        public bool ApproveInspectionComply(InspectionComplyModelClass objComplyModel)
        {
            bool result = false;
            DataTable dt = new DataTable();
            QueryResult qr = null;
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {

                    string cmdText = "UPDATE  NHRS_INSPECTION_COMPLY SET APPROVE='" + objComplyModel.status + "' WHERE INSPECTION_DESIGN=" + objComplyModel.InspectionDesign + "";
                    sf.Begin();
                     qr= sf.SubmitChanges(cmdText, null);




                }


                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qr != null)
                    {
                        result = qr.IsSuccess;
                    }
                }

                return result;
            }
        }

        // Check duplicate design for comply 
        public bool checkDUplicateDesignIncomply(string iD)
        {
            bool designId = false;
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    //string cmdText = "SELECT INSPECTION_DESIGN FROM NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN='"+iD+"'";
                    string cmdText = "SELECT INSPECTION_DESIGN FROM NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN='" + iD + "'";
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new { }
                    });

                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null && sf.Transaction.Connection != null)
                    {
                        sf.End();
                    }
                }
                if(dt!=null && dt.Rows.Count>0)
                {
                    designId=true;
                }
            }
            return designId;
        }


        //getImageName
        public string getImageNameFromDesign(string designId)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            string ImageName = "";


            try
            {
                cmd = "SELECT IMAGE_NAME from NHRS_HOUSE_MODEL WHERE MODEL_ID = " + designId + " ";
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
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                ImageName = dtbl.Rows[0][0].ConvertToString();
            }
            return ImageName;

        }


    }
}
