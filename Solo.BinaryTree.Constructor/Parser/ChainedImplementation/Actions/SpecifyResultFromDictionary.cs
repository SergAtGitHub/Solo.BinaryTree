using System.Linq;
using Solo.BinaryTree.Constructor.Infrastructure;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class SpecifyResultFromDictionary : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            var tree = args.SubtreesDictionary.First().Value;
            
            args.Result = tree.NavigateToRoot();
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.SubtreesDictionary.Count > 0 && args.Messages.Count == 0;
        }
    }
}