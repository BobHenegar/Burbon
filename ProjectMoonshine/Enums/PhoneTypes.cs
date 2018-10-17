using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Enums
{
    public enum PhoneTypes
    {
        [Description("--Please Select--")]
        Select = 0,
        [Description("Home Phone")]
        Home = 1,
        [Description("Work Phone")]
        Work = 2,
        [Description("Cell Phone")]
        Cell = 3
    }
}
