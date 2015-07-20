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

using System.Text;

using java.io;
using System.IO;
using System;

namespace CryptobySharp
{
	/// <summary>This class provides file loading and saving from and to disk.</summary>
	/// <remarks>
	/// This class provides file loading and saving from and to disk.
	/// In special there are methods for key file loading saving.
	/// </remarks>
	/// <author>Tobias Rees</author>
	public class CryptobyFileManager
	{
		/// <summary>This method load plain or encrypted file to byte array from disk</summary>
		/// <param name="filePath">The Path to file which bytes will be loaded</param>
		/// <returns>Byte array of file from filePath</returns>
		/// <exception cref="System.IO.IOException">
		/// If file not found or other IO problems there will be
		/// throw an IOException
		/// </exception>
		public static byte[] getBytesFromFile(string filePath)
		{
			//java.io.File file = new java.io.File(filePath);
			//BufferedInputStream bis = new BufferedInputStream(new FileInputStream(file));
			//int numByte = bis.available();
			//byte[] output = new byte[numByte];
			//bis.read(output, 0, numByte);
			//return output;

//			byte[] buff = null;
//			FileStream fs = new FileStream(filePath, 
//				FileMode.Open, 
//				FileAccess.Read);
//			BinaryReader br = new BinaryReader(fs);
//			long numBytes = new FileInfo(filePath).Length;
//			buff = br.ReadBytes((int) numBytes);
//			return buff;
			return System.IO.File.ReadAllBytes(filePath);
		}

		/// <summary>This method save plain or encrypted byte array to file on disk</summary>
		/// <param name="filePath">The Path to file which will be saved to disk</param>
		/// <param name="exportByte">The byte array will be saved to disk</param>
		/// <exception cref="System.IO.IOException">
		/// If file couldn't save or other IO problems there will
		/// be throw an IOException
		/// </exception>
		public static void putBytesToFile(string filePath, byte[] exportByte)
		{	
			if (System.IO.File.Exists (filePath)) {
				System.IO.File.Delete(filePath);
			}
			else
			{
				System.IO.File.Create(filePath).Dispose();
			}
			try {
//				FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
//				BinaryWriter bw = new BinaryWriter(fs);
//				bw.Write(exportByte);
//				bw.Close();
				System.IO.File.WriteAllBytes(filePath,exportByte);
				//FileOutputStream outputStream = new FileOutputStream (filePath);
				//outputStream.write(exportByte);
				//outputStream.close();
			} catch (System.IO.FileNotFoundException) {
				System.Console.WriteLine("File or Folder not found!");
				throw new System.IO.FileNotFoundException();
			}
		}

		/// <summary>This method load a key file from disk to byte array.</summary>
		/// <remarks>
		/// This method load a key file from disk to byte array. They key in the file
		/// must be in Hex String Block format which will be merged and converted
		/// from hex String to byte array as output.
		/// </remarks>
		/// <param name="filePath">Path to key file which bytes will be loaded</param>
		/// <returns>Byte array of hex string in filePath</returns>
		/// <exception cref="System.IO.IOException">
		/// If file not found or other IO problems there will be
		/// throw an IOException
		/// </exception>
		public static byte[] getKeyFromFile(string filePath)
		{
			StreamReader streamReader = new StreamReader(filePath);
			string sb = streamReader.ReadToEnd();
			streamReader.Close();
			//StringBuilder sb;
			//sb = new StringBuilder();
			//BufferedReader br = new BufferedReader (new FileReader (filePath));
			//string line = br.readLine();
			//while (line != null)
			//{
			//	sb.Append(line);
			//	line = br.readLine();
			//}
			System.Console.WriteLine(CryptobyHelper.hexStringToBytes (sb));
			return CryptobyHelper.hexStringToBytes (sb);
		}

		/// <summary>This method save a key file from byte array to disk.</summary>
		/// <remarks>This method save a key file from byte array to disk.</remarks>
		/// <param name="filePath">Path to file where key file will be saved to disk</param>
		/// <param name="key">
		/// Key String in Hex format which will be converted to Block and
		/// saved to disk in filePath
		/// </param>
		/// <exception cref="System.IO.IOException">
		/// If file couldn't save or other IO problems there will
		/// be throw an IOException
		/// </exception>
		public static void putKeyToFile(string filePath, string key)
		{
			try {
				System.IO.File.WriteAllText (filePath, key);
			} catch (System.IO.FileNotFoundException) {
				throw new System.IO.FileNotFoundException();
			}
		}
	}
}
