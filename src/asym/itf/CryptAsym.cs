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

using asym.itf;

namespace asym.itf
{
	/// <summary>Interface for asymmetric cryptology implementations</summary>
	/// <author>Tobias Rees</author>
	public interface CryptAsym
	{
		/// <summary>Implementation of asymmetric encrypt method</summary>
		/// <param name="plainInput">Byte array input to encrypt in implemented mode</param>
		/// <param name="publicKey">Public key to encrypt plainInput parameter</param>
		/// <returns>Encrypted byte array</returns>
		byte[] encrypt(byte[] plainInput, byte[] publicKey);

		/// <summary>Implementation of asymmetric decrypt method</summary>
		/// <param name="cryptInput">Encrypted byte array to decrypt in implemented mode</param>
		/// <param name="privateKey">Private key to decrypt cryptInput parameter</param>
		/// <returns>Decrypted byte array</returns>
		byte[] decrypt(byte[] cryptInput, byte[] privateKey);
	}
}
