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
using keygen.itf;
using sym.itf;
using ui.itf;

namespace CryptobySharp
{
	/// <summary>
	/// This class create objects of all interfaces in this application with in the
	/// client object defined implementation of these interfaces.
	/// </summary>
	/// <remarks>
	/// This class create objects of all interfaces in this application with in the
	/// client object defined implementation of these interfaces. User Interfaces get
	/// and set all methods of other interfaces over this core object.
	/// </remarks>
	/// <author>Tobias Rees</author>
	public sealed class CryptobyCore
	{
		private CryptSym cryptSym;

		private CryptAsym cryptAsym;

		private KeyGenSym keyGenSym;

		private KeyGenAsym keyGenAsym;

		private PrimeTest primetest;

		private CryptobyClient client;

		private CryptobyUI ui;

		/// <summary>
		/// Constructor set given CryptobyClient object to client variable and
		/// initialize every interface with from client given implementation
		/// </summary>
		/// <param name="client">
		/// Need CryptobyClient object with defined implementation of
		/// used interfaces
		/// </param>
		public CryptobyCore(CryptobyClient client)
		{
			this.setClient(client);
			this.initCryptSym();
			this.initCryptAsym();
			this.initSymKey();
			this.initAsymKey();
			this.initPrimeTest();
			this.initUI();
		}

		/// <summary>Initialize symmetric cryptologic mode which is defined in client object</summary>
		public void initCryptSym()
		{
			if (this.client.getCryptSymArt().Equals("AES"))
			{
				this.setCryptSym(new CryptAES());
			}
			else
			{
				this.setCryptSym(new CryptAES());
			}
		}

		/// <summary>Initialize asymmetric cryptologic mode which is defined in client object
		/// 	</summary>
		public void initCryptAsym()
		{
			if (this.client.getCryptAsymArt().Equals("RSA"))
			{
				this.setCryptAsym(new CryptRSA());
			}
			else
			{
				this.setCryptAsym(new CryptRSA());
			}
		}

		/// <summary>Initialize symmetric key generator which is defined in client object</summary>
		public void initSymKey()
		{
			if (this.client.getKeySymArt().Equals("SHA3"))
			{
				this.setKeyGenSym(new KeyGenSHA3());
			}
			else
			{
				this.setKeyGenSym(new KeyGenSHA3());
			}
		}

		/// <summary>Initialize asymmetric key generator which is defined in client object</summary>
		public void initAsymKey()
		{
			if (this.client.getKeySymArt().Equals("RSA"))
			{
				this.setKeyGenAsym(new KeyGenRSA(this));
			}
			else
			{
				this.setKeyGenAsym(new KeyGenRSA(this));
			}
		}

		/// <summary>Initialize Primetest mode which is defined in client object</summary>
		public void initPrimeTest()
		{
			if (this.client.getPrimTestArt().Equals("MillerRabin"))
			{
				this.setPrimetest(new MillerRabin(this.client.getPrimetestrounds()));
			}
			else
			{
				this.setPrimetest(new MillerRabin(this.client.getPrimetestrounds()));
			}
		}

		/// <summary>Initialize User Interface which is defined in client object</summary>
		public void initUI()
		{
			if (this.client.getUi().Equals("console"))
			{
				this.ui = new CryptobyConsole(this);
			}
			else
			{
				this.ui = new CryptobyConsole(this);
			}
		}

		/// <returns>Get Primetest object</returns>
		public PrimeTest getPrimetest()
		{
			return primetest;
		}

		/// <param name="primetest">Set implementation Primetest object</param>
		public void setPrimetest(PrimeTest primetest)
		{
			this.primetest = primetest;
		}

		/// <returns>Get CryptobyClient object</returns>
		public CryptobyClient getClient()
		{
			return client;
		}

		private void setClient(CryptobyClient client)
		{
			this.client = client;
		}

		/// <returns>Get User Interface object</returns>
		public CryptobyUI getUi()
		{
			return ui;
		}

		/// <param name="ui">Set implementation User Interface object</param>
		public void setUi(CryptobyUI ui)
		{
			this.ui = ui;
		}

		/// <returns>Get object of symmetric key generator implementation</returns>
		public KeyGenSym getKeyGenSym()
		{
			return keyGenSym;
		}

		/// <param name="keyGenSym">Set object of symmetric key generator implementation</param>
		public void setKeyGenSym(KeyGenSym keyGenSym)
		{
			this.keyGenSym = keyGenSym;
		}

		/// <returns>Get object of asymmetric key generator implementation</returns>
		public KeyGenAsym getKeyGenAsym()
		{
			return keyGenAsym;
		}

		/// <param name="keyGenAsym">Set object of asymmetric key generator implementation</param>
		public void setKeyGenAsym(KeyGenAsym keyGenAsym)
		{
			this.keyGenAsym = keyGenAsym;
		}

		/// <returns>Get object of symmetric cryptology mode implementation</returns>
		public CryptSym getCryptSym()
		{
			return cryptSym;
		}

		/// <param name="cryptSym">Set object of symmetric cryptology mode implementation</param>
		public void setCryptSym(CryptSym cryptSym)
		{
			this.cryptSym = cryptSym;
		}

		/// <returns>Get object of asymmetric cryptology mode implementation</returns>
		public CryptAsym getCryptAsym()
		{
			return cryptAsym;
		}

		/// <param name="cryptAsym">Set object of asymmetric cryptology mode implementation</param>
		public void setCryptAsym(CryptAsym cryptAsym)
		{
			this.cryptAsym = cryptAsym;
		}
	}
}
