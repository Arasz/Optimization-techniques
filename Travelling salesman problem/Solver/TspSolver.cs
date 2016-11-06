using System;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;

namespace ConsoleApplication.Solver
{
	public class TspSolver : SolverBase
	{
	    private readonly IPathAccumulator _pathAccumulator = new PathAccumulator();

		public TspSolver(IGraph completeGraph) : base(completeGraph)
		{
		}

		public override IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm)
		{
		    Statistics = new SolverStatistics();

		    for (var startNode = 0; startNode < CompleteGraph.NodesCount; startNode++)
		        Solve(tspSolvingAlgorithm, startNode);
		    return _pathAccumulator;
		}

	    private void Solve(IAlgorithm tspSolvingAlgorithm, int startNode)
		{
			Path bestPath;

			var context = SolvingTimeContext.Instance;
		    using (context)
		    {
		        bestPath = tspSolvingAlgorithm.Solve(startNode, CompleteGraph);
		    }

			Statistics.UpdateSolvingResults(bestPath, context.Elapsed);
		    _pathAccumulator.AddPath(bestPath);
		}
	}
}