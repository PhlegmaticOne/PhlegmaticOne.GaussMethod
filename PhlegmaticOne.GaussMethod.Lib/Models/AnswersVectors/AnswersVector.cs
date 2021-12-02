using PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

namespace PhlegmaticOne.GaussMethod.Lib.Models;
/// <summary>
/// Represents default implementation for answers vector accessing in specified way
/// </summary>
/// <typeparam name="T"></typeparam>
public class AnswersVector<T> : AnswersVectorBase<T>, IEquatable<AnswersVector<T>> where T : notnull
{
    private readonly Dictionary<T, double> _answers = new();
    /// <summary>
    /// Initializes new AnswersVector instance 
    /// </summary>
    /// <param name="answers">System answers</param>
    /// <param name="variableNames">Specified variable names</param>
    /// <exception cref="ArgumentNullException">Variable names is null</exception>
    /// <exception cref="InvalidOperationException">Answers length is not equal to variable names length</exception>
    public AnswersVector(double[] answers, IEnumerable<T> variableNames)
    {
        if (variableNames == null) throw new ArgumentNullException(nameof(variableNames));
        if (answers.Length != variableNames.Count()) throw new InvalidOperationException("");
        for (int i = 0; i < answers.Length; i++)
        {
            _answers.Add(variableNames.ElementAt(i), answers[i]);
        }
    }
    public override double this[T key] => _answers[key];
    /// <summary>
    /// Count of answers
    /// </summary>
    public int Count => _answers.Count;
    public override string ToString() => $"{GetType().Name}. Count: {Count}";
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(obj as AnswersVector<T>);
    }
    public bool Equals(AnswersVector<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _answers.Except(other._answers).Any() == false;
    }
    public override int GetHashCode() => _answers.GetHashCode();
}