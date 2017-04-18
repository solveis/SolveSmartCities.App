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
    }
}
