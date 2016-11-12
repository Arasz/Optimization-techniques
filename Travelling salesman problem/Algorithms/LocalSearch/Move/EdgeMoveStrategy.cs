using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    internal class EdgeMoveStrategy : IMoveStrategy
    {
        public int CostImprovement { get; set; }

        public int FirstNodePathIndex { get; set; }

        public int SecondNodePathIndex { get; set; }

        public Path Move(Path path)
        {
            var nodes = path.Nodes;

            var distanceInPathSegments = SecondNodePathIndex - FirstNodePathIndex;

            var firstSegment = nodes.GetRange(0, FirstNodePathIndex + 1);

            var secondSegment = nodes.GetRange(FirstNodePathIndex + 1, distanceInPathSegments);

            var thirdSegment = nodes.GetRange(SecondNodePathIndex + 1, path.Count - SecondNodePathIndex - 1);

            secondSegment.Reverse();
            nodes.Clear();
            nodes.AddRange(firstSegment);
            nodes.AddRange(secondSegment);
            nodes.AddRange(thirdSegment);

            return new Path(nodes, path);
        }
    }
}