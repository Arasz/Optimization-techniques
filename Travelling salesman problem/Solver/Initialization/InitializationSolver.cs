using ConsoleApplication.Algorithms;
using ConsoleApplication.Solver.Results;

namespace ConsoleApplication.Solver
{
    public class InitializationSolver : IInitializationSolver
    {
        private readonly IAlgorithm _algorithm;
        private readonly ISolver _solver;

        public InitializationSolver(ISolver solver, IAlgorithm algorithm)
        {
            _solver = solver;
            _algorithm = algorithm;
        }

        public ISolverResult Solve(int startNode) => _solver.Solve(_algorithm, startNode);
    }
}