using ConsoleApplication.Algorithms;
using ConsoleApplication.Solver.SolverVisitor;
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

		void Solve(IAlgorithm tspSolvingAlgorithm, int startNode);

		void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator);

		void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode);
	}
}