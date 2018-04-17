using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.GoogleMap
{
   public class GoogleMapData
    {

       public string DistrictId { get; set; }
       public string VCDId { get; set; }
       public string Ward { get; set; }
       public string Type { get; set; }
       public string Latitude { get; set; }
       public string Longitude { get; set; }
       public string houseID { get; set; }
       public string houseOwnerName { get; set; }

       public string Targeted { get; set; }
       public string enrolled { get; set; }
       public string Payment { get; set; }
       public string Zoom { get; set; }
       public List<GoogleMapModel> lstModel { get; set; }

    }
}
