using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using MIS.Models.NHRP.Setup;
using EntityFramework;
using ExceptionHandler;
using System.Data.OracleClient;
using System.Data;

namespace MIS.Services.Setup
{
    public class BankAcctypService
    {
        public void SaveBankAccType(BankAccTyp bnkacty)
        {
            BnkAccTyp banktypett = new BnkAccTyp();
            using (ServiceFactory service=new ServiceFactory())
            {
                try
                {
                    banktypett.bank_acc_type_cd = bnkacty.Bank_Acc_Typ_cd.ToDecimal();
                    banktypett.defined_cd = bnkacty.Defined_cd;
                    banktypett.desc_eng = bnkacty.Desc_Eng;
                    banktypett.desc_loc = bnkacty.Desc_Loc;
                    banktypett.short_name = bnkacty.Short_Name;
                    banktypett.short_name_loc = bnkacty.Short_Name_Loc;
                    //banktypett.approved = "Y";
                    banktypett.approved = bnkacty.Approved;
                    banktypett.approved_by = bnkacty.Approved_by;
                    banktypett.approved_dt = DateTime.Now.ToDateTime();
                    banktypett.approved_dt_loc = bnkacty.Approved_dt_loc;
                    banktypett.Mode = bnkacty.Mode;
                    banktypett.disabled = "N";
                    banktypett.entered_by = SessionCheck.getSessionUsername();
                    banktypett.entered_dt = DateTime.Now.ToDateTime();
                    banktypett.entered_dt_loc = bnkacty.Entered_dt_loc;
                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(banktypett, true);



                }
                catch(OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch(Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if(service.Transaction!=null)
                    {
                        service.End();
                    }
                }
            }
        }

        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select nvl(max(to_number(BANK_ACC_TYPE_CD)),0)+1 from NHRS_BANK_ACC_TYPE";
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
        public DataTable BankTAccType_GetAllCriteriaToTable(string initialStr)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select BANK_ACC_TYPE_CD,DEFINED_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESCENG," + Utils.ToggleLanguage("SHORT_NAME", "SHORT_NAME_LOC") + " as SHORTNAME,APPROVED from NHRS_BANK_ACC_TYPE";
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

        public BankAccTyp FillBankAccountType(string bankacctypCd)
        {
            BankAccTyp obj = new BankAccTyp();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select BANK_ACC_TYPE_CD,DEFINED_CD,DESC_ENG,DESC_LOC,SHORT_NAME,SHORT_NAME_LOC from NHRS_BANK_ACC_TYPE where BANK_ACC_TYPE_CD = '" + bankacctypCd + "'";
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
                        obj.Bank_Acc_Typ_cd = Convert.ToInt32(dt.Rows[0]["BANK_ACC_TYPE_CD"]);
                        obj.Defined_cd = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                        obj.Desc_Eng = dt.Rows[0]["DESC_ENG"].ToString();
                        obj.Desc_Loc = dt.Rows[0]["DESC_LOC"].ToString();
                        obj.Short_Name = dt.Rows[0]["SHORT_NAME"].ToString();
                        obj.Short_Name_Loc = dt.Rows[0]["SHORT_NAME_LOC"].ToString();
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

        public bool ManageBankAccTyp(BankAccTyp bnkactyp)
        {
            BnkAccTyp bnkinfo = new BnkAccTyp();
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                service.PackageName = "PKG_NHRS_SETUP";
                bnkinfo.bank_acc_type_cd = bnkactyp.Bank_Acc_Typ_cd;
                bnkinfo.defined_cd = bnkactyp.Defined_cd.ConvertToString();
                bnkinfo.Mode = bnkactyp.Mode;
                if (bnkactyp.Mode == "I" || bnkactyp.Mode == "U")
                {
                    bnkinfo.desc_eng = Utils.ConvertToString(bnkactyp.Desc_Eng);
                    bnkinfo.desc_loc = Utils.ConvertToString(bnkactyp.Desc_Loc);
                    bnkinfo.defined_cd = Utils.ConvertToString(bnkactyp.Defined_cd);
                    bnkinfo.short_name = Utils.ConvertToString(bnkactyp.Short_Name);
                    bnkinfo.short_name_loc = Utils.ConvertToString(bnkactyp.Short_Name_Loc);
                    bnkinfo.order_no = bnkactyp.Order_no;
                    bnkinfo.disabled = bnkactyp.Disabled;
                }
                bnkinfo.approved = bnkactyp.Approved;
                bnkinfo.approved_by = bnkactyp.Approved_by;
                bnkinfo.approved_dt = bnkactyp.Approved_dt.ToDateTime();
                bnkinfo.entered_by= SessionCheck.getSessionUsername();
                bnkinfo.entered_dt = bnkactyp.Entered_dt.ToDateTime();
                bnkinfo.Ip_Address = CommonVariables.IPAddress;
                bnkinfo.Mode = bnkactyp.Mode;
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(bnkinfo, true);
                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
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

        public DataTable getBnkacTyp()
        {
            DataTable dt = null;
            string cmdText = "";
            using(ServiceFactory service=new ServiceFactory())
            {
                cmdText = "Select DEFINED_CD,DESC_ENG,DESC_LOC from NHRS_BANK_ACC_TYPE";
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
                catch(Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if(service.Transaction!=null)
                    {
                        service.End();
                    }
                }
                return dt;
            }
        }
    }
}
