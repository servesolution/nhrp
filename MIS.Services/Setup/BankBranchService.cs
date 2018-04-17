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
    public class BankBranchService
    {
        public void insertBankBranchName(NHRSBankBranch nhrsbranch)
        {
            NhrsBankBranchInfo nhrsbankbranchinfo = new NhrsBankBranchInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    if (nhrsbranch.Mode == "I")
                    {
                        nhrsbankbranchinfo.BankBranchCd = GetMaxvalue().ToDecimal();
                    }
                    if (nhrsbranch.Mode == "U" || nhrsbranch.Mode == "D" || nhrsbranch.Mode == "A")
                    {
                        nhrsbankbranchinfo.BankBranchCd = nhrsbranch.BANK_BRANCH_CD;
                    }
                    nhrsbankbranchinfo.BankCd = nhrsbranch.BANK_CD.ToDecimal();
                    nhrsbankbranchinfo.DefinedCd = nhrsbranch.DEFINED_CD;
                    nhrsbankbranchinfo.DescEng = nhrsbranch.DESC_ENG;
                    nhrsbankbranchinfo.DescLoc = nhrsbranch.DESC_LOC;
                    nhrsbankbranchinfo.ShortName = nhrsbranch.DESC_ENG;
                    nhrsbankbranchinfo.ShortNameLoc = nhrsbranch.DESC_LOC;
                    nhrsbankbranchinfo.DistrictCd = nhrsbranch.DISTRICT_CD.ToDecimal();
                    nhrsbankbranchinfo.VdcMunCd = nhrsbranch.VDC_MUN_CD.ToDecimal();
                    nhrsbankbranchinfo.TelephoneNo = nhrsbranch.TELEPHONE_NO;
                    nhrsbankbranchinfo.FaxNo = nhrsbranch.FAX_NO;
                    nhrsbankbranchinfo.Url = nhrsbranch.URL;
                    nhrsbankbranchinfo.Email = nhrsbranch.EMAIL;
                    nhrsbankbranchinfo.AddressEng = nhrsbranch.ADDRESS_ENG;
                    nhrsbankbranchinfo.AddressLoc = nhrsbranch.ADDRESS_LOC;
                    nhrsbankbranchinfo.WardNo = nhrsbranch.WARD_NO.ToDecimal();
                    nhrsbankbranchinfo.BankType = nhrsbranch.BANK_TYPE;
                    nhrsbankbranchinfo.Remarks = nhrsbranch.REMARKS;
                    nhrsbankbranchinfo.Approved = "N";
                    nhrsbankbranchinfo.Disabled = "N";
                    nhrsbankbranchinfo.OrderNo = 0;
                    nhrsbankbranchinfo.ApprovedBy = SessionCheck.getSessionUsername();
                    nhrsbankbranchinfo.ApprovedDt = DateTime.Now.ToDateTime();
                    nhrsbankbranchinfo.EnteredBy = SessionCheck.getSessionUsername();
                    nhrsbankbranchinfo.EnteredDt = DateTime.Now.ToDateTime();
                    nhrsbankbranchinfo.Mode = nhrsbranch.Mode;
                    nhrsbankbranchinfo.IsDistHeadQtr = nhrsbranch.IS_DIST_HEADQTR;
                    nhrsbankbranchinfo.NumClaimLimit = nhrsbranch.NUM_CLAIM_LIMIT;
                    nhrsbankbranchinfo.BranchSTDcd = nhrsbranch.BRANCH_STD_CD;
                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(nhrsbankbranchinfo, true);

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
        public NHRSBankBranch FillBranchBank(string BankBranchCd)
        {
            NHRSBankBranch obj = new NHRSBankBranch();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_BANK_BRANCH where BANK_BRANCH_CD = '" + BankBranchCd + "'";
                    try
                    {
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
                    if (dt.Rows.Count > 0)
                    {
                        obj.BANK_CD = dt.Rows[0]["BANK_CD"].ConvertToString();
                        obj.BANK_BRANCH_CD = dt.Rows[0]["BANK_BRANCH_CD"].ToDecimal();
                        obj.DEFINED_CD = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.DESC_ENG = dt.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.DESC_LOC = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToString();
                        obj.VDC_MUN_CD = dt.Rows[0]["VDC_MUN_CD"].ToString();
                        obj.WARD_NO = dt.Rows[0]["WARD_NO"].ToString();
                        obj.TELEPHONE_NO = dt.Rows[0]["TELEPHONE_NO"].ConvertToString();
                        obj.FAX_NO = dt.Rows[0]["FAX_NO"].ConvertToString();
                        obj.URL = dt.Rows[0]["URL"].ConvertToString();
                        obj.EMAIL = dt.Rows[0]["EMAIL"].ConvertToString();
                        obj.ADDRESS_ENG = dt.Rows[0]["ADDRESS_ENG"].ConvertToString();
                        obj.BANK_TYPE = dt.Rows[0]["BANK_TYPE"].ConvertToString();
                        obj.IS_DIST_HEADQTR = dt.Rows[0]["IS_DIST_HEADQTR"].ConvertToString();
                        obj.BRANCH_STD_CD = dt.Rows[0]["BRANCH_STD_CD"].ConvertToString();
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
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(BANK_BRANCH_CD)),0)+1 from NHRS_BANK_BRANCH";
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
                    if (dt != null)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
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


            }

            return result;
        }
        public DataTable getallBankName(string initialStr)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select BANK_BRANCH_CD,BANK_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from NHRS_BANK_BRANCH";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(" + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + ") like '{0}%'", initialStr);
                }
                cmdText += String.Format(" order by DEFINED_CD");
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
        public DataTable getBankBranchList(NHRSBankBranch objBankBranch)
        {
            DataTable dtbl = null;
            string cmdText = "";
            string districtCd = objBankBranch.DISTRICT_CD.ConvertToString();
            string vdcCd = objBankBranch.VDC_MUN_CD.ConvertToString();
            string ward = objBankBranch.WARD_NO.ConvertToString();
            Object bankCd = objBankBranch.BANK_CD.ToDecimal();

            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select BB.BANK_BRANCH_CD,BB.BRANCH_STD_CD,BB.BANK_CD,NB.DESC_ENG as BANK_NAME_ENG,NB.DESC_LOC AS BANK_NAME_LOC,BB.DEFINED_CD,BB.DESC_ENG,BB.DESC_LOC,BB.SHORT_NAME,BB.SHORT_NAME_LOC,NB.DISTRICT_CD,MD.DESC_ENG as DISTRICT_ENG,MD.DESC_LOC AS DISTRICT_LOC,VM.VDC_MUN_CD,VM.DESC_ENG AS VDC_ENG,VM.DESC_LOC AS VDC_LOC,BB.WARD_NO,BB.APPROVED " +
                    "from NHRS_BANK_BRANCH BB " +
"INNER JOIN NHRS_BANK NB ON NB.BANK_CD=BB.BANK_CD " +
"INNER JOIN MIS_DISTRICT MD ON MD.DISTRICT_CD=BB.DISTRICT_CD " +
"INNER JOIN MIS_VDC_MUNICIPALITY VM ON VM.VDC_MUN_CD=BB.VDC_MUN_CD " +
"where 1=1";

                if (bankCd != null)
                {
                    cmdText += String.Format(" and BB.BANK_CD='" + bankCd + "'");
                }
                if (districtCd != "")
                {
                    cmdText += String.Format(" and BB.DISTRICT_CD= '" + districtCd + "'");
                }
                if (vdcCd != "")
                {
                    cmdText += String.Format(" and BB.VDC_MUN_CD='" + vdcCd + "'");
                }
                if (ward != "")
                {
                    cmdText += String.Format(" and BB.WARD_NO='" + ward + "'");
                }
                cmdText += String.Format(" order by BB.DEFINED_CD");
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

        public bool CheckDuplicateBranchID(string code)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select * from NHRS_BANK_BRANCH  where DEFINED_CD ='" + code + "'";


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

        public bool CheckDuplicateBranchstdID(string code,string bankcd)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select * from NHRS_BANK_BRANCH  where BRANCH_STD_CD ='" + code + "' and BANK_CD='" + bankcd + "'";


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
    }
}
