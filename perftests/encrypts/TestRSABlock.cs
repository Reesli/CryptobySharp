using System;
using java.math;
using NUnit.Framework;

namespace CryptobySharp
{
	public class TestRSABlock
	{
		int cycles = 1;
		private static readonly BigInteger E = BigInteger.valueOf(65537);
		CryptobyClient client;
		CryptobyCore core;
		KeyGenRSA generator;
		int keySize1024 = 1024;
		int keySize2048 = 2048;
		int keySize4096 = 4096;
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
			generator = new KeyGenRSA(core);
		}

		private byte[] encryptBlock(byte[] block, BigInteger n)
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

		private byte[] decryptBlock(byte[] block, BigInteger n, BigInteger d)
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

		private byte[] getDfromKey(byte[] privateKey)
		{
			// Get D from the first Part of the PrivateKey
			int midOfKey = privateKey.Length / 2;
			byte[] dByteArray = new byte[midOfKey];
			System.Array.Copy(privateKey, 0, dByteArray, 0, midOfKey);
			return dByteArray;
		}

		private byte[] getNfromKey(byte[] privateKey)
		{
			// Get N from the second Part of the PrivateKey
			int midOfKey = privateKey.Length / 2;
			byte[] nByteArray = new byte[midOfKey];
			System.Array.Copy(privateKey, midOfKey, nByteArray, 0, midOfKey);
			return nByteArray;
		}

		public void testGetNfromKey(){
			for (int i = 0; i < cycles; i++) {
				nVal = getNfromKey (privKey);
			}
		}

		public void testGetDfromKey(){
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
			int rounds = 20;

			Console.WriteLine ("Test Performance Cryptoby FileManager");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

			generator.initGenerator (keySize1024);
			privKey = generator.getPrivateKeyByte ();

			Console.WriteLine ("\nGet N Value from PrivateKey");
			PerfMeter.run(new Action(testGetNfromKey),rounds);
			Console.WriteLine ("\nGet D Value from PrivateKey");
			PerfMeter.run(new Action(testGetDfromKey),rounds);
			data = new byte[nVal.Length-1];
			Random rand = new Random ();
			rand.NextBytes (data);
			Console.WriteLine ("\nEncrypt One Block with N");
			PerfMeter.run(new Action(testEncryptBlock),rounds);
			Console.WriteLine (encBlock.Length);
			Console.WriteLine ("\nDecrypt One Block with N and D");
			PerfMeter.run(new Action(testDecryptBlock),rounds);
			Console.WriteLine (decBlock.Length);
			Assert.AreEqual (data,decBlock);

		}


	}
}

