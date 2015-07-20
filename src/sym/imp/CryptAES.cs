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

using java.nio;
using sym.imp;
using sym.itf;

namespace CryptobySharp
{
	/// <summary>
	/// This class is an implementation of Rijndael AES cryptology algorithm which
	/// uses CBC mode.
	/// </summary>
	/// <remarks>
	/// This class is an implementation of Rijndael AES cryptology algorithm which
	/// uses CBC mode.
	/// </remarks>
	/// <author>Tobias Rees</author>
	public class CryptAES : CryptSym
	{
		private const int nBlocks = 4;

		private const int nBytes = nBlocks * 4;

		private int keyLength;

		private int nRounds;

		private int keyCount;

		private CryptTablesAES tab;

		// words in a block, always 4 in AES Standard
		// Bytes in a block, four time of nBlocks
		// key length in words
		// number of rounds, = keyLength + 6
		// position in tempKey for RoundKey (= 0 each encrypt)
		// all the tables needed for AESy
		private byte[] initKeyExpand(byte[] key)
		{
			keyLength = (key.Length / 4);
			// words in a key, = 4, or 6, or 8
			nRounds = keyLength + 6;
			// corresponding number of rounds
			tab = new CryptTablesAES();
			// class to give values of various functions
			return expandKey(key);
		}

		/// <summary>Encrypt a byte array in blocks with AES algorithm in CBC mode.</summary>
		/// <remarks>Encrypt a byte array in blocks with AES algorithm in CBC mode.</remarks>
		/// <param name="plainInput">Plain byte array to encrypt</param>
		/// <param name="key">256 Bit key to encrypt plainInput</param>
		/// <returns>Return encrypted byte array</returns>
		public virtual byte[] encrypt(byte[] plainInput, byte[] key)
		{
			int inputLength = plainInput.Length;
			int percentProgress;
			int prevPercent = -1;
			byte[] inputLengthByte = ((byte[])ByteBuffer.allocate(4).order(ByteOrder.BIG_ENDIAN
				).putInt(inputLength).array());
			int restInput = plainInput.Length % nBytes;
			byte[] exKey = initKeyExpand(key);
			byte[] cipher = new byte[nBytes];
			byte[] cryptOutput = new byte[(inputLength - restInput) + nBytes * 2];
			int outputLength = cryptOutput.Length;
			byte[] initVector = new byte[nBytes];
			byte[] nextBlock = new byte[nBytes];
			SecureRandom scRandom = new SecureRandom();
			// Copy plaintext Array into crypt Array
			System.Array.Copy(plainInput, 0, cryptOutput, 0, inputLength);
			// Fill Initialization Vector with Random Bytes
			scRandom.nextBytes(initVector);
			// Copy first Input Block to nextBlock
			System.Array.Copy(cryptOutput, 0, nextBlock, 0, nBytes);
			// XOR Random initVektor with first Input Block
			nextBlock = CryptobyHelper.xorByteArrays(nextBlock, initVector);
			// Copy xored prevBlock into first Input Block
			System.Array.Copy(nextBlock, 0, cryptOutput, 0, nBytes);
			// Encrypt last BlockArray
			initVector = this.encryptCipher(initVector, exKey);
			// Add the initVector Array in to last BlockArray and encrypt it
			System.Array.Copy(initVector, 0, cryptOutput, outputLength - nBytes, nBytes);
			// Add in the first Byte after CryptText the origin length of plaintext Array
			System.Array.Copy(inputLengthByte, 0, cryptOutput, outputLength - nBytes * 2, 4);
			// Encrypt every Block in CBC Mode
			for (int i = 0; i < outputLength - nBytes; i += nBytes)
			{
				// Convert i to percent for ProgressBar
				percentProgress = (int)(((float)i / (float)(outputLength - nBytes)) * 100);
				// Print ProgressBar
				if (percentProgress > prevPercent)
				{
					CryptobyHelper.printProgressBar(percentProgress);
				}
				prevPercent = percentProgress;
				// Copy current block in to Cipher Array
				System.Array.Copy(nextBlock, 0, cipher, 0, nBytes);
				// Encrypt Cipher
				cipher = this.encryptCipher(cipher, exKey);
				// CBC Mode: XOR next PlainBlock with encrypted Cipher
				if (i + nBytes < outputLength)
				{
					System.Array.Copy(cryptOutput, i + nBytes, nextBlock, 0, nBytes);
					nextBlock = CryptobyHelper.xorByteArrays(nextBlock, cipher);
				}
				// Copy Cipher back in decryptOutput Array
				System.Array.Copy(cipher, 0, cryptOutput, i, nBytes);
			}
			CryptobyHelper.printProgressBar(100);
			return cryptOutput;
		}

