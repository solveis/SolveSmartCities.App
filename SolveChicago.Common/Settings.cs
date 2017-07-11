using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Azure;

namespace SolveChicago.Common
{
    public static class Settings
    {
        public static string StorageConnectionString
        {
            get
            {
                return GetSetting("StorageConnectionString", "UseDevelopmentStorage=true;");
            }
        }

        public class Website
        {
            public static string BaseUrl
            {
                get
                {
                    return GetSetting<string>("Website.Baseurl", "http://localhost:2486");
                }
            }

            public static string FromAddress
            {
                get
                {
                    return GetSetting<string>("Website.FromAddress", "dev+no-reply@solvechicago.com");
                }
            }

            public static bool UseCdn
            {
                get
                {
                    return GetSetting<bool>("Website.UseCdn", false);
                }
            }

            public static bool ShowStagingFeatures
            {
                get
                {
                    return GetSetting<bool>("Website.ShowStagingFeatures", false);
                }
            }
        }

        public class Crypto
        {
            public static string SharedSecret
            {
                get
                {
                    return GetSetting<string>("Crypto.SharedSecret", "sharedsecret");
                }
            }

            public static string Salt
            {
                get
                {
                    return GetSetting<string>("Crypto.Salt", "saltsalt");
                }
            }
        }

        public class SendGrid
        {
            public static string Username
            {
                get
                {
                    return GetSetting<string>("SendGrid.Username", "username");
                }
            }

            public static string Password
            {
                get
                {
                    return GetSetting<string>("SendGrid.Password", "password");
                }
            }

            public static string ApiKey
            {
                get
                {
                    return GetSetting<string>("SendGrid.ApiKey", "apikey");
                }
            }
        }

        public class Mail
        {
            public static string FromAddress
            {
                get
                {
                    return GetSetting<string>("Mail.FromAddress", "dev+info@solvechicago.com");
                }
            }
            public static string InfoEmail
            {
                get
                {
                    return GetSetting<string>("Mail.FromAddress", "dev+info@solvechicago.com");
                }
            }
        }

        public class Intercom
        {
            public static string AppId
            {
                get
                {
                    return GetSetting<string>("Intercom.AppId", "ag67g3o5");
                }
            }
        }


        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        private static T GetSetting<T>(string name, T defaultValue)
        {

            var value = CloudConfigurationManager.GetSetting(name);
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            else
                return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
