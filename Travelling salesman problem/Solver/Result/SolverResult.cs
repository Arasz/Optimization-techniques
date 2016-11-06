using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver.SolverResult
{
	public class SolverResult : ISolverResult
	{
		public IList<Path> Paths { get; } = new List<Path>();

		public void AddPath(Path path) => Paths.Add(path);
	}
}