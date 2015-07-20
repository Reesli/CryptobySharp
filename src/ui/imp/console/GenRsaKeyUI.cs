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
	/// <summary>This class provides menus in console UI for RSA generator implementation.
	/// 	</summary>
	/// <remarks>This class provides menus in console UI for RSA generator implementation.
	/// 	</remarks>
	/// <author>Tobias Rees</author>
	public class GenRsaKeyUI
	{
		private static readonly string quit = "QuitCrypt";

		/// <param name="console"></param>
		public static void genRSAKeysText(CryptobyConsole console)
		{
			Scanner scanner = new Scanner(java.lang.System.@in);
			// Initial Variables
			int keySize;
			int choice;
			string publicKey;
			string privateKey;
			// Set Default Key Size
			keySize = 1024;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Choose Key  in Bit");
				Console.Out.WriteLine("-------------------------\n");
				Console.Out.WriteLine("1 - 1024");
				Console.Out.WriteLine("2 - 2048");
				Console.Out.WriteLine("3 - 4096");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.WriteLine("That's not a number! Enter 1,2,3 or 4:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 4);
			switch (choice)
			{
				case 1:
				{
					keySize = 1024;
					break;
				}

				case 2:
				{
					keySize = 2048;
					break;
				}

				case 3:
				{
					keySize = 4096;
					break;
				}

				case 4:
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
			// Initial Key Generator
			console.getCore().getClient().setKeyAsymArt("RSA");
			console.getCore().initAsymKey();
			// Generate Keys
			console.getCore().getKeyGenAsym().initGenerator(keySize);
			publicKey = console.getCore().getKeyGenAsym().getPublicKey();
			privateKey = console.getCore().getKeyGenAsym().getPrivateKey();
			// Print Private Keys
			Console.Out.WriteLine(CryptobyHelper.printPrivateKeyBlock(privateKey));
			// Print Public Keys
			Console.Out.WriteLine(CryptobyHelper.printPublicKeyBlock(publicKey));
			// Enter for Continues
			CryptobyHelper.pressEnter();
			// Back to Menu Choose PrimeTest
			console.menuGenKey();
		}

		/// <param name="console"></param>
		public static void genRSAKeysFile(CryptobyConsole console)
		{
			Scanner scanner = new Scanner(java.lang.System.@in);
			string privateKeyPath;
			string publicKeyPath;
			// Initial Variables
			int keySize;
			int choice;
			string publicKey;
			string privateKey;
			// Set Default Key Size
			keySize = 1024;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Choose Key  in Bit");
				Console.Out.WriteLine("-------------------------\n");
				Console.Out.WriteLine("1 - 1024");
				Console.Out.WriteLine("2 - 2048");
				Console.Out.WriteLine("3 - 4096");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.WriteLine("That's not a number! Enter 1,2,3 or 4:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 4);
			switch (choice)
			{
				case 1:
				{
					keySize = 1024;
					break;
				}

				case 2:
				{
					keySize = 2048;
					break;
				}

				case 3:
				{
					keySize = 4096;
					break;
				}

				case 4:
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
			// Input Path for saving Private Key
			Console.Out.WriteLine("Enter Path to saving Private Key (Type '" + quit + 
				"' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				RsaUI.rsaCrypterFile(console);
			}
			privateKeyPath = scanner.next();
			// Input Path for saving Public Key
			Console.Out.WriteLine("Enter Path to saving Public Key (Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			publicKeyPath = scanner.next();
			// Initial Key Generator
			console.getCore().getClient().setKeyAsymArt("RSA");
			console.getCore().initAsymKey();
			// Generate Keys
			console.getCore().getKeyGenAsym().initGenerator(keySize);
			publicKey = console.getCore().getKeyGenAsym().getPublicKey();
			privateKey = console.getCore().getKeyGenAsym().getPrivateKey();
			//Put private Key to File
			try
			{
				CryptobyFileManager.putKeyToFile(privateKeyPath, privateKey);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				console.menuGenKey();
			}
			Console.Out.WriteLine("\nPrivate Key File saved to this Path:");
			Console.Out.WriteLine(privateKeyPath);
			//Put public Key to File
			try
			{
				CryptobyFileManager.putKeyToFile(publicKeyPath, publicKey);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				console.menuGenKey();
			}
			Console.Out.WriteLine("\nPublic Key File saved to this Path:");
			Console.Out.WriteLine(publicKeyPath);
			// Enter for Continues
			CryptobyHelper.pressEnter();
			// Back to Menu Choose PrimeTest
			console.menuGenKey();
		}
	}
}
