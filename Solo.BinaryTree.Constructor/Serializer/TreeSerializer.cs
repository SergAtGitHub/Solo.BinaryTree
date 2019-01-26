using System.Collections.Generic;
using Pipelines;
using Pipelines.Implementations.Pipelines;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public class TreeSerializer : PipelineExecutor, ITreeSerializer
    {
        public static readonly TreeSerializer Instance = new TreeSerializer(new TreeSerializeAction[]
        {
            new MakeDescisionAboutEmptyStrings(true),
            new SerializeToTextWriter()
        });

        public TreeSerializer(IEnumerable<IProcessor> actions) : base(PredefinedPipeline.FromProcessors(actions))
        {
        }
        
        public CommandResult Serialize(TreeSerializationArgs arguments)
        {
            base.Execute(arguments).Wait();
            return CommandResult.Ok();
        }
    }
}