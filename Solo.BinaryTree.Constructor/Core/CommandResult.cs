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

        private CommandResult(T result, string failureMessage)
        {
            this.result = result;
            FailureMessage = failureMessage;
        }

        public static CommandResult<T> Ok(T result)
        {
            if (result == null)
            {
                throw new InvalidOperationException("Result cannot be null.");
            }

            return new CommandResult<T>(result, String.Empty);
        }

        public static CommandResult<T> Failure(string failureMessage)
        {
            if (string.IsNullOrWhiteSpace(failureMessage))
            {
                throw new InvalidOperationException("Message has to be provided for a failure result.");
            }

            return new CommandResult<T>(null, failureMessage);
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

        public static CommandResult<T> Ok<T>(T result) where T : class
        {
            return CommandResult<T>.Ok(result);
        }

        public static CommandResult Failure(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Please explain why operation has failed.", nameof(message));
            }

            return new CommandResult(message);
        }

        public static CommandResult<T> Failure<T>(string message) where T : class
        {
            return CommandResult<T>.Failure(message);
        }
    }
}