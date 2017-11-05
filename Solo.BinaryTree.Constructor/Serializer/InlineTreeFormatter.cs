namespace Solo.BinaryTree.Constructor.Serializer
{
    public class InlineTreeFormatter : ITreeFormatter
    {
        public static readonly InlineTreeFormatter Instance = new InlineTreeFormatter();

        public InlineTreeFormatter() : this("{0}, {1}, {2}")
        {
        }

        public InlineTreeFormatter(string messageFormat)
        {
            MessageFormat = messageFormat;
        }

        public string MessageFormat { get; }

        public string Format(Tree node)
        {
            if (node == null) return string.Empty;

            if (node.Left == null && node.Right == null) return string.Empty;

            return string.Format(MessageFormat, node.Data, node.Left?.Data ?? "#",
                node.Right?.Data ?? "#");

        }
    }
}