using NUnit.Framework;
using System;
using java.security;
using java.math;

namespace CryptobySharp
{
	public class TestRandPrimJava
	{
		const int cycles = 1;
		SecureRandom secRand;
		const int big512 = 512;
		const int big1024 = 1024;
		const int big2048 = 2048;

		[TestFixtureSetUp] public void Init()
		{ 
			secRand = new SecureRandom ();
		}

		public void testRandPrim512() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger bigPrime = new BigInteger (big512,1,secRand);
			}
		}

		public void testRandPrim1024() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger bigPrime = new BigInteger (big1024,1,secRand);
			}
		}

		public void testRandPrim2048() {
			for(int i = 0; i < cycles; i++)
			{
				BigInteger bigPrime = new BigInteger (big2048,5,secRand);
			}
		}

		[Test]
		public void testRun(){
			int rounds = 20;

			Console.WriteLine ("Test Performance OpenJDK BigInteger Primes with SecureRandom");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round" + cycles);
			Console.WriteLine ("\nGenerate Secure Random Prime 512 Bit");
			PerfMeter.run(new Action(testRandPrim512),rounds);
			Console.WriteLine ("\nGenerate Secure Random Prime 1024 Bit");
			PerfMeter.run(new Action(testRandPrim1024),rounds);
			Console.WriteLine ("\nGenerate Secure Random Prime 2048 Bit");
			PerfMeter.run(new Action(testRandPrim2048),rounds);
		}

	}
}

