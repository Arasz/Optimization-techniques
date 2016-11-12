using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Solver.Result
{
	public class NullSolverResult : ISolverResult
	{
		public IList<Path> Paths { get; } = new List<Path>();

		public void AddPath(Path path)
		{
		}
	}
}