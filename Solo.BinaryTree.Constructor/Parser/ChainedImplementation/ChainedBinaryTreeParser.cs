using System;
using System.Collections.Generic;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation
{
    public class ChainedBinaryTreeParser : IBinaryTreeParseAlgorythm
    {
        public static readonly ChainedBinaryTreeParser Instance = new ChainedBinaryTreeParser(new BinaryTreeParseAction[]
        {
            new DescribeTreeFromStreamContext(), 
            new DescribeTreeFromStringList(), 
            new CreateTreeFromParsedValues(), 
            new SpecifyResultFromDictionary()
        });

        public ChainedBinaryTreeParser(IEnumerable<BinaryTreeParseAction> actions)
        {
            Actions = new List<IAction<BinaryTreeParseArguments>>(actions);
        }

        public List<IAction<BinaryTreeParseArguments>> Actions { get; }

        public CommandResult<Tree> ParseBinaryTree(BinaryTreeParseArguments arguments)
        {
            Actions.ForEach(action => action.Process(arguments));

            if (arguments.Result != null)
            {
                return CommandResult<Tree>.Ok(arguments.Result);
            }

            return CommandResult<Tree>.Failure(string.Join(Environment.NewLine, arguments.Messages));
        }
    }
}