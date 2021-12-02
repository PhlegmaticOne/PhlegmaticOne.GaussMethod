using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;
/// <summary>
/// Represents instance which resolves system with Gauss and Jordan linear algorithm
/// </summary>
public class GaussJordanAlgorithm : GaussBothRunAlgorithmBase
{
    /// <summary>
    /// Initializes new GaussJordanAlgorithm instance
    /// </summary>
    /// <param name="extendedSystemMatrix">Specified extended matrix system</param>
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