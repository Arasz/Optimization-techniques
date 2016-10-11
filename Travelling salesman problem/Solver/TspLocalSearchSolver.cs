using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspLocalSearchSolver : ISolver
	{
		private readonly IGraph _completeGraph;
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;
		public IEnumerable<int> BestPath { get; }

		public int BestResult { get; }

		public int MeanReasult { get; }

		public int WorstResult { get; }

		public TspLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm)
		{
			_completeGraph = completeGraph;
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
		}

		public void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			_initializationSolver.Solve(_initializationAlgorithm);

			var startNode = _initializationSolver.BestPath.First();

			tspSolvingAlgorithm.Solve(startNode, _completeGraph, _initializationSolver.BestPath.ToList());
		}
	}
}