using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Solver
{
	public class TspSolver : SolverBase
	{
	    private readonly ISolverResult _solverResult = new SolverResult.SolverResult();

		public TspSolver(IGraph completeGraph) : base(completeGraph)
		{
		}

		public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
		{
		    Statistics = new SolverStatistics();

		    for (var startNode = 0; startNode < CompleteGraph.NodesCount; startNode++)
		        Solve(tspSolvingAlgorithm, startNode);
		    return _solverResult;
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
		    _solverResult.AddPath(bestPath);
		}
	}
}