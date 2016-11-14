using ConsoleApplication.Graphs;
using ConsoleApplication.Similarity;
using System.Collections.Generic;

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