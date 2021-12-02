namespace PhlegmaticOne.GaussMethod.Lib.MatrixGetters;
/// <summary>
/// Contract for getting matrix representation from different sources
/// </summary>
public interface IMatrixGetter
{
    /// <summary>
    /// Gets matrix representation from specified source
    /// </summary>
    IEnumerable<string> GetMatrixRepresentation();
}