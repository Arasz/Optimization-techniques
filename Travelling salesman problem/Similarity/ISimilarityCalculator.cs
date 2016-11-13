using System.Collections.Generic;
using ConsoleApplication.Solver.Result;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculator
    {
        /// <summary>
        /// Calculates similarity beetwen paths in path accumulator
        /// </summary>
        /// <returns>List of paths similarity values</returns>
        IEnumerable<double> CalculatePathsSimilarities(ISimilarityCalculationStrategy calculationStrategy, ISolverResult solverResult);
    }
}