namespace Solo.BinaryTree.Constructor.Core
{
    public interface IResultAnalyzer<T> where T : class
    {
        CommandResult<T> Analyze(QueryArguments<T> arguments);
    }
}