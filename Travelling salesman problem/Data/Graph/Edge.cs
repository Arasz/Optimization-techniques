using System;

namespace TSP.Data.Graph
{
	public class Edge
	{
		public int Cost { get; }

		private Tuple<Node, Node> ConnectedNodes { get; }

		public Edge(int cost, Tuple<Node, Node> connectedNodes)
		{
			Cost = cost;
			ConnectedNodes = connectedNodes;
		}
	}
}