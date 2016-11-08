using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
	public struct Edge
	{
		public int SourceNode { get; }

		public int TargetNode { get; }

		public int Weight { get; }

		public Edge(int sourceNode, int targetNode, int weight)
		{
			SourceNode = sourceNode;
			TargetNode = targetNode;
			Weight = weight;
		}

		public override int GetHashCode() => (SourceNode.GetHashCode() * 17 + TargetNode.GetHashCode()) * 17 + Weight.GetHashCode();
	}

	public class EdgeEqualityComparer : IEqualityComparer<Edge>
	{
		public bool Equals(Edge x, Edge y)
			=> x.SourceNode == y.SourceNode && x.TargetNode == y.TargetNode && x.Weight == y.Weight;

		//|| x.SourceNode == y.TargetNode && x.TargetNode == y.SourceNode && x.Weight == y.Weight;

		public int GetHashCode(Edge obj) => obj.GetHashCode();
	}
}