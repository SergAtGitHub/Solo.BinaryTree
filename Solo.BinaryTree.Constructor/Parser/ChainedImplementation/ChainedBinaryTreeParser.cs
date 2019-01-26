using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Pipelines;
using Pipelines.Implementations.Pipelines;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation
{
    public class ChainedBinaryTreeParser : PipelineExecutor, IBinaryTreeParseAlgorythm
    {
        public static readonly ChainedBinaryTreeParser Instance = new ChainedBinaryTreeParser(new BinaryTreeParseAction[]
        {
            new DescribeTreeFromStreamContext(), 
            new DescribeTreeFromStringList(), 
            new CreateTreeFromParsedValues(), 
            new SpecifyResultFromDictionary()
        });

        public ChainedBinaryTreeParser(IEnumerable<BinaryTreeParseAction> actions) : base(PredefinedPipeline.FromProcessors(actions))
        {
            ResultAnalyzer = new QueryResultAnalyzer<Tree>();
        }

        public IResultAnalyzer<Tree> ResultAnalyzer { get; }

        public CommandResult<Tree> ParseBinaryTree(BinaryTreeParseArguments arguments)
        {
            base.Execute(arguments).Wait();
            return ResultAnalyzer.Analyze(arguments);
        }
    }
}