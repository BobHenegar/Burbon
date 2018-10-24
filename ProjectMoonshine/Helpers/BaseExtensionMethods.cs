using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectMoonshine.Helpers
{
    public static class BaseExtensionMethods
    {
        /// <summary>
        /// Gets the first match of a regex
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ExtractMatchSubstring(this Regex regex, string input)
        {
            var result = string.Empty;
            if (regex.IsMatch(input))
            {
                var match = regex.Match(input);
                if (match.Length > 0)
                {
                    result = match.Groups[1].Value;
                }
            }
            return result;
        }


        private static string[] SplitUrl(this string input)
        {
            // Split the url to url + query string
            var fullUrl = input;
            var questionMarkIndex = fullUrl.IndexOf('?');
            string url = fullUrl;
            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
            }
            var result = input.Split('/');
            return result;
        }

        /// <summary>
        /// Gets the controller from a relative URL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetActionFromUrl(this string input)
        {
            var result = string.Empty;
            var split = input.SplitUrl();
            if (split.Length > 2)
            {
                result = split[2];
            }
            return result;
        }


        /// <summary>
        /// Gets the action from a relative URL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetControllerFromUrl(this string input)
        {
            var result = string.Empty;
            var split = input.SplitUrl();
            if (split.Length > 1)
            {
                result = split[1];
            }
            return result;
        }

        public static string GetRegexMatch(this Regex regex, string input)
        {
            var result = string.Empty;
            if (regex.IsMatch(input))
            {
                var match = regex.Match(input);
                result = match.Value;
            }
            return result;
        }

        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        public static List<string> GetAllDescriptions(this Enum value)
        {
            DescriptionAttribute[] da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Select(s => s.Description).ToList();
        }

        public static async Task<byte[]> ReadByteArrayAsync(this IFormFile fileForm)
        {
            using (StreamReader reader = new StreamReader(fileForm.OpenReadStream()))
            {
                return Encoding.ASCII.GetBytes(await reader.ReadToEndAsync());
            }
        }
    }
}
