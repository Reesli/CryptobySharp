using System;
using java.math;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestRSABlock
	{
		const int cycles = 10;
		static readonly BigInteger E = BigInteger.valueOf(65537);
		CryptobyClient client;
		CryptobyCore core;
		KeyGenRSA keyGen;
		const int keySize1024 = 1024;
		const int keySize2048 = 2048;
		const int keySize4096 = 4096;
		byte[] privKey;
		byte[] nVal;
		byte[] dVal;
		byte[] data;
		byte[] encBlock;
		byte[] decBlock;

		[TestFixtureSetUp] public void Init()
		{ 
			client = new CryptobyClient();
			core = new CryptobyCore(client);
			keyGen = new KeyGenRSA(core);
		}

		private static byte[] encryptBlock(byte[] block, BigInteger n)
		{
			BigInteger blockInt = new BigInteger(block);
			if (blockInt.compareTo(BigInteger.ZERO) < 0)
			{
				blockInt = blockInt.modPow(E, n);
				return blockInt.multiply(BigInteger.valueOf(-1)).toByteArray();
			}
			else
			{
				return (blockInt).modPow(E, n).toByteArray();
			}
		}

		private static byte[] decryptBlock(byte[] block, BigInteger n, BigInteger d)
		{
			BigInteger blockInt = new BigInteger(block);
			if (blockInt.compareTo(BigInteger.ZERO) < 0)
			{
				blockInt = blockInt.modPow(d, n);
				return blockInt.multiply(BigInteger.valueOf(-1)).toByteArray();
			}
			else
			{
				return (blockInt).modPow(d, n).toByteArray();
			}
		}

		private static byte[] getDfromKey(byte[] privateKey)
		{
			// Get D from the first Part of the PrivateKey
			int midOfKey = privateKey.Length / 2;
			byte[] dByteArray = new byte[midOfKey];
			Array.Copy(privateKey, 0, dByteArray, 0, midOfKey);
			return dByteArray;
		}

		private static byte[] getNfromKey(byte[] privateKey)
		{
			// Get N from the second Part of the PrivateKey
			int midOfKey = privateKey.Length / 2;
			byte[] nByteArray = new byte[midOfKey];
			Array.Copy(privateKey, midOfKey, nByteArray, 0, midOfKey);
			return nByteArray;
		}

		private void testGetNfromKey(){
			for (int i = 0; i < cycles; i++) {
				nVal = getNfromKey (privKey);
			}
		}

		private void testGetDfromKey(){
			for (int i = 0; i < cycles; i++) {
				dVal = getDfromKey (privKey);
			}
		}

		public void testEncryptBlock(){
			for (int i = 0; i < cycles; i++) {
				BigInteger nBig = new BigInteger (nVal);
				encBlock = encryptBlock (data, nBig);
			}
		}

		public void testDecryptBlock(){
			for (int i = 0; i < cycles; i++) {
				BigInteger nBig = new BigInteger (nVal);
				BigInteger dBig = new BigInteger (dVal);
				decBlock = decryptBlock (encBlock,nBig,dBig);
			}
		}

		[Test]
		public void testRun(){
			const int rounds = 1;
			Random rand = new Random ();

			Console.WriteLine ("Test Performance Blockwise RSA Enc/Dec");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			keyGen.initGenerator (keySize1024);
			privKey = keyGen.getPrivateKeyByte ();
			testGetDfromKey ();
			testGetNfromKey ();

			data = new byte[nVal.Length-1];
			rand.NextBytes (data);
			Console.WriteLine ("\nBlockwise 1024 Bit Key");
			Console.WriteLine ("Encrypt One Block with N");
			PerfMeter.run(new Action(testEncryptBlock),rounds);
			Console.WriteLine ("\nDecrypt One Block with N and D");
			PerfMeter.run(new Action(testDecryptBlock),rounds);
			Assert.AreEqual (data,decBlock);

			keyGen.initGenerator (keySize2048);
			privKey = keyGen.getPrivateKeyByte ();
			testGetDfromKey ();
			testGetNfromKey ();

			data = new byte[nVal.Length-1];
			rand.NextBytes (data);
			Console.WriteLine ("\nBlockwise 2048 Bit Key");
			Console.WriteLine ("Encrypt One Block with N");
			PerfMeter.run(new Action(testEncryptBlock),rounds);
			Console.WriteLine ("\nDecrypt One Block with N and D");
			PerfMeter.run(new Action(testDecryptBlock),rounds);
			Assert.AreEqual (data,decBlock);

			keyGen.initGenerator (keySize4096);
			privKey = keyGen.getPrivateKeyByte ();
			testGetDfromKey ();
			testGetNfromKey ();

			data = new byte[nVal.Length-1];
			rand.NextBytes (data);
			Console.WriteLine ("\nBlockwise 4096 Bit Key");
			Console.WriteLine ("Encrypt One Block with N");
			PerfMeter.run(new Action(testEncryptBlock),rounds);
			Console.WriteLine ("\nDecrypt One Block with N and D");
			PerfMeter.run(new Action(testDecryptBlock),rounds);
			Assert.AreEqual (data,decBlock);

		}


	}
}

