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
	/// <summary>
	/// This class provide an implementation of the class Thread to generate big
	/// prime numbers in parallel mode.
	/// </summary>
	/// <remarks>
	/// This class provide an implementation of the class Thread to generate big
	/// prime numbers in parallel mode.
	/// </remarks>
	/// <author>Tobias Rees</author>
	public class GenPrimeThread
	{
		private readonly SecureRandom scRandom;

		private BigInteger prime;

		private readonly CryptobyCore core;

		private readonly int halfKeyBitSize;

		private readonly int keyByteSize;

		/// <summary>Constructor sets variables and initializes the SecureRandom object.</summary>
		/// <remarks>Constructor sets variables and initializes the SecureRandom object.</remarks>
		/// <param name="appCore">Input CryptobyCore object to get a primetest object</param>
		/// <param name="keyBitSize">
		/// With the half size of this key will generate be a prime
		/// number in the run method
		/// </param>
		public GenPrimeThread(CryptobyCore appCore, int keyBitSize)
		{
			scRandom = new SecureRandom();
			halfKeyBitSize = keyBitSize / 2;
			keyByteSize = halfKeyBitSize / 8;
			core = appCore;
		}

		/// <summary>Generate a prime number with half size of keyBitSize</summary>
		public void run()
		{
			do
			{
				prime = new BigInteger(halfKeyBitSize - 1, 1, scRandom);
			}
			while (!(core.getPrimetest().isPrime(prime)) || prime.toByteArray().Length != keyByteSize);
		}

		//prime = new BigInteger(halfKeyBitSize - 1, scRandom);
		//scRandom.nextBytes(bytes);
		//prime = new BigInteger(bytes);
		//System.out.println("Loop: "+prime.bitLength());
		/// <summary>Get generated BigInteger prime number</summary>
		/// <returns>Return generated prime number as BigInteger</returns>
		public virtual BigInteger getPrime()
		{
			return prime;
		}
	}
}
