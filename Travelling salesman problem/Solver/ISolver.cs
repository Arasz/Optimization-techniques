using ConsoleApplication.Common;
using System.Collections.Generic;

namespace ConsoleApplication.Solver
{
    public interface ISolver
    {
        IEnumerable<Point> BestPath { get; }

        int BestResult { get; }

        int MeanReasult { get; }

        int WorstResult { get; }

        void Solve();
    }
}