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
using System.IO;
using NUnit.Framework;
using java.util;
using java.util.logging;

namespace CryptobySharp
{
	/// <author>Tobias Rees</author>
	public class CryptobyFileManagerTest
	{
		/// <summary>Test of getBytesFromFile method, of class FileManager.</summary>
		/// <remarks>Test of getBytesFromFile method, of class FileManager.</remarks>
		[NUnit.Framework.Test]
		public virtual void testGetAndPutByteFiles()
		{
			System.Console.Out.WriteLine("Put Plaintext, get Plainfile, encrypt and decrypt Byte Files");
			for (int i = 1; i < 100; i += 3)
			{   
				string filePathPlain = "test.txt";
				string filePathEnc = "test.cty";
				string filePathDec = "test2.txt";
				int keySize = 1024;
				byte[] testBytes = new byte[i * 100 + i];
				new Random().nextBytes(testBytes);
				CryptobyClient client = new CryptobyClient();
				CryptobyCore core = new CryptobyCore(client);
				CryptRSA rsa = new CryptRSA();
				KeyGenRSA generator = new KeyGenRSA(core);
				CryptobyFileManager filemgr = new CryptobyFileManager ();
				try
				{
					filemgr.putBytesToFile(filePathPlain, testBytes);
				}
				catch (IOException ex) {
					Logger.getLogger (typeof(CryptobyFileManagerTest).FullName).log (Level.SEVERE, null
						, ex);
				}
				generator.initGenerator(keySize);
				byte[] publicKey = generator.getPublicKeyByte();
				byte[] privateKey = generator.getPrivateKeyByte();
				byte[] plainInput = null;
				try
				{
					plainInput = filemgr.getBytesFromFile(filePathPlain);
				}
				catch (IOException ex)
				{
					Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
						, ex);
				}
				Assert.AreEqual(testBytes, plainInput);
				byte[] encrypt = rsa.encrypt(plainInput, publicKey);
				// Put encrypted Bytes from File
				try
				{
					filemgr.putBytesToFile(filePathEnc, encrypt);
				}
				catch (IOException ex)
				{
					Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
						, ex);
				}
				// Get encrypted Bytes from File
				byte[] fileEncrypt = null;
				try
				{
					fileEncrypt = filemgr.getBytesFromFile (filePathEnc);
				}
				catch (IOException ex)
				{
					Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
						, ex);
				}
				Assert.AreEqual(fileEncrypt, encrypt);

				byte[] decrypt = rsa.decrypt(fileEncrypt, privateKey);
				Assert.AreEqual(testBytes, decrypt);
				try
				{
					filemgr.putBytesToFile(filePathDec, decrypt);
				}
				catch (IOException ex)
				{
					Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
						, ex);
				}
				filemgr = null;
			}
		}

		/// <summary>Test of getKeyFromFile method, of class FileManager.</summary>
		/// <remarks>Test of getKeyFromFile method, of class FileManager.</remarks>
		[NUnit.Framework.Test]
		public virtual void testPutAndGetKey()
		{
			System.Console.Out.WriteLine("Put and Get Keys");
			string publicKeyFilePath = "publicKey.pub";
			string privateKeyFilePath = "privateKey.prv";
			int keySize = 1024;
			CryptobyClient client = new CryptobyClient();
			CryptobyCore core = new CryptobyCore(client);
			KeyGenRSA generator = new KeyGenRSA(core);
			CryptobyFileManager filemgr = new CryptobyFileManager ();
			generator.initGenerator(keySize);
			byte[] publicKeyByte = generator.getPublicKeyByte();
			byte[] privateKeyByte = generator.getPrivateKeyByte();
			string publicKey = generator.getPublicKey();
			string privateKey = generator.getPrivateKey();
			try
			{
				filemgr.putKeyToFile(publicKeyFilePath, publicKey);
			}
			catch (IOException ex)
			{
				Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
					, ex);
			}
			try
			{
				filemgr.putKeyToFile(privateKeyFilePath, privateKey);
			}
			catch (IOException ex)
			{
				Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
					, ex);
			}
			byte[] resultPublic = null;
			try
			{
				resultPublic = filemgr.getKeyFromFile(publicKeyFilePath);
			}
			catch (IOException ex)
			{
				Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
					, ex);
			}
			byte[] resultPrivate = null;
			try
			{
				resultPrivate = filemgr.getKeyFromFile(privateKeyFilePath);
			}
			catch (IOException ex)
			{
				Logger.getLogger(typeof(CryptobyFileManagerTest).FullName).log(Level.SEVERE, null
					, ex);
			}
			filemgr = null;
			Assert.AreEqual(publicKeyByte, resultPublic);
			Assert.AreEqual(privateKeyByte, resultPrivate);
			Assert.AreEqual(publicKey, CryptobyHelper.bytesToHexString(resultPublic
				));
			Assert.AreEqual(privateKey, CryptobyHelper.bytesToHexString(resultPrivate
				));
		}
		//
		//    /**
		//     * Test of putKeyToFile method, of class FileManager.
		//     */
		//    @Test
		//    public void testPutKeyToFile() {
		//        System.out.println("putKeyToFile");
		//        String filePath = "";
		//        String key = "";
		//        FileManager.putKeyToFile(filePath, key);
		//        // TODO review the generated test code and remove the default call to fail.
		//        fail("The test case is a prototype.");
		//    }
	}
}
