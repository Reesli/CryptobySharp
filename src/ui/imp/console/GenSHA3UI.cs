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
using System;

using java.util;

namespace CryptobySharp
{
	/// <summary>This class provides menus in console UI for SHA3 implementation.</summary>
	/// <remarks>This class provides menus in console UI for SHA3 implementation.</remarks>
	/// <author>Tobias Rees</author>
	public class GenSHA3UI
	{
		private static readonly string quit = "QuitCrypt";

		/// <param name="console"></param>
		public static void genSHA3KeyText(CryptobyConsole console)
		{
			Scanner scanner = new Scanner(java.lang.System.@in);
			// Initial Variables
			int keySize;
			int choice;
			string pwAns;
			string key;
			string password;
			// Set Default Key Size
			keySize = 256;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Select Key Size in Bit");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - 224");
				Console.Out.WriteLine("2 - 256");
				Console.Out.WriteLine("3 - 384");
				Console.Out.WriteLine("4 - 512");
				Console.Out.WriteLine("5 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.WriteLine("That's not a number! Enter 1,2,3,4 or 5:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 5);
			switch (choice)
			{
				case 1:
				{
					keySize = 224;
					break;
				}

				case 2:
				{
					keySize = 256;
					break;
				}

				case 3:
				{
					keySize = 384;
					break;
				}

				case 4:
				{
					keySize = 512;
					break;
				}

				case 5:
				{
					console.menuGenKey();
					break;
				}

				default:
				{
					console.menuGenKey();
					break;
				}
			}
			do
			{
				// Input a Password or nothing, in the case it will be used a Secure Random number
				Console.Out.WriteLine("Do you want to use a password. If not, it will be used a SecureRandom password."
					);
				Console.Out.Write("Enter y or n: ");
				pwAns = scanner.next();
			}
			while (!pwAns.Equals("y") && !pwAns.Equals("n"));
			if (pwAns.Equals("y"))
			{
				Console.Out.Write("Enter Password for the Key: ");
				password = scanner.next();
			}
			else
			{
				password = string.Empty;
			}
			// Initial Key Generator
			console.getCore().getClient().setKeySymArt("SHA3");
			console.getCore().initSymKey();
			// Get Result of Test
			if (password.Equals(string.Empty))
			{
				key = console.getCore().getKeyGenSym().generateKey(keySize);
			}
			else
			{
				key = console.getCore().getKeyGenSym().generateKey(keySize, password);
			}
			// Print Key
			Console.Out.WriteLine("SHA3-" + keySize + ": " + key);
			// Enter for Continues
			CryptobyHelper.pressEnter();

			// Back to Menu Choose PrimeTest
			console.menuGenKey();
		}

		/// <param name="console"></param>
		public static void genSHA3KeyFile(CryptobyConsole console)
		{
			Scanner scanner = new Scanner(java.lang.System.@in);
			string keyPath;
			// Initial Variables
			int keySize;
			int choice;
			string pwAns;
			string key;
			string password;
			// Set Default Key Size
			keySize = 256;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Select Key Size in Bit");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - 224");
				Console.Out.WriteLine("2 - 256");
				Console.Out.WriteLine("3 - 384");
				Console.Out.WriteLine("4 - 512");
				Console.Out.WriteLine("5 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.WriteLine("That's not a number! Enter 1,2,3,4 or 5:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 5);
			switch (choice)
			{
				case 1:
				{
					keySize = 224;
					break;
				}

				case 2:
				{
					keySize = 256;
					break;
				}

				case 3:
				{
					keySize = 384;
					break;
				}

				case 4:
				{
					keySize = 512;
					break;
				}

				case 5:
				{
					console.menuGenKey();
					break;
				}

				default:
				{
					console.menuGenKey();
					break;
				}
			}
			do
			{
				// Input a Password or nothing, in the case it will be used a Secure Random number
				Console.Out.WriteLine("Do you want to use a password. If not, it will be used a SecureRandom password."
					);
				Console.Out.WriteLine("Enter y or n: ");
				pwAns = scanner.next();
			}
			while (!pwAns.Equals("y") && !pwAns.Equals("n"));
			if (pwAns.Equals("y"))
			{
				Console.Out.Write("Enter Password for the Key: ");
				password = scanner.next();
			}
			else
			{
				password = string.Empty;
			}
			// Input Path for saving Private Key
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to saving Private Key(Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				console.menuGenKey();
			}
			keyPath = scanner.next();
			// Initial Key Generator
			console.getCore().getClient().setKeySymArt("SHA3");
			console.getCore().initSymKey();
			// Get Result of Test
			if (password.Equals(string.Empty))
			{
				key = console.getCore().getKeyGenSym().generateKey(keySize);
			}
			else
			{
				key = console.getCore().getKeyGenSym().generateKey(keySize, password);
			}
			// Save Key
			try
			{
				//Put private Key to File
				CryptobyFileManager.putKeyToFile(keyPath, key);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				console.menuGenKey();
			}
			Console.Out.WriteLine("\nAES Key File saved to this Path:");
			Console.Out.WriteLine(keyPath);
			// Enter for Continues
			CryptobyHelper.pressEnter();

			// Back to Menu Choose PrimeTest
			console.menuGenKey();
		}
	}
}
