using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public interface ISelector
	{
		/// <summary>
		/// Selects parents from population 
		/// </summary>
		/// <returns> Parents </returns>
		Tuple<Path, Path> Select(ICollection<Path> population);
	}
}