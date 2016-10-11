using System.Collections.Generic;
using System.IO;
using System.Xml;
using TSP.Data.Graph;

namespace ConsoleApplication.Data
{
	public class GraphLoader
	{
		private readonly string _dataPath;

		public Graph LoadedGraph { get; private set; }

		public GraphLoader(string dataPath)
		{
			_dataPath = dataPath;
		}

		private void Load()
		{
			using (var dataStream = File.OpenRead(_dataPath))
			{
				using (var xmlReader = XmlReader.Create(dataStream))
				{
					// read size from file
					LoadedGraph = new Graph(100);
					var nodeCounter = 0;
					while (xmlReader.ReadToFollowing("vertex"))
					{
						var edges = new List<Edge>();
						while (xmlReader.ReadToFollowing("edge"))
						{
							xmlReader.MoveToFirstAttribute();
							var cost = int.Parse(xmlReader.Value);
							xmlReader.MoveToContent();
							var nodeId = int.Parse(xmlReader.Value);
							edges.Add(new Edge(cost, nodeId));
						}

						var node = new Node(nodeCounter);
					}
				}
			}
		}
	}
}