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
using java.math;

namespace CryptobySharp
{
	/// <summary>Interface for prime test implementations.</summary>
	/// <remarks>Interface for prime test implementations.</remarks>
	/// <author>Tobias Rees</author>
	public interface PrimeTest
	{
		/// <summary>In this method the input number will be tested.</summary>
		/// <remarks>In this method the input number will be tested.</remarks>
		/// <param name="number">Input number to test</param>
		/// <returns>True or false if the number is a prime</returns>
		bool isPrime(BigInteger number);

		/// <summary>Get probability that input number in isPrime method is a prime.</summary>
		/// <remarks>Get probability that input number in isPrime method is a prime.</remarks>
		/// <returns>Return probability in percent as double</returns>
		double getProbability();
	}
}
