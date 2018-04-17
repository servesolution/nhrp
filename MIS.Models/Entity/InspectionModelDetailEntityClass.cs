using EntityFramework;
using System;

//{
[Table(Name = "NHRS_INSPECTION_MODEL_DTL")]
public class InspectionModelDetailEntityClass : EntityBase
{
    public System.Decimal? INSPECTION_MODEL_DTL_ID = null;
    public System.Decimal ?INSPECTION_CODE_ID = null;
    public System.Decimal? MODEL_ID = null;
   
    public System.String ipAddress = null;

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public System.String IpAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    [Column(Name = "INSPECTION_MODEL_DTL_ID", IsKey = true, SequenceName = "")]
    public System.Decimal? iNSPECTION_MODEL_DTL_ID
    {
        get { return INSPECTION_MODEL_DTL_ID; }
        set { INSPECTION_MODEL_DTL_ID = value; }
    }
    [Column(Name = "INSPECTION_CODE_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? iNSPECTION_CODE_ID
    {
        get { return INSPECTION_CODE_ID; }
        set { INSPECTION_CODE_ID = value; }
    }
    [Column(Name = "MODEL_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? mODEL_ID
    {
        get { return MODEL_ID; }
        set { MODEL_ID = value; }
    }
    



}
//}
