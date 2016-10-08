using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
    public class GraphIterator : IGraphIterator
    {
        private readonly IGraph _completeGraph;

        public int CurrentNode { get; private set; }

        public IList<Edge> Edges { get; private set; }

        public GraphIterator(IGraph completeGraph)
        {
            _completeGraph = completeGraph;
            CurrentNode = 0;
            RecreateEdges();
        }

        public int EdgeWeight(int targetNode) => _completeGraph.NodeEdgesWeights(CurrentNode)[targetNode];

        public void MoveAlongEdge(Edge edge)
        {
            CurrentNode = edge.TargetNode;
            RecreateEdges();
        }

        public void MoveTo(int node)
        {
            CurrentNode = node;
            RecreateEdges();
        }

        private void RecreateEdges()
        {
            var edgesWeights = _completeGraph.NodeEdgesWeights(CurrentNode);
            Edges = new List<Edge>(edgesWeights.Length);

            for (var node = 0; node < edgesWeights.Length; node++)
                if (node != CurrentNode)
                    Edges.Add(new Edge(CurrentNode, node, edgesWeights[node]));
        }
    }
}