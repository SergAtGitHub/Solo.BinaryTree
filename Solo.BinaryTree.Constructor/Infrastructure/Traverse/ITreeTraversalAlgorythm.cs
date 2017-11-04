using System.Collections.Generic;

namespace Solo.BinaryTree.Constructor.Infrastructure.Traverse
{
    public interface ITreeTraversalAlgorythm
    {
        IEnumerable<Tree> GetAll(Tree tree);
    }
}