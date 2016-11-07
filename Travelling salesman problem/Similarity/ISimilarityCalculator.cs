using System.Collections.Generic;
using ConsoleApplication.Solver.SolverResult;

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