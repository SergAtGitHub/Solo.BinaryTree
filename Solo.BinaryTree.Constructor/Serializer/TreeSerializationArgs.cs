using System.IO;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public class TreeSerializationArgs
    {
        public Tree Tree { get; set; }
        public TextWriter TextWriter { get; set; }
        public ITreeFormatter Formatter { get; set; }
    }
}