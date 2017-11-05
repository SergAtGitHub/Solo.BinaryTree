using System;
using System.Collections.Generic;
using System.Linq;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;

namespace Solo.BinaryTree.Constructor.Infrastructure
{
    public static class TreeExtensions
    {
        public static Tree AddLeftAndNavigateToIt(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Left);
            return tree.Left;
        }

        public static Tree AddLeftAndStay(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Left);
            return tree;
        }

        public static Tree AddLeftAndNavigateToRoot(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Left);
            return tree.NavigateToRoot();
        }

        public static Tree AddRightAndNavigateToRoot(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Right);
            return tree.NavigateToRoot();
        }

        public static Tree NavigateToRoot(this Tree tree)
        {
            while (tree.Parent != null)
            {
                tree = tree.Parent;
            }

            return tree;
        }

        public static Tree AddRightAndNavigateBack(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Right);
            return tree.NavigateBack();
        }

        public static Tree AddRightAndNavigateToIt(this Tree tree, string data)
        {
            tree.AddNode(data, BinaryChildrenEnum.Right);
            return tree.Right;
        }

        public static Tree NavigateBack(this Tree tree)
        {
            return tree.Parent;
        }

    }

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

        public void AddLine(params string[] data)
        {
            var traverse = new LeavesTraverse();
            var dataCounter = 0;

            foreach (var leaf in traverse.GetAll(Root))
            {
                if (dataCounter >= data.Length) break;
                leaf.AddNode(data[dataCounter++], BinaryChildrenEnum.Left);
                if (dataCounter >= data.Length) break;
                leaf.AddNode(data[dataCounter++], BinaryChildrenEnum.Right);
            }
        }
    }
}