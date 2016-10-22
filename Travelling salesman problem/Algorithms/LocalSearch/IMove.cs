using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    public interface IMove
    {
        /// <summary>
        /// Reprezentuje zysk jaki otrzymujemy po wykonaniu ruchu (zmniejszenie kosztu) 
        /// </summary>
        int CostImprovement { get; set; }

        bool Move(List<int> path);
    }
}