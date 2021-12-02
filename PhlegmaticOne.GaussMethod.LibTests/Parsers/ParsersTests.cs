using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.MatrixGetters;
using PhlegmaticOne.GaussMethod.Lib.Models;
using System;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers.Tests;

[TestClass()]
public class ParsersTests
{
    [TestMethod()]
    public void OnlyCoefficientsParsingTest()
    {
        var dirPath = $@"..\..\..\..\";
        var fileName = "OnlyCoefficients5x5.txt";
        var matrixGetter = new FileMatrixGetter(dirPath, fileName);
        var matrixRepresentation = matrixGetter.GetMatrixRepresentation();

        var variableNames = MatrixParser.TryParse<int>(matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);

        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(variableNames);

        Assert.IsNotNull(answersVector);
        Assert.IsTrue(answersVector[0] != 0);
    }
    [TestMethod()]
    public void LettersParsingTest()
    {
        var dirPath = $@"..\..\..\..\";
        var fileName = "Letters5x5.txt";
        var matrixGetter = new FileMatrixGetter(dirPath, fileName);
        var matrixRepresentation = matrixGetter.GetMatrixRepresentation();

        var variableNames = MatrixParser.TryParse<char>(matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);

        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(variableNames);

        Assert.IsNotNull(answersVector);
        Assert.IsTrue(answersVector['a'] != 0);
    }
    [TestMethod()]
    public void OneUnknownVariableParsingTest()
    {
        var dirPath = $@"..\..\..\..\";
        var fileName = "OneUnknownVariable5x5.txt";
        var matrixGetter = new FileMatrixGetter(dirPath, fileName);
        var matrixRepresentation = matrixGetter.GetMatrixRepresentation();

        var variableNames = MatrixParser.TryParse<string>(matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);

        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        var answersVector = algorithm.Solve(variableNames);

        Assert.IsNotNull(answersVector);
        Assert.IsTrue(answersVector["y0"] != 0);
    }
}