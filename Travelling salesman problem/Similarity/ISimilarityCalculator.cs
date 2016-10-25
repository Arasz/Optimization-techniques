using ConsoleApplication.Solver.SolverVisitor;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculator
    {
        /// <summary>
        /// Calculates similarity beetwen paths in path accumulator
        /// </summary>
        /// <returns>Similarity matrix</returns>
        double[,] CalculateSimilarityMatrix(IPathAccumulator pathAccumulator);
    }
}