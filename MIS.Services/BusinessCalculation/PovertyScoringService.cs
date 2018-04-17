using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Registration.Member;
using System.Data;
using System.Web;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Web.Mvc;
using MIS.Models.Registration.Household;

namespace MIS.Services.BusinessCalculation
{
    public class PovertyScoringService
    {
        public DataTable Household_GetAllCriteriaToTable(string district, string fromHousehold, string toHousehold, string rulecalc, string bussrule, int pageIndex, int pageSize, string rangefrom, string rangeto, string ascdesc, string VdcMunCd, string WardNo)
        {
            DataTable dtbl = null;
            string cmdText = "";
            if (district != "" && district != null && district != "0")
            {
                district = GetData.GetCodeFor(DataType.District, district);
            }
            if (!string.IsNullOrEmpty(VdcMunCd) && VdcMunCd != "0")
            {
                VdcMunCd = GetData.GetCodeFor(DataType.VdcMun, VdcMunCd);
            }
            
            using (ServiceFactory service = new ServiceFactory())
            {
                if (!string.IsNullOrWhiteSpace(rangefrom))
                {
                    rangefrom = GetValueForPercentage(rangefrom, district);
                }
                if (!string.IsNullOrWhiteSpace(rangeto))
                {
                    rangeto = GetValueForPercentage(rangeto, district);
                }
                service.Begin();
                cmdText = "SELECT RowNum,TMP.* FROM (select RNG.*, ROWNUM PAGE  from (SELECT MST.*  FROM  VW_HOUSEHOLD_DTL MST where 1=1";

                if (district != "" && district != null && district != "0")
                {
                    cmdText += String.Format(" and PER_DISTRICT_CD='" + district + "'");
                }
                if (!string.IsNullOrEmpty(VdcMunCd) && VdcMunCd != "0")
                {
                    cmdText += String.Format(" and PER_VDC_MUN_CD='" + VdcMunCd + "'");
                }
                if (!string.IsNullOrEmpty(WardNo) && WardNo != "0")
                {
                    cmdText += String.Format(" and PER_WARD_NO='" + WardNo + "'");
                }
                if (fromHousehold != "" && fromHousehold != null && toHousehold != "" && toHousehold != null)
                {
                    cmdText += String.Format(" and SUBSTR ( h_defined_cd , INSTR (h_defined_cd ,'-' , 1,  2) +1 , LENGTH ( h_defined_cd ) -  INSTR (h_defined_cd ,'-' , 1,  2) ) between To_Number(SUBSTR ( '" + fromHousehold + "' , INSTR ('" + fromHousehold + "' ,'-' , 1,  2) +1 , LENGTH ( '" + fromHousehold + "' ) -  INSTR ('" + fromHousehold + "' ,'-' , 1,  2) ))  and  To_Number(SUBSTR ( '" + toHousehold + "' , INSTR ('" + toHousehold + "' ,'-' , 1,  2) +1 , LENGTH ( '" + toHousehold + "' ) -  INSTR ('" + toHousehold + "' ,'-' , 1,  2) ))");


                }
                if ((fromHousehold != "" && fromHousehold != null) && (toHousehold == "" || toHousehold == null))
                {
                    cmdText += String.Format(" and SUBSTR ( h_defined_cd , INSTR (h_defined_cd ,'-' , 1,  2) +1 , LENGTH ( h_defined_cd ) -  INSTR (h_defined_cd ,'-' , 1,  2) ) >= To_Number(SUBSTR ( '" + fromHousehold + "' , INSTR ('" + fromHousehold + "' ,'-' , 1,  2) +1 , LENGTH ( '" + fromHousehold + "' ) -  INSTR ('" + fromHousehold + "' ,'-' , 1,  2) ))");
                }
                if ((fromHousehold == "" || fromHousehold == null) && (toHousehold != "" && toHousehold != null))
                {
                    cmdText += String.Format(" and SUBSTR ( h_defined_cd , INSTR (h_defined_cd ,'-' , 1,  2) +1 , LENGTH ( h_defined_cd ) -  INSTR (h_defined_cd ,'-' , 1,  2) ) between (0) and To_Number(SUBSTR ( '" + toHousehold + "' , INSTR ('" + toHousehold + "' ,'-' , 1,  2) +1 , LENGTH ( '" + toHousehold + "' ) -  INSTR ('" + toHousehold + "' ,'-' , 1,  2) )) ");
                }

                if (rulecalc == "Calculated")
                {

                    if (bussrule != "" && bussrule != null && bussrule != "0")
                    {
                        cmdText += String.Format(" and RULE_FLAG='" + bussrule + "'");
                    }
                    cmdText += String.Format(" and RULE_CALC='Y' ");
                }
                else if (rulecalc == "Not Calculated")
                {

                    cmdText += String.Format(" and RULE_CALC='N' ");
                }

                cmdText += " ORDER BY RULE_VALUE asc ) RNG";
                cmdText += "  ) TMP WHERE ROWNUM BETWEEN " + (((pageIndex - 1) * pageSize) + 1) + " AND " + (pageIndex * pageSize) + "";
                if (rangefrom != "" && rangefrom != null && rangeto != "" && rangeto != null)
                {
                    cmdText += string.Format("  AND  PAGE between ceil(" + rangefrom + ") and ceil(" + rangeto + ")");


                }
                else if ((rangefrom != "" && rangefrom != null) && (rangeto == "" || rangeto == null))
                {
                    cmdText += string.Format("  AND  PAGE   >= ceil(" + rangefrom + ")");


                }
                else if ((rangeto != "" && rangeto != null) && (rangefrom == "" || rangefrom == null))
                {
                    cmdText += string.Format("  AND  PAGE between '" + 0 + "' and ceil(" + rangeto + ")");

                }
                cmdText += " ORDER BY   TMP.RULE_VALUE  " + ascdesc + ", (SUBSTR (TMP.H_DEFINED_CD  , INSTR(TMP.H_DEFINED_CD ,'-') + 4)) " + ascdesc;

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


        public DataTable Household_GetAllCriteriaToTableToApprove(string sessionid, string bussrule, int pageIndex, int pageSize)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();

                if (bussrule == "PMT")
                {
                    cmdText = "SELECT RowNum,TMP.* FROM (SELECT MST.* , ROWNUM PAGE,(select sum(RULE_VALUE) FROM MIS_PMT_CALC_TEMP_RESULTS WHERE SESSION_ID='" + sessionid + "'  and household_id = mst.household_id) AS RULE_VALUE_TEMP FROM  VW_HOUSEHOLD_DTL MST where 1=1";
                    cmdText += " and HOUSEHOLD_ID IN (SELECT DISTINCT(HOUSEHOLD_ID) FROM MIS_PMT_CALC_TEMP_RESULTS where SESSION_ID='" + sessionid + "')";
                }
                else
                {
                    cmdText = "SELECT RowNum,TMP.* FROM (SELECT MST.* , ROWNUM PAGE,(select round(sum(weightage),4) FROM MIS_MPI_CALC_TEMP_RESULTS WHERE SESSION_ID='" + sessionid + "' AND   household_id = mst.household_id) AS RULE_VALUE_TEMP FROM  VW_HOUSEHOLD_DTL MST where 1=1";
                    cmdText += " and HOUSEHOLD_ID IN (SELECT DISTINCT(HOUSEHOLD_ID) FROM MIS_MPI_CALC_TEMP_RESULTS where SESSION_ID='" + sessionid + "')";
                }
                cmdText += " ORDER BY (SUBSTR (H_DEFINED_CD  , INSTR(H_DEFINED_CD ,'-') + 4)) ASC";
                cmdText += " ) TMP WHERE PAGE BETWEEN " + (((pageIndex - 1) * pageSize) + 1) + " AND " + (pageIndex * pageSize) + "";
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
        public int GetCount(string district, string fromHousehold, string toHousehold, string rulecalc, string bussrule, int pageIndex, int pageSize, string rangefrom, string rangeto, string ascdesc,string vdcMunCd, string wardNo)
        {
            int countValue = 0;
            DataTable dtbl = null;
            string cmdText = "";

            if (district != "" && district != null && district != "0")
            {
                district = GetData.GetCodeFor(DataType.District, district);
            }
            if (!string.IsNullOrEmpty(vdcMunCd) && vdcMunCd != "0")
            {
                vdcMunCd = GetData.GetCodeFor(DataType.VdcMun, vdcMunCd);
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    if (!string.IsNullOrWhiteSpace(rangefrom))
                    {
                        rangefrom = GetValueForPercentage(rangefrom, district);
                    }
                    if (!string.IsNullOrWhiteSpace(rangeto))
                    {
                        rangeto = GetValueForPercentage(rangeto, district);
                    }
                    cmdText = "SELECT count(*) Count FROM (select * from  (SELECT MST.* , ROWNUM PAGE FROM  VW_HOUSEHOLD_DTL MST where 1=1";

                    if (district != "" && district != null && district != "0")
                    {
                        cmdText += String.Format(" and PER_DISTRICT_CD='" + district + "'");
                    }
                    if (!string.IsNullOrEmpty(vdcMunCd) && vdcMunCd != "0")
                    {
                        cmdText += String.Format(" and PER_VDC_MUN_CD='" + vdcMunCd + "'");
                    }
                    if (!string.IsNullOrEmpty(wardNo) && wardNo != "0")
                    {
                        cmdText += String.Format(" and PER_WARD_NO='" + wardNo + "'");
                    }
                    if (fromHousehold != "" && fromHousehold != null && toHousehold != "" && toHousehold != null)
                    {
                        cmdText += String.Format(" and (SUBSTR (H_DEFINED_CD  , INSTR(H_DEFINED_CD ,'-') + 4)) between (SUBSTR ('" + fromHousehold + "' , INSTR('" + fromHousehold + "','-') + 4)) and (SUBSTR ('" + toHousehold + "' , INSTR('" + toHousehold + "','-') + 4))");
                    }
                    if ((fromHousehold != "" && fromHousehold != null) && (toHousehold == "" || toHousehold == null))
                    {
                        cmdText += String.Format(" and (SUBSTR (H_DEFINED_CD  , INSTR(H_DEFINED_CD ,'-') + 4)) >= (SUBSTR ('" + fromHousehold + "' , INSTR('" + fromHousehold + "','-') + 4)) ");
                    }
                    if ((fromHousehold == "" || fromHousehold == null) && (toHousehold != "" && toHousehold != null))
                    {
                        cmdText += String.Format(" and (SUBSTR (H_DEFINED_CD  , INSTR(H_DEFINED_CD ,'-') + 4)) between (0) and (SUBSTR ('" + toHousehold + "' , INSTR('" + toHousehold + "','-') + 4))");
                    }

                    if (rulecalc == "Calculated")
                    {
                        cmdText += String.Format(" and RULE_CALC='Y'");
                        if (bussrule != "" && bussrule != null && bussrule != "0")
                        {
                            cmdText += String.Format(" and RULE_FLAG='" + bussrule + "'");
                        }
                    }
                    else if (rulecalc == "Not Calculated")
                    {
                        cmdText += String.Format(" and RULE_CALC='N'");
                    }
                    cmdText += " ORDER BY (SUBSTR (H_DEFINED_CD  , INSTR(H_DEFINED_CD ,'-') + 4)) " + ascdesc;
                    //cmdText += " ) TMP WHERE ROWNUM BETWEEN " + (((pageIndex - 1) * pageSize) + 1) + " AND " + (pageIndex * pageSize) + "";
                    if (rangefrom != "" && rangefrom != null && rangeto != "" && rangeto != null)
                    {
                        cmdText += string.Format(" ) where page between '" + rangefrom + "' and '" + rangeto + "'");
                    }
                    if ((rangefrom != "" && rangefrom != null) && (rangeto == "" || rangeto == null))
                    {
                        cmdText += string.Format(" ) where page >= '" + rangefrom + "'");
                    }
                    if ((rangeto != "" && rangeto != null) && (rangefrom == "" || rangefrom == null))
                    {
                        cmdText += string.Format(" ) where page between '" + 0 + "' and '" + rangeto + "'");
                    }

                    cmdText += "  ) TMP";
                    dtbl = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        countValue = Convert.ToInt32(drow["COUNT"].ToString());
                    }
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
            return countValue;
        }
        public string GetSessionID()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                cmdText = "SELECT SEQ_PMT_CALC.NEXTVAL as SESSIONID FROM DUAL";
                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dtbl.Rows[0]["SESSIONID"].ToString();
        }
        public DataTable GetReportForPovertyScoring(string householdid, string calcType)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                cmdText = "select * from MIS_PMT_MPI_CALC_RESULTS where HOUSEHOLD_ID='" + householdid + "' and BATCH_ID=(select max(Batch_ID) from MIS_PMT_MPI_CALC_RESULTS where HOUSEHOLD_ID='" + householdid + "') and RULE_FLAG='" + calcType + "' order by SNO";
                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public DataTable GetTempValueForReport(string householdid, string calcType)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                if (calcType == "PMT")
                {
                    cmdText = "select * from MIS_PMT_CALC_TEMP_RESULTS WHERE HOUSEHOLD_ID='" + householdid + "'  AND session_id= (select max(session_id) from mis_pmt_calc_temp_results WHERE household_id = '" + householdid + "')";
                }
                else
                {
                    cmdText = "select * from MIS_MPI_CALC_TEMP_RESULTS WHERE HOUSEHOLD_ID='" + householdid + "'  AND session_id= (select max(session_id) from MIS_MPI_CALC_TEMP_RESULTS WHERE household_id = '" + householdid + "')";
                }
                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
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
        public SelectListItem GetHouseholdById(string householdID)
        {


            SelectListItem lii = new SelectListItem();

            lii.Value = "";
            lii.Text = "";
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "SELECT * FROM MIS_HOUSEHOLD_INFO where HOUSEHOLD_ID='" + householdID + "'";

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
                foreach (DataRow drow in dtbl.Rows)
                {
                    lii.Value = drow["DEFINED_CD"].ToString();
                    lii.Text = drow["RULE_FLAG"].ToString();
                }
            }
            catch (Exception)
            {
                lii = null;
            }
            return lii;
        }
    
