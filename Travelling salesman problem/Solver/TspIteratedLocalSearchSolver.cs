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

	    private const int IlsRepeatAmount = 10;

	    private readonly Random _randomGenerator;

		public TspIteratedLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm) : base(completeGraph)
		{
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
            _randomGenerator = new Random();
		}

		public override IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm)
		{
			var context = SolvingTimeContext.Instance;
		    Statistics = new SolverStatistics();
		    var resultPathAccumulator = new PathAccumulator();

		    for(var i=0; i<IlsRepeatAmount; i++)
			{

			    Path bestPath;
			    using(context)
				{
                	var pathAccumulator = _initializationSolver.Solve(_initializationAlgorithm, StartNode);

					bestPath = pathAccumulator.Paths[0];

					var timer = new Stopwatch();
            		timer.Start();

				    var newPath = bestPath;

				    while(timer.ElapsedMilliseconds < AlgorithmSolveTimeMs)
		            {
            			for(var j=0; j<PerturbanceLength; j++)
                		{
                    		var move = GetRandomMove(newPath, CompleteGraph);
                    		newPath = move.Move(bestPath);
                		}

						var solvedPath = tspSolvingAlgorithm.Solve(bestPath.Nodes.First(), CompleteGraph, bestPath);

                		if (solvedPath.Cost >= bestPath.Cost)
		                    continue;

                		bestPath = newPath;
					}
				}

			    resultPathAccumulator.AddPath(bestPath);
				Statistics.UpdateSolvingResults(bestPath, context.Elapsed);
				Console.WriteLine(i + " / 10");
			}
		    return resultPathAccumulator;
		}

	    private int StartNode => _randomGenerator.Next(0, CompleteGraph.NodesCount-1);

	    private IMoveStrategy GetRandomMove(Path path, IGraph completeGraph)
        {
            var moveType = _randomGenerator.Next(0, 1);
            return moveType == 1 ?
                GetRandomEdgeMove(path, completeGraph) :
                GetRandomNodeMove(path, completeGraph);
        }

        private IMoveStrategy GetRandomNodeMove(Path path, IGraph completeGraph)
        {
            var nodes = path.Nodes;

            var nodeMoveStrategy = new NodeMoveStrategy();
            var unvisitedNodes = completeGraph.Nodes.Where(node => !nodes.Contains(node)).ToList();

            var excludedNodeIndex = _randomGenerator.Next(1, path.Count-1);
            var nodeAfterMove = _randomGenerator.Next(1, unvisitedNodes.Count-1);
            nodeMoveStrategy.ExcludedNodePathIndex = excludedNodeIndex;
            nodeMoveStrategy.NodeAfterMove = unvisitedNodes[nodeAfterMove];
            
            var currentCost = completeGraph.Weight(nodes[excludedNodeIndex - 1], nodes[excludedNodeIndex]) + completeGraph.Weight(nodes[excludedNodeIndex], nodes[excludedNodeIndex + 1]);
            var costFromUnvisited = completeGraph.Weight(nodes[excludedNodeIndex - 1], nodeMoveStrategy.NodeAfterMove) + completeGraph.Weight(nodeMoveStrategy.NodeAfterMove, nodes[excludedNodeIndex + 1]);
            
            nodeMoveStrategy.CostImprovement = costFromUnvisited - currentCost;

            return nodeMoveStrategy;
            
        }

        private IMoveStrategy GetRandomEdgeMove(Path path, IGraph completeGraph)
        {
            var nodes = path.Nodes;
            var newMove = new EdgeMoveStrategy();

            var i = _randomGenerator.Next(0, path.Count - 3);
            var j = _randomGenerator.Next(i+2, path.Count - 1);

            var lineCost = completeGraph.Weight(nodes[i], nodes[i + 1]) + completeGraph.Weight(nodes[j], nodes[j + 1]);

            var crossCost = completeGraph.Weight(nodes[i], nodes[j]) + completeGraph.Weight(nodes[i + 1], nodes[j + 1]);

            var costDifference = crossCost - lineCost;

            newMove.CostImprovement = costDifference;
            newMove.FirstNodePathIndex = i;
            newMove.SecondNodePathIndex = j;

            return newMove;
        }
    }
}