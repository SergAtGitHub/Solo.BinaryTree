using System.Collections.Generic;
using System.IO;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Parser
{
    public class BinaryTreeParseArguments : QueryArguments<Tree>
    {
        public TextReader TextReader { get; set; }
        public IEnumerable<string> TextStrings { get; set; }
        public IEnumerable<BinaryTreeNodeModel> NodeModels { get; set; }
        public Dictionary<string, Tree> SubtreesDictionary { get; } = new Dictionary<string, Tree>();
    }
}