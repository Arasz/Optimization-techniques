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
			var currentCostIncrese = 0.0;

			while (currentCostIncrese < 0)
			{
				var bestMove = FindBestMove(path, completeGraph);
				//TODO - do the move 
				currentCostIncrese = bestMove.CostDifference;
			}

			return solution.Aggregate(0, (accu, ele) => accu += ele);
		}

		private Move FindBestMove(IList<int> path, IGraph completeGraph)
		{
			Move bestVertice = FindBestVerticeFlip(path, completeGraph);
			Move bestEdge = FindBestEdgeFlip(path);

			if(bestEdge.CostDifference > 0 && bestVertice.CostDifference > 0)
				return null;

			if(bestEdge.CostDifference < bestVertice.CostDifference)
				return bestEdge;
			return bestVertice;
		}

        private Move FindBestVerticeFlip(IList<int> path, IGraph completeGraph)
        {
            var iterator = completeGraph.Iterator;
			Move bestMove = new Move();
			for (var pointIndex = 1; pointIndex < (path.Count - 1); pointIndex++){
				int currentCost = completeGraph.Weight(pointIndex-1, pointIndex) + completeGraph.Weight(pointIndex, pointIndex+1);
				iterator.MoveTo(pointIndex);
				//TODO - dla kazdego z punktów grafu oblicz koszt i wpisz róznice (new - current) do best move
			}
			return bestMove;
        }

        private Move FindBestEdgeFlip(IList<int> path)
        {
			//TODO - implement
            throw new NotImplementedException();
        }
    }

	internal class Move
	{
		public int FirstPointIndex {get; set;}
        public int SecondPointIndex{get; set;}
        public float CostDifference{get; set;}

        public LocalSearchStrategy Strategy{get; set;}
	}

	internal enum LocalSearchStrategy  { VERTICES, EDGES};
}