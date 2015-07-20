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

using keygen.itf;

using java.lang;
using java.math;

namespace CryptobySharp
{
	/// <summary>This class provides RSA key generation.</summary>
	/// <remarks>This class provides RSA key generation.</remarks>
	/// <author>Tobias Rees</author>
	public class KeyGenRSA : KeyGenAsym
	{
		private readonly CryptobyCore core;

		private BigInteger p;

		private BigInteger q;

		private BigInteger n;

		private BigInteger phi;

		private static readonly BigInteger E = BigInteger.valueOf(65537);

		private BigInteger d;

		private double log2ofPQ;

		private string privKey;

		private string pubKey;

		private byte[] privKeyByte;

		private byte[] pubKeyByte;

		private int keyByteSize;

		private readonly int cores;

		/// <summary>Initial CryptobyCore object and available processor cores.</summary>
		/// <remarks>Initial CryptobyCore object and available processor cores.</remarks>
		/// <param name="appCore">Input CryptobyCore object which provides other objects of application.
		/// 	</param>
		public KeyGenRSA(CryptobyCore appCore)
		{
			core = appCore;
			cores = Runtime.getRuntime().availableProcessors();
		}

		/// <summary>Generate RSA Private and Public Key with Size of keyBitSize.</summary>
		/// <remarks>Generate RSA Private and Public Key with Size of keyBitSize.</remarks>
		/// <param name="keyBitSize">
		/// Size of RSA key which will be generated. Allowed are
		/// 1024, 2048 and 4096
		/// </param>
		public virtual void initGenerator(int keyBitSize)
		{
			if (keyBitSize != 1024 && keyBitSize != 2048 && keyBitSize != 4096)
			{
				throw new ArgumentException("Just Keys with Size of 1024,2048 or 4096 are allowed!"
					);
			}
			generateKeys(keyBitSize);
			// Generate Public Key to Hex String
			pubKeyByte = n.toByteArray();
			pubKey = CryptobyHelper.bytesToHexString(pubKeyByte);
			byte[] dByte = d.toByteArray();
			privKeyByte = new byte[dByte.Length + pubKeyByte.Length];
			// Copy D ByteArray into first Part and N ByteArray into second Part
			Array.Copy(dByte, 0, privKeyByte, 0, dByte.Length);
			Array.Copy(pubKeyByte, 0, privKeyByte, dByte.Length, pubKeyByte.Length);
			// Generate Private Key to Hex String
			privKey = CryptobyHelper.bytesToHexString(privKeyByte);
		}

		/// <returns>Return private key as Hex String</returns>
		public virtual string getPrivateKey()
		{
			try
			{
				return privKey;
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("KeyGenerator is not initialised! First use initGenerator!"
					);
			}
		}

		/// <returns>Return public key as Hex String</returns>
		public virtual string getPublicKey()
		{
			try
			{
				return pubKey;
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("KeyGenerator is not initialised! First use initGenerator!"
					);
			}
		}

		/// <returns>Return private key as byte array</returns>
		public virtual byte[] getPrivateKeyByte()
		{
			try
			{
				return privKeyByte;
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("KeyGenerator is not initialised! First use initGenerator!"
					);
			}
		}

		/// <returns>Return public key as byte array</returns>
		public virtual byte[] getPublicKeyByte()
		{
			try
			{
				return pubKeyByte;
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentException("KeyGenerator is not initialised! First use initGenerator!"
					);
			}
		}

		private void generateKeys(int keyBitSize)
		{
			keyByteSize = keyBitSize / 8;
			do
			{
				// Generate Primes for Q and P
				// Use Cores parallel, if there are more than 1
				if (cores > 1)
				{
					do
					{
						BigInteger[] primes = getPrimesParallel(keyBitSize);
						p = primes[0];
						q = primes[1];
					}
					while (p.compareTo(q) == 0);
				}
				else
				{
					do
					{
						p = getPrimesParallel(keyBitSize)[0];
						q = getPrimesParallel(keyBitSize)[0];
						log2ofPQ = java.lang.Math.abs(CryptobyHelper.logBigInteger(p) - CryptobyHelper.logBigInteger
							(q));
					}
					while (log2ofPQ <= 0.1 || log2ofPQ >= 30);
				}
				// Calculate n Module
				calcN();
				// Calculate Phi Module
				calcPhi();
				// Calculate D Module
				calcD();
			}
			while (n.toByteArray().Length != (keyByteSize) || d.toByteArray().Length != (keyByteSize
				));
		}

		private void calcN()
		{
			n = p.multiply(q);
		}

		private void calcPhi()
		{
			// Calc phi of n
			phi = p.subtract(BigInteger.ONE).multiply(q.subtract(BigInteger.ONE));
		}

		private void calcD()
		{
			d = E.modInverse(phi);
		}

		private BigInteger[] getPrimesParallel(int keyBitSize)
		{
			System.Threading.Thread[] startThreads = new System.Threading.Thread[cores];
			GenPrimeThread[] primeThreads = new GenPrimeThread[cores];
			BigInteger[] primes = new BigInteger[cores];
			BigInteger[] retPrime = new BigInteger[2];
			for (int i = 0; i < cores; i++)
			{
				primeThreads[i] = new GenPrimeThread (core, keyBitSize);
				startThreads[i] = new System.Threading.Thread(new System.Threading.ThreadStart(primeThreads[i].run));
			}
			// Start Threads
			for (int i_1 = 0; i_1 < cores; i_1++)
			{
				startThreads[i_1].Start();
			}
			for (int i_2 = 0; i_2 < cores; i_2++)
			{
				startThreads[i_2].Join();
			}
			for (int i_3 = 0; i_3 < cores; i_3++)
			{
				primes[i_3] = primeThreads[i_3].getPrime();
			}
			for (int i_4 = 0; i_4 < cores; i_4++)
			{
				for (int j = cores - 1; j >= 0; j--)
				{
					log2ofPQ = java.lang.Math.abs(CryptobyHelper.logBigInteger(primes[i_4]) - CryptobyHelper
						.logBigInteger(primes[j]));
					if (log2ofPQ >= 0.1 || log2ofPQ <= 30)
					{
						retPrime[0] = primes[i_4];
						retPrime[1] = primes[j];
						return retPrime;
					}
				}
			}
			retPrime[0] = BigInteger.ZERO;
			retPrime[1] = BigInteger.ZERO;
			return retPrime;
		}
	}
}
