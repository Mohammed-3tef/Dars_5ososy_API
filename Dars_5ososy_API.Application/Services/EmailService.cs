using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
                    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css' />
                    <title>Welcome to Matchd</title>
                    <style>
                        :root {{
                            --shadow-sm: 0 2px 8px rgba(0, 0, 0, 0.04);
                            --shadow-md: 0 10px 30px rgba(0, 0, 0, 0.06);
                        }}

                        * {{
                            margin: 0;
                            padding: 0;
                            box-sizing: border-box;
                        }}

                        body {{
                            font-family: 'Plus Jakarta Sans', -apple-system, BlinkMacSystemFont, sans-serif;
                            background-color: #f8fafc;
                            color: #334155;
                            line-height: 1.5;
                            padding: 40px 20px;
                        }}

                        .container {{
                            max-width: 800px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 20px;
                            box-shadow: var(--shadow-md);
                            overflow: hidden;
                        }}

                        /* Header */
                        .header {{
                            text-align: center;
                            padding: 30px 30px 10px;
                        }}
        
                        .logo {{
                            max-width: 100%;
                            margin: 0 auto;
                        }}
        
                        .logo img {{
                            width: 100%;
                            height: auto;
                            display: block;
                        }}

                        /* Welcome Section */
                        .welcome-section {{
                            padding: 30px 40px;
                            text-align: center;
                        }}

                        .welcome-title {{
                            font-size: 32px;
                            font-weight: 800;
                            color: #0f172a;
                            letter-spacing: -0.5px;
                            margin-bottom: 15px;
                        }}

                        .welcome-title-accent {{
                            color: #10b981;
                        }}

                        .welcome-topic  {{
                            font-size: 24px;
                            font-weight: 800;
                            color: #0f172a;
                            margin-bottom: 10px;
                        }}

                        .welcome-text {{
                            font-size: 16px;
                            color: #64748b;
                            max-width: 480px;
                            margin: 0 auto;
                            font-weight: 500;
                        }}

                        .ball-icon {{
                            margin: 20px 0;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                            gap: 20px;
                            color: #10b981;
                        }}

                        .ball-icon::before,
                        .ball-icon::after {{
                            content: '';
                            width: 80px;
                            height: 2px;
                            background-color: #10b981;
                            opacity: 0.2;
                        }}

                        .ball-icon i {{
                            font-size: 36px;
                            animation: spin 20s linear infinite;
                        }}

                        /* Features Section */
                        .features-section {{
                            padding: 20px 40px 40px;
                        }}

                        .features-grid {{
                            display: grid;
                            grid-template-columns: repeat(3, 1fr);
                            gap: 16px;
                        }}

                        .feature-box {{
                            text-align: center;
                            padding: 24px 16px;
                            background-color: #ffffff;
                            border: 1px solid #f1f5f9;
                            border-radius: 12px;
                            box-shadow: var(--shadow-sm);
                            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
                        }}

                        .feature-box:hover {{
                            transform: translateY(-4px);
                            box-shadow: 0 12px 20px rgba(0, 0, 0, 0.08);
                            border-color: rgba(16, 185, 129, 0.3);
                        }}

                        .feature-icon {{
                            width: 52px;
                            height: 52px;
                            margin: 0 auto 16px;
                            background-color: rgba(16, 185, 129, 0.1);
                            color: #10b981;
                            border-radius: 50%;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                            font-size: 22px;
                            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
                        }}

                        .feature-box:hover .feature-icon {{
                            background-color: #10b981;
                            color: #ffffff;
                        }}

                        .feature-title {{
                            font-size: 14px;
                            font-weight: 700;
                            color: #0f172a;
                            margin-bottom: 8px;
                            letter-spacing: -0.2px;
                        }}

                        .feature-text {{
                            font-size: 12px;
                            color: #64748b;
                            line-height: 1.5;
                            font-weight: 500;
                        }}

                        /* Glad Section - Completely Re-architected */
                        .glad-section {{
                            background: linear-gradient(135deg, #0f172a 0%, #1e293b 100%);
                            margin: 0 40px 40px;
                            border-radius: 12px;
                            display: flex;
                            align-items: center;
                            justify-content: space-between;
                            overflow: hidden;
                            position: relative;
                        }}

                        .glad-content {{
                            padding: 40px;
                            flex: 1.2;
                            z-index: 2;
                        }}

                        .glad-header-wrapper {{
                            display: flex;
                            align-items: center;
                            gap: 16px;
                            margin-bottom: 12px;
                        }}

                        .glad-icon {{
                            color: #10b981;
                            background-color: rgba(255, 255, 255, 0.08);
                            border-radius: 50%;
                            width: 44px;
                            height: 44px;
                            display: flex;
                            align-items: center;
                            justify-content: center;
                            font-size: 20px;
                            flex-shrink: 0;
                        }}

                        .glad-title {{
                            font-size: 18px;
                            font-weight: 700;
                            color: #ffffff;
                            letter-spacing: -0.3px;
                        }}

                        .glad-text {{
                            font-size: 14px;
                            line-height: 1.6;
                            color: #94a3b8;
                            font-weight: 500;
                        }}

                        .player-image {{
                            flex: 0.8;
                            display: flex;
                            justify-content: flex-end;
                            z-index: 1;
                        }}

                        .player-image img {{
                            max-width: 100%;
                            max-height: 200px;
                            object-fit: contain;
                            display: block;
                        }}

                        /* Footer */
                        .footer-section {{
                            background-color: #fafafa;
                            padding: 40px 40px;
                            text-align: center;
                            border-top: 1px solid #f1f5f9;
                        }}

                        .footer-signature {{
                            font-size: 14px;
                            color: #64748b;
                            margin-bottom: 20px;
                            font-weight: 500;
                        }}

                        .footer-team {{
                            color: #10b981;
                            font-weight: 700;
                        }}

                        .social-links {{
                            display: flex;
                            justify-content: center;
                            gap: 12px;
                        }}

                        .social-link {{
                            width: 38px;
                            height: 38px;
                            background-color: #f1f5f9;
                            border-radius: 50%;
                            display: inline-flex;
                            align-items: center;
                            justify-content: center;
                            text-decoration: none;
                            color: #64748b;
                            font-size: 16px;
                            transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
                        }}

                        .social-link:hover {{
                            background-color: #10b981;
                            color: #ffffff;
                            transform: translateY(-2px);
                        }}

                        /* Responsive Breakpoints */
                        @media (max-width: 600px) {{
                            body {{
                                padding: 16px 12px;
                            }}
                            .welcome-section {{
                                padding: 20px 20px;
                            }}
                            .welcome-title {{
                                font-size: 26px;
                            }}
                            .features-section {{
                                padding: 10px 20px 30px;
                            }}
                            .features-grid {{
                                grid-template-columns: 1fr;
                                gap: 12px;
                            }}
                            .glad-section {{
                                flex-direction: column;
                                margin: 0 20px 30px;
                                text-align: center;
                            }}
                            .glad-content {{
                                padding: 30px 20px 20px;
                            }}
                            .glad-header-wrapper {{
                                flex-direction: column;
                                gap: 10px;
                            }}
                            .player-image {{
                                align-self: center;
                                justify-content: center;
                                width: 100%;
                                padding: 0 20px 20px;
                            }}
                            .player-image img {{
                                max-height: 160px;
                            }}
                            .footer-section {{
                                padding: 30px 20px;
                            }}
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <div class='logo'>
                                <img src='https://drive.google.com/u/0/drive-viewer/AKGpihbATFVdoIzOWpL_cvuY5rX7QTSKK_nNB9g8JopUYxrVvNc50jV0jzPpKTplEiMedG81v2C6XIFH2Pv4r59d2ol8xFwm6d2TLKA=s1600-rw-v1?auditContext=forDisplay' alt='Matchd Logo'>
                            </div>
                        </div>

                        <div class='welcome-section'>
                            <h1 class='welcome-title'>Welcome to <span class='welcome-title-accent'>Matchd</span>!</h1>
                            <div class='welcome-text'>
                                <div class='welcome-topic'>
                                    {topic}
                                </div>
                
                                <p>{message}</p>
                                   
                                <br/>
                
                                <a href='{link}' style='
                                    display:inline-block;
                                    background-color:#10b981;
                                    color:#fff;
                                    padding:10px 20px;
                                    border-radius:12px;
                                    text-decoration:none;
                                    font-weight:600;
                                '>
                                    {topic}
                                </a>
                            </div>
                        </div>

                        <div class='glad-section'>
                            <div class='glad-content'>
                                <div class='glad-header-wrapper'>
                                    <div class='glad-title'>We're Glad You're Here!</div>
                                </div>
                                <div class='glad-text'>Matchd is more than just matches. It's about passion, community, and the beautiful game.</div>
                            </div>
                            <div class='player-image'>
                                <img src='https://drive.google.com/u/0/drive-viewer/AKGpihZDjBu3fJB_0mYeSITqSe64baeieLkTqbl8n9K26a5YkWha8naUJ-qtYKSE6ZzbkwhS9acwafJDMd7-YXRIJs-tZRIBfjmVMA=s2560?auditContext=forDisplay' alt='Player Image'>
                            </div>
                        </div>

                        <div class='footer-section'>
                            <div class='footer-signature'>
                                Best regards,<br>
                                <span class='footer-team'>The Matchd Team</span>
                            </div>
                            <div class='social-links'>
                                <a href='https://www.instagram.com/yourprofile' class='social-link' title='Instagram'><i class='fab fa-instagram'></i></a>
                                <a href='https://www.facebook.com/yourprofile' class='social-link' title='Facebook'><i class='fab fa-facebook-f'></i></a>
                                <a href='mailto:appvioteam@gmail.com' class='social-link' title='Email'><i class='fas fa-envelope'></i></a>
                            </div>
                        </div>
                    </div>
                </body>
                </html>
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
