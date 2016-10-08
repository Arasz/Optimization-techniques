using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver;
using System;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        private const int STEPS = 50;
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dataLoader = new GraphLoader(@"/Users/palka/PUT/TO/lab01/Optimization-techniques/Travelling salesman problem/kroA100.xml", 100);
            var graph = dataLoader.Load();
            var solver = new TspSolver(graph);
            solver.Solve(new NearestNeighbourAlgorithm(STEPS));
            Console.WriteLine($"Min cost {solver.BestResult}, max cost {solver.WorstResult}, mean cost {solver.MeanReasult}");
            Console.WriteLine($"Best path ({solver.BestPath.Count()} elements): {solver.BestPath.Select(i => i.ToString()).Aggregate("", (accu, str) => accu += $"{str}, ")}");
            Console.WriteLine();
        }
    }
}