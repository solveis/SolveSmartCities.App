//using SolveChicago.App.Web.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SolveChicago.App.Models
//{
//    public class SolveChicagoSession : BaseController
//    {
//        public static SolveChicagoSession Current
//        {
//            get
//            {
//                BaseController controller = new BaseController();
//                SolveChicagoSession session = controller.HttpContext.Current.Session[SessionIdentifier] as SolveChicagoSession;
//                if (session == null)
//                {
//                    if (session.UserManager. .IsUserInRole(SiteRoles.Consultant))
//                    {
//                        int[] mappings = UserProfileMappingService.GetUserProfileMappings(WebSecurity.CurrentUserId, UserProfileMappingService.MappingType.Expert);
//                        if (mappings.Count() == 1)
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Expert, mappings.First());
//                        }
//                        else
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Expert, -1);
//                        }
//                    }
//                    else if (Roles.IsUserInRole(SiteRoles.Customer))
//                    {
//                        int[] mappings = UserProfileMappingService.GetUserProfileMappings(WebSecurity.CurrentUserId, UserProfileMappingService.MappingType.Organization);
//                        if (mappings.Count() == 1)
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Organization, mappings.First());
//                        }
//                        else
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Organization, -1);
//                        }
//                    }
//                    else if (Roles.IsUserInRole(SiteRoles.Portfolio))
//                    {
//                        int[] mappings = UserProfileMappingService.GetUserProfileMappings(WebSecurity.CurrentUserId, UserProfileMappingService.MappingType.Portfolio);
//                        if (mappings.Count() == 1)
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Portfolio, mappings.First());
//                        }
//                        else
//                        {
//                            session = UpdateCurrentSession(UserProfileMappingService.MappingType.Portfolio, -1);
//                        }
//                    }
//                    else
//                    {
//                        session = UpdateCurrentSession(UserProfileMappingService.MappingType.Organization, -1);
//                    }
//                }

//                return session;
//            }
//        }

//        public static SolveChicagoSession UpdateCurrentSession(UserProfileMappingService.MappingType mappingType, int id, int? timezoneOffset = null, bool? allowUnauthenticated = false)
//        {
//            if (WebSecurity.IsAuthenticated || (allowUnauthenticated.HasValue && allowUnauthenticated.Value))
//            {
//                SolveChicagoSession session = HttpContext.Current.Session[SessionIdentifier] as SolveChicagoSession;
//                if (session == null)
//                {
//                    session = new SolveChicagoSession();
//                }
//                session.ID = id;
//                session.IsAdmin = Roles.IsUserInRole(SiteRoles.Admin);
//                session.MappingType = mappingType;
//                session.TimezoneOffset = timezoneOffset;
//                HttpContext.Current.Session.Add(SessionIdentifier, session);
//                return session;
//            }
//            return new SolveChicagoSession() { ID = -1, IsAdmin = false, MappingType = UserProfileMappingService.MappingType.Organization, TimezoneOffset = null };
//        }

//        private static string SessionIdentifier
//        {
//            get { return string.Format("dps_{0}", WebSecurity.CurrentUserId); }
//        }
//    }
//}