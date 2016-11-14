using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.Results;
using ConsoleApplication.Solver.Statistics;
using System;

namespace ConsoleApplication.Solver
{
    public abstract class SolverBase : ISolver
    {
        protected readonly IGraph CompleteGraph;

        public virtual ISolverStatistics SolvingStatistics => Statistics;

        internal BasicSolverStatistics Statistics { get; set; }

        protected SolverBase(IGraph completeGraph)
        {
            CompleteGraph = completeGraph;
            Statistics = new BasicSolverStatistics();
        }

        public virtual ISolverResult Solve(IAlgorithm tspSolvingAlgorithm)
        {
            throw new NotImplementedException();
        }

        public virtual ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, ISolverResult solverResult)
        {
            throw new NotImplementedException();
        }

        public virtual ISolverResult Solve(IAlgorithm tspSolvingAlgorithm, int startNode)
        {
            throw new NotImplementedException();
        }
    }
}