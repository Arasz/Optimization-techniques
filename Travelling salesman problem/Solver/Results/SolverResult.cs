using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver.Results
{
    public class SolverResult : ISolverResult
    {
        public IList<Path> Paths { get; }

        public SolverResult()
        {
            Paths = new List<Path>();
        }

        public SolverResult(IEnumerable<Path> paths)
        {
            Paths = paths.ToList();
        }

        public void AddPath(Path path) => Paths.Add(path);
    }
}