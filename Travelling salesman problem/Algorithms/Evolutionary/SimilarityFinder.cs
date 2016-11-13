using ConsoleApplication.Graphs;
using System.Collections.Generic;
using ConsoleApplication.Similarity;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class SimilarityFinder : ISimilarityFinder
	{
	    private readonly IEnumerable<ISimilarityCalculationStrategy> _similarityCalculationStrategies;

	    public SimilarityFinder(IEnumerable<ISimilarityCalculationStrategy> similarityCalculationStrategies)
	    {
	        _similarityCalculationStrategies = similarityCalculationStrategies;
	    }

		public IEnumerable<Fragment> FindSimilarFragments(Path first, Path second)
		{
		    var fragments = new List<Fragment>();

		    foreach (var similarityCalculationStrategy in _similarityCalculationStrategies)
		        fragments.AddRange(similarityCalculationStrategy.FindSimilarFragments(first, second));

		    return fragments;
		}
	}
}