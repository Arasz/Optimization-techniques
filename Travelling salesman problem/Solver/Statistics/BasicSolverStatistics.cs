using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver.Statistics
{
    internal class BasicSolverStatistics : ISolverStatistics
    {
        private readonly List<int> _costs;

        private readonly List<TimeSpan> _solvingTimes;

        public Path BestPath { get; private set; }

        public IReadOnlyList<int> Costs => _costs;

        public TimeSpan MaxSolvingTime { get; private set; }

        public int MeanCost => (int)Math.Round(_costs.Average());

        public TimeSpan MeanSolvingTime => TimeSpan.FromMilliseconds(_solvingTimes.Average(span => span.TotalMilliseconds));

        public TimeSpan MinSolvingTime { get; private set; }

        public IReadOnlyList<TimeSpan> SolvingTimes => _solvingTimes;

        public int WorstCost { get; private set; }

        public BasicSolverStatistics()
        {
            _costs = new List<int>();
            _solvingTimes = new List<TimeSpan>();

            BestPath = Path.WorstCasePath;
        }

        public void UpdateSolvingResults(Path bestPath, TimeSpan solvingTime)
        {
            UpdatePathResults(bestPath);
            UpdateTimeMeasures(solvingTime);
        }

        private void UpdatePathResults(Path path)
        {
            if (path.Cost < BestPath.Cost)
                BestPath = path;

            if (path.Cost > WorstCost)
                WorstCost = path.Cost;

            _costs.Add(path.Cost);
        }

        private void UpdateTimeMeasures(TimeSpan localTime)
        {
            if (localTime < MinSolvingTime)
                MinSolvingTime = localTime;

            if (localTime > MaxSolvingTime)
                MaxSolvingTime = localTime;

            _solvingTimes.Add(localTime);
        }
    }
}