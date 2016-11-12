using ConsoleApplication.Solver.SolverResult;

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