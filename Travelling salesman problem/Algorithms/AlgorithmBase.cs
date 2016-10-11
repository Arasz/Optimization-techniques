using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms
{
	public abstract class AlgorithmBase : IAlgorithm
	{
		protected readonly IEdgeFinder _edgeFinder;
		protected readonly int _steps;

		protected AlgorithmBase(int steps, IEdgeFinder edgeFinder)
		{
			_steps = steps;
			_edgeFinder = edgeFinder;
		}

		public abstract int Solve(int startNode, IGraph completeGraph, IList<int> path);
	}
}