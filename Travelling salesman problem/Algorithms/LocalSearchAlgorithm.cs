using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
	public class LocalSearchAlgorithm : AlgorithmBase
	{
		public LocalSearchAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
		{
		}

		public int CalculateMoveCost(IList<int> move)
		{
			return 0;
		}

		public override int Solve(int startNode, IGraph completeGraph, IList<int> path)
		{
			var solution = path;
			var epsilon = 0.1;
			var currentIncrese = 0;

			var cost = 0;

			while (currentIncrese > epsilon)
			{
				var bestMove = FindBestMove(path, cost);

				var moveCost = CalculateMoveCost(bestMove);

				currentIncrese = cost - moveCost;

				if (moveCost > cost)
					solution = bestMove;
			}

			return solution.Aggregate(0, (accu, ele) => accu += ele);
		}

		private IList<int> FindBestMove(IList<int> path, int cost)
		{
			return path;
		}
	}
}