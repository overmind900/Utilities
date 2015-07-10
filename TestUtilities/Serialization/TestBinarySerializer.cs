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

		[Serializable]
		public class TestBinarySerializerClassB : TestBinarySerializerClass, IConvertible {
			private double m_Double;

			public double MyDouble {
				get { return m_Double; }
				set { m_Double = value; }
			}

			public override int GetHashCode() {
				return base.GetHashCode();
			}

			public override bool Equals( object obj ) {
				return base.Equals( obj ) && 
					obj is TestBinarySerializerClassB &&
					( (TestBinarySerializerClassB)obj ).m_Double.Equals(this.m_Double);
			}
			
			#region IConvertible
			public TypeCode GetTypeCode() {
				throw new NotSupportedException();
			}

			public bool ToBoolean( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public byte ToByte( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public char ToChar( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public DateTime ToDateTime( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public decimal ToDecimal( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public double ToDouble( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public short ToInt16( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public int ToInt32( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public long ToInt64( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public sbyte ToSByte( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public float ToSingle( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public string ToString( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public object ToType( Type conversionType, IFormatProvider provider ) {
				return this;
			}

			public ushort ToUInt16( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public uint ToUInt32( IFormatProvider provider ) {
				throw new NotSupportedException();
			}

			public ulong ToUInt64( IFormatProvider provider ) {
				throw new NotSupportedException();
			} 
			#endregion

		}

		[Test]
		public void SerializeDeserializeWithInharents() {
			TestBinarySerializerClassB orignal = new TestBinarySerializerClassB() { MyInt = 4, MyString = "Hello World", MyDouble = 4.2 };
			byte[] serialized = orignal.ToByteArray();
			Assert.IsNotEmpty( serialized );
			TestBinarySerializerClassB deserialized = serialized.FromByteArrayToObject<TestBinarySerializerClassB>();
			Assert.AreNotSame( orignal, deserialized );
			Assert.AreEqual( orignal, deserialized );
		}

		[Test]
		public void SerializeDeserializeWithInharents2() {
			TestBinarySerializerClass orignal = new TestBinarySerializerClassB() { MyInt = 4, MyString = "Hello World", MyDouble = 4.2 };
			byte[] serialized = orignal.ToByteArray();
			Assert.IsNotEmpty( serialized );
			TestBinarySerializerClass deserialized = serialized.FromByteArrayToObject<TestBinarySerializerClass>();
			Assert.AreNotSame( orignal, deserialized );
			Assert.AreEqual( orignal, deserialized );
			Assert.AreEqual( 4.2, ( (TestBinarySerializerClassB)deserialized ).MyDouble );
		}
	}
}