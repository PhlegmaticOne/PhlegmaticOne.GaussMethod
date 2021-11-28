namespace PhlegmaticOne.GaussMethod.Lib.Models.AnswersVectors;

public abstract class AnswersVectorBase<T> where T: notnull
{
    public abstract double this[T key] { get; }
}