        public DataTable GetHouseholdDetail(string householdID)
        { 
              DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "SELECT t1.h_defined_cd householddefinedcd, t2.full_name_eng, t2.full_name_loc, t1.per_district_eng, t1.per_district_loc, t1.per_vdcmunicipility_eng, t1.per_vdcmunicipility_loc,t1.PER_WARD_NO, t1.PER_AREA_ENG, t1.PER_AREA_LOC FROM vw_household_dtl t1, mis_member t2 WHERE t1.member_id = t2.member_id AND t1.household_id ='" + householdID + "'";

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
                
            }
            catch (Exception)
            {
                dtbl = null;
            }
            return dtbl;
        }

        public string GetValueForPercentage(string rangeValue, string DistrictCode)
        {
            DataTable dtbl = null;
            string cmdText = "";
            double money_value = 0;
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                cmdText = "select count(*) VALUE_AMOUNT from VW_HOUSEHOLD_DTL where rule_calc='Y' and per_district_cd='" + DistrictCode + "'";
                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dtbl != null)
            {
                if (dtbl.Rows[0]["VALUE_AMOUNT"].ToString() != "")
                {
                    money_value = (Convert.ToDouble(rangeValue) * Convert.ToDouble(dtbl.Rows[0]["VALUE_AMOUNT"].ToString())) / 100;
                }
            }
            return money_value.ToString();
        }

    }
}
