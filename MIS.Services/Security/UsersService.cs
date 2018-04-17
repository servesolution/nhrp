using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using System.Data;
using MIS.Models.Security;
using System.Web;
using MIS.Services.Core;
using System.Data.OracleClient;
using System.Data.Common;
using ExceptionHandler;
using System.Security.Cryptography;
using MIS.Models.Core;
using System.Net.Mail;
namespace MIS.Services.Security
{
    public class UsersService
    {
        #region Insert/Update/Delete User Information
        public static string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["STMPServer"].ToString();
        public static string adminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"].ToString();
        public bool UserUID(Users objUsers, Group objGroup, string mode)
        {
            QueryResult qResult = null;
            ComWebUsrInfo obj = new ComWebUsrInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
               
                service.PackageName = "PKG_COM_WEB_SECURITY";
                obj.UsrCd = objUsers.usrCd.ConvertToString().Trim();
                obj.UsrName = objUsers.usrName.ConvertToString().Trim();
                obj.Password = objUsers.password.ConvertToString().Trim();
                obj.EmpCd = objUsers.empCd.ConvertToString().Trim();
                obj.Status = objUsers.status.ConvertToString().Trim();
                //obj.ExpiryDt = objUsers.expDay + "-" + objUsers.expMonth + "-" + objUsers.expYear;
                obj.ExpiryDt = objUsers.expiryDt.ToString("dd-MM-yyyy");
                //obj.ExpiryDtLoc = objUsers.expiryDtLoc.ToString();
                obj.ExpiryDtLoc = objUsers.expiryDtLoc.ConvertToString();
                obj.EnteredBy = strUserName.Trim();//objUsers.enteredBy.ConvertToString().Trim();
                obj.LastUpdatedBy = objUsers.enteredBy.ConvertToString().Trim();
                obj.Approved = objUsers.approved;
                obj.ApprovedBy = objUsers.enteredBy.ConvertToString().Trim();
                obj.ApprovedDt = DateTime.Now.ToString("dd-MMM-yyyy");
                obj.Email = objUsers.email.ConvertToString().Trim();
                obj.Mobile_No = objUsers.mobilenumber.ConvertToString().Trim();
                obj.Verification_Required = objUsers.VerificationRequired.ConvertToString().Trim();
                

                if (obj.Verification_Required.ConvertToString() == "")
                {
                    obj.Verification_Required = "N";
                }
                obj.IPAddress = CommonVariables.IPAddress;
                obj.Mode = mode;
                if(objUsers.isbankUser==true)
                {
                    obj.Is_Bank_User = "Y";
                }
                else
                {
                    obj.Is_Bank_User = "N";
                }

                obj.Bank_CD = objUsers.bankCode.ConvertToString();

                if (objUsers.isDonor == true)
                {
                    obj.IsDonor = "Y";
                }
                else
                {
                    obj.IsDonor = "N";
                }
                //obj.Is_Bank_User = objUsers.isbankUser.ConvertToString();
                obj.DONOR_CD = objUsers.donor_cd.ConvertToString();

              
                
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(obj, true);
                    if (qResult.IsSuccess == true && obj.Mode == "I")
                    {
                        string toName = "";
                        toName = obj.UsrName;                       
                        EmailMessage emMessage = new EmailMessage();


                        emMessage.From = "";
                        emMessage.To = obj.Email;
                        emMessage.Subject = "User Created";
                        emMessage.Body = "Dear " + toName + ",<br> Your user is created as username: " + obj.Email + " password: " + Utils.DecryptString(obj.Password.Trim()) + ". Your account is still not verified please contact administrator for account verification.<br><br> Thank You";
                        Core.MailSend.SendMail(emMessage);

                        ComWebUsrGrpInfo objGrp = new ComWebUsrGrpInfo();
                        objGrp.UsrCd = qResult["V_USR_CD"].ConvertToString();
                        
                            objGrp.GrpCd = objUsers.GroupCode;//By default we set the user to general group
                        
                        objGrp.EnteredBy = strUserName.Trim();
                        objGrp.LastUpdatedBy = objGroup.EnterBy.ConvertToString().Trim();
                        objGrp.Ipaddress = CommonVariables.IPAddress;
                        objGrp.Mode = "I";
                        QueryResult qrGrp = service.SubmitChanges(objGrp, true);
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

            }
            return qResult.IsSuccess;
        }
        public static bool SendMail(EmailMessage objMessage)
        {
            bool chkSend = false;
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer1 = new SmtpClient(SmtpServer);
            try
            {
                msg.From = new MailAddress(adminEmail);
                msg.To.Add(new MailAddress(objMessage.To));
                msg.Priority = MailPriority.Normal;
                msg.IsBodyHtml = true;
                msg.Subject = objMessage.Subject;
                msg.Body = objMessage.Body;
                if (!string.IsNullOrEmpty(objMessage.CC))
                    msg.CC.Add(objMessage.CC);
                if (!string.IsNullOrEmpty(objMessage.BCC))
                    msg.Bcc.Add(objMessage.BCC);
                if (!string.IsNullOrEmpty(objMessage.Attachment))
                    msg.Attachments.Add(new Attachment(objMessage.Attachment));

                SmtpServer1.Port = 587;
                SmtpServer1.Credentials = new System.Net.NetworkCredential("secureserveithome@gmail.com", "SSITH@2016");
                SmtpServer1.EnableSsl = true;

                SmtpServer1.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return chkSend;

        }
        public static void SendEmail(MailMessage message)
        {
            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = SmtpServer;
            mailClient.Send(message);
        }

        #endregion

        #region Fill tables of User Information
        public DataTable FillUser()
        {

            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
               
                string cmdText = @"select COM_WEB_USR.USR_CD, COM_WEB_USR.EMP_CD, COM_WEB_USR.USR_NAME,COM_WEB_USR.EMAIL, COM_WEB_GRP.GRP_NAME, 
                                    COM_WEB_USR.EXPIRY_DT, COM_WEB_USR.STATUS, COM_WEB_USR.APPROVED FROM COM_WEB_USR LEFT OUTER JOIN COM_WEB_USR_GRP ON 
                                    COM_WEB_USR.USR_CD=COM_WEB_USR_GRP.USR_CD INNER JOIN COM_WEB_GRP ON 
                                    COM_WEB_USR_GRP.GRP_CD=COM_WEB_GRP.GRP_CD order by COM_WEB_USR.USR_NAME ASC";
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
                catch (Exception)
                {
                    dtbl = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

                return dtbl;
            }
        }

        public DataTable SearchUsers(string initialStr, string email, string userName, string groupCode, string orderby, string order)
        {
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {


                string cmdText = @"select COM_WEB_USR.USR_CD, COM_EMPLOYEE.DEF_EMPLOYEE_CD, COM_WEB_USR.USR_NAME,COM_WEB_USR.EMAIL, COM_WEB_GRP.GRP_NAME, COM_WEB_USR.EXPIRY_DT, 
                                 COM_WEB_USR.STATUS,COM_WEB_USR.APPROVED FROM COM_WEB_USR LEFT JOIN COM_WEB_USR_GRP ON COM_WEB_USR.USR_CD=COM_WEB_USR_GRP.USR_CD LEFT OUTER JOIN 
                                 COM_WEB_GRP ON COM_WEB_USR_GRP.GRP_CD=COM_WEB_GRP.GRP_CD LEFT OUTER JOIN COM_EMPLOYEE ON COM_WEB_USR.EMP_CD=COM_EMPLOYEE.EMPLOYEE_CD WHERE 1=1 and COM_WEB_USR.STATUS='E'";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" AND Upper(COM_WEB_USR.USR_NAME) like '{0}%'", initialStr);
                }
                if (groupCode != null && groupCode != "")
                {
                    cmdText += " AND COM_WEB_USR_GRP.GRP_CD = '" + groupCode.ToUpper() + "'";
                }
                if (userName != "")
                {
                    cmdText += String.Format(" AND UPPER(COM_WEB_USR.USR_NAME) LIKE '%" + userName.ToUpper() + "%'");
                }
                if (email != "")
                {
                    cmdText += String.Format(" AND COM_WEB_USR.EMAIL LIKE '%" + email + "%'");
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "EXPIRY_DT")
                    {
                        cmdText += String.Format(" order by  ({0}) {1}", orderby, order);
                    }
                    else if (orderby.ToUpper() == "USR_CD")
                    {
                        cmdText += String.Format(" order by TO_NUMBER(COM_WEB_USR.USR_CD) " + order + "");
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
                }
                else
                {
                    cmdText += String.Format(" order by TO_NUMBER(COM_WEB_USR.USR_CD) " + order + "");
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
                catch (Exception)
                {
                    dtbl = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                return dtbl;
            }
        }

        public DataTable ManageUsersList(string id, string vdcMunCode, string wardCode, string officeCode, string positionCode, string definedCode, string fullName)
        {
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                
                string cmdText = "";
                string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                if (sessionLanguage == "English")
                {
                    cmdText = @"SELECT E.FIRST_NAME_ENG FIRST_NAME,E.MIDDLE_NAME_ENG MIDDLE_NAME,E.LAST_NAME_ENG LAST_NAME,E.FIRST_NAME_LOC,E.MIDDLE_NAME_LOC,E.LAST_NAME_LOC,E.EMPLOYEE_CD,E.DEF_EMPLOYEE_CD,E.FULL_NAME_ENG FULL_NAME,E.FULL_NAME_LOC FULL_NAME_NEP,D.DESC_ENG DISTNAME,V.DESC_ENG VDCNAME,E.PER_WARD_NO,P.DESC_ENG POSITIONNAME,S.DESC_ENG POSSUBCLASSNAME,
                                DES.DESC_ENG DESIGNATIONNAME FROM COM_EMPLOYEE E LEFT OUTER JOIN MIS_DISTRICT D ON E.PER_DISTRICT_CD=D.DISTRICT_CD LEFT OUTER JOIN
                                MIS_VDC_MUNICIPALITY V ON E.PER_VDC_MUN_CD=V.VDC_MUN_CD LEFT OUTER JOIN MIS_POSITION P ON E.POSITION_CD=P.POSITION_CD 
                                LEFT OUTER JOIN MIS_POSITION_SUB_CLASS_MST S ON E.POS_SUB_CLASS_CD=S.POS_SUB_CLASS_CD LEFT OUTER JOIN MIS_DESIGNATION DES
                                ON E.DESIGNATION_CD=DES.DESIGNATION_CD WHERE E.EMPLOYEE_CD NOT IN (SELECT EMP_CD FROM COM_WEB_USR where EMP_CD is not null)";
                    if (fullName != "" && fullName != "null" && fullName != null)
                    {
                        cmdText += String.Format(" AND Upper(E.FULL_NAME_ENG) LIKE '" + fullName.ToUpper() + "%'");
                    }
                }
                else
                {
                    cmdText = @"SELECT E.FIRST_NAME_ENG FIRST_NAME,E.MIDDLE_NAME_ENG MIDDLE_NAME,E.LAST_NAME_ENG LAST_NAME,E.FIRST_NAME_LOC,E.MIDDLE_NAME_LOC,E.LAST_NAME_LOC,E.EMPLOYEE_CD,E.FULL_NAME_ENG FULL_NAME,E.DEF_EMPLOYEE_CD,E.FULL_NAME_LOC FULL_NAME_NEP,D.DESC_LOC DISTNAME,V.DESC_LOC VDCNAME,E.PER_WARD_NO,P.DESC_LOC POSITIONNAME,S.DESC_LOC POSSUBCLASSNAME,
                                    DES.DESC_LOC DESIGNATIONNAME FROM COM_EMPLOYEE E LEFT OUTER JOIN MIS_DISTRICT D ON E.PER_DISTRICT_CD=D.DISTRICT_CD LEFT OUTER JOIN
                                    MIS_VDC_MUNICIPALITY V ON E.PER_VDC_MUN_CD=V.VDC_MUN_CD LEFT OUTER JOIN MIS_POSITION P ON E.POSITION_CD=P.POSITION_CD 
                                    LEFT OUTER JOIN MIS_POSITION_SUB_CLASS_MST S ON E.POS_SUB_CLASS_CD=S.POS_SUB_CLASS_CD LEFT OUTER JOIN MIS_DESIGNATION DES
                                    ON E.DESIGNATION_CD=DES.DESIGNATION_CD WHERE E.EMPLOYEE_CD NOT IN(SELECT EMP_CD FROM COM_WEB_USR where EMP_CD is not null)";
                    if (fullName != "" && fullName != "null" && fullName != null)
                    {
                        cmdText += String.Format(" AND (E.FULL_NAME_LOC) LIKE ('" + fullName + "%')");
                    }
                }
                if (id != "" && id != null)
                {
                    cmdText += String.Format(" AND E.PER_DISTRICT_CD='" + id + "'");
                }
                if (vdcMunCode != "" && vdcMunCode != null)
                {
                    cmdText += String.Format(" AND E.PER_VDC_MUN_CD='" + vdcMunCode + "'");
                }
                if (wardCode != "" && wardCode != null && wardCode != "null" && wardCode != "0")
                {
                    cmdText += String.Format(" AND E.PER_WARD_NO='" + wardCode + "'");
                }
                if (officeCode != "" && officeCode != "null" && officeCode != null)
                {
                    cmdText += String.Format(" AND E.OFFICE_CD='" + officeCode + "'");
                }
                if (positionCode != "" && positionCode != "null" && positionCode != null)
                {
                    cmdText += String.Format(" AND E.POSITION_CD='" + positionCode + "'");
                }
                if (definedCode != "" && definedCode != "null" && definedCode != null)
                {
                    cmdText += String.Format(" AND E.DEF_EMPLOYEE_CD='" + definedCode.ToUpper() + "'");
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
                catch (Exception)
                {
                    dtbl = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

                return dtbl;
            }
        }

        public Users getUserbyEmail(string userEmail)
        {
            Users obj = null;
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    
                    string cmdText = String.Format("select * from com_web_usr where lower(EMAIL)=:usr");
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(cmdText, new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = userEmail.ToLower().Trim(), ParameterName="usr"}                                                       
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    obj = new Users();
                    obj.empCd = dtbl.Rows[0]["EMP_CD"].ConvertToString();
                    obj.usrCd = dtbl.Rows[0]["USR_CD"].ConvertToString();
                    obj.usrName = dtbl.Rows[0]["USR_NAME"].ConvertToString();
                    obj.password = dtbl.Rows[0]["PASSWORD"].ConvertToString();
                    obj.email = dtbl.Rows[0]["EMAIL"].ConvertToString();
                    string expDate = String.Format("{0:dd/MM/yyyy}", dtbl.Rows[0]["EXPIRY_DT"]);
                    if (expDate != "")
                    {
                        string[] arr = expDate.Split('/');
                        obj.expYear = Convert.ToDecimal((arr[2].ConvertToString()) == "" ? "0" : arr[2].ConvertToString());
                        obj.expMonth = Convert.ToDecimal((arr[1].ConvertToString()) == "" ? "0" : arr[1].ConvertToString());
                        obj.expDay = Convert.ToDecimal((arr[0].ConvertToString()) == "" ? "0" : arr[0].ConvertToString());
                    }
                    obj.enteredBy = dtbl.Rows[0]["ENTERED_BY"].ConvertToString();
                    obj.enteredDt = DateTime.Parse(dtbl.Rows[0]["ENTERED_DT"].ConvertToString());
                    obj.status = dtbl.Rows[0]["STATUS"].ConvertToString();
                    obj.approved = dtbl.Rows[0]["APPROVED"].ToBooleanYN();
                    obj.approvedDt = DateTime.Parse(dtbl.Rows[0]["APPROVED_DT"].ConvertToString());
                    obj.lastUpdatedBy = dtbl.Rows[0]["LAST_UPDATED_BY"].ConvertToString();
                    obj.lastUpdatedDt = DateTime.Parse(dtbl.Rows[0]["LAST_UPDATED_DT"].ConvertToString());
                    obj.internalUsrFlag = dtbl.Rows[0]["INTERNAL_USR_FLAG"].ToBooleanYN();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }
        #endregion

        #region Populate Data For Updates
        public Users PopulateUserDetails(string UserCode)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {


                    string cmdText = String.Format("SELECT T2.DEF_EMPLOYEE_CD,cwug.grp_cd,T1.* "+
                    ",NBU.BRANCH_STD_CD AS BRANCH_CD FROM COM_WEB_USR T1 " +
                    "LEFT OUTER JOIN COM_EMPLOYEE T2 ON T1.EMP_CD=T2.EMPLOYEE_CD "+
                    "left outer join com_web_usr_grp cwug on cwug.usr_cd = T1.usr_cd  "+
                    "LEFT OUTER JOIN nhrs_bank_users NBU ON NBU.USR_CD = T1.USR_CD "+
                    "WHERE T1.USR_CD='" + UserCode + "'");
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(cmdText, null);
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    string expDate = String.Format("{0:dd/MM/yyyy}", dtbl.Rows[0]["EXPIRY_DT"]);
                   
                    obj.expiryDt = Convert.ToDateTime(dtbl.Rows[0]["EXPIRY_DT"]);
                    obj.expiryDtLoc = dtbl.Rows[0]["EXPIRY_DT_LOC"].ConvertToString();
                    obj.empCd = dtbl.Rows[0]["EMP_CD"].ToString();
                    obj.definedCode = dtbl.Rows[0]["DEF_EMPLOYEE_CD"].ConvertToString();
                    obj.usrCd = dtbl.Rows[0]["USR_CD"].ToString();
                    obj.usrName = dtbl.Rows[0]["USR_NAME"].ToString();
                    obj.password = dtbl.Rows[0]["PASSWORD"].ToString();
                    obj.email = dtbl.Rows[0]["EMAIL"].ToString();
                    obj.mobilenumber = dtbl.Rows[0]["MOBILE_NUMBER"].ConvertToString();
                    obj.VerificationRequired = dtbl.Rows[0]["VERIFICATION_REQUIRED"].ConvertToString();
                    obj.GroupCode = dtbl.Rows[0]["GRP_CD"].ConvertToString();
                    obj.bankCode = dtbl.Rows[0]["BANK_CD"].ConvertToString();
                    obj.donor_cd = dtbl.Rows[0]["DONOR_CD"].ConvertToString();
                    obj.branchStdCode = dtbl.Rows[0]["BRANCH_CD"].ConvertToString();

                    if (dtbl.Rows[0]["APPROVED"].ToString() == "Y")
                    {
                        obj.approved = true;
                    }
                    else
                    {
                        obj.approved = false;
                    }
                    obj.status = dtbl.Rows[0]["STATUS"].ToString();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }
        #endregion

        #region Check for Uniqueness while inserting and updating
        public bool CheckDuplicateUsers(string usrCode, string empCode)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
               
                cmdText = @"select USR_CD FROM COM_WEB_USR WHERE USR_CD='" + usrCode + "'";
                if (empCode != "" && empCode != null)
                {
                    cmdText += " OR EMP_CD='" + empCode.ToUpper() + "'";
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
                catch (Exception)
                {

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

        public bool CheckDuplicateEmail(string email, string userCode)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = @"select USR_CD FROM COM_WEB_USR WHERE EMAIL='" + email.Trim() + "'";
                if (userCode != "" && userCode != null)
                {
                    cmdText += " AND USR_CD<>'" + userCode.ToUpper() + "'";
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
                catch (Exception)
                {

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
        #endregion

        #region Fill DropDownLists
        public List<Users> GetDistrictsbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<Users> lstDistricts = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select DISTRICT_CD, DESC_ENG DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    }
                    else
                    {
                        cmdText = "select DISTRICT_CD, DESC_LOC DESCRIPTION FROM MIS_DISTRICT where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and DISTRICT_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and lower(DESC_LOC) like lower('%" + desc + "%')";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstDistricts.Add(new Users { distCode = Convert.ToInt32(drow["DISTRICT_CD"]), distName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception)
            {
                lstDistricts = null;
            }
            return lstDistricts;
        }

        public List<Users> GetOfficebyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<Users> lstOffice = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select OFFICE_CD, DESC_ENG DESCRIPTION FROM MIS_OFFICE where 1=1";
                    }
                    else
                    {
                        cmdText = "select OFFICE_CD, DESC_LOC DESCRIPTION FROM MIS_OFFICE where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and OFFICE_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(DESC_LOC) like '%" + desc.ToUpper() + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstOffice.Add(new Users { officeCode = (drow["OFFICE_CD"]).ToString(), officeName = drow["DESCRIPTION"].ToString() });
                }

            }
            catch (Exception)
            {
                lstOffice = new List<Users>();
            }
            return lstOffice;
        }

        public List<Users> GetPositionbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<Users> lstPosition = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select POSITION_CD, DESC_ENG DESCRIPTION FROM MIS_POSITION where 1=1";
                    }
                    else
                    {
                        cmdText = "select POSITION_CD, DESC_LOC DESCRIPTION FROM MIS_POSITION where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and POSITION_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(DESC_LOC) like '%" + desc.ToUpper() + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstPosition.Add(new Users { positionCode = (drow["POSITION_CD"]).ToString(), positionName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception)
            {
                lstPosition = null;
            }
            return lstPosition;
        }

        public List<Users> GetPositionSubClassbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            List<Users> lstPositionSubClass = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select POS_SUB_CLASS_CD, DESC_ENG DESCRIPTION FROM MIS_POSITION_SUB_CLASS_MST where 1=1";
                    }
                    else
                    {
                        cmdText = "select POS_SUB_CLASS_CD, DESC_LOC DESCRIPTION FROM MIS_POSITION_SUB_CLASS_MST where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and POS_SUB_CLASS_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(DESC_LOC) like '%" + desc.ToUpper() + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstPositionSubClass.Add(new Users { positionSubClassCode = (drow["POS_SUB_CLASS_CD"]).ToString(), positionSubClassName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception)
            {
                lstPositionSubClass = new List<Users>(); ;
            }
            return lstPositionSubClass;
        }

        public List<Users> GetRegStatebyCodeandDescForZoneCode(string code, string desc, string zoneid)
        {
            DataTable dtbl = null;
            List<Users> lstRegionState = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = "";
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select R.DEFINED_CD, R.DESC_ENG DESCRIPTION FROM MIS_REGION_STATE R INNER JOIN MIS_ZONE Z ON R.REG_ST_CD=Z.REG_ST_CD WHERE Z.ZONE_CD='" + Convert.ToInt32(zoneid) + "'";
                    }
                    else
                    {
                        cmdText = "select R.DEFINED_CD, R.DESC_LOC DESCRIPTION FROM MIS_REGION_STATE R INNER JOIN MIS_ZONE Z ON R.REG_ST_CD=Z.REG_ST_CD WHERE Z.ZONE_CD='" + Convert.ToInt32(zoneid) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and R.DEFINED_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(R.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(R.DESC_LOC) like '%" + desc + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstRegionState.Add(new Users { regStateCode = Convert.ToInt32(drow["DEFINED_CD"]), regStateName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception)
            {
                lstRegionState = null;
            }
            return lstRegionState;
        }

        public List<Users> GetZonebyCodeandDescForDistCode(string code, string desc, string districtid)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            List<Users> lstZone = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select Z.DEFINED_CD, Z.DESC_ENG DESCRIPTION FROM MIS_ZONE Z INNER JOIN MIS_DISTRICT D ON Z.ZONE_CD=D.ZONE_CD WHERE D.DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    else
                    {
                        cmdText = "select Z.DEFINED_CD, Z.DESC_LOC DESCRIPTION FROM MIS_ZONE Z INNER JOIN MIS_DISTRICT D ON Z.ZONE_CD=D.ZONE_CD WHERE D.DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    }
                    if (code != "")
                    {
                        cmdText += " and Z.DEFINED_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(Z.DESC_ENG) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(Z.DESC_LOC) like '%" + desc.ToUpper() + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstZone.Add(new Users { zoneCode = Convert.ToInt32(drow["DEFINED_CD"]), zoneName = drow["DESCRIPTION"].ToString() });
                }
            }
            catch (Exception)
            {
                lstZone = null;
            }
            return lstZone;
        }

        public List<Users> GetVDCMunbyCodeandDescForDistCode(string code, string desc, string districtid)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            List<Users> lstVDCMun = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select VDC_MUN_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM MIS_VDC_MUNICIPALITY WHERE DISTRICT_CD='" + Convert.ToInt32(districtid) + "'";
                    if (code != "")
                    {
                        cmdText += " and VDC_MUN_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and " + Utils.ToggleLanguage("UPPER(DESC_ENG)", "DESC_LOC") + " like '%" + Utils.ToggleLanguage(desc.ToUpper(), desc) + "%'";
                    }
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
                    catch (Exception)
                    {

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
                    lstVDCMun.Add(new Users { vdcMunCode = Convert.ToInt32(drow["VDC_MUN_CD"]), vdcMunName = drow["DESCRIPTION"].ToString() });
                }

            }
            catch (Exception)
            {
                lstVDCMun = null;
            }
            return lstVDCMun;
        }

        public List<Users> GetWardbyCodeandDescForVDCMunCode(string code, string desc, string vdcid)
        {
            DataTable dtbl = null;
            List<Users> lstWard = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "select WARD_NO FROM MIS_WARD WHERE VDC_MUN_CD='" + Convert.ToInt32(vdcid) + "'";
                    if (code != "")
                    {
                        cmdText += " and WARD_NO like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        cmdText += " and WARD_NO like '%" + desc + "%'";
                    }
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
                    catch (Exception)
                    {

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
                    lstWard.Add(new Users { wardCode = Convert.ToInt32(drow["WARD_NO"]) });
                }
            }
            catch (Exception)
            {
                lstWard = null;
            }
            return lstWard;
        }

        public List<Users> GetGroupbyCodeandDesc(string code, string desc)
        {
            DataTable dtbl = null;
            string cmdText = string.Empty;
            List<Users> lstGroup = new List<Users>();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ToString();
                    if (sessionLanguage == "English")
                    {
                        cmdText = "select GRP_CD, GRP_NAME DESCRIPTION FROM COM_WEB_GRP where 1=1";
                    }
                    else
                    {
                        cmdText = "select GRP_CD, GRP_NAME DESCRIPTION FROM COM_WEB_GRP where 1=1";
                    }
                    if (code != "")
                    {
                        cmdText += " and GRP_CD like '%" + code + "%'";
                    }
                    if (desc != "")
                    {
                        if (sessionLanguage == "English")
                        {
                            cmdText += " and upper(GRP_NAME) like '%" + desc.ToUpper() + "%'";
                        }
                        else
                        {
                            cmdText += " and upper(GRP_NAME) like '%" + desc.ToUpper() + "%'";
                        }
                    }
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
                    catch (Exception)
                    {

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
                    lstGroup.Add(new Users { GroupCode = (drow["GRP_CD"]).ToString(), GroupName = drow["DESCRIPTION"].ToString() });
                }

            }
            catch (Exception)
            {
                lstGroup = new List<Users>();
            }
            return lstGroup;
        }
        #endregion

