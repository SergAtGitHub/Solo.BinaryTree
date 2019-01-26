using System.Threading.Tasks;
using Pipelines;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation
{
    public abstract class BinaryTreeParseAction : SafeProcessor<BinaryTreeParseArguments>
    {
        public override Task SafeExecute(BinaryTreeParseArguments args)
        {
            Execute(args);
            return Done;
        }

        public abstract void Execute(BinaryTreeParseArguments arguments);

        public override bool SafeCondition(BinaryTreeParseArguments args)
        {
            return base.SafeCondition(args) && CanExecute(args);
        }

        public abstract bool CanExecute(BinaryTreeParseArguments arguments);
    }
}