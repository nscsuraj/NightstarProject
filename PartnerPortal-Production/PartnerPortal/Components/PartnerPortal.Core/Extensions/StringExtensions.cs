using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace System
{
    /// <summary>
    /// Class containing extension methods for System.String
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string representing a time and a string representing whether the 
        /// time is AM or PM to a nullable DateTime object.
        /// Only guaranteed to work for time strings that conform to this regular expression:
        /// @"^((([0]?[0-9]|1[0-2])[:]?[0-5][0-9]))$
        /// </summary>
        /// <param name="time">A string representing a time.</param>
        /// <param name="AMPM">A string representing whether the given time is AM or PM.</param>
        /// <returns>null if the conversion fails. A DateTime object otherwise.</returns>
        public static DateTime? ConvertToDateTime(this string time, string AMPM)
        {
            // If needed, insert a colon 2 characters from the end of the string
            time = (time.Contains(":")) ? time : time.Insert(time.Length - 2, ":");

            DateTime dtTemp;
            bool conversionSuccess = DateTime.TryParse(time + " " + AMPM, out dtTemp);

            if (conversionSuccess)
            {
                return dtTemp;
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ConvertToDateTime(this string date)
        {
            DateTime dtTemp;
            bool conversionSuccess = DateTime.TryParse(date, out dtTemp);

            if (conversionSuccess)
            {
                return dtTemp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// From Json to Obj
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="s">string</param>
        /// <returns>T</returns>
        public static T FromJSONToObj<T>(this string s)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(s);
        }

        /// <summary>
        /// Checks if the string is not null or empty and if the length is greater than the max specified.
        /// </summary>
        /// <param name="stringToTest">The string to test.</param>
        /// <param name="maxLength">Max possible length of the string.</param>
        /// <returns>
        /// 	<c>true</c> if [has length greater than] [the specified string to test]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasLengthGreaterThan(this string stringToTest, int maxLength)
        {
            return !string.IsNullOrEmpty(stringToTest) && stringToTest.Length > maxLength;
        }

        /// <summary>
        /// Returns null if the trim of this string is empty, else returns the trimmed string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>string</returns>
        public static string NullIfTrimEmpty(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;

            string trimmed = s.Trim();

            if (String.IsNullOrEmpty(trimmed))
                return null;

            return trimmed;
        }

        /// <summary>
        /// Parses as int.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>int</returns>
        public static int ParseAsInt(this string s)
        {
            return s.ParseAsInt(default(int));
        }

        /// <summary>
        /// Parses as int.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>int</returns>
        public static int ParseAsInt(this string s, int defaultValue)
        {
            if (s == null)
                return defaultValue;

            int returnValue;
            return int.TryParse(s, out returnValue) ? returnValue : defaultValue;
        }

        /// <summary>
        /// Removes the last character from supplied string.
        /// </summary>
        /// <param name="s">string s</param>
        /// <returns>string after removing last character</returns>
        public static string RemoveLastCharacter(this string s)
        {
            return String.IsNullOrEmpty(s) ? s : s.Remove(s.Length - 1, 1);
        }

        /// <summary>
        /// Checks if the given string has a trailing comma, and removes the comma if it does.
        /// </summary>
        /// <param name="s">The string</param>
        /// <returns>A string with no trailing comma.</returns>
        public static string RemoveTrailingComma(this string s)
        {
            return (String.IsNullOrEmpty(s) || !s.EndsWith(",")) ? s : s.Remove(s.Length - 1, 1);
        }

        /// <summary>
        /// Returns null if the string is empty, else returns the string replacing escape characters.
        /// </summary>
        /// <param name="s">string s</param>
        /// <returns>string without escape characters</returns>
        public static string ReplaceEscapeCharacters(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;
            string replace = s.Replace("&", "&amp;").Replace("'", "&apos;").Replace(">", "&gt;").Replace("<", "&lt;");
            return replace;
        }

        /// <summary>
        ///  An extension method to convert a string to an ASCII encoded byte array
        /// </summary>
        /// <param name="s">string to be turned into a byte array</param>
        /// <returns>a byte array of the string that was passed in</returns>
        public static byte[] ToAsciiEncodedByteArray(this string s)
        {
            return (new ASCIIEncoding()).GetBytes(s);
        }

        /// <summary>
        /// Splits a string into into a list
        /// </summary>
        /// <param name="input">the string to split</param>
        /// <param name="delimeter">the delimiter depending on which the string will split</param>
        /// <returns>An List (as an IList) of the substrings.</returns>
        public static IList<string> ToList(this string input, char delimeter)
        {
            return new List<string>(input.Split(delimeter));
        }

        /// <summary>
        /// Tries to get the value of a DateTime column from the DataRow.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="row">The row to get the value from.</param>
        /// <param name="columnName">The name of the column that holds the value.</param>
        /// <param name="defaultValue">The value to use if the column is null if not found.</param>
        /// <param name="dateFormatter">The way to format the date</param>
        /// <returns>Correctly casted date column value or default value.</returns>
        public static T TryGetValue<T>(this string input, T defaultValue)
        {
            var type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (!string.IsNullOrEmpty(input))
            {
                return (T)Convert.ChangeType(input, type);
            }
            return defaultValue;
        }
    }
}