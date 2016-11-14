using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class NearestNeighborAlgorithm : AlgorithmBase
    {
        public NearestNeighborAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
        {
        }

        public override Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath)
        {
            var iterator = completeGraph.Iterator;
            iterator.MoveTo(startNode);

            var nodes = new List<int> { startNode };

            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            while (--steps > 0)
            {
                var edgeToNearestNode = _edgeFinder.NearestNodeEdge(iterator.Edges, unvisitedNodes);

                iterator.MoveAlongEdge(edgeToNearestNode);

                unvisitedNodes.Remove(iterator.CurrentNode);

                nodes.Add(iterator.CurrentNode);
            }

            nodes.Add(startNode);

            return new Path(nodes, new DefaultCostCalculationStrategy(completeGraph));
        }
    }
}