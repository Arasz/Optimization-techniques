using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;

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
			var pathAccumulator = new PathAccumulator();
            var startNode = _randomGenerator.Next(0, _completeGraph.NodesCount-1);
			_initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);

            var context = SolvingTimeContext.Instance;
            for(int i=0; i<MSLSRepeatAmount; i++){
                for(int j=0; j<InsideAlgorithmRepeatAmount; j++){
			        foreach (var path in pathAccumulator.Paths)
			        {
				        var localPath = path.NodesList;
				        int localResult;

				        using (context)
					        localResult = tspSolvingAlgorithm.Solve(path.NodesList.First(), _completeGraph, localPath);
				        UpdatePathResults(localResult, localPath);
			        }
                }
                UpdateTimeMeasures(context.Elapsed);
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