using NUnit.Framework;
using System;
using System.Security.Cryptography;

namespace CryptobySharp
{
	public class TestSecRandC
	{
		const int cycles = 10000;
		private static RNGCryptoServiceProvider secRand; 
		byte[] big512;
		byte[] big1024;
		byte[] big2048;

		[TestFixtureSetUp] public void Init()
		{ 
			secRand = new RNGCryptoServiceProvider();
			big512 = new byte[64];
			big1024 = new byte[128];
			big2048 = new byte[256];
		}

		public void testSecRand512() {
			for(int i = 0; i < cycles; i++)
			{
				secRand.GetBytes (big512);
			}
		}

		public void testSecRand1024() {
			for(int i = 0; i < cycles; i++)
			{
				secRand.GetBytes (big1024);
			}
		}

		public void testSecRand2048() {
			for(int i = 0; i < cycles; i++)
			{
				secRand.GetBytes (big2048);
			}
		}

		[Test]
		public void testRun(){
			int rounds = 20;

			Console.WriteLine ("Test Performance C# SecureRandom");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round" + cycles);
			Console.WriteLine ("\nGenerate Secure Random 512 Bit");
			PerfMeter.run(new Action(testSecRand512),rounds);
			Console.WriteLine ("\nGenerate Secure Random 1024 Bit");
			PerfMeter.run(new Action(testSecRand1024),rounds);
			Console.WriteLine ("\nGenerate Secure Random 2048 Bit");
			PerfMeter.run(new Action(testSecRand2048),rounds);
		}

	}
}

