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
                    return GetSetting<string>("SendGrid.Username", "azure_e141a28bef5aa8088c3d87fc0d73c9a4@azure.com");
                }
            }

            public static string Password
            {
                get
                {
                    return GetSetting<string>("SendGrid.Password", "JHGFD$%78909iOUIHGHFC");
                }
            }

            public static string ApiKey
            {
                get
                {
                    return GetSetting<string>("SendGrid.ApiKey", "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");
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
