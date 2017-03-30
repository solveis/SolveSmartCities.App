using SendGrid.Helpers.Mail;
using SolveChicago.Common;
using SolveChicago.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolveChicago.Service
{
    public class EmailService : BaseService
    {
        public EmailService(SolveChicagoEntities db) : base(db) { }

        public async Task DeliverSendGridMessage(string toName, string fromName, string subject, string templateId, Dictionary<string, string> subs, string fromAddress, int organizationId = 0, string communicationType = "", string userId = "", Dictionary<string, byte[]> attachments = null)
        {
            string apiKey = Settings.SendGrid.ApiKey;
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            //break the comma delinated toName into a string list
            List<string> toAddresses = new List<string>();
            int i = 1;
            foreach (string to in toName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Regex.IsMatch(to.Trim(), Constants.Regex.EmailPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    toAddresses.Add(to.Trim());
                    i++;
                }
            }
            SendGrid.Helpers.Mail.Email from = new SendGrid.Helpers.Mail.Email(fromAddress, fromName);
            SendGrid.Helpers.Mail.Email primaryTo = new SendGrid.Helpers.Mail.Email(toAddresses.First());
            Content content = new Content("text/html", " ");

            Mail mail = new Mail(from, subject, primaryTo, content);
            
            mail.TemplateId = templateId;

            // add the rest (skip first) of the to addresses
            foreach (string to in toAddresses.Skip(1))
            {
                mail.Personalization[0].AddTo(new SendGrid.Helpers.Mail.Email(to));
            }

            foreach (var sub in subs)
            {
                mail.Personalization[0].AddSubstitution(sub.Key, sub.Value);
            }

            if (attachments != null && attachments.Any())
            {
                foreach (KeyValuePair<string, byte[]> attachment in attachments)
                {
                    SendGrid.Helpers.Mail.Attachment attach = new SendGrid.Helpers.Mail.Attachment();
                    attach.Filename = attachment.Key;
                    attach.Content = Convert.ToBase64String(attachment.Value);
                    mail.AddAttachment(attach);
                }
            }

            bool success = false;
            try
            {
                SendGrid.CSharp.HTTP.Client.Response response = await sg.client.mail.send.post(requestBody: mail.Get());
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            finally
            {
                if (organizationId > 0 && !string.IsNullOrEmpty(communicationType) && !string.IsNullOrEmpty(userId))
                {
                    CommunicationService service = new CommunicationService(db);
                    service.Log(DateTime.UtcNow, organizationId, communicationType, userId, success);
                }  
            }
        }

        public SmtpClient InitSendGridClient()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.sendgrid.net";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Settings.SendGrid.Username, Settings.SendGrid.Password);
            return client;
        }

        public Mail InitSendGridMessage(string toName, string fromName, string subject, string body, bool isBodyHtml = false)
        {
            //break the comma delinated toName into a string list
            List<string> toAddresses = new List<string>();
            int i = 1;
            foreach (string to in toName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (Regex.IsMatch(to.Trim(), Constants.Regex.EmailPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    toAddresses.Add(to.Trim());
                    i++;
                }
            }
            SendGrid.Helpers.Mail.Email from = new SendGrid.Helpers.Mail.Email(Settings.Mail.FromAddress.ToString(), fromName);
            SendGrid.Helpers.Mail.Email primaryTo = new SendGrid.Helpers.Mail.Email(toAddresses.First());
            Content content = new Content(isBodyHtml ? "text/html" : "text/plain", body);
            Mail mail = new Mail(from, subject, primaryTo, content);

            // add the rest (skip first) of the to addresses
            foreach (string to in toAddresses.Skip(1))
            {
                mail.Personalization[0].AddTo(new SendGrid.Helpers.Mail.Email(to));
            }

            return mail;
        }

        /// <summary>
        /// Queues the message.
        /// </summary>
        /// <param name="toAddresses">To addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The HTML body.</param>
        /// <param name="attachments">The attachments.</param>
        public async Task DeliverSmtpMessageAsync(string toAddresses, string subject, string body, bool isBodyHtml = false, Dictionary<string, byte[]> attachments = null)
        {
            string apiKey = Settings.SendGrid.ApiKey;
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            subject = subject.Substring(0, subject.Length <= 78 ? subject.Length : 78);
            SmtpClient client = InitSendGridClient();
            Mail mail = InitSendGridMessage(toAddresses, Constants.GenericNames.Admin, subject, body, isBodyHtml);
            if (attachments != null && attachments.Any())
            {
                foreach (KeyValuePair<string, byte[]> attachment in attachments)
                {
                    SendGrid.Helpers.Mail.Attachment attach = new SendGrid.Helpers.Mail.Attachment();
                    attach.Filename = attachment.Key;
                    attach.Content = attachment.Value.ToString();
                    mail.AddAttachment(attach);
                }
            }
            try
            {
                SendGrid.CSharp.HTTP.Client.Response response = await sg.client.mail.send.post(requestBody: mail.Get());
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Body.ReadAsStringAsync().Result);
                Console.WriteLine(response.Headers.ToString());
            }
            catch { }
        }
    }
}
