using PhlegmaticOne.GaussMethod.Lib.Models;
using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public abstract class GaussAlgorithm
{
    protected ExtendedSystemMatrix ExtendedSystemMatrix;
    protected GaussAlgorithm(ExtendedSystemMatrix extendedSystemMatrix)
    {
        ExtendedSystemMatrix = extendedSystemMatrix ??
                               throw new ArgumentNullException(nameof(extendedSystemMatrix));
        InitialMatrix = extendedSystemMatrix.Clone() as ExtendedSystemMatrix;
    }

    public ExtendedSystemMatrix? InitialMatrix { get; }
    public abstract double[] Solve();
    public abstract AnswersVectorBase<T> Solve<T>(IEnumerable<T> variableNames) where T : notnull;
    public override string ToString() => GetType().Name;
}