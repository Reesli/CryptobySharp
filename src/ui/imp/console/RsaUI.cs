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
using System.Text;

namespace CryptobySharp
{
	/// <summary>This class provides menus in console UI for RSA implementation.</summary>
	/// <remarks>This class provides menus in console UI for RSA implementation.</remarks>
	/// <author>Tobias Rees</author>
	public class RsaUI
	{
		private static Scanner scanner = new Scanner(java.lang.System.@in);

		private static readonly string quit = "QuitCrypt";

		private static byte[] plainByte;

		private static byte[] cryptByte;

		private static byte[] privateKeyByte;

		private static byte[] publicKeyByte;

		private static string plainFilePath;

		private static string cryptFilePath;

		private static string privateKeyPath;

		private static string publicKeyPath;

		private static int choice;

		private static int keySize;

		private static char[] charTextHex;

		// UI for File Cryption Menu
		/// <param name="console"></param>
		public static void rsaCrypterFile(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("RSA File Cryption");
				Console.Out.WriteLine("-----------------\n");
				Console.Out.WriteLine("1 - Encryption and generate Keys");
				Console.Out.WriteLine("2 - Encryption with own Key");
				Console.Out.WriteLine("3 - Decryption");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3 or 4: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 4);
			switch (choice)
			{
				case 1:
					rsaEncGenKeysFile (console);
					break;
				case 2:
					rsaEncrypterFile(console);
					break;
				case 3:
					rsaDecrypterFile (console);
					break;
				case 4:
					console.menuFileAsym();
					break;
				default:
				{
					rsaCrypterFile(console);
					break;
				}
			}
		}

		private static void rsaEncrypterFile(CryptobyConsole console)
		{
			// Input Path to File for encryption
			scanner = new Scanner (java.lang.System.@in);
			scanner.useDelimiter ("\n");
			Console.Out.WriteLine ("Enter Path to File for Encryption (Type '" + quit +
			"' to Escape):");
			if (scanner.hasNext (quit)) {
				rsaCrypterFile (console);
			}
			plainFilePath = scanner.next ();
			// Get Bytes from PlainFile
			try {
				plainByte = CryptobyFileManager.getBytesFromFile (plainFilePath);
			} catch (System.IO.IOException) {
				CryptobyHelper.printIOExp ();
				rsaCrypterFile (console);
			}
			// Input Path to save encrypted File
			scanner = new Scanner (java.lang.System.@in);
			scanner.useDelimiter ("\n");
			Console.Out.WriteLine ("Enter Path to save encrypted File (Type '" + quit +
			"' to Escape):");
			if (scanner.hasNext (quit)) {
				rsaCrypterFile (console);
			}
			cryptFilePath = scanner.next ();
			// Input Path to Public Key File for encryption
			scanner = new Scanner (java.lang.System.@in);
			scanner.useDelimiter ("\n");
			Console.Out.WriteLine ("Enter Path to Public Key File (Type '" + quit + "' to Escape):"
			);
			if (scanner.hasNext (quit)) {
				rsaCrypterFile (console);
			}
			publicKeyPath = scanner.next ();
			// Get Bytes from Public Key File
			try {
				publicKeyByte = CryptobyFileManager.getKeyFromFile (publicKeyPath);
			} catch (System.IO.IOException) {
				CryptobyHelper.printIOExp ();
				rsaCrypterFile (console);
			} catch (FormatException) {
				Console.Out.WriteLine ("Key File format is not correct!");
				rsaCrypterFile (console);
			}
			// Initial RSA Crypt Object
			initRSAKeyGen (console);
			// Encrypt the File with given Public Key
			Console.Out.WriteLine ("\nEncryption in progress...");
			cryptByte = console.getCore ().getCryptAsym ().encrypt (plainByte, publicKeyByte);
			Console.Out.WriteLine ("\nEncryption successfull. Saving File now...");
			// Put encrypted Bytes to File
			try {
				CryptobyFileManager.putBytesToFile (cryptFilePath, cryptByte);
			} catch (System.IO.IOException) {
				CryptobyHelper.printIOExp ();
				rsaCrypterFile (console);
			}

			Console.Out.WriteLine ("\nEncrypted File saved to this Path:");
			Console.Out.WriteLine (cryptFilePath);

			// Reset Variables
			initRSAKeyGen (console);
			cryptByte = null;
			plainByte = null;
			publicKeyByte = null;

			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine ("\nGo back to RSA File Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter ();
			rsaCrypterFile (console);
		}

		private static void rsaEncGenKeysFile(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			// Set Default Key Size
			keySize = 1024;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Choose Key Size in Bit");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - 1024");
				Console.Out.WriteLine("2 - 2048");
				Console.Out.WriteLine("3 - 4096");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3 or 4: ");
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
					rsaCrypterFile(console);
					break;
				}

