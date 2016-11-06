using System.Linq;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    public class NodeSimilarityStrategy : ISimilarityCalculationStrategy
    {
        public double CalculateSimilarity(Path first, Path second)
        {
            return first.Nodes
                .Zip(second.Nodes, (fromFirst, fromSecond) => fromFirst == fromSecond)
                .Count(wereEqual => wereEqual);
        }
    }
}