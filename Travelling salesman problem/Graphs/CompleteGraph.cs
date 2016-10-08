using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Graphs
{
    public class CompleteGraph : IGraph
    {
        private readonly int[][] _graphMatrix;

        public IGraphIterator Iterator => new GraphIterator(this);

        public IEnumerable<int> Nodes { get; }

        public int NodesCount => _graphMatrix.Length;

        public CompleteGraph(int[][] graphMatrix)
        {
            _graphMatrix = graphMatrix;

            Nodes = Enumerable.Range(0, _graphMatrix.Length);
        }

        public int[] NodeEdgesWeights(int node) => _graphMatrix[node];

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var row in _graphMatrix)
            {
                foreach (var cost in row)
                    stringBuilder.Append($"{cost}, ");

                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}