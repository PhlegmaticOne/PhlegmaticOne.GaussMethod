using Newtonsoft.Json;
using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Common.Controllers;

public class Gauss
{
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
    public double[] SolveParallel(object data)
    {
        if (data is double[,] matrix)
        {
            var extended = new ExtendedSystemMatrix(matrix);
            var gauss = new GaussParallelAlgorithm(extended);
            return gauss.Solve();
        }
        return Array.Empty<double>();
    }
}