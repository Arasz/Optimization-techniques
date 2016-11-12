using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
	public class GreedyCycleAlgorithm : AlgorithmBase
	{
		public GreedyCycleAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
		{
		}

		private static Result FindBestResult(int firstNode, int secondNode, IEnumerable<int> unvisitedNodes, IGraph graph)
		{
			var result = new Result();

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

	    public override Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null)
	    {
	        var iterator = completeGraph.Iterator;
	        var pathNodes = new List<int>();


	        iterator.MoveTo(startNode);

	        pathNodes.Add(startNode);
	        var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
	        var steps = _steps;

	        //find nearest to start point
	        var nearestNodeEdge = _edgeFinder.NearestNodeEdge(iterator.Edges, unvisitedNodes);

	        iterator.MoveAlongEdge(nearestNodeEdge);
	        unvisitedNodes.Remove(iterator.CurrentNode);
	        pathNodes.Add(iterator.CurrentNode);
	        iterator.MoveTo(pathNodes.Last());
	        pathNodes.Add(startNode);

	        steps--;

	        while (--steps > 0)
	        {
	            var bestResult = new Result();
	            for (var node = 0; node < (pathNodes.Count - 2); node++)
	            {
	                var localBestResult = FindBestResult(pathNodes[node], pathNodes[node + 1], unvisitedNodes, completeGraph);

	                if (bestResult.Cost < 0 || localBestResult.Cost < bestResult.Cost)
	                {
	                    bestResult = localBestResult;
	                    bestResult.FollowingNode = node + 1;
	                }
	            }

	            pathNodes.Insert(bestResult.FollowingNode, bestResult.BestNode);

	            unvisitedNodes.Remove(bestResult.BestNode);
	        }
	        return new Path(pathNodes, new DefaultCostCalculationStrategy(completeGraph));

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