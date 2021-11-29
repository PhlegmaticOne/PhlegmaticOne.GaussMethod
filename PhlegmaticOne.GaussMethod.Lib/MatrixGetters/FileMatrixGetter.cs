namespace PhlegmaticOne.GaussMethod.Lib.MatrixGetters;

public class FileMatrixGetter : IMatrixGetter
{
    private readonly FileInfo _fileInfo;
    public FileMatrixGetter(string directoryPath, string fileName)
    {
        _fileInfo = new DirectoryInfo(directoryPath).GetFiles().First(f => f.Name.Contains(fileName));
    }
    public IEnumerable<string> GetMatrixRepresentation() => File.ReadAllLines(_fileInfo.FullName);
}