using ConsoleApplication.Algorithms;
using System;
using System.Collections.Generic;

namespace ConsoleApplication.Solver
{
	public interface ISolver
	{
		IList<int> BestPath { get; }

		int BestResult { get; }

		TimeSpan MaxSolvingTime { get; }

		int MeanReasult { get; }

		TimeSpan MeanSolvingTime { get; }

		TimeSpan MinSolvingTime { get; }

		int WorstResult { get; }

		void Solve(IAlgorithm tspSolvingAlgorithm);
	}
}