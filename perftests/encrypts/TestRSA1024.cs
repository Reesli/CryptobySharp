using System;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestRSA1024
	{
		int cycles = 1;
		byte[] preData;
		byte[] modData;
		byte[] resData;
		int kb100;
		int kb500;
		int mb1;
		int keySize = 1024;
		byte[] pubKey;
		byte[] privKey;

		CryptobyClient client;
		CryptobyCore core;
		KeyGenRSA generator;
		CryptRSA rsa;

		[TestFixtureSetUp] public void Init()
		{ 
			kb100 = 1024 * 100;
			kb500 = kb100 * 5;
			mb1 = kb100 * 10;
			client = new CryptobyClient();
			core = new CryptobyCore(client);
			generator = new KeyGenRSA(core);
			rsa = new CryptRSA();
		}

		public void testRSA1024Enc() {
			modData = rsa.encrypt(preData, pubKey);
		}

		public void testRSA1024Dec() {
			resData = rsa.decrypt(modData, privKey);
		}

		[Test]
		public void testRun(){
			int rounds = 1;

			Console.WriteLine ("Test Performance Cryptoby FileManager");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			generator.initGenerator(keySize);
			pubKey = generator.getPublicKeyByte();
			privKey = generator.getPrivateKeyByte();

			preData = new byte[kb100];
			Console.WriteLine ("\nEncrypt RSA 1024 Bit 100KB");
			PerfMeter.run(new Action(testRSA1024Enc),rounds);
			Console.WriteLine ("\nDecrypt RSA 1024 Bit 100KB");
			PerfMeter.run(new Action(testRSA1024Dec),rounds);
			Assert.AreEqual (preData,resData);

//			modData = null;
//			resData = null;
//			preData = new byte[kb500];
//			Console.WriteLine ("\nEncrypt RSA 1024 Bit 500KB");
//			PerfMeter.run(new Action(testRSA1024Enc),rounds);
//			Console.WriteLine ("\nDecrypt RSA 1024 Bit 500KB");
//			PerfMeter.run(new Action(testRSA1024Dec),rounds);
//			Assert.AreEqual (preData,resData);
//
//			modData = null;
//			resData = null;
//			preData = new byte[mb1];
//			Console.WriteLine ("\nEncrypt RSA 1024 Bit 1MB");
//			PerfMeter.run(new Action(testRSA1024Enc),rounds);
//			Console.WriteLine ("\nDecrypt RSA 1024 Bit 1MB");
//			PerfMeter.run(new Action(testRSA1024Dec),rounds);
//			Assert.AreEqual (preData,resData);
		}
	}
}

