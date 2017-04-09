using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Common
{
    public class Constants
    {
        public class Regex
        {
            public static string UrlPattern = @"(mailto\:|(news|(ht|f)tp(s?))\://)(([^[:space:]]+)|([^[:space:]]+)( #([^#]+)#)?)";
            public static string FilePattern = @"(/[a-zA-Z0-9]+)+(\.[a-zA-Z]{2,4})((\?\w*=?\w*)(\&\w+=\w+)*)?";
            public static string EmailPattern = "^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*\\.([A-Za-z]{2,4})$";
        }

        public class GenericNames
        {
            public const string Admin = "Solve Chicago Community Support";
        }

        public class Communication
        {
            public const string Inquiry = "Inquiry";
        }

        public class Roles
        {
            public const string Admin = "Admin";
            public const string Member = "Member";
            public const string CaseManager = "CaseManager";
            public const string Corporation = "Corporation";
            public const string Nonprofit = "Nonprofit";
            public const string Referrer = "Referrer";
        }

        public static class Upload
        {
            public const string AdminPhotos = "adminphotos";
            public const string MemberPhotos = "memberphotos";
            public const string CaseManagerPhotos = "casemanagerphotos";
            public const string CorporationPhotos = "corporationphotos";
            public const string NonprofitPhotos = "nonprofitphotos";
            public const string ReferrerPhotos = "Referrerphotos";
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

        public static class GovernmentPrograms
        {
            public static class Tier
            {
                public const string Federal = "Federal";
                public const string State = "State";
            }

            public static class Locality
            {
                public class Federal
                {
                    public const string USA = "USA";
                }

                public static class State
                {
                    public const string Alabama = "Alabama ";
                    public const string Alaska = "Alaska ";
                    public const string Arizona = "Arizona ";
                    public const string Arkansas = "Arkansas ";
                    public const string California = "California ";
                    public const string Colorado = "Colorado ";
                    public const string Connecticut = "Connecticut ";
                    public const string Delaware = "Delaware ";
                    public const string Florida = "Florida ";
                    public const string Georgia = "Georgia ";
                    public const string Hawaii = "Hawaii ";
                    public const string Idaho = "Idaho ";
                    public const string Illinois = "Illinois";
                    public const string Indiana = "Illinois Indiana ";
                    public const string Iowa = "Iowa ";
                    public const string Kansas = "Kansas ";
                    public const string Kentucky = "Kentucky ";
                    public const string Louisiana = "Louisiana ";
                    public const string Maine = "Maine ";
                    public const string Maryland = "Maryland ";
                    public const string Massachusetts = "Massachusetts ";
                    public const string Michigan = "Michigan ";
                    public const string Minnesota = "Minnesota ";
                    public const string Mississippi = "Mississippi ";
                    public const string Missouri = "Missouri ";
                    public const string Montana = "Montana";
                    public const string Nebraska = "Montana Nebraska ";
                    public const string Nevada = "Nevada ";
                    public const string NewHampshire = "New Hampshire ";
                    public const string NewJersey = "New Jersey ";
                    public const string NewMexico = "New Mexico ";
                    public const string NewYork = "New York ";
                    public const string NorthCarolina = "North Carolina ";
                    public const string NorthDakota = "North Dakota ";
                    public const string Ohio = "Ohio ";
                    public const string Oklahoma = "Oklahoma ";
                    public const string Oregon = "Oregon ";
                    public const string Pennsylvania = "Pennsylvania";
                    public const string RhodeIsland = "Pennsylvania Rhode Island ";
                    public const string SouthCarolina = "South Carolina ";
                    public const string SouthDakota = "South Dakota ";
                    public const string Tennessee = "Tennessee ";
                    public const string Texas = "Texas ";
                    public const string Utah = "Utah ";
                    public const string Vermont = "Vermont ";
                    public const string Virginia = "Virginia ";
                    public const string Washington = "Washington ";
                    public const string WestVirginia = "West Virginia ";
                    public const string Wisconsin = "Wisconsin ";
                    public const string Wyoming = "Wyoming";
                }
            }
        }
    }
}
