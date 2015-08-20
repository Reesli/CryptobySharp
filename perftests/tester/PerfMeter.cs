using System;
using System.Diagnostics;

namespace CryptobySharp
{
	public static class PerfMeter
	{

		public static void run(Action testMethod, int rounds){
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Reset();
			stopwatch.Start();
			while (stopwatch.ElapsedMilliseconds < 1200)  // A Warmup of 1000-1500 mS 
				// stabilizes the CPU cache and pipeline.
			{
				testMethod(); // Warmup
				clearMemory ();
			}
			stopwatch.Stop();
			long totalmem;
			Console.WriteLine ("Round;Runtime ms;Memory KB");
			for (int repeat = 0; repeat < rounds; ++repeat)
			{
				stopwatch.Reset();
				stopwatch.Start();
				testMethod();
				stopwatch.Stop();
				totalmem = getUsedMemoryKB ();
				clearMemory ();
				Console.WriteLine((1+repeat)+";"+stopwatch.ElapsedMilliseconds + ";"
					+totalmem);
			}
		}

		private static long getUsedMemoryKB(){
			return (GC.GetTotalMemory (false) / 1024);
		}

		private static void clearMemory(){
			GC.Collect ();
			GC.WaitForPendingFinalizers();
			GC.Collect ();
		}
	}
}