		/// <summary>Decrypt a byte array in blocks with AES algorithm in CBC mode.</summary>
		/// <remarks>Decrypt a byte array in blocks with AES algorithm in CBC mode.</remarks>
		/// <param name="cryptInput">Encrypted byte array to decrypt</param>
		/// <param name="key">256 Bit key to decrypt cryptInput</param>
		/// <returns>Return decrypted byte array</returns>
		public virtual byte[] decrypt(byte[] cryptInput, byte[] key)
		{
			int percentProgress;
			int prevPercent = -1;
			byte[] exKey = initKeyExpand(key);
			byte[] decryptOutput = cryptInput;
			int outputLength = decryptOutput.Length;
			byte[] cipher = new byte[nBytes];
			byte[] inputLengthByte = new byte[nBytes];
			byte[] plainOutput;
			byte[] initVector = new byte[nBytes];
			byte[] prevBlock = new byte[nBytes];
			// Add the initVector Array in to last BlockArray and encrypt it
			System.Array.Copy(decryptOutput, outputLength - nBytes, initVector, 0, nBytes);
			// Decrypt last BlockArray
			initVector = this.decryptCipher(initVector, exKey);
			// Copy initVector to prevBlock Array
			System.Array.Copy(initVector, 0, prevBlock, 0, nBytes);
			for (int i = 0; i < outputLength - nBytes; i += nBytes)
			{
				// Convert i to percent for ProgressBar
				percentProgress = (int)(((float)i / (float)(outputLength - nBytes)) * 100);
				// Print ProgressBar
				if (percentProgress > prevPercent)
				{
					CryptobyHelper.printProgressBar(percentProgress);
				}
				prevPercent = percentProgress;
				// Copy current block in to Cipher Array
				System.Array.Copy(decryptOutput, i, cipher, 0, nBytes);
				// Decrypt Cipher
				cipher = this.decryptCipher(cipher, exKey);
				// CBC Mode: XOR next PlainBlock with encrypted Cipher
				if (i + nBytes < outputLength)
				{
					cipher = CryptobyHelper.xorByteArrays(prevBlock, cipher);
					System.Array.Copy(decryptOutput, i, prevBlock, 0, nBytes);
				}
				// Copy Cipher back in current decryptOutput Array
				System.Array.Copy(cipher, 0, decryptOutput, i, nBytes);
			}
			// Read last Index of encryptet Output  
			// and use the Integer Content for lenght of plainOutput
			System.Array.Copy(cryptInput, outputLength - nBytes * 2, inputLengthByte, 0, 4);
			int lengthOriginArray = ByteBuffer.wrap(inputLengthByte).order(ByteOrder.BIG_ENDIAN
				).getInt();
			try
			{
				plainOutput = new byte[lengthOriginArray];
				System.Array.Copy(decryptOutput, 0, plainOutput, 0, lengthOriginArray);
			}
			catch (Exception)
			{
				plainOutput = cryptInput;
			}
			CryptobyHelper.printProgressBar(100);
			return plainOutput;
		}

		private byte[] decryptCipher(byte[] cipher, byte[] exKey)
		{
			keyCount = 4 * nBlocks * (nRounds + 1);
			byte[][] state = arrayToMatrix(cipher);
			// Preround
			state = invAddRoundKey(state, exKey);
			// Crypt Rounds
			for (int i = 1; i < nRounds; i++)
			{
				state = invShiftRows(state);
				state = invSubBytes(state);
				state = invAddRoundKey(state, exKey);
				state = invMixColumns(state);
			}
			// Endround
			state = invShiftRows(state);
			state = invSubBytes(state);
			state = invAddRoundKey(state, exKey);
			return matrixToArray(state);
		}

