using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.Models;
using System.Diagnostics;

var paramssss = new List<double[]>
{
    new double[] {5, 30},
    new double[] {10, 30},
    new double[] {50, 30},
    new double[] {100, 30},
    new double[] {250, 30},
    new double[] {500, 30},
    new double[] {1000, 30},
};
foreach (var par in paramssss)
{
    var comparing = new CompareGaussAlgorithms();
    var matrix = comparing.GenerateMatrixWithRows((int)par[0], par[1]);
    var system = new ExtendedSystemMatrix(matrix);
    var times = comparing.SolveWith( new GaussJordanAlgorithm(system.Clone() as ExtendedSystemMatrix),
                                                                    new GaussParallelAlgorithm(system.Clone() as ExtendedSystemMatrix));
    foreach (var tuple in times)
    {
        Console.WriteLine("\n\n\n");
        Console.WriteLine($"Rows in matrix: {(int)par[0]}\n");
        Console.WriteLine("Algorithm: {0} - Time: {1}", tuple.Item1, tuple.Item2);
    }
}
Console.ReadLine();

internal class CompareGaussAlgorithms
{
    private static Random _random = new();
    internal double[,] GenerateMatrixWithRows(int rowsCount, double maxNumberModule)
    {
        var matrix = new double[rowsCount, rowsCount + 1];
        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < rowsCount + 1; j++)
            {
                matrix[i, j] = _random.NextDouble() * maxNumberModule;
            }
        }

        return matrix;
    }
    internal List<Tuple<string, TimeSpan>> SolveWith(params GaussAlgorithm[] algorithms)
    {
        var result = new List<Tuple<string, TimeSpan>>();
        foreach (var algorithm in algorithms)
        {
            var timer = new Stopwatch();
            timer.Start();
            algorithm.Solve();
            timer.Stop();
            result.Add(new Tuple<string, TimeSpan>(algorithm.GetType().Name, timer.Elapsed));
        }

        return result;
    }
}
