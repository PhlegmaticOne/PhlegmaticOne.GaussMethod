using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.GaussMethod.Lib.Algorithms;
using PhlegmaticOne.GaussMethod.Lib.MatrixGetters;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers.Tests;

[TestClass()]
public class OnlyCoefficientsParserTests
{
    [TestMethod()]
    public void ParseTest()
    {
        var dirPath = $@"..\..\..\..\";
        var fileName = "OneUnknownVariable5x5.txt";
        var matrixGetter = new FileMatrixGetter(dirPath, fileName);
        var matrixRepresentation = matrixGetter.GetMatrixRepresentation();

        var variableNames =
            MatrixParser.TryParse<string>(matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);

        var algorithm = new GaussParallelAlgorithm(extendedSystemMatrix);
        algorithm.OnSolved += (sender, args) => Console.WriteLine(args.AnswersVector.Length); 
        var answersVector = algorithm.Solve(variableNames);

        Assert.IsNotNull(answersVector);
    }
}