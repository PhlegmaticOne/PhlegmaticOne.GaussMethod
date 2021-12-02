using PhlegmaticOne.GaussMethod.Lib.Events;
using PhlegmaticOne.GaussMethod.Lib.Models;
using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Algorithms;
/// <summary>
/// Base class for gauss algorithms implementations 
/// </summary>
public abstract class GaussAlgorithm
{
    protected ExtendedSystemMatrix ExtendedSystemMatrix;
    /// <summary>
    /// Event when system is solved
    /// </summary>
    public virtual event EventHandler<CommonSystemSolvedEventArgs> OnSolved;
    /// <summary>
    /// Initializes new GaussAlgorithm instance
    /// </summary>
    /// <param name="extendedSystemMatrix">Specified extended matrix system</param>
    /// <exception cref="ArgumentNullException">System is null</exception>
    protected GaussAlgorithm(ExtendedSystemMatrix extendedSystemMatrix)
    {
        ExtendedSystemMatrix = extendedSystemMatrix ??
                               throw new ArgumentNullException(nameof(extendedSystemMatrix));
        InitialMatrix = extendedSystemMatrix.Matrix;
    }
    /// <summary>
    /// Invokes event when system was resolved
    /// </summary>
    protected virtual void OnSystemSolved(double[] answersVector) => OnSolved?.Invoke(this, new CommonSystemSolvedEventArgs(InitialMatrix, answersVector));
    /// <summary>
    /// Initial system matrix
    /// </summary>
    public double[,] InitialMatrix { get; }
    /// <summary>
    /// Solves system in specified way
    /// </summary>
    /// <returns></returns>
    public abstract double[] Solve();
    /// <summary>
    /// Solves system in specified way
    /// </summary>
    /// <typeparam name="T">Type in which answers variables will be</typeparam>
    /// <param name="variableNames">Variable names</param>
    /// <returns>Vector of answers with named variables</returns>
    public abstract AnswersVectorBase<T> Solve<T>(IEnumerable<T> variableNames) where T : notnull;
    public override string ToString() => GetType().Name;
}