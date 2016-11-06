using System;
using System.Collections.Generic;

namespace ConsoleApplication.Graphs
{
	public class Path
	{
	    private readonly ICostCalculationStrategy _costCalculationStrategy;
	    private int _cost = -1;

	    /// <summary>
	    /// Koszt ścieżki
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
	    /// Ilość wierzchołków na ścieżce
	    /// </summary>
	    public int Count => Nodes.Count;

	    /// <summary>
	    /// Path nodes
	    /// </summary>
		public List<int> Nodes { get; }


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