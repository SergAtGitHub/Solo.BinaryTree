using System.Collections.Generic;

namespace Solo.BinaryTree.Constructor.Infrastructure.Traverse
{
    public class DepthTraverse : ITreeTraversalAlgorythm
    {
        public static readonly DepthTraverse Instance = new DepthTraverse();

        public IEnumerable<Tree> GetAll(Tree tree)
        {
            if (tree == null)
                yield break;

            yield return tree;

            foreach (var left in GetAll(tree.Left))
            {
                yield return left;
            }

            foreach (var right in GetAll(tree.Right))
            {
                yield return right;
            }
        }
    }
}