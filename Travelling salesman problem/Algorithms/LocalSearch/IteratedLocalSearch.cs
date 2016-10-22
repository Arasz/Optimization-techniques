using ConsoleApplication.Graphs;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    public class IteratedLocalSearchAlgorithm : LocalSearchAlgorithm
    {
        private Random _randomGenerator;

        private int PerturbanceLength = 2;
        public IteratedLocalSearchAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
        {
            _randomGenerator = new Random();
        }

        public override int Solve(int startNode, IGraph completeGraph, List<int> path)
        {
            
            var perturbance = generatePerturbance(path, completeGraph);
            foreach(IMove move in perturbance){
                move.Move(path);
            }
            var bestMove = FindBestMove(path, completeGraph);
            bestMove.Move(path);

            return CalculateCost(path, completeGraph);

        }

        private List<IMove> generatePerturbance(List<int> path, IGraph completeGraph)
        {
            List<IMove> perturbance = new List<IMove>();
            for(int i=0; i<PerturbanceLength; i++){
                perturbance.Add(getRandomMove(path, completeGraph));
            }
            return perturbance;
        }

        private IMove getRandomMove(List<int> path, IGraph completeGraph)
        {
            int moveType = _randomGenerator.Next(0, 1);
            if(moveType == 1){
                return getRandomEdgeMove(path, completeGraph);
            }
            return getRandomNodeMove(path, completeGraph);
        }

        private IMove getRandomNodeMove(List<int> path, IGraph completeGraph)
        {
            NodeMove nodeMove = new NodeMove();
            var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Contains(node)).ToList();
            var excludedNodeIndex = _randomGenerator.Next(1, path.Count);
            var NodeAfterMove = _randomGenerator.Next(1, unvisitedNodes.Count);
            nodeMove.ExcludedNodePathIndex = path[excludedNodeIndex];
            nodeMove.NodeAfterMove = unvisitedNodes[NodeAfterMove];
            
            var currentCost = completeGraph.Weight(path[excludedNodeIndex - 1], nodeMove.ExcludedNodePathIndex) + completeGraph.Weight(nodeMove.ExcludedNodePathIndex, path[excludedNodeIndex + 1]);
            var costFromUnvisited = completeGraph.Weight(path[excludedNodeIndex - 1], nodeMove.NodeAfterMove) + completeGraph.Weight(nodeMove.NodeAfterMove, path[excludedNodeIndex + 1]);
            
            nodeMove.CostImprovement = costFromUnvisited - currentCost;

            return nodeMove;
            
        }

        private IMove getRandomEdgeMove(List<int> path, IGraph completeGraph)
        {
            var newMove = new EdgeMove();

            var i = _randomGenerator.Next(0, path.Count - 3);
            var j = _randomGenerator.Next(i+2, path.Count - 1);

            var lineCost = completeGraph.Weight(path[i], path[i + 1]) + completeGraph.Weight(path[j], path[j + 1]);

            var crossCost = completeGraph.Weight(path[i], path[j]) + completeGraph.Weight(path[i + 1], path[j + 1]);

            var costDifference = crossCost - lineCost;

            newMove.CostImprovement = costDifference;
            newMove.FirstNodePathIndex = i;
            newMove.SecondNodePathIndex = j;

            return newMove;
        }
    }
}