using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Parser;

namespace Solo.BinaryTree.Constructor
{
    public class ChainedBinaryTreeParser : IBinaryTreeParseAlgorythm
    {
        public static readonly ChainedBinaryTreeParser Instance = new ChainedBinaryTreeParser(new BinaryTreeParseAction[0]);

        public ChainedBinaryTreeParser(IEnumerable<BinaryTreeParseAction> actions)
        {
            Actions = new List<IAction<BinaryTreeParseArguments>>(actions);
        }

        public List<IAction<BinaryTreeParseArguments>> Actions { get; }

        public CommandResult<Tree> ParseBinaryTree(BinaryTreeParseArguments arguments)
        {
            Actions.ForEach(action => action.Process(arguments));

            return new CommandResult<Tree>(arguments.Result, string.Join(Environment.NewLine, arguments.Messages));
        }
    }

    public abstract class BinaryTreeParseAction : BaseAction<BinaryTreeParseArguments> { }

    public class DescribeTreeFromStreamContext : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.TextStrings = this.DeclareEnumerableStrings(args.StreamReader);
        }

        public virtual IEnumerable<string> DeclareEnumerableStrings(StreamReader streamReader)
        {
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                yield return str;
            }
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.StreamReader != null && args.TextStrings == null;
        }
    }

    public class DescribeTreeFromStringList : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.NodeModels = this.DeclareEnumerableNodes(args.TextStrings);
        }

        public virtual IEnumerable<BinaryTreeNodeModel> DeclareEnumerableNodes(IEnumerable<string> streamNodes)
        {
            return streamNodes.Select(this.ParseString);
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

    public class CreateTreeFromParsedValues : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            foreach (var model in args.NodeModels)
            {
                CommandResult this.Process();
            }
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.NodeModels != null;
        }
    }
}