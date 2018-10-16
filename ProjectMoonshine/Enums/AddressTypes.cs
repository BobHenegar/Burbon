using System.ComponentModel;

namespace ProjectMoonshine.Enums
{
    public enum AddressTypes
    {
        [Description("--Please Select--")]
        Select = 0,
        [Description("Home Address")]
        Home = 1,
        [Description("Work Address")]
        Work = 2
    }
}
