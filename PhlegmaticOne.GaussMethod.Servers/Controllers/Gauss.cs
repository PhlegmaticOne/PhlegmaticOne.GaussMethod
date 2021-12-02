using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Common.Controllers;
/// <summary>
/// Controller for solving system by Gauss methods with received data
/// </summary>
public class Gauss
{
    /// <summary>
    /// Solves system linear
    /// </summary>
    /// <param name="data">Received in request body data</param>
    /// <returns>Answers vector of system</returns>
    public double[] Solve(string data)
    {
        var matrix = JsonConvert.DeserializeObject<double[,]>(data);
        if (matrix is not null)
        {
            var extended = new ExtendedSystemMatrix(matrix);
            var gauss = new GaussJordanAlgorithm(extended);
            return gauss.Solve();
        }

        return Array.Empty<double>();
    }
    /// <summary>
    /// Solves system parallel
    /// </summary>
    /// <param name="data">Received in request body data</param>
    /// <returns>Answers vector of system</returns>
    public double[] SolveParallel(string data)
    {
        var matrix = JsonConvert.DeserializeObject<double[,]>(data);
        if (matrix is not null)
        {
            var extended = new ExtendedSystemMatrix(matrix);
            var gauss = new GaussParallelAlgorithm(extended);
            return gauss.Solve();
        }

        return Array.Empty<double>();
    }
}