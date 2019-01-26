﻿using System.Collections.Generic;
using System.IO;
using Pipelines;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Parser
{
    public class BinaryTreeParseArguments : QueryContext<Tree>
    {
        public TextReader TextReader { get; set; }
        public IEnumerable<string> TextStrings { get; set; }
        public IEnumerable<CommandResult<BinaryTreeNodeModel>> NodeModels { get; set; }
        public Dictionary<string, Tree> SubtreesDictionary { get; } = new Dictionary<string, Tree>();
    }
}