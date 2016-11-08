using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Similarity
{
    public  class ForEachSimilarityCalculator : ISimilarityCalculator
    {
        public IEnumerable<double> CalculatePathsSimilarities(ISimilarityCalculationStrategy calculationStrategy, ISolverResult solverResult)
        {
            var count = solverResult.Paths.Count;
            var result = new List<double>(Enumerable.Range(0, count).Select(value => value*0d));

            for (var i = 0; i < count; i++)
             {
                 for (var j = 0; j < count; j++)
                     result[i] += (i != j) ? calculationStrategy.CalculateSimilarity(solverResult.Paths[i], solverResult.Paths[j]): 0;

                 result[i] /= (count - 1);
             }
            return result;
        }
    }
}