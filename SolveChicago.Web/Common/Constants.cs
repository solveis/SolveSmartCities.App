using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Web.Common
{
    public class Constants
    {
        public class Roles
        {
            public static string Admin = "Admin";
            public static string Member = "Member";
            public static string CaseManager = "CaseManager";
            public static string Corporation = "Corporation";
            public static string Nonprofit = "Nonprofit";
        }

        public static class Upload
        {
            public static string AdminPhotos = "adminphotos";
            public static string MemberPhotos = "memberphotos";
            public static string CaseManagerPhotos = "casemanagerphotos";
            public static string CorporationPhotos = "corporationphotos";
            public static string NonprofitPhotos = "nonprofitphotos";
        }
    }
}
