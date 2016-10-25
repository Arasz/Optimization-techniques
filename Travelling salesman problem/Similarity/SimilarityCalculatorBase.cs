using System.Linq;
using ConsoleApplication.Solver.SolverVisitor;

namespace ConsoleApplication.Similarity
{
    public abstract class SimilarityCalculatorBase : ISimilarityCalculator
    {
        public abstract double[,] CalculateSimilarityMatrix(IPathAccumulator pathAccumulator);

        protected abstract double CalculateSimilarity(Path first, Path second);
    }
}