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
        var result = algorithm.Solve();
        Assert.AreEqual(2, result[0]);
        Assert.AreEqual(3, result[1]);
        Assert.AreEqual(-1, result[2]);
    }
}