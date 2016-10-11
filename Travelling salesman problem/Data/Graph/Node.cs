using System.Collections.Generic;
using System.Linq;

namespace TSP.Data.Graph
{
	public class Node
	{
		public Edge CheapestEdge => Edges.First();

		public SortedSet<Edge> Edges { get; set; }

		public int Id { get; }

		public Node(int id)
		{
			Id = id;
		}
	}
}