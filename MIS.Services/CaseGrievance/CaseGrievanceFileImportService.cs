using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;

namespace MIS.Services.CaseGrievance
{
    public class CaseGrievanceFileImportService
    {
        public Boolean SaveDataFromFileBrowse(DataTable paramTable, string fileName, out string exc)
        {
            QueryResult qr = null;
            QueryResult qr1 = null;
            bool res = false;
            exc = string.Empty;
            string batchID = "";
            string district = "";
            string vdc = "";
            string ward = "";
            //string nradefinedcode = "";
            if (paramTable != null)
            {
                district = paramTable.Rows[0][2].ToString();
                vdc = paramTable.Rows[0][3].ToString();
                ward = paramTable.Rows[0][4].ToString();
            }
            //string fileExtension = Path.GetExtension(fileName);   
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_NHRS_GRIEVANCE_FILE_BATCH",
                                               "I",
                                               district,
                                               vdc,
                                                ward,
                                                 "Completed",
                                                fileName,//filename                                                 
                                                 CurrentDate,
                                                 SessionCheck.getSessionUsername(),
                                                 DBNull.Value,
                                                 DBNull.Value);

                    //Main Table 
                    batchID = qr["v_BATCH_ID"].ConvertToString();
                    //batchID = paramTable.Rows[0][3].ToString();
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        string OtherHouse = string.Empty;
                        if (paramTable.Rows[i][22].ToString() == "1")
                        {
                            OtherHouse = "Y";
                        }
                        if (paramTable.Rows[i][22].ToString() == "2")
                        {
                            OtherHouse = "N";
                        }


                        //foreach (DataRow row in paramTable.Rows)

                        //if (CheckDuplicate(paramTable.Rows[i][3].ToString()))
                        ////if (CheckDuplicate(row["DIST"] + "-" + row["VDCMUN"] + "-" + row["WARD"] + "-" + row["EA"] + "-" + row["AGRMNT_NO"]))
                        //{
                        //    res = true;
                        //    string cmdText = String.Format("update NHRS_ENROLLMENT_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='Data Duplication' where BATCH_ID='" + batchID + "'");
                        //    SaveErrorMessgae(cmdText);
                        //    cmdText = String.Format("delete from NHRS_ENROLLMENT_MOU where FILE_BATCH_ID='" + batchID + "'");
                        //    SaveErrorMessgae(cmdText);
                        //    break;
                        //}
                        qr = service.SubmitChanges("PR_GRIEVANCE_REGISTRATION",
                            //"I",
                                           Convert.ToDecimal(paramTable.Rows[i][0].ToString()), //Form No
                                           Convert.ToDecimal(paramTable.Rows[i][0].ToString()),// Defined Cd
                                            Convert.ToDecimal(paramTable.Rows[i][1].ToString()), // Registration no
                                           Convert.ToDecimal(paramTable.Rows[i][2].ToString()), //District
                                            Convert.ToDecimal(paramTable.Rows[i][3].ToString()), //VDC
                                            Convert.ToDecimal(paramTable.Rows[i][4].ToString()),// Ward
                                           paramTable.Rows[i][5].ToString(),// Area
                                           DBNull.Value,  //Registration Date Eng
                                           paramTable.Rows[i][6].ToString(), //Registration Date Loc
                                           paramTable.Rows[i][7].ToString(),//Full Name Eng
                                           paramTable.Rows[i][7].ToString(),//Full Name Loc
                                           paramTable.Rows[i][8].ToString(), //Lalpurja No
                                           DBNull.Value,  //Issued date Eng
                                           paramTable.Rows[i][9].ToString(),  //Issued Date Loc
                                           paramTable.Rows[i][10].ToString(),  // Fathers Name Eng
                                            paramTable.Rows[i][10].ToString(),  // Fathers Name Loc
                                           paramTable.Rows[i][11].ToString(),  //citizenship No
                                           DBNull.Value,  //Issued Date Eng
                                           paramTable.Rows[i][12].ToString(), //issued Date Loc
                                           paramTable.Rows[i][13].ToString(), //GrandFather Name LOc
                                            paramTable.Rows[i][13].ToString(), //GrandFather Name Eng
                                            Convert.ToDecimal(paramTable.Rows[i][14].ToString()),  //Dist Cd
                                           Convert.ToDecimal(paramTable.Rows[i][15].ToString()),  //VDC CD
                                            Convert.ToDecimal(paramTable.Rows[i][17].ToString()), //Ward
                                            Convert.ToDecimal(paramTable.Rows[i][16].ToString()), // Member Count
                                           paramTable.Rows[i][18].ToString(),  // Area
                                           paramTable.Rows[i][19].ToString(),  // Nissa No
                                           paramTable.Rows[i][20].ToString(),   // Phone No
                                            Convert.ToDecimal(paramTable.Rows[i][21].ToString()), // Legal Owner
                                            OtherHouse, //Other House
                                           paramTable.Rows[i][32].ToString(),  // Enumerator Full Name Eng
                                            DBNull.Value,
                                            paramTable.Rows[i][33].ToString(), //Enumenator Date Signed
                                           paramTable.Rows[i][34].ToString(),  //Case Addressed full Name
                                           DBNull.Value,
                                           paramTable.Rows[i][35].ToString(),  //Case Addressed Date
                                           SessionCheck.getSessionUsername(),
                                           DateTime.Now.ToShortDateString(),
                                           SessionCheck.getSessionUsername(),
                                           DateTime.Now.ToShortDateString(),
                                            batchID,
                                            DBNull.Value
                                           );

