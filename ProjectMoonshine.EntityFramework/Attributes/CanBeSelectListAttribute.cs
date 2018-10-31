using System;

namespace ProjectMoonshine.EntityFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CanBeSelectListAttribute : Attribute
    {
        public bool CanBeSelectList {get;set;}
        public CanBeSelectListAttribute(bool selectList = false)
        {
            CanBeSelectList = selectList;
        }
    }
}
