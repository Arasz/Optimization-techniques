using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms
{
    /// <summary>
    /// Implementation of algorithm which can solve TSP 
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Solves TSP 
        /// </summary>
        /// <param name="startNode"> Algorithm start point </param>
        /// <param name="graph"> Algorithm input data </param>
        /// <param name="path"> Path found by algorithm </param>
        /// <returns> Total path length </returns>
        int Solve(int startNode, Graph graph, out IList<int> path);
    }
}