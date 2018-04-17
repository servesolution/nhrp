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

namespace MIS.Services.Setup
{
    public class MaterialService
    {
        /// <summary>
        /// Get Material Tree Items
        /// </summary>
        /// <returns>TreeItems List</returns>
        CommonFunction commonFC = new CommonFunction();
        public List<TreeItems> GetMaterialHierarchy(string SearchText)
        {
            #region Database Data Fetch
            DataTable dt = new DataTable();
            //DataTable dt = null;
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select Material_CD CD, UPPER_Material_CD UpperCd, Desc_ENG,DESC_LOC, defined_cd , ORDER_NO from MIS_MATERIAL " +
                         " START with (UPPER_Material_CD is null OR UPPER_Material_CD='0')   " +
                         " CONNECT by prior Material_CD=UPPER_Material_CD";
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
                Label = Utils.GetLabel("Material"),
                Order = (0)

            });

            return treeItem;
        }

        /// <summary>
        /// Get detail for the selected material
        /// </summary>
        /// <param name="MaterialCd"></param>
        /// <returns>datatable</returns>
        public DataTable GetMaterialDetail(string MaterialCd)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select *  from MIS_Material T1";
                if (MaterialCd.ConvertToString() != "")
                {
                    cmdText = cmdText + " WHERE T1.Material_CD=" + Convert.ToDecimal(MaterialCd);
                }
                try
                {
                    service.Begin();
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
            return dtbl;
        }

        /// <summary>
        /// Function to Insert/Update/Delete Material
        /// </summary>
        /// <param name="objMenu"></param>
        /// <param name="mode"></param>
        /// <returns>Bool</returns>
        public bool MaterialUID(MISMaterial objMaterial, string mode, out string exc)
        {
            QueryResult qResult = null;
            bool res = false;
            MisMaterialInfo objMaterialEntity = new MisMaterialInfo();
            exc = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";
                objMaterialEntity.MaterialCd = objMaterial.MaterialCd;
                if (mode=="I" || mode=="U")
                {
                    objMaterialEntity.DefinedCd = objMaterial.DefinedCd;
                    objMaterialEntity.DescEng = objMaterial.DescEng;
                    objMaterialEntity.DescLoc = objMaterial.DescLoc;
                    objMaterialEntity.ShortName = objMaterial.ShortName;
                    objMaterialEntity.ShortNameLoc = objMaterial.ShortNameLoc;
                    //objMaterialEntity.UpperMaterialCd = commonFC.GetCodeFromDataBase(objMaterial.UpperMaterialCd, "MIS_MATERIAL", "MATERIAL_CD");
                    //if (objMaterialEntity.UpperMaterialCd == "")
                    //{
                        //objMaterialEntity.UpperMaterialCd = null;
                    //}
                    objMaterialEntity.OrderNo = objMaterial.OrderNo;
                    //objMaterialEntity.Disabled = objMaterial.Disabled;
                    objMaterialEntity.EnteredBy = SessionCheck.getSessionUsername();//objMaterial.EnteredBy;
                    //objMaterialEntity.EnteredDt = objMaterial.EnteredDt.ToString("dd-MM-yyyy");
                    objMaterialEntity.EnteredDtLoc = objMaterial.EnteredDtLoc;
                    objMaterialEntity.GroupFlag = objMaterial.GroupFlag;
                    objMaterialEntity.IPAddress = CommonVariables.IPAddress;   
                }
                if (mode == "I" || mode == "U" || mode == "A")
                {
                    //objMaterialEntity.Approved = objMaterial.Approved;
                    objMaterialEntity.ApprovedBy = objMaterial.ApprovedBy;
                    //objMaterialEntity.ApprovedDt = objMaterial.ApprovedDt.ToString("dd-MM-yyyy");
                    objMaterialEntity.ApprovedDtLoc = objMaterial.ApprovedDtLoc;
                }
                objMaterialEntity.Mode = mode;
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objMaterialEntity, true);
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

        public bool CheckPresenceOfChildren(Decimal code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select * from MIS_Material where UPPER_Material_CD ='" + code + "'";
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

        /// <summary>
        /// Funtion to check the unique Defined Code
        /// </summary>
        /// <param name="DefinedCd"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckDuplicateDefinedCode(string DefinedCd, string code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                if (code == string.Empty)
                {
                    cmdText = "select * from MIS_MATERIAL where DEFINED_CD ='" + DefinedCd + "'";
                }
                else
                {
                    cmdText = "select * from MIS_MATERIAL where DEFINED_CD ='" + DefinedCd + "' and MATERIAL_CD !='" + code + "'";
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
                if (dtbl.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }

        public string GetUpperCode(string childCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select UPPER_MATERIAL_CD from MIS_MATERIAL where MATERIAL_CD ='" + childCode + "'";
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
                    return dtbl.Rows[0]["UPPER_MATERIAL_CD"].ConvertToString();
                }
                return string.Empty;
            }
        }
    }
}
