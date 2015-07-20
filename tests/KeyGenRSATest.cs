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

namespace CryptobySharp
{
	/// <author>Tobias Rees</author>
	public class KeyGenRSATest
	{
		internal int rounds;

		public KeyGenRSATest()
		{
			rounds = 10;
		}

		/// <summary>Test of genPrivateKey method, of class KeyGenRSA.</summary>
		/// <remarks>Test of genPrivateKey method, of class KeyGenRSA.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenPrivatePublicKey1024()
		{
			for (int i = 0; i < rounds; i++)
			{
				System.Console.Out.WriteLine("genKey1024bit");
				int keySize = 1024;
				CryptobyClient client = new CryptobyClient();
				CryptobyCore core = new CryptobyCore(client);
				KeyGenRSA instance = new KeyGenRSA(core);
				instance.initGenerator(keySize);
				string resultPriv = instance.getPrivateKey();
				string resultPub = instance.getPublicKey();
				byte[] publicKey = CryptobyHelper.hexStringToBytes(resultPub);
				byte[] privateKey = CryptobyHelper.hexStringToBytes(resultPriv);
				NUnit.Framework.Assert.IsTrue(publicKey.Length == 128);
				NUnit.Framework.Assert.IsTrue(privateKey.Length == 256);
			}
		}

		/// <summary>Test of genPrivateKey method, of class KeyGenRSA.</summary>
		/// <remarks>Test of genPrivateKey method, of class KeyGenRSA.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGenPrivatePublicKey2048()
		{
			for (int i = 0; i < rounds; i++)
			{
				System.Console.Out.WriteLine("genKey2048bit");
				int keySize = 2048;
				CryptobyClient client = new CryptobyClient();
				CryptobyCore core = new CryptobyCore(client);
				KeyGenRSA instance = new KeyGenRSA(core);
				instance.initGenerator(keySize);
				string resultPriv = instance.getPrivateKey();
				string resultPub = instance.getPublicKey();
				byte[] publicKey = CryptobyHelper.hexStringToBytes(resultPub);
				byte[] privateKey = CryptobyHelper.hexStringToBytes(resultPriv);
				NUnit.Framework.Assert.IsTrue(publicKey.Length == 256);
				NUnit.Framework.Assert.IsTrue(privateKey.Length == 512);
			}
		}
	}
}
