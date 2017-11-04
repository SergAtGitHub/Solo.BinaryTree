using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Parser
{
    public interface IBinaryTreeParseAlgorythm
    {
        CommandResult<Tree> ParseBinaryTree(BinaryTreeParseArguments arguments);
    }
}