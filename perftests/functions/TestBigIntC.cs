using NUnit.Framework;
using System;
using System.Numerics;

namespace CryptobySharp
{
	public class TestBigIntC
	{
		const int cycles = 100000;
		byte[] big512;
		byte[] big1024;
		byte[] big2048;

		[TestFixtureSetUp] public void Init()
		{ 
			big512 = new byte[64];
			big1024 = new byte[128];
			big2048 = new byte[256];
		}

		public void testBigInt512() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger testBigInt = new BigInteger(big512);
			}
		}

		public void testBigInt1024() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger testBigInt = new BigInteger(big1024);
			}
		}

		public void testBigInt2048() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger testBigInt = new BigInteger(big2048);
			}
		}

		[Test]
		public void testRun(){
			int rounds = 20;

			Console.WriteLine ("Test Performance C# BigInteger Generation");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round" + cycles);
			Console.WriteLine ("\nGenerate BigInteger 512 Bit");
			PerfMeter.run(new Action(testBigInt512),rounds);
			Console.WriteLine ("\nGenerate BigInteger 1024 Bit");
			PerfMeter.run(new Action(testBigInt1024),rounds);
			Console.WriteLine ("\nGenerate BigInteger 2048 Bit");
			PerfMeter.run(new Action(testBigInt2048),rounds);
		}

	}
}

