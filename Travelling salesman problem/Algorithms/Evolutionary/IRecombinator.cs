using ConsoleApplication.Graphs;

namespace ConsoleApplication.Algorithms.Evolutionary
{
	public interface IRecombinator
	{
		/// <summary>
		/// Creates child from mother and father 
		/// </summary>
		Path Recombine(Path mother, Path father);
	}
}