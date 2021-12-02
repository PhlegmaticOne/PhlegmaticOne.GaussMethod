using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;

public class MatrixParser
{
    private static readonly IEnumerable<MatrixParserBase> _parsers = new List<MatrixParserBase>()
    {
        new OnlyCoefficientsParser(),
        new LettersPatternParser(),
        new OneUnknownVariableParser()
    };
    /// <summary>
    /// Default matrix parser
    /// </summary>
    /// <typeparam name="T">Variable names type</typeparam>
    /// <param name="matrixRepresentation">Matrix rows representation to parse to</param>
    /// <param name="extendedSystemMatrix">Parsed matrix</param>
    /// <returns>Variable names in matrix</returns>
    /// <exception cref="ArgumentNullException">Rows of matrix is null</exception>
    /// <exception cref="InvalidOperationException">There are no fitted parser for incoming rows of matrix</exception>
    public static IEnumerable<T> TryParse<T>(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix)
    {
        if (matrixRepresentation is null)
        {
            throw new ArgumentNullException(nameof(matrixRepresentation));
        }
        var coefficientsWithVariables = matrixRepresentation.First().Split(' ');
        foreach (var parser in _parsers)
        {
            if (parser.IsCoefficientWithVariableFit(coefficientsWithVariables.First()))
            {
                var variableNames = parser.TryParse(matrixRepresentation, out ExtendedSystemMatrix result) as IEnumerable<T>;
                extendedSystemMatrix = result;
                return variableNames;
            }
        }

        throw new InvalidOperationException($"Can't convert to type {typeof(T).Name} with existed parsers");
    }
    /// <summary>
    /// Adds new parser in parsers collection of class
    /// </summary>
    /// <param name="matrixParser"></param>
    public void AddNewParser(MatrixParserBase matrixParser) => _parsers.Append(matrixParser);
}