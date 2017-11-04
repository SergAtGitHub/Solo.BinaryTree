using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Infrastructure;

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
                return CommandResult<Tree>.Failure(TreeMessages.CanotCreateTreeWithoutData);
            }

            return CommandResult<Tree>.Ok(new Tree(data));
        }

        public CommandResult OverrideNode(Tree newNode, BinaryChildrenEnum binaryChildrenEnum)
        {
            if (newNode.Parent != null)
            {
                if (newNode.Parent != this)
                {
                    var failure = string.Format(TreeMessages.CannotSpecifyChildBecauseItAlreadyHasParent,
                        this.Data, nameof(binaryChildrenEnum), newNode.Data, newNode.Parent.Data);

                    CommandResult.Failure(failure);
                }
                else
                {
                    if (binaryChildrenEnum == BinaryChildrenEnum.Left && this.Right == newNode
                        || binaryChildrenEnum == BinaryChildrenEnum.Right && this.Left == newNode)
                    {
                        CommandResult.Failure(TreeMessages.CannotAddReferenceTwice);
                    }
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
}