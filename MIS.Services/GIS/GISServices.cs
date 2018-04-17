using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.GIS;
using System.Data;
using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;

namespace MIS.Services.GIS
{
    public class GISServices
    {
        public List<TargetingGeometry> getCoordination(string type = "T", string dist = null, string vdc = null, string ward = null)
        {

            List<TargetingGeometry> targetingList = new List<TargetingGeometry>();


            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    if (vdc == "''")
                    {
                        vdc = null;
                    }
                    service.PackageName = "PKG_NHRS_COORDINATES";
                    dtbl = service.GetDataTable(true, "PR_GETCOORDINATEWITHSTATUS", dist, vdc, ward, type, DBNull.Value);
                    //dtbl = service.GetDataTable(true, "PR_GETCOORDINATEWITHSTATUS", "52", "4", ward, type, DBNull.Value);
                    if (dtbl != null)
                    {
                        int j = 0;

                        foreach (DataRow dr in dtbl.Rows)
                        {
                            var targetingGeometry = new TargetingGeometry();
                            targetingGeometry.type = "Point";
                            string[] geomArray = { dr["LONGITUDE"].ConvertToString(), dr["LATITUDE"].ConvertToString() };
                            targetingGeometry.coordinates = geomArray;
                            targetingList.Add(targetingGeometry);

                            j++;

                        }

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

                return targetingList;



            }

        }


        public List<TargetingCor> getCoor(string type = "T", string dist = null, string vdc = null, string ward = null, string PANumber = null)
        {

            List<TargetingCor> targetingList = new List<TargetingCor>();


            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    if (vdc == "''")
                    {
                        vdc = null;
                    }
                    service.PackageName = "PKG_NHRS_COORDINATES";
                    dtbl = service.GetDataTable(true, "PR_GETCOORDINATEWITHSTATUS", dist, vdc, ward, type, PANumber, DBNull.Value);
                    //dtbl = service.GetDataTable(true, "PR_GETCOORDINATEWITHSTATUS", "52", "4", ward, type, DBNull.Value);
                    if (dtbl != null)
                    {
                        int j = 0;

                        foreach (DataRow dr in dtbl.Rows)
                        {

                            var targetingGeometry = new TargetingCor();

                            targetingGeometry.Name = dr["FULL_NAME_ENG"].ConvertToString();
                            targetingGeometry.lat =  dr["LATITUDE"].ToFloat() ;
                            targetingGeometry.lng =  dr["LONGITUDE"].ToFloat();
                            if (type == "P" || type == "I")
                                targetingGeometry.installment = dr["PAYROLL_INSTALL_CD"].ConvertToString();
                            else
                                targetingGeometry.installment = "4";
                            targetingList.Add(targetingGeometry);
                        }
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
                return targetingList;
            }
        }
        public List<summaryDataJson> getSummaryData(string Dist, string VDC = null)
        {
            var summaryDataJson = new summaryDataJson();
            List<summaryDataJson> SummaryList = new List<summaryDataJson>();
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_COORDINATES";
                    dtbl = service.GetDataTable(true, "PR_COUNT_SURVEY", Dist, VDC, DBNull.Value, DBNull.Value);
                    if (dtbl != null)
                    {
                        int j = 0;

                        foreach (DataRow dr in dtbl.Rows)
                        {

                            if (dtbl.Columns.Contains("VDC_MUN"))
                            {
                                summaryDataJson.VDC = dr["VDC_MUN"].ToString();
                                summaryDataJson.VDCID = dr["vdc_defined_cd"].ToString(); 
                            }
                            else
                            {
                              
                                summaryDataJson.VDC = "";
                                summaryDataJson.VDCID = "";
                            }
                            if (!string.IsNullOrEmpty(Dist))
                            {
                                summaryDataJson.District = dr["District"].ToString();
                                summaryDataJson.DistrictId = dr["district_cd"].ToString();
                            }
                            summaryDataJson.Survey = dr["Surveyed"].ToString();
                            summaryDataJson.Beneficiary = dr["Beneficiary"].ToString();
                            summaryDataJson.Enrolled = dr["Enrolled_Benef"].ToString();
                            summaryDataJson.Grievance = dr["Grievance"].ToString();
                            summaryDataJson.payment_1_tranch = dr["FIRST_INSTALLMENT"].ToString();
                            summaryDataJson.payment_2_tranch = dr["SECOND_INSTALLMENT"].ToString();
                            summaryDataJson.payment_3_tranch = dr["THIRD_INSTALLMENT"].ToString();
                            summaryDataJson.inspection_1 = dr["FIRST_INSPECTION"].ToString();
                            summaryDataJson.inspection_2 = dr["SECOND_INSPECTION"].ToString();
                            summaryDataJson.inspection_3 = dr["THIRD_INSPECTION"].ToString();
                            SummaryList.Add(summaryDataJson);
                            j++;
                        }
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
                return SummaryList;
            }

        }
    }
}
