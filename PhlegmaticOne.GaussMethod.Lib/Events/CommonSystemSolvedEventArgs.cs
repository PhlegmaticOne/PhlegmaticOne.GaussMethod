using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Events;

public class CommonSystemSolvedEventArgs : EventArgs
{
    public double[,] InitialMatrix { get; }
    public double[] AnswersVector { get; }

    public CommonSystemSolvedEventArgs(double[,] initialMatrix, double[] answersVector)
    {
        InitialMatrix = initialMatrix;
        AnswersVector = answersVector;
    }
}