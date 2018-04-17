using EntityFramework;
using ExceptionHandler;
using MIS.Models.Enrollment;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MIS.Models.Setup;

namespace MIS.Services.Setup
{
    public class EligibilityRuleService
    {
        //public DataTable getEligibilitySearchRule()
        //{
        //    DataTable dt = new DataTable();
            
        //    NhrsSetupEligibilityRule modl=new NhrsSetupEligibilityRule();
        //    using (ServiceFactory service=new ServiceFactory())
        //    {
        //        try
        //        {
        //            modl.RULE_CD = modl.RULE_CD;
        //            service.Begin();
        //            service.PackageName = "PKG_NHRS_SETUP";
                    
        //            QueryResult qr = service.SubmitChanges(modl, true);

        //        }
        //        catch (OracleException oe)
        //        {
        //            service.RollBack();
        //            ExceptionManager.AppendLog(oe);
        //        }
        //        catch (Exception ex)
        //        {
        //            service.RollBack();
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //        return dt;

        //    }

        //}
         public DataTable getEligibilitySearchRule(string rulecd)
        {
            DataTable dtbl = null;
             using(ServiceFactory service=new ServiceFactory())
             {
                 string cmdText = "select * FROM NHRS_ELIGIBILITY_RULE_DTL where 1=1";
                 if (rulecd != null && rulecd!="")
                 {
                     cmdText+="AND RULE_CD='"+rulecd+"'";
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
                 catch(Exception)
                 {
                     dtbl = null;
                 }
                 finally
                 {
                     if(service.Transaction!=null)
                     {
                         service.End();
                     }
                 }
                 return dtbl;
             }
        }
        public void UpdateEligibilityRule(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRule ElgbRule = new NhrsEntitySetupEligibilityRule();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    ElgbRule.DefinedCd = nhrsElgbRle.DEFINED_CD.ConvertToString();
                    ElgbRule.RuleName = nhrsElgbRle.RULE_NAME;
                    ElgbRule.ZoneCd = nhrsElgbRle.ZONE_CD.ToDecimal();
                    ElgbRule.DistrictCD = nhrsElgbRle.DISTRICT_CD.ToDecimal();
                    ElgbRule.VdcMunCD = nhrsElgbRle.VDC_MUN_CD.ToDecimal();
                    ElgbRule.WardNo = nhrsElgbRle.WARD_NO.ToDecimal();
                    ElgbRule.RuleEffectiveDtFrom = nhrsElgbRle.RULE_EFFECTIVE_DT_FROM.ToDateTime();
                    ElgbRule.RuleEffectiveDtFromLoc = nhrsElgbRle.RULE_EFFECTIVE_DT_FROM_LOC.ConvertToString();
                    ElgbRule.RuleEffectiveDtTo = nhrsElgbRle.RULE_EFFECTIVE_DT_TO.ToDateTime();
                    ElgbRule.RuleEffectiveDtToLoc = nhrsElgbRle.RULE_EFFECTIVE_DT_LOC_TO;
                    ElgbRule.OrderNo = nhrsElgbRle.ORDER_NO;
                    ElgbRule.Remarks = nhrsElgbRle.REMARKS;
                    ElgbRule.Disabled = nhrsElgbRle.DISABLED.ConvertToString();
                    ElgbRule.Approved = nhrsElgbRle.APPROVED.ConvertToString();
                    ElgbRule.ApprovedBy = nhrsElgbRle.APPROVED_BY;
                    ElgbRule.ApprovedDt = DateTime.Now.ToDateTime();
                    ElgbRule.UpdatedDt = DateTime.Now.ToDateTime();
                    ElgbRule.ApprovedDtLoc = nhrsElgbRle.APPROVED_DT_LOC;
                    ElgbRule.EnteredBy = CommonVariables.UserName;
                    ElgbRule.EnterdDt = DateTime.Now.ToDateTime();

                    ElgbRule.Mode = "U";

                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);


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
        public void UpdateEligibilityDetailRule(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRuleDetail ElgbRule = new NhrsEntitySetupEligibilityRuleDetail();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleSno = nhrsElgbRle.RULE_SNO;
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    ElgbRule.TableName = nhrsElgbRle.TABLE_NAME;
                    ElgbRule.ColumnName = nhrsElgbRle.COLUMN_NAME;
                    ElgbRule.ColumnValue = nhrsElgbRle.COLUMN_VALUE;
                    ElgbRule.OPERATOR = nhrsElgbRle.OPERATOR;
                    ElgbRule.Disabled = nhrsElgbRle.DISABLED;
                    ElgbRule.RangeFrom = nhrsElgbRle.RANGE_FROM.ConvertToString();
                    ElgbRule.RangeTo = nhrsElgbRle.RANGE_TO.ConvertToString();
                    ElgbRule.OrderNo = nhrsElgbRle.ORDER_NO;
                    ElgbRule.Remarks = nhrsElgbRle.REMARKS.ConvertToString();
                    ElgbRule.EnteredBy = CommonVariables.UserName;
                    ElgbRule.EnterdDt = DateTime.Now.ConvertToString();

                    ElgbRule.Mode = "U";

                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);


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

        public void InsertEligibilityRule(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRule ElgbRule = new NhrsEntitySetupEligibilityRule();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    ElgbRule.DefinedCd = nhrsElgbRle.DEFINED_CD.ConvertToString();
                    ElgbRule.RuleName = nhrsElgbRle.RULE_NAME;
                    ElgbRule.ZoneCd = nhrsElgbRle.ZONE_CD.ToDecimal();
                    ElgbRule.DistrictCD = nhrsElgbRle.DISTRICT_CD.ToDecimal();
                    ElgbRule.VdcMunCD = nhrsElgbRle.VDC_MUN_CD.ToDecimal();
                    ElgbRule.WardNo = nhrsElgbRle.WARD_NO.ToDecimal();
                    ElgbRule.RuleEffectiveDtFrom = nhrsElgbRle.RULE_EFFECTIVE_DT_FROM.ToDateTime();
                    ElgbRule.RuleEffectiveDtFromLoc = nhrsElgbRle.RULE_EFFECTIVE_DT_FROM_LOC.ConvertToString();
                    ElgbRule.RuleEffectiveDtTo = nhrsElgbRle.RULE_EFFECTIVE_DT_TO.ToDateTime(); ;
                    ElgbRule.RuleEffectiveDtToLoc = nhrsElgbRle.RULE_EFFECTIVE_DT_LOC_TO.ConvertToString();
                    ElgbRule.OrderNo = nhrsElgbRle.ORDER_NO.ToDecimal();
                    ElgbRule.Disabled = nhrsElgbRle.DISABLED.ConvertToString();
                    ElgbRule.Approved = nhrsElgbRle.APPROVED.ConvertToString();
                    ElgbRule.ApprovedBy = nhrsElgbRle.APPROVED_BY;
                    ElgbRule.ApprovedDt = DateTime.Now.ToDateTime();
                    ElgbRule.UpdatedDt = DateTime.Now.ToDateTime();
                    ElgbRule.ApprovedDtLoc = nhrsElgbRle.APPROVED_DT_LOC;
                    ElgbRule.EnteredBy = CommonVariables.UserName;
                    ElgbRule.EnterdDt = DateTime.Now.ToDateTime();
                    ElgbRule.EnteredDtLoc = DateTime.Now.ConvertToString();

                    ElgbRule.Mode = "I";

                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);

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
        public void InsertEligibilityRuleDetail(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRuleDetail ElgbRule = new NhrsEntitySetupEligibilityRuleDetail();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    ElgbRule.TableName = nhrsElgbRle.TABLE_NAME;
                    ElgbRule.ColumnName = nhrsElgbRle.COLUMN_NAME;
                    ElgbRule.ColumnValue = nhrsElgbRle.COLUMN_VALUE;
                    ElgbRule.OPERATOR = nhrsElgbRle.OPERATOR;
                    ElgbRule.Disabled = nhrsElgbRle.DISABLED;
                    ElgbRule.RangeFrom = nhrsElgbRle.RANGE_FROM.ConvertToString();
                    ElgbRule.RangeTo = nhrsElgbRle.RANGE_TO.ConvertToString();
                    ElgbRule.OrderNo = nhrsElgbRle.ORDER_NO;
                    ElgbRule.Remarks = nhrsElgbRle.REMARKS.ConvertToString();
                    ElgbRule.EnteredBy = CommonVariables.UserName;
                    ElgbRule.EnterdDt = DateTime.Now.ConvertToString();
                    ElgbRule.Mode = "I";
                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);

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
        public void DeleteEligibilityRule(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRule ElgbRule = new NhrsEntitySetupEligibilityRule();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    
                    ElgbRule.Mode = "D";

                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);

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
        public void DeleteEligibilityRuleDetail(NhrsSetupEligibilityRule nhrsElgbRle)
        {
            NhrsEntitySetupEligibilityRuleDetail ElgbRule = new NhrsEntitySetupEligibilityRuleDetail();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    ElgbRule.RuleCd = nhrsElgbRle.RULE_CD;
                    ElgbRule.RuleSno = nhrsElgbRle.RULE_SNO;
                    ElgbRule.Mode = "D";

                    service.PackageName = "PKG_NHRS_SETUP";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(ElgbRule, true);

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
        public DataTable getEligibilityRuleList()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Select DEFINED_CD,RULE_NAME,RULE_EFFECTIVE_DT_FROM,RULE_EFFECTIVE_DT_TO,DISTRICT_CD,VDC_MUN_CD from NHRS_ELIGIBILITY_RULE ";
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
        public DataTable getEligibilityRule(string rulecd)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Select * from NHRS_ELIGIBILITY_RULE where DEFINED_CD='"+rulecd+"'";
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
        public DataTable getEligibilityDetailRule(string rulecd,string rulesno)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Select * from NHRS_ELIGIBILITY_RULE_DTL where RULE_CD='" + rulecd + "' AND RULE_SNO='"+rulesno+"'";
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
        public DataTable getEligibilityRuleDetailList()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Select RULE_CD,RULE_SNO,TABLE_NAME,COLUMN_NAME,COLUMN_VALUE,OPERATOR,RANGE_FROM,RANGE_TO from NHRS_ELIGIBILITY_RULE_DTL ";
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