                        //Case Grievance Type
                        string CaseRegistrationID = qr["v_CASE_REGISTRATION_ID"].ConvertToString();
                        string CaseGrievanceType = paramTable.Rows[i][29].ConvertToString();
                        string[] Typevalues = CaseGrievanceType.Split('/');
                        for (int j = 0; j < Typevalues.Length - 1; j++)
                        {
                            qr = service.SubmitChanges("PR_GRIEVANCE_TYPE_DETAIL",
                                DBNull.Value,
                                CaseRegistrationID.ToDecimal(),
                                Typevalues[j],
                                SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 DBNull.Value,
                                 batchID
                                );
                        }

                        //Document Detail
                        string DocumentType = paramTable.Rows[i][31].ConvertToString();
                        string[] Docvalues = DocumentType.Split('/');
                        for (int j = 0; j < Docvalues.Length - 1; j++)
                        {
                            qr = service.SubmitChanges("PR_GRIEVANCE_DOC_DETAIL",
                                DBNull.Value,
                                CaseRegistrationID.ToDecimal(),
                                Docvalues[j],
                                SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 batchID
                                );
                        }

                        //Other House
                        qr = service.SubmitChanges("PR_GRIEVANCE_OTH_DETAIL",
                            DBNull.Value,
                            CaseRegistrationID.ToDecimal(),
                            paramTable.Rows[i][23].ConvertToString(),
                            paramTable.Rows[i][24].ToDecimal(),
                            paramTable.Rows[i][25].ToDecimal(),
                            paramTable.Rows[i][26].ToDecimal(),
                            paramTable.Rows[i][27].ConvertToString(),
                            paramTable.Rows[i][28].ToDecimal(),
                            SessionCheck.getSessionUsername(),
                             DateTime.Now.ToShortDateString(),
                             SessionCheck.getSessionUsername(),
                             DateTime.Now.ToShortDateString(),
                              batchID
                            );


                    }

                }

                catch (OracleException oe)
                {
                    res = true;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);
                    if (batchID != "")
                    {
                        string cmdText = String.Format("update NHRS_GRIEVANCE_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_REGISTRATION where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_DOC_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_REGISTRATION where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_OTH_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                    }
                }
                catch (Exception ex)
                {
                    res = true;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                    if (batchID != "")
                    {
                        string cmdText = String.Format("update NHRS_GRIEVANCE_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_REGISTRATION where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_DOC_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_TYPE_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_OTH_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                    }
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
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;

            }
        }
        public Boolean SaveExcelDataFromFileBrowse(DataTable paramTable, string District, string VDC, string fileName, out string exc)
        {
            QueryResult qr = null;
             bool res = false;
            exc = string.Empty;
            string batchID = "";
            
            string FirstName = "";
            string MiddleName = "";
            string LastName = "";
            string FatherFirstName = "";
            string FatherMiddleName = "";
            string FatherLastName = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    var CurrentDate = DateTime.Now;

                    qr = service.SubmitChanges("PR_NHRS_GRIEVANCE_FILE_BATCH",
                                               "I",
                                               District.ConvertToString(),
                                               VDC.ConvertToString(),
                                                 "Completed",
                                                fileName,//filename                                                 
                                                 CurrentDate,
                                                 SessionCheck.getSessionUsername(),
                                                 DBNull.Value,
                                                 DBNull.Value);

                    //Main Table 
                    batchID = qr["v_BATCH_ID"].ConvertToString();
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        if (paramTable.Rows[i][3].ToString().Split(' ')[0] != " ")
                        {
                            if (paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {

                                FirstName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[0];
                                MiddleName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[1];
                                LastName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[3];
                            }
                            if (paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                FirstName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[0];
                                MiddleName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[1];
                                LastName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                FirstName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[0];
                                MiddleName = " ";
                                LastName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                            if (paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ').Count() < 2)
                            {

                                FirstName = paramTable.Rows[i][3].ToString().TrimEnd(' ').Split(' ')[0];
                                MiddleName = " ";
                                LastName = " ";
                            }
                        }
                        if (paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[0] != " ")
                        {
                            if (paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ').Count() > 3)
                            {
                                FatherFirstName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[1];
                                FatherLastName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[2] + paramTable.Rows[i][4].ToString().Split(' ')[3];
                            }
                            if (paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ').Count() == 3)
                            {

                                FatherFirstName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[1];
                                FatherLastName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[2];
                            }
                            if (paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ').Count() == 2)
                            {

                                FatherFirstName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[0];
                                FatherMiddleName = " ";
                                FatherLastName = paramTable.Rows[i][4].ToString().TrimEnd(' ').Split(' ')[1];
                            }
                        }

                        qr = service.SubmitChanges("PR_EXCELGRIEVANCE_REGISTRATION",
                            //"I",
                                               Convert.ToDecimal(paramTable.Rows[i][1].ToString()), //Form No                                          
                                               Convert.ToDecimal(paramTable.Rows[i][2].ToString()), // Registration no
                                               FirstName,
                                               MiddleName,
                                               LastName,
                                               paramTable.Rows[i][3].ToString(),//Full Name Eng
                                               District.ConvertToString(), //District
                                               VDC.ConvertToString(), //VDC
                                               Convert.ToDecimal(paramTable.Rows[i][7].ToString()),// Ward
                                               paramTable.Rows[i][8].ToString(),// Area  
                                               paramTable.Rows[i][2].ToString(), //Defined CD
                                               paramTable.Rows[i][9].ToString(),  // Nissa No
                                               paramTable.Rows[i][10].ToString(),  //citizenship No                                            
                                               paramTable.Rows[i][13].ToString(),   // Phone No  
                            //OtherHouse,
                                               FatherFirstName,
                                               FatherMiddleName,
                                               FatherLastName,
                                               paramTable.Rows[i][4].ToString(), //father name
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now.ToShortDateString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now.ToShortDateString(),
                                               batchID,
                                               DBNull.Value
                                           );

                        //Case Grievance Type
                        string CaseRegistrationID = qr["v_CASE_REGISTRATION_ID"].ConvertToString();
                        string CaseGrievanceType = paramTable.Rows[i][12].ConvertToString();
                        if (CaseGrievanceType.ConvertToString() == "")
                        {
                            CaseGrievanceType = "3";
                        }
                        string[] Typevalues = CaseGrievanceType.Split('/');
                        for (int j = 0; j < Typevalues.Length; j++)
                        {
                            qr = service.SubmitChanges("PR_GRIEVANCE_TYPE_DETAIL",
                                DBNull.Value,//grievance type id
                                CaseRegistrationID.ToDecimal(),//reg id
                                Typevalues[j],//grievance type id
                                SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 SessionCheck.getSessionUsername(),
                                 DateTime.Now.ToShortDateString(),
                                 paramTable.Rows[i][14].ToString(),
                                 batchID
                                );
                        }
                    }
                }
                catch (OracleException oe)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);
                        string cmdText = String.Format("update NHRS_GRIEVANCE_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_REGISTRATION where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_TYPE_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        //string ErrorMessage = GetBatchErrorMessage(batchID).ConvertToString();
                    }
                }
                catch (Exception ex)
                {
                    if (batchID != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("update NHRS_GRIEVANCE_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_REGISTRATION where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        cmdText = String.Format("delete from NHRS_GRIEVANCE_TYPE_DETAIL where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        //string ErrorMessage = GetBatchErrorMessage(batchID).ConvertToString();

                    }
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
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;

            }
        }

        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

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
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }

        public DataTable GetCaseRegistrationID(string FormNo, string District, string VDC, string Ward)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT CASE_REGISTRATION_ID FROM NHRS_GRIEVANCE_REGISTRATION where 1=1";

                if (FormNo != "")
                {
                    cmdText += " and upper(FORM_NO) ='" + FormNo.ToUpper() + "'";
                }
                if (District != "")
                {
                    cmdText += " and upper(REGISTRATION_DIST_CD)='" + District.ToUpper() + "'";
                }
                if (VDC != "")
                {
                    cmdText += " and upper(REGISTRATION_VDC_MUN_CD) ='" + VDC.ToUpper() + "'";
                }
                if (Ward != "")
                {
                    cmdText += " and upper(REGISTRATION_WARD_NO) ='" + Ward.ToUpper() + "'";
                }
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

        public DataTable GetBatchErrorMessage(string FileName)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT ERROR_MESSAGE FROM nhrs_grievance_file_batch where 1=1 and Filename='" + FileName + "'";
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

        public bool CheckDuplicate(string FormNo, string District, string VDC, string Ward,string Name,string NissaNo)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            //string lstStr = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT CASE_REGISTRATION_ID FROM NHRS_GRIEVANCE_REGISTRATION where 1=1";

                //if (FormNo != "")
                //{
                //    cmdText += " and upper(FORM_NO) ='" + FormNo.ToUpper() + "'";
                //}
                if (District != "")
                {
                    cmdText += " and upper(REGISTRATION_DIST_CD)='" + District.ToUpper() + "'";
                }
                if (VDC != "")
                {
                    cmdText += " and upper(REGISTRATION_VDC_MUN_CD) ='" + VDC.ToUpper() + "'";
                }
                if (Ward != "")
                {
                    cmdText += " and upper(REGISTRATION_WARD_NO) ='" + Ward.ToUpper() + "'";
                }
                if (Name != "")
                {
                    cmdText += " and upper(FULL_NAME_ENG) ='" + Name.ToUpper() + "'";
                }
                if (NissaNo != "")
                {
                    cmdText += " and upper(BENEFICIARY_ID) ='" + NissaNo.ToUpper() + "'";
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
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }
        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_GRIEVANCE_FILE_BATCH WHERE STATUS='Completed'";
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
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }
        public System.Data.DataTable GetDataImportRecordByDistrict(string district, string vdc)
        {
            DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    dtbl = service.GetDataTable(true, "PR_GET_GRIEVANCE_FILE_BATCH",
                         district.ToDecimal(),//DistrictID
                        vdc.ToDecimal(),//VDC
                        //DBNull.Value,//WARD
                        DBNull.Value);
                    // dtbl = service.GetDataTable(cmdTxt, null);

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
    }
}
