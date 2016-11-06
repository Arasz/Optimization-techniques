using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using ConsoleApplication.Solver.SolverVisitor;

namespace ConsoleApplication.Solver
{
    public class PathSimilaritySolver : SolverBase
    {
        private readonly ISolver _initializationSolver;
        private readonly IAlgorithm _initializationAlgorithm;
        private readonly int _generatedPaths;
        private readonly Random _randomGenerator;
        private readonly IEnumerable<ISimilarityCalculator> _similarityCalculators;

        public PathSimilaritySolver(IGraph completeGraph, ISolver initializationSolver, RandomPathAlgorithm initializationAlgorithm,
            int generatedPaths = 1000) : base(completeGraph)
        {
            _initializationSolver = initializationSolver;
            _initializationAlgorithm = initializationAlgorithm;
            _generatedPaths = generatedPaths;
            _randomGenerator = new Random();
            _similarityCalculators = new List<ISimilarityCalculator>
            {
                new NodeSimilarityCalculator(), new EdgeSimillarityCalculator()
            };
        }

        public override IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
        {
            var bestResult = int.MaxValue;
            var bestPath = new List<int>();
            pathAccumulator = new PathAccumulator();

            for (var j = 0; j < _generatedPaths; j++)
            {
                var startNode = _randomGenerator.Next(0, CompleteGraph.NodesCount - 1);

                _initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);


                var accumulatedPath = pathAccumulator.Paths[0];
                var localPath = pathAccumulator.Paths[j].Nodes;
                var localResult = tspSolvingAlgorithm.Solve(localPath.First(), CompleteGraph, localPath);

                if (localResult < bestResult)
                {
                    bestResult = localResult;
                    bestPath = localPath;
                }
            }

            var nodeSimilarity = _similarityCalculators.First().CalculateSimilarityMatrix(pathAccumulator);
            var edgeSimilarity = _similarityCalculators.Last().CalculateSimilarityMatrix(pathAccumulator);
        }

        public override void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode)
        {
            throw new System.NotImplementedException();
        }
    }
}