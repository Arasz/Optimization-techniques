using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class Fragment
	{
		public Edge Edge { get; set; }

		public int Node { get; set; } = -1;

		public Fragment(Edge edge, int node)
		{
			Edge = edge;
			Node = node;
		}
	}
}