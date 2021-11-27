using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public class GaussJordanAlgorithm : GaussBothRunAlgorithmBase
{
    public GaussJordanAlgorithm(ExtendedSystemMatrix extendedSystemMatrix) : base(extendedSystemMatrix) { }

    protected override Action<int> RowIteratingStraightAction => (startRowIndex) =>
    {
        for (int rowAfter = startRowIndex + 1; rowAfter < ExtendedSystemMatrix.RowCount; ++rowAfter)
        {
            ExtendedSystemMatrix.SubtractRowMultipliedBy(rowAfter, startRowIndex, ExtendedSystemMatrix[rowAfter, startRowIndex]);
        }
    };

    protected override Action<int> RowIteratingReverseAction => (startRowIndex) =>
    {
        for (int rowBefore = startRowIndex - 1; rowBefore >= 0; --rowBefore)
        {
            ExtendedSystemMatrix.SubtractRowMultipliedBy(rowBefore, startRowIndex,
                                                         ExtendedSystemMatrix[rowBefore, startRowIndex]);
        }
    };
}