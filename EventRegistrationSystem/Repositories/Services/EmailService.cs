using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace EventRegistrationSystem.Repositories.Services
{
    public class EmailService
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;
        private readonly string _fromEmail;


        public EmailService(string apiKey, string apiSecret, IConfiguration configuration)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
            _fromEmail = Environment.GetEnvironmentVariable("MAILJET_FROM_EMAIL");
        }
        public async Task SendConfirmationEmailAsync(string recipientEmail, string recipientName, string eventTitle)
        {
            MailjetClient client = new MailjetClient(_apiKey, _apiSecret);

            string htmlBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                            color: #333;
                        }}
                        .container {{
                            width: 80%;
                            margin: 0 auto;
                            padding: 20px;
                            border: 1px solid #ddd;
                            border-radius: 10px;
                            background-color: #f9f9f9;
                        }}
                        .header {{
                            background-color: #007bff;
                            color: white;
                            padding: 10px;
                            border-radius: 10px 10px 0 0;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 0.9em;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Event Registration Confirmation</h2>
                        </div>
                        <p>Dear {recipientName},</p>
                        <p>Thank you for registering for the event: <strong>{eventTitle}</strong>.</p>
                        <p>We look forward to seeing you there!</p>
                        <br />
                        <p>Best regards,</p>
                        <p><strong>Event Registration System</strong></p>
                        <div class='footer'>
                            <p>If you have any questions, feel free to contact us at support@example.com.</p>
                        </div>
                    </div>
                </body>
                </html>";

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, _fromEmail)
            .Property(Send.FromName, "Event Registration System")
            .Property(Send.Subject, "Event Registration Confirmation")
            .Property(Send.HtmlPart, htmlBody)
            .Property(Send.To, recipientEmail);

            MailjetResponse response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                throw new Exception($"Failed to send email. StatusCode: {response.StatusCode}, ErrorInfo: {response.GetErrorInfo()}");
            }
        }
    }
}
