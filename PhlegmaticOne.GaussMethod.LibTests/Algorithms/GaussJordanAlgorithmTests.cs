using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.GaussMethod.Lib.Models;
using System.Collections.Generic;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms.Tests;

[TestClass()]
public class GaussJordanAlgorithmTests
{
    private static double[,] _matrix => new double[,]
    {
        {2, 1, -1, 8},
        {-3, -1, 2, -11},
        {-2, 1, 2, -3}
    };

    [TestMethod()]
    public void SolveTest()
    {
        var extendedSystemMatrix = new ExtendedSystemMatrix(_matrix);
        var algorithm = new GaussJordanAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(new List<string>() { "x1", "x2", "x3" });
        Assert.AreEqual(2, answersVector["x1"]);
        Assert.AreEqual(3, answersVector["x2"]);
        Assert.AreEqual(-1, answersVector["x3"]);
    }
    [TestMethod()]
    public void SolveParallelTest()
    {
        var extendedSystemMatrix = new ExtendedSystemMatrix(_matrix);
        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(new List<string>() { "x1", "x2", "x3" });
        Assert.AreEqual(2, answersVector["x1"]);
        Assert.AreEqual(3, answersVector["x2"]);
        Assert.AreEqual(-1, answersVector["x3"]);
    }
    [TestMethod()]
    public void SolveTestWithEvent()
    {
        var extendedSystemMatrix = new ExtendedSystemMatrix(_matrix);
        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        algorithm.OnSolved += (sender, e) =>
        {
            var answersVector = e.AnswersVector;
            var expected = _matrix;
            Assert.AreEqual(2, answersVector[0]);
            Assert.AreEqual(3, answersVector[1]);
            Assert.AreEqual(-1, answersVector[2]);
            for (int i = 0; i < e.InitialMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < e.InitialMatrix.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], e.InitialMatrix[i, j]);
                }
            }
            Assert.IsInstanceOfType(sender, typeof(GaussParallelAlgorithm));
        };
        algorithm.Solve();
    }
}