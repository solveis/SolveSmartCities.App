using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace SolveChicago.App.Web
{
    public class Profile : ProfileBase
    {
        public string DisplayName
        {
            get { return this["DisplayName"] as string; }
            set { this["DisplayName"] = value; }
        }
        public string PhoneNumber
        {
            get { return this["PhoneNumber"] as string; }
            set { this["PhoneNumber"] = value; }
        }
        public string Address1
        {
            get { return this["Address1"] as string; }
            set { this["Address1"] = value; }
        }
        public string Address2
        {
            get { return this["Address2"] as string; }
            set { this["Address2"] = value; }
        }
        public string City
        {
            get { return this["City"] as string; }
            set { this["City"] = value; }
        }
        public string Province
        {
            get { return this["Province"] as string; }
            set { this["Province"] = value; }
        }
        public string Country
        {
            get { return this["Country"] as string; }
            set { this["Country"] = value; }
        }
        public bool ReceiveEmail
        {
            get { return this["ReceiveEmail"] as bool? ?? false; }
            set { this["ReceiveEmail"] = value; }
        }
    }
}