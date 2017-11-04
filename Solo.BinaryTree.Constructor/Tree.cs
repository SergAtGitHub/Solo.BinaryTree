using System;
using System.Collections.Generic;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor
{
    public class Tree
    {
        public string Data { get; }

        public Tree Left { get; private set; }
        public Tree Right { get; private set; }
        public Tree Parent { get; private set; }

        private Tree(string data)
        {
            this.Data = data;
        }

        public static CommandResult<Tree> Create(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return CommandResult<Tree>.Failure("Cannot create a node without a data.");
            }

            return CommandResult<Tree>.Ok(new Tree(data));
        }

        public CommandResult OverrideNode(Tree newNode, BinaryChildrenEnum binaryChildrenEnum)
        {
            if (newNode.Parent != null)
            {
                if (newNode.Parent != this)
                {
                    CommandResult.Failure("Cannot specify this as a child because it already has a parent.");
                }
            }
            else
            {
                newNode.Parent = this;
            }

            if (binaryChildrenEnum == BinaryChildrenEnum.Left)
            {
                this.Left = newNode;
            }

            if (binaryChildrenEnum == BinaryChildrenEnum.Right)
            {
                this.Right = newNode;
            }

            return CommandResult.Ok();
        }

        public CommandResult AddNode(string newNode, BinaryChildrenEnum binaryChildrenEnum)
        {
            var createTreeResult = Tree.Create(newNode);

            if (createTreeResult.IsFailure)
            {
                return CommandResult.Failure(createTreeResult.FailureMessage);
            }

            return this.OverrideNode(createTreeResult.Result, binaryChildrenEnum);
        }
    }

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

    public class TreeComparer : IEqualityComparer<Tree>
    {
        public static readonly TreeComparer Instance = new TreeComparer();

        public bool Equals(Tree x, Tree y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Data == y.Data && Equals(x.Left, y.Left) && Equals(x.Right, y.Right);
        }

        public int GetHashCode(Tree obj)
        {
            throw new NotImplementedException();
        }
    }
}