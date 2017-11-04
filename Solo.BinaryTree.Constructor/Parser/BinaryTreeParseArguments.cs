using System.Collections.Generic;
using System.IO;

namespace Solo.BinaryTree.Constructor.Parser
{
    public class BinaryTreeParseArguments
    {
        public List<string> Messages { get; } = new List<string>();
        public Tree Result { get; set; }
        public TextReader TextReader { get; set; }
        public IEnumerable<string> TextStrings { get; set; }
        public IEnumerable<BinaryTreeNodeModel> NodeModels { get; set; }
        public Dictionary<string, Tree> SubtreesDictionary { get; } = new Dictionary<string, Tree>();
    }
}