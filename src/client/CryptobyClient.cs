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
	/// <summary>This class start the Cryptoby application with initial parameters</summary>
	/// <author>Tobias Rees</author>
	public sealed class CryptobyClient
	{
		private CryptobyCore core;

		private string primTestArt;

		private int primetestrounds;

		private string keySymArt;

		private string keyAsymArt;

		private string cryptSymArt;

		private string cryptAsymArt;

		private string ui;

		/// <summary>
		/// Constructor set default implementations of classes, which will be
		/// initialed in the CryptobyCore class.
		/// </summary>
		/// <remarks>
		/// Constructor set default implementations of classes, which will be
		/// initialed in the CryptobyCore class.
		/// </remarks>
		public CryptobyClient()
		{
			this.setPrimTestArt("MillerRabin");
			this.setPrimetestrounds(5);
			this.setUi("console");
			this.setCryptSymArt("AES");
			this.setCryptAsymArt("RSA");
			this.setKeySymArt("SHA3");
			this.setKeyAsymArt("RSA");
			this.core = new CryptobyCore(this);
		}

		/// <summary>Start CryptobyClient class and run User Interface in core object</summary>
		/// <param name="args">no arguments yet</param>
		public static void Main(string[] args)
		{
			CryptobyClient client = new CryptobyClient();
			System.Console.Out.WriteLine("Fork me on Github: https://github.com/Reesli/Cryptoby/fork"
				);
			client.getCore().getUi().run();
		}

		/// <summary>Close application</summary>
		public void exitApp()
		{
			System.Environment.Exit(1);
		}

		/// <returns>Get Core object of application</returns>
		public CryptobyCore getCore()
		{
			return core;
		}

		/// <param name="core">Set a new Core object in application</param>
		public void setCore(CryptobyCore core)
		{
			this.core = core;
		}

		/// <returns>Get implementation of Primetest</returns>
		public string getPrimTestArt()
		{
			return primTestArt;
		}

		/// <param name="primTestArt">Set implementation of Primetest</param>
		public void setPrimTestArt(string primTestArt)
		{
			this.primTestArt = primTestArt;
		}

		/// <returns>Get rounds which used Primetest implementation</returns>
		public int getPrimetestrounds()
		{
			return primetestrounds;
		}

		/// <param name="primetestrounds">Set rounds for Primetest implementation if needed</param>
		public void setPrimetestrounds(int primetestrounds)
		{
			this.primetestrounds = primetestrounds;
		}

		/// <returns>Get implementation of User Interface</returns>
		public string getUi()
		{
			return ui;
		}

		/// <param name="ui">Set implementation of User Interface</param>
		public void setUi(string ui)
		{
			this.ui = ui;
		}

		/// <returns>
		/// Get implementation of key generator for a symmetric cryptologic
		/// mode
		/// </returns>
		public string getKeySymArt()
		{
			return keySymArt;
		}

		/// <param name="keySymArt">
		/// Set implementation of key generator for a symmetric
		/// cryptologic mode
		/// </param>
		public void setKeySymArt(string keySymArt)
		{
			this.keySymArt = keySymArt;
		}

		/// <returns>
		/// Get implementation of key generator for an asymmetric cryptologic
		/// mode
		/// </returns>
		public string getKeyAsymArt()
		{
			return keyAsymArt;
		}

		/// <param name="keyAsymArt">
		/// Set implementation of key generator for an asymmetric
		/// cryptologic mode
		/// </param>
		public void setKeyAsymArt(string keyAsymArt)
		{
			this.keyAsymArt = keyAsymArt;
		}

		/// <returns>Get implementation of a symmetric cryptologic mode</returns>
		public string getCryptSymArt()
		{
			return cryptSymArt;
		}

		/// <param name="cryptSymArt">Set implementation of a symmetric cryptologic mode</param>
		public void setCryptSymArt(string cryptSymArt)
		{
			this.cryptSymArt = cryptSymArt;
		}

		/// <returns>Get implementation of an asymmetric cryptologic mode</returns>
		public string getCryptAsymArt()
		{
			return cryptAsymArt;
		}

		/// <param name="cryptAsymArt">Set implementation of an asymmetric cryptologic mode</param>
		public void setCryptAsymArt(string cryptAsymArt)
		{
			this.cryptAsymArt = cryptAsymArt;
		}
	}
}
