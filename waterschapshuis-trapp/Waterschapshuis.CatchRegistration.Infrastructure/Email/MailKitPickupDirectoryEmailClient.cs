using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Waterschapshuis.CatchRegistration.Core.Email;
using Waterschapshuis.CatchRegistration.Core.Settings;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Email
{
    public class MailKitPickupDirectoryEmailClient : IEmailClient
    {
        private readonly ILogger<MailKitPickupDirectoryEmailClient> _logger;
        private readonly SmtpSettings _settings;

        public MailKitPickupDirectoryEmailClient(SmtpSettings settings,
            ILogger<MailKitPickupDirectoryEmailClient> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Send(EmailMessage email)
        {
            var message = new MimeMessage();
            message.SetEmailData(email, _settings);

            if (!Directory.Exists(_settings.PickupDirectoryLocation))
            {
                Directory.CreateDirectory(_settings.PickupDirectoryLocation);
            }
            var filePath = Path.Combine(_settings.PickupDirectoryLocation, $"{DateTime.Now.Ticks}.eml");
            message.WriteTo(filePath);

            _logger.LogDebug($"Email to '{message.To}' regarding '{message.Subject}' is written to file system: {filePath}");
        }

        public Task SendBulkAsync(IEnumerable<EmailMessage> emails)
        {
            foreach (EmailMessage email in emails)
            {
                var message = new MimeMessage();
                message.SetEmailData(email, _settings);

                if (!Directory.Exists(_settings.PickupDirectoryLocation))
                {
                    Directory.CreateDirectory(_settings.PickupDirectoryLocation);
                }
                var filePath = Path.Combine(_settings.PickupDirectoryLocation, $"{DateTime.Now.Ticks}.eml");
                message.WriteTo(filePath);

                _logger.LogDebug($"Email to '{message.To}' regarding '{message.Subject}' is written to file system: {filePath}");
            }

            return Task.CompletedTask;
        }
    }
}
