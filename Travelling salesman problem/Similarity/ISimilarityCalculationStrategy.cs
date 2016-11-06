using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    public interface ISimilarityCalculationStrategy
    {
        /// <summary>
        /// Calculates similarity beetwen paths
        /// </summary>
        /// <returns>Similarity matrix</returns>
        double CalculateSimilarity(Path first, Path second);
    }
}