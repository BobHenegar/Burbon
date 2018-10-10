using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProjectMoonshine.EntityFramework.Attributes
{
    public class E164PhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            var phone = value.ToString();
            phone = GlobalConstants.RegExPatternPhoneTrim.Replace(phone, string.Empty);
            return GlobalConstants.RegExE164Phone.IsMatch(phone);
        }
    }
}
