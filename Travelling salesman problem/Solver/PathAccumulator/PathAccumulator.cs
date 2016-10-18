using System.Collections.Generic;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public class PathAccumulator : IPathAccumulator
	{
		public IList<Path> Paths { get; } = new List<Path>();

		public void AddPath(Path path) => Paths.Add(path);
	}
}