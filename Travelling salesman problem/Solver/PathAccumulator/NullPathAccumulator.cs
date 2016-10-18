using System.Collections.Generic;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public class NullPathAccumulator : IPathAccumulator
	{
		public IList<Path> Paths { get; } = new List<Path>();

		public void AddPath(Path path)
		{
		}
	}
}