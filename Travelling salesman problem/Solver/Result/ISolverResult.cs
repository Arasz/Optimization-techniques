using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Solver.Result
{
	public interface ISolverResult
	{
		IList<Path> Paths { get; }

		void AddPath(Path path);
	}
}