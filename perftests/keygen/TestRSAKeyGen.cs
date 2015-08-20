using System;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestRSAKeyGen
	{
		const int cycles = 1;
		int keySize;
		byte[] privKey;
		byte[] pubKey;

		KeyGenRSA keyGen;

		[TestFixtureSetUp] public void Init()
		{ 
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			keyGen = new KeyGenRSA (core);
		}

		public void testGenRSAKey() {
			keyGen.initGenerator (keySize);
		}

		[Test]
		public void testRun(){
			const int rounds = 50;

			Console.WriteLine ("Test Performance RSA Key Generation");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			keySize = 1024;
			Console.WriteLine ("\nGenerate RSA Key 1024 Bit");
			PerfMeter.run(new Action(testGenRSAKey),rounds);
			privKey = keyGen.getPrivateKeyByte();
			pubKey = keyGen.getPublicKeyByte();
			Assert.AreEqual(privKey.Length*4,keySize );
			Assert.AreEqual(pubKey.Length*8,keySize );

			keySize = 2048;
			Console.WriteLine ("\nGenerate RSA Key 2048 Bit");
			PerfMeter.run(new Action(testGenRSAKey),rounds);
			privKey = keyGen.getPrivateKeyByte();
			pubKey = keyGen.getPublicKeyByte();
			Assert.AreEqual(privKey.Length*4,keySize );
			Assert.AreEqual(pubKey.Length*8,keySize );

			keySize = 4096;
			Console.WriteLine ("\nGenerate RSA Key 4096 Bit");
			PerfMeter.run(new Action(testGenRSAKey),rounds);
			privKey = keyGen.getPrivateKeyByte();
			pubKey = keyGen.getPublicKeyByte();
			Assert.AreEqual(privKey.Length*4,keySize );
			Assert.AreEqual(pubKey.Length*8,keySize );

		}
	}
}
	