using System;
using Solo.BinaryTree.Constructor.Core;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public interface ITreeSerializer
    {
        CommandResult Serialize(TreeSerializationArgs arguments);
    }
}