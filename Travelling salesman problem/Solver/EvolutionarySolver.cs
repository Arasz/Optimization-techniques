using System;
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
		private readonly IRecombinator _recombinator;
		private readonly ISelector _selector;
	    private readonly int _solvingTime;
	    private readonly Stopwatch _stopwatch;
	    private readonly Random _random;

		private int StartNode(Path path) => path.Nodes[_random.Next(0,path.Nodes.Count)];

		public EvolutionarySolver(IGraph completeGraph, IRecombinator recombinator, ISelector selector, int solvingTime = 1000)
		    : base(completeGraph)
		{
			_recombinator = recombinator;
			_selector = selector;
		    _solvingTime = solvingTime;
		    _stopwatch = new Stopwatch();
		    _random = new Random();
		}

		public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
		{
			Statistics = new SolverStatistics();

			ISet<Path> initialPopulation = new HashSet<Path>(solverResult.Paths);

			_stopwatch.Start();

			while (_stopwatch.ElapsedMilliseconds < _solvingTime)
			{
				var parents = _selector.Select(initialPopulation);
				var child = _recombinator.Recombine(parents.Item1, parents.Item2);
				var optimalChild = tspSolvingAlgorithm.Solve(StartNode(child), CompleteGraph, child);
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