		private byte[] encryptCipher(byte[] cipher, byte[] exKey)
		{
			keyCount = 0;
			byte[][] state = arrayToMatrix(cipher);
			// Preround
			state = addRoundKey(state, exKey);
			// Crypt Rounds
			for (int i = 1; i < nRounds; i++)
			{
				state = subBytes(state);
				state = shiftRows(state);
				state = mixColumns(state);
				state = addRoundKey(state, exKey);
			}
			// Endround
			state = subBytes(state);
			state = shiftRows(state);
			state = addRoundKey(state, exKey);
			return matrixToArray(state);
		}

		// Expand Key in 4xnk Matrix
		private byte[] expandKey(byte[] key)
		{
			byte[] tempKey = new byte[4 * nBlocks * (nRounds + 1)];
			// room for expanded key
			byte[] temp = new byte[4];
			// first just copy key to tempKey
			int j = 0;
			while (j < 4 * keyLength)
			{
				tempKey[j] = key[j++];
			}
			// here j == 4*keyLength;
			int i;
			while (j < 4 * nBlocks * (nRounds + 1))
			{
				i = j / 4;
				// j is always multiple of 4 here
				// handle everything word-at-a time, 4 bytes at a time
				for (int iTemp = 0; iTemp < 4; iTemp++)
				{
					temp[iTemp] = tempKey[j - 4 + iTemp];
				}
				if (i % keyLength == 0)
				{
					byte tTemp;
					byte tRcon;
					byte oldtemp0 = temp[0];
					for (int iTemp_1 = 0; iTemp_1 < 4; iTemp_1++)
					{
						if (iTemp_1 == 3)
						{
							tTemp = oldtemp0;
						}
						else
						{
							tTemp = temp[iTemp_1 + 1];
						}
						if (iTemp_1 == 0)
						{
							tRcon = tab.Rcon(i / keyLength);
						}
						else
						{
							tRcon = 0;
						}
						temp[iTemp_1] = unchecked((byte)(tab.SBox(tTemp) ^ tRcon));
					}
				}
				else
				{
					if (keyLength > 6 && (i % keyLength) == 4)
					{
						for (int iTemp_1 = 0; iTemp_1 < 4; iTemp_1++)
						{
							temp[iTemp_1] = tab.SBox(temp[iTemp_1]);
						}
					}
				}
				for (int iTemp_2 = 0; iTemp_2 < 4; iTemp_2++)
				{
					tempKey[j + iTemp_2] = unchecked((byte)(tempKey[j - 4 * keyLength + iTemp_2] ^ temp
						[iTemp_2]));
					j = j + 4;
				}
			}
			return tempKey;
		}

		// Encryption Functions
		// ShiftRows: simple circular shift of rows 1, 2, 3 by 1, 2, 3
		private byte[][] shiftRows(byte[][] state)
		{
			byte[] t = new byte[4];
			for (int r = 1; r < 4; r++)
			{
				for (int c = 0; c < nBlocks; c++)
				{
					t[c] = state[r][(c + r) % nBlocks];
				}
				System.Array.Copy(t, 0, state[r], 0, nBlocks);
			}
			return state;
		}

		// MixColumns: complex and sophisticated mixing of columns
		private byte[][] mixColumns(byte[][] state)
		{
			int[] sp = new int[4];
			byte b02 = unchecked((byte)unchecked((int)(0x02)));
			byte b03 = unchecked((byte)unchecked((int)(0x03)));
			for (int c = 0; c < 4; c++)
			{
				sp[0] = tab.FFMul(b02, state[0][c]) ^ tab.FFMul(b03, state[1][c]) ^ state[2][c] ^
					 state[3][c];
				sp[1] = state[0][c] ^ tab.FFMul(b02, state[1][c]) ^ tab.FFMul(b03, state[2][c]) ^
					 state[3][c];
				sp[2] = state[0][c] ^ state[1][c] ^ tab.FFMul(b02, state[2][c]) ^ tab.FFMul(b03, 
					state[3][c]);
				sp[3] = tab.FFMul(b03, state[0][c]) ^ state[1][c] ^ state[2][c] ^ tab.FFMul(b02, 
					state[3][c]);
				for (int i = 0; i < 4; i++)
				{
					state[i][c] = unchecked((byte)(sp[i]));
				}
			}
			return state;
		}

