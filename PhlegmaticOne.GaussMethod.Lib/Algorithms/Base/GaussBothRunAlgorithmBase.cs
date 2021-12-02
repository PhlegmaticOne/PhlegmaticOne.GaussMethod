using PhlegmaticOne.GaussMethod.Lib.Models;
using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;
/// <summary>
/// Represents base class for Gauss algorithms variations where calculations run in both directions 
/// </summary>
public abstract class GaussBothRunAlgorithmBase : GaussAlgorithm
{
    /// <summary>
    /// Initializes new GaussBothRunAlgorithmBase instance
    /// </summary>
    /// <param name="extendedSystemMatrix">Specified extended matrix system</param>
    protected GaussBothRunAlgorithmBase(ExtendedSystemMatrix extendedSystemMatrix) : base(extendedSystemMatrix) { }
    public override double[] Solve()
    {
        var result = StraightRun().ReverseRun().ExtendedSystemMatrix.MainDiagonal();
        OnSystemSolved(result);
        return result;
    }
    public override AnswersVectorBase<T> Solve<T>(IEnumerable<T> variableNames) => new AnswersVector<T>(Solve(), variableNames);
    /// <summary>
    /// Action which mutates rows of matrix when straight run executing
    /// </summary>
    protected abstract Action<int> RowIteratingStraightAction { get; }
    /// <summary>
    /// Action which mutates rows of matrix when reverse run executing
    /// </summary>
    protected abstract Action<int> RowIteratingReverseAction { get; }
    /// <summary>
    /// Straight run in algorithm
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Reverse run in algorithm
    /// </summary>
    /// <returns></returns>
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