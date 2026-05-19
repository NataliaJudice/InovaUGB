using InnovaCore.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace InnovaCore.Services.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        private readonly string _emailOrigem = "innocore777@gmail.com";

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendStatusUpdateEmailAsync(string userEmail, string tituloTarefa, string status)
        {
            if (string.IsNullOrEmpty(userEmail)) return;

            string corStatus;
            string mensagemDestaque;
            string saudacao = "Olá, Inovador(a)!";

            switch (status.ToLower())
            {
                case "enviada":
                    corStatus = "#00e5ff";
                    mensagemDestaque = "Sua proposta foi recebida com sucesso! Nossa equipe técnica já foi notificada.";
                    break;
                case "aprovada":
                    corStatus = "#00ff88";
                    mensagemDestaque = "Excelente notícia! Sua ideia foi validada e agora avançamos para a fase de execução!";
                    break;
                case "rejeitada":
                case "inviabilizada":
                    corStatus = "#ff0055";
                    mensagemDestaque = "Agradecemos o envio da sua proposta. No momento, decidimos não prosseguir com este projeto específico.";
                    break;
                default:
                    corStatus = "#7000ff";
                    mensagemDestaque = "O status da sua proposta foi atualizado no sistema. Confira os detalhes abaixo:";
                    break;
            }

            await EnviarEmailBase(userEmail, $"InovaUGB: Atualização da sua Proposta", saudacao, mensagemDestaque, tituloTarefa, status, corStatus);
        }

        public async Task SendStatusUpdateEmailAsyncSetores(string setorEmail, string tituloTarefa, string descricaoTarefa, string status)
        {
            if (string.IsNullOrEmpty(setorEmail)) return;

            string corStatus = "#7000ff";
            string mensagemDestaque;
            string saudacao = "Atenção, Equipe!";
            string assunto;

            switch (status.ToLower())
            {
                case "enviada":
                    corStatus = "#00e5ff";
                    assunto = "📢 NOVA PROPOSTA: Pendente de Análise";
                    mensagemDestaque = "Uma nova ideia acaba de entrar no funil do seu setor. Realize a análise técnica inicial.";
                    break;
                case "aprovada":
                    corStatus = "#00ff88";
                    assunto = "✅ PROJETO CONFIRMADO: Iniciar Planejamento";
                    mensagemDestaque = "A proposta abaixo foi aprovada e agora é uma Tarefa oficial.";
                    break;
                default:
                    assunto = $"⚙️ GESTÃO: Alteração em {tituloTarefa}";
                    mensagemDestaque = "Houve uma movimentação administrativa na proposta abaixo.";
                    break;
            }

            await EnviarEmailBase(setorEmail, assunto, saudacao, mensagemDestaque, tituloTarefa, status, corStatus, descricaoTarefa);
        }

        private async Task EnviarEmailBase(string destino, string assunto, string saudacao, string mensagemDestaque, string titulo, string status, string cor, string? descricao = null)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress("Sistema InovaUGB", _emailOrigem));

            try
            {
                mensagem.To.Add(MailboxAddress.Parse(destino));
            }
            catch (Exception)
            {
                throw new Exception($"O endereço de e-mail '{destino}' é inválido.");
            }

            mensagem.Subject = assunto;

            string htmlDescricao = !string.IsNullOrEmpty(descricao)
                ? $@"<div style='margin-top: 20px; padding-top: 20px; border-top: 1px solid {cor}22; text-align: left;'>
                        <p style='color: #a0a0a0; font-size: 11px; font-weight: bold; text-transform: uppercase; margin: 0 0 8px 0;'>Descrição da Solicitação</p>
                        <p style='color: #d0d0d0; font-size: 14px; line-height: 1.5; margin: 0;'>{descricao}</p>
                     </div>"
                : "";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                <div style='background-color: #050505; padding: 40px 20px; font-family: Arial, sans-serif; color: #ffffff;'>
                    <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; background-color: #111111; border: 1px solid #1a1a1a; border-radius: 20px; overflow: hidden;'>
                        <tr>
                            <td align='center' style='padding: 40px 0 20px 0;'>
                                <div style='display: inline-block; border: 1px solid {cor}4d; color: {cor}; font-size: 10px; padding: 4px 12px; border-radius: 50px; letter-spacing: 2px; background: {cor}0d; margin-bottom: 15px;'>WORKFLOW HUB</div>
                                <h1 style='margin: 0; font-size: 32px; font-weight: 900; text-transform: uppercase;'>
                                    <span style='color: #ffffff;'>INOVA</span><span style='color: {cor};'>UGB</span>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td style='padding: 20px 40px; text-align: center;'>
                                <h2 style='color: #ffffff; font-size: 20px; margin-bottom: 15px;'>{saudacao}</h2>
                                <p style='color: #a0a0a0; font-size: 16px; line-height: 1.6;'>{mensagemDestaque}</p>
                                <div style='background: {cor}0d; border: 1px solid {cor}33; border-radius: 15px; padding: 25px; margin: 30px 0;'>
                                    <p style='color: #a0a0a0; font-size: 11px; font-weight: bold; text-transform: uppercase; margin: 0 0 10px 0;'>Identificação do Projeto</p>
                                    <p style='color: #ffffff; font-size: 18px; font-weight: bold; margin: 0 0 20px 0;'>{titulo}</p>
                                    <div style='display: inline-block; background: {cor}; color: #000000; padding: 10px 25px; border-radius: 50px; font-weight: 900; font-size: 12px; text-transform: uppercase;'>{status}</div>
                                    {htmlDescricao}
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align='center' style='padding: 0 40px 40px 40px;'>
                                <a href='http://seusite.com/Login' style='background-color: #ffffff; color: #000000; padding: 15px 35px; border-radius: 50px; text-decoration: none; font-weight: bold; font-size: 13px;'>ACESSAR PLATAFORMA</a>
                            </td>
                        </tr>
                        <tr>
                            <td align='center' style='background-color: #080808; padding: 20px; border-top: 1px solid #1a1a1a;'>
                                <small style='color: #444444; font-size: 11px;'>&copy; 2026 InovaUGB - Inteligência em Engenharia</small>
                            </td>
                        </tr>
                    </table>
                </div>"
            };

            mensagem.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                string senhaEmail = _configuration["Pass:Psw"];
                if (string.IsNullOrEmpty(senhaEmail))
                    throw new Exception("Configuração de senha do e-mail não encontrada.");

                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailOrigem, senhaEmail);
                await smtp.SendAsync(mensagem);
            }
            catch (SmtpCommandException ex)
            {
                throw new Exception($"Erro de comando SMTP: {ex.Message}");
            }
            catch (AuthenticationException)
            {
                throw new Exception("Falha na autenticação do servidor de e-mail. Verifique as credenciais.");
            }
            catch (Exception ex)
            {
                throw new Exception("Falha inesperada ao enviar e-mail: " + ex.Message);
            }
            finally
            {
                if (smtp.IsConnected)
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}