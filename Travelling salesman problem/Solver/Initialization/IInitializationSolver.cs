using ConsoleApplication.Solver.Results;

namespace ConsoleApplication.Solver
{
    public interface IInitializationSolver
    {
        /// <summary>
        /// Solves TSP starting from given node. 
        /// </summary>
        /// <returns></returns>
        ISolverResult Solve(int startNode);
    }
}