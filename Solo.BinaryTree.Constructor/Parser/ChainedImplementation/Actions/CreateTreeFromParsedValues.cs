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
                root = new Tree(){ Data = node.Root };
                subtreesDictionary.Add(node.Root, root);
            }

            var processSubtree = ProcessSubtree(node.Left, subtreesDictionary, root);
            if (processSubtree.IsFailure) return processSubtree;

            processSubtree = ProcessSubtree(node.Right, subtreesDictionary, root);
            if (processSubtree.IsFailure) return processSubtree;

            return CommandResult.Ok();
        }

        protected CommandResult ProcessSubtree(string key, Dictionary<string, Tree> subtreesDictionary, Tree root)
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

                tree.Parent = root;
                root.Right = tree;
            }
            else
            {
                var tree = new Tree() {Data = key, Parent = root};
                subtreesDictionary.Add(key, tree);
            }
            return CommandResult.Ok();
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.NodeModels != null && args.SubtreesDictionary != null;
        }
    }
}