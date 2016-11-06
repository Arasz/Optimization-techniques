using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Solver
{
    public class PathSimilaritySolver : SolverBase
    {
        private readonly IInitializationSolver _initializationSolver;
        private readonly IEnumerable<ISimilarityCalculationStrategy> _calculatedSimilarities;
        private readonly int _generatedPaths;
        private readonly Random _randomGenerator;
        private readonly ISimilarityCalculator _similarityCalculator;


        public Dictionary<string, double[,]> SimilarityMatrices { get; } = new Dictionary<string, double[,]>();

        public PathSimilaritySolver(IGraph completeGraph, IInitializationSolver initializationSolver,
            IEnumerable<ISimilarityCalculationStrategy> calculatedSimilarities, int generatedPaths = 1000) : base(completeGraph)
        {
            _initializationSolver = initializationSolver;
            _calculatedSimilarities = calculatedSimilarities;
            _generatedPaths = generatedPaths;
            _randomGenerator = new Random();
            _similarityCalculator = new SimilarityCalculator();
        }

        public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
        {
            var bestPath = InitialBestPath;
            ISolverResult solverResult = new NullSolverResult();

            for (var j = 0; j < _generatedPaths; j++)
            {
                solverResult = _initializationSolver.Solve(StartNode);

                var localPath = solverResult.Paths[j];

                localPath = tspSolvingAlgorithm.Solve(localPath.Nodes.First(), CompleteGraph, localPath);

                if (localPath.Cost < bestPath.Cost)
                    bestPath = localPath;
            }

            CalculateSimilarities(solverResult);

            return solverResult;
        }

        private static Path InitialBestPath => new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue) );

        private void CalculateSimilarities(ISolverResult solverResult)
        {
            foreach (var strategy in _calculatedSimilarities)
            {
                SimilarityMatrices[strategy.GetType().Name] = _similarityCalculator
                    .CalculateSimilarityMatrix(strategy, solverResult);
            }
        }

        private int StartNode => _randomGenerator.Next(0, CompleteGraph.NodesCount - 1);
    }
}