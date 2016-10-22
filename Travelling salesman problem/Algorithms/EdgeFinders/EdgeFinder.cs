using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleApplication.Algorithms
{
    public class EdgeFinder : IEdgeFinder{

        public Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes) => edges
                                                  .Where(edge => unvisitedNodes.Contains(edge.TargetNode))
                                                  .OrderBy(edge => edge.Weight)
                                                  .First();

        public Edge RandomNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes, Random randomGenerator)
        {
             var unvisitedEdges = edges.Where(edge => unvisitedNodes.Contains(edge.TargetNode));
			 return unvisitedEdges.ElementAt(randomGenerator.Next(0, unvisitedEdges.Count()));
        }
        
    }
}