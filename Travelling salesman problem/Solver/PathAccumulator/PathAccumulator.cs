﻿using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public class PathAccumulator : IPathAccumulator
	{
		public IList<Path> Paths { get; } = new List<Path>();

		public IPathAccumulator AddPath(Path path) => Paths.Add(path);
	}
}