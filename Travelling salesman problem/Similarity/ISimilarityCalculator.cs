using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculator
    {
        /// <summary>
        /// Calculates similarity beetwen paths in path accumulator
        /// </summary>
        /// <returns>Similarity matrix</returns>
        double[,] CalculateSimilarityMatrix(ISimilarityCalculationStrategy calculationStrategy,
            ISolverResult solverResult);
    }
}