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
            public const string Admin = "Admin";
            public const string Member = "Member";
            public const string CaseManager = "CaseManager";
            public const string Corporation = "Corporation";
            public const string Nonprofit = "Nonprofit";
        }

        public static class Upload
        {
            public const string AdminPhotos = "adminphotos";
            public const string MemberPhotos = "memberphotos";
            public const string CaseManagerPhotos = "casemanagerphotos";
            public const string CorporationPhotos = "corporationphotos";
            public const string NonprofitPhotos = "nonprofitphotos";
        }

        public static class Gender
        {
            public const string Male = "Male";
            public const string Female = "Female";
            public const string Other = "Other";
        }

        public static class Family
        {
            public static class Relationships
            {
                public const string Parent = "Parent";
                public const string Child = "Child";
                public const string Spouse = "Spouse";
            }
        }

        public static class School
        {
            public static class Degrees
            {
                public const string HSDiploma = "HS Diploma";
                public const string GED = "GED";
                public const string BachelorsDegree = "Bachelor's Degree";
                public const string MastersDegree = "Master's Degree";
                public const string PostGraduateDegree = "Post Graduate Degree";
            }

            public static class Types
            {
                public const string HighSchool = "High School";
                public const string UndergraduateCollege = "Undergraduate College";
                public const string GraduateCollege = "Graduate College";
                public const string PostGraduateCollege = "Post Graduate College";
            }
        }
    }
}
