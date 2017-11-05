using System.Collections.Generic;
using System.Linq;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;

namespace Solo.BinaryTree.Constructor.Infrastructure.Builders
{
    public class LineByLineTreeBuilder
    {
        public Tree Root { get; }
        protected Queue<Tree> LatestLevel { get; } = new Queue<Tree>();

        public LineByLineTreeBuilder() : this(Tree.Create("root").Result)
        {
        }

        public LineByLineTreeBuilder(Tree root)
        {
            Root = root;
            LatestLevel.Enqueue(root);
        }

        public void AddData(params string[] data)
        {
            var dataCounter = 0;

            while (LatestLevel.Count > 0)
            {
                Tree node = LatestLevel.Dequeue();
                if (node == null) continue;

                if (dataCounter >= data.Length) break;
                var dataObject = data[dataCounter++];
                if (dataObject != SpecialIndicators.NullNodeIndicator)
                {
                    LatestLevel.Enqueue(node.AddLeftAndNavigateToIt(dataObject));
                }

                if (dataCounter >= data.Length) break;
                dataObject = data[dataCounter++];
                if (dataObject != SpecialIndicators.NullNodeIndicator)
                {
                    LatestLevel.Enqueue(node.AddRightAndNavigateToIt(dataObject));
                }
            }
        }
    }
}