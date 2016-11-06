using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public interface IPathAccumulator
	{
		IList<Path> Paths { get; }

		void AddPath(Path path);
	}
}