namespace PhlegmaticOne.GaussMethod.Lib.Models;
/// <summary>
/// Represents instance for usable manipulating over 2D matrix for system solving
/// </summary>
public class ExtendedSystemMatrix : IEquatable<ExtendedSystemMatrix>, ICloneable
{
    private readonly double[,] _coefficientMatrix;
    internal double[,] Matrix { get; }
    /// <summary>
    /// Initializes new ExtendedSystemMatrix
    /// </summary>
    /// <param name="coefficientMatrix">Specified coefficients</param>
    /// <exception cref="InvalidOperationException">Rows count not equal to columns count plus 1</exception>
    public ExtendedSystemMatrix(double[,] coefficientMatrix)
    {
        if (coefficientMatrix.GetLength(0) + 1 != coefficientMatrix.GetLength(1))
        {
            throw new InvalidOperationException();
        }
        _coefficientMatrix = coefficientMatrix;
        Matrix = coefficientMatrix.Clone() as double[,];
    }
    /// <summary>
    /// Initializes new ExtendedSystemMatrix
    /// </summary>
    /// <param name="coefficientMatrix">Specified coefficients</param>
    /// <exception cref="ArgumentNullException">Coefficients is null</exception>
    /// <exception cref="InvalidOperationException">Rows count not equal to columns count plus 1</exception>
    internal ExtendedSystemMatrix(IEnumerable<IEnumerable<double>> coefficientMatrix)
    {
        if (coefficientMatrix is null) throw new ArgumentNullException(nameof(coefficientMatrix));
        var rows = coefficientMatrix.Count();
        var cols = coefficientMatrix.First().Count();
        if (rows + 1 != cols) throw new InvalidOperationException();

        _coefficientMatrix = new double[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                _coefficientMatrix[i, j] = coefficientMatrix.ElementAt(i).ElementAt(j);
            }
        }
        Matrix = _coefficientMatrix.Clone() as double[,];
    }
    /// <summary>
    /// Row count in matrix
    /// </summary>
    public int RowCount => _coefficientMatrix.GetLength(0);
    /// <summary>
    /// Column count in matrix
    /// </summary>
    public int ColumnCount => _coefficientMatrix.GetLength(1);
    /// <summary>
    /// Accessing to elements in matrix
    /// </summary>
    public double this[int row, int column] => _coefficientMatrix[row, column];
    /// <summary>
    /// Multiplies row on a specified number
    /// </summary>
    /// <param name="rowNumber">Row number in matrix</param>
    /// <param name="number">Specified number</param>
    /// <exception cref="InvalidOperationException">Row number <0 or >=RowCount</exception>
    public void MultiplyRow(int rowNumber, double number)
    {
        if (rowNumber < 0 || rowNumber > RowCount)
            throw new InvalidOperationException(
                "Row number cannot be less than zero or more than system matrix row count");

        for (int i = 0; i < ColumnCount; ++i) _coefficientMatrix[rowNumber, i] *= number;
    }
    /// <summary>
    /// Swaps two rows in matrix
    /// </summary>
    /// <param name="rowNumberFirst">First row to swap number</param>
    /// <param name="rowNumberSecond">Second row to swap number</param>
    /// <exception cref="InvalidOperationException">Row number <0 or >=RowCount</exception>
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
    /// <summary>
    /// Gets leading row in matrix
    /// </summary>
    /// <param name="startRowIndexToSearch">Start wor number from which search beginning</param>
    /// <param name="columnPosition">Column number where maximal element is searching</param>
    /// <returns>Index of leading row</returns>
    /// <exception cref="InvalidOperationException">Column number <0 or >=ColumnCount, row number <0 or >=RowCount</exception>
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
    /// <summary>
    /// Subtracts row multiplied by a specified number from different wor
    /// </summary>
    /// <param name="rowNumberToSubtract">Row number from which subtract is executing</param>
    /// <param name="rowNumberToBeSubtracted">Row number from which multiplies on a specified number</param>
    /// <param name="multipleCoefficient">Specified number to multiply row</param>
    /// <exception cref="InvalidOperationException">Row numbers <0 or >=RowCount</exception>
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
    /// <summary>
    /// Retrieves elements in matrix from main diagonal into a new array
    /// </summary>
    public double[] MainDiagonal()
    {
        var result = new double[RowCount];
        for (int i = 0; i < RowCount; ++i) result[i] = _coefficientMatrix[i, RowCount];
        return result;
    }
    public override string ToString() => $"Variables: {RowCount}";
    public object Clone() => new ExtendedSystemMatrix(_coefficientMatrix);
    public bool Equals(ExtendedSystemMatrix? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (Math.Abs(_coefficientMatrix[i, j] - other._coefficientMatrix[i, j]) > 100 * double.Epsilon)
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
        return Equals(obj as ExtendedSystemMatrix);
    }
    public override int GetHashCode() => _coefficientMatrix.GetHashCode();
}