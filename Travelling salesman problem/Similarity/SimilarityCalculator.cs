using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Similarity
{
    public  class SimilarityCalculator : ISimilarityCalculator
    {
        public double[,] CalculateSimilarityMatrix(ISimilarityCalculationStrategy calculationStrategy, ISolverResult solverResult)
        {
            var count = solverResult.Paths.Count;
            var matrix = new double[count, count];
            for (var i = 0; i < count; i++)
                for (var j = 0; j < count; j++)
                    matrix[i, j] = calculationStrategy.CalculateSimilarity(solverResult.Paths[i], solverResult.Paths[j]);

            return matrix;
        }
    }
}