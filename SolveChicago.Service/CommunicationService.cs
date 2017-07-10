using SolveChicago.Common;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveChicago.Service
{
    public class CommunicationService : BaseService
    {
        public CommunicationService(SolveChicagoEntities db) : base(db) { }

        public void Log(DateTime date, string communicationType, string userId, bool success)
        {
            db.Communications.Add(new Communication
            {
                Date = date,
                Type = communicationType,
                UserId = userId,
                Success = success
            });
            db.SaveChanges();
        }

        public void AdminInvite(string invitee, string inviter, string inviteUrl)
        {
            string communicationType = string.Format(Constants.Communication.AdminInvite);
            EmailService service = new EmailService(db);
            service.DeliverSendGridMessage(
                invitee,
                Constants.Global.SolveSmartCities,
                inviter,
                "1b6d368a-4488-42ea-b189-d26a9d503a8b",
                new Dictionary<string, string>
                {
                    { "-inviter-", inviter },
                    { "-inviteUrl-", inviteUrl },
                    { "-year-", DateTime.UtcNow.Year.ToString() },
                },
                Settings.Website.FromAddress,
                communicationType,
                "",
                null
            ).Wait();
        }

        public void ResetPassword(string email, string userId, string resetUrl)
        {
            string communicationType = string.Format(Constants.Communication.PasswordReset);
            EmailService service = new EmailService(db);
            service.DeliverSendGridMessage(
                email,
                Constants.Global.Admin,
                " ",
                "5575522e-75d3-49c7-948b-cb1af07fc24f",
                new Dictionary<string, string>
                {
                    { "-resetUrl-", resetUrl },
                    { "-year-", DateTime.UtcNow.Year.ToString() },
                },
                Settings.Website.FromAddress,
                communicationType,
                userId,
                null
            ).Wait();
        }

        public void NonprofitInviteCaseManager(CaseManager caseManager, string inviter, string inviteUrl)
        {
            string communicationType = string.Format(Constants.Communication.CaseManagerInvite, caseManager.Id);
            EmailService service = new EmailService(db);
            service.DeliverSendGridMessage(
                caseManager.Email,
                Constants.Global.SolveSmartCities,
                inviter,
                "e23bc535-b6c9-450b-9e41-9210b9fc332d",
                new Dictionary<string, string>
                {
                    { "-name-", caseManager.FirstName },
                    { "-inviter-", inviter },
                    { "-inviteUrl-", inviteUrl },
                    { "-year-", DateTime.UtcNow.Year.ToString() },
                },
                Settings.Website.FromAddress,
                communicationType,
                "",
                null
            ).Wait();
        }

        public void SendSurveyToMember(Member member, string inviter, string surveyUrl)
        {
            string communicationType = string.Format(Constants.Communication.MemberSurveyInvite, member.Id);
            EmailService service = new EmailService(db);
            service.DeliverSendGridMessage(
                member.Email,
                Constants.Global.SolveSmartCities,
                "",
                "fe187dfe-6ce4-4137-bf86-9b309e6ea015",
                new Dictionary<string, string>
                {
                    { "-name-", member.FirstName },
                    { "-inviter-", inviter },
                    { "-surveyUrl-", surveyUrl },
                    { "-year-", DateTime.UtcNow.Year.ToString() },
                },
                Settings.Website.FromAddress,
                communicationType,
                "",
                null
            ).Wait();
        }

        public void JobPlacedVerification(string jobName, Member member, string nonprofitName, string confirmUrl)
        {
            if(member.AspNetUser != null) // existing user
            {
                string communicationType = string.Format(Constants.Communication.JobPlacedVerification, member.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    member.Email,
                    Constants.Global.SolveSmartCities,
                    "",
                    "9ad1ac25-a038-45de-b82e-54e81a169d2d",
                    new Dictionary<string, string>
                    {
                        { "-name-", member.FirstName },
                        { "-nonprofitName-", nonprofitName },
                        { "-confirmUrl-", confirmUrl },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    "",
                    null
                ).Wait();
            }
            else // new user
            {
                string communicationType = string.Format(Constants.Communication.JobPlacedVerification, member.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    member.Email,
                    Constants.Global.SolveSmartCities,
                    "",
                    "b7fa9616-63d8-4bde-9400-0cc26516c1ec",
                    new Dictionary<string, string>
                    {
                        { "-memberFirstName-", member.FirstName },
                        { "-nonprofitName-", nonprofitName },
                        { "-confirmUrl-", confirmUrl },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    "",
                    null
                ).Wait();
            }
            
        }
        
        public void Referral(Nonprofit referredNpo, Member member, Nonprofit referringNpo)
        {
            // referred NPO email notification
            foreach(AspNetUser user in referredNpo.AspNetUsers.Where(x => (x.ReceiveEmail ?? true)))
            {
                string communicationType = string.Format(Constants.Communication.Referral, referredNpo.Id, member.Id, referringNpo.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    user.Email,
                    Constants.Global.SolveSmartCities,
                    referringNpo.Name,
                    "79bd8009-7796-4c7e-b94b-568aad9ee7d7",
                    new Dictionary<string, string>
                    {
                        { "-referredNPO-", referredNpo.Name },
                        { "-referringNPO-", referringNpo.Name},
                        { "-member-", $"{member.FirstName} {member.LastName}" },
                        { "-confirmUrl-", Settings.Website.BaseUrl },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    user.Id,
                    null
                ).Wait();
            }
        }

        public void InviteMemberToNonprofit(Member member, Nonprofit nonprofit)
        {
            // member email notification
            if (member.AspNetUser != null) // member already has a Solve Smart Cities login
            {
                string communicationType = string.Format(Constants.Communication.InviteExistingMemberToNonprofit, member.Id, nonprofit.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    member.AspNetUser.Email,
                    Constants.Global.SolveSmartCities,
                    nonprofit.Name,
                    "107d9612-8577-4df0-9f6b-0b066428944d",
                    new Dictionary<string, string>
                    {
                        { "-invitingNpo-", nonprofit.Name},
                        { "-memberFirstName-", $"{member.FirstName}" },
                        { "-confirmUrl-", $"{Settings.Website.BaseUrl}/Prospects/AcceptInvitation?memberId={member.Id}&nonprofitId={nonprofit.Id}" },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    member.AspNetUser.Id,
                    null
                ).Wait();
            }
            else // member needs to create a Solve Smart Cities login
            {
                // member email notification
                string communicationType = string.Format(Constants.Communication.InviteNewMemberToNonprofit, member.Id, nonprofit.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    member.Email,
                    Constants.Global.SolveSmartCities,
                    nonprofit.Name,
                    "d85301f0-8313-4970-8271-f1d5624366d0",
                    new Dictionary<string, string>
                    {
                        { "-invitingNpo-", nonprofit.Name},
                        { "-memberFirstName-", $"{member.FirstName}" },
                        { "-confirmUrl-", $"{Settings.Website.BaseUrl}/Prospects/AcceptInvitation?memberId={member.Id}&nonprofitId={nonprofit.Id}" },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    member.AspNetUser.Id,
                    null
                ).Wait();
            }
        }

        public void InvitationAccepted(Nonprofit nonprofit, Member member)
        {
            // alert NPO that invitation was accepted
            foreach (AspNetUser user in nonprofit.AspNetUsers.Where(x => (x.ReceiveEmail ?? true)))
            {
                string communicationType = string.Format(Constants.Communication.InvitationAccepted, nonprofit.Id, member.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    user.Email,
                    Constants.Global.SolveSmartCities,
                    $"{member.FirstName} {member.LastName}",
                    "ba0fbe64-cf88-48c1-97a8-45b133ffc78a",
                    new Dictionary<string, string>
                    {
                        { "-nonprofit-", nonprofit.Name},
                        { "-memberName-", $"{member.FirstName} {member.LastName}" },
                        { "-confirmUrl-", Settings.Website.BaseUrl },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    user.Id,
                    null
                ).Wait();
            }
        }

        public void ReferralSucceeded(Nonprofit nonprofit, Member member, Nonprofit referredNonprofit)
        {
            // alert NPO that their referral was successful
            foreach (AspNetUser user in nonprofit.AspNetUsers.Where(x => (x.ReceiveEmail ?? true)))
            {
                string communicationType = string.Format(Constants.Communication.ReferralSucceeded, referredNonprofit.Id, member.Id, nonprofit.Id);
                EmailService service = new EmailService(db);
                service.DeliverSendGridMessage(
                    user.Email,
                    Constants.Global.SolveSmartCities,
                    $"{member.FirstName} {member.LastName} to {referredNonprofit.Name}",
                    "637b8a6a-a254-4415-881f-7b11d197ac64",
                    new Dictionary<string, string>
                    {
                        { "-nonprofit-", referredNonprofit.Name},
                        { "-member-", $"{member.FirstName} {member.LastName}" },
                        { "-confirmUrl-", Settings.Website.BaseUrl },
                        { "-year-", DateTime.UtcNow.Year.ToString() },
                    },
                    Settings.Website.FromAddress,
                    communicationType,
                    user.Id,
                    null
                ).Wait();
            }
        }
    }
}
