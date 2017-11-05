using System;
using System.Data;
using System.IO;
using Solo.BinaryTree.Constructor.Parser;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;
using Solo.BinaryTree.Constructor.Serializer;

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
            return BuildTree((TextReader) streamReader);
        }

        public static Tree BuildTreeByFilePath(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                return BuildTree(streamReader);
            }
        }

        public static Tree BuildTreeByStringInput(string input)
        {
            using (StringReader stringReader = new StringReader(input))
            {
                return BuildTree(stringReader);
            }
        }

        public static Tree BuildTree(TextReader textReader)
        {
            var binaryTreeParseArguments = new BinaryTreeParseArguments
            {
                TextReader = textReader
            };

            var parseResult = Algorythm.ParseBinaryTree(binaryTreeParseArguments);

            if (parseResult.IsSuccess)
            {
                return parseResult.Result;
            }

            throw new InvalidOperationException(parseResult.FailureMessage);
        }

        public static string Serialize(Tree tree)
        {
            return Serialize(tree, InlineTreeFormatter.Instance);
        }

        public static string Serialize(Tree tree, string serializationFormat)
        {
            return Serialize(tree, formatter: new InlineTreeFormatter(serializationFormat));
        }

        public static string Serialize(Tree tree, ITreeFormatter formatter)
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                TreeSerializationArgs arguments = new TreeSerializationArgs
                {
                    TextWriter = stringWriter,
                    Formatter = formatter,
                    Tree = tree,
                    SkipEmptyLines = true
                };

                TreeSerializer.Instance.Serialize(arguments);

                return stringWriter.ToString();
            }
        }
    }
}