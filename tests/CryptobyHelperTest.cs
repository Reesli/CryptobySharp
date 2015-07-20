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
using System.Text;

namespace CryptobySharp
{
	/// <author>Toby</author>
	public class CryptobyHelperTest
	{
		/// <summary>Test of charToBlockString method, of class CryptobyHelper.</summary>
		/// <remarks>Test of charToBlockString method, of class CryptobyHelper.</remarks>
		[NUnit.Framework.Test]
		public virtual void testCharToBlockString()
		{
			System.Console.Out.WriteLine("charToBlockString");
			byte[] cryptByte = Encoding.UTF8.GetBytes("TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"
				);
			char[] charTextHex = CryptobyHelper.bytesToHexStringUpper(cryptByte).ToCharArray(
				);
			string result = CryptobyHelper.charToBlockString(charTextHex);
			System.Console.Out.WriteLine(result);
		}

		/// <summary>Test of printHexBlock method, of class CryptobyHelper.</summary>
		/// <remarks>Test of printHexBlock method, of class CryptobyHelper.</remarks>
		[NUnit.Framework.Test]
		public virtual void testPrintHexBlock()
		{
			System.Console.Out.WriteLine("printHexBlock");
			byte[] cryptByte = Encoding.UTF8.GetBytes("TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"
				);
			char[] inputCharTextHex = CryptobyHelper.bytesToHexStringUpper(cryptByte).ToCharArray
				();
			string cryptType = "RSA";
			int inputKeySize = 1024;
			string result = CryptobyHelper.printHexBlock(cryptType, inputKeySize, inputCharTextHex
				);
			System.Console.Out.WriteLine(result);
		}

		/// <summary>Test of printPrivateKeyBlock method, of class CryptobyHelper.</summary>
		/// <remarks>Test of printPrivateKeyBlock method, of class CryptobyHelper.</remarks>
		[NUnit.Framework.Test]
		public virtual void testPrintPrivateKeyBlock()
		{
			System.Console.Out.WriteLine("printPrivateKeyBlock");
			string privateKey = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest";
			string result = CryptobyHelper.printPrivateKeyBlock(privateKey);
			System.Console.Out.WriteLine(result);
		}

		/// <summary>Test of printPublicKeyBlock method, of class CryptobyHelper.</summary>
		/// <remarks>Test of printPublicKeyBlock method, of class CryptobyHelper.</remarks>
		[NUnit.Framework.Test]
		public virtual void testPrintPublicKeyBlock()
		{
			System.Console.Out.WriteLine("printPublicKeyBlock");
			string publicKey = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest";
			string result = CryptobyHelper.printPublicKeyBlock(publicKey);
			System.Console.Out.WriteLine(result);
		}

		/// <summary>Test of getEOBString method, of class CryptobyHelper.</summary>
		/// <remarks>Test of getEOBString method, of class CryptobyHelper.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGetEOBString()
		{
			System.Console.Out.WriteLine("getEOBString");
			string expResult = string.Empty;
			string result = CryptobyHelper.getEOBString();
		}
	}
}
