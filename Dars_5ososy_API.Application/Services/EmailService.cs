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
        <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
        <html xmlns='http://www.w3.org/1999/xhtml' lang='en'>
        <head>
            <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
            <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
            <title>Welcome to Dars 5ososy</title>
        </head>
        <body style='margin: 0; padding: 0; background-color: #f8fafc; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, Helvetica, Arial, sans-serif; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;'>
            
            <!-- Outer wrapper table to force centering -->
            <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color: #f8fafc; padding: 40px 20px;'>
                <tr>
                    <td align='center'>
                        
                        <!-- Main Content Container Card -->
                        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; background-color: #ffffff; border-radius: 20px; border: 1px solid #e2e8f0; overflow: hidden; box-shadow: 0 10px 30px rgba(37, 99, 235, 0.04);'>
                            
                            <!-- Top Accent Decorative Bar -->
                            <tr>
                                <td height='6' style='background-color: #2563eb; font-size: 0; line-height: 0;'>&nbsp;</td>
                            </tr>
                            
                            <!-- Header Content (Title) -->
                            <tr>
                                <td align='center' style='padding: 40px 40px 10px 40px;'>
                                    <h1 style='margin: 0; font-size: 26px; font-weight: 800; color: #1e293b; letter-spacing: -0.5px;'>
                                        Welcome to <span style='color: #2563eb;'>Dars 5ososy</span>
                                    </h1>
                                </td>
                            </tr>
                            
                            <!-- Inner Content Box -->
                            <tr>
                                <td style='padding: 20px 40px;'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color: #ffffff; border: 1px solid #e2e8f0; border-radius: 14px; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.02);'>
                                        <tr>
                                            <td align='center' style='padding: 32px 24px;'>
                                                
                                                <!-- Topic Heading -->
                                                <div style='margin: 0 0 12px 0; font-size: 18px; font-weight: 700; color: #2563eb; text-transform: uppercase; letter-spacing: 0.5px;'>
                                                    {topic}
                                                </div>
                                                
                                                <!-- Message Content -->
                                                <p style='margin: 0 0 28px 0; font-size: 15px; line-height: 1.6; color: #64748b; font-weight: 500;'>
                                                    {message}
                                                </p>
                                                
                                                <!-- BULLETPROOF BUTTON SOLUTION -->
                                                <table border='0' cellpadding='0' cellspacing='0' style='margin: 0 auto;'>
                                                    <tr>
                                                        <td align='center' bgcolor='#2563eb' style='border-radius: 10px; background-color: #2563eb;'>
                                                            <a href='{link}' target='_blank' style='display: inline-block; padding: 14px 32px; font-size: 15px; font-family: sans-serif; font-weight: 600; color: #ffffff; text-decoration: none; border-radius: 10px; background-color: #2563eb; border: 1px solid #2563eb;'>
                                                                Go to {topic}
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <!-- Info Alert Callout Banner -->
                            <tr>
                                <td style='padding: 10px 40px 30px 40px;'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color: #eff6ff; border-left: 4px solid #2563eb; border-radius: 4px 12px 12px 4px;'>
                                        <tr>
                                            <td style='padding: 20px 24px;'>
                                                <div style='margin: 0 0 4px 0; font-size: 15px; font-weight: 700; color: #1e293b;'>We're Glad You're Here!</div>
                                                <div style='margin: 0; font-size: 13.5px; line-height: 1.5; color: #475569; font-weight: 500;'>Dars 5ososy is designed to elevate your learning experience. Dive into engaging lessons, connect with expert tutors, and achieve your academic goals.</div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                            <!-- Footer Area -->
                            <tr>
                                <td align='center' style='background-color: #fafafa; padding: 32px 40px; border-top: 1px solid #e2e8f0;'>
                                    
                                    <p style='margin: 0 0 16px 0; font-size: 14px; line-height: 1.5; color: #64748b; font-weight: 500;'>
                                        Best regards,<br />
                                        <strong style='color: #2563eb; font-weight: 700;'>The Dars 5ososy Team</strong>
                                    </p>
                                    
                                    <!-- Social HTML Links -->
                                    <table border='0' cellpadding='0' cellspacing='0' style='margin: 0 auto;'>
                                        <tr>
                                            <td style='padding: 0 6px;'>
                                                <a href='https://www.instagram.com/yourprofile' style='display: inline-block; font-size: 13px; font-weight: 600; color: #64748b; text-decoration: none; padding: 6px 12px; border: 1px solid #e2e8f0; border-radius: 6px; background-color: #ffffff;'>Instagram</a>
                                            </td>
                                            <td style='padding: 0 6px;'>
                                                <a href='https://www.facebook.com/yourprofile' style='display: inline-block; font-size: 13px; font-weight: 600; color: #64748b; text-decoration: none; padding: 6px 12px; border: 1px solid #e2e8f0; border-radius: 6px; background-color: #ffffff;'>Facebook</a>
                                            </td>
                                            <td style='padding: 0 6px;'>
                                                <a href='mailto:appvioteam@gmail.com' style='display: inline-block; font-size: 13px; font-weight: 600; color: #64748b; text-decoration: none; padding: 6px 12px; border: 1px solid #e2e8f0; border-radius: 6px; background-color: #ffffff;'>Email Contact</a>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            
                        </table>
                        
                    </td>
                </tr>
            </table>
            
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
