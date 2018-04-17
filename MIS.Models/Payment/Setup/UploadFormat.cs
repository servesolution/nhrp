using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.Setup
{
    public class UploadFormat
    {
        public UploadFormat() { }
        public string UploadFormatCD { get; set; }
        public string ColHeadingName { get; set; }
        public string DbHeadingName { get; set; }
        public string ColumnOrder { get; set; }
        public string DataType { get; set; }
        public string DbDataType { get; set; }
        public string DefaultValue { get; set; }
        public int? ColumnSize { get; set; }

    }
}
