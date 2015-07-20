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
using java.security;

using java.util;
using keygen.itf;
using System.Text;

namespace CryptobySharp
{
	/// <summary>This class provides SHA3-Keccak hash permutation.</summary>
	/// <remarks>This class provides SHA3-Keccak hash permutation.</remarks>
	/// <author>Tobias Rees</author>
	public class KeyGenSHA3 : KeyGenSym
	{
		private static readonly long[] KeccakRoundConstants = new long[] { unchecked((long
			)(0x0000000000000001L)), unchecked((long)(0x0000000000008082L)), unchecked((long
			)(0x800000000000808AL)), unchecked((long)(0x8000000080008000L)), unchecked((long
			)(0x000000000000808BL)), unchecked((long)(0x0000000080000001L)), unchecked((long
			)(0x8000000080008081L)), unchecked((long)(0x8000000000008009L)), unchecked((long
			)(0x000000000000008AL)), unchecked((long)(0x0000000000000088L)), unchecked((long
			)(0x0000000080008009L)), unchecked((long)(0x000000008000000AL)), unchecked((long
			)(0x000000008000808BL)), unchecked((long)(0x800000000000008BL)), unchecked((long
			)(0x8000000000008089L)), unchecked((long)(0x8000000000008003L)), unchecked((long
			)(0x8000000000008002L)), unchecked((long)(0x8000000000000080L)), unchecked((long
			)(0x000000000000800AL)), unchecked((long)(0x800000008000000AL)), unchecked((long
			)(0x8000000080008081L)), unchecked((long)(0x8000000000008080L)), unchecked((long
			)(0x0000000080000001L)), unchecked((long)(0x8000000080008008L)) };

		private static readonly int[] KeccakRhoOffsets = new int[] { 0, 1, 62, 28, 27, 36
			, 44, 6, 55, 20, 3, 10, 43, 25, 39, 41, 45, 15, 21, 8, 18, 2, 61, 56, 14 };

		private const int nrRounds = 24;

		private const int KeccakPermutationSize = 1600;

		private const int KeccakPermutationSizeInBytes = (KeccakPermutationSize / 8);

		private const int KeccakMaximumRate = 1152;

		private const int KeccakMaximumRateInBytes = (KeccakMaximumRate / 8);

		private static readonly byte[] state = new byte[KeccakPermutationSizeInBytes];

		private static readonly long[] stateAsWords = new long[KeccakPermutationSize / 64
			];

		private static readonly byte[] dataQueue = new byte[KeccakMaximumRateInBytes];

		private static readonly long[] B = new long[25];

		private static readonly long[] C = new long[5];

		private static readonly long[] D = new long[5];

		private static int rate;

		private static int capacity;

		private static byte diversifier;

		private static int hashLength;

		private static int bitsInQueue;

		/// <summary>Generate a random SHA3 Hash/Key as String.</summary>
		/// <remarks>Generate a random SHA3 Hash/Key as String.</remarks>
		/// <param name="keySize">Size of hash. Allowed are 224, 256, 384 and 512.</param>
		/// <returns>SHA3 hash as Hex String</returns>
		public virtual string generateKey(int keySize)
		{
			SecureRandom scRandom = new SecureRandom();
			byte[] randomPW = new byte[40];
			scRandom.nextBytes(randomPW);
			KeyGenSHA3.init(keySize);
			KeyGenSHA3.update(randomPW, randomPW.Length * 8);
			string output = CryptobyHelper.bytesToHexString(KeyGenSHA3.getHash());
			return output;
		}

		/// <summary>Generate a SHA3 Hash/Key depend on password input as String.</summary>
		/// <remarks>Generate a SHA3 Hash/Key depend on password input as String.</remarks>
		/// <param name="keySize">Size of hash. Allowed are 224, 256, 384 and 512.</param>
		/// <param name="password">String password which will be hashed</param>
		/// <returns>SHA3 hash as Hex String</returns>
		public virtual string generateKey(int keySize, string password)
		{
			byte[] bytePW = Encoding.UTF8.GetBytes(password);
			KeyGenSHA3.init(keySize);
			KeyGenSHA3.update(bytePW, bytePW.Length * 8);
			string output = CryptobyHelper.bytesToHexString(KeyGenSHA3.getHash());
			return output;
		}

