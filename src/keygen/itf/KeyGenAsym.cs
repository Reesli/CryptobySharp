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
	/// <summary>Interface for asymmetric key generator implementations.</summary>
	/// <remarks>
	/// Interface for asymmetric key generator implementations. With the
	/// initGenerator the keys will be generated and the getter methods provides the
	/// generated keys
	/// </remarks>
	/// <author>Tobias Rees</author>
	public interface KeyGenAsym
	{
		/// <param name="keyBitSize">Size of key which will be generated</param>
		void initGenerator(int keyBitSize);

		/// <returns>Return private key as String</returns>
		string getPrivateKey();

		/// <returns>Return public key as String</returns>
		string getPublicKey();

		/// <returns>Return private key as byte array</returns>
		byte[] getPrivateKeyByte();

		/// <returns>Return public key as byte array</returns>
		byte[] getPublicKeyByte();
	}
}
