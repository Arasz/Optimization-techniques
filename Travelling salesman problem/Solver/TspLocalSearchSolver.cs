using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

		public TspLocalSearchSolver(IGraph completeGraph, ISolver initializationSolver, IAlgorithm initializationAlgorithm) : base(completeGraph)
		{
			_initializationSolver = initializationSolver;
			_initializationAlgorithm = initializationAlgorithm;
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm)
		{
			var pathAccumulator = new PathAccumulator();

			_initializationSolver.Solve(_initializationAlgorithm, pathAccumulator);

			foreach (var path in pathAccumulator.Paths)
			{
				var localPath = path.NodesList;
				var context = SolvingTimeContext.Instance;

				int localResult;
				var startNode = path.NodesList.First();

				using (context)
					localResult = tspSolvingAlgorithm.Solve(startNode, _completeGraph, localPath);
				UpdateResults(localResult, localPath, context.Elapsed);
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