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
	/// <author>Tobias Rees</author>
	public class KeyGenSHA3Test
	{
		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_random_length224()
		{
			System.Console.Out.WriteLine("generate random 224bit Key");
			int keySize = 224;
			KeyGenSHA3 instance = new KeyGenSHA3();
			int expResult = 224;
			int result = Encoding.UTF8.GetBytes(instance.generateKey(keySize)).Length
				 * 4;
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.AreEqual(expResult, result);
		}

		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_random_length256()
		{
			System.Console.Out.WriteLine("generate random 256bit Key");
			int keySize = 256;
			KeyGenSHA3 instance = new KeyGenSHA3();
			int expResult = 256;
			int result = Encoding.UTF8.GetBytes(instance.generateKey(keySize)).Length
				 * 4;
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.AreEqual(expResult, result);
		}

		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_random_length384()
		{
			System.Console.Out.WriteLine("generate random 384bit Key");
			int keySize = 384;
			KeyGenSHA3 instance = new KeyGenSHA3();
			int expResult = 384;
			int result = Encoding.UTF8.GetBytes(instance.generateKey(keySize)).Length
				 * 4;
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.AreEqual(expResult, result);
		}

		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_random_length512()
		{
			System.Console.Out.WriteLine("generate random 512bit Key");
			int keySize = 512;
			KeyGenSHA3 instance = new KeyGenSHA3();
			int expResult = 512;
			int result = Encoding.UTF8.GetBytes(instance.generateKey(keySize)).Length
				 * 4;
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.AreEqual(expResult, result);
		}

		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_String()
		{
			System.Console.Out.WriteLine("generate 256bit Key and comparse with given Key");
			int keySize = 256;
			string password = "testest";
			KeyGenSHA3 instance = new KeyGenSHA3();
			string expResult = "e195622d04525e14469076f4175b990a72995ea7c9f379c465670c330b4f8b60";
			string result = instance.generateKey(keySize, password);
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.AreEqual(expResult, result);
		}

		/// <summary>Test of generateKey method, of class KeyGenSHA3.</summary>
		/// <remarks>Test of generateKey method, of class KeyGenSHA3.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenerateKey_int_String_false()
		{
			System.Console.Out.WriteLine("generate 256bit false Key and comparse with given Key"
				);
			int keySize = 256;
			string password = "falsePW";
			KeyGenSHA3 instance = new KeyGenSHA3();
			string expResult = "e195622d04525e14469076f4175b990a72995ea7c9f379c465670c330b4f8b60";
			string result = instance.generateKey(keySize, password);
			System.Console.Out.WriteLine(result);
			NUnit.Framework.Assert.IsFalse(expResult.Equals(result));
		}
	}
}
