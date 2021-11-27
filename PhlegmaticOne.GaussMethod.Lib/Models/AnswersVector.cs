namespace PhlegmaticOne.GaussMethod.Lib.Models;

public class AnswersVector : IEquatable<AnswersVector>
{
    private readonly double[] _answers;

    public AnswersVector(double[] answers)
    {
        _answers = answers;
    }
    public double this[int index] => _answers[index];
    public int Count => _answers.Length;
    public override string ToString() => $"{GetType().Name}. Count: {Count}";
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(obj as AnswersVector);
    }

    public bool Equals(AnswersVector? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _answers.Except(other._answers).Any() == false;
    }

    public override int GetHashCode() => _answers.GetHashCode();
}