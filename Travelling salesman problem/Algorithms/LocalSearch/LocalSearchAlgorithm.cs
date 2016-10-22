using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    public class LocalSearchAlgorithm : AlgorithmBase
    {
        public LocalSearchAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
        {
        }

        public override int Solve(int startNode, IGraph completeGraph, List<int> path)
        {
            while (FindBestMove(path, completeGraph)?.Move(path) ?? false) ;
            return CalculateCost(path, completeGraph);

        }

        protected int CalculateCost(List<int> path, IGraph completeGraph)
        {
            var cost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                cost += completeGraph.Weight(path[i], path[i+1]);
            }
            return cost;
        }

        protected IMove FindBestEdgeMove(IList<int> path, IGraph completeGraph)
        {
            var bestLocalSearchMove = new EdgeMove();

            for (var i = 0; i < path.Count - 3; i++)
            {
                for (var j = i + 2; j < path.Count - 1; j++)
                {
                    var lineCost = completeGraph.Weight(path[i], path[i + 1]) + completeGraph.Weight(path[j], path[j + 1]);

                    var crossCost = completeGraph.Weight(path[i], path[j]) + completeGraph.Weight(path[i + 1], path[j + 1]);

                    var costDifference = crossCost - lineCost;

                    if (crossCost < lineCost && costDifference < bestLocalSearchMove.CostImprovement)
                    {
                        bestLocalSearchMove.CostImprovement = costDifference;
                        bestLocalSearchMove.FirstNodePathIndex = i;
                        bestLocalSearchMove.SecondNodePathIndex = j;
                    }
                }
            }

            return bestLocalSearchMove;
        }

        protected IMove FindBestMove(IList<int> path, IGraph completeGraph)
        {
            var bestVertice = FindBestNodeMove(path, completeGraph);
            var bestEdge = FindBestEdgeMove(path, completeGraph);

            if (bestEdge.CostImprovement >= 0 && bestVertice.CostImprovement >= 0)
                return null;

            return bestEdge.CostImprovement < bestVertice.CostImprovement ? bestEdge : bestVertice;
        }

        protected IMove FindBestNodeMove(IList<int> path, IGraph completeGraph)
        {
            var bestLocalSearchMove = new NodeMove();
            for (var pathIndex = 0; pathIndex < path.Count - 2; pathIndex++)
            {
                var previousNode = path[pathIndex];
                var currentNode = path[pathIndex + 1];
                var nextNode = path[pathIndex + 2];

                var currentCost = completeGraph.Weight(previousNode, currentNode) + completeGraph.Weight(currentNode, nextNode);

                var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Contains(node)).ToList();

                foreach (var unvisitedNode in unvisitedNodes)
                {
                    var costFromUnvisited = completeGraph.Weight(previousNode, unvisitedNode) + completeGraph.Weight(unvisitedNode, nextNode);

                    var costDifference = costFromUnvisited - currentCost;

                    if (costFromUnvisited < currentCost && costDifference < bestLocalSearchMove.CostImprovement)
                    {
                        bestLocalSearchMove.CostImprovement = costDifference;
                        bestLocalSearchMove.ExcludedNodePathIndex = pathIndex + 1; // node you want to exclude from path
                        bestLocalSearchMove.NodeAfterMove = unvisitedNode; // node you want to add to path
                    }
                }
            }
            //TODO - include path indexees groups: last, fist, second and one before last, last, first
            return bestLocalSearchMove;
        }
    }
}