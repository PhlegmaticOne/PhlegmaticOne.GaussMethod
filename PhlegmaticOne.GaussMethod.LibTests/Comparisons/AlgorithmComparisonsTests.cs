using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.LibTests.Comparisons;

[TestClass]
public class AlgorithmComparisonsTests
{
    [DataTestMethod]
    [DataRow(5, 30)]
    [DataRow(10, 30)]
    [DataRow(50, 30)]
    [DataRow(100, 30)]
    [DataRow(250, 30)]
    [DataRow(500, 30)]
    [DataRow(1000, 30)]
    public void ValidationTests(int rowsCount, double maxNumberModule)
    {
        var comparing = new CompareGaussAlgorithms();
        var system = new ExtendedSystemMatrix(comparing.GenerateMatrixWithRows(rowsCount, maxNumberModule));
        var linearAnswers = comparing.SolveLinear(system.Clone() as ExtendedSystemMatrix);
        var parallelAnswers = comparing.SolveParallel(system.Clone() as ExtendedSystemMatrix);
        for (int i = 0; i < linearAnswers.Length; i++)
        {
            Assert.AreEqual(linearAnswers[i], parallelAnswers[i], 0.0001);
        }
    }
}
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

    internal double[] SolveLinear(ExtendedSystemMatrix extendedSystemMatrix) => new GaussJordanAlgorithm(extendedSystemMatrix).Solve();
    internal double[] SolveParallel(ExtendedSystemMatrix extendedSystemMatrix) => new GaussParallelAlgorithm(extendedSystemMatrix).Solve();
}