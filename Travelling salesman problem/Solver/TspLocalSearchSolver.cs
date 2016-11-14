using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Results;
using ConsoleApplication.Solver.Statistics;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public class TspLocalSearchSolver : SolverBase
    {
        public TspLocalSearchSolver(IGraph completeGraph) : base(completeGraph)
        {
        }

        public override ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
        {
            Statistics = new BasicSolverStatistics();

            var newAccumulator = new SolverResult();

            foreach (var path in solverResult.Paths)
            {
                var context = SolvingTimeContext.Instance;

                Path localyBestPath;

                using (context)
                {
                    localyBestPath = tspSolvingAlgorithm.Solve(SelectStartNode(path), CompleteGraph, path);
                }

                newAccumulator.AddPath(localyBestPath);
                Statistics.UpdateSolvingResults(localyBestPath, context.Elapsed);
            }

            return newAccumulator;
        }

        private int SelectStartNode(Path path) => path.Nodes.First();
    }
}