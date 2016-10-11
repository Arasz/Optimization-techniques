using System.Collections.Generic;

namespace TSP.Data.Graph
{
	public class Graph
	{
		private IList<Node> Nodes { get; }

		public Graph(int initialSizie = 10)
		{
			Nodes = new List<Node>(initialSizie);
		}

		public void AddNode(Node node) => Nodes.Add(node);

		public void RemoveNode(Node node) => Nodes.Remove(node);
	}
}