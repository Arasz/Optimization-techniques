using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication.Similarity;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class Recombinator : IRecombinator
	{
		private readonly ISimilarityFinder _similarityFinder;
	    private readonly int _pathSize;

	    public Recombinator(ISimilarityFinder similarityFinder, int pathSize)
	    {
	        _similarityFinder = similarityFinder;
	        _pathSize = pathSize;
	    }

		public Path Recombine(Path mother, Path father)
		{
			var similarFragments = _similarityFinder.FindSimilarFragments(mother, father);
			return ConnectFragments(similarFragments, (mother.Nodes.Concat(father.Nodes).Distinct().ToArray()));
		}

		private Path ConnectFragments(IEnumerable<Fragment> fragments, IReadOnlyList<int> combinnedPathsNodes )
		{
			var newPathNodes = new List<int>();

		    foreach (var fragment in fragments)
		    {
		        if(fragment.Edge == null)
		            newPathNodes.Add(fragment.Node);
		        else
		        {
		            newPathNodes.Add(fragment.Edge.SourceNode);
		            newPathNodes.Add(fragment.Edge.TargetNode);
		        }
		    }

		    var distinctPath =  new HashSet<int>(newPathNodes);

		    if (distinctPath.Count < _pathSize)
		    {
		        var random = new Random();

		        while (_pathSize < distinctPath.Count)
		            distinctPath.Add(combinnedPathsNodes[random.Next(0, combinnedPathsNodes.Count)]);
		    }

		    return new Path(distinctPath.ToList(), new ConstCostCalculationStrategy(0));
		}
	}
}