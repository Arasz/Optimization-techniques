using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Similarity
{
    public class NodeSimilarityStrategy : ISimilarityCalculationStrategy
    {
        public double CalculateSimilarity(Path first, Path second)
        {
            return first.Nodes
                .Count(node => second.Nodes.Contains(node));
        }

        public IEnumerable<Fragment> FindSimilarFragments(Path first, Path second)
        {
            return first.Nodes
                .Where(node => second.Nodes.Contains(node))
                .Select(node => new Fragment(node));
        }
    }
}