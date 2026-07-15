using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ApacStellar2026.Models;

namespace ApacStellar2026.Service;

public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<SmtpSettings> smtpOptions, ILogger<EmailSender> logger)
    {
        _smtpSettings = smtpOptions.Value;
        _logger = logger;
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var message = BuildMessage(email, "Confirm your APAC Stellar account", $@"
            <h2>Welcome to APAC Stellar</h2>
            <p>Hi {user.UserName ?? email},</p>
            <p>Thanks for registering! Please confirm your email address by clicking the link below:</p>
            <p><a href=""{confirmationLink}"" style=""display:inline-block;padding:12px 24px;background:#4CAF50;color:white;text-decoration:none;border-radius:4px;"">Confirm Email</a></p>
            <p>Or copy and paste this link into your browser:</p>
            <p><code>{confirmationLink}</code></p>
            <hr/>
            <p style=""color:#666;font-size:12px;"">APAC Stellar Financial Dashboard</p>");
        await SendAsync(message);
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        var message = BuildMessage(email, "Reset your APAC Stellar password", $@"
            <h2>Password Reset Request</h2>
            <p>Hi {user.UserName ?? email},</p>
            <p>Click the link below to reset your password:</p>
            <p><a href=""{resetLink}"" style=""display:inline-block;padding:12px 24px;background:#2196F3;color:white;text-decoration:none;border-radius:4px;"">Reset Password</a></p>
            <p>Or copy and paste this link:</p>
            <p><code>{resetLink}</code></p>
            <p>If you did not request this, you can safely ignore this email.</p>
            <hr/>
            <p style=""color:#666;font-size:12px;"">APAC Stellar Financial Dashboard</p>");
        await SendAsync(message);
    }

    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var message = BuildMessage(email, "Your APAC Stellar password reset code", $@"
            <h2>Password Reset Code</h2>
            <p>Hi {user.UserName ?? email},</p>
            <p>Use this code to reset your password:</p>
            <h1 style=""font-size:32px;letter-spacing:4px;text-align:center;"">{resetCode}</h1>
            <p>If you did not request this, you can safely ignore this email.</p>
            <hr/>
            <p style=""color:#666;font-size:12px;"">APAC Stellar Financial Dashboard</p>");
        await SendAsync(message);
    }

    private MimeMessage BuildMessage(string toEmail, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromAddress));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = htmlBody };
        return message;
    }

    private async Task SendAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port,
                _smtpSettings.UseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email sent to {Recipient}: {Subject}",
                message.To.FirstOrDefault()?.ToString(), message.Subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email sending failed to {Recipient}: {Subject}",
                message.To.FirstOrDefault()?.ToString(), message.Subject);
        }
    }
}