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


namespace CryptobySharp
{
	/// <summary>This class provides different helper methods and table for CryptAES class.
	/// 	</summary>
	/// <remarks>This class provides different helper methods and table for CryptAES class.
	/// 	</remarks>
	/// <author>Tobias Rees</author>
	public class CryptTablesAES
	{
		/// <summary>Constructor load different tables</summary>
		public CryptTablesAES()
		{
			// AEStables: construct various 256-byte tables needed for AES
			loadE();
			loadL();
			loadInv();
			loadS();
			loadInvS();
			loadPowX();
		}

		readonly byte[] E = new byte[256];

		readonly byte[] L = new byte[256];

		readonly byte[] S = new byte[256];

		readonly byte[] invS = new byte[256];

		readonly byte[] inv = new byte[256];

		readonly byte[] powX = new byte[15];

		// "exp" table (base 0x03)
		// "Log" table (base 0x03)
		// SubBytes table
		// inverse of SubBytes table
		// multiplicative inverse table
		// powers of x = 0x02
		// Routines to access table entries
		/// <param name="b"></param>
		/// <returns></returns>
		public virtual byte SBox(byte b)
		{
			return S[b & unchecked(0xff)];
		}

		/// <param name="b"></param>
		/// <returns></returns>
		public virtual byte invSBox(byte b)
		{
			return invS[b & unchecked(0xff)];
		}

		/// <param name="i"></param>
		/// <returns></returns>
		public virtual byte Rcon(int i)
		{
			return powX[i - 1];
		}

		// FFMulFast: fast multiply using table lookup
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public virtual byte FFMulFast(byte a, byte b)
		{
			if (a == 0 || b == 0)
			{
				return 0;
			}
			int t = (L[(a & unchecked((0xff)))] & unchecked((0xff))) + (L[(b & unchecked(
				(0xff)))] & unchecked((0xff)));
			if (t > 255)
			{
				t = t - 255;
			}
			return E[(t & unchecked((0xff)))];
		}

		// FFMul: slow multiply, using shifting
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public virtual byte FFMul(byte a, byte b)
		{
			byte aa = a;
			byte bb = b;
			byte r = 0;
			byte t;
			while (aa != 0)
			{
				if ((aa & 1) != 0)
				{
					r = unchecked((byte)(r ^ bb));
				}
				t = unchecked((byte)(bb & unchecked((0x80))));
				bb = unchecked((byte)(bb << 1));
				if (t != 0)
				{
					bb = unchecked((byte)(bb ^ unchecked((0x1b))));
				}
				aa = unchecked((byte)((aa & unchecked((0xff))) >> 1));
			}
			return r;
		}

		// loadE: create and load the E table
		void loadE()
		{
			byte x = unchecked((byte)unchecked((0x01)));
			int index = 0;
			E[index++] = unchecked((byte)unchecked((0x01)));
			for (int i = 0; i < 255; i++)
			{
				byte y = FFMul(x, unchecked((byte)unchecked((0x03))));
				E[index++] = y;
				x = y;
			}
		}

		// loadL: load the L table using the E table
		void loadL()
		{
			// careful: had 254 below several places
			for (int i = 0; i < 255; i++)
			{
				L[E[i] & unchecked((0xff))] = unchecked((byte)i);
			}
		}

		// loadS: load in the table S
		void loadS()
		{
			for (int i = 0; i < 256; i++)
			{
				S[i] = unchecked((byte)(subBytes(unchecked((byte)(i & unchecked((0xff))))) &
				unchecked((0xff))));
			}
		}

		// loadInv: load in the table inv
		void loadInv()
		{
			for (int i = 0; i < 256; i++)
			{
				inv[i] = unchecked((byte)(FFInv(unchecked((byte)(i & unchecked((0xff))))) & 
				unchecked((0xff))));
			}
		}

		// loadInvS: load the invS table using the S table
		void loadInvS()
		{
			for (int i = 0; i < 256; i++)
			{
				invS[S[i] & unchecked((0xff))] = unchecked((byte)i);
			}
		}

		// loadPowX: load the powX table using multiplication
		void loadPowX()
		{
			const byte x = unchecked((byte)unchecked((0x02)));
			byte xp = x;
			powX[0] = 1;
			powX[1] = x;
			for (int i = 2; i < 15; i++)
			{
				xp = FFMul(xp, x);
				powX[i] = xp;
			}
		}

		// FFInv: the multiplicative inverse of a byte value
		/// <param name="b"></param>
		/// <returns></returns>
		public virtual byte FFInv(byte b)
		{
			byte e = L[b & unchecked((0xff))];
			return E[unchecked((0xff)) - (e & unchecked((0xff)))];
		}

		// ithBIt: return the ith bit of a byte
		/// <param name="b"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		public virtual int ithBit(byte b, int i)
		{
			int[] m = new int[] { unchecked((0x01)), unchecked((0x02)), unchecked((0x04)), unchecked((0x08)), unchecked((0x10)),
				unchecked((0x20)), unchecked((0x40)), unchecked((0x80)) };
			return (b & m[i]) >> i;
		}

		// subBytes: the subBytes function
		/// <param name="b"></param>
		/// <returns></returns>
		public virtual int subBytes(byte b)
		{
			int res = 0;
			if (b != 0)
			{
				// if b == 0, leave it alone
				b = unchecked((byte)(FFInv(b) & unchecked((0xff))));
			}
			const byte c = unchecked((byte)unchecked((0x63)));
			for (int i = 0; i < 8; i++)
			{
				int temp = ithBit(b, i) ^ ithBit(b, (i + 4) % 8) ^ ithBit(b, (i + 5) % 8) ^ ithBit
					(b, (i + 6) % 8) ^ ithBit(b, (i + 7) % 8) ^ ithBit(c, i);
				res = res | (temp << i);
			}
			return res;
		}
	}
}
