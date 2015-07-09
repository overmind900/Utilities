using System.Security.Cryptography;
using NUnit.Framework;
using Utilities.Cryptography;

namespace TestUtilities.Cryptography {
	[TestFixture]
	public class TestCryptoHelper {


		[Test]
		public void EncryptDecrypt() {

			using ( AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider() ) {
				ICryptoTransform encryptor = aesAlg.CreateEncryptor();
				ICryptoTransform decryptor = aesAlg.CreateDecryptor();
				const string orignal = "Hello World";

				byte[] encrypted = CryptoHelper.Encrypt( orignal, encryptor );
				Assert.NotNull( encrypted );
				Assert.IsNotEmpty( encrypted );

				string decrypted = CryptoHelper.Decrypt( encrypted, decryptor );

				Assert.AreEqual( orignal, decrypted );
			}
		}
	}
}
