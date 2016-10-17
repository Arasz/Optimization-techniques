using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleApplication.Algorithms
{
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
				if(bestMove.Strategy == LocalSearchStrategy.VERTICES){
					//flip VERTICES
					// FIRST - node you want to exclude from path
					// SECOND - node you want to add to path
					int nodetoExcludeIndex = bestMove.FirstPointIndex;
					path[nodetoExcludeIndex] = bestMove.SecondPointIndex;
				} else
				{
					//flip EDGES
					List<int> output = new List<int>(path);
					int length = bestMove.SecondPointIndex - bestMove.FirstPointIndex;
					List<int> path1 = output.GetRange(0, bestMove.FirstPointIndex);
					List<int> path2 = output.GetRange(bestMove.FirstPointIndex+1, length);
					List<int> path3 = output.GetRange(bestMove.SecondPointIndex+1, output.Count - length);
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

		private Move FindBestMove(IList<int> path, IGraph completeGraph)
		{
			Move bestVertice = FindBestVerticeFlip(path, completeGraph);
			Move bestEdge = FindBestEdgeFlip(path, completeGraph);

			if(bestEdge.CostDifference > 0 && bestVertice.CostDifference > 0)
				return null;

			if(bestEdge.CostDifference < bestVertice.CostDifference)
				return bestEdge;
			return bestVertice;
		}

        private Move FindBestVerticeFlip(IList<int> path, IGraph completeGraph)
        {
			Move bestMove = new Move();
			bestMove.Strategy = LocalSearchStrategy.VERTICES;
			for (var pointIndex = 1; pointIndex < (path.Count - 1); pointIndex++){
				int indexBefore = path[pointIndex-1];
				int index = path[pointIndex];
				int indexAfter = path[pointIndex+1];
				int currentCost = completeGraph.Weight(indexBefore, index) + completeGraph.Weight(index, indexAfter);
				var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Contains(node)).ToList();
				for(int i = 0; i < unvisitedNodes.Count; i++){
					int newNode = unvisitedNodes[i];
					int newCost = completeGraph.Weight(indexBefore, newNode) + completeGraph.Weight(newNode, indexAfter);
					int newCostDifference = newCost - currentCost;
					if(newCost < currentCost && bestMove.CostDifference < newCostDifference){
						bestMove.CostDifference = newCostDifference;
						bestMove.FirstPointIndex = pointIndex; // node you want to exclude from path
						bestMove.SecondPointIndex = unvisitedNodes[i]; // node you want to add to path
					}
				}
			}
			//TODO - include path indexees groups: last, fist, second and one before last, last, first
			return bestMove;
        }

        private Move FindBestEdgeFlip(IList<int> path, IGraph completeGraph)
        {
			Move bestMove = new Move();
			bestMove.Strategy = LocalSearchStrategy.EDGES;
			
			for(int i=0; i<path.Count - 1; i++){
				for(int j = i; j<path.Count - 1; j++){
					int currentCost = completeGraph.Weight(path[i], path[i+1]) + completeGraph.Weight(path[j], path[j+1]);
					int newCost = completeGraph.Weight(path[i], path[j]) + completeGraph.Weight(path[i+1], path[j+1]);
					int newCostDifference = newCost - currentCost;
					if(newCost < currentCost && bestMove.CostDifference < newCostDifference){
						bestMove.CostDifference = newCostDifference;
						bestMove.FirstPointIndex = i; 
						bestMove.SecondPointIndex = j;
					}
				}
			}

			return bestMove;
        }
    }

	internal class Move
	{
		public int FirstPointIndex {get; set;}
        public int SecondPointIndex{get; set;}
        public int CostDifference{get; set;}

        public LocalSearchStrategy Strategy{get; set;}
	}

	internal enum LocalSearchStrategy  { VERTICES, EDGES};
}