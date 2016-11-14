using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    public interface IMoveStrategy
    {
        /// <summary>
        /// Diffrence between old path cost and new path cost 
        /// </summary>
        int CostImprovement { get; }

        /// <summary>
        /// Makes move (change in path) 
        /// </summary>
        /// <param name="path"> Entry path </param>
        /// <returns> New path </returns>
        Path Move(Path path);
    }
}