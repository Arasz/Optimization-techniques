using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
	/// <summary>
	/// Reprezentuje ruch wykonany w algorytmie 
	/// </summary>
	internal struct LocalSearchMove
	{
		public int CostDifference { get; set; }

		public int FirstPointIndex { get; set; }

		public int SecondPointIndex { get; set; }

		public LocalSearchStrategy Strategy { get; set; }
	}

	public class LocalSearchAlgorithm : AlgorithmBase
	{
		public LocalSearchAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
		{
		}

		public override int Solve(int startNode, IGraph completeGraph, IList<int> path)
		{
			var solution = path;
			var currentCostIncrese = -1;

			while (currentCostIncrese < 0)
			{
				var bestMove = FindBestMove(path, completeGraph);
				if (bestMove.Strategy == LocalSearchStrategy.Vertices)
				{
					//flip VERTICES
					// FIRST - node you want to exclude from path
					// SECOND - node you want to add to path
					var nodetoExcludeIndex = bestMove.FirstPointIndex;
					path[nodetoExcludeIndex] = bestMove.SecondPointIndex;
				}
				else
				{
					//flip EDGES
					var output = new List<int>(path);
					var length = bestMove.SecondPointIndex - bestMove.FirstPointIndex;
					var path1 = output.GetRange(0, bestMove.FirstPointIndex);
					var path2 = output.GetRange(bestMove.FirstPointIndex + 1, length);
					var path3 = output.GetRange(bestMove.SecondPointIndex + 1, output.Count - length);
					path2.Reverse();
					path.Clear();
					output.AddRange(path1);
					output.AddRange(path2);
					output.AddRange(path3);
					path = output;
				}
				currentCostIncrese = bestMove.CostDifference;
			}
			return solution.Aggregate(0, (accu, ele) => accu += ele);
		}

		private LocalSearchMove FindBestEdgeFlip(IList<int> path, IGraph completeGraph)
		{
			LocalSearchMove bestLocalSearchMove = new LocalSearchMove();
			bestLocalSearchMove.Strategy = LocalSearchStrategy.Edges;

			for (int i = 0; i < path.Count - 1; i++)
			{
				for (int j = i; j < path.Count - 1; j++)
				{
					int currentCost = completeGraph.Weight(path[i], path[i + 1]) + completeGraph.Weight(path[j], path[j + 1]);
					int newCost = completeGraph.Weight(path[i], path[j]) + completeGraph.Weight(path[i + 1], path[j + 1]);
					int newCostDifference = newCost - currentCost;
					if (newCost < currentCost && bestLocalSearchMove.CostDifference < newCostDifference)
					{
						bestLocalSearchMove.CostDifference = newCostDifference;
						bestLocalSearchMove.FirstPointIndex = i;
						bestLocalSearchMove.SecondPointIndex = j;
					}
				}
			}

			return bestLocalSearchMove;
		}

		private LocalSearchMove FindBestMove(IList<int> path, IGraph completeGraph)
		{
			LocalSearchMove bestVertice = FindBestVerticeFlip(path, completeGraph);
			LocalSearchMove bestEdge = FindBestEdgeFlip(path, completeGraph);

			if (bestEdge.CostDifference > 0 && bestVertice.CostDifference > 0)
				return null;

			if (bestEdge.CostDifference < bestVertice.CostDifference)
				return bestEdge;
			return bestVertice;
		}

		private LocalSearchMove FindBestVerticeFlip(IList<int> path, IGraph completeGraph)
		{
			LocalSearchMove bestLocalSearchMove = new LocalSearchMove();
			bestLocalSearchMove.Strategy = LocalSearchStrategy.Vertices;
			for (var pointIndex = 1; pointIndex < (path.Count - 1); pointIndex++)
			{
				int indexBefore = path[pointIndex - 1];
				int index = path[pointIndex];
				int indexAfter = path[pointIndex + 1];
				int currentCost = completeGraph.Weight(indexBefore, index) + completeGraph.Weight(index, indexAfter);
				var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Contains(node)).ToList();
				for (int i = 0; i < unvisitedNodes.Count; i++)
				{
					int newNode = unvisitedNodes[i];
					int newCost = completeGraph.Weight(indexBefore, newNode) + completeGraph.Weight(newNode, indexAfter);
					int newCostDifference = newCost - currentCost;
					if (newCost < currentCost && bestLocalSearchMove.CostDifference < newCostDifference)
					{
						bestLocalSearchMove.CostDifference = newCostDifference;
						bestLocalSearchMove.FirstPointIndex = pointIndex; // node you want to exclude from path
						bestLocalSearchMove.SecondPointIndex = unvisitedNodes[i]; // node you want to add to path
					}
				}
			}
			//TODO - include path indexees groups: last, fist, second and one before last, last, first
			return bestLocalSearchMove;
		}
	}

	internal enum LocalSearchStrategy
	{
		Vertices,
		Edges
	};
}