		/// <summary>Generate a random SHA3 Hash/Key as byte array.</summary>
		/// <remarks>Generate a random SHA3 Hash/Key as byte array.</remarks>
		/// <param name="keySize">Size of hash. Allowed are 224, 256, 384 and 512.</param>
		/// <returns>SHA3 hash as byte array</returns>
		public virtual byte[] generateKeyByte(int keySize)
		{
			SecureRandom scRandom = new SecureRandom();
			byte[] randomPW = new byte[40];
			scRandom.nextBytes(randomPW);
			KeyGenSHA3.init(keySize);
			KeyGenSHA3.update(randomPW, randomPW.Length * 8);
			byte[] output = KeyGenSHA3.getHash();
			return output;
		}

		/// <summary>Generate a SHA3 Hash/Key depend on password input as byte array.</summary>
		/// <remarks>Generate a SHA3 Hash/Key depend on password input as byte array.</remarks>
		/// <param name="keySize">Size of hash. Allowed are 224, 256, 384 and 512.</param>
		/// <param name="password">String password which will be hashed</param>
		/// <returns>SHA3 hash as byte array</returns>
		public virtual byte[] generateKeyByte(int keySize, string password)
		{
			byte[] bytePW = Encoding.UTF8.GetBytes(password);
			KeyGenSHA3.init(keySize);
			KeyGenSHA3.update(bytePW, bytePW.Length * 8);
			byte[] output = KeyGenSHA3.getHash();
			return output;
		}

		private static void init(int hashLength)
		{
			switch (hashLength)
			{
				case 224:
				{
					capacity = 448;
					break;
				}

				case 256:
				{
					capacity = 512;
					break;
				}

				case 384:
				{
					capacity = 768;
					break;
				}

				case 512:
				{
					capacity = 1024;
					break;
				}

				default:
				{
					throw new Exception("Not allowed Hash Length!");
				}
			}
			rate = KeccakPermutationSize - capacity;
			diversifier = unchecked((byte)(hashLength / 8));
			KeyGenSHA3.hashLength = hashLength;
			Arrays.fill(state, unchecked((byte)0));
			Arrays.fill(dataQueue, unchecked((byte)0));
			bitsInQueue = 0;
		}

		private static void update(byte[] data, int databitlen)
		{
			if ((bitsInQueue % 8) != 0)
			{
				throw new Exception("Only the last call may contain a partial byte");
			}
			int k = 0;
			while (k < databitlen)
			{
				if ((bitsInQueue == 0) && (databitlen >= rate) && (k <= (databitlen - rate)))
				{
					int wholeBlocks = (databitlen - k) / rate;
					int curData = (int)(k / 8);
					for (int j = 0; j < wholeBlocks; j++, curData += rate / 8)
					{
						for (int i = 0; i < rate / 8; i++)
						{
							state[i] ^= data[i + curData];
						}
						keccakPermutation();
					}
					k += wholeBlocks * rate;
				}
				else
				{
					int partialBlock = databitlen - k;
					if (partialBlock + bitsInQueue > rate)
					{
						partialBlock = rate - bitsInQueue;
					}
					int partialByte = partialBlock % 8;
					partialBlock -= partialByte;
					System.Array.Copy(data, k / 8, dataQueue, bitsInQueue / 8, partialBlock / 8);
					bitsInQueue += partialBlock;
					k += partialBlock;
					if (bitsInQueue == rate)
					{
						absorbQueue();
					}
					if (partialByte > 0)
					{
						// Align the last partial byte to the least significant bits
						byte lastByte = unchecked((byte)((int)(((uint)(data[k / 8] & unchecked((int)(0xFF
							)))) >> (8 - partialByte))));
						dataQueue[bitsInQueue / 8] = lastByte;
						bitsInQueue += partialByte;
						k += partialByte;
					}
				}
			}
		}

		private static byte[] getHash()
		{
			keccakPad();
			byte[] hashval = new byte[hashLength / 8];
			if (hashLength > 0)
			{
				System.Array.Copy(dataQueue, 0, hashval, 0, hashLength / 8);
			}
			return hashval;
		}

		private static void keccakPermutation()
		{
			fromBytesToWords();
			for (int i = 0; i < nrRounds; i++)
			{
				theta();
				rho();
				pi();
				chi();
				iota(i);
			}
			fromWordsToBytes();
		}

		private static void theta()
		{
			for (int x = 0; x < 5; x++)
			{
				C[x] = 0;
				for (int y = 0; y < 5; y++)
				{
					C[x] ^= stateAsWords[index(x, y)];
				}
				D[x] = rot(C[x], 1);
			}
			for (int x_1 = 0; x_1 < 5; x_1++)
			{
				for (int y = 0; y < 5; y++)
				{
					stateAsWords[index(x_1, y)] ^= D[(x_1 + 1) % 5] ^ C[(x_1 + 4) % 5];
				}
			}
		}

