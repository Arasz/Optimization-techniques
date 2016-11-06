using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver.SolverResult
{
	public interface ISolverResult
	{
		IList<Path> Paths { get; }

		void AddPath(Path path);
	}
}