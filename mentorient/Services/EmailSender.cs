﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace mentorient.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            Configuration = configuration;
        }

        public AuthMessageSenderOptions Options { get; } // set only via Secret Manager
        public IConfiguration Configuration { get; set; }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Configuration.GetConnectionString("SendGridKey"), subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("no-reply@mentorient.com", "Michael Henderson"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }

    }
}
