using ConsoleApplication.Common;
using ConsoleApplication.Data;
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
        /// <param name="startPoint"> Algorithm start point </param>
        /// <param name="data"> Algorithm input data </param>
        /// <param name="path"> Path found by algorithm </param>
        /// <returns> Total path length </returns>
        int Solve(Point startPoint, IData data, out IEnumerable<Point> path);
    }
}