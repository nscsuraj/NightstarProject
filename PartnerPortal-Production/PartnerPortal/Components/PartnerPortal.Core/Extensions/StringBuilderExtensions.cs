using System.Collections.Specialized;
namespace System.Text
{
    /// <summary>
    /// Extension methods for the StringBuilder class.
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class StringBuilderExtensions
    {

        /// <summary>
        /// Appends the format conditionally.
        /// </summary>
        /// <param name="sb">The StringBuilder class.</param>
        /// <param name="format">The format.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="args">The args.</param>
        public static void AppendFormatConditionally(this StringBuilder sb, string format, bool condition, params object[] args)
        {
            if (condition)
            {
                sb.AppendFormat(format, args);
            }
        }

        /// <summary>
        /// Appends the formatted string to the StringBuilder if the input is not null or empty.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="format">The format for the input.</param>
        /// <param name="input">The input.</param>
        public static void AppendFormatIfNotNullOrEmpty(this StringBuilder stringBuilder, string format, string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                stringBuilder.AppendFormat(format, input);
            }
        }

        /// <summary>
        /// Appends the input string to the StringBuilder if it is not null or empty.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="input">The input.</param>
        public static void AppendIfNotNullOrEmpty(this StringBuilder stringBuilder, string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                stringBuilder.Append(input);
            }
        }

        /// <summary>
        /// Appends an escaped quoted XML attribute.
        /// </summary>
        /// <param name="sb">The StringBuilder class.</param>
        /// <param name="paramName">A string containing parameter name</param>
        /// <param name="paramValue">An object containing parameter value</param>
        /// <returns></returns>
        public static StringBuilder AppendXMLAttribute(this StringBuilder sb, string paramName, object paramValue)
        {
            return sb.Append(paramName).Append("=\"").Append(paramValue).Append("\" ");
        }

        /// <summary>
        /// Appends an escaped quoted XML attribute conditionally.
        /// </summary>
        /// <param name="sb">The StringBuilder class..</param>
        /// <param name="paramName">A string containing parameter name</param>
        /// <param name="paramValue">An object containing parameter value</param>
        /// <param name="condition">A boolean condition</param>
        /// <returns></returns>
        public static StringBuilder AppendXMLAttribute(this StringBuilder sb, string paramName, object paramValue, bool condition)
        {
            return condition ? sb.AppendXMLAttribute(paramName, paramValue) : sb;
        }

        /// <summary>
        /// Appends the jqGrid Paging variables as escaped quoted XML attributes.
        /// </summary>
        /// <param name="sb">The StringBuilder class.</param>
        /// <param name="request">A NameValueCollection containing jqgrid paging variables</param>
        /// <returns></returns>
        public static StringBuilder AppendJQGridPagingAttributes(this StringBuilder sb, NameValueCollection request)
        {
            return sb.AppendXMLAttribute("intPageSize", request["rows"])
                .AppendXMLAttribute("intPageNumber", request["page"])
                .AppendXMLAttribute("strOrderDir", request["sord"])
                .AppendXMLAttribute("strOrderBy", String.IsNullOrEmpty(request["sidx"]) ? null : request["sidx"].Replace("FMT", ""));
        }

    }

}
