using InnovaCore.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Threading.Tasks;

namespace InnovaCore.Services.Services
{
    public class EmailServices : IEmailServices
    {
        public async Task SendStatusUpdateEmailAsync(string userEmail, string tituloTarefa, string status)
        {
            string corStatus;
            string mensagemDestaque;

            switch (status.ToLower())
            {
                case "aprovada":
                    corStatus = "#00ff88"; // Verde Neon para sucesso
                    mensagemDestaque = "Sua solicitação avançou para a próxima fase!";
                    break;
                case "rejeitada":
                    corStatus = "#ff0055"; // Vermelho/Rosa Neon para erro
                    mensagemDestaque = "Gostamos muito da sua ideia, mas infelizmente não conseguiremos prosseguir com a proposta... Pelo motivo de:";
                    break;
                default:
                    corStatus = "#00e5ff"; // Ciano original para "Em Análise", "Pendente", etc.
                    mensagemDestaque = "Temos uma atualização no pipeline da sua solicitação:";
                    break;
            }


            var mensagem = new MimeMessage();
            // Remetente
            mensagem.From.Add(new MailboxAddress("Sistema InnovaCore", "innocore777@gmail.com"));
            // Destinatário
            mensagem.To.Add(MailboxAddress.Parse(userEmail));
            mensagem.Subject = $"Atualização de Status: {tituloTarefa}";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
<div style='background-color: #050505; padding: 40px 20px; font-family: ""Inter"", Arial, sans-serif; color: #ffffff;'>
    <table align='center' border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; background-color: #111111; border: 1px solid #1a1a1a; border-radius: 20px; overflow: hidden; box-shadow: 0 0 30px {corStatus}1a;'>
        <tr>
            <td align='center' style='padding: 40px 0 20px 0;'>
                <div style='display: inline-block; border: 1px solid {corStatus}4d; color: {corStatus}; font-size: 10px; padding: 4px 12px; border-radius: 50px; letter-spacing: 2px; background: {corStatus}0d; margin-bottom: 15px;'>SISTEMA DE STATUS</div>
                <h1 style='margin: 0; font-size: 32px; font-weight: 900; letter-spacing: -1px; text-transform: uppercase;'>
                    <span style='color: #ffffff;'>INNO</span><span style='color: {corStatus}; text-shadow: 0 0 15px {corStatus}99;'>CORE</span>
                </h1>
            </td>
        </tr>

        <tr>
            <td style='padding: 20px 40px; text-align: center;'>
                <h2 style='color: #ffffff; font-size: 20px; margin-bottom: 10px;'>Olá!</h2>
                <p style='color: #a0a0a0; font-size: 16px; line-height: 1.6;'>
                    {mensagemDestaque}
                </p>
                
                <div style='background: {corStatus}0d; border: 1px solid {corStatus}33; border-radius: 15px; padding: 25px; margin: 30px 0;'>
                    <p style='color: #a0a0a0; font-size: 12px; font-weight: bold; text-transform: uppercase; margin: 0 0 10px 0; letter-spacing: 1px;'>Solicitação</p>
                    <p style='color: #ffffff; font-size: 18px; font-weight: bold; margin: 0 0 20px 0;'>{tituloTarefa}</p>
                    
                    <div style='display: inline-block; background: {corStatus}; color: #000000; padding: 10px 25px; border-radius: 50px; font-weight: 900; font-size: 14px; text-transform: uppercase; box-shadow: 0 0 15px {corStatus}66;'>
                        {status}
                    </div>
                </div>
            </td>
        </tr>

        <tr>
            <td align='center' style='padding: 0 40px 40px 40px;'>
                <a href='http://seusite.com/Login' style='background-color: #ffffff; color: #000000; padding: 15px 35px; border-radius: 50px; text-decoration: none; font-weight: bold; font-size: 13px; display: inline-block;'>ACESSAR DASHBOARD</a>
            </td>
        </tr>

        <tr>
            <td align='center' style='background-color: #080808; padding: 20px; border-top: 1px solid #1a1a1a;'>
                <small style='color: #444444; font-size: 11px;'>&copy; 2026 InnovaCore - Hub de Engenharia Criativa</small>
            </td>
        </tr>
    </table>
</div>";

            mensagem.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                try
                {
                    // No Gmail, use a porta 587 com StartTls
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // IMPORTANTE: Aqui você deve usar a SENHA DE APP do Google, não a sua senha real
                    await smtp.AuthenticateAsync("innocore777@gmail.com", "dtnl agui gjnk yfxm");

                    await smtp.SendAsync(mensagem);
                }
                catch (Exception ex)
                {
                    // Isso ajudará você a ver o erro real no console se falhar
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