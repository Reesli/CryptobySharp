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
			clearMemory ();
			Console.WriteLine ("Runtime ms;Memory KB");
			for (int repeat = 0; repeat < rounds; ++repeat)
			{
				stopwatch.Reset();
				stopwatch.Start();
				testMethod();
				stopwatch.Stop();
				clearMemory ();
				Console.WriteLine(stopwatch.ElapsedMilliseconds + ";"
					+(GC.GetTotalMemory(false)/1024));
			}
		}

		private static void clearMemory(){
			GC.Collect ();
			GC.WaitForPendingFinalizers();
			GC.Collect ();
		}
	}
}

