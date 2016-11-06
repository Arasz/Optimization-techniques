using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspLocalSearchSolver : SolverBase
	{

		public override IPathAccumulator Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
		{
		    Statistics = new SolverStatistics();

		    var newAccumulator = new PathAccumulator();

			foreach (var path in pathAccumulator.Paths)
			{
				var context = SolvingTimeContext.Instance;

			    Path localyBestPath;

			    using (context)
			    {
			        localyBestPath = tspSolvingAlgorithm.Solve(SelectStartNode(path), CompleteGraph, path);
			    }

			    newAccumulator.AddPath(localyBestPath);
				Statistics.UpdateSolvingResults(localyBestPath, context.Elapsed);
			}

		    return newAccumulator;
		}

	    private int SelectStartNode(Path path) => path.Nodes.First();

	    public TspLocalSearchSolver(IGraph completeGraph) : base(completeGraph)
	    {
	    }
	}
}