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
            
            CommandResult processLeft = ProcessChild(subtreesDictionary, node.Left, root, BinaryChildrenEnum.Left);
            if (processLeft.IsFailure)
            {
                return CommandResult.Failure(processLeft.FailureMessage);
            }

            CommandResult processRight = ProcessChild(subtreesDictionary, node.Right, root, BinaryChildrenEnum.Right);
            if (processRight.IsFailure)
            {
                return CommandResult.Failure(processRight.FailureMessage);
            }

            return CommandResult.Ok();
        }

        protected virtual CommandResult ProcessChild(Dictionary<string, Tree> subtreesDictionary, string key, Tree root,
            BinaryChildrenEnum childrenEnum)
        {
            if (key == "#") return CommandResult.Ok();

            var dictionaryResult = GetFromDictionaryOrAdd(subtreesDictionary, key);

            if (dictionaryResult.IsFailure)
            {
                return CommandResult.Failure(dictionaryResult.FailureMessage);
            }

            return root.OverrideNode(dictionaryResult.Result, childrenEnum);
        }

        protected virtual CommandResult<Tree> GetFromDictionaryOrAdd(Dictionary<string, Tree> subtreesDictionary, string key)
        {
            if (!subtreesDictionary.ContainsKey(key))
            {
                var createTreeResult = Tree.Create(key);

                if (createTreeResult.IsFailure)
                {
                    return CommandResult<Tree>.Failure(createTreeResult.FailureMessage);
                }

                subtreesDictionary.Add(key, createTreeResult.Result);
            }

            return CommandResult.Ok(subtreesDictionary[key]);
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.NodeModels != null && args.SubtreesDictionary != null;
        }
    }
}