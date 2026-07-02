using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Shared.Helpers;
using Dars_5ososy_API.Shared.Settings;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Mail;

namespace Dars_5ososy_API.Application.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public static string CreateEmailBody(string topic, string message, string link)
        {
            string template = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <link rel='preconnect' href='https://fonts.googleapis.com'>
                    <link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>
                    <link href='https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700;800&display=swap' rel='stylesheet'>
                    <title>Welcome to Dars 5ososy</title>
                    <style>
                        :root {{
                            --primary: #2563eb;
                            --primary-light: #eff6ff;
                            --text-main: #1e293b;
                            --text-muted: #64748b;
                            --shadow-sm: 0 2px 8px rgba(0, 0, 0, 0.04);
                            --shadow-md: 0 10px 30px rgba(37, 99, 235, 0.06);
                        }}

                        * {{
                            margin: 0;
                            padding: 0;
                            box-sizing: border-box;
                        }}

                        body {{
                            font-family: 'Plus Jakarta Sans', -apple-system, BlinkMacSystemFont, sans-serif;
                            background-color: #f8fafc;
                            color: var(--text-main);
                            line-height: 1.6;
                            padding: 40px 20px;
                        }}

                        .container {{
                            max-width: 680px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 24px;
                            box-shadow: var(--shadow-md);
                            border: 1px solid #e2e8f0;
                            overflow: hidden;
                        }}

                        /* Top Decorative Bar instead of an Icon */
                        .top-bar {{
                            height: 6px;
                            background-color: var(--primary);
                            width: 100%;
                        }}

                        /* Welcome Section */
                        .welcome-section {{
                            padding: 40px 40px 30px;
                            text-align: center;
                        }}

                        .welcome-title {{
                            font-size: 28px;
                            font-weight: 800;
                            color: var(--text-main);
                            letter-spacing: -0.5px;
                            margin-bottom: 24px;
                        }}

                        .welcome-title-accent {{
                            color: var(--primary);
                        }}

                        .welcome-content {{
                            background-color: #ffffff;
                            border: 1px solid #e2e8f0;
                            padding: 32px 24px;
                            border-radius: 16px;
                            box-shadow: var(--shadow-sm);
                            margin-bottom: 30px;
                        }}

                        .welcome-topic {{
                            font-size: 20px;
                            font-weight: 700;
                            color: var(--primary);
                            margin-bottom: 12px;
                            text-transform: uppercase;
                            letter-spacing: 0.5px;
                        }}

                        .welcome-text {{
                            font-size: 15px;
                            color: var(--text-muted);
                            font-weight: 500;
                            margin-bottom: 28px;
                        }}

                        /* Action Button */
                        .cta-button {{
                            display: inline-block;
                            background-color: var(--primary);
                            color: #ffffff;
                            padding: 14px 32px;
                            border-radius: 12px;
                            text-decoration: none;
                            font-weight: 600;
                            font-size: 15px;
                            transition: all 0.2s ease;
                            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.2);
                        }}

                        .cta-button:hover {{
                            background-color: #1d4ed8;
                            transform: translateY(-1px);
                            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.3);
                        }}

                        /* Info Section with an elegant border accent */
                        .info-section {{
                            background: linear-gradient(135deg, var(--primary-light) 0%, #f0fdf4 100%);
                            margin: 0 40px 40px;
                            padding: 24px;
                            border-radius: 16px;
                            border-left: 5px solid var(--primary);
                            text-align: left;
                        }}

                        .info-title {{
                            font-size: 16px;
                            font-weight: 700;
                            color: var(--text-main);
                            margin-bottom: 6px;
                        }}

                        .info-text {{
                            font-size: 14px;
                            line-height: 1.6;
                            color: #475569;
                            font-weight: 500;
                        }}

                        /* Footer */
                        .footer-section {{
                            background-color: #f8fafc;
                            padding: 32px 40px;
                            text-align: center;
                            border-top: 1px solid #e2e8f0;
                        }}

                        .footer-signature {{
                            font-size: 14px;
                            color: var(--text-muted);
                            margin-bottom: 20px;
                            font-weight: 500;
                        }}

                        .footer-team {{
                            color: var(--primary);
                            font-weight: 700;
                        }}

                        .social-links {{
                            display: flex;
                            justify-content: center;
                            gap: 16px;
                        }}

                        .social-link {{
                            font-size: 13px;
                            font-weight: 600;
                            text-decoration: none;
                            color: var(--text-muted);
                            transition: all 0.2s ease;
                            padding: 6px 12px;
                            border-radius: 6px;
                            background-color: #ffffff;
                            border: 1px solid #e2e8f0;
                        }}

                        .social-link:hover {{
                            color: var(--primary);
                            border-color: var(--primary);
                            background-color: var(--primary-light);
                        }}

                        /* Responsive Breakpoints */
                        @media (max-width: 600px) {{
                            body {{
                                padding: 16px 12px;
                            }}
                            .welcome-section {{
                                padding: 24px 16px;
                            }}
                            .welcome-title {{
                                font-size: 24px;
                            }}
                            .info-section {{
                                margin: 0 16px 30px;
                                padding: 20px;
                            }}
                            .footer-section {{
                                padding: 24px 16px;
                            }}
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='top - bar'></div>

                                < div class='welcome-section'>
                            <h1 class='welcome-title'>Welcome to<span class='welcome-title-accent'>Dars 5ososy</span></h1>
                    
                            <div class='welcome-content'>
                                <div class='welcome-topic'>
                                    {topic}
                                </div>
                                <p class='welcome-text'>{message}</p>

                                < a href = '{link}' class= 'cta-button' >
                                    Go to {topic}
                                </ a >
                            </ div >
                        </ div >

                        < div class= 'info-section' >
                            < div class= 'info-title' > We're Glad You're Here!</div>
                            <div class= 'info-text' > Dars 5ososy is designed to elevate your learning experience. Dive into engaging lessons, connect with expert tutors, and achieve your academic goals.</div>
                        </div>

                        <div class= 'footer-section' >
                            < div class= 'footer-signature' >
                                Best regards,<br>
                                <span class= 'footer-team' > The Dars 5ososy Team</span>
                            </div>
                            <div class= 'social-links' >
                                < a href = 'https://www.instagram.com/yourprofile' class= 'social-link' title = 'Instagram' > Instagram </ a >
                                < a href = 'https://www.facebook.com/yourprofile' class= 'social-link' title = 'Facebook' > Facebook </ a >
                                < a href = 'mailto:appvioteam@gmail.com' class= 'social-link' title = 'Email' > Email Contact </ a >
                            </ div >
                        </ div >
                    </ div >
                </ body >
                </ html >
            ";

            return template;
        }

        public async Task SendEmailAsync(EmailRequestDTO request)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail, _settings.FromName),
                    Subject = request.Subject,
                    Body = request.Body,
                    IsBodyHtml = request.IsHtml
                };

                message.To.Add(request.ToEmail);

                using var smtp = new SmtpClient(_settings.Host, _settings.Port)
                {
                    EnableSsl = _settings.EnableSSL,
                    Credentials = new NetworkCredential(
                        _settings.UserName,
                        _settings.Password)
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                await ExceptionLogger.Log(ex);
            }
        }
    }
}
