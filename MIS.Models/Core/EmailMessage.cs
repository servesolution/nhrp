﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }     

    }
}
