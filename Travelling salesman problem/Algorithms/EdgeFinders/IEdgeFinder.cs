using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms
{
    public interface IEdgeFinder{
        Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes);
    }
}