using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class GraspEdgeFinder : IEdgeFinder{
        protected readonly int _restrictedCandidateListSize;
        protected Random _randomGenerator;

        public GraspEdgeFinder(int restrictedCandidateListSize)
        {
            _restrictedCandidateListSize = restrictedCandidateListSize;
            _randomGenerator = new Random();
        }

        protected virtual IEnumerable<Edge> BuildRestrictedCandidateList(IEnumerable<Edge> edges) => edges
            .OrderBy(edge => edge.Weight)
            .Take(_restrictedCandidateListSize);

        public Edge NearestNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes)
        {
            var rcl = BuildRestrictedCandidateList(edges.Where(edge => unvisitedNodes.Contains(edge.TargetNode)));
            return SelectRandomEdge(rcl);
        }

        protected virtual Edge SelectRandomEdge(IEnumerable<Edge> restrictedCandidateList)
        {
            var index = _randomGenerator.Next(0, _restrictedCandidateListSize);
            return restrictedCandidateList.ElementAt(index);
        }

        public Edge RandomNodeEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes, Random randomGenerator)
        {
             var unvisitedEdges = edges.Where(edge => unvisitedNodes.Contains(edge.TargetNode));
			 return unvisitedEdges.ElementAt(randomGenerator.Next(0, unvisitedEdges.Count()));
        }
    }
}