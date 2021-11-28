using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms.Tests;

[TestClass()]
public class GaussJordanAlgorithmTests
{
    [TestMethod()]
    public void SolveTest()
    {
        var matrix = new double[3, 4]
        {
            {2, 1, -1, 8},
            {-3, -1, 2, -11},
            {-2, 1, 2, -3}
        };
        var extendedSystemMatrix = new ExtendedSystemMatrix(matrix);
        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(new List<string>(){"x1", "x2", "x3"});
        Assert.AreEqual(2, answersVector["x1"]);
        Assert.AreEqual(3, answersVector["x2"]);
        Assert.AreEqual(-1, answersVector["x3"]);
    }
}