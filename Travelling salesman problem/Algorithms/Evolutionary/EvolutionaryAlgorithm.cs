using ConsoleApplication.Graphs;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class EvolutionaryAlgorithm : AlgorithmBase
	{
		public EvolutionaryAlgorithm(int steps, IEdgeFinder edgeFinder) : base(steps, edgeFinder)
		{
		}

		public override Path Solve(int startNode, IGraph completeGraph, Path precalculatedPath = null)
		{
			throw new System.NotImplementedException();
		}

		private Path ConncetSimilarFragments(IEnumerable<Fragment> fragments)
		{
			return new Path(new List<int>(), new ConstCostCalculationStrategy(2));
		}

		private IEnumerable<Fragment> FindSimilarFragments(Path first, Path second)
		{
			return new List<Fragment>();
		}

		private Path RecombinationFunction(Path father, Path mother)
		{
			return ConncetSimilarFragments(FindSimilarFragments(father, mother));
		}
	}
}