using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication.Solver
{
	public class TspMultipleStartLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

        private int InsideAlgorithmRepeatAmount = 1000;

        private int MSLSRepeatAmount = 10;

        private Random _randomGenerator;

		public TspMultipleStartLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm) : base(completeGraph)
		{
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
            _randomGenerator = new Random();
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			var context = SolvingTimeContext.Instance;  

			int bestResult = Int32.MaxValue;
			var bestPath = new List<int>();
			for(int i=0; i<MSLSRepeatAmount; i++)
			{
				using(context)
				{
					for(int j=0; j<InsideAlgorithmRepeatAmount; j++)
			    	{
						var pathAccumulator = new PathAccumulator();
						var startNode = _randomGenerator.Next(0, _completeGraph.NodesCount-1);
                		_initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);
						var accumulatedPath = pathAccumulator.Paths[0];
				    	var localPath = accumulatedPath.NodesList;
						var localResult = tspSolvingAlgorithm.Solve(localPath.First(), _completeGraph, localPath);
						if(localResult < bestResult){
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