namespace ConsoleApplication.Graphs
{
    public struct Edge
    {
        public int SourceNode { get; }

        public int TargetNode { get; }

        public int Weight { get; }

        public Edge(int sourceNode, int targetNode, int weight)
        {
            SourceNode = sourceNode;
            TargetNode = targetNode;
            Weight = weight;
        }
    }
}