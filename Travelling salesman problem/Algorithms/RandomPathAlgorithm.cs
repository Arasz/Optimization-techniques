using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms
{
    public class RandomPathAlgorithm : AlgorithmBase
    {
        private readonly Random _randomGenerator;

        public RandomPathAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
        {
            _randomGenerator = new Random();
        }

        public override Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null)
        {
            var iterator = completeGraph.Iterator;

            var nodes = new List<int>(startNode);

            var unvisitedNodes = completeGraph.Nodes.Where(node => node != startNode).ToList();
            var steps = _steps;

            while (--steps > 0)
            {
                var edgeToNearestNode = _edgeFinder.RandomNodeEdge(iterator.Edges, unvisitedNodes, _randomGenerator);

                iterator.MoveAlongEdge(edgeToNearestNode);

                unvisitedNodes.Remove(iterator.CurrentNode);

                nodes.Add(iterator.CurrentNode);
            }
            nodes.Add(startNode);

            return new Path(nodes, new DefaultCostCalculationStrategy(completeGraph));
        }
    }
}