using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
	public class TspSolver : SolverBase
	{
		public TspSolver(IGraph completeGraph) : base(completeGraph)
		{
		}

		public override void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator)
		{
			for (var startNode = 0; startNode < _completeGraph.NodesCount; startNode++)
			{
				SolveOnce(tspSolvingAlgorithm, pathAccumulator, startNode);
			}
		}

		public override void SolveOnce(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode)
		{
			var path = new List<int>();
			int localResult;
			//TODO: pass steps in ctr
			var context = SolvingTimeContext.Instance;
			using (context)
			localResult = tspSolvingAlgorithm.Solve(startNode, _completeGraph, path);
			UpdateResults(localResult, path, context.Elapsed);
			pathAccumulator.AddPath(new Path(path.ToList(), localResult));
		}
	}
}