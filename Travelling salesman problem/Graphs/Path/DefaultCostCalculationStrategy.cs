namespace ConsoleApplication.Graphs
{
    public class DefaultCostCalculationStrategy : ICostCalculationStrategy
    {
        private readonly IGraph _graph;

        public DefaultCostCalculationStrategy(IGraph graph)
        {
            _graph = graph;
        }

        public int Calculate(Path path)
        {
            var result = 0;
            for (var i = 0; i < path.Count - 1; i++)
                result += _graph.Weight(path.Nodes[i], path.Nodes[i + 1]);

            return result;
        }
    }
}