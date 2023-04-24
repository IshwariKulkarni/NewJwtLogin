using MimeKit;
using System.Net.Mail;
//using EmailService = NewJwtLogin.Services.EmailService;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NewJwtLogin.Services
{
    public class EmailService
    {
        public void SendEmail(string to, string subject, string html)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Aishwarya Nikhal", "aishwaryanikhal12@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = html
            };



            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("aishwaryanikhal12@gmail.com", "**Pooja@12");
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
