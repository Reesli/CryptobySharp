
using java.security;
using java.math;

namespace CryptobySharp
{
	public class testBigInteger
	{
		
		public void testBigIntRandPrime512() {
			SecureRandom scRandom = new SecureRandom();
			for (int i = 0; i < 1; i++) {
				BigInteger testBigInt = new BigInteger(512, 1, scRandom);
			}
		}

		[NUnit.Framework.Test]
		public void testSpeed(){
			// initialize a new test group
			SpeedTester myTest = new SpeedTester(testBigIntRandPrime512);

			myTest.RunTest (1);
			System.Console.WriteLine (myTest.AverageRunningTime);
		}
	}
}

