using System;
using Xunit;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using SolveChicago.Common;

namespace SolveChicago.Tests.Common
{
    [ExcludeFromCodeCoverage]
    public class SettingsTest
    {
        [Fact]
        public void Settings_Instantiate_Success()
        {
            string storageConnectionString = Settings.StorageConnectionString;
            string websiteBaseUrl = Settings.Website.BaseUrl;
            string fromAddress = Settings.Website.FromAddress;
            bool useCdn = Settings.Website.UseCdn;
            string cryptoSharedSecret = Settings.Crypto.SharedSecret;
            string SendGridUserName = Settings.SendGrid.Username;
            string password = Settings.SendGrid.Password;
            string apiKey = Settings.SendGrid.ApiKey;
            string mailFromAddress = Settings.Mail.FromAddress;
            string mailInfoEmail = Settings.Mail.InfoEmail;
        }
    }
}
