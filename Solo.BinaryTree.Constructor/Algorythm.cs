using System;
using System.Collections.Generic;
using System.IO;

namespace Solo.BinaryTree.Constructor
{
    public interface IAction<in TArgs>
    {
        void Process(TArgs arguments);
    }

    public interface IBinaryTreeParseAlgorythm
    {
        BinaryTreeParseResult ParseBinaryTree(BinaryTreeParseArguments arguments);
    }

    public class ChainedBinaryTreeParser : IBinaryTreeParseAlgorythm
    {
        public ChainedBinaryTreeParser(IEnumerable<BinaryTreeParseAction> actions)
        {
            Actions = new List<BinaryTreeParseAction>(actions);
        }

        public List<BinaryTreeParseAction> Actions { get; }

        public BinaryTreeParseResult ParseBinaryTree(BinaryTreeParseArguments arguments)
        {
            Actions.ForEach(action => action.Execute(arguments));

            return new BinaryTreeParseResult(arguments.Result, string.Join(Environment.NewLine, arguments.Messages));
        }
    }

    public abstract class BaseAction<TArgs> : IAction<TArgs>
    {
        public void Process(TArgs arguments)
        {
            if (!CanExecute(arguments))
            {
                return;
            }

            Execute(arguments);
        }

        public abstract void Execute(TArgs args);

        public virtual bool CanExecute(TArgs args)
        {
            return true;
        }
    }

    public abstract class BinaryTreeParseAction : BaseAction<BinaryTreeParseArguments> { }

    public class BinaryTreeParseArguments
    {
        public List<string> Messages { get; } = new List<string>();
        public Tree Result { get; set; }
        public StreamReader StreamReader { get; set; }
    }

    public struct BinaryTreeParseResult
    {
        private readonly Tree result;

        public Tree Result
        {
            get
            {
                if (result == null)
                    throw new InvalidOperationException("Result has no value.");

                return result;
            }
        }

        public bool IsSuccess => result != null;

        public bool IsFailure => !IsSuccess;

        public string FailureMessage { get; }

        public BinaryTreeParseResult(Tree result, string failureMessage)
        {
            if (result == null && string.IsNullOrWhiteSpace(failureMessage))
            {
                throw new InvalidOperationException("Message has to be provided for a non-specified result.");
            }

            this.result = result;
            FailureMessage = failureMessage;
        }
    }

    public class Tree
    {
        
    }
}