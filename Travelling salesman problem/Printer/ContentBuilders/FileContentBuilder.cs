using ConsoleApplication.Solver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication.Printer.ContentBuilders
{
    public class FileContentBuilder : IContentBuilder
    {
        private readonly FileInfo _coordinateFile;
        private readonly ISolver _solver;

        public FileContentBuilder(ISolver solver, string coordinateFilePath)
        {
            _solver = solver;
            _coordinateFile = new FileInfo(coordinateFilePath);

            if (!_coordinateFile.Exists)
                throw new FileLoadException("Couldn't find file with nodes coordinates!." +
                                            $"\nGiven path: {coordinateFilePath}");
        }

        public string BuildContent(string title)
        {
            var coordinates = ReadCoordinates();

            var builder = new StringBuilder();
            builder.AppendLine("".PadRight(30, '*'));
            builder.AppendLine(title + $" Date: {DateTime.Now}");
            builder.AppendLine($"Min cost: {_solver.BestResult}, Mean cost: {_solver.MeanReasult}," +
                               $" Max cost: {_solver.WorstResult}");
            builder.AppendLine($"Elements in path: {_solver.BestPath.Count()}");
            builder.AppendLine("Path:");
            foreach (var node in _solver.BestPath)
                builder.AppendLine($"{node} {coordinates[node + 1]}");
            builder.AppendLine("".PadRight(30, '*'));
            return builder.ToString();
        }

        private IDictionary<int, string> ReadCoordinates()
        {
            var result = new Dictionary<int, string>();
            using (var stream = _coordinateFile.OpenRead())
            using (var reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    if (!char.IsDigit(line.First()))
                    {
                        line = reader.ReadLine();
                        continue;
                    }
                    if (line == "EOF")
                        break;
                    var splited = line.Split(' ');
                    result[int.Parse(splited[0])] = $"{splited[1]} {splited[2]}";
                    line = reader.ReadLine();
                }
            }
            return result;
        }
    }
}