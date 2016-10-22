using ConsoleApplication.Algorithms;
using ConsoleApplication.Graphs;
using ConsoleApplication.Solver.SolverVisitor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Int32;

namespace ConsoleApplication.Solver
{
	public abstract class SolverBase : ISolver
	{
		protected readonly IGraph _completeGraph;

		private List<TimeSpan> TimeResults = new List<TimeSpan>();
		public virtual IList<int> BestPath { get; protected set; }

		public virtual int BestResult { get; protected set; } = MaxValue;

		public virtual TimeSpan MaxSolvingTime { get; protected set; } = TimeSpan.MinValue;

		public virtual int MeanReasult { get; protected set; }

		public virtual TimeSpan MeanSolvingTime { get; protected set; }

		public virtual TimeSpan MinSolvingTime { get; protected set; } = TimeSpan.MaxValue;

		public virtual IList<int> Results { get; } = new List<int>();

		public virtual int WorstResult { get; protected set; } = MinValue;

		protected SolverBase(IGraph completeGraph)
		{
			_completeGraph = completeGraph;
		}

		public virtual void Solve(IAlgorithm tspSolvingAlgorithm) => Solve(tspSolvingAlgorithm, new NullPathAccumulator());

		public virtual void Solve(IAlgorithm tspSolvingAlgorithm, int startNode) => Solve(tspSolvingAlgorithm, new NullPathAccumulator(), startNode);

		public abstract void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator);

		public abstract void Solve(IAlgorithm tspSolvingAlgorithm, IPathAccumulator pathAccumulator, int startNode);


		protected virtual void UpdateResults(int localResult, IList<int> path, TimeSpan localTime)
		{
			UpdatePathResults(localResult, path);
			UpdateTimeMeasures(localTime);

		}
		protected virtual void UpdatePathResults(int localResult, IList<int> path)
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

		protected virtual void UpdateTimeMeasures(TimeSpan localTime)
		{
			if (localTime < MinSolvingTime)
				MinSolvingTime = localTime;

			if (localTime > MaxSolvingTime)
				MaxSolvingTime = localTime;

			TimeResults.Add(localTime);
			MeanSolvingTime = new TimeSpan((long)TimeResults.Average(span => span.Ticks));
		}

		protected class SolvingTimeContext : IDisposable
		{
			private readonly Stopwatch _stopwatch;
			public TimeSpan Elapsed => _stopwatch.Elapsed;

			public static SolvingTimeContext Instance => new SolvingTimeContext(new Stopwatch());

			private SolvingTimeContext(Stopwatch stopwatch)
			{
				_stopwatch = stopwatch;
				_stopwatch.Start();
			}

			public void Dispose()
			{
				_stopwatch.Stop();
			}
		}
	}
}