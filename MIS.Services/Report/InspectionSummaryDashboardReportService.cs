using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using EntityFramework;

namespace MIS.Services.Report
{
    public class InspectionSummaryDashboardReportService
    {
        public DataTable InspectionSummary()
        {
            DataTable dt = null;
           
            //string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                //cmdText = "SELECT A.DISTRICT,A.INSPECTION_ONE FROM "
                //+ "(SELECT " + Utils.ToggleLanguage("DIS.DESC_ENG", "DIS.DESC_LOC") + " AS DISTRICT ,COUNT(NIM.INSPECTION_LEVEL1) AS INSPECTION_ONE"
                //+ " FROM MIS_DISTRICT DIS"
                //+ " JOIN NHRS_INSPECTION_DESIGN NID ON DIS.DISTRICT_CD= NID.DISTRICT_CD "
                //+ " JOIN NHRS_INSPECTION_MST NIM ON NID.INSPECTION_MST_ID=NIM.INSPECTION_MST_ID "
                //+ " WHERE INSPECTION_LEVEL2 IS NOT NULL OR (FINAL_DECISION_2_APPROVE='Y' AND INSPECTION_LEVEL1 IS NOT NULL)"
                //+ " GROUP BY" + Utils.ToggleLanguage("DIS.DESC_ENG", "DIS.DESC_LOC") + ")A"
                //+ " JOIN "
                //+ " (SELECT COUNT(INSPECTION_LEVEL2 FROM NHRS_INSPECTION_MST  WHERE INSPECTION_LEVEL3 IS NOT NULL OR (FINAL_DECISION_2_APPROVE='Y' "
                //+ " AND INSPECTION_LEVEL2 IS NOT NULL))B";



                






                //  cmdText = "SELECT " + Utils.ToggleLanguage(" DIS.DESC_ENG", "DIS.DESC_LOC")
                //+ " AS DISTRICT,  A.INSPECTION_ONE, B.INSPECTION_TWO,C.INSPECTION_THREE FROM "
                //+ " ( "
                //+ "(SELECT NID.DISTRICT_CD ,COUNT(NIM.INSPECTION_LEVEL1) AS INSPECTION_ONE"
                //+ " FROM NHRS_INSPECTION_MST NIM"
                //+ " JOIN NHRS_INSPECTION_DESIGN NID ON NID.INSPECTION_MST_ID=NIM.INSPECTION_MST_ID "
                //+ " WHERE INSPECTION_LEVEL2 IS NOT NULL OR (FINAL_DECISION_2_APPROVE='Y' AND INSPECTION_LEVEL1 IS NOT NULL)"
                //+ " GROUP BY DISTRICT_CD)A"
                //+ " FULL OUTER JOIN "
                //+ " (SELECT NID.DISTRICT_CD ,COUNT(NIM.INSPECTION_LEVEL2) AS INSPECTION_TWO"
                //+ " FROM NHRS_INSPECTION_MST NIM"
                //+ " JOIN NHRS_INSPECTION_DESIGN NID ON NID.INSPECTION_MST_ID=NIM.INSPECTION_MST_ID "
                //+ " WHERE INSPECTION_LEVEL3 IS NOT NULL OR (FINAL_DECISION_2_APPROVE='Y' AND INSPECTION_LEVEL2 IS NOT NULL)"
                //+ " GROUP BY DISTRICT_CD)B"
                //+ " ON A.DISTRICT_CD=B.DISTRICT_CD "
                //+ " FULL OUTER JOIN "
                //+ " (SELECT NID.DISTRICT_CD ,COUNT(NIM.INSPECTION_LEVEL3) AS INSPECTION_THREE"
                //+ " FROM NHRS_INSPECTION_MST NIM"
                //+ " JOIN NHRS_INSPECTION_DESIGN NID ON NID.INSPECTION_MST_ID=NIM.INSPECTION_MST_ID "
                //+ " WHERE FINAL_DECISION_2_APPROVE='Y' AND INSPECTION_LEVEL3 IS NOT NULL"
                //+ " GROUP BY DISTRICT_CD)C"
                //+ " ON A.DISTRICT_CD=C.DISTRICT_CD "
                //+ " JOIN"
                //+ " MIS_DISTRICT DIS ON A.DISTRICT_CD=DIS.DISTRICT_CD"
                //+ " ) WHERE 1=1  "
                //+ " ORDER BY " + Utils.ToggleLanguage("DIS.DESC_ENG", "DIS.DESC_LOC");
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "GET_INSPECTION_DASHBOARD",Utils.ToggleLanguage("E", "N"),""
                        
                         
                    );
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
