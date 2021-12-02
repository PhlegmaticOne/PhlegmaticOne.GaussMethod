namespace PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;
/// <summary>
/// Represents base instance for accessing to algorithm answers in specified way
/// </summary>
/// <typeparam name="T">Type of variables names</typeparam>
public abstract class AnswersVectorBase<T> where T : notnull
{
    /// <summary>
    /// Accessing to algorithm answers in specified way
    /// </summary>
    /// <param name="key">Variable name</param>
    /// <returns>Answer of system</returns>
    public abstract double this[T key] { get; }
}