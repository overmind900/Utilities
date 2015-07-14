using System;
using System.Security.Cryptography;
using NUnit.Framework;
using TestUtilities.Serialization;
using Utilities;

namespace TestUtilities.Cryptography {
	[TestFixture]
	public class TestCryptography {
		ICryptoTransform m_Encryptor;
		ICryptoTransform m_Decryptor;

		[SetUp]
		public void SetUp() {
			using ( AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider() ) {
				m_Encryptor = aesAlg.CreateEncryptor();
				m_Decryptor = aesAlg.CreateDecryptor();
			}
		}
	
		[Test]
		public void EncryptDecrypt() {
			const string orignal = "Hello World";
			byte[] encrypted = orignal.Encrypt( m_Encryptor );
			Assert.NotNull( encrypted );
			Assert.IsNotEmpty( encrypted );

			string decrypted = encrypted.Decrypt( m_Decryptor );

			Assert.AreEqual( orignal, decrypted );
			
		}

		[Test]
		public void TestObjectEncryptDecrypt() {
			TestBinarySerializer.TestBinarySerializerClassB orignal = new TestBinarySerializer.TestBinarySerializerClassB() { MyInt = 4, MyString = "Hello World", MyDouble = 4.2 };
			byte[] encrypted = orignal.Encrypt( m_Encryptor );
			Assert.NotNull( encrypted );
			Assert.IsNotEmpty( encrypted );

			TestBinarySerializer.TestBinarySerializerClassB decrypted = encrypted.Decrypt< TestBinarySerializer.TestBinarySerializerClassB>( m_Decryptor );

			Assert.AreEqual( orignal, decrypted );
		}
	}
}
