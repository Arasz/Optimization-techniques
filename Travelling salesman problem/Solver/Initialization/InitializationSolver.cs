using ConsoleApplication.Algorithms;
using ConsoleApplication.Solver.SolverResult;

namespace ConsoleApplication.Solver
{
    public class InitializationSolver : IInitializationSolver
    {
        private readonly ISolver _solver;
        private readonly IAlgorithm _algorithm;

        public InitializationSolver(ISolver solver, IAlgorithm algorithm)
        {
            _solver = solver;
            _algorithm = algorithm;
        }

        public ISolverResult Solve(int startNode) => _solver.Solve(_algorithm, startNode);
    }
}