using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.ViewModels
{
    public class QueryViewModel
    {
        public List<Group> Groups { get; set; }
        public string QueryName { get; set; }
    }
    public class Rule
    {
        public QueryProperty LeftProperty { get; set; }
        public EqualityComparisons EqualityComparison { get; set; }
        public QueryProperty Joins { get; set; }
    }

    public class Group
    {
        public List<Rule> Rules { get; set; }
        public GroupTypes GroupType { get; set; }
    }

    public enum EqualityComparisons
    {
        LessThan = 1,
        GreaterThan = 2,
        LessThanOrEqualsTo = 3,
        GreaterThanOrEqualsTo = 4,
        Equals = 5
    }

    public enum GroupTypes
    {
        And = 1,
        Or = 2
    }

    public enum TypeDescriptors
    {
        String = 1,
        Guid = 2
    }
    public class QueryProperty
    {
        object Value { get; set; }
        Type Type { get; set; }
    }
}
