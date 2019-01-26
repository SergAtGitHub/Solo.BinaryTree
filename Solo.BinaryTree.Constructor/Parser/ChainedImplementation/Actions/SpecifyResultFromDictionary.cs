using System.Linq;
using Solo.BinaryTree.Constructor.Infrastructure;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class SpecifyResultFromDictionary : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            var tree = args.SubtreesDictionary.First().Value;
            
            args.SetResultWithInformation(tree.NavigateToRoot(), "Result is set.");
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return args.SubtreesDictionary.Count > 0 && args.GetAllMessages().Length == 0;
        }
    }
}