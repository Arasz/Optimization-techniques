using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Graphs
{
    public class Graph
    {
        private readonly int[][] _graphMatrix;

        private int _currentNode;
        private int _lastNode;
        private int _minNode;

        public int CurrentNode
        {
            get { return _currentNode; }
            set
            {
                if (value > _lastNode)
                    _currentNode = _lastNode;
                else if (value < _minNode)
                    _currentNode = _minNode;
                else
                    _currentNode = value;
            }
        }

        public int[] Nodes { get; }

        public int NodesCount => _graphMatrix.Length;

        public Graph(int[][] graphMatrix, int minNode, int lastNode)
        {
            _graphMatrix = graphMatrix;
            _minNode = minNode;
            _lastNode = lastNode;

            Nodes = Enumerable.Range(_minNode, _lastNode + 1).ToArray();
            CurrentNode = _minNode;
        }

        public int Cost(int destinationNode) => _graphMatrix[CurrentNode][destinationNode];

        public int Cost(int startNode, int destinationNode) => _graphMatrix[startNode][destinationNode];

        public int NearestNode(IEnumerable<int> unvisitedNodes){
            int bestNode = -1;
            var minCost = -1;
            foreach(int index in unvisitedNodes){
                if(minCost < 0 || minCost > _graphMatrix[CurrentNode][index]){
                    minCost = _graphMatrix[CurrentNode][index];
                    bestNode = index;
                }
            }
            return bestNode;
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