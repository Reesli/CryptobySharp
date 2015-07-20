/*
 * Copyright (C) 2014 Toby
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using java.security;
using java.math;

namespace CryptobySharp
{
	/// <summary>This class provides the implementation of the Miller Rabin prime number test.
	/// 	</summary>
	/// <remarks>This class provides the implementation of the Miller Rabin prime number test.
	/// 	</remarks>
	/// <author>Tobias Rees</author>
	public class MillerRabin : PrimeTest
	{
		private static readonly BigInteger TWO = BigInteger.valueOf(2);

		private readonly int rounds;

		private double probability;

		/// <summary>
		/// Constructor needs input for number of rounds which will be used in
		/// isPrime method to increase probability that the number is prime.
		/// </summary>
		/// <remarks>
		/// Constructor needs input for number of rounds which will be used in
		/// isPrime method to increase probability that the number is prime.
		/// </remarks>
		/// <param name="rounds"></param>
		public MillerRabin(int rounds)
		{
			this.rounds = rounds;
		}

		/// <summary>Test the input number to be probability a prime number.</summary>
		/// <remarks>Test the input number to be probability a prime number.</remarks>
		/// <param name="number">Number for prime test</param>
		/// <returns>
		/// Return true is a probability a prime and false if sure not a
		/// prime
		/// </returns>
		public virtual bool isPrime(BigInteger number)
		{
			bool result = false;
			SecureRandom random = new SecureRandom();
			for (int i = 0; i < this.rounds; i++)
			{
				result = runMillerRabin(number, random);
			}
			if (result == false)
			{
				this.probability = 0;
			}
			else
			{
				this.probability = this.calcProbability(this.rounds);
			}
			return result;
		}

		/// <summary>Get probability that's the number is prime in percent.</summary>
		/// <remarks>Get probability that's the number is prime in percent.</remarks>
		/// <returns>Return probability as double</returns>
		public virtual double getProbability()
		{
			return this.probability;
		}

		private static bool runMillerRabin(BigInteger number, SecureRandom random
			)
		{
			if (number.compareTo(BigInteger.valueOf(3)) <= 0)
			{
				return number.compareTo(BigInteger.ONE) != 0;
			}
			// Ensures that temp > 1 and temp < n.
			BigInteger temp = BigInteger.ZERO;
			do
			{
				temp = new BigInteger(number.bitLength() - 1, random);
			}
			while (temp.compareTo(BigInteger.ONE) <= 0);
			// Screen out n if our random number happens to share a factor with n.
			if (!number.gcd(temp).Equals(BigInteger.ONE))
			{
				return false;
			}
			// For debugging, prints out the integer to test with.
			//System.out.println("Testing with " + temp);
			BigInteger d = number.subtract(BigInteger.ONE);
			// Figure s and d Values
			int s = 0;
			while ((d.mod(TWO)).Equals(BigInteger.ZERO))
			{
				d = d.divide(TWO);
				s++;
			}
			BigInteger curValue = temp.modPow(d, number);
			// If this works out, it's a prime
			if (curValue.Equals(BigInteger.ONE))
			{
				return true;
			}
			// Otherwise, we will check to see if this value successively 
			// squared ever yields -1.
			for (int r = 0; r < s; r++)
			{
				// We need to really check n-1 which is equivalent to -1.
				if (curValue.Equals(number.subtract(BigInteger.ONE)))
				{
					return true;
				}
				else
				{
					// Square this previous number - here I am just doubling the 
					// exponent. A more efficient implementation would store the
					// value of the exponentiation and square it mod n.
					curValue = curValue.modPow(TWO, number);
				}
			}
			// If none of our tests pass, we return false. The number is 
			// definitively composite if we ever get here.
			return false;
		}

		private double calcProbability(int rounds)
		{
			return 100 - (1 / (java.lang.Math.pow(4, rounds))) * 100;
		}
	}
}
