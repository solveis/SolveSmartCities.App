using SolveChicago.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SolveChicago.App.Tests.Common
{
    public class CryptoTest
    {
        [Fact]
        public void Crypto_EncryptStringAES_DecryptStringAES_ReturnsDecryptedString()
        {
            string text = "HelloIAmText";
            string encryptedText = Crypto.EncryptStringAES(text, Settings.Crypto.SharedSecret);
            string decryptedText = Crypto.DecryptStringAES(encryptedText, Settings.Crypto.SharedSecret);

            Assert.Equal("HelloIAmText", decryptedText);
        }

        [Fact]
        public void Crypto_EncryptStringAES_BlankPlainText_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.EncryptStringAES("", Settings.Crypto.SharedSecret));

        }

        [Fact]
        public void Crypto_EncryptStringAES_BlankSharedSecret_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.EncryptStringAES("HelloIAmText", ""));
        }

        [Fact]
        public void Crypto_DecryptStringAES_BlankPlainText_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.DecryptStringAES("", Settings.Crypto.SharedSecret));

        }

        [Fact]
        public void Crypto_DecryptStringAES_BlankSharedSecret_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.DecryptStringAES("HelloIAmText", ""));
        }
    }
}
