using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;

namespace MIS.Services.Inspection
{
    public class InspectionJsonAdditionalService
    {
        public bool CheckDuplicate(string PaNumber, string FormNumber, string inspectionLevel)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_INSPECTION_PAPER_DTL where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND SERIAL_NUMBER='" + FormNumber.ToString() + "' AND INSPECTION_LEVEL='" + inspectionLevel.ConvertToString() + "'  ";

 
                try
                {
                    dt = service.GetDataTable(cmdText, null);

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
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }

        public bool CheckHigherInspection(string PaNumber, string FormNumber, string inspectionLevel)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT ";
                if(inspectionLevel=="2")
                {
                    cmdText = cmdText + "APP_FOR_INSP2 ,";
                }
                if (inspectionLevel == "3")
                {
                    cmdText = cmdText + "APP_FOR_INSP3 ,";
                }
                cmdText =cmdText+ "NRA_DEFINED_CD FROM NHRS_INSPECTION_APPLICATION where 1=1 ";

                if (inspectionLevel == "2")
                {
                    cmdText = cmdText + "AND APP_FOR_INSP2 = '1'";
                }
                if (inspectionLevel == "3")
                {
                    cmdText = cmdText + "AND APP_FOR_INSP2 = '2'";
                }

                try
                {
                    dt = service.GetDataTable(cmdText, null);


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
            if (dt.Rows.Count> 0)
            {
                if (inspectionLevel == "2" && (dt.Rows[0]["APP_FOR_INSP2"].ConvertToString() != null && dt.Rows[0]["APP_FOR_INSP2"].ConvertToString() != ""))
                {
                    res = true;
                }
                if (inspectionLevel == "3" && (dt.Rows[0]["APP_FOR_INSP3"].ConvertToString() != null && dt.Rows[0]["APP_FOR_INSP3"].ConvertToString() != ""))
                {
                    res = true;
                }
                
            }

            return res;
        }



        // save duplicate data 
        public Boolean saveDuplicateInspectionData(string paNumber, string formNumber, string benfname, string district, string vdc, string ward, string fileName, string reason)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qrSaveDuplicateInspection = null;
            bool res = false;
            string duplicateId = "";
            string definedId = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qrSaveDuplicateInspection = service.SubmitChanges("PR_NHRS_INSPECTION_DUPLI_DATA",
                                               "I",
                                              duplicateId.ToDecimal(),
                                               "".ConvertToString(),
                                               DateTime.Now,                    // inspection date 
                                               paNumber.ConvertToString(),
                                               formNumber.ConvertToString(),
                                               benfname.ConvertToString(),
                                               district.ToDecimal(),                     // district
                                               vdc.ToDecimal(),             // vdc mun cd
                                               ward.ToDecimal(),                         // ward TOLE

                                               "".ConvertToString(),
                                               "".ConvertToString(),
                                               "".ConvertToString(),
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),


                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),
                                                fileName.ConvertToString(),
                                                reason.ConvertToString()

                                                   );
                }


                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
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
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qrSaveDuplicateInspection != null)
                    {
                        res = qrSaveDuplicateInspection.IsSuccess;
                    }
                }

                return res;

            }
        }

        public bool CheckUpdate(string PaNumber, string level, string formNumber)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_INSPECTION_PAPER_DTL where NRA_DEFINED_CD='" + PaNumber.ToUpper() + "' AND INSPECTION_LEVEL='" + level.ConvertToString() + "' AND SERIAL_NUMBER!='" + formNumber.ToString() + "'";




                try
                {
                    dt = service.GetDataTable(cmdText, null);

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
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }



     
    }
}
