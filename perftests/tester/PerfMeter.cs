using System;
using System.Diagnostics;
using System.Threading;

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
			}
			stopwatch.Stop();
			GC.Collect ();
			Console.WriteLine ("Runtime ms;Memory KB");
			for (int repeat = 0; repeat < rounds; ++repeat)
			{
				stopwatch.Reset();
				stopwatch.Start();
				testMethod();
				stopwatch.Stop();
				Console.WriteLine(stopwatch.ElapsedMilliseconds + ";"
					+(GC.GetTotalMemory(true)/1024));
			}
		}
	}
}

