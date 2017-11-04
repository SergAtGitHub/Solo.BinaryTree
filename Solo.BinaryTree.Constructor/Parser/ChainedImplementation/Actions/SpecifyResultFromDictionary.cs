using System.Linq;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class SpecifyResultFromDictionary : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            var tree = args.SubtreesDictionary.First().Value;

            while (tree.Parent != null)
            {
                tree = tree.Parent;
            }

            args.Result = tree;
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.SubtreesDictionary.Count > 0;
        }
    }
}