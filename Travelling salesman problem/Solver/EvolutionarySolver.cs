using ConsoleApplication.Algorithms;
using ConsoleApplication.Algorithms.Evolutionary;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Result;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class EvolutionarySolver : SolverBase
	{
		private readonly IAlgorithm _optimizationAlgorithm;
		private readonly IRecombinator _recombinator;
		private readonly ISelector _selector;
		private Stopwatch _stopwatch;

		private int StartNode => 0;

		public EvolutionarySolver(IGraph completeGraph, IRecombinator recombinator, ISelector selector, IAlgorithm optimizationAlgorithm) : base(completeGraph)
		{
			_recombinator = recombinator;
			_selector = selector;
			_optimizationAlgorithm = optimizationAlgorithm;
			_stopwatch = new Stopwatch();
		}

		public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
		{
			Statistics = new SolverStatistics();

			ISet<Path> initialPopulation = new HashSet<Path>(solverResult.Paths);

			_stopwatch.Start();

			while (_stopwatch.ElapsedMilliseconds < 1000)
			{
				var parents = _selector.Select(initialPopulation);
				var child = _recombinator.Recombine(parents.Item1, parents.Item2);
				var optimalChild = _optimizationAlgorithm.Solve(StartNode, CompleteGraph, child);
				initialPopulation = EnhancePopulation(optimalChild, initialPopulation);

				//TODO: Statystyki
				//Statistics.UpdateSolvingResults(initialPopulation.OrderByDescending(path => path.Cost).First(), _stopwatch.Elapsed);
			}

			return new SolverResult(initialPopulation.ToList());
		}

		private static ISet<Path> EnhancePopulation(Path optimalChild, ISet<Path> population)
		{
			return population;
		}
	}
}