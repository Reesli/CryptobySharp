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

using sym.itf;

namespace sym.itf
{
	/// <summary>Interface for symmetric cryptology implementations</summary>
	/// <author>Tobias Rees</author>
	public interface CryptSym
	{
		/// <summary>Implementation of symmetric encrypt method</summary>
		/// <param name="plainInput">Plain byte array to encrypt</param>
		/// <param name="key">Key to encrypt plainInput as byte array</param>
		/// <returns></returns>
		byte[] encrypt(byte[] plainInput, byte[] key);

		/// <summary>Implementation of symmetric decrypt method</summary>
		/// <param name="cryptInput">Plain byte array to encrypt</param>
		/// <param name="key">Key to decrypt plainInput as byte array</param>
		/// <returns></returns>
		byte[] decrypt(byte[] cryptInput, byte[] key);
	}
}
