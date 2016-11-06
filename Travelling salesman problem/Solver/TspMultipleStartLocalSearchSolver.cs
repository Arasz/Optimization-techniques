using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Int32;

namespace ConsoleApplication.Solver
{
	public class TspMultipleStartLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

	    private const int InsideAlgorithmRepeatAmount = 1000;

	    private const int MslsRepeatAmount = 10;

	    private readonly Random _randomGenerator;

		public TspMultipleStartLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm) : base(completeGraph)
		{
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
            _randomGenerator = new Random();
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			var context = SolvingTimeContext.Instance;  

			var bestResult = MaxValue;
			var bestPath = new List<int>();
			for(var i=0; i<MslsRepeatAmount; i++)
			{
				using(context)
				{
					for(var j=0; j<InsideAlgorithmRepeatAmount; j++)
			    	{
						var pathAccumulator = new PathAccumulator();

						var startNode = _randomGenerator.Next(0, CompleteGraph.NodesCount-1);

                		_initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);

						var accumulatedPath = pathAccumulator.Paths[0];
				    	var localPath = accumulatedPath.Nodes;
						var localResult = tspSolvingAlgorithm.Solve(localPath.First(), CompleteGraph, localPath);

				        if(localResult < bestResult)
						{
							bestResult = localResult;
							bestPath = localPath;
						}
			    	}
					UpdatePathResults(bestResult, bestPath);
					UpdateTimeMeasures(context.Elapsed);
				}
			}
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
		{
			throw new NotImplementedException();
		}

        public override void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode)
        {
            throw new NotImplementedException();
        }
    }
}