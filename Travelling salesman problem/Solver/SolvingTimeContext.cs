using System;
using System.Diagnostics;

namespace ConsoleApplication.Solver
{
    internal class SolvingTimeContext : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        public TimeSpan Elapsed => _stopwatch.Elapsed;

        public static SolvingTimeContext Instance => new SolvingTimeContext(new Stopwatch());

        private SolvingTimeContext(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
        }
    }
}