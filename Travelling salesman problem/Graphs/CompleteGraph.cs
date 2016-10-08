using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Graphs
{
    public class CompleteGraph : IGraph
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

        public IGraphIterator Iterator => new GraphIterator(this);

        public IEnumerable<int> Nodes { get; }

        public int NodesCount => _graphMatrix.Length;

        public CompleteGraph(int[][] graphMatrix)
        {
            _graphMatrix = graphMatrix;

            Nodes = Enumerable.Range(0, _graphMatrix.Length);
            CurrentNode = _minNode;
        }

        public int Cost(int destinationNode)
        {
            return _graphMatrix[CurrentNode][destinationNode];
        }

        public int NearestNode(IEnumerable<int> unvisitedNodes)
        {
            var bestNode = -1;
            var minCost = -1;
            foreach (var index in unvisitedNodes.Where(index => minCost < 0 || minCost > _graphMatrix[CurrentNode][index]))
            {
                minCost = _graphMatrix[CurrentNode][index];
                bestNode = index;
            }
            return bestNode;
        }

        public int[] NodeEdgesWeights(int node) => _graphMatrix[node];

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