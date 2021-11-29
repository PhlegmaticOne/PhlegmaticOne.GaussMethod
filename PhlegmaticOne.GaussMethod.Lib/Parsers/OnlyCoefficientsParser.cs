using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;

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