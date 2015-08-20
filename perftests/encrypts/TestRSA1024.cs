using System;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestRSA1024
	{
		const int cycles = 1;
		byte[] preData;
		byte[] modData;
		byte[] resData;
		int kb10;
		int kb50;
		int kb100;
		const int keySize = 1024;
		byte[] pubKey;
		byte[] privKey;

		Random rand;
		CryptobyClient client;
		CryptobyCore core;
		KeyGenRSA keyGen;
		CryptRSA rsa;

		[TestFixtureSetUp] public void Init()
		{ 
			kb10 = 1024 * 10;
			kb50 = kb10 * 5;
			kb100 = kb10 * 10;
			client = new CryptobyClient();
			core = new CryptobyCore(client);
			keyGen = new KeyGenRSA(core);
			rsa = new CryptRSA();
			rand = new Random ();
		}

		public void testRSAEnc() {
			modData = rsa.encrypt(preData, pubKey);
		}

		public void testRSADec() {
			resData = rsa.decrypt(modData, privKey);
		}

		[Test]
		public void testRun(){
			const int rounds = 50;

			Console.WriteLine ("Test Performance RSA Data Enc/Dec");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			keyGen.initGenerator(keySize);
			pubKey = keyGen.getPublicKeyByte();
			privKey = keyGen.getPrivateKeyByte();

			preData = new byte[kb10];
			rand.NextBytes (preData);
			Console.WriteLine ("\nEncrypt RSA 1024 Bit 10KB");
			PerfMeter.run(new Action(testRSAEnc),rounds);
			Console.WriteLine ("\nDecrypt RSA 1024 Bit 10KB");
			PerfMeter.run(new Action(testRSADec),rounds);
			Assert.AreEqual (preData,resData);

			modData = null;
			resData = null;
			preData = new byte[kb50];
			rand.NextBytes (preData);
			Console.WriteLine ("\nEncrypt RSA 1024 Bit 50KB");
			PerfMeter.run(new Action(testRSAEnc),rounds);
			Console.WriteLine ("\nDecrypt RSA 1024 Bit 50KB");
			PerfMeter.run(new Action(testRSADec),rounds);
			Assert.AreEqual (preData,resData);

			modData = null;
			resData = null;
			preData = new byte[kb100];
			rand.NextBytes (preData);
			Console.WriteLine ("\nEncrypt RSA 1024 Bit 100KB");
			PerfMeter.run(new Action(testRSAEnc),rounds);
			Console.WriteLine ("\nDecrypt RSA 1024 Bit 100KB");
			PerfMeter.run(new Action(testRSADec),rounds);
			Assert.AreEqual (preData,resData);
		}
	}
}
	