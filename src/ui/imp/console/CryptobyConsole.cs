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
using ui.itf;


namespace CryptobySharp
{
	/// <summary>This class provides main menus for console UI.</summary>
	/// <remarks>This class provides main menus for console UI.</remarks>
	/// <author>Tobias Rees</author>
	public class CryptobyConsole : CryptobyUI
	{
		private readonly CryptobyCore core;

		private readonly Scanner scanner = new Scanner(java.lang.System.@in);

		/// <param name="core"></param>
		public CryptobyConsole(CryptobyCore core)
		{
			this.core = core;
		}

		public virtual void run()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\nCryptoby - Crypt your Stuff");
				Console.Out.WriteLine("---------------------------\n");
				Console.Out.WriteLine("1 - Crypt Files");
				Console.Out.WriteLine("2 - Crypt Text");
				Console.Out.WriteLine("3 - Generate Key");
				Console.Out.WriteLine("4 - Primetest");
				Console.Out.WriteLine("5 - Quit");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3,4 or 5: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 5);
			switch (choice)
			{
				case 1:
				{
					this.menuFileCrypt();
					break;
				}

				case 2:
				{
					this.menuTextCrypt();
					break;
				}

				case 3:
				{
					this.menuGenKey();
					break;
				}

				case 4:
				{
					this.menuPrimeTest();
					break;
				}

				case 5:
				{
					this.core.getClient().exitApp();
					break;
				}

				default:
				{
					this.run();
					break;
				}
			}
		}

		public virtual void menuTextCrypt()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Text Cryption Menu");
				Console.Out.WriteLine("Select Cryptology Type");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - Asymmetric Cryption");
				Console.Out.WriteLine("2 - Symmetric Cryption");
				Console.Out.WriteLine("3 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2 or 3: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 3);
			switch (choice)
			{
				case 1:
				{
					this.menuTextAsym();
					break;
				}

				case 2:
				{
					this.menuTextSym();
					break;
				}

				case 3:
				{
					this.run();
					break;
				}

				default:
				{
					this.menuTextCrypt();
					break;
				}
			}
		}

		// Symmetric String Cryption Menu
		public virtual void menuTextSym()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Text Cryption Menu");
				Console.Out.WriteLine("Select Symmetric Cryption Methode");
				Console.Out.WriteLine("---------------------------------\n");
				Console.Out.WriteLine("1 - AES");
				Console.Out.WriteLine("2 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1 or 2: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 2);
			switch (choice)
			{
				case 1:
				{
					AesUI.aesCrypterText(this);
					break;
				}

				case 2:
				{
					this.menuTextCrypt();
					break;
				}

				default:
				{
					this.menuTextSym();
					break;
				}
			}
		}

		// Asymmetric String Cryption Menu
		public virtual void menuTextAsym()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Text Cryption Menu");
				Console.Out.WriteLine("Select Asymmetric Cryption Methode");
				Console.Out.WriteLine("----------------------------------\n");
				Console.Out.WriteLine("1 - RSA");
				Console.Out.WriteLine("2 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1 or 2:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 2);
			switch (choice)
			{
				case 1:
				{
					RsaUI.rsaCrypterText(this);
					break;
				}

				case 2:
				{
					this.menuTextCrypt();
					break;
				}

				default:
				{
					this.menuTextSym();
					break;
				}
			}
		}

		public virtual void menuFileCrypt()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("File Cryption Menu");
				Console.Out.WriteLine("Select Cryptology Type");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - Asymmetric Cryption");
				Console.Out.WriteLine("2 - Symmetric Cryption");
				Console.Out.WriteLine("3 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2 or 3: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 3);
			switch (choice)
			{
				case 1:
				{
					this.menuFileAsym();
					break;
				}

				case 2:
				{
					this.menuFileSym();
					break;
				}

				case 3:
				{
					this.run();
					break;
				}

				default:
				{
					this.menuFileCrypt();
					break;
				}
			}
		}

		// Symmetric File Cryption Menu
		public virtual void menuFileSym()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("File Cryption Menu");
				Console.Out.WriteLine("Select Symmetric Cryption Methode");
				Console.Out.WriteLine("---------------------------------\n");
				Console.Out.WriteLine("1 - AES");
				Console.Out.WriteLine("2 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1 or 2: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 2);
			switch (choice)
			{
				case 1:
				{
					AesUI.aesCrypterFile(this);
					break;
				}

				case 2:
				{
					this.menuFileCrypt();
					break;
				}

				default:
				{
					this.menuFileSym();
					break;
				}
			}
		}

		// Asymmetric File Cryption Menu
		public virtual void menuFileAsym()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("File Cryption Menu");
				Console.Out.WriteLine("Select Asymmetric Cryption Methode");
				Console.Out.WriteLine("----------------------------------\n");
				Console.Out.WriteLine("1 - RSA");
				Console.Out.WriteLine("2 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1 or 2: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 2);
			switch (choice)
			{
				case 1:
				{
					RsaUI.rsaCrypterFile(this);
					break;
				}

				case 2:
				{
					this.menuFileCrypt();
					break;
				}

				default:
				{
					this.menuFileAsym();
					break;
				}
			}
		}

		// Key Generator Menu
		public virtual void menuGenKey()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Select Key Generator");
				Console.Out.WriteLine("--------------------\n");
				Console.Out.WriteLine("1 - SHA3-Keccak Key as Text");
				Console.Out.WriteLine("2 - SHA3-Keccak Key as File");
				Console.Out.WriteLine("3 - RSA Keys as Text");
				Console.Out.WriteLine("4 - RSA Keys as Files");
				Console.Out.WriteLine("5 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3,4 or 5:");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 5);
			switch (choice)
			{
				case 1:
				{
					GenSHA3UI.genSHA3KeyText(this);
					break;
				}

				case 2:
				{
					GenSHA3UI.genSHA3KeyFile(this);
					break;
				}

				case 3:
				{
					GenRsaKeyUI.genRSAKeysText(this);
					break;
				}

				case 4:
				{
					GenRsaKeyUI.genRSAKeysFile(this);
					break;
				}

				case 5:
				{
					this.run();
					break;
				}

				default:
				{
					this.menuGenKey();
					break;
				}
			}
		}

		// Prime Test Menu
		public virtual void menuPrimeTest()
		{
			int choice;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Select PrimeTest Methode");
				Console.Out.WriteLine("------------------------\n");
				Console.Out.WriteLine("1 - Miller Rabin");
				Console.Out.WriteLine("2 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1 or 2: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 2);
			switch (choice)
			{
				case 1:
				{
					MillerRabinUI.testMillerRabin(this);
					break;
				}

				case 2:
				{
					this.run();
					break;
				}

				default:
				{
					this.menuPrimeTest();
					break;
				}
			}
		}

		/// <returns></returns>
		public virtual CryptobyCore getCore()
		{
			return core;
		}
	}
}
