using NUnit.Framework;
using System;

namespace CryptobySharp
{
	public class TestFileManagerJava
	{
		const int cycles = 1;
		byte[] file1;
		byte[] file10;
		byte[] file100;
		int mb1;
		int mb10;
		int mb100;
		String filePath;

		[TestFixtureSetUp] public void Init()
		{ 
			filePath = "perfTestFile";
			mb1 = 1024 * 1024;
			mb10 = mb1 * 10;
			mb100 = mb10 * 10;
		}

		public void testWrite1ToFile() {
			for(int i = 0; i < cycles; i++)
			{
				CryptobyFileManager.putBytesToFile (filePath, file1);
			}
		}

		public void testWrite10ToFile() {
			for(int i = 0; i < cycles; i++)
			{
				CryptobyFileManager.putBytesToFile (filePath, file10);
			}
		}

		public void testWrite100ToFile() {
			for(int i = 0; i < cycles; i++)
			{
				CryptobyFileManager.putBytesToFile (filePath, file100);
			}
		}

		public void testRead1FromFile(){
			file1 = CryptobyFileManager.getBytesFromFile(filePath);
		}

		public void testRead10FromFile(){
			file10 = CryptobyFileManager.getBytesFromFile(filePath);
		}

		public void testRead100FromFileJava(){
			file100 = CryptobyFileManager.getBytesFromFileJava(filePath);
		}

		[Test]
		public void testRun(){
			int rounds = 20;

			Console.WriteLine ("Test Performance Cryptoby FileManager");
			Console.WriteLine ("Rounds: " + rounds);
			Console.WriteLine ("Cycles per Round: " + cycles);

//			Console.WriteLine ("\nWrite 1MB to File");
//			file1 = new byte[mb1];
//			PerfMeter.run(new Action(testWrite1ToFile),rounds);
//			Console.WriteLine ("\nRead 1MB from File");
//			PerfMeter.run(new Action(testRead1FromFile),rounds);
//			file1 = null;
//
//			Console.WriteLine ("\nWrite 10MB to File");
//			file10 = new byte[mb10];
//			PerfMeter.run(new Action(testWrite10ToFile),rounds);
//			Console.WriteLine ("\nRead 10MB from File");
//			PerfMeter.run(new Action(testRead10FromFile),rounds);
//			file10 = null;

			Console.WriteLine ("\nWrite 100MB to File");
			file100 = new byte[mb100];
			PerfMeter.run(new Action(testWrite100ToFile),rounds);

			Console.WriteLine ("\nRead 100MB from File with OpenJDK Libs");
			PerfMeter.run(new Action(testRead100FromFileJava),rounds);

			file100 = null;
			CryptobyFileManager.putBytesToFile (filePath, new byte[0]);
		}

	}
}

