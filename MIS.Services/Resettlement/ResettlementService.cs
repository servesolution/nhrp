using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using MIS.Models.ManageResettlement;

namespace MIS.Services.Resettlement
{
    public class ResettlementService
    {

        //save duplicate resettlement
        public bool saveDuplicateData(string Benf,string respondance, string dist,string vdc,  string pa, string fileName, string reason)
        {
            bool result = false;
            QueryResult qr = null;
            string WARD_CD = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    string[] splitPa = pa.ConvertToString().Trim().Split('-');
                    if (splitPa.Length > 0)
                    {
                       
                        if (splitPa.Length == 5 && splitPa[2].ConvertToString() != "")
                        {
                            WARD_CD = splitPa[2].ConvertToString();
                        }



                    }

                    service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                    service.Begin();

                    //SAVE File info
                    qr = service.SubmitChanges("SAVE_RESETTLEMENT_DUPLICATE",
                                               "I",
                                                DBNull.Value,
                                               Benf.ToString(),
                                               respondance.ToString(),
                                               dist.ToDecimal(),
                                               vdc.ToDecimal(),
                                               WARD_CD.ToDecimal(),
                                               pa.ToString(),
                                               reason.ToString(),
                                              SessionCheck.getSessionUsername(),//entered by
                                               DateTime.Now,//entered date
                                               System.DateTime.Now.ConvertToString(),//entered date loc
                                               fileName.ToString());

                }
                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qr != null)
                    {
                        result = qr.IsSuccess;
                    }
                }
            }
            return result;
        }
        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME  FROM NHRS_RESETTLEMENT_FILE_BATCH WHERE STATUS ='Completed'";
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

        #region chek duplicate
        public bool CheckDuplicate(string PaNumber )
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_RESETTLEMENT_BENF where NRA_DEFINED_CD='" + PaNumber.ConvertToString() + "'  ";



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
        #endregion

        #region check if validation
        public bool CheckIfPaNameExist(string PaNumber, string RespondanceName)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            string RESPONDANT_F_NAME = null;
            string RESPONDANT_M_NAME = null;
            string RESPONDANT_L_NAME = null;


            #region respondance name split
            if (RespondanceName.Trim() != "")
            {
                string[] respondanceName = RespondanceName.Split(' ');
                if (respondanceName.Length > 0)
                {
                    if (respondanceName.Length == 1)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                    }
                    else if (respondanceName.Length == 2)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_L_NAME = respondanceName[1].ConvertToString();
                    }
                    else if (respondanceName.Length == 3)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                        RESPONDANT_L_NAME = respondanceName[2].ConvertToString();
                    }
                    else
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                        for (int j = 2; j < respondanceName.Length; j++)
                        {
                            if (RESPONDANT_L_NAME == "" || RESPONDANT_L_NAME == null)
                            {
                                RESPONDANT_L_NAME = respondanceName[j].ConvertToString();
                            }
                            else
                            {
                                RESPONDANT_L_NAME = RESPONDANT_L_NAME + " " + respondanceName[j].ConvertToString();
                            }

                        }

                    }
                }
            }
            #endregion

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_RESETTLEMENT_BENF where NRA_DEFINED_CD='" + PaNumber.ConvertToString() + "'   ";
                    if(RESPONDANT_F_NAME!="" && RESPONDANT_F_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_F_NAME='" + RESPONDANT_F_NAME + "'";
                    } 
                    if(RESPONDANT_M_NAME!="" && RESPONDANT_M_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_M_NAME='" + RESPONDANT_M_NAME + "'";
                    }
                    if(RESPONDANT_L_NAME!="" && RESPONDANT_L_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_L_NAME='" + RESPONDANT_L_NAME + "'";
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


        public bool CheckIfNissaNameExist(string Nisa, string RespondanceName)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            string RESPONDANT_F_NAME = null;
            string RESPONDANT_M_NAME = null;
            string RESPONDANT_L_NAME = null;


            #region respondance name split
            if (RespondanceName.Trim() != "")
            {
                string[] respondanceName = RespondanceName.Split(' ');
                if (respondanceName.Length > 0)
                {
                    if (respondanceName.Length == 1)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                    }
                    else if (respondanceName.Length == 2)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_L_NAME = respondanceName[1].ConvertToString();
                    }
                    else if (respondanceName.Length == 3)
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                        RESPONDANT_L_NAME = respondanceName[2].ConvertToString();
                    }
                    else
                    {
                        RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                        RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                        for (int j = 2; j < respondanceName.Length; j++)
                        {
                            if (RESPONDANT_L_NAME == "" || RESPONDANT_L_NAME == null)
                            {
                                RESPONDANT_L_NAME = respondanceName[j].ConvertToString();
                            }
                            else
                            {
                                RESPONDANT_L_NAME = RESPONDANT_L_NAME + " " + respondanceName[j].ConvertToString();
                            }

                        }

                    }
                }
            }
            #endregion

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();


                cmdText = "SELECT NRA_DEFINED_CD FROM NHRS_RESETTLEMENT_BENF where SLIP_NO='" + Nisa.ConvertToString() + "'   ";
                    if(RESPONDANT_F_NAME!="" && RESPONDANT_F_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_F_NAME='" + RESPONDANT_F_NAME + "'";
                    } 
                    if(RESPONDANT_M_NAME!="" && RESPONDANT_M_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_M_NAME='" + RESPONDANT_M_NAME + "'";
                    }
                    if(RESPONDANT_L_NAME!="" && RESPONDANT_L_NAME!=null)
                    {
                        cmdText=cmdText+" and  RESPONDANT_L_NAME='" + RESPONDANT_L_NAME + "'";
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
        #endregion

        #region import data
        public string ImportResettlement(DataTable paramTable, string fileName, out string ExceptionMessage, string districtCd, string VdcCd)
        {
            bool result = false;
            string batchID = "";
            string exc = string.Empty;
            ExceptionMessage = string.Empty;
            QueryResult qrFIleBatch = null;
            QueryResult qrResetlement = null;
            string PA_NO = "";
            CommonFunction com = new CommonFunction();


         
           

         

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                    service.Begin(); 

                    //SAVE File info
                    qrFIleBatch = service.SubmitChanges("PR_RESETTLEMENT_FILE_BATCH",
                                               "I",
                                                DBNull.Value,
                                                DBNull.Value,
                                                districtCd.ToDecimal(),
                                                VdcCd.ToDecimal(),
                                                "Completed",
                                                fileName,//filename                                                 
                                                DateTime.Now,
                                                SessionCheck.getSessionUsername(),
                                                DBNull.Value);

                    batchID = qrFIleBatch["v_FILE_BATCH_ID"].ConvertToString();



                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {

                        string RESPONDANT_F_NAME = null;
                        string RESPONDANT_M_NAME = null;
                        string RESPONDANT_L_NAME = null;
                        string FATHERS_F_NAME = null;
                        string FATHERS_M_NAME = null;
                        string FATHERS_L_NAME = null;
                        string GRAND_FF_NAME = null;
                        string GRAND_FM_NAME = null;
                        string GRAND_FL_NAME = null;
                        string BENEFICIARY_F_NAME = null;
                        string BENEFICIARY_M_NAME = null;
                        string BENEFICIARY_L_NAME = null;
                        string DISTRICTT_CD = null;
                        string VDC_MUN_CD = null;
                        string WARD_CD = null;


                        PA_NO = paramTable.Rows[i][11].ConvertToString();
                        #region respondance name split
                        if(paramTable.Rows[i][1].ConvertToString().Trim()!="")
                        {
                            string[] respondanceName = paramTable.Rows[i][1].ConvertToString().Split(' ');
                            if(respondanceName.Length>0)
                            {
                                if (respondanceName.Length == 1)
                                {
                                    RESPONDANT_F_NAME = respondanceName[0].ConvertToString(); 
                                }
                                else if(respondanceName.Length==2)
                                {
                                    RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                                    RESPONDANT_L_NAME = respondanceName[1].ConvertToString();
                                }
                                else if (respondanceName.Length == 3)
                                {
                                    RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                                    RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                                    RESPONDANT_L_NAME = respondanceName[2].ConvertToString();
                                }
                                else
                                {
                                    RESPONDANT_F_NAME = respondanceName[0].ConvertToString();
                                    RESPONDANT_M_NAME = respondanceName[1].ConvertToString();
                                    for (int j = 2; j < respondanceName.Length;j++ )
                                    {
                                        if (RESPONDANT_L_NAME == "" || RESPONDANT_L_NAME == null)
                                        {
                                            RESPONDANT_L_NAME =respondanceName[j].ConvertToString() ;
                                        }
                                        else
                                        {
                                            RESPONDANT_L_NAME=RESPONDANT_L_NAME+ " "+ respondanceName[j].ConvertToString();
                                        }
                                        
                                    }
                                        
                                }
                            }
                        }
                        #endregion

                        #region fathers name split

                        if(paramTable.Rows[i][4].ConvertToString().Trim()!="")
                        {
                            string[] name = paramTable.Rows[i][4].ConvertToString().Split(' ');
                            if(name.Length>0)
                            {
                                if (name.Length == 1)
                                {
                                    FATHERS_F_NAME = name[0].ConvertToString(); 
                                }
                                else if(name.Length==2)
                                {
                                    FATHERS_F_NAME = name[0].ConvertToString();
                                    FATHERS_L_NAME = name[1].ConvertToString();
                                }
                                else if (name.Length == 3)
                                {
                                    FATHERS_F_NAME = name[0].ConvertToString();
                                    FATHERS_M_NAME = name[1].ConvertToString();
                                    FATHERS_L_NAME = name[2].ConvertToString();
                                }
                                else
                                {
                                    FATHERS_F_NAME = name[0].ConvertToString();
                                    FATHERS_M_NAME = name[1].ConvertToString();
                                    for (int j = 2; j < name.Length;j++ )
                                    {
                                        if (FATHERS_L_NAME == "" || FATHERS_L_NAME == null)
                                        {
                                            FATHERS_L_NAME =name[j].ConvertToString() ;
                                        }
                                        else
                                        {
                                            FATHERS_L_NAME=FATHERS_L_NAME+ " "+ name[j].ConvertToString();
                                        }
                                        
                                    }
                                        
                                }
                            }
                        }
                        #endregion

                        #region grand father name split 
                          if(paramTable.Rows[i][5].ConvertToString().Trim()!="")
                            {
                                string[] name = paramTable.Rows[i][5].ConvertToString().Split(' ');
                                if(name.Length>0)
                                {
                                    if (name.Length == 1)
                                    {
                                        GRAND_FF_NAME = name[0].ConvertToString(); 
                                    }
                                    else if(name.Length==2)
                                    {
                                        GRAND_FF_NAME = name[0].ConvertToString();
                                        GRAND_FL_NAME = name[1].ConvertToString();
                                    }
                                    else if (name.Length == 3)
                                    {
                                        GRAND_FF_NAME = name[0].ConvertToString();
                                        GRAND_FM_NAME = name[1].ConvertToString();
                                        GRAND_FL_NAME = name[2].ConvertToString();
                                    }
                                    else
                                    {
                                        GRAND_FF_NAME = name[0].ConvertToString();
                                        GRAND_FM_NAME = name[1].ConvertToString();
                                        for (int j = 2; j < name.Length;j++ )
                                        {
                                            if(GRAND_FL_NAME=="" || GRAND_FL_NAME==null)
                                            {
                                                GRAND_FL_NAME =name[j].ConvertToString() ;
                                            }
                                            else
                                            {
                                                GRAND_FL_NAME=GRAND_FL_NAME+ " "+ name[j].ConvertToString();
                                            }
                                        
                                        }
                                        
                                    }
                                }
                            }
                        #endregion

                        #region beneficiary name split

                        if(paramTable.Rows[i][16].ConvertToString().Trim()!="")
                            {
                                string[] name = paramTable.Rows[i][16].ConvertToString().Split(' ');
                                if(name.Length>0)
                                {
                                    if (name.Length == 1)
                                    {
                                        BENEFICIARY_F_NAME = name[0].ConvertToString(); 
                                    }
                                    else if(name.Length==2)
                                    {
                                        BENEFICIARY_F_NAME = name[0].ConvertToString();
                                        BENEFICIARY_L_NAME = name[1].ConvertToString();
                                    }
                                    else if (name.Length == 3)
                                    {
                                        BENEFICIARY_F_NAME = name[0].ConvertToString();
                                        BENEFICIARY_M_NAME = name[1].ConvertToString();
                                        BENEFICIARY_L_NAME = name[2].ConvertToString();
                                    }
                                    else
                                    {
                                        BENEFICIARY_F_NAME = name[0].ConvertToString();
                                        BENEFICIARY_M_NAME = name[1].ConvertToString();
                                        for (int j = 2; j < name.Length;j++ )
                                        {
                                            if (BENEFICIARY_L_NAME == "" || BENEFICIARY_L_NAME == null)
                                            {
                                                BENEFICIARY_L_NAME =name[j].ConvertToString() ;
                                            }
                                            else
                                            {
                                                BENEFICIARY_L_NAME = BENEFICIARY_L_NAME + " " + name[j].ConvertToString();
                                            }
                                        
                                        }
                                        
                                    }
                                }
                            }
                        #endregion 
                        
                        #region check district vdc code
                        if (paramTable.Rows[i][15].ConvertToString().Trim() == "RCB")
                        {
                            string[] splitPa = paramTable.Rows[i][11].ConvertToString().Trim().Split('-');
                            if (splitPa.Length >0 )
                            {
                                if (splitPa.Length == 1 && splitPa[0].ConvertToString()!="")
                                {
                                    DISTRICTT_CD = splitPa[0].ToString();
                                }
                                if (splitPa.Length == 2 && splitPa[1].ConvertToString() != "")
                                {
                                    VDC_MUN_CD = splitPa[1].ConvertToString();
                                }
                                if (splitPa.Length == 3 && splitPa[2].ConvertToString() != "")
                                {
                                    WARD_CD = splitPa[2].ConvertToString();
                                }
                                
                               
                                
                            }
                        }
                        else
                        {
                            if (paramTable.Rows[i][6].ConvertToString().Trim() != "")
                            {
                                DataTable ditctCd = new DataTable();
                                string cmdText = "SELECT KLL_DISTRICT_CD FROM MLD_KLL_DISTRICT_MAP where KLL_DISTRICT_NAME='" + paramTable.Rows[i][6].ConvertToString().Trim() + "'"; 
                                ditctCd = service.GetDataTable(cmdText, null);
                                if(ditctCd.Rows.Count>0)
                                {
                                    DISTRICTT_CD = ditctCd.Rows[0]["KLL_DISTRICT_CD"].ConvertToString();
                                }
                            }
                            
                            if (paramTable.Rows[i][7].ConvertToString().Trim() != "")
                            {
                                DataTable vdcCd = new DataTable();
                                if(DISTRICTT_CD!=null && DISTRICTT_CD!="")
                                {
                                    string cmdText = "SELECT KLL_VDC_CD FROM MLD_KLL_VDC_MAP where KLL_VDC_NAME='" + paramTable.Rows[i][7].ConvertToString().Trim() + "' and KLL_DISTRICT_CD='" + DISTRICTT_CD.ConvertToString() + "'";
                                    vdcCd = service.GetDataTable(cmdText, null);
                                    if (vdcCd.Rows.Count > 0)
                                    {
                                        VDC_MUN_CD = vdcCd.Rows[0]["KLL_VDC_CD"].ConvertToString();
                                        WARD_CD = paramTable.Rows[i][8].ConvertToString();
                                    }
                                }
                                
                            }
                        }
                        #endregion

                        qrResetlement = service.SubmitChanges("PR_RESETTLEMENT_BENF_UPLOAD",
                                               "I",
                                                DBNull.Value,
                                                DBNull.Value,
                                                RESPONDANT_F_NAME .ConvertToString(),
                                                RESPONDANT_M_NAME.ConvertToString(),
                                                RESPONDANT_L_NAME.ConvertToString(),
                                                FATHERS_F_NAME.ConvertToString(),
                                                FATHERS_M_NAME.ConvertToString(),
                                                FATHERS_L_NAME.ConvertToString(),
                                                GRAND_FF_NAME.ConvertToString(),
                                                GRAND_FM_NAME.ConvertToString(),
                                                GRAND_FL_NAME.ConvertToString(),
                                                BENEFICIARY_F_NAME.ConvertToString(),
                                                BENEFICIARY_M_NAME.ConvertToString(),
                                                BENEFICIARY_L_NAME.ConvertToString(), 
                                                paramTable.Rows[i][2].ToDecimal(),//age
                                                paramTable.Rows[i][3].ToDecimal(),//family member count
                                                districtCd.ToDecimal(),
                                                VdcCd.ToDecimal(),
                                                WARD_CD.ToDecimal(),
                                                paramTable.Rows[i][9].ConvertToString(),//tole
                                                paramTable.Rows[i][10].ConvertToString(),//citizenship number
                                                paramTable.Rows[i][11].ConvertToString(),//pa number
                                                paramTable.Rows[i][12].ToDecimal(),//ea
                                                paramTable.Rows[i][13].ToDecimal(),//hh_number
                                                paramTable.Rows[i][14].ToDecimal(),//slip number
                                                paramTable.Rows[i][15].ConvertToString(),//mis review
                                                paramTable.Rows[i][17].ConvertToString(),//phone
                                                paramTable.Rows[i][18].ConvertToString(),//review by engineer
                                                DBNull.Value,//status
                                                DBNull.Value,//approved
                                                DBNull.Value,//approved by
                                                DBNull.Value,//approved date
                                                DBNull.Value,//approved date loc  
                                               SessionCheck.getSessionUsername(),//entered by
                                               DateTime.Now,//entered date
                                               System.DateTime.Now.ConvertToString(),//entered date loc
                                                DBNull.Value,//updated by
                                                DBNull.Value,//updated date
                                                DBNull.Value,//updated date loc 
                                                batchID.ToDecimal()
                                                
                       );








                    }

                }
                catch (OracleException oe)
                {
                    if (batchID != "")
                    {
                        result = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe); 
                        string cmdText = String.Format("delete from NHRS_RESETTLEMENT_BENF where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText); 

                        cmdText = String.Format("update RESETTLEMENT_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        ExceptionMessage = "PA Number: " + PA_NO + "- " + oe.Message;
                    }
                }
                catch (Exception ex)
                {
                    if (batchID != "")
                    {
                        result = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);


                        string cmdText = String.Format("delete from NHRS_RESETTLEMENT_BENF where FILE_BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("update RESETTLEMENT_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                        SaveErrorMessgae(cmdText);
                        ExceptionMessage = "PA Number: " + PA_NO + "- " + ex.Message;
                    }
                }

                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qrFIleBatch != null)
                    {
                        result = qrFIleBatch.IsSuccess;
                    }
                }
                return ExceptionMessage.ConvertToString();
            }
        }
        #endregion

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



        #region manual insert
        public bool saveResettlement(ResettlementModelClass objModel)
        {
            bool result = false;
            QueryResult qrResetlement = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                    service.Begin();

                    if(objModel.Mode=="I")
                    {
                        qrResetlement = service.SubmitChanges("PR_NHRS_RESETTLEMENT_BENF",
                                               "I",
                                                DBNull.Value,
                                                DBNull.Value,
                                                objModel.ResFirstName.ConvertToString(),
                                                objModel.ResMiddleName.ConvertToString(),
                                                objModel.ResLastName.ConvertToString(),
                                                objModel.ResFathersFirstName.ConvertToString(),
                                                objModel.ResFathersMiddleName.ConvertToString(),
                                                objModel.ResFathersLastName.ConvertToString(),
                                                objModel.ResGFathersFirstName.ConvertToString(),
                                                objModel.ResGFathersMiddleName.ConvertToString(),
                                                objModel.ResGFathersLastName.ConvertToString(),
                                                objModel.ResBeneficairyFName.ConvertToString(),
                                                objModel.ResBeneficairyMName.ConvertToString(),
                                                objModel.ResBeneficairyLName.ConvertToString(),
                                                objModel.ResAge.ToDecimal(),//age
                                                objModel.ResFmc.ToDecimal(),//family member count
                                                objModel.ResDistrict.ToDecimal(),
                                                objModel.ResVDCMUN.ToDecimal(),
                                                objModel.ResWard.ToDecimal(),
                                                objModel.ResTole.ConvertToString(),//tole
                                                objModel.ResCtzNo.ConvertToString(),//citizenship number
                                                objModel.ResPaNo.ConvertToString(),//pa number
                                                objModel.ResEa.ToDecimal(),//ea
                                                objModel.ResHhSn.ToDecimal(),//hh_number
                                                objModel.ResSlipNo.ToDecimal(),//slip number
                                                objModel.ResMisReview.ConvertToString(),//mis review
                                                objModel.ResPhone.ConvertToString(),//phone
                                                objModel.ResRemarks.ConvertToString(),//review by engineer
                                                DBNull.Value,//status
                                                DBNull.Value,//approved
                                                DBNull.Value,//approved by
                                                DBNull.Value,//approved date
                                                DBNull.Value,//approved date loc  
                                                SessionCheck.getSessionUsername(),//entered by
                                                DateTime.Now,//entered date
                                                System.DateTime.Now.ConvertToString(),//entered date loc
                                                DBNull.Value,//updated by
                                                DBNull.Value,//updated date
                                                DBNull.Value//updated date loc 


                       ); 
                    }
                    else
                    {
                        qrResetlement = service.SubmitChanges("PR_NHRS_RESETTLEMENT_BENF",
                                                objModel.Mode,
                                                objModel.ResettlementId.ToDecimal(),
                                                DBNull.Value,
                                                objModel.ResFirstName.ConvertToString(),
                                                objModel.ResMiddleName.ConvertToString(),
                                                objModel.ResLastName.ConvertToString(),
                                                objModel.ResFathersFirstName.ConvertToString(),
                                                objModel.ResFathersMiddleName.ConvertToString(),
                                                objModel.ResFathersLastName.ConvertToString(),
                                                objModel.ResGFathersFirstName.ConvertToString(),
                                                objModel.ResGFathersMiddleName.ConvertToString(),
                                                objModel.ResGFathersLastName.ConvertToString(),
                                                objModel.ResBeneficairyFName.ConvertToString(),
                                                objModel.ResBeneficairyMName.ConvertToString(),
                                                objModel.ResBeneficairyLName.ConvertToString(),
                                                objModel.ResAge.ToDecimal(),//age
                                                objModel.ResFmc.ToDecimal(),//family member count
                                                objModel.ResDistrict.ToDecimal(),
                                                objModel.ResVDCMUN.ToDecimal(),
                                                objModel.ResWard.ToDecimal(),
                                                objModel.ResTole.ConvertToString(),//tole
                                                objModel.ResCtzNo.ConvertToString(),//citizenship number
                                                objModel.ResPaNo.ConvertToString(),//pa number
                                                objModel.ResEa.ToDecimal(),//ea
                                                objModel.ResHhSn.ToDecimal(),//hh_number
                                                objModel.ResSlipNo.ToDecimal(),//slip number
                                                objModel.ResMisReview.ConvertToString(),//mis review
                                                objModel.ResPhone.ConvertToString(),//phone
                                                objModel.ResRemarks.ConvertToString(),//review by engineer
                                                DBNull.Value,//status
                                                DBNull.Value,//approved
                                                DBNull.Value,//approved by
                                                DBNull.Value,//approved date
                                                DBNull.Value,//approved date loc  

                                                DBNull.Value,//entered by
                                                DBNull.Value,//entered date
                                                DBNull.Value,//entered date loc 

                                                SessionCheck.getSessionUsername(),//updated by
                                                DateTime.Now,//updated date
                                                System.DateTime.Now.ConvertToString()//updated date loc


                       ); 
                    }

                    
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
                if (qrResetlement != null)
                {
                    result = qrResetlement.IsSuccess;
                }
                return result;
            }
        }
        #endregion

        #region get resettlement data by id
        public ResettlementModelClass  getResettlementDataById(string id)
        {
            DataTable dt = new DataTable();
            string cmdText = null;
            ResettlementModelClass objModel = new ResettlementModelClass();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_RESETTLEMENT_BENF where RESETTLEMENT_ID='"+ id.ConvertToString()+"'"; 
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
                objModel.ResettlementId= dt.Rows[0]["RESETTLEMENT_ID"].ConvertToString();

                objModel.ResFirstName= dt.Rows[0]["RESPONDANT_F_NAME"].ConvertToString(); 
                objModel.ResMiddleName= dt.Rows[0]["RESPONDANT_M_NAME"].ConvertToString(); 
                objModel.ResLastName= dt.Rows[0]["RESPONDANT_L_NAME"].ConvertToString();

                objModel.ResFathersFirstName= dt.Rows[0]["FATHERS_F_NAME"].ConvertToString();
                objModel.ResFathersMiddleName= dt.Rows[0]["FATHERS_M_NAME"].ConvertToString();
                objModel.ResFathersLastName= dt.Rows[0]["FATHERS_L_NAME"].ConvertToString();

                objModel.ResGFathersFirstName= dt.Rows[0]["GRAND_FF_NAME"].ConvertToString();
                objModel.ResGFathersMiddleName= dt.Rows[0]["GRAND_FM_NAME"].ConvertToString();
                objModel.ResGFathersLastName= dt.Rows[0]["GRAND_FL_NAME"].ConvertToString();

                objModel.ResBeneficairyFName= dt.Rows[0]["BENEFICIARY_F_NAME"].ConvertToString();
                objModel.ResBeneficairyMName= dt.Rows[0]["BENEFICIARY_M_NAME"].ConvertToString();
                objModel.ResBeneficairyLName= dt.Rows[0]["BENEFICIARY_L_NAME"].ConvertToString();


                objModel.ResAge= dt.Rows[0]["AGE"].ConvertToString();
                objModel.ResFmc= dt.Rows[0]["FAMILY_MEMBER_CNT"].ConvertToString();
                objModel.ResDistrict= dt.Rows[0]["DISTRICT_CD"].ConvertToString();
                objModel.ResVDCMUN= dt.Rows[0]["VDC_MUN_CD"].ConvertToString();

                objModel.ResWard= dt.Rows[0]["WARD"].ConvertToString();
                objModel.ResTole= dt.Rows[0]["TOLE"].ConvertToString();
                objModel.ResCtzNo= dt.Rows[0]["CITIZENSHIP_NO"].ConvertToString();
                objModel.ResPaNo= dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();

                objModel.ResEa= dt.Rows[0]["ENUMERATION_AREA"].ConvertToString();
                objModel.ResHhSn= dt.Rows[0]["HH_SN"].ConvertToString();
                objModel.ResSlipNo= dt.Rows[0]["SLIP_NO"].ConvertToString();
                objModel.ResMisReview= dt.Rows[0]["MIS_REVIEW"].ConvertToString();

                objModel.ResPhone= dt.Rows[0]["PHONE_NUMBER"].ConvertToString();
                objModel.ResRemarks = dt.Rows[0]["ENGINEERS_REMARKS"].ConvertToString();
 
            }
            return objModel;
        }
        #endregion

        #region get all resettlement data
        public DataTable GetAllResettlement(ResettlementModelClass objModel)
        {
            DataTable dt = new DataTable();
            using(ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                service.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                dt = service.GetDataTable(true,"GET_RESETTLEMENT_SEARCH",
                    objModel.ResDistrict.ConvertToString(),
                    objModel.ResVDCMUN.ConvertToString(),
                    objModel.ResWard.ConvertToString(),
                    objModel.ResPaNo.ConvertToString(),
                    objModel.ResMisReview.ConvertToString(),
                    DBNull.Value,
                    DBNull.Value
                    );

            }
            return dt;

        }
        #endregion


        #region get uploaded data
        public DataTable GetUploaded(string distCd, string VdcCd)
        {
            DataTable dt = new DataTable();
            string cmd = "select FILENAME,STATUS,md.desc_eng as district_eng,md.desc_loc as district_loc,mvm.desc_eng as vdc_eng, mvm.desc_loc as vdc_loc,nrfb.ENTERED_DATE  "
            +"from NHRS_RESETTLEMENT_FILE_BATCH nrfb "
            + " left outer join mis_district md on nrfb.DISCTRICT_CD = md.district_cd "
            + " left outer join mis_vdc_municipality mvm on nrfb.VDC_MUN_CD = mvm.VDC_MUN_CD  "
            +"where 1=1  ";
            if(distCd!=null && distCd!="")
            {
                cmd = cmd + "and nrfb.DISCTRICT_CD='" + distCd + "'";
            }
            if (VdcCd != null && VdcCd != "")
            {
                cmd = cmd + "and nrfb.VDC_MUN_CD='" + VdcCd + "'";
            }
            using(ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                   
                    dt = sf.GetDataTable(cmd, null);
                }
                catch(OracleException oe)
                {

                }
                catch(Exception ex)
                {

                }
                
            }
            return dt;
        }
        #endregion


        #region resettlement duplicate data(get)
        public DataTable GetDuplicate(string fileName)
        {
            DataTable dt = new DataTable();
            string cmd = "select *from NHRS_RESETTLEMENT_DUPLICATE "
             
            + "where 1=1  ";
            if (fileName != null && fileName != "")
            {
                cmd = cmd + "and FILE_NAME='" + fileName + "'";
            }
           
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {

                    dt = sf.GetDataTable(cmd, null);
                }
                catch (OracleException oe)
                {

                }
                catch (Exception ex)
                {

                }

            }
            return dt;
        }
        #endregion
    }
}
