using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApplication.Solver
{
	public class TspIteratedLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

        private int ILSRepeatAmount = 4;

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

			int bestResult = Int32.MaxValue;
			var bestPath = new List<int>();
			for(int i=0; i<ILSRepeatAmount; i++)
			{
				using(context)
				{
					var pathAccumulator = new PathAccumulator();
					var startNode = _randomGenerator.Next(0, _completeGraph.NodesCount-1);
                	_initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);
					var accumulatedPath = pathAccumulator.Paths[0];
				    bestPath = accumulatedPath.NodesList;
					bestResult = tspSolvingAlgorithm.Solve(bestPath.First(), _completeGraph, bestPath);
				}
				UpdatePathResults(bestResult, bestPath);
				UpdateTimeMeasures(context.Elapsed);
				Console.WriteLine(i + " / 10");
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