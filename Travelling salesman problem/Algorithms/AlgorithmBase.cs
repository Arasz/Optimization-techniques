using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public abstract class AlgorithmBase : IAlgorithm
    {
        protected readonly int _steps;

        public AlgorithmBase(int steps)
        {
            _steps = steps;
        }

        public abstract int Solve(int startNode, IGraph completeGraph, out IList<int> path);

        protected virtual Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes) => edges
                                                  .Where(edge => unvisitedNodes.Contains(edge.TargetNode))
                                                  .OrderBy(edge => edge.Weight)
                                                  .First();
    }
}