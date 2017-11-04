using System;

namespace Solo.BinaryTree.Constructor.Core
{
    public struct CommandResult<T> where T : class 
    {
        private readonly T result;

        public T Result
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

        public CommandResult(T result, string failureMessage)
        {
            if (result == null && string.IsNullOrWhiteSpace(failureMessage))
            {
                throw new InvalidOperationException("Message has to be provided for a non-specified result.");
            }

            this.result = result;
            FailureMessage = failureMessage;
        }
    }

    public struct CommandResult
    {
        public bool IsSuccess => string.IsNullOrWhiteSpace(FailureMessage);

        public bool IsFailure => !IsSuccess;

        public string FailureMessage { get; }

        private CommandResult(string failureMessage)
        {
            FailureMessage = failureMessage;
        }

        public static CommandResult Ok()
        {
            return new CommandResult();
        }

        public static CommandResult Failure(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Please explain why operation has failed.", nameof(message));
            }

            return new CommandResult(message);
        }
    }
}