using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class NearestNeighborAlgorithm : IAlgorithm
    {
        private int _steps { get; }

        public NearestNeighborAlgorithm(int steps)
        {
            _steps = steps;
        }

        public int Solve(int startNode, IGraph completeGraph, out IList<int> path)
        {
            var iterator = completeGraph.Iterator;
            iterator.MoveTo(startNode);

            path = new List<int>();
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            while (--steps > 0)
            {
                var edgeToNearestNode = NearestNodeEdge(iterator.Edges, unvisitedNodes);

                pathCost += edgeToNearestNode.Weight;

                iterator.MoveAlongEdge(edgeToNearestNode);

                unvisitedNodes.Remove(iterator.CurrentNode);

                path.Add(iterator.CurrentNode);
            }

            pathCost += iterator.EdgeWeight(startNode);
            path.Add(startNode);

            return pathCost;
        }

        private Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes) => edges
            .Where(edge => unvisitedNodes.Contains(edge.TargetNode))
            .OrderBy(edge => edge.Weight)
            .First();
    }
}