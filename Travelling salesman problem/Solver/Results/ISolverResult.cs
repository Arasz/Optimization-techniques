using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Solver.Results
{
    public interface ISolverResult
    {
        IList<Path> Paths { get; }

        void AddPath(Path path);
    }
}