using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using ConsoleApplication.Solver.Result;

namespace ConsoleApplication.Solver
{
    public class PathSimilaritySolver : SolverBase
    {
        private readonly IInitializationSolver _initializationSolver;
        private readonly IEnumerable<ISimilarityCalculationStrategy> _calculatedSimilarities;
        private readonly int _generatedPaths;
        private readonly Random _randomGenerator;


        public Dictionary<string, IList<SimilaritySolverResult>> SimilairityValues{ get; } =
            new Dictionary<string ,IList<SimilaritySolverResult>>();

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

        private static Path InitialBestPath => new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue) );

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

        private int StartNode => _randomGenerator.Next(0, CompleteGraph.NodesCount - 1);
    }

    public struct SimilaritySolverResult
    {
        public SimilaritySolverResult(double similarityValue, int cost)
        {
            SimilarityValue = similarityValue;
            Cost = cost;
        }

        public double SimilarityValue { get; }

        public int Cost { get; }

        public override string ToString() => $"S: {SimilarityValue} | C: {Cost}";
    }
}