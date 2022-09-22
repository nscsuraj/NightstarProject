using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

namespace SMNResponsive.Core.Extensions
{
    public class GenericHelper
    {
        public static T CloneObject<T>(T model)
        {
            string json = JsonConvert.SerializeObject(model);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool PrepareDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool HasWriteAccessToDirectory(string filePath, bool createIfNecessary = false)
        {
            try
            {
                // Create directory if it does not exists
                if (createIfNecessary && !Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                // Attempt to get a list of security permissions from the file. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                Directory.GetAccessControl(filePath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public static bool SaveFile(HttpPostedFile file, string basePath)
        {
            try
            {
                // Create directory if it does not exists
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                file.SaveAs(Path.Combine(basePath, file.FileName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetSlug(string s)
        {
            // Replace space with dash, and then remove non alphanumeric
            return new Regex("[^a-zA-Z0-9]").Replace(s.Replace(" ", "-"), "");
        }
                        
        public static string GetRandomString(int size, bool lowerCase)
        {
            try
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                if (lowerCase)
                    return builder.ToString().ToLower();

                return builder.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static string GetGuidString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            return GuidString;
        }

        public static string GetBaseURL()
        {
            string baseUrl = string.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme,
                HttpContext.Current.Request.Url.Authority);
            return baseUrl;

        }

        public static string GetAppURL()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
                return "http://" + HttpContext.Current.Request.Url.Host;
            else
                return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
        }

        /// <summary>
        /// Return an URL without the querystring parameters
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static string GetCleanURL(string URL)
        {
            Uri uri = new Uri(URL);
            return uri.GetLeftPart(UriPartial.Path);
        }

        /// <summary>
        /// Returns a Request.RawURL without the querystring parameters
        /// </summary>
        /// <param name="RawURL"></param>
        /// <returns></returns>
        public static string GetCleanRawURL(string RawURL)
        {
            string baseUrl = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme,
            HttpContext.Current.Request.Url.Authority);
            Uri uri = new Uri(baseUrl + RawURL);
            return uri.GetLeftPart(UriPartial.Path);
        }
    }
}