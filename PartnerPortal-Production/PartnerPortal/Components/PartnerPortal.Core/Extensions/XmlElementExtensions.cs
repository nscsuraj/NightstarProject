namespace System.Xml
{
    /// <summary>
    ///     Class containing extension methods for XmlElement
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class XmlElementExtensions
    {
        /// <summary>
        /// Sets the conditional attribute.
        /// </summary>
        /// <param name="appXmlElement">The app XML element.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        public static void SetConditionalAttribute(this XmlElement appXmlElement, string name, string value, bool condition)
        {
            if (condition)
            {
                appXmlElement.SetAttribute(name, value);
            }
        }
    }
}
