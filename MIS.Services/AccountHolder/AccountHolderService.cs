using EntityFramework;
using ExceptionHandler;
using MIS.Models.AccountHolder;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MIS.Services.AccountHolder
{
   public class AccountHolderService
    {
       public DataTable AccountHolderSearchList(AccountHolderSearch objaccountsearch)
        {
            DataTable dt = new DataTable();
            Object bankcd = DBNull.Value;
            Object bankbranchcd = DBNull.Value;
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            //Object from = DBNull.Value;
            //Object to = DBNull.Value;
            Object outresults = DBNull.Value;
            if (objaccountsearch.bankcd.ConvertToString() != "")
            {
                bankcd = objaccountsearch.bankcd;
            }
            if (objaccountsearch.bankbranchcd.ConvertToString() != "")
            {
                bankbranchcd = objaccountsearch.bankbranchcd;
            }
            if (objaccountsearch.districtcd.ConvertToString() != "")
            {
                District = objaccountsearch.districtcd;
            }
            if (objaccountsearch.vdcmuncd.ConvertToString() != "")
            {
                Vdc = objaccountsearch.vdcmuncd;
            }
            if (objaccountsearch.wardno.ConvertToString() != "")
            {
                Ward = objaccountsearch.wardno;
            }
            //if (objaccountsearch.BFROM.ConvertToString() != "")
            //{
            //    from = objaccountsearch.BFROM;
            //}
            //if (objaccountsearch.BTO.ConvertToString() != "")
            //{
            //    to = objaccountsearch.BTO;
            //}
            
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_BANK";
                service.Begin();
                dt = service.GetDataTable(true, "PR_BANK_ENROLLMENT_SEARCH",
                        bankcd,
                        bankbranchcd,
                        District,
                        Vdc,
                        Ward,
                        //from,
                        //to,
                        DBNull.Value
                    );


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
            return dt;

        }
    }
}
