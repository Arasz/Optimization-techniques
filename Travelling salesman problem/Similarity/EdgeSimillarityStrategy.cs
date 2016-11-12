using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Similarity
{
	public class EdgeSimillarityStrategy : ISimilarityCalculationStrategy
	{
		public double CalculateSimilarity(Path first, Path second)
		{
			var equalityComparrer = new EdgeEqualityComparer();
			var firstEdges = new HashSet<Edge>(equalityComparrer);
			var secondEdges = new HashSet<Edge>(equalityComparrer);

			// we assum that paths have the same length
			for (var n = 0; n < first.Count - 1; n++)
			{
				firstEdges.Add(new Edge(first.Nodes[n], first.Nodes[n + 1], 0));
				secondEdges.Add(new Edge(second.Nodes[n], second.Nodes[n + 1], 0));
			}
			return firstEdges.Count(edge => secondEdges.Contains(edge, equalityComparrer));
		}
	}
}