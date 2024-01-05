using ControleContatos.Interface;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ControleContatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _config;

        public Email(IConfiguration config)
        {
            _config = config;
        }

        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _config.GetValue <string> ("SMTP:Host");
                string nome = _config.GetValue <string> ("SMTP:Nome");
                string userName = _config.GetValue <string> ("SMTP:UserName");
                string senha = _config.GetValue <string> ("SMTP:Senha");
                int porta = _config.GetValue <int> ("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(userName, senha);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
