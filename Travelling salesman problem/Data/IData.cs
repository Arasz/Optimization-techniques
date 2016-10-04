using ConsoleApplication.Common;
using System.Collections.Generic;

namespace ConsoleApplication.Data
{
    /// <summary>
    /// Data for TSP 
    /// </summary>
    public interface IData
    {
        IEnumerable<Point> Positions { get; }
    }
}