using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Managers
{
    public class SMTPMailService
    {
        public async Task<bool> SendMail(string to, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "<host>";
                client.Port = int.MaxValue;

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("<username>", "<password>");

                MailMessage message = CreateMailMessage(to, subject, body);
                client.Send(message);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private MailMessage CreateMailMessage(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("<registered mail id>");
            mailMessage.To.Add(to);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            return mailMessage;
        }
    }
}
