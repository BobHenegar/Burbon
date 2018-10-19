using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectMoonshine
{
    public static class GlobalConstants
    {
        public static readonly int PageSize = 50;
        public static readonly string RefererHeader = "Referer";
        public static readonly string RefAction = "refAction";
        public static readonly string RefController = "refController";
        public static readonly string RefId = "refId";
        public static readonly string RefText = "refText";
        public static Regex IsValidGuidRegex = new Regex(@"([0-9A-Fa-f]{8}[-]{1}[0-9A-Fa-f]{4}[-]{1}[[0-9A-Fa-f]{4}[-]{1}[0-9A-Fa-f]{4}[-]{1}[0-9A-Fa-f]{12})", RegexOptions.Compiled);
        public static List<string> StateList = new List<string>() { "Please Select", "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN",
            "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA",
            "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY" };
        public static List<string> ProcessTypeList = new List<string>() { "Please Select", "Application", "Renewal", "Reinstatement" };
    }
}
