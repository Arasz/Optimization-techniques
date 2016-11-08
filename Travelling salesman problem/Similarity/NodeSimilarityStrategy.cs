using System.Linq;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    public class NodeSimilarityStrategy : ISimilarityCalculationStrategy
    {
        public double CalculateSimilarity(Path first, Path second)
        {
            return first.Nodes
                .Count(node => second.Nodes.Contains(node));
        }
    }
}