using ConsoleApplication.Algorithms;
using ConsoleApplication.Common;
using ConsoleApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication.Solver
{
    public class TspSolver : ISolver
    {
        private readonly IData _data;

        public IEnumerable<Point> BestPath { get; private set; }

        public int BestResult { get; private set; }

        public int MeanReasult { get; private set; }

        public IList<int> Results { get; } = new List<int>();

        public IAlgorithm TspSolvingAlgorithm { get; set; }

        public int WorstResult { get; private set; }

        public TspSolver(IAlgorithm tspSolvingAlgorithm, IData data)
        {
            _data = data;
            TspSolvingAlgorithm = tspSolvingAlgorithm;
        }

        public void Solve()
        {
            var cityPositions = _data.Positions;

            BestResult = int.MaxValue;
            WorstResult = int.MinValue;

            foreach (var cityPosition in cityPositions)
            {
                IEnumerable<Point> path;
                var localResult = TspSolvingAlgorithm.Solve(cityPosition, _data, out path);

                UpdateResults(localResult, path);
            }
        }

        private void UpdateResults(int localResult, IEnumerable<Point> path)
        {
            if (localResult < BestResult)
            {
                BestResult = localResult;
                BestPath = path;
            }

            if (localResult > WorstResult)
                WorstResult = localResult;

            Results.Add(localResult);
            MeanReasult = (int)Math.Floor(Results.Average());
        }
    }
}