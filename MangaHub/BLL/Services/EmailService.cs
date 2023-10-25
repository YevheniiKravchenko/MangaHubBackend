using BLL.Contracts;
using Common.Configs;
using DAL.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly EmailCreds _emailCreds;

        public EmailService(
            Lazy<IUnitOfWork> unitOfWork,
            EmailCreds emailCreds)
        {
            _unitOfWork = unitOfWork;
            _emailCreds = emailCreds;
        }

        public void SendResetPasswordEmail(string emailAddress)
        {
            var user = _unitOfWork.Value.Users.Value.GetUserByEmail(emailAddress);
            var resetPasswordToken = _unitOfWork.Value.Users.Value.GenerateResetPasswordToken(user.UserId);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailCreds.EmailUserName));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Reset password";

            var resetPasswordUrl = string.Format(_emailCreds.ResetPasswordUrl, resetPasswordToken);
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<p>To reset password please follow the link</p><a href=\"{resetPasswordUrl}\">Reset password</a>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailCreds.EmailHost, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailCreds.EmailUserName, _emailCreds.EmailPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
