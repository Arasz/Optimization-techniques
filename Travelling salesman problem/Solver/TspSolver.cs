using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Result;

namespace ConsoleApplication.Solver
{
	public class TspSolver : SolverBase
	{

		public TspSolver(IGraph completeGraph) : base(completeGraph)
		{
		}

		public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
		{
		    Statistics = new SolverStatistics();
            var solverResult = new SolverResult();


		    for (var startNode = 0; startNode < CompleteGraph.NodesCount; startNode++)
		        Solve(tspSolvingAlgorithm, startNode, solverResult);
		    return solverResult;
		}

	    private ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, int startNode, ISolverResult solverResult)
	    {
	        Path bestPath;

	        var context = SolvingTimeContext.Instance;
	        using (context)
	        {
	            bestPath = tspSolvingAlgorithm.Solve(startNode, CompleteGraph);
	        }

	        Statistics.UpdateSolvingResults(bestPath, context.Elapsed);
	        solverResult.AddPath(bestPath);

	        return solverResult;
	    }

	    public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, int startNode)
	    {
	        var solverResult = new SolverResult();
	        return Solve(tspSolvingAlgorithm, startNode, solverResult);
	    }
	}
}