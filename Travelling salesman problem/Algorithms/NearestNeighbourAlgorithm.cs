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

		public override int Solve(int startNode, IGraph completeGraph, List<int> path)
		{
			var iterator = completeGraph.Iterator;
			iterator.MoveTo(startNode);

			var pathCost = 0;

			path.Add(startNode);
			var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
			var steps = _steps;

			while (--steps > 0)
			{
				var edgeToNearestNode = _edgeFinder.NearestNodeEdge(iterator.Edges, unvisitedNodes);

				pathCost += edgeToNearestNode.Weight;

				iterator.MoveAlongEdge(edgeToNearestNode);

				unvisitedNodes.Remove(iterator.CurrentNode);

				path.Add(iterator.CurrentNode);
			}

			pathCost += iterator.EdgeWeight(startNode);
			path.Add(startNode);

			return pathCost;
		}
	}
}