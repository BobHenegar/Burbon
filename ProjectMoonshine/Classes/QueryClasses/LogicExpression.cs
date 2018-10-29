using ProjectMoonshine.Enums;

namespace ProjectMoonshine.Classes.QueryClasses
{
    public class LogicExpression
    {
        public QueryProperty Lhs { get; set; }
        public EqualityComparisons Logic { get; set; }
        public string Rhs { get; set; }
    }
}
