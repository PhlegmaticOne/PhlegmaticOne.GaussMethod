namespace PhlegmaticOne.GaussMethod.Lib.Events;
/// <summary>
/// Event arguments for SystemSolved event
/// </summary>
public class CommonSystemSolvedEventArgs : EventArgs
{
    /// <summary>
    /// Initial matrix of algorithm
    /// </summary>
    public double[,] InitialMatrix { get; }
    /// <summary>
    /// Answers got from algorithm
    /// </summary>
    public double[] AnswersVector { get; }

    /// <summary>
    /// Initializes new CommonSystemSolvedEventArgs
    /// </summary>
    /// <param name="initialMatrix">Specified initial matrix</param>
    /// <param name="answersVector">Specified answers vector</param>
    public CommonSystemSolvedEventArgs(double[,] initialMatrix, double[] answersVector) =>
        (InitialMatrix, AnswersVector) = (initialMatrix, answersVector);
    public override string ToString() => $"{GetType().Name}. Answers: {AnswersVector.Length}";
}