		private static void rho()
		{
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					stateAsWords[index(x, y)] = rot(stateAsWords[index(x, y)], KeccakRhoOffsets[index
						(x, y)]);
				}
			}
		}

		private static void pi()
		{
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					B[index(x, y)] = stateAsWords[index(x, y)];
				}
			}
			for (int x_1 = 0; x_1 < 5; x_1++)
			{
				for (int y = 0; y < 5; y++)
				{
					stateAsWords[index(0 * x_1 + 1 * y, 2 * x_1 + 3 * y)] = B[index(x_1, y)];
				}
			}
		}

		private static void chi()
		{
			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 5; x++)
				{
					C[x] = stateAsWords[index(x, y)] ^ ((~stateAsWords[index(x + 1, y)]) & stateAsWords
						[index(x + 2, y)]);
				}
				for (int x_1 = 0; x_1 < 5; x_1++)
				{
					stateAsWords[index(x_1, y)] = C[x_1];
				}
			}
		}

		private static void iota(int indexRound)
		{
			stateAsWords[index(0, 0)] ^= KeccakRoundConstants[indexRound];
		}

		private static void keccakPad()
		{
			if ((bitsInQueue % 8) != 0)
			{
				// The bits are numbered from 0=LSB to 7=MSB
				byte padByte = unchecked((byte)(1 << (bitsInQueue % 8)));
				dataQueue[bitsInQueue / 8] |= padByte;
				bitsInQueue += 8 - (bitsInQueue % 8);
			}
			else
			{
				dataQueue[bitsInQueue / 8] = unchecked((int)(0x01));
				bitsInQueue += 8;
			}
			if (bitsInQueue == rate)
			{
				absorbQueue();
			}
			dataQueue[bitsInQueue / 8] = diversifier;
			bitsInQueue += 8;
			if (bitsInQueue == rate)
			{
				absorbQueue();
			}
			dataQueue[bitsInQueue / 8] = unchecked((byte)(rate / 8));
			bitsInQueue += 8;
			if (bitsInQueue == rate)
			{
				absorbQueue();
			}
			dataQueue[bitsInQueue / 8] = unchecked((int)(0x01));
			bitsInQueue += 8;
			if (bitsInQueue > 0)
			{
				absorbQueue();
			}
			System.Array.Copy(state, 0, dataQueue, 0, rate / 8);
		}

		private static void absorbQueue()
		{
			// bitsInQueue is assumed to be a multiple of 8
			Arrays.fill(dataQueue, bitsInQueue / 8, rate / 8, unchecked((byte)0));
			for (int i = 0; i < rate / 8; i++)
			{
				state[i] ^= dataQueue[i];
			}
			keccakPermutation();
			bitsInQueue = 0;
		}

		// Helper Functions
		private static long rot(long a, int offset)
		{
			return (a << offset) | ((long)(((ulong)a) >> -offset));
		}

		private static void fromBytesToWords()
		{
			for (int i = 0, j = 0; i < (KeccakPermutationSize / 64); i++, j += 8)
			{
				stateAsWords[i] = ((long)state[j] & unchecked((long)(0xFFL))) | ((long)state[j + 
					1] & unchecked((long)(0xFFL))) << 8 | ((long)state[j + 2] & unchecked((long)(0xFFL
					))) << 16 | ((long)state[j + 3] & unchecked((long)(0xFFL))) << 24 | ((long)state
					[j + 4] & unchecked((long)(0xFFL))) << 32 | ((long)state[j + 5] & unchecked((long
					)(0xFFL))) << 40 | ((long)state[j + 6] & unchecked((long)(0xFFL))) << 48 | ((long
					)state[j + 7] & unchecked((long)(0xFFL))) << 56;
			}
		}

		private static void fromWordsToBytes()
		{
			for (int i = 0, j = 0; i < (KeccakPermutationSize / 64); i++, j += 8)
			{
				state[j] = unchecked((byte)(stateAsWords[i]));
				state[j + 1] = unchecked((byte)(stateAsWords[i] >> 8));
				state[j + 2] = unchecked((byte)(stateAsWords[i] >> 16));
				state[j + 3] = unchecked((byte)(stateAsWords[i] >> 24));
				state[j + 4] = unchecked((byte)(stateAsWords[i] >> 32));
				state[j + 5] = unchecked((byte)(stateAsWords[i] >> 40));
				state[j + 6] = unchecked((byte)(stateAsWords[i] >> 48));
				state[j + 7] = unchecked((byte)(stateAsWords[i] >> 56));
			}
		}

		private static int index(int x, int y)
		{
			return (((x) % 5) + 5 * ((y) % 5));
		}
	}
}