				default:
				{
					rsaEncGenKeysFile(console);
					break;
				}
			}
			// Input Path to File for encryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to File for encryption (Type '" + quit +
				 "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			plainFilePath = scanner.next();
			// Input Path to File for encryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to saving encrypted File (Type '" + quit
				 + "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			cryptFilePath = scanner.next();
			// Input Path for saving Private Key
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to saving Private Key (Type '" + quit + 
				"' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			privateKeyPath = scanner.next();
			// Input Path for saving Public Key
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to saving Public Key (Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			publicKeyPath = scanner.next();
			// Get Bytes from PlainFile
			try
			{
				plainByte = CryptobyFileManager.getBytesFromFile(plainFilePath);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				rsaCrypterFile(console);
			}
			// Initial RSA Crypt Object
			initRSAKeyGen(console);
			// Get Public Key in Bytecode
			Console.Out.WriteLine("\nGenerate Private and Public RSA Keys...");
			console.getCore().getKeyGenAsym().initGenerator(keySize);
			publicKeyByte = console.getCore().getKeyGenAsym().getPublicKeyByte();
			// Get Public and Private Key as String
			string publicKey = console.getCore().getKeyGenAsym().getPublicKey();
			string privateKey = console.getCore().getKeyGenAsym().getPrivateKey();
			// Encrypt the File with given Key
			Console.Out.WriteLine("\nEncryption in progress...");
			cryptByte = console.getCore().getCryptAsym().encrypt(plainByte, publicKeyByte);
			Console.Out.WriteLine("\nEncryption successfull. Saving File now...");
			//Put encrypted Bytes to File
			try
			{
				CryptobyFileManager.putBytesToFile(cryptFilePath, cryptByte);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				rsaCrypterFile(console);
			}
			Console.Out.WriteLine("\nEncrypted File saved to this Path:");
			Console.Out.WriteLine(cryptFilePath);
			//Put private Key to File
			try
			{
				CryptobyFileManager.putKeyToFile(privateKeyPath, privateKey);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				rsaCrypterFile(console);
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
				rsaCrypterFile(console);
			}
			Console.Out.WriteLine("\nPublic Key File saved to this Path:");
			Console.Out.WriteLine(publicKeyPath);
			// Reset Variables
			initRSAKeyGen(console);
			cryptByte = null;
			plainByte = null;
			publicKeyByte = null;
			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine("\nGo back to RSA File Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			rsaCrypterFile(console);
		}

		private static void rsaDecrypterFile(CryptobyConsole console)
		{
			// Input Path to File for decryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to File for Decryption (Type '" + quit +
				 "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			cryptFilePath = scanner.next();
			// Get Bytes from PlainFile
			try
			{
				cryptByte = CryptobyFileManager.getBytesFromFile(cryptFilePath);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				rsaCrypterFile(console);
			}
			// Input Path saving Path
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter saving Path for decrypted File (Type '" + quit
				 + "' to Escape):");
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			plainFilePath = scanner.next();
			// Input Path to Key File for decryption
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Enter Path to Private Key File (Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterFile(console);
			}
			privateKeyPath = scanner.next();
			// Get Bytes from Private Key File
			try
			{
				privateKeyByte = CryptobyFileManager.getKeyFromFile(privateKeyPath);
			}
			catch (System.IO.IOException)
			{
				CryptobyHelper.printIOExp();
				rsaCrypterFile(console);
			}
			catch (FormatException)
			{
				Console.Out.WriteLine("Key File format is not correct!");
				rsaCrypterFile(console);
			}
			// Initial RSA Crypt Object
			initRSAKeyGen(console);
			// Encrypt the File with given Key
			Console.Out.WriteLine("\nDecryption in progress...");
			try
			{
				plainByte = console.getCore().getCryptAsym().decrypt(cryptByte, privateKeyByte);
			}
			catch (Exception)
			{
				Console.Out.WriteLine("\nUnable to decrypt this File!!");
				plainByte = null;
				// Press Return for Continues
				CryptobyHelper.pressEnter();
				rsaCrypterFile(console);
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
				rsaCrypterFile(console);
			}
			Console.Out.WriteLine("\nDecrypted File saved to this Path:");
			Console.Out.WriteLine(plainFilePath);
			// Reset Variables
			initRSAKeyGen(console);
			cryptByte = null;
			plainByte = null;
			publicKeyByte = null;
			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine("\nGo back to RSA File Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			rsaCrypterFile(console);
		}

		// UIs for Text Cryption Menu
		/// <param name="console"></param>
		public static void rsaCrypterText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("RSA Text Cryption");
				Console.Out.WriteLine("-----------------\n");
				Console.Out.WriteLine("1 - Encryption and generate Keys");
				Console.Out.WriteLine("2 - Encryption with own Key");
				Console.Out.WriteLine("3 - Decryption");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3 or 4: ");
					scanner.next();
				}
				choice = scanner.nextInt();
			}
			while (choice < 1 || choice > 4);
			switch (choice)
			{
				case 1:
				{
					rsaEncGenKeysText(console);
					break;
				}

				case 2:
				{
					rsaEncrypterText(console);
					break;
				}

				case 3:
				{
					rsaDecrypterText(console);
					break;
				}

				case 4:
				{
					console.menuTextAsym();
					break;
				}

				default:
				{
					rsaCrypterText(console);
					break;
				}
			}
		}

		private static void rsaEncrypterText(CryptobyConsole console)
		{
			// Input your String Text to encrypt
			plainByte = scanPlainText(console);
			// Input the Public Key to encrypt
			publicKeyByte = scanPublicKey(console);
			// Initial RSA Crypt Object
			initRSAKeyGen(console);
			// Encrypt the String Text with given Key
			Console.Out.WriteLine("\nEncryption in progress...");
			cryptByte = console.getCore().getCryptAsym().encrypt(plainByte, publicKeyByte);
			Console.Out.WriteLine("\nEncryption successfull...");
			// Convert byte Array into a Hexcode String
			charTextHex = CryptobyHelper.bytesToHexStringUpper(cryptByte).ToCharArray();
			// Print encrypted Text in Hex Block form
			Console.Out.WriteLine(CryptobyHelper.printHexBlock("RSA", keySize, charTextHex
				));
			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine("\nGo back to RSA Text Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			rsaCrypterText(console);
		}

		private static void rsaEncGenKeysText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			string privateKey;
			string publicKey;
			// Set Default Key Size
			keySize = 1024;
			do
			{
				Console.Out.WriteLine("\n");
				Console.Out.WriteLine("Choose Key Size in Bit");
				Console.Out.WriteLine("----------------------\n");
				Console.Out.WriteLine("1 - 1024");
				Console.Out.WriteLine("2 - 2048");
				Console.Out.WriteLine("3 - 4096");
				Console.Out.WriteLine("4 - Back");
				Console.Out.Write("Enter Number: ");
				while (!scanner.hasNextInt())
				{
					Console.Out.Write("That's not a number! Enter 1,2,3 or 4: ");
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
					rsaCrypterText(console);
					break;
				}

				default:
				{
					rsaEncGenKeysText(console);
					break;
				}
			}
			// Input your String Text to encrypt
			plainByte = scanPlainText(console);
			// Initial RSA Crypt Object
			initRSAKeyGen(console);
			// Get Public Key in Bytecode
			Console.Out.WriteLine("\nGenerate Private and Public RSA Keys...");
			console.getCore().getKeyGenAsym().initGenerator(keySize);
			publicKeyByte = console.getCore().getKeyGenAsym().getPublicKeyByte();
			// Get Public and Private Key as String
			publicKey = console.getCore().getKeyGenAsym().getPublicKey();
			privateKey = console.getCore().getKeyGenAsym().getPrivateKey();
			// Encrypt the String Text with given Key
			Console.Out.WriteLine("\nEncryption in progress...");
			cryptByte = console.getCore().getCryptAsym().encrypt(plainByte, publicKeyByte);
			Console.Out.WriteLine("\nEncryption successfull...");
			// Convert crypted byte Array into a Hexcode String
			charTextHex = CryptobyHelper.bytesToHexStringUpper(cryptByte).ToCharArray();
			// Print encrypted Text in Hex Block form
			Console.Out.WriteLine(CryptobyHelper.printHexBlock("RSA", keySize, charTextHex
				));
			// Print Private Keys
			Console.Out.WriteLine(CryptobyHelper.printPrivateKeyBlock(privateKey));
			// Print Public Keys
			Console.Out.WriteLine(CryptobyHelper.printPublicKeyBlock(publicKey));
			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine("\nGo back to RSA Text Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			rsaCrypterText(console);
		}

		private static void rsaDecrypterText(CryptobyConsole console)
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
						rsaCrypterText(console);
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
			// Input the Private Key
			privateKeyByte = scanPrivateKey(console);
			// Initial RSA Crypt Object
			initRSAKeyGen(console);
			// Decrypt the String Text with given Key
			Console.Out.WriteLine("\nDecryption in progress...");
			try
			{
				plainByte = console.getCore().getCryptAsym().decrypt(cryptByte, privateKeyByte);
			}
			catch (Exception)
			{
				Console.Out.WriteLine("\nUnable to decrypt this String!!");
				plainByte = null;
				// Press Return for Continues
				CryptobyHelper.pressEnter();
				rsaCrypterText(console);
			}
			Console.Out.WriteLine("\nDecryption finished...");
			// Print decrypted Text
			Console.Out.WriteLine("\nRSA-" + keySize + " decrypted Text:");
			Console.Out.WriteLine(Encoding.UTF8.GetString(plainByte));
			// Reset RSA Crypt Object to release Memory
			initRSAKeyGen(console);
			// Back to Menu rsaCrypter with Enter (Return) Key
			Console.Out.WriteLine("\nGo back to RSA Text Crypter Menu: Press Enter");
			CryptobyHelper.pressEnter();
			rsaCrypterText(console);
		}

		// Help Functions
		private static byte[] scanPrivateKey(CryptobyConsole console)
		{
			byte[] retKey = null;
			do
			{
				scanner = new Scanner(java.lang.System.@in);
				string keyText = string.Empty;
				// Input Private Key for decryption
				Console.Out.WriteLine("\nEnter the private Key (Type '" + quit + "' to Escape):"
					);
				try
				{
					while (!scanner.hasNext(CryptobyHelper.getEOBString()))
					{
						if (scanner.hasNext(quit))
						{
							rsaCrypterText(console);
						}
						keyText = keyText + scanner.next();
					}
					retKey = CryptobyHelper.hexStringToBytes(keyText);
					keySize = retKey.Length * 4;
				}
				catch (FormatException)
				{
					// Catch false format of Input
					Console.Out.WriteLine("Not allowed Characters in Private Key! Just lower alphanumeric Characters!"
						);
					retKey = BigInteger.ZERO.toByteArray();
					keySize = 0;
				}
				catch (ArgumentNullException)
				{
					Console.Out.WriteLine("NullPointerException catched! Try again!");
					retKey = BigInteger.ZERO.toByteArray();
					keySize = 0;
				}
			}
			while (keySize != 1024 && keySize != 2048 && keySize != 4096);
			return retKey;
		}

		private static byte[] scanPublicKey(CryptobyConsole console)
		{
			byte[] retKey = null;
			do
			{
				scanner = new Scanner(java.lang.System.@in);
				string keyText = string.Empty;
				// Input Key for decryption
				Console.Out.WriteLine("\nEnter the public Key (Type '" + quit + "' to Escape):"
					);
				try
				{
					while (!scanner.hasNext(CryptobyHelper.getEOBString()))
					{
						if (scanner.hasNext(quit))
						{
							rsaCrypterText(console);
						}
						keyText = keyText + scanner.next();
					}
					retKey = CryptobyHelper.hexStringToBytes(keyText);
					keySize = retKey.Length * 8;
				}
				catch (FormatException)
				{
					Console.Out.WriteLine("Not allowed Characters in Private Key! Just lower alphanumeric Characters!"
						);
					retKey = BigInteger.ZERO.toByteArray();
					keySize = 0;
				}
				catch (ArgumentNullException)
				{
					Console.Out.WriteLine("NullPointerException catched! Try again!");
					privateKeyByte = BigInteger.ZERO.toByteArray();
					keySize = 0;
				}
			}
			while (keySize != 1024 && keySize != 2048 && keySize != 4096);
			return retKey;
		}

		private static byte[] scanPlainText(CryptobyConsole console)
		{
			scanner = new Scanner(java.lang.System.@in);
			Console.Out.WriteLine("Your Text to encrypt (Type '" + quit + "' to Escape):"
				);
			scanner.useDelimiter("\n");
			if (scanner.hasNext(quit))
			{
				rsaCrypterText(console);
			}
			return Encoding.UTF8.GetBytes(scanner.next());
		}

		private static void initRSAKeyGen(CryptobyConsole console)
		{
			console.getCore().getClient().setCryptAsymArt("RSA");
			console.getCore().initCryptAsym();
		}
	}
}
