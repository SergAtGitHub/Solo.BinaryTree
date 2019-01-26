using System;
using System.Linq;
using Pipelines;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;

namespace Solo.BinaryTree.Constructor.Core
{
    public class QueryResultAnalyzer<T> : IResultAnalyzer<T> where T : class
    {
        public CommandResult<T> Analyze(QueryContext<T> arguments)
        {
            if (arguments.GetResult() != null)
            {
                return CommandResult.Ok(arguments.GetResult());
            }

            return CommandResult.Failure<T>(string.Join(Environment.NewLine,
                arguments.GetAllMessages().Select(message => message.Message)));
        }
    }
}