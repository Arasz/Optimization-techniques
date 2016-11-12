using System;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver
{
    public interface ISolverStatistics
    {
        Path BestPath { get; }

        TimeSpan MaxSolvingTime { get; }

        int MeanCost { get; }

        TimeSpan MeanSolvingTime { get; }

        TimeSpan MinSolvingTime { get; }

        int WorstCost { get; }
    }
}