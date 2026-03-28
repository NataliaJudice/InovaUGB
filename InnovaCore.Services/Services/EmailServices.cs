using InnovaCore.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration; // <-- 1. Adicionei este using

namespace InnovaCore.Services.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration; // <-- 2. Criei esta variável privada

        // <-- 3. Criei o construtor que injeta o IConfiguration
        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendStatusUpdateEmailAsync(string userEmail, string tituloTarefa, string status)
        {
            string corStatus;
            string mensagemDestaque;

            switch (status.ToLower())
            {
                case "aprovada":
                    corStatus = "#00ff88";
                    mensagemDestaque = "Sua solicitação avançou para a próxima fase!";
                    break;
                case "rejeitada":
                    corStatus = "#ff0055";
                    mensagemDestaque = "Gostamos muito da sua ideia, mas infelizmente não conseguiremos prosseguir com a proposta... Pelo motivo de:";
                    break;
                default:
                    corStatus = "#00e5ff";
                    mensagemDestaque = "Temos uma atualização no pipeline da sua solicitação:";
                    break;
            }

            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Sistema InnovaCore", "innocore777@gmail.com"));
            mensagem.To.Add(MailboxAddress.Parse(userEmail));
            mensagem.Subject = $"Atualização de Status: {tituloTarefa}";

            var bodyBuilder = new BodyBuilder();

            // ... (AQUI FICA TODO O SEU CÓDIGO HTML DO BODYBUILDER INTACTO) ...
            bodyBuilder.HtmlBody = $@"<div style='background-color: #050505... (omitido para encurtar) ...</div>";

            mensagem.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // <-- 4. Aqui a mágica acontece! Puxamos a senha do cofre usando a chave "Pass:Psw"
                    string senhaEmail = _configuration["Pass:Psw"];
                    await smtp.AuthenticateAsync("innocore777@gmail.com", senhaEmail);

                    await smtp.SendAsync(mensagem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO SMTP: " + ex.Message);
                    throw;
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}