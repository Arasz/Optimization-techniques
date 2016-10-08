using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace ConsoleApplication.Graphs
{
    public class GraphLoader
    {
        private readonly string _dataPath;

        private readonly int _nodesCount;

        public GraphLoader(string dataPath, int nodesCount)
        {
            _nodesCount = nodesCount;
            _dataPath = dataPath;
        }

        public CompleteGraph Load()
        {
            if (!File.Exists(_dataPath))
                throw new FileNotFoundException();

            var graphMatrix = InitializeMatrix();

            using (var fileStream = File.OpenRead(_dataPath))
            using (var reader = XmlReader.Create(fileStream))
            {
                reader.ReadToFollowing("vertex");
                for (var node = 0; node < _nodesCount; node++)
                {
                    // Subtract one from size to compensate for current node
                    for (var i = 0; i < _nodesCount - 1; i++)
                    {
                        reader.ReadToFollowing("edge");
                        reader.MoveToFirstAttribute();
                        var cost = reader.Value;
                        reader.MoveToContent();
                        var connectedNode = reader.ReadElementContentAsInt();
                        graphMatrix[node][connectedNode] = (int)Math.Round(double.Parse(cost, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture));
                    }
                }
            }

            return new CompleteGraph(graphMatrix);
        }

        private int[][] InitializeMatrix()
        {
            var graphMatrix = new int[_nodesCount][];
            for (var i = 0; i < _nodesCount; i++)
                graphMatrix[i] = new int[_nodesCount];
            return graphMatrix;
        }
    }
}