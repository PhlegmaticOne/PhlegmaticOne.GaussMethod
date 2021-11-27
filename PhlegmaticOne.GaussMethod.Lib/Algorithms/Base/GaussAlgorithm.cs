using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;

public abstract class GaussAlgorithm
{
    protected ExtendedSystemMatrix ExtendedSystemMatrix;
    protected GaussAlgorithm(ExtendedSystemMatrix extendedSystemMatrix) => ExtendedSystemMatrix = extendedSystemMatrix ?? 
                                                                            throw new ArgumentNullException(nameof(extendedSystemMatrix));
    public abstract AnswersVector Solve();
    public override string ToString() => GetType().Name;
}