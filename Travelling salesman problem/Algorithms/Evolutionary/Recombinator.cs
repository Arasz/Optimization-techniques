using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class Recombinator : IRecombinator
	{
		private readonly ISimilarityFinder _similarityFinder;

		public Recombinator(ISimilarityFinder similarityFinder)
		{
			_similarityFinder = similarityFinder;
		}

		public Path Recombine(Path mother, Path father)
		{
			var similarFragments = _similarityFinder.FindSimilarFragments(mother, father);
			return ConnectFragments(similarFragments);
		}

		private Path ConnectFragments(IEnumerable<Fragment> fragments)
		{
			throw new NotImplementedException();
		}
	}
}