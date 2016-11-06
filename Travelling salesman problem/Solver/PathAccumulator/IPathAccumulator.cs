using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public interface IPathAccumulator
	{
		IList<Path> Paths { get; }

		IPathAccumulator AddPath(Path path);
	}
}