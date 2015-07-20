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
using java.util;
using System.Text;

namespace CryptobySharp
{
	/// <author>Tobias Rees</author>
	public class CryptRSATest
	{
		/// <summary>Test one Block with 1024Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt1024_oneBlock()
		{
			System.Console.Out.WriteLine("RSACrypt1024oneBlock");
			int keySize = 1024;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			generator.initGenerator(keySize);
			string smallString = "Text to Test for Testing from Tester by Testcase." + "Text to Test for Testing from Tester by Testcase.Text to Test";
			byte[] plainInput = Encoding.UTF8.GetBytes(smallString);
			byte[] publicKey = generator.getPublicKeyByte();
			byte[] privateKey = generator.getPrivateKeyByte();
			CryptRSA rsa = new CryptRSA();
			byte[] expResult = plainInput;
			byte[] result = rsa.encrypt(plainInput, publicKey);
			result = rsa.decrypt(result, privateKey);
			Assert.AreEqual(expResult, result);
		}

		/// <summary>Test longer String with 1024Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt1024_longString()
		{
			System.Console.Out.WriteLine("RSACrypt1024longString");
			int keySize = 1024;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			generator.initGenerator(keySize);
			string longString = "Warmly little before cousin sussex entire men set. " + "Blessing it ladyship on sensible judgment settling outweigh. "
				 + "Worse linen an of civil jokes leave offer. Parties all clothes" + " removal cheered calling prudent her. And residence for met "
				 + "the estimable disposing. Mean if he they been no hold mr. Is " + "at much do made took held help. Latter person am secure of "
				 + "estate genius at.Six started far placing saw respect females " + "old. Civilly why how end viewing attempt related enquire visitor."
				 + " Man particular insensible celebrated conviction stimulated " + "principles day. Sure fail or in said west. Right my front it "
				 + "wound cause fully am sorry if. She jointure goodness interest " + "debating did outweigh. Is time from them full my gone in went."
				 + " Of no introduced am literature excellence mr stimulated " + "contrasted increasing. Age sold some full like rich new. "
				 + "Amounted repeated as believed in confined juvenile.Started his" + " hearted any civilly. So me by marianne admitted speaking. "
				 + "Men bred fine call ask. Cease one miles truth day above seven. " + "Suspicion sportsmen provision suffering mrs saw engrossed something. "
				 + "Snug soon he on plan in be dine some.";
			byte[] plainInput = Encoding.UTF8.GetBytes(longString);
			byte[] publicKey = generator.getPublicKeyByte();
			byte[] privateKey = generator.getPrivateKeyByte();
			CryptRSA rsa = new CryptRSA();
			byte[] expResult = plainInput;
			byte[] enc = rsa.encrypt(plainInput, publicKey);
			byte[] result = rsa.decrypt(enc, privateKey);
			Assert.AreEqual(expResult, result);
		}

		/// <summary>Test big Block (10000 Bytes) with 1024Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt1024_BiggerBlock()
		{
			System.Console.Out.WriteLine("RSACrypt1024Bigger");
			int keySize = 1024;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			java.util.Random rand = new java.util.Random();
			for(int i = 1;i<50;i++){
				byte[] expResult = new byte[i * 100];
				rand.nextBytes(expResult);
				KeyGenRSA generator = new KeyGenRSA(core);
				generator.initGenerator(keySize);
				byte[] publicKey = generator.getPublicKeyByte();
				byte[] privateKey = generator.getPrivateKeyByte();
				CryptRSA rsa = new CryptRSA();
				byte[] encres = rsa.encrypt (expResult, publicKey);
				byte[] encres2 = rsa.encrypt (expResult, publicKey);;
				byte[] result = rsa.decrypt(encres, privateKey);
				byte[] result2 = rsa.decrypt(encres2, privateKey);
				Assert.AreEqual(result, result2);
				Assert.AreEqual(expResult, result);
			}


		}

		/// <summary>Test small String with false 1024Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt1024false()
		{
			System.Console.Out.WriteLine("RSACrypt1024false");
			int keySize = 1024;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			generator.initGenerator(keySize);
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			string publicKeyString = generator.getPublicKey();
			byte[] publicKey = CryptobyHelper.hexStringToBytes(publicKeyString);
			generator.initGenerator(keySize);
			string privateKeyString = generator.getPrivateKey();
			byte[] privateKey = CryptobyHelper.hexStringToBytes(privateKeyString);
			CryptRSA rsa = new CryptRSA();
			byte[] expResult = plainInput;
			byte[] result = rsa.encrypt(plainInput, publicKey);
			result = rsa.decrypt(result, privateKey);
			NUnit.Framework.Assert.IsFalse(Arrays.equals(expResult, result));
		}

		/// <summary>Test small String with 2048Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt2048()
		{
			System.Console.Out.WriteLine("RSACrypt2048");
			int keySize = 2048;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			generator.initGenerator(keySize);
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			string publicKeyString = generator.getPublicKey();
			byte[] publicKey = CryptobyHelper.hexStringToBytes(publicKeyString);
			string privateKeyString = generator.getPrivateKey();
			byte[] privateKey = CryptobyHelper.hexStringToBytes(privateKeyString);
			CryptRSA rsa = new CryptRSA();
			byte[] expResult = plainInput;
			byte[] result = rsa.encrypt(plainInput, publicKey);
			result = rsa.decrypt(result, privateKey);
			Assert.AreEqual(expResult, result);
		}

		/// <summary>Test small String with 4096Bit Key</summary>
		[NUnit.Framework.Test]
		public virtual void testRSACrypt4096()
		{
			System.Console.Out.WriteLine("RSACrypt4096");
			int keySize = 4096;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			generator.initGenerator(keySize);
			byte[] plainInput = Encoding.UTF8.GetBytes("Text to Test for Testing from Tester by Testcase"
				);
			string publicKeyString = generator.getPublicKey();
			byte[] publicKey = CryptobyHelper.hexStringToBytes(publicKeyString);
			string privateKeyString = generator.getPrivateKey();
			byte[] privateKey = CryptobyHelper.hexStringToBytes(privateKeyString);
			CryptRSA rsa = new CryptRSA();
			byte[] expResult = plainInput;
			byte[] result = rsa.encrypt(plainInput, publicKey);
			result = rsa.decrypt(result, privateKey);
			Assert.AreEqual(expResult, result);
		}
	}
}
