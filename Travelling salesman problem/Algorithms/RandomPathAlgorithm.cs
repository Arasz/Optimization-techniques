using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleApplication.Algorithms
{
	public class RandomPathAlgorithm : AlgorithmBase
	{
        private Random _randomGenerator;
		public RandomPathAlgorithm (int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
		{
            _randomGenerator = new Random();
		}

		public override int Solve(int startNode, IGraph completeGraph, List<int> path)
		{
            
        	var iterator = completeGraph.Iterator;
			int size = completeGraph.NodesCount;

			var pathCost = 0;

			path.Add(startNode);
			var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
			var steps = _steps;

			while (--steps > 0)
			{
				var edgeToNearestNode = getRandomEdge(iterator.Edges, unvisitedNodes);

				pathCost += edgeToNearestNode.Weight;

				iterator.MoveAlongEdge(edgeToNearestNode);

				unvisitedNodes.Remove(iterator.CurrentNode);

				path.Add(iterator.CurrentNode);
			}

			pathCost += iterator.EdgeWeight(startNode);
			path.Add(startNode);

			return pathCost;
		}

	 	public Edge getRandomEdge(IEnumerable<Edge> edges, IEnumerable<int> unvisitedNodes){
			 var unvisitedEdges = edges.Where(edge => unvisitedNodes.Contains(edge.TargetNode));
			 int index = _randomGenerator.Next(0, unvisitedEdges.Count());
			 return unvisitedEdges.ElementAt(index);
		}
                                
	}
}