using System.Collections.Generic;

namespace Solo.BinaryTree.Constructor.Infrastructure.Traverse
{
    public class LeavesTraverse : ITreeTraversalAlgorythm
    {
        public static readonly LeavesTraverse Instance = new LeavesTraverse();

        public IEnumerable<Tree> GetAll(Tree tree)
        {
            if (tree == null)
                yield break;

            if (tree.Left == null && tree.Right == null)
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