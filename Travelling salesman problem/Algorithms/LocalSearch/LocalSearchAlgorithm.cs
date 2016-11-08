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

        public override Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null)
        {
            var optimizedPath = precalculatedPath;
            var bestMove = FindBestMove(precalculatedPath, completeGraph);

            while (bestMove != null)
            {
                optimizedPath = bestMove.Move(optimizedPath);
                bestMove = FindBestMove(optimizedPath, completeGraph);
            }

            return optimizedPath;
        }

        private static IMoveStrategy FindBestEdgeMove(Path path, IGraph completeGraph)
        {
            var bestLocalSearchMove = new EdgeMoveStrategy();

            for (var i = 0; i < path.Count - 3; i++)
            {
                for (var j = i + 2; j < path.Count - 1; j++)
                {
                    var lineCost = completeGraph.Weight(path.Nodes[i], path.Nodes[i + 1])
                                   +
                                   completeGraph.Weight(path.Nodes[j], path.Nodes[j + 1]);

                    var crossCost = completeGraph.Weight(path.Nodes[i], path.Nodes[j])
                                    +
                                    completeGraph.Weight(path.Nodes[i + 1], path.Nodes[j + 1]);

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

        private static IMoveStrategy FindBestMove(Path path, IGraph completeGraph)
        {
            var bestVertice = FindBestNodeMove(path, completeGraph);
            var bestEdge = FindBestEdgeMove(path, completeGraph);

            if (bestEdge.CostImprovement >= 0 && bestVertice.CostImprovement >= 0)
                return null;

            return bestEdge.CostImprovement < bestVertice.CostImprovement ? bestEdge : bestVertice;
        }

        private static IMoveStrategy FindBestNodeMove(Path path, IGraph completeGraph)
        {
            var bestLocalSearchMove = new NodeMoveStrategy();
            for (var pathIndex = 0; pathIndex < path.Count - 2; pathIndex++)
            {
                var previousNode = path.Nodes[pathIndex];
                var currentNode = path.Nodes[pathIndex + 1];
                var nextNode = path.Nodes[pathIndex + 2];

                var currentCost = completeGraph.Weight(previousNode, currentNode) + completeGraph.Weight(currentNode, nextNode);

                var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Nodes.Contains(node)).ToList();

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