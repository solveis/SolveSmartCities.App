using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Azure;

namespace SolveChicago.App.Common
{
    public class Settings
    {
        public static string StorageConnectionString
        {
            get
            {
                return GetSetting("StorageConnectionString", "UseDevelopmentStorage=true;");
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
