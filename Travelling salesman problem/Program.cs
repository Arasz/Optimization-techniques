﻿using ConsoleApplication.Algorithms.LocalSearch;
using ConsoleApplication.Algorithms;
using ConsoleApplication.Configuration;
using ConsoleApplication.Graphs;
using ConsoleApplication.Printer;
using ConsoleApplication.Printer.ContentBuilders;
using ConsoleApplication.Solver;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApplication
{
	public class Program
	{
		private const int Steps = 50;

		public static void Main(string[] args)
		{
			var buildConfig = BuildConfiguration();
			var dataPath = buildConfig[$"{nameof(AppConfiguration)}:{nameof(AppConfiguration.GraphDataPath)}"];
			var coordinatesPath = buildConfig[$"{nameof(AppConfiguration)}:{nameof(AppConfiguration.GraphCoordinatesPath)}"];
			var dataLoader = new GraphLoader(dataPath, 100);
			var graph = dataLoader.Load();

			var solver = new TspSolver(graph);

			var resultPrinter = new ResultPrinter()
				.AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(solver))
				.AddPrinter(new FilePrinter($"{DateTime.Now.Date.ToFileTime()}_results.txt"), new FileContentBuilder(solver, coordinatesPath));

			//RunBasicAlgorithms(solver, resultPrinter);

			//RunAlgorithmsWithLocalSearch(graph, solver);

			RunMSLSAlgorithms(graph, solver);

			
		}

        private static void RunBasicAlgorithms(TspSolver solver, IResultPrinter resultPrinter)
        {
			SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new EdgeFinder()), "NEAREST NEIGHBOR", resultPrinter);

			SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new EdgeFinder()), "GREEDY CYCLE", resultPrinter);

			SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)), "NEAREST NEIGHBOR GRASP", resultPrinter);

			SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)), "GREEDY CYCLE GRASP", resultPrinter);
        }

		private static void RunAlgorithmsWithLocalSearch(IGraph graph, TspSolver solver)
        {
            var localSearchSolver = new TspLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new EdgeFinder()));
			SolveAndPrint(localSearchSolver, new LocalSearchAlgorithm(Steps, new EdgeFinder()),
				"NN WITH LOCAL SEARCH", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new EdgeFinder()));
			SolveAndPrint(localSearchSolver, new LocalSearchAlgorithm(Steps, new EdgeFinder()),
				"GC with local search opt", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
			SolveAndPrint(localSearchSolver, new LocalSearchAlgorithm(Steps, new EdgeFinder()),
				"NN Grasp with local search opt", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
			SolveAndPrint(localSearchSolver, new LocalSearchAlgorithm(Steps, new EdgeFinder()),
				"GC GRASP with local search opt", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
			SolveAndPrint(localSearchSolver, new LocalSearchAlgorithm(Steps, new EdgeFinder()),
				"RANDOM with local search opt", getLocalSearchResultPrinter(localSearchSolver));
        }

		private static void RunMSLSAlgorithms(IGraph graph, TspSolver solver)
        {
			var localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
			var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"NN Grasp with local search (MSLS)", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"GC Grasp with local search (MSLS)", getLocalSearchResultPrinter(localSearchSolver));

			localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"Random with local search (MSLS)", getLocalSearchResultPrinter(localSearchSolver));
        }

        private static IResultPrinter getLocalSearchResultPrinter(ISolver localSearchSolver)
        {
            return new ResultPrinter().AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(localSearchSolver));
        }

        private static IConfigurationRoot BuildConfiguration() => new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("config.json")
			.Build();

		private static void SolveAndPrint(ISolver solver, IAlgorithm algorithm, string title, IResultPrinter resultPrinter)
		{
			solver.Solve(algorithm);
			resultPrinter.Print(title);
		}
	}
}