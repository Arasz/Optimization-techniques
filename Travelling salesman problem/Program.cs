using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver;
using System;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dataLoader = new GraphLoader(@"D:\Library\Documents\GitHub\Optimization techniques\Travelling salesman problem\kroA100.xml", 100);
            var graph = dataLoader.Load();
            var solver = new TspSolver(new NearestNeighbourAlgorithm(), graph);
            solver.Solve();
            Console.WriteLine($"Min cost {solver.BestResult}, max cost {solver.WorstResult}, mean cost {solver.MeanReasult}");
            Console.WriteLine($"Best path ({solver.BestPath.Count()} elements): {solver.BestPath.Select(i => i.ToString()).Aggregate("", (accu, str) => accu += $"{str}, ")}");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}