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
    }
}
