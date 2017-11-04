using System;
using System.Data;
using System.IO;
using Solo.BinaryTree.Constructor.Parser;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;

namespace Solo.BinaryTree.Constructor
{
    public static class Api
    {
        private static IBinaryTreeParseAlgorythm _algorythm;

        public static IBinaryTreeParseAlgorythm Algorythm
        {
            get => _algorythm ?? ChainedBinaryTreeParser.Instance;
            set => _algorythm = value;
        }

        public static Tree BuildTree(StreamReader streamReader)
        {
            var binaryTreeParseArguments = new BinaryTreeParseArguments
            {
                StreamReader = streamReader
            };

            var parseResult = Algorythm.ParseBinaryTree(binaryTreeParseArguments);

            if (parseResult.IsSuccess)
            {
                return parseResult.Result;
            }

            throw new ArgumentException(parseResult.FailureMessage, nameof(streamReader));
        }
    }
}