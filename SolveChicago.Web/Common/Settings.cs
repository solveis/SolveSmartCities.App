using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Azure;

namespace SolveChicago.Web.Common
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

        public class Crypto
        {
            public static string SharedSecret
            {
                get
                {
                    return GetSetting<string>("SharedSecret", "JBHVCRES#%$^&*(OIUH");
                }
            }

            public static string Salt
            {
                get
                {
                    return GetSetting<string>("Salt", "o6806642kbM7c5");
                }
            }
        }

        public class Mail
        {
            public static string FromAddress
            {
                get
                {
                    return GetSetting<string>("FromAddress", "dev@solvechicago.com");
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
