using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Enums
{
    public enum EmailTypes
    {
        [Description("--Please Select--")]
        Select = 0,
        [Description("Personal")]
        Personal = 1,
        [Description("Work")]
        Work = 2
    }
}
