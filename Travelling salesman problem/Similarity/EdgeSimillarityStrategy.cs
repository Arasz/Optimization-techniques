using ConsoleApplication.Graphs;

namespace ConsoleApplication.Similarity
{
    public class EdgeSimillarityStrategy : ISimilarityCalculationStrategy
    {
        public double CalculateSimilarity(Path first, Path second)
        {
            var similarityCounter = 0;
            // we assum that paths have the same length
            for (var n = 0; n < first.Count-1; n++)
            {
                if (first.Nodes[n] == second.Nodes[n] && first.Nodes[n + 1] == second.Nodes[n + 1])
                    similarityCounter++;
                else if (first.Nodes[n+1] == second.Nodes[n] && first.Nodes[n] == second.Nodes[n + 1])
                    similarityCounter++;
                else if (first.Nodes[n] == second.Nodes[n+1] && first.Nodes[n+1] == second.Nodes[n])
                    similarityCounter++;
            }
            return similarityCounter;
        }

    }
}