using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public class GaussParallelAlgorithm : GaussBothRunAlgorithmBase
{
    public GaussParallelAlgorithm(ExtendedSystemMatrix extendedSystemMatrix) : base(extendedSystemMatrix) { }

    protected override Action<int> RowIteratingStraightAction => startRowIndex =>
    {
        Parallel.For(startRowIndex + 1, ExtendedSystemMatrix.RowCount, i =>
        {
            ExtendedSystemMatrix.SubtractRowMultipliedBy(i, startRowIndex, ExtendedSystemMatrix[i, startRowIndex]);
        });
    };

    protected override Action<int> RowIteratingReverseAction => startRowIndex =>
    {
        Parallel.For(0, startRowIndex, i =>
        {
            ExtendedSystemMatrix.SubtractRowMultipliedBy(i, startRowIndex, ExtendedSystemMatrix[i, startRowIndex]);
        });
    };
}