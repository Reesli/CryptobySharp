using System;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestAES256
	{
		const int cycles = 1;
		byte[] preData;
		byte[] modData;
		byte[] resData;
		int mb1;
		int mb5;
		int mb10;
		const int keySize = 256;
		byte[] privKey;

		CryptAES aes;

		[TestFixtureSetUp] public void Init()
		{ 
			mb1 = 1024 * 1024;
			mb5 = mb1 * 5;
			mb10 = mb1 * 10;
			aes = new CryptAES();
		}

		public void testAESEnc() {
			modData = aes.encrypt(preData, privKey);
		}

		public void testAESDec() {
			resData = aes.decrypt(modData, privKey);
		}

		[Test]
		public void testRun(){
			const int rounds = 3;

			Console.WriteLine ("Test Performance AES Data Enc/Dec");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			KeyGenSHA3 keyGen = new KeyGenSHA3();
			privKey = keyGen.generateKeyByte(keySize, "password");

			preData = new byte[mb1];
			Console.WriteLine ("\nEncrypt AES 256 Bit 1MB");
			PerfMeter.run(new Action(testAESEnc),rounds);
			Console.WriteLine ("\nDecrypt AES 256 Bit 1MB");
			PerfMeter.run(new Action(testAESDec),rounds);
			Assert.AreEqual (preData,resData);

			preData = new byte[mb5];
			Console.WriteLine ("\nEncrypt AES 256 Bit 5MB");
			PerfMeter.run(new Action(testAESEnc),rounds);
			Console.WriteLine ("\nDecrypt AES 256 Bit 5MB");
			PerfMeter.run(new Action(testAESDec),rounds);
			Assert.AreEqual (preData,resData);

			preData = new byte[mb10];
			Console.WriteLine ("\nEncrypt AES 256 Bit 10MB");
			PerfMeter.run(new Action(testAESEnc),rounds);
			Console.WriteLine ("\nDecrypt AES 256 Bit 10MB");
			PerfMeter.run(new Action(testAESDec),rounds);
			Assert.AreEqual (preData,resData);
		}
	}
}
	