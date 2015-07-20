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
using java.util;


namespace CryptobySharp
{
	/// <summary>This class provides menus in console UI for Miller Rabin implementation.
	/// 	</summary>
	/// <remarks>This class provides menus in console UI for Miller Rabin implementation.
	/// 	</remarks>
	/// <author>Tobias Rees</author>
	public class MillerRabinUI
	{
		/// <param name="console"></param>
		public static void testMillerRabin(CryptobyConsole console)
		{
			Scanner scanner = new Scanner(java.lang.System.@in);
			// Initial Variables
			int rounds;
			string percent;
			BigInteger number;
			do
			{
				// Input Number for Primenumber Testing
				System.Console.Out.WriteLine("Set Primenumber to Test.");
				System.Console.Out.Write("Please enter a positive number: ");
				while (!scanner.hasNextBigInteger())
				{
					System.Console.Out.Write("That's not a number! Enter a positive number: ");
					scanner.next();
				}
				number = scanner.nextBigInteger();
			}
			while (number.compareTo(BigInteger.ONE) < 0);
			do
			{
				// Set the rounds of the Miller Rabin Test
				System.Console.Out.WriteLine("Set rounds parameter between 1 and 15.");
				System.Console.Out.Write("Please enter the number of rounds: ");
				while (!scanner.hasNextInt())
				{
					System.Console.Out.Write("That's not a number! Enter a valid number: ");
					scanner.next();
				}
				rounds = scanner.nextInt();
			}
			while (rounds < 1 || rounds > 15);
			// Initial Miller Rabin Object
			console.getCore().getClient().setPrimTestArt("MillerRabin");
			console.getCore().getClient().setPrimetestrounds(rounds);
			console.getCore().initPrimeTest();
			// Get Result of Test
			if (console.getCore().getPrimetest().isPrime(number))
			{
				percent = console.getCore().getPrimetest().getProbability().ToString();
				System.Console.Out.WriteLine("\nResult: Number is probably a Primenumber, probability: "
					 + percent + "%");
			}
			else
			{
				System.Console.Out.WriteLine("\nResult: Number is NOT a Primenumber");
			}
			// Back to Menu Choose PrimeTest
			System.Console.Out.WriteLine("\nGo back to Primetest Menu: Press Enter");
			CryptobyHelper.pressEnter();
			console.menuPrimeTest();
		}
	}
}
