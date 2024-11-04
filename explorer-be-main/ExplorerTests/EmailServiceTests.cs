using System;
using System.Threading.Tasks;
using FluentAssertions;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Moq;
using Xunit;

namespace ExplorerTests
{
    public class EmailServiceTests
    {
        private readonly Mock<IOptions<MailSettings>> _mailSettingsMock;
        private readonly Mock<SmtpClient> _smtpClientMock;
        private readonly EmailService _emailService;
        private readonly MailSettings _mailSettings;

        public EmailServiceTests()
        {
            _mailSettings = new MailSettings
            {
                Mail = "test@example.com",
                Password = "password",
                Host = "smtp.example.com",
                Port = 587
            };

            _mailSettingsMock = new Mock<IOptions<MailSettings>>();
            _mailSettingsMock.Setup(ms => ms.Value).Returns(_mailSettings);

            _smtpClientMock = new Mock<SmtpClient>();

            _emailService = new EmailService(_mailSettingsMock.Object, _smtpClientMock.Object);
        }
    }
}