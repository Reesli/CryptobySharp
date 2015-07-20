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
using java.math;

using java.util;
using java.util.logging;
using System.Text;

namespace CryptobySharp
{
	/// <summary>This class provides menus in console UI for AES implementation.</summary>
	/// <remarks>This class provides menus in console UI for AES implementation.</remarks>
	/// <author>Tobias Rees</author>
	public class AesUI
	{
		private static Scanner scanner = new Scanner(java.lang.System.@in);

		private static readonly string quit = "QuitCrypt";

		private static string plainFilePath;

		private static string cryptFilePath;

		private static string keyPath;

		private static byte[] plainByte;

		private static byte[] cryptByte;

		private static byte[] key;

		private static int keySize;

		private static char[] charTextHex;

		/// <param name="console"></param>
		public static void aesCrypterFile(CryptobyConsole console)
		{
			Console.Out.WriteLine("\n");
			Console.Out.WriteLine("AES File Crypter");
			Console.Out.WriteLine("----------------\n");
			switch (choiceText())
			{
				case 1:
				{
					aesEncrypterFile(console);
					break;
				}

				case 2:
				{
					aesDecrypterFile(console);
					break;
				}

				case 3:
				{
					console.menuFileSym();
					break;
				}

				default:
				{
					aesCrypterFile(console);
					break;
				}
			}
		}

		private static void aesEncrypterFile(CryptobyConsole console)
		{
			// Input Path to File for encryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to File for encryption (Type '" + quit +
				 "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				aesCrypterFile(console);
			}
			plainFilePath = scanner.next();
			// Get Bytes from PlainFile
			try
			{
				plainByte = CryptobyFileManager.getBytesFromFile(plainFilePath);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				aesCrypterFile(console);
			}
			// Input Path saving Path
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to save encrypted File (Type '" + quit +
				 "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				aesCrypterFile(console);
			}
			cryptFilePath = scanner.next();
			// Input Key File for encryption
			key = scanKeyFile(console);
			// Initial AES Crypt Object
			initAESKeyGen(console);
			// Encrypt the String Text with given Key
			Console.Out.WriteLine("\nEncrypting in progress...");
			cryptByte = console.getCore().getCryptSym().encrypt(plainByte, key);
			Console.Out.WriteLine("\nEncryption successfull. Saving File now...");
			//Put encrypted Bytes to File
			try
			{
				CryptobyFileManager.putBytesToFile(cryptFilePath, cryptByte);
			}
			catch (System.IO.IOException ex)
			{
				Logger.getLogger(typeof(RsaUI).FullName).log(Level.SEVERE, null, ex);
			}
			Console.Out.WriteLine("\nEncrypted File saved to this Path:");
			Console.Out.WriteLine(cryptFilePath);
			// Reset Variables
			initAESKeyGen(console);
			cryptByte = null;
			plainByte = null;
			key = null;
			// Back to File Crypter Menu
			Console.Out.WriteLine("\nGo back to AES File Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			aesCrypterFile(console);
		}

		private static void aesDecrypterFile(CryptobyConsole console)
		{
			// Input Path to File for decryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to decrypt a File (Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				aesCrypterFile(console);
			}
			cryptFilePath = scanner.next();
			try
			{
				// Get Bytes from PlainFile
				cryptByte = CryptobyFileManager.getBytesFromFile(cryptFilePath);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				aesCrypterFile(console);
			}
			// Input Path saving Path
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to save decrypted File (Type '" + quit +
				 "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				aesCrypterFile(console);
			}
			plainFilePath = scanner.next();
			// Input your Key for encryption
			key = scanKeyFile(console);
			// Initial AES Crypt Object
			initAESKeyGen(console);
			// Decrypt the String Text with given Key
			Console.Out.WriteLine("\nDecrypting in progress...");
			try
			{
				plainByte = console.getCore().getCryptSym().decrypt(cryptByte, key);
			}
			catch (Exception)
			{
				Console.Out.WriteLine("\nUnable to decrypt this String!!");
				// Enter for Continues
				CryptobyHelper.pressEnter();
				aesCrypterFile(console);
			}
			Console.Out.WriteLine("\nDecryption finished. Saving File now...");
			try
			{
				//Put encrypted Bytes to File
				CryptobyFileManager.putBytesToFile(plainFilePath, plainByte);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				aesCrypterFile(console);
			}
			Console.Out.WriteLine("\nDecrypted File saved to this Path:");
			Console.Out.WriteLine(plainFilePath);
			// Reset Variables
			initAESKeyGen(console);
			cryptByte = null;
			plainByte = null;
			key = null;
			// Back to File Crypter Menu
			Console.Out.WriteLine("\nGo back to AES File Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			aesCrypterFile(console);
		}

		/// <param name="console"></param>
		public static void aesCrypterText(CryptobyConsole console)
		{
			Console.Out.WriteLine("\n");
			Console.Out.WriteLine("AES Text Crypter");
			Console.Out.WriteLine("----------------\n");
			switch (choiceText())
			{
				case 1:
				{
					aesEncrypterText(console);
					break;
				}

				case 2:
				{
					aesDecrypterText(console);
					break;
				}

				case 3:
				{
					console.menuTextSym();
					break;
				}

				default:
				{
					aesCrypterText(console);
					break;
				}
			}
		}

