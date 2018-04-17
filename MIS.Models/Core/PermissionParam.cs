using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
    public class PermissionParam
    {
        public string EnableAdd { get; set; }
        public string EnableEdit { get; set; }
        public string EnableDelete { get; set; }
        public string EnableApprove { get; set; }
        public string EnablePrint { get; set; } 
    }
}
