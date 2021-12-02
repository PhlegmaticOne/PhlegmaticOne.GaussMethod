using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;
/// <summary>
/// Represents instance which resolves system with Gauss and Jordan parallel algorithm
/// </summary>
public class GaussParallelAlgorithm : GaussBothRunAlgorithmBase
{
    /// <summary>
    /// Initializes new GaussParallelAlgorithm instance
    /// </summary>
    /// <param name="extendedSystemMatrix">Specified extended matrix system</param>
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