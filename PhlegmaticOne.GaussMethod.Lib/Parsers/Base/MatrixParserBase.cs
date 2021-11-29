using System.Collections;
using System.Text.RegularExpressions;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;

public abstract class MatrixParserBase
{
    public abstract Regex CoefficientWithVariablePattern { get; }
    public virtual bool IsCoefficientWithVariableFit(string coefficientWithVariableRepresentation) =>
        CoefficientWithVariablePattern.IsMatch(coefficientWithVariableRepresentation);
    public abstract IEnumerable TryParse(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);
}