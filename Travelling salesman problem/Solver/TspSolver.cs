using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspSolver : ISolver
	{
		private readonly IGraph _completeGraph;

		public IList<int> BestPath { get; private set; }

		public int BestResult { get; private set; }

		public TimeSpan MaxSolvingTime { get; }

		public int MeanReasult { get; private set; }

		public TimeSpan MeanSolvingTime { get; }

		public TimeSpan MinSolvingTime { get; }

		public IList<int> Results { get; } = new List<int>();

		public int WorstResult { get; private set; }

		public TspSolver(IGraph completeGraph)
		{
			_completeGraph = completeGraph;
		}

		public void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			BestResult = int.MaxValue;
			WorstResult = int.MinValue;

			for (var startNode = 0; startNode < _completeGraph.NodesCount; startNode++)
			{
				IList<int> path = new List<int>();
				//TODO: pass steps in ctr
				var localResult = tspSolvingAlgorithm.Solve(startNode, _completeGraph, path);
				UpdateResults(localResult, path);
			}
		}

		private void UpdateResults(int localResult, IList<int> path)
		{
			if (localResult < BestResult)
			{
				BestResult = localResult;
				BestPath = path;
			}

			if (localResult > WorstResult)
				WorstResult = localResult;

			Results.Add(localResult);
			MeanReasult = (int)Math.Round(Results.Average());
		}
	}
}