		// SubBytes: apply Sbox substitution to each byte of state
		private byte[][] subBytes(byte[][] state)
		{
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < nBlocks; col++)
				{
					state[row][col] = tab.SBox(state[row][col]);
				}
			}
			return state;
		}

		// AddRoundKey: xor a portion of expanded key with state
		private byte[][] addRoundKey(byte[][] state, byte[] exKey)
		{
			for (int c = 0; c < nBlocks; c++)
			{
				for (int r = 0; r < 4; r++)
				{
					state[r][c] = unchecked((byte)(state[r][c] ^ exKey[keyCount++]));
				}
			}
			return state;
		}

		// Decryption Functions
		// InvShiftRows: right circular shift of rows 1, 2, 3 by 1, 2, 3
		private byte[][] invShiftRows(byte[][] state)
		{
			byte[] t = new byte[4];
			for (int r = 1; r < 4; r++)
			{
				for (int c = 0; c < nBlocks; c++)
				{
					t[(c + r) % nBlocks] = state[r][c];
				}
				System.Array.Copy(t, 0, state[r], 0, nBlocks);
			}
			return state;
		}

		// InvMixColumns: complex and sophisticated mixing of columns
		private byte[][] invMixColumns(byte[][] state)
		{
			int[] sp = new int[4];
			byte b0b = unchecked((byte)unchecked((int)(0x0b)));
			byte b0d = unchecked((byte)unchecked((int)(0x0d)));
			byte b09 = unchecked((byte)unchecked((int)(0x09)));
			byte b0e = unchecked((byte)unchecked((int)(0x0e)));
			for (int c = 0; c < 4; c++)
			{
				sp[0] = tab.FFMul(b0e, state[0][c]) ^ tab.FFMul(b0b, state[1][c]) ^ tab.FFMul(b0d
					, state[2][c]) ^ tab.FFMul(b09, state[3][c]);
				sp[1] = tab.FFMul(b09, state[0][c]) ^ tab.FFMul(b0e, state[1][c]) ^ tab.FFMul(b0b
					, state[2][c]) ^ tab.FFMul(b0d, state[3][c]);
				sp[2] = tab.FFMul(b0d, state[0][c]) ^ tab.FFMul(b09, state[1][c]) ^ tab.FFMul(b0e
					, state[2][c]) ^ tab.FFMul(b0b, state[3][c]);
				sp[3] = tab.FFMul(b0b, state[0][c]) ^ tab.FFMul(b0d, state[1][c]) ^ tab.FFMul(b09
					, state[2][c]) ^ tab.FFMul(b0e, state[3][c]);
				for (int i = 0; i < 4; i++)
				{
					state[i][c] = unchecked((byte)(sp[i]));
				}
			}
			return state;
		}

		// InvSubBytes: apply inverse Sbox substitution to each byte of state
		private byte[][] invSubBytes(byte[][] state)
		{
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < nBlocks; col++)
				{
					state[row][col] = tab.invSBox(state[row][col]);
				}
			}
			return state;
		}

		// InvAddRoundKey: same as AddRoundKey, but backwards
		private byte[][] invAddRoundKey(byte[][] state, byte[] key)
		{
			for (int c = nBlocks - 1; c >= 0; c--)
			{
				for (int r = 3; r >= 0; r--)
				{
					state[r][c] = unchecked((byte)(state[r][c] ^ key[--keyCount]));
				}
			}
			return state;
		}

		// Help Functions
		// Converts the given byte array to a 4 by 4 matrix by column
		private byte[][] arrayToMatrix(byte[] array)
		{
			byte[][] matrix = new byte[][] { new byte[keyLength], new byte[keyLength], new byte
				[keyLength], new byte[keyLength] };
			int inLoc = 0;
			for (int c = 0; c < nBlocks; c++)
			{
				for (int r = 0; r < 4; r++)
				{
					matrix[r][c] = array[inLoc++];
				}
			}
			return matrix;
		}

		// Converts the given matrix to the corresponding array (by columns)
		private byte[] matrixToArray(byte[][] matrix)
		{
			byte[] array = new byte[16];
			int outLoc = 0;
			for (int c = 0; c < nBlocks; c++)
			{
				for (int r = 0; r < 4; r++)
				{
					array[outLoc++] = matrix[r][c];
				}
			}
			return array;
		}
	}
}
