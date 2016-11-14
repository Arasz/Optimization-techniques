using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
    /// <summary>
    /// Representation of path in graph 
    /// </summary>
    public class Path
    {
        private readonly ICostCalculationStrategy _costCalculationStrategy;

        private int _cost = -1;

        /// <summary>
        /// Path cost 
        /// </summary>
        public int Cost
        {
            get
            {
                if (_cost < 0)
                    _cost = _costCalculationStrategy.Calculate(this);
                return _cost;
            }
        }

        /// <summary>
        /// Nodes on the path count 
        /// </summary>
        public int Count => Nodes.Count;

        /// <summary>
        /// Path nodes 
        /// </summary>
        public List<int> Nodes { get; }

        /// <summary>
        /// Path with no nodes and cost equal to max int value 
        /// </summary>
        public static Path WorstCasePath => new Path(new List<int>(), new ConstCostCalculationStrategy(int.MaxValue));

        public Path(List<int> nodes, Path prototype)
        {
            Nodes = nodes;
            _costCalculationStrategy = prototype._costCalculationStrategy;
        }

        public Path(List<int> nodes, ICostCalculationStrategy costCalculationStrategy)
        {
            _costCalculationStrategy = costCalculationStrategy;
            Nodes = nodes;
        }
    }
}