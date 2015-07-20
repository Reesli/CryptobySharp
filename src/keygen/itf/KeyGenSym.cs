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

using keygen.itf;

namespace keygen.itf
{
	/// <summary>Interface for symmetric key generator implementations</summary>
	/// <author>Tobias Rees</author>
	public interface KeyGenSym
	{
		/// <param name="keySize">Size of key which will be generated</param>
		/// <returns>Return key as String</returns>
		string generateKey(int keySize);

		/// <param name="keySize">Size of key which will be generated</param>
		/// <param name="password">Password for key</param>
		/// <returns>Return key as String</returns>
		string generateKey(int keySize, string password);

		/// <param name="keySize">Size of key which will be generated</param>
		/// <returns>Return key as byte array</returns>
		byte[] generateKeyByte(int keySize);

		/// <param name="keySize">Size of key which will be generated</param>
		/// <param name="password">Password for key</param>
		/// <returns>Return key as byte array</returns>
		byte[] generateKeyByte(int keySize, string password);
	}
}
