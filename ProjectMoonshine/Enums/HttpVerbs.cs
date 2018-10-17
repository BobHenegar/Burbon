using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Enums
{
    public enum HttpVerbs
    {
        [Description("GET")]
        GET = 1,
        [Description("POST")]
        POST = 2,
        [Description("PUT")]
        PUT = 3
    }
}
