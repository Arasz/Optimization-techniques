using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using ConsoleApplication.Solver.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public struct SimilaritySolverResult
    {
        public int Cost { get; }

        public double SimilarityValue { get; }

        public SimilaritySolverResult(double similarityValue, int cost)
        {
            SimilarityValue = similarityValue;
            Cost = cost;
        }

        public override string ToString() => $"S: {SimilarityValue} | C: {Cost}";
    }

    public class PathSimilaritySolver : SolverBase
    {
        private readonly IEnumerable<ISimilarityCalculationStrategy> _calculatedSimilarities;
        private readonly int _generatedPaths;
        private readonly IInitializationSolver _initializationSolver;
        private readonly Random _randomGenerator;

        public Dictionary<string, IList<SimilaritySolverResult>> SimilairityValues { get; } =
            new Dictionary<string, IList<SimilaritySolverResult>>();

        private static Path InitialBestPath => new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue));

        private int StartNode => _randomGenerator.Next(0, CompleteGraph.NodesCount - 1);

        public PathSimilaritySolver(IGraph completeGraph, IInitializationSolver initializationSolver,
                            IEnumerable<ISimilarityCalculationStrategy> calculatedSimilarities, int generatedPaths = 1000) : base(completeGraph)
        {
            _initializationSolver = initializationSolver;
            _calculatedSimilarities = calculatedSimilarities;
            _generatedPaths = generatedPaths;
            _randomGenerator = new Random();
        }

        public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
        {
            var bestPath = InitialBestPath;
            var solverResult = new SolverResult();

            for (var j = 0; j < _generatedPaths; j++)
            {
                var localSolverResult = _initializationSolver.Solve(StartNode);

                var localPath = localSolverResult.Paths[0];

                localPath = tspSolvingAlgorithm.Solve(localPath.Nodes.First(), CompleteGraph, localPath);

                solverResult.AddPath(localPath);

                if (localPath.Cost < bestPath.Cost)
                    bestPath = localPath;
            }

            CalculateSimilarities(solverResult, new ForEachSimilarityCalculator());
            CalculateSimilarities(solverResult, new WithBestSimilarityCalculator(bestPath));

            return solverResult;
        }

        private void CalculateSimilarities(ISolverResult solverResult, ISimilarityCalculator similarityCalculator)
        {
            foreach (var strategy in _calculatedSimilarities)
            {
                SimilairityValues[$"{strategy.GetType().Name}|{similarityCalculator.GetType().Name}"] =
                    similarityCalculator.CalculatePathsSimilarities(strategy, solverResult)
                    .Zip(solverResult.Paths, (similarity, path) => new SimilaritySolverResult(similarity, path.Cost))
                    .ToList();
            }
        }
    }
}