using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspLocalSearchSolver : ISolver
	{
		private readonly IGraph _completeGraph;
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;
		private SolvingTimeContext _solvingTimeContext;
		public IList<int> BestPath { get; private set; }

		public int BestResult { get; private set; }

		public TimeSpan MaxSolvingTime { get; private set; }

		public int MeanReasult { get; private set; }

		public TimeSpan MeanSolvingTime { get; private set; }

		public TimeSpan MinSolvingTime { get; private set; }

		public int WorstResult { get; private set; }

		public TspLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm)
		{
			_completeGraph = completeGraph;
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
			_solvingTimeContext = new SolvingTimeContext(new Stopwatch());
		}

		public void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			_initializationSolver.Solve(_initializationAlgorithm);

			var startNode = _initializationSolver.BestPath.First();

			BestPath = _initializationSolver.BestPath;

			using (_solvingTimeContext)
				BestResult = tspSolvingAlgorithm.Solve(startNode, _completeGraph, BestPath);

			MinSolvingTime = _solvingTimeContext.Elapsed;
		}

		private class SolvingTimeContext : IDisposable
		{
			private readonly Stopwatch _stopwatch;

			public TimeSpan Elapsed => _stopwatch.Elapsed;

			public SolvingTimeContext(Stopwatch stopwatch)
			{
				_stopwatch = stopwatch;
				_stopwatch.Start();
			}

			public void Dispose()
			{
				_stopwatch.Stop();
			}
		}
	}
}