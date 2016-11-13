using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Similarity
{
    public class EdgeSimillarityStrategy : ISimilarityCalculationStrategy
    {
        public double CalculateSimilarity(Path first, Path second)
        {
            var firstEdges = TransformPathToEdgeCollecetion(first);
            var secondEdges = TransformPathToEdgeCollecetion(second);

            return firstEdges.Count(edge => secondEdges.Contains(edge));
        }

        public IEnumerable<Fragment> FindSimilarFragments(Path first, Path second)
        {
            var firstEdges = TransformPathToEdgeCollecetion(first);
            var secondEdges = TransformPathToEdgeCollecetion(second);

            return firstEdges
                .Where(edge => secondEdges.Contains(edge))
                .Select(edge => new Fragment(edge));
        }

        private HashSet<Edge> TransformPathToEdgeCollecetion(Path path)
        {
            var edges = new HashSet<Edge>(new EdgeEqualityComparer());
            for (var n = 0; n < path.Count - 1; n++)
                edges.Add(new Edge(path.Nodes[n], path.Nodes[n + 1], 0));
            return edges;
        }
    }
}