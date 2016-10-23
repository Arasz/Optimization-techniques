using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspIteratedLocalSearchSolver : SolverBase
	{
		private readonly IAlgorithm _initializationAlgorithm;
		private readonly ISolver _initializationSolver;

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
			var pathAccumulator = new PathAccumulator();

                for(int i=0; i<ILSRepeatAmount; i++){
                    var startNode = _randomGenerator.Next(0, _completeGraph.NodesCount-1);
                    _initializationSolver.Solve(_initializationAlgorithm, pathAccumulator, startNode);
                }
                var context = SolvingTimeContext.Instance;
                using (context)
                {          
			        foreach (var path in pathAccumulator.Paths)
			        {
				        var localPath = path.NodesList;
				        int localResult;

					    localResult = tspSolvingAlgorithm.Solve(path.NodesList.First(), _completeGraph, localPath);
				        UpdatePathResults(localResult, localPath);
                    }
                }
                UpdateTimeMeasures(context.Elapsed);

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