using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public abstract class GaussBothRunAlgorithmBase : GaussAlgorithm
{
    protected GaussBothRunAlgorithmBase(ExtendedSystemMatrix extendedSystemMatrix) : base(extendedSystemMatrix) { }
    public override AnswersVector Solve()
    {
        StraightRun();
        ReverseRun();
        return new AnswersVector(ExtendedSystemMatrix.LastColumn());
    }
    protected abstract Action<int> RowIteratingStraightAction { get; }
    protected abstract Action<int> RowIteratingReverseAction { get; }
    protected virtual void StraightRun()
    {
        for (int row = 0; row < ExtendedSystemMatrix.RowCount; row++)
        {
            var leadingRowIndex = ExtendedSystemMatrix.GetLeadingRowIndex(row, row);
            ExtendedSystemMatrix.SwapRows(row, leadingRowIndex);
            ExtendedSystemMatrix.MultiplyRow(row, 1 / ExtendedSystemMatrix[row, row]);
            RowIteratingStraightAction.Invoke(row);
        }
    }
    protected virtual void ReverseRun()
    {
        for (int row = ExtendedSystemMatrix.RowCount - 1; row >= 0; --row)
        {
            ExtendedSystemMatrix.MultiplyRow(row, 1 / ExtendedSystemMatrix[row, row]);
            RowIteratingReverseAction.Invoke(row);
        }
    }
}