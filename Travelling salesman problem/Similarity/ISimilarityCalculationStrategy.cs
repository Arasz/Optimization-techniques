using System.Collections.Generic;
using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculationStrategy
    {
        /// <summary>
        /// Calculates similarity factor between paths
        /// </summary>
        /// <returns>Similarity matrix</returns>
        double CalculateSimilarity(Path first, Path second);

        /// <summary>
        /// Finds similar fragments between two paths
        /// </summary>
        IEnumerable<Fragment> FindSimilarFragments(Path first, Path second);
    }
}