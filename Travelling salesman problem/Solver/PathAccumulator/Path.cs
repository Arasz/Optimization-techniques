using System.Collections.Generic;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public class Path
	{
		public int Cost { get; set; }

		public IList<int> NodesList { get; set; }

		public Path(IList<int> nodesList, int cost)
		{
			NodesList = nodesList;
			Cost = cost;
		}
	}
}