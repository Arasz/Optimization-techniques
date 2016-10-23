using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.LocalSearch
{
     internal class NodeMove : IMove
    {
        public int CostImprovement { get; set; }

        public int ExcludedNodePathIndex { get; set; }

        private int ExcludedNodeValue { get; set; }

        public int NodeAfterMove { get; set; }

        public bool Move(List<int> path)
        {
            ExcludedNodeValue = path[ExcludedNodePathIndex];
            path[ExcludedNodePathIndex] = NodeAfterMove;
            ExcludedNodeValue = -1;
            return true;
        }

        public bool Undo(List<int> path)
        {
            if(ExcludedNodeValue < 0)
                return false;
                
            path[ExcludedNodePathIndex] = ExcludedNodeValue;
            return true;
        }
    }
}