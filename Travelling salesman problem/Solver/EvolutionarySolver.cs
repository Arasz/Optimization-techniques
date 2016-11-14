using ConsoleApplication.Algorithms;
using ConsoleApplication.Algorithms.Evolutionary;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Results;
using ConsoleApplication.Solver.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public class EvolutionarySolver : SolverBase
    {
        private readonly Random _random;
        private readonly IRecombinator _recombinator;
        private readonly ISelector _selector;
        private readonly Stopwatch _solverStopwatch;
        private readonly int _solvingTime;

        public EvolutionarySolver(IGraph completeGraph, IRecombinator recombinator, ISelector selector, int solvingTime = 1000)
            : base(completeGraph)
        {
            _recombinator = recombinator;
            _selector = selector;
            _solvingTime = solvingTime;
            _solverStopwatch = new Stopwatch();
            _random = new Random();
        }

        public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
        {
            Statistics = new BasicSolverStatistics();

            ISet<Path> initialPopulation = new HashSet<Path>(solverResult.Paths);

            var optimalChilderens = new SolverResult();

            _solverStopwatch.Start();
            var localSearchSolveTime = SolvingTimeContext.Instance;

            while (_solverStopwatch.ElapsedMilliseconds < _solvingTime)
            {
                var parents = _selector.Select(initialPopulation);
                var child = _recombinator.Recombine(parents.Item1, parents.Item2);

                Path optimalChild;
                using (localSearchSolveTime)
                {
                    optimalChild = tspSolvingAlgorithm.Solve(StartNode(child), CompleteGraph, child);
                }

                optimalChilderens.AddPath(optimalChild);
                initialPopulation = EnhancePopulation(optimalChild, initialPopulation);

                Statistics.UpdateSolvingResults(optimalChild, localSearchSolveTime.Elapsed);
            }

            return optimalChilderens;
        }

        private static ISet<Path> EnhancePopulation(Path optimalChild, ISet<Path> population)
        {
            var worstIndividual = population.OrderByDescending(path => path.Cost).Last();

            if (worstIndividual.Cost > optimalChild.Cost)
            {
                population.Remove(worstIndividual);
                population.Add(optimalChild);
            }

            return population;
        }

        private int StartNode(Path path) => path.Nodes[_random.Next(0, path.Nodes.Count)];
    }
}