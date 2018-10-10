using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectMoonshine.EntityFramework
{
    public static class GlobalConstants
    {
        private static readonly string CountryCodePattern = @"(1|7|20|27|3[0-4]|36|39|4[0-1]|4[3-9]|5[1-8]|6[0-6]|8[1-2]|84|86|9[0-5]|98|2[1-6][0-9]|2[8-9][0-9]|35[0-9]|3[7-8][0-9]|42[0-9]|50[0-9]|59[0-9]|6[7-9][0-9]|80[0-9]|83[0-9]|85[0-9]|8[7-9][0-9]|9[6-7][0-9]|99[0-9])";
        public static readonly Regex RegExCountryCode = new Regex(@"^" + CountryCodePattern + "$", RegexOptions.Compiled);
        // NANP (North American Numbering Plan) numbers are ten digits in length, and they are in the format:
        // NXX-NXX-XXXX
        // Where N is any digit 2-9 and X is any digit 0-9
        private static readonly string PhoneNumberPattern = "[2-9]{1}[0-9]{2}[2-9]{1}[0-9]{6}";
        public static readonly Regex RegExPhoneNumber = new Regex(@"^" + PhoneNumberPattern + "$", RegexOptions.Compiled);
        public static readonly Regex RegExE164Phone = new Regex(@"^[+]" + CountryCodePattern + PhoneNumberPattern + "$", RegexOptions.Compiled);
        public static readonly Regex RegExPatternPhoneTrim = new Regex("[() \r\n-]", RegexOptions.Compiled);
    }
}
