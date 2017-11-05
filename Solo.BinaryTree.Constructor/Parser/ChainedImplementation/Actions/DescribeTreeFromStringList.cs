using System;
using System.Collections.Generic;
using System.Linq;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Infrastructure;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class DescribeTreeFromStringList : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.NodeModels = this.DeclareEnumerableNodes(args.TextStrings);
        }

        public virtual IEnumerable<CommandResult<BinaryTreeNodeModel>> DeclareEnumerableNodes(IEnumerable<string> streamNodes)
        {
            return streamNodes.Where(str => !string.IsNullOrWhiteSpace(str)).Select(this.ParseString);
        }

        public virtual CommandResult<BinaryTreeNodeModel> ParseString(string line)
        {
            var members = line.Split(SpecialIndicators.NodeDescriptionDelimiters, StringSplitOptions.RemoveEmptyEntries);

            if (members.Length != 3)
            {
                return CommandResult<BinaryTreeNodeModel>.Failure(
                    string.Format(ChainedBinaryTreeMessages.TheStringWasNotInExpectedFormat, line));
            }

            return CommandResult.Ok(new BinaryTreeNodeModel()
            {
                Root = members[0],
                Left = members[1],
                Right = members[2]
            });
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.TextStrings != null && args.NodeModels == null;
        }
    }
}