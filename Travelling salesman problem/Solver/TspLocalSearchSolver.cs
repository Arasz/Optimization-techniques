using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using System.Linq;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Solver
{
	public class TspLocalSearchSolver : SolverBase
	{

		public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
		{
		    Statistics = new SolverStatistics();

		    var newAccumulator = new SolverResult.SolverResult();

			foreach (var path in solverResult.Paths)
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