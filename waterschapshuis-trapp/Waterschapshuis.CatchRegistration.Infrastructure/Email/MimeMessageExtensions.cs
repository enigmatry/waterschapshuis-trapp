﻿using System;
using System.Linq;
using Waterschapshuis.CatchRegistration.Core.Email;
using Waterschapshuis.CatchRegistration.Core.Settings;
using MimeKit;
using MimeKit.Text;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Email
{
    public static class MimeMessageExtensions
    {
        public static void SetEmailData(this MimeMessage message, EmailMessage email, SmtpSettings settings)
        {
            message.To.AddRange(email.To.Select(address => new MailboxAddress(address)));
            message.From.AddRange(email.From.Select(address => new MailboxAddress(address)));

            if (!String.IsNullOrEmpty(settings.From))
            {
                message.From.Add(new MailboxAddress(settings.From));
            }

            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = email.Content
            };
        }
    }
}
