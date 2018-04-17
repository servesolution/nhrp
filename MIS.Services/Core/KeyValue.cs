using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Services.Core
{
    public class KeyValue
    {
            public string Id { get; set; }

            public IEnumerable<IDictionary<string, string>> Data { get; set; }
        
    }
}
