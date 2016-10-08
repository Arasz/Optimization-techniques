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

            path = new List<int>();
            if (_steps < 1)
            {
                return 0;
            }
            completeGraph.CurrentNode = startNode;
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            //find nearest to start point
            var nearestUnvisited = completeGraph.NearestNode(unvisitedNodes);
            pathCost += completeGraph.Cost(startNode, nearestUnvisited);
            pathCost += completeGraph.Cost(nearestUnvisited, startNode);

            completeGraph.CurrentNode = nearestUnvisited;
            unvisitedNodes.Remove(nearestUnvisited);

            path.Add(nearestUnvisited);
            path.Add(startNode);
            steps--;

            pathCost += completeGraph.Cost(startNode);
            {
                Result bestResult = new Result();
                for (int node = 0; node < (path.Count - 2); node++)
                {
                    var localBestResult = getBestResult(path[node], path[node + 1], unvisitedNodes, graph);
                    if (bestResult.cost < 0 || localBestResult.cost < bestResult.cost)
                    {
                        bestResult = localBestResult;
                        bestResult.followingNode = node + 1;
                    }
                }
                int previousNode = path[bestResult.followingNode - 1];
                int followingNode = path[bestResult.followingNode];
                path.Insert(bestResult.followingNode, bestResult.bestNode);
                pathCost -= completeGraph.Cost(previousNode, followingNode);
                pathCost += completeGraph.Cost(previousNode, bestResult.bestNode) + completeGraph.Cost(bestResult.bestNode, followingNode);
                unvisitedNodes.Remove(bestResult.bestNode);
            }
            return pathCost;
        }

        private Result getBestResult(int firstNode, int secondNode, List<int> unvisitedNodes, Graph graph)
        {
            Result result = new Result();

            foreach (int unvisitedNode in unvisitedNodes)
            {
                int previousCost = graph.Cost(firstNode, secondNode);
                int cost = graph.Cost(firstNode, unvisitedNode) + graph.Cost(unvisitedNode, secondNode);
                int costDifference = cost - previousCost;
                if (result.cost < 0 || costDifference < result.cost)
                {
                    result.cost = costDifference;
                    result.bestNode = unvisitedNode;
                }
            }
            return result;
        }

    }

    class Result
    {

        public Result()
        {
            bestNode = -1;
            cost = -1;
            followingNode = -1;
        }
        public int bestNode { get; set; }
        public int cost { get; set; }
        public int followingNode { get; set; }
    }
}