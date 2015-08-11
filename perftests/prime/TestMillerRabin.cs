using System;
using NUnit.Framework;
using java.math;

namespace CryptobySharp
{
	public class TestMillerRabin
	{
		const int cycles = 1;
		MillerRabin ptest;
		BigInteger prime;
		const int round5 = 5;
		const int round10 = 10;
		const int round15 = 15;
		bool bolprime = false;

		[TestFixtureSetUp] public void Init()
		{ 
			
		}

		public void testPrimeProbe() {
			for(int i = 0; i < cycles; i++){
				bolprime = ptest.isPrime (prime);
			}
		}

		[Test]
		public void testRun(){
			const int rounds = 1;

			Console.WriteLine ("Test Performance of Miller Rabin Prime Probe");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);


			prime = BigPrimes.Dig100;
			ptest = new MillerRabin (round5);
			Console.WriteLine ("\nTest 100 Number Prime 5 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round10);
			Console.WriteLine ("\nTest 100 Number Prime 10 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round15);
			Console.WriteLine ("\nTest 100 Number Prime 15 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			Assert.AreEqual(prime.isProbablePrime(round15),bolprime);


			prime = BigPrimes.Dig500;
			ptest = new MillerRabin (round5);
			Console.WriteLine ("\nTest 5000 Number Prime 5 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round10);
			Console.WriteLine ("\nTest 5000 Number Prime 10 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round15);
			Console.WriteLine ("\nTest 5000 Number Prime 15 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			Assert.AreEqual(prime.isProbablePrime(round15),bolprime);

			prime = BigPrimes.Dig1000;
			ptest = new MillerRabin (round5);
			Console.WriteLine ("\nTest 10000 Number Prime 5 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round10);
			Console.WriteLine ("\nTest 10000 Number Prime 10 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			ptest = new MillerRabin (round15);
			Console.WriteLine ("\nTest 10000 Number Prime 15 Rounds");
			PerfMeter.run(new Action(testPrimeProbe),rounds);
			Assert.AreEqual(prime.isProbablePrime(round15),bolprime);

		}


	}
}
	