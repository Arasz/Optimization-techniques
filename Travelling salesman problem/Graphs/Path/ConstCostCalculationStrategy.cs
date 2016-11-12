namespace ConsoleApplication.Graphs
{
    public class ConstCostCalculationStrategy : ICostCalculationStrategy
    {
        private readonly int _cost;

        public ConstCostCalculationStrategy(int cost)
        {
            _cost = cost;
        }

        public int Calculate(Path path) => _cost;
    }
}