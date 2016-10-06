using System.Collections.Generic;

namespace ConsoleApplication.Solver
{
    public interface ISolver
    {
        IEnumerable<int> BestPath { get; }

        int BestResult { get; }

        int MeanReasult { get; }

        int WorstResult { get; }

        void Solve();
    }
}