using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Graphs
{
    public class Graph
    {
        private readonly int[][] _graphMatrix;

        private int _currentNode;
        private int _maxNode;
        private int _minNode;
        private int[] _nearestNodes;

        public int CostToNearest => _graphMatrix[CurrentNode][_nearestNodes[CurrentNode]];

        public int CurrentNode
        {
            get { return _currentNode; }
            set
            {
                if (value > _maxNode)
                    _currentNode = _maxNode;
                else if (value < _minNode)
                    _currentNode = _minNode;
                else
                    _currentNode = value;
            }
        }

        public int[] Nodes { get; }

        public int NodesCount => _graphMatrix.Length;

        public Graph(int[][] graphMatrix, int minNode, int maxNode)
        {
            _graphMatrix = graphMatrix;
            _minNode = minNode;
            _maxNode = maxNode;

            Nodes = Enumerable.Range(_minNode, _maxNode + 1).ToArray();
            CurrentNode = _minNode;
        }

        public int Cost(int destinationNode) => _graphMatrix[CurrentNode][destinationNode];

        public IEnumerable<int> NearestNodes() //TODO: Optimize this shit
        {
            var lastMinCost = 0;

            for (var x = 0; x < _maxNode + 1; x++)
            {
                var minCost = _graphMatrix[CurrentNode].Where(cost => cost > lastMinCost).Min();
                lastMinCost = minCost;

                var index = 0;
                while (index < _graphMatrix[CurrentNode].Length - 1)
                {
                    index++;
                    if (_graphMatrix[CurrentNode][index] == minCost)
                        break;
                }
                yield return index;
            }

            yield return -1;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (var i = 0; i < _graphMatrix.Length; i++)
            {
                for (var j = 0; j < _graphMatrix[i].Length; j++)
                    stringBuilder.Append($"{_graphMatrix[i][j]}, ");

                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}