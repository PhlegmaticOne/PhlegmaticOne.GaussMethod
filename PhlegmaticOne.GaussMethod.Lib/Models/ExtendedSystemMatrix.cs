using System.Text;

namespace PhlegmaticOne.GaussMethod.Lib.Models;

public class ExtendedSystemMatrix : IEquatable<ExtendedSystemMatrix>
{
    private readonly double[,] _coefficientMatrix;
    public ExtendedSystemMatrix(double[,] coefficientMatrix) => _coefficientMatrix = coefficientMatrix;
    public int RowCount => _coefficientMatrix.GetLength(0);
    public int ColumnCount => _coefficientMatrix.GetLength(1);
    public double this[int row, int column] => _coefficientMatrix[row, column];
    public void MultiplyRow(int rowNumber, double number)
    {
        if (rowNumber < 0 || rowNumber > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");
        for (int i = 0; i < ColumnCount; ++i) _coefficientMatrix[rowNumber, i] *= number;
    }

    public void SwapRows(int rowNumberFirst, int rowNumberSecond)
    {
        if (rowNumberFirst < 0 || rowNumberFirst > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");
        if (rowNumberSecond < 0 || rowNumberSecond > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");

        if (rowNumberFirst == rowNumberSecond)
            return;

        var buffer = new double[ColumnCount];
        for (int i = 0; i < ColumnCount; i++) buffer[i] = _coefficientMatrix[rowNumberFirst, i];

        for (int i = 0; i < ColumnCount; i++)
        {
            _coefficientMatrix[rowNumberFirst, i] = _coefficientMatrix[rowNumberSecond, i];
            _coefficientMatrix[rowNumberSecond, i] = buffer[i];
        }
    }

    public int GetLeadingRowIndex(int startRowIndexToSearch, int columnPosition)
    {
        if (columnPosition < 0 || columnPosition > ColumnCount)
            throw new InvalidOperationException(
                "Column number cannot be less than zero or more than system matrix column count");
        if (startRowIndexToSearch < 0 || startRowIndexToSearch > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");

        var resultIndex = startRowIndexToSearch;
        var tempMaxElement = _coefficientMatrix[startRowIndexToSearch, columnPosition];
        for (int i = startRowIndexToSearch + 1; i < RowCount; i++)
        {
            if (_coefficientMatrix[i, columnPosition] > tempMaxElement)
            {
                tempMaxElement = _coefficientMatrix[i, columnPosition];
                resultIndex = i;
            }
        }

        return resultIndex;
    }

    public void SubtractRowMultipliedBy(int rowNumberToSubtract, int rowNumberToBeSubtracted, double multipleCoefficient)
    {
        if (rowNumberToSubtract < 0 || rowNumberToSubtract > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");
        if (rowNumberToBeSubtracted < 0 || rowNumberToBeSubtracted > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");
        for (int i = 0; i < ColumnCount; i++)
            _coefficientMatrix[rowNumberToSubtract, i] -=
                _coefficientMatrix[rowNumberToBeSubtracted, i] * multipleCoefficient;
    }

    public double[] LastColumn()
    {
        var result = new double[RowCount];
        for (int i = 0; i < RowCount; ++i) result[i] = _coefficientMatrix[i, RowCount];
        return result;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var cols = ColumnCount;
        var rows = RowCount;
        for (int i = 0; i < rows; i++)
        {
            sb.Append("{");
            for (int j = 0; j < cols; j++)
            {
                sb.Append(j == cols - 1 ? $"{_coefficientMatrix[i, j]} " : $"{_coefficientMatrix[i, j]}, ");
            }
            sb.AppendLine("}");
        }
        return sb.ToString();
    }

    public bool Equals(ExtendedSystemMatrix? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (Math.Abs(_coefficientMatrix[i, j] - other._coefficientMatrix[i, j]) > double.Epsilon)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ExtendedSystemMatrix) obj);
    }

    public override int GetHashCode() => _coefficientMatrix.GetHashCode();
}