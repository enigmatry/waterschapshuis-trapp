using JetBrains.Annotations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Email;
using Waterschapshuis.CatchRegistration.Core.Settings;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Email
{
    [UsedImplicitly]
    public class MailKitEmailClient : IEmailClient
    {
        private readonly ILogger<MailKitEmailClient> _logger;
        private readonly SmtpSettings _settings;

        public MailKitEmailClient(SmtpSettings settings, ILogger<MailKitEmailClient> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Send(EmailMessage email)
        {
            var message = new MimeMessage();
            message.SetEmailData(email, _settings);

            var stopWatch = new Stopwatch();

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_settings.Server, _settings.Port);
                if (!String.IsNullOrEmpty(_settings.Username) && !String.IsNullOrEmpty(_settings.Password))
                {
                    smtpClient.Authenticate(_settings.Username, _settings.Password);
                }
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }

            _logger.LogDebug(
                $"Email to '{message.To}' regarding '{message.Subject}', using host: {_settings.Server} and port: {_settings.Port}, sent in [{stopWatch.ElapsedMilliseconds}ms]");
        }

        public async Task SendBulkAsync(IEnumerable<EmailMessage> emails)
        {
            var stopWatch = new Stopwatch();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_settings.Server, _settings.Port);

                if (!String.IsNullOrEmpty(_settings.Username) && !String.IsNullOrEmpty(_settings.Password))
                {
                    await smtpClient.AuthenticateAsync(_settings.Username, _settings.Password);
                }

                foreach (var mail in emails)
                {
                    try
                    {
                        var message = new MimeMessage();
                        message.SetEmailData(mail, _settings);
                        await smtpClient.SendAsync(message);

                        _logger.LogDebug(
                            "Email(s) to {To} regarding '{Subject}', using host: {Host} and port: {Port}, sent in [{ElapsedMilliseconds}ms]", message.To, mail.Subject, _settings.Server, _settings.Port, stopWatch.ElapsedMilliseconds);
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "Error occured while sending email: {mail}; {msg}", mail, ex.Message);
                    }
                }

                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
