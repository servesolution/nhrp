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
    public class BankMappingService
    {
        public void SaveMappedBank(NHRSBankMapping nhrpbankmapping)
        {
            NhRPBankMappingInfo nhrpbankmappinginfo = new NhRPBankMappingInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    nhrpbankmappinginfo.MAPPING_ID = nhrpbankmapping.MAPPING_ID.ToDecimal();
                    nhrpbankmappinginfo.BANK_CD = nhrpbankmapping.BANK_CD.ToDecimal();
                    nhrpbankmappinginfo.BANK_BRANCH_CD = nhrpbankmapping.BANK_BRANCH_CD.ToDecimal();
                    nhrpbankmappinginfo.DISTRICT_CD = nhrpbankmapping.DISTRICT_CD.ToDecimal();
                    nhrpbankmappinginfo.VDC_MUN_CD = nhrpbankmapping.VDC_MUN_CD.ToDecimal();
                    nhrpbankmappinginfo.WARD_NO = nhrpbankmapping.WARD_NO.ToDecimal();
                    nhrpbankmappinginfo.BFROM = nhrpbankmapping.BFROM.ToDecimal();
                    nhrpbankmappinginfo.BTO = nhrpbankmapping.BTO.ToDecimal();
                    nhrpbankmappinginfo.IS_ACTIVE = "Y";
                    nhrpbankmappinginfo.ENTERED_BY = SessionCheck.getSessionUsername();
                    nhrpbankmappinginfo.ENTERED_DT = DateTime.Now.ToDateTime();
                    nhrpbankmappinginfo.ENTERED_DT_LOC = DateTime.Now.ConvertToString();
                    nhrpbankmappinginfo.Mode = nhrpbankmapping.MODE;
                    nhrpbankmappinginfo.IP_ADDRESS = CommonVariables.IPAddress;
                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(nhrpbankmappinginfo, true);

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

        public DataTable GetBeneficiaries(NHRSBankMapping bankMap)
        {
            DataTable dt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object pa_number = DBNull.Value;
            Object IS_BANK_MAPPED = DBNull.Value;
            Object Beneficairy_Type = DBNull.Value;
            Object p_STATUS = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if (bankMap.DISTRICT_CD.ConvertToString() != "")
            {
                District = bankMap.DISTRICT_CD.ToInt32(); 
            }
            if (bankMap.VDC_MUN_CD.ConvertToString() != "")
            {
                Vdc = bankMap.VDC_MUN_CD.ToInt32();
            }
            if (bankMap.WARD_NO.ConvertToString() != "")
            {
                Ward = bankMap.WARD_NO.ToInt32();
            }
            if (bankMap.PA_NUMBER.ConvertToString() != "")
            {
                pa_number = bankMap.PA_NUMBER.ToString();
            }
            if (bankMap.IS_BANK_MAPPED.ConvertToString() != "")
            {
                IS_BANK_MAPPED = bankMap.IS_BANK_MAPPED.ToString();
            }
            if (bankMap.BeneficiaryType.ConvertToString() != ""  )
            {
                Beneficairy_Type = bankMap.BeneficiaryType.ToString();
            }
            if (bankMap.Status.ConvertToString() != "")
            {
                p_STATUS = bankMap.Status.ToString();
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "Pr_PA_by_location",
                        District,
                        Vdc,
                        Ward,
                        pa_number,
                        IS_BANK_MAPPED,
                        Beneficairy_Type,
                        P_OUT_RESULT
                    );
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
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

        public DataTable GetDonorBeneficiaries(NHRSBankMapping bankMap)
        {
            DataTable dt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object pa_number = DBNull.Value;
            Object IS_DONOR_MAPPED = DBNull.Value;
            Object Beneficairy_Type = DBNull.Value;
            Object p_STATUS = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if (bankMap.DISTRICT_CD.ConvertToString() != "")
            {
                District = bankMap.DISTRICT_CD.ToInt32();
            }
            if (bankMap.VDC_MUN_CD.ConvertToString() != "")
            {
                Vdc = bankMap.VDC_MUN_CD.ToInt32();
            }
            if (bankMap.WARD_NO.ConvertToString() != "")
            {
                Ward = bankMap.WARD_NO.ToInt32();
            }
            if (bankMap.PA_NUMBER.ConvertToString() != "")
            {
                pa_number = bankMap.PA_NUMBER.ToString();
            }
            if (bankMap.IS_DONOR_MAPPED.ConvertToString() != "")
            {
                IS_DONOR_MAPPED = bankMap.IS_DONOR_MAPPED.ToString();
            }
            if (bankMap.BeneficiaryType.ConvertToString() != "")
            {
                Beneficairy_Type = bankMap.BeneficiaryType.ToString();
            }
            if (bankMap.Status.ConvertToString() != "")
            {
                p_STATUS = bankMap.Status.ToString();
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_DONOR_MAPPING",
                        District,
                        Vdc,
                        Ward,
                        pa_number,
                        IS_DONOR_MAPPED,
                        Beneficairy_Type,
                        P_OUT_RESULT
                    );
            }
               

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
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

        public DataTable GetMappedBeneficiaries(NHRSBankMapping bankMap)
        {
            DataTable dt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object pa_number = DBNull.Value;
            Object bank_cd = DBNull.Value;
            Object branch_cd = DBNull.Value;
            Object benef_type = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;


          
            if (bankMap.DISTRICT_CD != null)
            {
                District = bankMap.DISTRICT_CD.ToInt32();
            }
            if (bankMap.VDC_MUN_CD != null)
            {
                Vdc = bankMap.VDC_MUN_CD.ToInt32();

            }
            if (bankMap.WARD_NO != null)
            {
                Ward = bankMap.WARD_NO.ToInt32();
            }
            if (bankMap.PA_NUMBER != null)
            {
                pa_number = bankMap.PA_NUMBER.ToString();
            }
            if (bankMap.BANK_CD != null)
            {
                bank_cd = bankMap.BANK_CD.ToString();
            }
            if (bankMap.BANK_BRANCH_CD != null)
            {
                branch_cd = bankMap.BANK_BRANCH_CD.ToString();
            }
            if (bankMap.BeneficiaryType != null)
            {
                benef_type = bankMap.BeneficiaryType.ToString();
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "Pr_PA_by_location",
                        District,
                        Vdc,
                        Ward,
                        pa_number,
                        bank_cd,
                        branch_cd,
                        benef_type,
                        P_OUT_RESULT
                    );
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
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

        public QueryResult MapBankData(List<NHRSBankMapping> bankMap)
        {
            QueryResult qr = null;
            CommonFunction commonFC = new CommonFunction();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object pa_number = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;
            DateTime Entered_Dt = DateTime.Now;
            string Entered_Dt_LOC = NepaliDate.getNepaliDate(Entered_Dt.ConvertToString());
            string entered_by = CommonVariables.UserName.ToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                
                foreach (var item in bankMap)
                {
                    qr = service.SubmitChanges("PR_MAP_BANK_DATA",
                                                    item.PA_NUMBER,
                                                    item.BANK_CD,
                                                    item.BANK_BRANCH_CD,
                                                    entered_by, // entered by
                                                    Entered_Dt, // entered date
                                                    Entered_Dt_LOC
                                                    );
                }

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                qr = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return qr;
        }
        public QueryResult DeleteMappedPO(string PA_NUMBER)
        {
            QueryResult qr = null;
            CommonFunction commonFC = new CommonFunction();
            Object PA = PA_NUMBER;
            string query = "DELETE FROM NHRS_DONOR_MAPPING_DTL WHERE NRA_DEFINED_CD = '" + PA + "' ";
                
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                qr = service.SubmitChanges(query, null);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                qr = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return qr;
        }

        public QueryResult MapDonorData(List<NHRSBankMapping> bankMap)
        {
            QueryResult qr = null;
            CommonFunction commonFC = new CommonFunction();
           
            Object pa_number = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;
            DateTime Entered_Dt = DateTime.Now;
            string Entered_Dt_LOC = NepaliDate.getNepaliDate(Entered_Dt.ConvertToString());
            string entered_by = CommonVariables.UserName.ToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();

                foreach (var item in bankMap)
                {
                    qr = service.SubmitChanges("PR_MAP_DONOR_DATA",
                                                    item.PA_NUMBER,
                                                    item.Donor_CD,
                                                    item.Address,
                                                    entered_by, 
                                                    Entered_Dt, 
                                                    Entered_Dt_LOC
                                                    );
                }

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                qr = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return qr;
        }
        public DataTable BankMappingSearchList(NHRSBankMapping objBankMapping)
        {
            DataTable dt = new DataTable();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object bankcd = DBNull.Value;
            if (objBankMapping.DISTRICT_CD.ConvertToString() != "")
            {
                District = objBankMapping.DISTRICT_CD;
            }
            if (objBankMapping.VDC_MUN_CD != "")
            {
                Vdc = objBankMapping.VDC_MUN_CD;
            }
            if (objBankMapping.WARD_NO.ConvertToString() != "")
            {
                Ward = objBankMapping.WARD_NO;
            }
            if (objBankMapping.BANK_CD.ConvertToString() != "")
            {
                bankcd = objBankMapping.BANK_CD;
            }
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_SETUP";
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_BANK_MAPPING_SEARCH",
                        District,
                        Vdc,
                        Ward,
                        bankcd,
                        DBNull.Value,
                        DBNull.Value,
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
        public DataTable GetMappingBankDetails(string id)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Select * from NHRS_BANK_MAPPING WHERE MAPPING_ID='"+id+"'";
               
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
