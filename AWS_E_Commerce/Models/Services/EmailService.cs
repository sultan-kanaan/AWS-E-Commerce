using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Services
{
    public class EmailService
    {
        public async Task<bool> SendEmail(string message, string toEmail, string subject)
        {

            SendGridClient client = new SendGridClient("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");
            SendGridMessage msg = new SendGridMessage();

            msg.SetFrom("21029318@student.ltuc.com", "sultan");
            msg.AddTo(toEmail);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, message);

            await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.IsSuccessStatusCode;

        }
    }
}
