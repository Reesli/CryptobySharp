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
using System.Text;
using System.Linq;

namespace CryptobySharp
{
	/// <summary>
	/// This class provides different static helper methods which will be used in the
	/// other classes of the application
	/// </summary>
	/// <author>Tobias Rees</author>
	public class CryptobyHelper
	{
		private static readonly string EOB = "EndOfBlock";

		/// <summary>Convert byte array to Hex String.</summary>
		/// <remarks>Convert byte array to Hex String.</remarks>
		/// <param name="bytes">Byte array to convert in Hex String</param>
		/// <returns>Hex String of bytes</returns>
		public static string bytesToHexString(byte[] bytes)
		{
			StringBuilder sb = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
			{
				sb.Append(b.ToString("x2"));
			}
			return sb.ToString();
		}

		/// <summary>Convert Hex String to byte array.</summary>
		/// <remarks>Convert Hex String to byte array.</remarks>
		/// <param name="hexString">Hex String to convert in byte array</param>
		/// <returns>Byte array of hexString</returns>
		public static byte[] hexStringToBytes(string hexString)
		{

			return Enumerable.Range(0, hexString.Length)
				.Where(x => x % 2 == 0)
				.Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
				.ToArray();
			//byte[] bytes = new byte[hexString.Length / 2];
			//for (int i = 0; i < bytes.Length; i++)
			//{
			//	bytes[i] = unchecked((byte)Convert.ToInt16((hexString.Substring(2 * i, 2 * i + 2))));
			//}
			//return bytes;
		}

		/// <summary>Convert byte array to Upper Hex String.</summary>
		/// <remarks>Convert byte array to Upper Hex String.</remarks>
		/// <param name="bytes">Byte array to convert in Upper Hex String</param>
		/// <returns>Upper Hex String of bytes</returns>
		public static string bytesToHexStringUpper(byte[] bytes)
		{

			StringBuilder sb = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
			{
				sb.Append(b.ToString("X2"));
			}
			return sb.ToString();
			//char[] hexArray = "0123456789ABCDEF".ToCharArray();
			//char[] hexChars = new char[bytes.Length * 2];
			//for (int j = 0; j < bytes.Length; j++)
			//{
			//	int v = bytes[j] & unchecked((int)(0xFF));
			//	hexChars[j * 2] = hexArray[(int)(((uint)v) >> 4)];
			//	hexChars[j * 2 + 1] = hexArray[v & unchecked((int)(0x0F))];
			//}
			//return new string(hexChars);
		}

		/// <summary>Calculate Log2 of a BigInteger object value.</summary>
		/// <remarks>Calculate Log2 of a BigInteger object value.</remarks>
		/// <param name="val">BigInteger object value which will calculate with log2</param>
		/// <returns>Result of calculating of val as double value</returns>
		public static double logBigInteger(BigInteger val)
		{
			double LOG2 = java.lang.Math.log(2.0);
			int blex = val.bitLength() - 1022;
			if (blex > 0)
			{
				val = val.shiftRight(blex);
			}
			double res = java.lang.Math.log(val.doubleValue());
			return blex > 0 ? res + blex * LOG2 : res;
		}

		/// <summary>Convert a char array to block format with defined line length.</summary>
		/// <remarks>Convert a char array to block format with defined line length.</remarks>
		/// <param name="charTextHex">Char array from a String to convert</param>
		/// <returns>Return converted block String</returns>
		public static string charToBlockString(char[] charTextHex)
		{
			int lenLine = 64;
			java.lang.StringBuilder sb = new java.lang.StringBuilder();
			char[] temp = new char[lenLine];
			for (int i = 0; i < charTextHex.Length; i = i + lenLine)
			{
				if ((charTextHex.Length - i) < lenLine)
				{
					temp = new char[(charTextHex.Length - i)];
					Array.Copy(charTextHex, i, temp, 0, (charTextHex.Length - i));
					sb.append(new string(temp));
					sb.append("\n");
				}
				else
				{
					Array.Copy(charTextHex, i, temp, 0, lenLine);
					sb.append(new string(temp));
					sb.append("\n");
				}
			}
			return sb.ToString();
		}

		/// <summary>Need Enter/Return key from keyboard input to continues.</summary>
		/// <remarks>
		/// Need Enter/Return key from keyboard input to continues. Used for Console
		/// User Interfaces.
		/// </remarks>
		public static void pressEnter()
		{
			do {
     			
			} while (Console.ReadKey(true).Key != ConsoleKey.Enter);
		}

