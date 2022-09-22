using System.Reflection;
using PartnerPortal.Core.Attributes;


namespace System
{
    /// <summary>
    /// Extension methods for Enumeration class.
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Toes the display string.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static string ToDisplayString(this Enum e)
        {
            string displayString = e.ToString();

            //Replace "_DASH_" with " - "
            if (displayString.Contains("_DASH_"))
            {
                displayString = displayString.Replace("_DASH_", " - ");
            }
            //Replace "_" with space
            if (displayString.Contains("_"))
            {
                displayString = displayString.Replace("_", " ");
            }

            return displayString;
        }

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string[] GetEnumValueNameData(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            EnumValueDataAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(EnumValueDataAttribute), false) as EnumValueDataAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].ProcedureInformation : null;
        }

        /// <summary>
        /// Toes the value string.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static string ToValueString(this Enum e)
        {
            return e.ToString("d");
        }
    }
}
