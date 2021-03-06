﻿using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Results;
using System.Collections.Generic;
using System.Linq;

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
                .Select(path => calculationStrategy.CalculateSimilarity(_bestPath, path))
                .ToList();
        }
    }
}