		/// <summary>
		/// Method merge inputs and add a title to return String which can print
		/// encrypted text and title in User Interfaces.
		/// </summary>
		/// <remarks>
		/// Method merge inputs and add a title to return String which can print
		/// encrypted text and title in User Interfaces.
		/// </remarks>
		/// <param name="cryptType">Type of cryptology implementation as String</param>
		/// <param name="inputKeySize">Size of used key as integer</param>
		/// <param name="inputCharTextHex">
		/// Char array of String which will be converted to
		/// block
		/// </param>
		/// <returns>Return String of merged input parameter and text</returns>
		public static string printHexBlock(string cryptType, int inputKeySize, char[] inputCharTextHex
			)
		{
			java.lang.StringBuilder sb = new java.lang.StringBuilder();
			sb.append("\n").append(cryptType).append("-").append(inputKeySize).append(" encrypted Text in Hex form (Copy with '"
				 + EOB + "'):\n");
			sb.append(CryptobyHelper.charToBlockString(inputCharTextHex));
			sb.append(EOB);
			return sb.ToString();
		}

		/// <summary>Method convert private key String to block format and add a title.</summary>
		/// <remarks>Method convert private key String to block format and add a title.</remarks>
		/// <param name="privateKey">String converted to char array and than to block String</param>
		/// <returns>Return block String of privateKey</returns>
		public static string printPrivateKeyBlock(string privateKey)
		{
			java.lang.StringBuilder sb = new java.lang.StringBuilder();
			sb.append("\nPrivate Key:\n");
			sb.append(CryptobyHelper.charToBlockString(privateKey.ToCharArray()));
			sb.append(EOB);
			return sb.ToString();
		}

		/// <summary>Method convert public key String to block format and add a title.</summary>
		/// <remarks>Method convert public key String to block format and add a title.</remarks>
		/// <param name="publicKey">String converted to char array and than to block String</param>
		/// <returns>Return block String of publicKey</returns>
		public static string printPublicKeyBlock(string publicKey)
		{
			java.lang.StringBuilder sb = new java.lang.StringBuilder();
			sb.append("\nPublic Key:\n");
			sb.append(CryptobyHelper.charToBlockString(publicKey.ToCharArray()));
			sb.append(EOB);
			return sb.ToString();
		}

		/// <summary>EndOfBlock String marks the end of a block for Scanner objects</summary>
		/// <returns>Get EndOfBlock String</returns>
		public static string getEOBString()
		{
			return EOB;
		}

		/// <summary>XOR two byte arrays.</summary>
		/// <remarks>XOR two byte arrays.</remarks>
		/// <param name="firstArray">First byte array to XOR with secArray</param>
		/// <param name="secArray">Second byte array to XOR with firstArray</param>
		/// <returns>Return result of XOR as byte array</returns>
		public static byte[] xorByteArrays(byte[] firstArray, byte[] secArray)
		{
			byte[] xorArray = new byte[firstArray.Length];
			int i = 0;
			foreach (byte b in secArray)
			{
				xorArray[i] = unchecked((byte)(b ^ firstArray[i++]));
			}
			return xorArray;
		}

		/// <summary>Print a progress bar and percent of progress to console.</summary>
		/// <remarks>
		/// Print a progress bar and percent of progress to console. In this format:
		/// [====================================================================================================]
		/// 100%
		/// </remarks>
		/// <param name="percentProgress">
		/// Percent of progress. Integer value has to be
		/// between 0 and 100.
		/// </param>
		public static void printProgressBar(int percentProgress)
		{
			if (percentProgress >= 0 || percentProgress <= 100)
			{
				java.lang.StringBuilder sb = new java.lang.StringBuilder();
				sb.append("[");
				for (int i = 0; i < percentProgress; i++)
				{
					sb.append("=");
				}
				for (int i_1 = 100; i_1 > percentProgress; i_1--)
				{
					sb.append(".");
				}
				sb.append("] ");
				sb.append(percentProgress);
				sb.append("%");
				sb.append("\r");
				Console.Out.Write(sb.ToString());
			}
			else
			{
				Console.Out.Write("Input has to between 0 and 100.");
			}
		}

		/// <summary>Print IO Exception Error.</summary>
		/// <remarks>Print IO Exception Error.</remarks>
		public static void printIOExp()
		{
			Console.Out.WriteLine("File not found or other IO Error! Go back to Menu."
				);
		}
	}
}
