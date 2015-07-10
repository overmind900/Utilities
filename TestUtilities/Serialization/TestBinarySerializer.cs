using System;
using NUnit.Framework;
using Utilities.Serialization;
namespace TestUtilities.Serialization {
	[TestFixture]
	public class TestBinarySerializer {
		[Serializable]
		public class TestBinarySerializerClass {
			private int m_Int;

			public int MyInt {
				get { return m_Int; }
				set { m_Int = value; }
			}
			private string m_String;

			public string MyString {
				get { return m_String; }
				set { m_String = value; }
			}
			
			public override int GetHashCode() {
				// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
				return base.GetHashCode();
			}

			public override bool Equals( object obj ) {
				return obj is TestBinarySerializerClass &&
					( (TestBinarySerializerClass)obj ).m_Int == this.m_Int &&
					( (TestBinarySerializerClass)obj ).m_String.Equals( m_String );
			}
		}

		[Test]
		public void SerializeDeserialize() {

			TestBinarySerializerClass orignal = new TestBinarySerializerClass() { MyInt = 4, MyString = "Hello World" };
			byte[] serialized = orignal.ToByteArray();
			Assert.IsNotEmpty( serialized );
			TestBinarySerializerClass deserialized = serialized.FromByteArrayToObject<TestBinarySerializerClass>();
			Assert.AreNotSame( orignal, deserialized );
			Assert.AreEqual( orignal, deserialized );

		}
	}
}