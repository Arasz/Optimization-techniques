using ConsoleApplication.Algorithms.LocalSearch;
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

			//RunBasicAlgorithms(solver, coordinatesPath);

			//RunAlgorithmsWithLocalSearch(graph, solver, coordinatesPath);

//			RunMSLSAlgorithms(graph, solver, coordinatesPath);

			//RunILSAlgorithm(graph, solver, coordinatesPath);

		    RunRandomLSPathsStatistics(graph, solver, coordinatesPath);

			
		}

        private static void RunBasicAlgorithms(TspSolver solver, string coordinatesPath)
        {
			var resultPrinter = new ResultPrinter()
				.AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(solver))
				.AddPrinter(new FilePrinter($"{DateTime.Now.Date.ToFileTime()}_results.txt"), new FileContentBuilder(solver, coordinatesPath));


			SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new EdgeFinder()), "NEAREST NEIGHBOR", resultPrinter);

			SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new EdgeFinder()), "GREEDY CYCLE", resultPrinter);

			SolveAndPrint(solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)), "NEAREST NEIGHBOR GRASP", resultPrinter);

			SolveAndPrint(solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)), "GREEDY CYCLE GRASP", resultPrinter);
        }

		private static void RunAlgorithmsWithLocalSearch(IGraph graph, TspSolver solver, string coordinatesPath)
		{
		    var edgeFinder = new EdgeFinder();
		    var graspEdgeFiner = new GraspEdgeFinder(3);

		    var localSearchSolver = new TspLocalSearchSolver(graph);

		    var localSearchALgorithm = new LocalSearchAlgorithm(Steps,edgeFinder);

		    var generatedPaths = solver.Solve(new NearestNeighborAlgorithm(Steps, edgeFinder));
		    localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
		    LocalSearchResultPrinter(localSearchSolver, "NN_LS", coordinatesPath).Print("NN with local search opt");

		    generatedPaths = solver.Solve(new GreedyCycleAlgorithm(Steps, edgeFinder));
		    localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
		    LocalSearchResultPrinter(localSearchSolver, "GC_LS", coordinatesPath).Print("GC with local search opt");

		    generatedPaths = solver.Solve(new NearestNeighborAlgorithm(Steps, graspEdgeFiner));
		    localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
		    LocalSearchResultPrinter(localSearchSolver,  "NNG_LS", coordinatesPath).Print("NN Grasp with local search opt");

		    generatedPaths = solver.Solve(new GreedyCycleAlgorithm(Steps, graspEdgeFiner));
		    localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
		    LocalSearchResultPrinter(localSearchSolver, "GCG_LS", coordinatesPath).Print("GC GRASP with local search opt");

		    generatedPaths = solver.Solve(new RandomPathAlgorithm(Steps, edgeFinder));
		    localSearchSolver.Solve(localSearchALgorithm, generatedPaths);
		    LocalSearchResultPrinter(localSearchSolver, "Random_LS", coordinatesPath).Print("RANDOM with local search opt");
        }

		private static void RunMSLSAlgorithms(IGraph graph, TspSolver solver, string coordinatesPath)
        {
			var localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
			var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"NN Grasp with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "NNG_MSLS", coordinatesPath));

			localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"GC Grasp with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "GCG_MSLS", coordinatesPath));

			localSearchSolver = new TspMultipleStartLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"Random with local search (MSLS)", LocalSearchResultPrinter(localSearchSolver, "Random_MSLS", coordinatesPath));
        }

		private static void RunILSAlgorithm(IGraph graph, TspSolver solver, string coordinatesPath)
        {
            var localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new NearestNeighborAlgorithm(Steps, new GraspEdgeFinder(3)));
			var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"NN Grasp with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "NNG_ILS", coordinatesPath));

			localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new GreedyCycleAlgorithm(Steps, new GraspEdgeFinder(3)));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"GC Grasp with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "GCG_ILS", coordinatesPath));

			localSearchSolver = new TspIteratedLocalSearchSolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
			localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
			SolveAndPrint(localSearchSolver, localSearchAlgorithm,
				"Random with local search (ILS)", LocalSearchResultPrinter(localSearchSolver, "Random_ILS", coordinatesPath));
        }

	    private static void RunRandomLSPathsStatistics(IGraph graph, TspSolver solver, string coordinatesPath)
	    {
	        var localSearchSolver = new PathSimilaritySolver(graph, solver, new RandomPathAlgorithm(Steps, new EdgeFinder()));
	        var localSearchAlgorithm = new LocalSearchAlgorithm(Steps, new EdgeFinder());
	        localSearchSolver.Solve(localSearchAlgorithm);
	    }

		private static IResultPrinter LocalSearchResultPrinter(ISolver localSearchSolver, string algorithmName, string coordinatesPath)
        {
            return new ResultPrinter()
					.AddPrinter(new ConsolePrinter(), new ConsoleContentBuilder(localSearchSolver))
					.AddPrinter(new FilePrinter($"{algorithmName}_{DateTime.Now.Date.ToFileTime()}_results.txt"), new FileContentBuilder(localSearchSolver, coordinatesPath));
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