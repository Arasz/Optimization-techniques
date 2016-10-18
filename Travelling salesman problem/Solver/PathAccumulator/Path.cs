using System.Collections.Generic;

namespace ConsoleApplication.Solver.SolverVisitor
{
	public class Path
	{
		public int Cost { get; set; }

		public List<int> NodesList { get; set; }

		public Path(List<int> nodesList, int cost)
		{
			NodesList = nodesList;
			Cost = cost;
		}
	}
}