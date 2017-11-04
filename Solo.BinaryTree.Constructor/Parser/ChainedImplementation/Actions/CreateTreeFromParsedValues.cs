using System;
using System.Collections.Generic;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class CreateTreeFromParsedValues : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            foreach (var model in args.NodeModels)
            {
                CommandResult result = this.ProcessModelInDictionary(model, args.SubtreesDictionary);

                if (result.IsFailure)
                {
                    args.Messages.Add(result.FailureMessage);
                    return;
                }
            }
        }

        public virtual CommandResult ProcessModelInDictionary(BinaryTreeNodeModel node, Dictionary<string, Tree> subtreesDictionary)
        {
            Tree root = null;
            if (subtreesDictionary.ContainsKey(node.Root))
            {
                root = subtreesDictionary[node.Root];
                if (root.Left != null || root.Right != null)
                {
                    return CommandResult.Failure(
                        String.Format(
                            "Duplicating entry with key [{0}] was found, please ensure that you don't have duplicates.",
                            node.Root));
                }
            }
            else
            {
                var createRootResult = Tree.Create(node.Root);
                if (createRootResult.IsFailure)
                {
                    return CommandResult.Failure(createRootResult.FailureMessage);
                }

                root = createRootResult.Result;
                subtreesDictionary.Add(node.Root, root);
            }

            var key = node.Left;
            if (key != "#")
            {
                if (subtreesDictionary.ContainsKey(key))
                {
                    var tree = subtreesDictionary[key];

                    if (tree.Parent != null)
                    {
                        return CommandResult.Failure(
                            String.Format(
                                "Algorythm found a node [{0}] referenced twice by [{1}] and [{2}], which is not possible in tree structures, please review the tree.",
                                key, tree.Parent.Data, root.Data));
                    }

                    root.OverrideNode(tree, BinaryChildrenEnum.Left);
                }
                else
                {
                    var addLeft = root.AddNode(key, BinaryChildrenEnum.Left);
                    if (addLeft.IsFailure)
                    {
                        return CommandResult.Failure(addLeft.FailureMessage);
                    }

                    subtreesDictionary.Add(key, root.Left);
                }
            }

            key = node.Right;
            if (key != "#")
            {
                if (subtreesDictionary.ContainsKey(key))
                {
                    var tree = subtreesDictionary[key];

                    if (tree.Parent != null)
                    {
                        return CommandResult.Failure(
                            String.Format(
                                "Algorythm found a node [{0}] referenced twice by [{1}] and [{2}], which is not possible in tree structures, please review the tree.",
                                key, tree.Parent.Data, root.Data));
                    }

                    root.OverrideNode(tree, BinaryChildrenEnum.Right);
                }
                else
                {
                    var addRight = root.AddNode(key, BinaryChildrenEnum.Right);
                    if (addRight.IsFailure)
                    {
                        return CommandResult.Failure(addRight.FailureMessage);
                    }

                    subtreesDictionary.Add(key, root.Right);
                }
            }

            return CommandResult.Ok();
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.NodeModels != null && args.SubtreesDictionary != null;
        }
    }
}