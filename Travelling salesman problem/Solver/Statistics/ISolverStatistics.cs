using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleApplication.Solver.Statistics
{
    public interface ISolverStatistics
    {
        Path BestPath { get; }

        IReadOnlyList<int> Costs { get; }

        TimeSpan MaxSolvingTime { get; }

        int MeanCost { get; }

        TimeSpan MeanSolvingTime { get; }

        TimeSpan MinSolvingTime { get; }

        IReadOnlyList<TimeSpan> SolvingTimes { get; }

        int WorstCost { get; }
    }
}