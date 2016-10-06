using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public class TspSolver : ISolver
    {
        private readonly Graph _graph;

        public IEnumerable<int> BestPath { get; private set; }

        public int BestResult { get; private set; }

        public int MeanReasult { get; private set; }

        public IList<int> Results { get; } = new List<int>();

        public IAlgorithm TspSolvingAlgorithm { get; set; }

        public int WorstResult { get; private set; }

        public TspSolver(IAlgorithm tspSolvingAlgorithm, Graph graph)
        {
            _graph = graph;
            TspSolvingAlgorithm = tspSolvingAlgorithm;
        }

        public void Solve()
        {
            BestResult = int.MaxValue;
            WorstResult = int.MinValue;

            for (var startNode = 0; startNode < _graph.NodesCount; startNode++)
            {
                IList<int> path;
                //TODO: pass steps in ctr
                var localResult = TspSolvingAlgorithm.Solve(startNode, _graph, 50, out path);

                UpdateResults(localResult, path);
            }
        }

        private void UpdateResults(int localResult, IEnumerable<int> path)
        {
            if (localResult < BestResult)
            {
                BestResult = localResult;
                BestPath = path;
            }

            if (localResult > WorstResult)
                WorstResult = localResult;

            Results.Add(localResult);
            MeanReasult = (int)Math.Round(Results.Average());
        }
    }
}