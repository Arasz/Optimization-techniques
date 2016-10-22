
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.LocalSearch
{
    internal class EdgeMove : IMove
    {
        public int CostImprovement { get; set; }

        public int FirstNodePathIndex { get; set; }

        public int SecondNodePathIndex { get; set; }

        public bool Move(List<int> path)
        {
            var distanceInPathSegments = SecondNodePathIndex - FirstNodePathIndex;

            var firstSegment = path.GetRange(0, FirstNodePathIndex + 1);

            var secondSegment = path.GetRange(FirstNodePathIndex + 1, distanceInPathSegments);

            var thirdSegment = path.GetRange(SecondNodePathIndex + 1, path.Count - SecondNodePathIndex - 1);

            secondSegment.Reverse();
            path.Clear();
            path.AddRange(firstSegment);
            path.AddRange(secondSegment);
            path.AddRange(thirdSegment);

            return true;
        }
    }
}