using System.Net.Mail;
using System.Net;
using System;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.DAL;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CenterRegisterCard.EmailFunctions
{
    public class EmailDataFunctions
    {
        public static void SendEmail(User user, string path)
        {
            try
            {
                Random rand = new Random();

                CenterRegisterCardContext.EmailMessageRecovery = rand.Next(10000, 99999).ToString();

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("centerregistercard@mail.ru");

                mail.To.Add(new MailAddress(user.Email));

                mail.Subject = "Восстановление пароля";


                string htmlBody = @$"
        <html>
        <body>
            <h1>Здравствуйте,{user.Surname} {user.Name} {user.Patronymic}</h1>
            <img src=""cid:image1"">
            <p>Вы получили это сообщение, потому что запросили восстановление пароля от вашего аккаунта.</p>
            <h2>Дата запроса: {DateTime.Now}</h2>
            <h3>Для восстановления пароля, пожалуйста, введите пароль ПИН-код:</h2>
            <h4>{CenterRegisterCardContext.EmailMessageRecovery}</h4>
        </body>
        </html>";

                mail.Body = htmlBody;

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                Attachment imageAttachment = new Attachment(@$"{path}\Images\mail.png");
                imageAttachment.ContentId = "image1"; 
                mail.Attachments.Add(imageAttachment);

                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru");

                smtpClient.Credentials = new NetworkCredential("centerregistercard@mail.ru", "f33AXCynXxXMeyfPQfTR"); 

                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка отправки сообщения: " + ex.Message);
            }
        }


        public static void SendEmailActivateUserAccount(User user, string path)
        {
            try
            {
                Random rand = new Random();

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("centerregistercard@mail.ru");

                mail.To.Add(new MailAddress(user.Email));

                mail.Subject = "Ваша заявка одобрена!";


                string htmlBody = @$"
        <html>
        <body>
            <h1>Здравствуйте,{user.Surname} {user.Name} {user.Patronymic}</h1>
            <img src=""cid:image1"">
            <p>Вы получили это сообщение, потому что вша заявка была одорбрена на получение социальной карты.</p>
            <h2>Для того чтобы посмотреть свою социальную карту зайдите в личный кабинет</h2>
            <h3>С уважением:</h3>
            <h4>ЦРК</h4>
        </body>
        </html>";

                mail.Body = htmlBody;

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                Attachment imageAttachment = new Attachment(@$"{path}\Images\mail.png");
                imageAttachment.ContentId = "image1";
                mail.Attachments.Add(imageAttachment);

                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru");

                smtpClient.Credentials = new NetworkCredential("centerregistercard@mail.ru", "f33AXCynXxXMeyfPQfTR");

                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка отправки сообщения: " + ex.Message);
            }
        }
    }
}
