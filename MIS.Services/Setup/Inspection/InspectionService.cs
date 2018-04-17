using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Core;
using System.Data;
using EntityFramework;
using MIS.Services.Core;
using MIS.Models.Setup;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Setup.Inspection;

namespace MIS.Services.Setup.Inspection
{
    public class InspectionService
    {
        /// <summary>
        /// Get Inspection Tree Items
        /// </summary>
        /// <returns>TreeItems List</returns>
        public List<TreeItems> GetInspectionHierarchy(string SearchText)
        {
            #region Database Data Fetch
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
               
                string cmdText = "";

                cmdText = "select INSPECTION_CODE_ID CD,UPPER_INSPECTION_CODE_ID UpperCd, DESC_ENG,ORDER_NO,DESC_LOC, SHORT_NAME	,SHORT_NAME_LOC	,VALUE_TYPE   from NHRS_INSPECTION_DESC_DTL  where DISABLED='N'"
                         + "START with (UPPER_INSPECTION_CODE_ID is null OR UPPER_INSPECTION_CODE_ID=NULL)" +
                         "CONNECT by prior INSPECTION_CODE_ID=UPPER_INSPECTION_CODE_ID ORDER BY ORDER_NO";
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

            #endregion
            var treestr = new BuildTreeStructure().GetTreeHierarch(dt, SearchText);
            List<TreeItems> treeItem = new List<TreeItems>(); //custKeyVal.Value;
            treeItem.Add(new TreeItems()
            {
                Children = treestr.Children,
                Cd = (""),
                UpperCd = (null),
                Label = Utils.GetLabel("Inspection"),
                Order = (0)
            });

            return treeItem;
        }

        /// <summary>
        /// Get detail for the selected Inspection
        /// </summary>
        /// <param name="PositionCd"></param>
        /// <returns>datatable</returns>
        public DataTable GetInspectionDetail(string RCd)
        {
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    cmdText = "select * from NHRS_INSPECTION_DESC_DTL T1";
                    if (RCd != null)
                    {
                        cmdText = cmdText + " WHERE T1.INSPECTION_CODE_ID=" + RCd;

                    }
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
                dtbl = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }

        /// <summary>
        /// Function to Insert/Update/Delete Inspection
        /// </summary>
        /// <param name="objMenu"></param>
        /// <param name="mode"></param>
        /// <returns>Bool</returns>
        public bool InspectionUID(MISInspection objRep, string mode)
        {

            QueryResult qResult = null;
            MisInspectionInfo objRepEntity = new MisInspectionInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_NHRS_INSPECTION";
                objRepEntity.InspectionCd = objRep.InspectionCd.ToDecimal();
                objRepEntity.DefinedCd = objRep.DefinedCD;
                objRepEntity.UpperInspectionCd = objRep.upperInspectionCd.ToDecimal();
                objRepEntity.DescEng = objRep.descEng;
                objRepEntity.DescLoc = objRep.descLoc;
                objRepEntity.Shortname = objRep.Shortname;
                objRepEntity.ShortnameLoc = objRep.ShortnameLoc;
                objRepEntity.ValueType = objRep.ValueType;
                objRepEntity.OrderNo = objRep.orderNo;
                objRepEntity.Disabled = objRep.disabled;
                objRepEntity.Approved = objRep.Approved;
                objRepEntity.ApprovedBy = SessionCheck.getSessionUsername();
                objRepEntity.ApprovedDt = DateTime.Now.ToString("dd-MMM-yyyy");
                objRepEntity.ApprovedDtLoc = objRep.ApprovedDtLoc;
                objRepEntity.EnteredBy = SessionCheck.getSessionUsername();//objRep.enteredBy;
                objRepEntity.EnteredDt = DateTime.Now.ToString("dd-MMM-yyyy");
                objRepEntity.EnteredDtLoc = objRep.enteredLoc;
                objRepEntity.GroupFlag = objRep.groupFlag;
                objRepEntity.Ipaddress = CommonVariables.IPAddress;
                objRepEntity.Mode = mode;
                objRepEntity.HouseModel = objRep.houseModelId;  //adding house model values 
                objRepEntity.HierarchyCd = objRep.houseModelId;
                
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objRepEntity, true);
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return qResult.IsSuccess;

        }

        //Check Duplicate
        public bool CheckDuplicateDefinedCode(string DefinedCode, string InspectionCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                
                if (InspectionCode == string.Empty)
                {
                    cmdText = "select * from NHRS_INSPECTION_DESC_DTL where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from NHRS_INSPECTION_DESC_DTL where DEFINED_CD ='" + DefinedCode + "' and INSPECTION_CODE_ID !='" + InspectionCode + "'";
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
                    return true;
                }
                return false;
            }
        }
        public DataTable DamageGradeName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_DAMAGE_GRADE WHERE 1=1";
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
        public DataTable TechnicalSolutionName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_TECHNICAL_SOLUTION WHERE 1=1";
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

        public DataTable getAllModel()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS_HOUSE_MODEL WHERE 1=1";
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
    }
}
