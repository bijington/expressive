namespace Expressive.Functions
{
    public interface IFunction
    {
        string Name { get; }

        object Evaluate(object[] values);
    }
}
