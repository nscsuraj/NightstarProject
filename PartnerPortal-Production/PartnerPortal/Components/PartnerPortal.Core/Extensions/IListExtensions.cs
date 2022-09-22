using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace System
{
    /// <summary>
    /// Extension helper methods for IList of T.
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class IListExtensions
    {
        /// <summary>
        /// Takes a list and converts it to a List of SelectListItem with 
        /// the help of a lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public static IList<SelectListItem> ToSelectItemList<T>(this IList<T> list, Func<T, SelectListItem> func)
        {
            if (list == null)
                throw new ArgumentException("list cannot be null.", "list");
            if (func == null)
                throw new ArgumentException("func cannot be null.", "func");

            return list.Select(func).ToList();
        }

        /// <summary>
        /// Converts the Generic List into a MultiSelectList.
        /// Convenience method for helping populate ViewModel classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="dataValueField">The data value field.</param>
        /// <param name="dataTextField">The data text field.</param>
        /// <param name="selectedValues">The selected values.</param>
        /// <returns></returns>
        public static MultiSelectList ToMultiSelectList<T>(this IList<T> list, string dataValueField, string dataTextField, IEnumerable selectedValues)
        {
            return new MultiSelectList(list, dataValueField, dataTextField, selectedValues);
        }

        /// <summary>
        /// Converts the Generic List into a MultiSelectList.
        /// Convenience method for helping populate ViewModel classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="dataValueField">The data value field.</param>
        /// <param name="dataTextField">The data text field.</param>
        /// <param name="commaSeparatedSelectedValues">The comma separated selected values.</param>
        /// <returns></returns>
        public static MultiSelectList ToMultiSelectList<T>(this IList<T> list, string dataValueField, string dataTextField, string commaSeparatedSelectedValues)
        {
            var selectedValues = commaSeparatedSelectedValues.Split(',');
            return list.ToMultiSelectList(dataValueField, dataTextField, selectedValues);
        }

        /// <summary>
        /// Converts the Generic List into a delimited string.
        /// Convenience method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string ToDelimitedString<T>(this IList<T> list, char separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T t in list) sb.Append(t).Append(separator);

            return (sb.Length > 0) ? sb.Remove(sb.Length - 1, 1).ToString() : string.Empty;
        }

    }
}
