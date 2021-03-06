using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Results;
using ConsoleApplication.Solver.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public class TspMultipleStartLocalSearchSolver : SolverBase
    {
        private const int InsideAlgorithmRepeatAmount = 1000;
        private const int MslsRepeatAmount = 10;
        private readonly IAlgorithm _internalAlgorithm;
        private readonly ISolver _internalSolver;
        private readonly Random _randomGenerator;

        public TspMultipleStartLocalSearchSolver(IGraph completeGraph, ISolver internalSolver,
            IAlgorithm internalAlgorithm) : base(completeGraph)
        {
            _internalSolver = internalSolver;
            _internalAlgorithm = internalAlgorithm;
            _randomGenerator = new Random();
        }

        public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
        {
            var context = SolvingTimeContext.Instance;
            Statistics = new BasicSolverStatistics();
            var resultAccumulator = new SolverResult();

            var bestPath = new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue));

            for (var i = 0; i < MslsRepeatAmount; i++)
            {
                using (context)
                {
                    for (var j = 0; j < InsideAlgorithmRepeatAmount; j++)
                    {
                        var startNode = _randomGenerator.Next(0, CompleteGraph.NodesCount - 1);

                        var pathAccumulator = _internalSolver.Solve(_internalAlgorithm, startNode);

                        var localPath = pathAccumulator.Paths[0];

                        localPath = tspSolvingAlgorithm.Solve(localPath.Nodes.First(), CompleteGraph, localPath);

                        if (localPath.Cost < bestPath.Cost)
                            bestPath = localPath;
                    }
                    Statistics.UpdateSolvingResults(bestPath, context.Elapsed);
                    resultAccumulator.AddPath(bestPath);
                }
            }
            return resultAccumulator;
        }
    }
}