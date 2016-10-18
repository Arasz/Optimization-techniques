using System.Collections.Generic;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public interface IPathAccumulator
	{
		IList<Path> Paths { get; }

		void AddPath(Path path);
	}
}