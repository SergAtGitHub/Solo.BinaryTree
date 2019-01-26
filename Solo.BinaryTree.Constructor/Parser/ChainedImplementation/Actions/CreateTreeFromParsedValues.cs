using System;
using System.Collections.Generic;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Infrastructure;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class CreateTreeFromParsedValues : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            foreach (var getModelResult in args.NodeModels)
            {
                if (getModelResult.IsFailure)
                {
                    args.AddError(getModelResult.FailureMessage);
                    return;
                }

                CommandResult result = this.ProcessModelInDictionary(getModelResult.Result, args.SubtreesDictionary);

                if (result.IsFailure)
                {
                    args.AddError(result.FailureMessage);
                    return;
                }
            }
        }

        public virtual CommandResult ProcessModelInDictionary(BinaryTreeNodeModel node, Dictionary<string, Tree> subtreesDictionary)
        {
            var getRootResult = GetFromDictionaryOrAdd(subtreesDictionary, node.Root);

            if (getRootResult.IsFailure)
            {
                return CommandResult.Failure(getRootResult.FailureMessage);
            }

            Tree root = getRootResult.Result;
            if (root.Left != null || root.Right != null)
            {
                return CommandResult.Failure(
                    String.Format(
                        ChainedBinaryTreeMessages.DuplicatingEntryWasFound,
                        node.Root, node.Left, node.Right, root.Left?.Data, root.Right?.Data));
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
            if (key == SpecialIndicators.NullNodeIndicator) return CommandResult.Ok();

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
            return args.NodeModels != null && args.SubtreesDictionary != null;
        }
    }
}