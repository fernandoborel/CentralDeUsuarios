using CentralDeUsuarios.Infra.Messages.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CentralDeUsuarios.Infra.Messages.Helpers;

/// <summary>
/// Classe para envio de e-mails
/// </summary>
public class MailHelper
{
    private readonly MailSettings _mailSettings;

    public MailHelper(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public void Send(string mailTo, string subject, string body)
    {
        #region Escrevendo o e-mail

        var mailMessage = new MailMessage(_mailSettings.Email, mailTo);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;

        #endregion

        #region Enviando o e-mail

        var smtpClient = new SmtpClient(_mailSettings.Smtp, _mailSettings.Port.Value);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password);
        smtpClient.Send(mailMessage);

        #endregion
    }
}