		private static void aesEncrypterText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			scanner.useDelimiter("\n");
			// Input your String Text to encrypt
			Console.Out.WriteLine("\nYour Text to encrypt (Type '" + quit + "' to Escape):"
				);
			if (scanner.hasNext(quit))
			{
				aesCrypterText(console);
			}
			plainByte = Encoding.UTF8.GetBytes(scanner.next());
			// Input your Key for encryption
			key = scanKeyText(console);
			// Initial AES Crypt Object
			initAESKeyGen(console);
			// Encrypt the String Text with given Key
			Console.Out.WriteLine("\nEncrypting in progress...");
			cryptByte = console.getCore().getCryptSym().encrypt(plainByte, key);
			// Convert byte Array into a Hexcode String
			charTextHex = CryptobyHelper.bytesToHexStringUpper(cryptByte).ToCharArray();
			// Print encrypted Text in Hex Block form
			Console.Out.WriteLine("\nEncryption successfull...");
			Console.Out.WriteLine(CryptobyHelper.printHexBlock("AES", keySize, charTextHex
				));
			// Back to Text Crypter Menu
			Console.Out.WriteLine("\nGo back to AES Text Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			aesCrypterText(console);
		}

		private static void aesDecrypterText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			// Input encrypted Hex String Text to decrypt
			Console.Out.WriteLine("\nYour Text to decrypt (Type '" + quit + "' to Escape):"
				);
			// Convert crypted HexString Block to one String
			try
			{
				string cryptText = string.Empty;
				while (!scanner.hasNext(CryptobyHelper.getEOBString()))
				{
					if (scanner.hasNext(quit))
					{
						aesCrypterText(console);
					}
					cryptText = cryptText + scanner.next();
				}
				cryptByte = CryptobyHelper.hexStringToBytes(cryptText);
			}
			catch (FormatException)
			{
				// Catch false format of Input
				Console.Out.WriteLine("\nNot allowed Crypted Text! Must be a Upper Hex String!"
					);
				cryptByte = BigInteger.ZERO.toByteArray();
			}
			// Input your Key for encryption
			key = scanKeyText(console);
			// Initial AES Crypt Object
			initAESKeyGen(console);
			// Decrypt the String Text with given Key
			Console.Out.WriteLine("\nDecrypting in progress...");
			try
			{
				plainByte = console.getCore().getCryptSym().decrypt(cryptByte, key);
			}
			catch (Exception)
			{
				Console.Out.WriteLine("\nUnable to decrypt this String!!");
				// Enter for Continues
				CryptobyHelper.pressEnter();
				aesCrypterText(console);
			}
			// Print decrypted Text
			Console.Out.WriteLine("\nDecryption finished...");
			Console.Out.WriteLine("\nAES-" + keySize + " decrypted Text:");
			Console.Out.WriteLine(Encoding.UTF8.GetString(plainByte));
			// Back to Text Crypter Menu
			Console.Out.WriteLine("\nGo back to AES Text Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			aesCrypterText(console);
		}

		// Helper Functions    
		private static byte[] scanKeyText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			byte[] tempKey;
			do
			{
				Console.Out.WriteLine("\nAllowed Key Sizes 128,192 and 256 Bit.");
				Console.Out.WriteLine("Enter the AES Key (Type '" + quit + "' to Escape):"
					);
				if (scanner.hasNext(quit))
				{
					aesCrypterText(console);
				}
				tempKey = Encoding.UTF8.GetBytes(scanner.next());
				keySize = tempKey.Length * 4;
			}
			while (keySize != 128 && keySize != 192 && keySize != 256);
			return tempKey;
		}

		private static byte[] scanKeyFile(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			byte[] tempKey = null;
			do
			{
				Console.Out.WriteLine("\nAllowed Key Sizes 128,192 and 256 Bit.");
				Console.Out.WriteLine("Enter Path to Key File (Type '" + quit + "' to Escape):"
					);
				if (scanner.hasNext(quit))
				{
					aesCrypterFile(console);
				}
				keyPath = scanner.next();
				try
				{
					tempKey = CryptobyFileManager.getKeyFromFile(keyPath);
				}
				catch (System.IO.IOException)
				{
					CryptobyHelper.printIOExp();
					aesCrypterFile(console);
				}
				catch (FormatException)
				{
					Console.Out.WriteLine("Key File format is not correct!");
					aesCrypterFile(console);
				}
				keySize = tempKey.Length * 4;
			}
			while (keySize != 128 && keySize != 192 && keySize != 256);
			return tempKey;
		}

		private static void initAESKeyGen(CryptobyConsole console)
		{
			console.getCore().getClient().setCryptSymArt("AES");
			console.getCore().initCryptSym();
		}

		private static int choiceText()
		{
			scanner = new Scanner(java.lang.System.@in);
			int choice;
			do
			{
				Console.Out.WriteLine("1 - Encryption");
				Console.Out.WriteLine("2 - Decryption");
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
			return choice;
		}
	}
}
