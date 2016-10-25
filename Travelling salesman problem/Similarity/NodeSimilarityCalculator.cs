using System.Linq;
using ConsoleApplication.Solver.SolverVisitor;

namespace ConsoleApplication.Similarity
{
    public class NodeSimilarityCalculator : SimilarityCalculatorBase
    {
        public override double[,] CalculateSimilarityMatrix(IPathAccumulator pathAccumulator)
        {
            var count = pathAccumulator.Paths.Count;
            var matrix = new double[count, count];
            for (var i = 0; i < count; i++)
                for (var j = 0; j < count; j++)
                    matrix[i, j] = CalculateSimilarity(pathAccumulator.Paths[i], pathAccumulator.Paths[j]);

            return matrix;
        }

        protected override double CalculateSimilarity(Path first, Path second)
        {
            return first.NodesList
                .Zip(second.NodesList, (fromFirst, fromSecond) => fromFirst == fromSecond)
                .Count(wereEqual => wereEqual);
        }
    }
}