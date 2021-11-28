using PhlegmaticOne.GaussMethod.Lib.Models;
using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public abstract class GaussBothRunAlgorithmBase : GaussAlgorithm
{
    protected GaussBothRunAlgorithmBase(ExtendedSystemMatrix extendedSystemMatrix) : base(extendedSystemMatrix) { }
    public override double[] Solve() => StraightRun().ReverseRun().ExtendedSystemMatrix.LastColumn();
    public override AnswersVectorBase<T> Solve<T>(IEnumerable<T> variableNames) => new AnswersVector<T>(Solve(), variableNames);
    protected abstract Action<int> RowIteratingStraightAction { get; }
    protected abstract Action<int> RowIteratingReverseAction { get; }
    protected virtual GaussBothRunAlgorithmBase StraightRun()
    {
        for (int row = 0; row < ExtendedSystemMatrix.RowCount; row++)
        {
            var leadingRowIndex = ExtendedSystemMatrix.GetLeadingRowIndex(row, row);
            ExtendedSystemMatrix.SwapRows(row, leadingRowIndex);
            ExtendedSystemMatrix.MultiplyRow(row, 1 / ExtendedSystemMatrix[row, row]);
            RowIteratingStraightAction.Invoke(row);
        }

        return this;
    }
    protected virtual GaussBothRunAlgorithmBase ReverseRun()
    {
        for (int row = ExtendedSystemMatrix.RowCount - 1; row >= 0; --row)
        {
            ExtendedSystemMatrix.MultiplyRow(row, 1 / ExtendedSystemMatrix[row, row]);
            RowIteratingReverseAction.Invoke(row);
        }

        return this;
    }
}