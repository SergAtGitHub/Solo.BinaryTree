using System;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;

namespace Solo.BinaryTree.Constructor.Core
{
    public class QueryResultAnalyzer<T> : IResultAnalyzer<T> where T : class
    {
        public CommandResult<T> Analyze(QueryArguments<T> arguments)
        {
            if (arguments.Result != null)
            {
                return CommandResult.Ok(arguments.Result);
            }

            return CommandResult.Failure<T>(string.Join(Environment.NewLine, arguments.Messages));
        }
    }
}