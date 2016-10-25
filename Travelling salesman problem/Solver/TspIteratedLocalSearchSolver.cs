using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleApplication.Algorithms.LocalSearch;

namespace ConsoleApplication.Solver
{
	public class TspIteratedLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

		private const long AlgorithmSolveTimeMs = 4100;

        private const int PerturbanceLength = 2;

        private int ILSRepeatAmount = 10;

        private Random _randomGenerator;

		public TspIteratedLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm) : base(completeGraph)
		{
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
            _randomGenerator = new Random();
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			var context = SolvingTimeContext.Instance;

			var BestResult = int.MaxValue;
			var bestPath = new List<int>();
			for(var i=0; i<ILSRepeatAmount; i++)
			{
				using(context)
				{
					var pathAccumulator = new PathAccumulator();
					var startNode = _randomGenerator.Next(0, _completeGraph.NodesCount-1);
                	_initializationSolver.SolveOnce(_initializationAlgorithm, pathAccumulator, startNode);
					var accumulatedPath = pathAccumulator.Paths[0];
				    bestPath = accumulatedPath.NodesList;
					
					var timer = new Stopwatch();
            		timer.Start();
            		var cost = CalculateCost(bestPath, _completeGraph);
            		while(timer.ElapsedMilliseconds < AlgorithmSolveTimeMs)
					{
						var newPath = new List<int>(bestPath);
            			for(var j=0; j<PerturbanceLength; j++)
                		{
                    		var move = GetRandomMove(newPath, _completeGraph);
                    		move.Move(newPath);
                		}
						var newCost = tspSolvingAlgorithm.Solve(bestPath.First(), _completeGraph, bestPath);
                		if (newCost >= cost) continue;
                
                		bestPath = newPath;
                		cost = newCost;
					}
				    BestResult = cost;
				}
				UpdatePathResults(BestResult, bestPath);
				UpdateTimeMeasures(context.Elapsed);
				Console.WriteLine(i + " / 10");
			}
		}


		protected int CalculateCost(List<int> path, IGraph completeGraph)
        {
            var cost = 0;
            for (var i = 0; i < path.Count - 1; i++)
            {
                cost += completeGraph.Weight(path[i], path[i+1]);
            }
            return cost;
        }

		public override void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
		{
			throw new NotImplementedException();
		}

        public override void SolveOnce(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode)
        {
            throw new NotImplementedException();
        }

		private IMove GetRandomMove(List<int> path, IGraph completeGraph)
        {
            var moveType = _randomGenerator.Next(0, 1);
            return moveType == 1 ?
                GetRandomEdgeMove(path, completeGraph) :
                GetRandomNodeMove(path, completeGraph);
        }

        private IMove GetRandomNodeMove(List<int> path, IGraph completeGraph)
        {
            NodeMove nodeMove = new NodeMove();
            var unvisitedNodes = completeGraph.Nodes.Where(node => !path.Contains(node)).ToList();
            var excludedNodeIndex = _randomGenerator.Next(1, path.Count-1);
            var nodeAfterMove = _randomGenerator.Next(1, unvisitedNodes.Count-1);
            nodeMove.ExcludedNodePathIndex = excludedNodeIndex;
            nodeMove.NodeAfterMove = unvisitedNodes[nodeAfterMove];
            
            var currentCost = completeGraph.Weight(path[excludedNodeIndex - 1], path[excludedNodeIndex]) + completeGraph.Weight(path[excludedNodeIndex], path[excludedNodeIndex + 1]);
            var costFromUnvisited = completeGraph.Weight(path[excludedNodeIndex - 1], nodeMove.NodeAfterMove) + completeGraph.Weight(nodeMove.NodeAfterMove, path[excludedNodeIndex + 1]);
            
            nodeMove.CostImprovement = costFromUnvisited - currentCost;

            return nodeMove;
            
        }

        private IMove GetRandomEdgeMove(List<int> path, IGraph completeGraph)
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