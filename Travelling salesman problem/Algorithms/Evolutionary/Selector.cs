using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public class Selector : ISelector
	{
		private Random Random { get; } = new Random();

		/// <exception cref="ArgumentException">"Population must conatin at least two individuals"</exception>
		public Tuple<Path, Path> Select(ICollection<Path> population)
		{
			if (population.Count < 2)
				throw new ArgumentException("Population must conatin at least two individuals");

			var indexList = new HashSet<int>();
			while (indexList.Count < 2)
				indexList.Add(Random.Next(0, population.Count));

			return new Tuple<Path, Path>(population.ElementAt(indexList.First()), population.ElementAt(indexList.Last()));
		}
	}
}