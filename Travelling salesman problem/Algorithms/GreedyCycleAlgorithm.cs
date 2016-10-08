using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class GreedyCycleAlgorithm : AlgorithmBase
    {

        private IEdgeFinder _edgeFinder;
        public GreedyCycleAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps)
        {
            _edgeFinder = edgeFinder;
        }

        public override int Solve(int startNode, IGraph completeGraph, out IList<int> path)
        {
            var iterator = completeGraph.Iterator;

            path = new List<int>();
            if (_steps < 1)
                return 0;
            iterator.MoveTo(startNode);
            var pathCost = 0;

            path.Add(startNode);
            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            //find nearest to start point
            var nearestNodeEdge = _edgeFinder.NearestNodeEdge(iterator.Edges, unvisitedNodes);
            pathCost += nearestNodeEdge.Weight;

            iterator.MoveAlongEdge(nearestNodeEdge);
            unvisitedNodes.Remove(iterator.CurrentNode);
            path.Add(iterator.CurrentNode);
            iterator.MoveTo(path.Last());
            path.Add(startNode);
            pathCost += iterator.EdgeWeight(startNode);

            steps--;

            while (--steps > 0)
            {
                Result bestResult = new Result();
                for (var node = 0; node < (path.Count - 2); node++)
                {
                    var localBestResult = getBestResult(path[node], path[node + 1], unvisitedNodes, completeGraph);
                    if (bestResult.Cost < 0 || localBestResult.Cost < bestResult.Cost)
                    {
                        bestResult = localBestResult;
                        bestResult.FollowingNode = node + 1;
                    }
                }
                var previousNode = path[bestResult.FollowingNode - 1];
                var followingNode = path[bestResult.FollowingNode];

                path.Insert(bestResult.FollowingNode, bestResult.BestNode);

                pathCost -= completeGraph.Weight(previousNode, followingNode);
                pathCost += completeGraph.Weight(previousNode, bestResult.BestNode) + completeGraph.Weight(bestResult.BestNode, followingNode);

                unvisitedNodes.Remove(bestResult.BestNode);
            }
            return pathCost;
        }

        private Result getBestResult(int firstNode, int secondNode, List<int> unvisitedNodes, IGraph graph)
        {
            Result result = new Result();

            foreach (var unvisitedNode in unvisitedNodes)
            {
                var previousCost = graph.Weight(firstNode, secondNode);
                var cost = graph.Weight(firstNode, unvisitedNode) + graph.Weight(unvisitedNode, secondNode);
                var costDifference = cost - previousCost;
                if (result.Cost < 0 || costDifference < result.Cost)
                {
                    result.Cost = costDifference;
                    result.BestNode = unvisitedNode;
                }
            }
            return result;
        }
    }

    internal class Result
    {
        public int BestNode { get; set; }

        public int Cost { get; set; }

        public int FollowingNode { get; set; }

        public Result()
        {
            BestNode = -1;
            Cost = -1;
            FollowingNode = -1;
        }
    }
}