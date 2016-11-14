using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Solver.Results
{
    public class NullSolverResult : ISolverResult
    {
        public IList<Path> Paths { get; } = new List<Path>();

        public void AddPath(Path path)
        {
        }
    }
}