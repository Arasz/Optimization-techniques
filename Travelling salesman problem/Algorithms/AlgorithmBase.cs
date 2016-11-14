using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms
{
    public abstract class AlgorithmBase : IAlgorithm
    {
        protected readonly IEdgeFinder _edgeFinder;
        protected readonly int _steps;

        protected AlgorithmBase(int steps, IEdgeFinder edgeFinder)
        {
            _steps = steps;
            _edgeFinder = edgeFinder;
        }

        public abstract Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null);
    }
}