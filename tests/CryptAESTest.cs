/*
 * Copyright (C) 2014 Toby
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using NUnit.Framework;
using System.Text;
using System;

namespace CryptobySharp
{
	/// <author>Tobias Rees</author>
	public class CryptAESTest
	{
		[NUnit.Framework.Test]
		public virtual void testEncryptDecrypt256()
		{
			System.Console.Out.WriteLine("encrypt and decrypt testphrase");
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			KeyGenSHA3 keyGen = new KeyGenSHA3();
			string hexKey = keyGen.generateKey(256, "password");
			byte[] bKey = CryptobyHelper.hexStringToBytes(hexKey);
			CryptAES instance = new CryptAES();
			byte[] expResult = plainInput;
			byte[] result = instance.encrypt(plainInput, bKey);
			result = instance.decrypt(result, bKey);
			Assert.AreEqual(expResult, result);
		}

		[NUnit.Framework.Test]
		public virtual void testEncryptDecrypt256_HugeData()
		{
			System.Console.Out.WriteLine("encrypt and decrypt huge Data");
			byte[] plainInput = new byte[1000000];
			KeyGenSHA3 keyGen = new KeyGenSHA3();
			string hexKey = keyGen.generateKey(256, "password");
			byte[] bKey = CryptobyHelper.hexStringToBytes(hexKey);
			CryptAES instance = new CryptAES();
			byte[] expResult = plainInput;
			byte[] result = instance.encrypt(plainInput, bKey);
			result = instance.decrypt(result, bKey);
			for (int i = 0; i < 10;i++){

				result = instance.encrypt(plainInput, bKey);
				result = instance.decrypt(result, bKey);
			}
			Assert.AreEqual(expResult, result);
		}

		[NUnit.Framework.Test]
		public virtual void testEncryptDecrypt256_falseKey()
		{
			System.Console.Out.WriteLine("crypt false key");
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			KeyGenSHA3 keyGen = new KeyGenSHA3();
			string hexKey = keyGen.generateKey(256, "password");
			byte[] bKey = CryptobyHelper.hexStringToBytes(hexKey);
			CryptAES instance = new CryptAES();
			byte[] expResult = plainInput;
			byte[] result = instance.encrypt(plainInput, bKey);
			hexKey = keyGen.generateKey(256, "passwordFalse");
			bKey = CryptobyHelper.hexStringToBytes(hexKey);
			result = instance.decrypt(result, bKey);
			Assert.IsFalse(Encoding.UTF8.GetString(expResult).Equals
				(Encoding.UTF8.GetString(result)));
		}

		[NUnit.Framework.Test]
		public virtual void testEncryptDecrypt256_false()
		{
			System.Console.Out.WriteLine("crypt almost false key");
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			string hexKey = "13A9489AF957FF7B5E8E712737D0B4A0C92AE8EBAE9DD11E9C11B8CB79707017";
			byte[] bKey = CryptobyHelper.hexStringToBytes(hexKey);
			CryptAES instance = new CryptAES();
			byte[] expResult = plainInput;
			byte[] result = instance.encrypt(plainInput, bKey);
			hexKey = "13A9489AF957FF7B5E8E712737D0B4A0C92AE8EBAE9DD11E9C11B8CB79707011";
			bKey = CryptobyHelper.hexStringToBytes(hexKey);
			result = instance.decrypt(result, bKey);
			Assert.IsFalse(Encoding.UTF8.GetString(expResult).Equals
				(Encoding.UTF8.GetString(result)));
		}

		[NUnit.Framework.Test]
		public virtual void testEncryptDecrypt256_CBC()
		{
			System.Console.Out.WriteLine("encrypt and decrypt recurring words");
			byte[] plainInput = Encoding.UTF8.GetBytes("TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"
				);
			KeyGenSHA3 keyGen = new KeyGenSHA3();
			string hexKey = keyGen.generateKey(256, "password");
			byte[] bKey = CryptobyHelper.hexStringToBytes(hexKey);
			CryptAES instance = new CryptAES();
			byte[] expResult = plainInput;
			byte[] result = instance.encrypt(plainInput, bKey);
			string resString = CryptobyHelper.bytesToHexStringUpper(result);
			for (int i = 0; i < resString.Length - 32; i += 32)
			{
				Assert.IsFalse(resString.Substring(i, 32).Equals
					(resString.Substring(i + 32, 32)));
				
			}
			result = instance.decrypt(result, bKey);
			Assert.AreEqual(expResult, result);
		}
	}
}
