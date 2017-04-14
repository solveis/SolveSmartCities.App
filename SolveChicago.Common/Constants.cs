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

        public class Global
        {
            public const string Admin = "Solve Chicago Community Support";
            public const string SolveChicago = "Solve Chicago";
        }

        public class Communication
        {
            public const string Inquiry = "Inquiry";
            public const string MemberSurveyInvite = "MemberSurveyInvite";
        }

        public class Member
        {
            public class SurveyStep
            {
                public const string Invited = "Invited";
                public const string Personal = "Personal";
                public const string Family = "Family";
                public const string Education = "Education";
                public const string Jobs = "Jobs";
                public const string Nonprofits = "Nonprofits";
                public const string GovernmentPrograms = "GovernmentPrograms";
                public const string Complete = "Complete";

            }
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

        public class Upload
        {
            public const string AdminPhotos = "adminphotos";
            public const string MemberPhotos = "memberphotos";
            public const string CaseManagerPhotos = "casemanagerphotos";
            public const string CorporationPhotos = "corporationphotos";
            public const string NonprofitPhotos = "nonprofitphotos";
            public const string ReferrerPhotos = "Referrerphotos";
        }

        public class Gender
        {
            public const string Male = "Male";
            public const string Female = "Female";
            public const string Other = "Other";
        }

        public class Family
        {
            public static class Relationships
            {
                public const string Parent = "Parent";
                public const string Child = "Child";
                public const string Spouse = "Spouse";
            }
        }

        public class School
        {
            public class Degrees
            {
                public const string HSDiploma = "HS Diploma";
                public const string GED = "GED";
                public const string BachelorsDegree = "Bachelor's Degree";
                public const string MastersDegree = "Master's Degree";
                public const string PostGraduateDegree = "Post Graduate Degree";
            }

            public class Types
            {
                public const string HighSchool = "High School";
                public const string UndergraduateCollege = "Undergraduate College";
                public const string GraduateCollege = "Graduate College";
                public const string PostGraduateCollege = "Post Graduate College";
            }
        }

        public class GovernmentPrograms
        {
            public class Tier
            {
                public const string Federal = "Federal";
                public const string State = "State";
                public const string County = "County";
                public const string Municipal = "Municipal";
            }

            public static class Locality
            {
                public static class USA
                {
                    public const string Name = "USA";
                    public static class State
                    {
                        public static class Alabama
                        {
                            public const string Name = "USA - Alabama";
                            public static class County
                            {

                            }
                        }
                        public static class Alaska
                        {
                            public const string Name = "USA - Alaska";
                            public static class County
                            {

                            }
                        }
                        public static class Arizona
                        {
                            public const string Name = "USA - Arizona";
                            public static class County
                            {

                            }
                        }
                        public static class Arkansas
                        {
                            public const string Name = "USA - Arkansas";
                            public static class County
                            {

                            }
                        }
                        public static class California
                        {
                            public const string Name = "USA - California";
                            public static class County
                            {

                            }
                        }
                        public static class Colorado
                        {
                            public const string Name = "USA - Colorado";
                            public static class County
                            {

                            }
                        }
                        public static class Connecticut
                        {
                            public const string Name = "USA - Connecticut";
                            public static class County
                            {

                            }
                        }
                        public static class Delaware
                        {
                            public const string Name = "Delaware ";
                            public static class County
                            {

                            }
                        }
                        public static class Florida
                        {
                            public const string Name = "USA - Florida";
                            public static class County
                            {

                            }
                        }
                        public static class Georgia
                        {
                            public const string Name = "USA - Georgia";
                            public static class County
                            {

                            }
                        }
                        public static class Hawaii
                        {
                            public const string Name = "USA - Hawaii";
                            public static class County
                            {

                            }
                        }
                        public static class Idaho
                        {
                            public const string Name = "USA - Idaho";
                            public static class County
                            {

                            }
                        }
                        public static class Illinois
                        {
                            public const string Name = "USA - Illinois";
                            public static class County
                            {
                                public static class Cook
                                {
                                    public const string Name = "USA - IL - Cook";
                                    public static class Municipal
                                    {
                                        public const string Chicago = "USA - IL - Cook - Chicago";
                                    }
                                }
                            }
                        }
                        public static class Indiana
                        {
                            public const string Name = "USA - Indiana";
                            public static class County
                            {

                            }
                        }
                        public static class Iowa
                        {
                            public const string Name = "USA - Iowa";
                            public static class County
                            {

                            }
                        }
                        public static class Kansas
                        {
                            public const string Name = "USA - Kansas";
                            public static class County
                            {

                            }
                        }
                        public static class Kentucky
                        {
                            public const string Name = "USA - Kentucky";
                            public static class County
                            {

                            }
                        }
                        public static class Louisiana
                        {
                            public const string Name = "USA - Louisiana";
                            public static class County
                            {

                            }
                        }
                        public static class Maine
                        {
                            public const string Name = "USA - Maine";
                            public static class County
                            {

                            }
                        }
                        public static class Maryland
                        {
                            public const string Name = "USA - Maryland";
                            public static class County
                            {

                            }
                        }
                        public static class Massachusetts
                        {
                            public const string Name = "USA - Massachusetts";
                            public static class County
                            {

                            }
                        }
                        public static class Michigan
                        {
                            public const string Name = "USA - Michigan";
                            public static class County
                            {

                            }
                        }
                        public static class Minnesota
                        {
                            public const string Name = "USA - Minnesota";
                            public static class County
                            {

                            }
                        }
                        public static class Mississippi
                        {
                            public const string Name = "USA - Mississippi";
                            public static class County
                            {

                            }
                        }
                        public static class Missouri
                        {
                            public const string Name = "USA - Missouri";
                            public static class County
                            {

                            }
                        }
                        public static class Montana
                        {
                            public const string Name = "USA - Montana";
                            public static class County
                            {

                            }
                        }
                        public static class Nebraska
                        {
                            public const string Name = "USA - Nebraska";
                            public static class County
                            {

                            }
                        }
                        public static class Nevada
                        {
                            public const string Name = "USA - Nevada";
                            public static class County
                            {

                            }
                        }
                        public static class NewHampshire
                        {
                            public const string Name = "USA - New Hampshire";
                            public static class County
                            {

                            }
                        }
                        public static class NewJersey
                        {
                            public const string Name = "USA - New Jersey";
                            public static class County
                            {

                            }
                        }
                        public static class NewMexico
                        {
                            public const string Name = "USA - New Mexico";
                            public static class County
                            {

                            }
                        }
                        public static class NewYork
                        {
                            public const string Name = "USA - New York";
                            public static class County
                            {

                            }
                        }
                        public static class NorthCarolina
                        {
                            public const string Name = "USA - North Carolina";
                            public static class County
                            {

                            }
                        }
                        public static class NorthDakota
                        {
                            public const string Name = "USA - North Dakota";
                            public static class County
                            {

                            }
                        }
                        public static class Ohio
                        {
                            public const string Name = "USA - Ohio";
                            public static class County
                            {

                            }
                        }
                        public static class Oklahoma
                        {
                            public const string Name = "USA - Oklahoma";
                            public static class County
                            {

                            }
                        }
                        public static class Oregon
                        {
                            public const string Name = "USA - Oregon";
                            public static class County
                            {

                            }
                        }
                        public static class Pennsylvania
                        {
                            public const string Name = "USA - Pennsylvania";
                            public static class County
                            {

                            }
                        }
                        public static class RhodeIsland
                        {
                            public const string Name = "USA - Rhode Island";
                            public static class County
                            {

                            }
                        }
                        public static class SouthCarolina
                        {
                            public const string Name = "South Carolina ";
                            public static class County
                            {

                            }
                        }
                        public static class SouthDakota
                        {
                            public const string Name = "USA - South Dakota";
                            public static class County
                            {

                            }
                        }
                        public static class Tennessee
                        {
                            public const string Name = "USA - Tennessee";
                            public static class County
                            {

                            }
                        }
                        public static class Texas
                        {
                            public const string Name = "USA - Texas";
                            public static class County
                            {

                            }
                        }
                        public static class Utah
                        {
                            public const string Name = "USA - Utah";
                            public static class County
                            {

                            }
                        }
                        public static class Vermont
                        {
                            public const string Name = "USA - Vermont";
                            public static class County
                            {

                            }
                        }
                        public static class Virginia
                        {
                            public const string Name = "USA - Virginia";
                            public static class County
                            {

                            }
                        }
                        public static class Washington
                        {
                            public const string Name = "USA - Washington";
                            public static class County
                            {

                            }
                        }
                        public static class WestVirginia
                        {
                            public const string Name = "USA - West Virginia";
                            public static class County
                            {

                            }
                        }
                        public static class Wisconsin
                        {
                            public const string Name = "USA - Wisconsin";
                            public static class County
                            {

                            }
                        }
                        public static class Wyoming
                        {
                            public const string Name = "Wyoming";
                            public static class County
                            {

                            }
                        }
                        public static class PuertoRico
                        {
                            public const string Name = "USA - Puerto Rico";
                            public static class County
                            {

                            }
                        }
                        public static class Guam
                        {
                            public const string Name = "USA - Guam";
                            public static class County
                            {

                            }
                        }
                        public static class MarianaIslands
                        {
                            public const string Name = "USA - Northern Mariana Islands";
                            public static class County
                            {

                            }
                        }
                        public static class VirginIslands
                        {
                            public const string Name = "USA - U.S. Virgin Islands";
                            public static class County
                            {

                            }
                        }
                        public static class AmericanSamoa
                        {
                            public const string Name = "USA - American Samoa";
                            public static class County
                            {

                            }
                        }

                    }
                }
            }
        }
    }
}
