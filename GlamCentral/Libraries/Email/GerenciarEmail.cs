using GlamCentral.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace GlamCentral.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }


        public void EnviarSenhaParaColaboradorPorEmail(Funcionario funcionarios)
        {
            string corpoMsg = string.Format("<h2>Colaborador - Glam Central</h2>" +
                "Sua senha é:" +
                "<h3>{0}</h3>", funcionarios.Senha);


            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(funcionarios.Email);
            mensagem.Subject = "Colaborador - Glam Central - Senha do colaborador - " + funcionarios.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar Mensagem via SMTP
            _smtp.Send(mensagem);
        }
    }
}
