using System;
using System.Collections.Generic;
using System.Linq;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class DescribeTreeFromStringList : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.NodeModels = this.DeclareEnumerableNodes(args.TextStrings);
        }

        public virtual IEnumerable<BinaryTreeNodeModel> DeclareEnumerableNodes(IEnumerable<string> streamNodes)
        {
            return streamNodes.Where(str => !string.IsNullOrWhiteSpace(str)).Select(this.ParseString);
        }

        public virtual BinaryTreeNodeModel ParseString(string line)
        {
            var members = line.Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);

            if (members.Length != 3)
            {
                throw new ArgumentException(
                    "A line should contain 3 members in format similar to: 'Root, LeftNode, RightNode'.", nameof(line));
            }

            return new BinaryTreeNodeModel()
            {
                Root = members[0],
                Left = members[1],
                Right = members[2]
            };
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.TextStrings != null && args.NodeModels == null;
        }
    }
}