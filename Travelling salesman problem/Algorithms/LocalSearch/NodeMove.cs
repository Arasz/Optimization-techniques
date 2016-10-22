using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.LocalSearch
{
     internal class NodeMove : IMove
    {
        public int CostImprovement { get; set; }

        public int ExcludedNodePathIndex { get; set; }

        public int NodeAfterMove { get; set; }

        public bool Move(List<int> path)
        {
            path[ExcludedNodePathIndex] = NodeAfterMove;
            return true;
        }
    }
}