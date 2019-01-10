using System;
using System.Threading.Tasks;
using Company.PostsAndCommentsModels;
using Company.PostsAndCommentsModels.CreationModels;
using Company.PostsAndCommentsServices.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Company.PostsAndCommentsServices.Services
{
    public class EmailService: IEmailService
    {
        private const string Host = "smtp.yandex.ru";
        private const int Port = 465;

        public async Task Send(Mail mail)
        {
            if(mail == null || !mail.IsValid()) throw new ArgumentException();

            var emailMessage = new MimeMessage
            {
                From = {new MailboxAddress("PostsAndComments", AppSettings.SenderMail)},
                To = {new MailboxAddress(mail.RecipientEmail)},
                Subject = mail.Subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = mail.Text
                }
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(Host, Port);
                await client.AuthenticateAsync(AppSettings.SenderMail, AppSettings.SenderPass);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
