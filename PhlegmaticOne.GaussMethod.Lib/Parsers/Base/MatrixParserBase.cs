using PhlegmaticOne.GaussMethod.Lib.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;
/// <summary>
/// Represents base parser for matrix parsers
/// </summary>
public abstract class MatrixParserBase
{
    /// <summary>
    /// Regex which identifies how one system element look like
    /// </summary>
    public abstract Regex CoefficientWithVariablePattern { get; }
    /// <summary>
    /// Check if element is fits to specified regex
    /// </summary>
    /// <param name="coefficientWithVariableRepresentation"></param>
    /// <returns></returns>
    public virtual bool IsCoefficientWithVariableFit(string coefficientWithVariableRepresentation) =>
        CoefficientWithVariablePattern.IsMatch(coefficientWithVariableRepresentation);
    /// <summary>
    /// Parses matrix representation into a matrix instance
    /// </summary>
    /// <param name="matrixRepresentation">Rows of matrix to parse in matrix instance</param>
    /// <param name="extendedSystemMatrix">Parsed matrix</param>
    /// <returns>Variable names in matrix</returns>
    public abstract IEnumerable TryParse(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix);
}