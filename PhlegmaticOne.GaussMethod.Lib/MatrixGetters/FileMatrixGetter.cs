namespace PhlegmaticOne.GaussMethod.Lib.MatrixGetters;

public class FileMatrixGetter : IMatrixGetter
{
    private readonly FileInfo _fileInfo;
    /// <summary>
    /// Initializes new FileMatrixGetter instance
    /// </summary>
    /// <param name="directoryPath">specified directory path</param>
    /// <param name="fileName">Specified file name</param>
    public FileMatrixGetter(string directoryPath, string fileName) => _fileInfo = new DirectoryInfo(directoryPath).GetFiles().First(f => f.Name.Contains(fileName));
    /// <summary>
    /// Gets matrix representation from file
    /// </summary>
    public IEnumerable<string> GetMatrixRepresentation() => File.ReadAllLines(_fileInfo.FullName);
}