using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;

public interface IMatrixParser
{
    ExtendedSystemMatrix Parse(IEnumerable<string> matrixRows);
}