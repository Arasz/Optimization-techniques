using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms.LocalSearch
{
     internal class NodeMoveStrategy : IMoveStrategy
    {
        public int CostImprovement { get; set; }

        public int ExcludedNodePathIndex { get; set; }

        private int ExcludedNodeValue { get; set; }

        public int NodeAfterMove { get; set; }

        public Path Move(Path path)
        {
            var nodes = path.Nodes;

            ExcludedNodeValue = nodes[ExcludedNodePathIndex];

            nodes[ExcludedNodePathIndex] = NodeAfterMove;

            ExcludedNodeValue = -1;

            return new Path(nodes, path);
        }
    }
}