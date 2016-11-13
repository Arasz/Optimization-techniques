using ConsoleApplication.Graphs;
using System.Collections.Generic;
using ConsoleApplication.Similarity;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public interface ISimilarityFinder
	{
		IEnumerable<Fragment> FindSimilarFragments(Path first, Path second);
	}
}