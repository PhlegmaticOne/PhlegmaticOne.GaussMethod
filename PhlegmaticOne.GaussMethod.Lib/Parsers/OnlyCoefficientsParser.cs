using PhlegmaticOne.GaussMethod.Lib.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;
/// <summary>
/// Represents instance which parses matrix from elements like 4, -5, 2.233 etc
/// </summary>
public class OnlyCoefficientsParser : MatrixParserBase
{
    public override Regex CoefficientWithVariablePattern => new(@"^-?(\d)+$");
    public override IEnumerable TryParse(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix)
    {
        var matrixElements = matrixRepresentation.Select(s => s.Split(' ').Select(x => Convert.ToDouble(x)));
        extendedSystemMatrix = new ExtendedSystemMatrix(matrixElements);
        return Enumerable.Range(0, matrixElements.Count());
    }
}