using System.Collections.Generic;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public class TreeSerializer : ITreeSerializer
    {
        public static readonly TreeSerializer Instance = new TreeSerializer(new []{new SerializeToTextWriter()});

        public TreeSerializer(IEnumerable<IAction<TreeSerializationArgs>> actions)
        {
            Actions = new List<IAction<TreeSerializationArgs>>(actions);
        }

        public List<IAction<TreeSerializationArgs>> Actions { get; }

        public CommandResult Serialize(TreeSerializationArgs arguments)
        {
            Actions.ForEach(action => action.Process(arguments));
            return CommandResult.Ok();
        }
    }
}