using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Inspinia_MVC5.Extencion
{
    public class Correo
    {
        public static void EnviarCorreoGmail(string destinatarios, string asunto, string mensajeHtml) {
            try
            {

                var destino = destinatarios.Split(';');

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("SisVehicular@gmail.com");
                foreach (var item in destino)
                {
                    mail.To.Add(item);
                }
                mail.Subject = asunto;
                mail.Body = mensajeHtml;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("SisVehicular@gmail.com", "Sistema12345");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public static void SendEmailOutlook(string destinatarios, string asunto, string mensajeHtml)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
                {
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("lapa89@outlook.es", "contraseña"),
                    Port = 587,
                    EnableSsl = true,
                };
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("lapa89@outlook.es");
                msg.Subject = asunto;
                msg.Body = mensajeHtml;
                msg.To.Add(destinatarios);

                smtpClient.Send(msg);
                msg.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception exp)
            {
                //Console.WriteLine(exp.ToString());
            }
        }
    }
}