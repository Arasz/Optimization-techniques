using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    /// <summary>
    /// Path fragment. Contains edge or node. 
    /// </summary>
	public class Fragment
    {
        public Edge Edge { get; }

        public int Node { get; }

        public Fragment(Edge edge)
        {
            Edge = edge;
            Node = -1;
        }

        public Fragment(int node)
        {
            Edge = null;
            Node = node;
        }
    }
}