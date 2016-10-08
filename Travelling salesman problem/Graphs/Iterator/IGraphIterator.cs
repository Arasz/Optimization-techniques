using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
    public interface IGraphIterator
    {
        /// <summary>
        /// </summary>
        int CurrentNode { get; }

        /// <summary>
        /// All edges from current node 
        /// </summary>
        IList<Edge> Edges { get; }

        /// <summary>
        /// Weight of edge from current node to target node 
        /// </summary>
        int EdgeWeight(int targetNode);

        /// <summary>
        /// Moves iterator along given edge 
        /// </summary>
        void MoveAlongEdge(Edge edge);

        /// <summary>
        /// Moves iterator to given node 
        /// </summary>
        void MoveTo(int node);
    }
}