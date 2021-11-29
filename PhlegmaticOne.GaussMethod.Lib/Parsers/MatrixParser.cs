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

    public void AddNewParser(MatrixParserBase matrixParser) => _parsers.Append(matrixParser);
}