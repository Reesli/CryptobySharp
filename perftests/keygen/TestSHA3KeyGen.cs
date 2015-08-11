using System;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestSHA3KeyGen
	{
		const int cycles = 10000;
		int keySize;
		byte[] privKey;

		KeyGenSHA3 keyGen;

		[TestFixtureSetUp] public void Init()
		{ 
			keyGen = new KeyGenSHA3 ();
		}

		public void testGenSHA3Key() {
			for(int i = 0; i < cycles; i++){
				privKey = keyGen.generateKeyByte (keySize,"password");
			}
		}

		[Test]
		public void testRun(){
			const int rounds = 1;

			Console.WriteLine ("Test Performance SHA3 Key Generation");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			keySize = 224;
			Console.WriteLine ("\nGenerate SHA3 Key 224 Bit");
			PerfMeter.run(new Action(testGenSHA3Key),rounds);
			Assert.AreEqual(privKey.Length*8,keySize );

			keySize = 256;
			Console.WriteLine ("\nGenerate SHA3 Key 256 Bit");
			PerfMeter.run(new Action(testGenSHA3Key),rounds);
			Assert.AreEqual(privKey.Length*8,keySize );

			keySize = 384;
			Console.WriteLine ("\nGenerate SHA3 Key 384 Bit");
			PerfMeter.run(new Action(testGenSHA3Key),rounds);
			Assert.AreEqual(privKey.Length*8,keySize );

			keySize = 512;
			Console.WriteLine ("\nGenerate SHA3 Key 512 Bit");
			PerfMeter.run(new Action(testGenSHA3Key),rounds);
			Assert.AreEqual(privKey.Length*8,keySize );
		}
	}
}
	