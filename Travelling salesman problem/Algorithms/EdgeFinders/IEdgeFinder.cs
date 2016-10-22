using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System;

namespace ConsoleApplication.Algorithms
{
    public interface IEdgeFinder{
        Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes);
        Edge RandomNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes, Random randomGenerator);
    }
}