        #region Validate User Login
        public Users validateUserLogin(string Usercode, string Password)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string aa = Utils.EncryptString(Password);
                    string cmdText = String.Format("select CWU.*,CWUG.GRP_CD,CWG.GRP_NAME from COM_WEB_USR CWU INNER JOIN COM_WEB_USR_GRP CWUG ON CWUG.USR_CD=CWU.USR_CD INNER JOIN COM_WEB_GRP CWG on CWG.GRP_CD=CWUG.GRP_CD  where lower(EMAIL)=:usr and PASSWORD=:pwd and CWU.STATUS='E' and CWU.APPROVED='Y'");
                    cmdText += String.Format(" and to_date(EXPIRY_DT) >=TO_DATE(to_date('{0}','DD-MONTH-RRRR'),'DD/MM/RRRR')", System.DateTime.Now.ToString("dd-MMMM-yyyy"));
                    try
                    {
                        dtbl = service.GetDataTable(cmdText,
                            new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = Usercode.ToLower().Trim(), ParameterName="usr"},
                                new OracleParameter { DbType = DbType.String, Value = Utils.EncryptString(Password) ,   ParameterName="pwd"}
                        
                        }
                        );

                       
                    }

                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                     if (dtbl.Rows.Count > 0)
                    {
                         
                        obj.empCd = dtbl.Rows[0]["EMP_CD"].ConvertToString();
                        obj.usrCd = dtbl.Rows[0]["USR_CD"].ConvertToString();
                        obj.usrName = dtbl.Rows[0]["USR_NAME"].ConvertToString();
                        obj.password = dtbl.Rows[0]["PASSWORD"].ConvertToString();
                        obj.email = dtbl.Rows[0]["EMAIL"].ConvertToString();
                        obj.VerificationRequired = dtbl.Rows[0]["VERIFICATION_REQUIRED"].ConvertToString();
                        obj.verificationcode = dtbl.Rows[0]["VERIFICATION_CODE"].ConvertToString();
                        obj.verificationvalidity = (dtbl.Rows[0]["VERIFICATION_VALIDITY"]).ConvertToString("dd-MMM-yy");
                        obj.GroupCode = dtbl.Rows[0]["GRP_CD"].ConvertToString();
                        obj.GroupName = dtbl.Rows[0]["GRP_NAME"].ConvertToString();
                        //obj.verificationcode = Convert.ToBase64String(Encoding.UTF8.GetBytes(obj.verificationcode));
                    }
                 
            }


            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
        
            return obj;
       
    }


        
        #endregion

        #region GetUserDetail for SMS
        public Users GetUserDetail(string Usercode, string Password)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("select * from com_web_usr where lower(EMAIL)=:usr and PASSWORD=:pwd and STATUS='E' and APPROVED='Y'");
                    //cmdText += String.Format(" and to_date(EXPIRY_DT) >=TO_DATE(to_date('{0}','DD-MONTH-RRRR'),'DD/MM/RRRR')", System.DateTime.Now.ToString("dd-MMMM-yyyy"));
                    try
                    {
                        dtbl = service.GetDataTable(cmdText,
                            new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = Usercode.ToLower().Trim(), ParameterName="usr"},
                                new OracleParameter { DbType = DbType.String, Value = Utils.EncryptString(Password) ,   ParameterName="pwd"}
                        
                        }
                        );
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                   
                    obj.usrCd = dtbl.Rows[0]["USR_CD"].ConvertToString();
                    obj.usrName = dtbl.Rows[0]["USR_NAME"].ConvertToString();
                    obj.password = dtbl.Rows[0]["PASSWORD"].ConvertToString();
                    obj.empCd = dtbl.Rows[0]["EMP_CD"].ConvertToString();
                    obj.status = dtbl.Rows[0]["STATUS"].ConvertToString();
                    obj.expiryDt = Convert.ToDateTime(dtbl.Rows[0]["EXPIRY_DT"]);
                    obj.internalUsrFlag = dtbl.Rows[0]["INTERNAL_USR_FLAG"].ToBoolean();
                    obj.enteredBy = dtbl.Rows[0]["ENTERED_BY"].ConvertToString();
                    obj.enteredDt = Convert.ToDateTime(dtbl.Rows[0]["ENTERED_DT"]);
                    obj.lastUpdatedBy = dtbl.Rows[0]["LAST_UPDATED_BY"].ConvertToString();
                    obj.lastUpdatedDt = Convert.ToDateTime(dtbl.Rows[0]["LAST_UPDATED_DT"]);
                    obj.email = dtbl.Rows[0]["EMAIL"].ConvertToString();
                    obj.approved = dtbl.Rows[0]["APPROVED"].ToBoolean();
                    obj.approvedBy = dtbl.Rows[0]["APPROVED_BY"].ConvertToString();
                    obj.approvedDt = Convert.ToDateTime(dtbl.Rows[0]["APPROVED_DT"]);                    
                    obj.VerificationRequired = dtbl.Rows[0]["VERIFICATION_REQUIRED"].ConvertToString();
                    obj.verificationcode = dtbl.Rows[0]["VERIFICATION_CODE"].ConvertToString();
                    obj.verificationvalidity = dtbl.Rows[0]["VERIFICATION_VALIDITY"].ConvertToString();
                    obj.verificationcode = Convert.ToBase64String(Encoding.UTF8.GetBytes(obj.verificationcode));
                    obj.mobilenumber = dtbl.Rows[0]["MOBILE_number"].ConvertToString();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }

        public string GetUserPassword(string Usercode)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("select password from com_web_usr where lower(EMAIL)=:usr and STATUS='E'");
                    //cmdText += String.Format(" and to_date(EXPIRY_DT) >=TO_DATE(to_date('{0}','DD-MONTH-RRRR'),'DD/MM/RRRR')", System.DateTime.Now.ToString("dd-MMMM-yyyy"));
                    try
                    {
                        dtbl = service.GetDataTable(cmdText,
                            new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = Usercode.ToLower().Trim(), ParameterName="usr"}
                        
                        }
                        );
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    obj.password = dtbl.Rows[0]["PASSWORD"].ConvertToString();
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj.password.ToString();
        }

        public bool UpdateVerificationCode(string Usercode,string verificCode, string verifyValidity)
        {
            bool retValue = false;
            try
            {               
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("update com_web_usr set VERIFICATION_CODE='" + verificCode + "',VERIFICATION_VALIDITY=To_Date('" + verifyValidity + "','DD/MM/YYYY') where USR_CD='" + Usercode + "' and STATUS='E' and APPROVED='Y'");
                    
                    try
                    {
                        service.SubmitChanges(cmdText, null);
                        retValue = true;
                    }
                    catch (Exception)
                    {

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
            catch (Exception ex)
            {
               
                ExceptionManager.AppendLog(ex);
            }
            return retValue;
        }

        #endregion
       

        #region Check For Valid UserCode & Password
        public Users SelectUserPassword(string Usercode, string Password)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string aa = Utils.EncryptString(Password);
                    string cmdText = String.Format("select PASSWORD from com_web_usr where USR_CD=:usr and PASSWORD=:pwd and STATUS='E' and APPROVED='Y'");
                    
                    try
                    {
                        dtbl = service.GetDataTable(cmdText,
                            new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = Usercode.ToLower().Trim(), ParameterName="usr"},
                                new OracleParameter { DbType = DbType.String, Value = aa ,   ParameterName="pwd"}
                        
                        }
                        );
                    }
                    catch (Exception)
                    {

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
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }
        #endregion
        

        #region Check for Update Password


        public Users UpdatePassword(string Usercode, string Newpassword)
        {
            Users obj = new Users();
            DataTable dtbl = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string bb = Utils.EncryptString(Newpassword);
                    string cmdText = String.Format("UPDATE COM_WEB_USR SET PASSWORD =:newpwd WHERE USR_CD =:usr");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText,
                            new DbParameter[] { 
                                new OracleParameter { DbType = DbType.String, Value = Usercode.ToLower().Trim(), ParameterName="usr"},
                                new OracleParameter { DbType = DbType.String, Value = bb ,   ParameterName="newpwd"}
                        }
                        );
                        
                    }
                    catch (Exception)
                    {

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
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }

        #endregion


        public string GenerateRandomPassword()
        {
            Random random = new Random();
            string[] alpha = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string s = "";
            string a = "";
            s = String.Concat(s, random.Next(100).ToString().PadLeft(2, '0'));
            for (int i = 0; i <= 1; i++)
            {
                a = String.Concat(a, alpha[random.Next(0, 21)].ToString().ToLower());
            }
            string bbb = "", ccc = "";
            bbb = String.Concat(bbb, random.Next(10).ToString().PadLeft(1, '0'));
            ccc = String.Concat(ccc, alpha[random.Next(0, 21)].ToString().ToLower());
            return s + a + bbb + ccc + "#" + "$";
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}