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
    }
}
