﻿using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public interface ISimilarityFinder
	{
		IEnumerable<Fragment> FindSimilarFragments(Path first, Path second);
	}
}