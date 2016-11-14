using ConsoleApplication.Solver.Results;
using System.Collections.Generic;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculator
    {
        /// <summary>
        /// Calculates similarity between paths in path accumulator 
        /// </summary>
        /// <returns> List of paths similarity values </returns>
        IEnumerable<double> CalculatePathsSimilarities(ISimilarityCalculationStrategy calculationStrategy, ISolverResult solverResult);
    }
}