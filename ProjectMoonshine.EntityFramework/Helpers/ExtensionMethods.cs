using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Helpers
{
    public static class ExtensionMethods
    {
        public static string RecursivelyFormatException(this Exception exception, StringBuilder builderToFill = null, int level = 0, string methodName = "", [CallerMemberName] string callerName = "")
        {
            if (string.IsNullOrEmpty(methodName))
            {
                methodName = callerName;
            }
            var indent = new string(' ', level);
            if (builderToFill == null)
            {
                builderToFill = new StringBuilder();
            }
            else
            {
                builderToFill.Append(builderToFill.AppendLine(indent + "=== INNER EXCEPTION ==="));
            }

            if (exception.InnerException != null)
            {
                builderToFill.AppendLine("=== INNER EXCEPTION ===");
            }

            void append(string prop)
            {
                var propInfo = exception.GetType().GetProperty(prop);
                var val = propInfo.GetValue(exception);

                if (val != null)
                {
                    builderToFill.AppendFormat("{0}{1}: {2}{3}", indent, prop, val.ToString(), Environment.NewLine);
                }
            }

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (DictionaryEntry de in exception.Data)
            {
                builderToFill.AppendFormat("{0}{1} = {2}{3}", indent, de.Key, de.Value, Environment.NewLine);
            }

            if (exception.InnerException != null)
            {
                RecursivelyFormatException(exception.InnerException, builderToFill, ++level, methodName);
            }
            builderToFill.Append(string.Format("In Method: {0}", methodName));
            return builderToFill.ToString();
        }
    }
}
