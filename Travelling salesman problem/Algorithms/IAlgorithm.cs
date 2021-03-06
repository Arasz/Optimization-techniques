using ConsoleApplication.Graphs;

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
        /// <param name="completeGraph"> Algorithm input data </param>
        /// <param name="precalculatedPath"> Outuput from other TSP solving algorithm </param>
        /// <returns> Path with smallest cost found by algorithm </returns>
        Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null);
    }
}