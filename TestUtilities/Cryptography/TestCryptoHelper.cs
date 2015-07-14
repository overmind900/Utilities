using System;
using System.Security.Cryptography;
using NUnit.Framework;
using Utilities;

namespace TestUtilities.Cryptography {
	[TestFixture]
	public class TestCryptoHelper {

	
		[Test]
		public void EncryptDecrypt() {

			using ( AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider() ) {
				ICryptoTransform encryptor = aesAlg.CreateEncryptor();
				ICryptoTransform decryptor = aesAlg.CreateDecryptor();
				const string orignal = "Hello World";

				byte[] encrypted = orignal.Encrypt( encryptor );
				Assert.NotNull( encrypted );
				Assert.IsNotEmpty( encrypted );

				string decrypted = encrypted.Decrypt( decryptor );

				Assert.AreEqual( orignal, decrypted );
			}
		}
	}
}
