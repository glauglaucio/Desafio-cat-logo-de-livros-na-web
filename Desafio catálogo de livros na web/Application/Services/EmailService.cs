using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Desafio_catálogo_de_livros_na_web.Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarCodigoRecuperacaoEmail(string emailDestino, string codigo)
        {
            string smtpServer = _configuration["EmailSettings:SmtpServer"];
            int port = int.Parse(_configuration["EmailSettings:Port"]);
            string senderEmail = _configuration["EmailSettings:SenderEmail"];
            string senderPassword = _configuration["EmailSettings:SenderPassword"];

            using var smtpClient = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            using var mensagem = new MailMessage(senderEmail, emailDestino)
            {
                Subject = "Código de Recuperação de Senha",
                Body = $"Seu código de recuperação é: {codigo}",
                IsBodyHtml = false,
            };

            await smtpClient.SendMailAsync(mensagem);
        }
    }
}
