using PhlegmaticOne.GaussMethod.Lib.Events;
using PhlegmaticOne.GaussMethod.Lib.Models;
using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public abstract class GaussAlgorithm
{
    protected ExtendedSystemMatrix ExtendedSystemMatrix;
    public virtual event EventHandler<CommonSystemSolvedEventArgs> OnSolved; 
    protected GaussAlgorithm(ExtendedSystemMatrix extendedSystemMatrix)
    {
        ExtendedSystemMatrix = extendedSystemMatrix ??
                               throw new ArgumentNullException(nameof(extendedSystemMatrix));
        InitialMatrix = extendedSystemMatrix.Matrix;
    }

    protected virtual void OnSystemSolved(double[] answersVector)
    { 
        OnSolved?.Invoke(this, new CommonSystemSolvedEventArgs(InitialMatrix, answersVector));
    }
    public double[,] InitialMatrix { get; }
    public abstract double[] Solve();
    public abstract AnswersVectorBase<T> Solve<T>(IEnumerable<T> variableNames) where T : notnull;
    public override string ToString() => GetType().Name;
}