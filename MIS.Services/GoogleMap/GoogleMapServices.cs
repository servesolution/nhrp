using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.GoogleMap;
using System.Data;
using EntityFramework;
using ExceptionHandler;

namespace MIS.Services.GoogleMap
{
    public class GoogleMapServices
    {

        public GoogleMapData getCoordination(string type, string dist, string vdc, string ward)
        {
            List<GoogleMapModel> lstCordinate = new List<GoogleMapModel>();
            GoogleMapData objData = new GoogleMapData();

            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                //                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(vdc) && !string.IsNullOrEmpty(ward))
                //                {
                //                    if(type=="A")
                //                    {
                //                        cmdText = @"SELECT BAM.HOUSE_OWNER_ID,BAM.LATITUDE,BAM.LONGITUDE FROM NHRS.NHRS_BUILDING_ASSESSMENT_MST BAM INNER JOIN 
                //                NHRS.NHRS_HOUSE_OWNER_MST nhom ON BAM.HOUSE_OWNER_ID=nhom.HOUSE_OWNER_ID where BAM.DISTRICT_CD='" + dist + "' AND BAM.VDC_MUN_CD='" + vdc + "' BAM.WARD_NO='" + vdc + "'";
                //                    }
                //                    else
                //                    {
                //                        cmdText = @"SELECT BAM.HOUSE_OWNER_ID,BAM.LATITUDE,BAM.LONGITUDE FROM NHRS.NHRS_BUILDING_ASSESSMENT_MST BAM INNER JOIN 
                //                NHRS.NHRS_HOUSE_OWNER_MST nhom ON BAM.HOUSE_OWNER_ID=nhom.HOUSE_OWNER_ID where BAM.DISTRICT_CD='" + dist + "' AND BAM.VDC_MUN_CD='" + vdc + "' BAM.WARD_NO='" + vdc + "' BAM.TARGET_FLAG='" + type + "'";
                //                    }

                //                }


                try
                {
                    service.Begin();
                    //dtbl = service.GetDataTable(new
                    //{
                    //    query = cmdText,
                    //    args = new
                    //    {

                    //    }
                    //});
                    service.PackageName = "PKG_NHRS_COORDINATES";
                    dtbl = service.GetDataTable(true, "PR_GETCOORDINATEWITHSTATUS", dist, vdc, ward, type, DBNull.Value);
                    if (dtbl != null)
                    {
                        int j = 0;

                        foreach (DataRow dr in dtbl.Rows)
                        {
                           
                                GoogleMapModel obj = new GoogleMapModel();
                                obj.houseID = dr["HOUSE_OWNER_ID"].ToString();
                                obj.houseOwnerName = dr["FULL_NAME_ENG"].ToString();
                                obj.Longitude = dr["LONGITUDE"].ToString();
                                obj.Latitude = dr["LATITUDE"].ToString();
                                obj.enrolled = dr["ENROLLMENT_FLAG"].ToString();
                                obj.Targeted = dr["TARGET_FLAG"].ToString();
                                obj.Payment = dr["PAYROLL_GENERATION_FLAG"].ToString();
                                obj.Type = dr["TYPE"].ToString();
                                if (obj.Type == "T")
                                {
                                    obj.Type = "Targeted";
                                }
                                if (obj.Type == "E")
                                {
                                    obj.Type = "enrolled";
                                }
                                if (obj.Type == "P")
                                {
                                    obj.Type = "Payment";
                                }
                                if (obj.Type == "A")
                                {
                                    obj.Type = "All";
                                }

                                lstCordinate.Add(obj);
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
                objData.lstModel = lstCordinate;
                if (objData.lstModel.Count > 0)
                {
                    for (int i = 1; i < lstCordinate.Count; i++)
                    {
                        if (lstCordinate[i].Latitude != null && lstCordinate[i].Latitude != "0" && lstCordinate[i].Longitude != null && lstCordinate[i].Longitude != "")
                        {
                            objData.houseID = lstCordinate[i].houseID;
                            objData.houseOwnerName = lstCordinate[i].houseOwnerName;
                            objData.Latitude = lstCordinate[i].Latitude;
                            objData.Longitude = lstCordinate[i].Longitude;
                            objData.enrolled = lstCordinate[i].enrolled;
                            objData.Targeted = lstCordinate[i].Targeted;
                            objData.Payment = lstCordinate[i].Payment;
                            objData.Type = lstCordinate[i].Type;
                           

                        }
                    }
                }
                else
                {
                    objData.Latitude = "27.7000";
                    objData.Longitude = "85.3333";
                }
                if (dist != "" || dist != null)
                {
                    objData.Zoom = "10";
                }
                if (vdc != null && vdc != "")
                {
                    objData.Zoom = "15";
                }
                if (ward != null && ward != "")
                {
                    objData.Zoom = "20";
                }

                return objData;



            }

        }
    }
}
