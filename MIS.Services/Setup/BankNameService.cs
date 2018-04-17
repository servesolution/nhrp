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
    public class BankNameService
    {
        public void insertBankName(NHRPBankName nhrpbank)
        {
            NhrsBankInfo nhrpbankinfo = new NhrsBankInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    nhrpbankinfo.BankCd = nhrpbank.BANK_CD.ToDecimal();
                    nhrpbankinfo.DefinedCd = nhrpbank.DEFINED_CD;
                    nhrpbankinfo.DescEng = nhrpbank.DESC_ENG;
                    nhrpbankinfo.DescLoc = nhrpbank.DESC_LOC;
                    nhrpbankinfo.ShortName = nhrpbank.DESC_ENG;
                    nhrpbankinfo.ShortNameLoc = nhrpbank.DESC_LOC;
                    nhrpbankinfo.DistrictCd = nhrpbank.DISTRICT_CD;
                    nhrpbankinfo.VdcMunCd = nhrpbank.VDC_MUN_CD.ToDecimal();
                    nhrpbankinfo.TelephoneNo = nhrpbank.TELEPHONE_NO;
                    nhrpbankinfo.FaxNo = nhrpbank.FAX_NO;
                    nhrpbankinfo.Url = nhrpbank.URL;
                    nhrpbankinfo.Email = nhrpbank.EMAIL;
                    nhrpbankinfo.AddressEng = nhrpbank.ADDRESS_ENG;
                    nhrpbankinfo.AddressLoc = nhrpbank.ADDRESS_LOC;
                    nhrpbankinfo.WardNo = nhrpbank.WARD_NO.ToDecimal();
                    nhrpbankinfo.BankType = nhrpbank.BANK_TYPE;
                    nhrpbankinfo.Remarks = nhrpbank.REMARKS;
                    //nhrpbankinfo.Approved = "N";
                    nhrpbankinfo.Approved = nhrpbank.APPROVED;

                    nhrpbankinfo.Disabled = "N";
                    nhrpbankinfo.OrderNo = 0;
                    nhrpbankinfo.ApprovedBy = SessionCheck.getSessionUsername();
                    nhrpbankinfo.ApprovedDt = DateTime.Now.ToDateTime();
                    nhrpbankinfo.EnteredBy = SessionCheck.getSessionUsername();
                    nhrpbankinfo.EnteredDt = DateTime.Now.ToDateTime();
                    nhrpbankinfo.Mode = nhrpbank.MODE;
                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(nhrpbankinfo, true);

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
        public NHRPBankName FillBank(string BankCd)
        {
            NHRPBankName obj = new NHRPBankName();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_BANK where BANK_CD = '" + BankCd + "'";
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
                        obj.DEFINED_CD =dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.DESC_ENG = dt.Rows[0]["DESC_ENG"].ConvertToString();
                        obj.DESC_LOC = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToDecimal();
                        obj.VDC_MUN_CD = dt.Rows[0]["VDC_MUN_CD"].ToString();
                        obj.WARD_NO = dt.Rows[0]["WARD_NO"].ToDecimal();
                        obj.TELEPHONE_NO = dt.Rows[0]["TELEPHONE_NO"].ConvertToString();
                        obj.FAX_NO = dt.Rows[0]["FAX_NO"].ConvertToString();
                        obj.URL = dt.Rows[0]["URL"].ConvertToString();
                        obj.EMAIL = dt.Rows[0]["EMAIL"].ConvertToString();
                        obj.ADDRESS_ENG = dt.Rows[0]["ADDRESS_ENG"].ConvertToString();
                        obj.ADDRESS_LOC = dt.Rows[0]["ADDRESS_LOC"].ConvertToString();
                        obj.BANK_TYPE = dt.Rows[0]["BANK_TYPE"].ConvertToString();
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

        public bool CheckDuplicateDefinedCode(string DefinedCode, string BankCode)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                if (BankCode == string.Empty)
                {
                    cmdText = "select * from NHRS_BANK where DEFINED_CD ='" + DefinedCode + "'";
                }
                else
                {
                    cmdText = "select * from NHRS_BANK where DEFINED_CD ='" + DefinedCode + "' and BANK_CD !='" + BankCode + "'";
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
        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(BANK_CD)),0)+1 from NHRS_BANK";
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
                cmdText = "select BANK_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from NHRS_BANK";
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
        public DataTable getallBankNameList(NHRPBankName objBankName)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select BANK_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from NHRS_BANK where  1=1";
                if (objBankName.BANK_CD != null)
                {
                    cmdText += String.Format(" and BANK_CD='"+objBankName.BANK_CD+"'");
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
    }
}
