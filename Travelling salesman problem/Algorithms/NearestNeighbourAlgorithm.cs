using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class NearestNeighbourAlgorithm : IAlgorithm
    {
        public int Solve(int startNode, Graph graph, int steps, out IList<int> path)
        {
            graph.CurrentNode = startNode;
            path = new List<int>();
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = graph.Nodes.Where(node => node != startNode).ToList();

            while (--steps > 0)
            {
                var nearestUnvisited = graph.NearestNodes().First(nearestNode => unvisitedNodes.Contains(nearestNode));
                pathCost += graph.Cost(nearestUnvisited);
                graph.CurrentNode = nearestUnvisited;
                unvisitedNodes.Remove(nearestUnvisited);
                path.Add(nearestUnvisited);
            }

            return pathCost;
        }
    }
}