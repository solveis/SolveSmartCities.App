//using SolveChicago.Web.Common;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Mail;
//using System.Web;

//namespace SolveChicago.Web.Services
//{
//    public class EmailService
//    {
//        public static bool SendMessage(string toAddresses, string subject, string htmlBody, Dictionary<string, byte[]> attachments = null)
//        {
//            try
//            {
//                MailMessage mailMessage = new MailMessage(Settings.Mail.FromAddress, toAddresses)
//                {
//                    Subject = subject,
//                    Body = htmlBody,
//                    IsBodyHtml = true
//                };
//                List<MemoryStream> aMs = new List<MemoryStream>();
//                List<System.Net.Mail.Attachment> att = new List<System.Net.Mail.Attachment>();
//                if (attachments != null)
//                {
//                    foreach (KeyValuePair<string, byte[]> a in attachments)
//                    {
//                        var ms = new MemoryStream(a.Value);
//                        aMs.Add(ms);
//                        var at = new System.Net.Mail.Attachment(ms, a.Key);
//                        att.Add(at);
//                        mailMessage.Attachments.Add(at);
//                    }
//                }
//                SmtpClient smtpClient = new SmtpClient();
//                smtpClient.Send(mailMessage);
//                foreach (var a in att)
//                {
//                    a.Dispose();
//                }
//                foreach (var m in aMs)
//                    m.Dispose();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                string errorMessage;
//                if (!string.IsNullOrEmpty(toAddresses) && !string.IsNullOrEmpty(subject))
//                    errorMessage = string.Format("Could not send email to {0} with subject {1}", toAddresses, subject);
//                else
//                    errorMessage = "Could not send email, but could not parse additional details as to why or to whom.";
//                return false;
//            }
//        }
//    }
//}