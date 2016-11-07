
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Similarity
{
    public class WithBestSimilarityCalculator : ISimilarityCalculator
    {
        private readonly Path _bestPath;

        public WithBestSimilarityCalculator(Path bestPath)
        {
            _bestPath = bestPath;
        }

        public IEnumerable<double> CalculatePathsSimilarities(ISimilarityCalculationStrategy calculationStrategy, ISolverResult solverResult)
        {
            return solverResult.Paths
                .Select(path => (double)calculationStrategy.CalculateSimilarity(path, _bestPath))
                .ToList();
        }
    }
}