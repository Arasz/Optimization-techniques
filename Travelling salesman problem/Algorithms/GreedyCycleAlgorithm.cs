using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class GreedyCycleAlgorithm : IAlgorithm
    {
        private int _steps { get; }

        public GreedyCycleAlgorithm(int steps)
        {
            _steps = steps;
        }

        public int Solve(int startNode, IGraph completeGraph, out IList<int> path)
        {
            completeGraph.CurrentNode = startNode;
            path = new List<int>();
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            while (--steps > 0)
            {
                /* var nearestUnvisited = graph.NearestFragment(unvisitedNodes);
                 pathCost += graph.Cost(nearestUnvisited);
                 graph.CurrentNode = nearestUnvisited;
                 unvisitedNodes.Remove(nearestUnvisited);
                 path.Add(nearestUnvisited);*/
            }
            pathCost += completeGraph.Cost(startNode);
            return pathCost;
        }
    }
}