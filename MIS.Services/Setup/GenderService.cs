using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Setup;
using System.Data;
using System.Web;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Setup
{
    public class GenderService
    {
        #region  Insert, Update and Delete
        //Add Gender 
        public void AddGender(MISGender Gender)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                MisGenderInfo obj = new MisGenderInfo();
                service.PackageName = "PKG_SETUP";
                obj.GenderCd = Gender.GenderCD;
                obj.DefinedCd = Utils.ConvertToString(Gender.DefinedCD);
                obj.DescEng =Utils.ConvertToString( Gender.DescEng.ToUpper());
                obj.DescLoc = Utils.ConvertToString(Gender.DescLoc);
                obj.ShortName = Utils.ConvertToString(Gender.ShortName.ToUpper());
                obj.ShortNameLoc = Utils.ConvertToString(Gender.ShortNameLoc);
                obj.Approved = Gender.Approved;
                obj.Disabled = Gender.Disabled;
                obj.ApprovedBy = Gender.ApprovedBy;
                obj.ApprovedDt = Gender.ApprovedDt;
                obj.ApprovedDtLoc = Gender.ApproveDtLoc;
                obj.EnteredBy = SessionCheck.getSessionUsername();
                obj.EnteredDt = Gender.EnteredDt;
                obj.EnteredDtLoc = Gender.EnteredDtLoc;
                obj.Ipaddress = CommonVariables.IPAddress;
                obj.Mode = "I";            
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
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

        // Update Gender
        public void UpdateGender(MISGender Gender)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                MisGenderInfo obj = new MisGenderInfo();
                service.PackageName = "PKG_SETUP";
                obj.GenderCd = Gender.GenderCD;
                obj.DefinedCd = Gender.DefinedCD;
                obj.DescEng = Gender.DescEng.ConvertToString();
                obj.DescLoc = Gender.DescLoc;
                obj.ShortName = Gender.ShortName.ConvertToString();
                obj.ShortNameLoc = Gender.ShortNameLoc;
                obj.Approved = Gender.Approved;
                obj.Disabled = Gender.Disabled;
                obj.ApprovedBy = SessionCheck.getSessionUsername();
                obj.ApprovedDt = Gender.ApprovedDt;
                obj.ApprovedDtLoc = Gender.ApproveDtLoc;
                obj.EnteredBy = SessionCheck.getSessionUsername();
                obj.EnteredDt = Gender.EnteredDt;
                obj.EnteredDtLoc = Gender.EnteredDtLoc;
                obj.Ipaddress = null;
                obj.Mode = "U";                
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
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

        // Delete Gender
        public bool DeleteGender(MISGender gender,out string exc)
        {
            QueryResult qr = null;
            exc = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                MisGenderInfo obj = new MisGenderInfo();
                obj.GenderCd = gender.GenderCD;
                obj.DefinedCd = null;
                obj.DescEng = null;
                obj.DescLoc = null;
                obj.ShortName = null;
                obj.ShortNameLoc = null;
                obj.Approved = false;
                obj.Disabled = false;
                obj.ApprovedBy = null;
                obj.ApprovedDt = DateTime.Now;
                obj.ApprovedDtLoc = null;
                obj.EnteredBy = SessionCheck.getSessionUsername();
                obj.EnteredDt = DateTime.Now;
                obj.EnteredDtLoc = null;
                obj.Ipaddress = null;

                //Delete the group
                service.PackageName = "PKG_SETUP";
                
                obj.Mode = "D";
                try
                {
                    service.Begin();
                     qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    res = false;
                    service.RollBack();
                    ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }
        }
        #endregion

        #region Select Operation
        // Fill Gender For Edit
        public MISGender FillGender(string genderCd)
        {
            MISGender obj = new MISGender();            
            DataTable dt = null;            
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    
                    string CmdText = "select GENDER_CD,DEFINED_CD, DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC from MIS_GENDER where GENDER_CD = '" + genderCd + "'";
                    try
                    {
                        service.Begin();
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
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
                        service.End();
                    }
                    
                    if (dt.Rows.Count > 0)
                    {
                        obj.GenderCD = Convert.ToInt32(dt.Rows[0]["GENDER_CD"]);
                        obj.DefinedCD = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.DescEng = dt.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.DescLoc = dt.Rows[0]["DESC_LOC"].ConvertToString();
                        obj.ShortName = dt.Rows[0]["SHORT_NAME"].ConvertToString();
                        obj.ShortNameLoc = dt.Rows[0]["SHORT_NAME_LOC"].ConvertToString();
                    }
                }
                catch (Exception)
                {
                    obj = null;
                }
            }
            return obj;

        }

        // Change Gender Status
        public void ChangeStatus(MISGender gender)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                MisGenderInfo obj = new MisGenderInfo();
                service.PackageName = "PKG_SETUP";
                obj.GenderCd = gender.GenderCD;
                obj.Approved = gender.Approved;
                obj.ApprovedBy = gender.ApprovedBy;
                obj.EnteredBy = gender.EnteredBy;
                obj.Mode = "A";
                service.Begin();
                try
                {
                    QueryResult qr = service.SubmitChanges(obj, true);
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

        // Fetch The Maximum Code Value
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;        
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select nvl(max(to_number(GENDER_CD)),0)+1 from MIS_GENDER";
                    try
                    {
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
                    if (dt != null)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result = "";
                ExceptionManager.AppendLog(ex);
            }
            return result;

        }

        // Get All Table Criteria
        public DataTable Gender_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                cmdText = "select GENDER_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORT_NAME,APPROVED from MIS_GENDER";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {                    
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "DEFINED_CD")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
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
            return dtbl;
        }

        //Check Duplicate
        public bool CheckDuplicateDefinedCode(string DefinedCode, string GenderCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (GenderCode == string.Empty)
                {
                    cmdText = "select * from MIS_GENDER where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from MIS_GENDER where DEFINED_CD ='" + DefinedCode + "' and GENDER_CD !='" + GenderCode + "'";
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
    }
}