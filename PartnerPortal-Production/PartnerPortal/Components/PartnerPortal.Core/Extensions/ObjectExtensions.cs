using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using log4net;
using System.Web.Mvc;

namespace System
{
    /// <summary>
    /// Object Extension methods.
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class ObjectExtensions
    {


        /// <summary>
        /// Gets the logger for this object.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        public static ILog GetLogger(this object obj)
        {
            return LogManager.GetLogger(obj.GetType());
        }

        /// <summary>
        /// Maps to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static T MapTo<T>(this object obj, object destinationObject = null)
        {
            var t = typeof(T);
            object result;
            if (destinationObject != null)
            {
                result = destinationObject;
            }
            else
            {
                result = (T)Activator.CreateInstance(t);
            }
            foreach (PropertyInfo mPropertyInfo in result.GetType().GetProperties())
            {
                foreach (PropertyInfo oPropertyInfo in obj.GetType().GetProperties())
                {
                    //Check the method is not static
                    if (!oPropertyInfo.GetGetMethod().IsStatic 
                        && oPropertyInfo.Name == mPropertyInfo.Name)
                    {
                        //Check this property can write
                        if (result.GetType().GetProperty(oPropertyInfo.Name).CanWrite)
                        {
                            //Check the supplied property can read
                            if (oPropertyInfo.CanRead)
                            {
                                //Update the properties on this object
                                mPropertyInfo.SetValue(result,oPropertyInfo.GetValue(obj, null), null);
                            }
                        }
                    }
                }
            }
            return (T)result;
        }

        /// <summary>
        /// Data type conversion method that works with Nullable data types
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Object</param>
        /// <returns></returns>
        public static T To<T>(this IConvertible obj)
        {
            var t = typeof(T);
            var u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                if (obj == null) return default(T);
                return (T)Convert.ChangeType(obj, u);
            }

            return (T)Convert.ChangeType(obj, t);
        }

        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        /// <summary>
        /// Converts the object into a serialized string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string ToSerializedXmlString(this object obj)
        {
            try
            {
                String XmlizedString = null;
                var memoryStream = new MemoryStream();
                var xs = new XmlSerializer(obj.GetType());
                var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()).Remove(0, 1);
                return XmlizedString.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", ""); // strip off the header info.
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static string UTF8ByteArrayToString(Byte[] characters)
        {
            var encoding = new UTF8Encoding();
            var constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Object GetPropertyValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this Object obj, String name)
        {
            Object retval = GetPropertyValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        /// <summary>
        /// To the expando.
        /// </summary>
        /// <param name="anonymousObject">The anonymous object.</param>
        /// <returns>Expando object</returns>
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
}
