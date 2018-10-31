using System;

namespace ProjectMoonshine.EntityFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CanBeDescriptionAttribute : Attribute
    {
        private bool CanBeDescription {get;set;}
        public CanBeDescriptionAttribute(bool isDescription = false)
        {
            CanBeDescription = isDescription;
        }
        public bool IsDescription()
        {
            return this.CanBeDescription;
        }
    }
}
