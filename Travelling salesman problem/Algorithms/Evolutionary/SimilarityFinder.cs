using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using System;
using System.Collections.Generic;
using System.Linq;

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

			var random = new Random();

			return fragments.OrderBy(i => random.Next()).ToList();
		}
	}
}