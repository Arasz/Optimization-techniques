using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
    public interface IGraph
    {
        /// <summary>
        /// Provides interface to move through graph 
        /// </summary>
        IGraphIterator Iterator { get; }

        /// <summary>
        /// All graph nodes 
        /// </summary>
        IEnumerable<int> Nodes { get; }

        int NodesCount { get; }

        /// <summary>
        /// </summary>
        /// <param name="node"> Node (array index) </param>
        /// <returns> Weights for each edge connected with given node </returns>
        int[] NodeEdgesWeights(int node);

        /// <returns> Weight of edge between source node and target node </returns>
        int Weight(int sourceNode, int targetNode);
    }
}