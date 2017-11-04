using System;
using System.Collections.Generic;
using System.IO;
using Solo.BinaryTree.Constructor.Core;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public interface ITreeSerializer
    {
        CommandResult Serialize(TreeSerializationArgs arguments);
    }

    public class TreeSerializationArgs
    {
        public Tree Tree { get; set; }
        public TextWriter TextWriter { get; set; }
        public ITreeFormatter Formatter { get; set; }
    }

    public interface ITreeFormatter
    {
        string Format(Tree tree);
    }

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

    public abstract class TreeSerializeAction : BaseAction<TreeSerializationArgs> { }

    public class SerializeToTextWriter : TreeSerializeAction
    {
        private ITreeTraversalAlgorythm treeTraversalAlgorythm;

        public ITreeTraversalAlgorythm TreeTraversalAlgorythm
        {
            get => treeTraversalAlgorythm ?? DepthTraverse.Instance;
            set => treeTraversalAlgorythm = value;
        }

        public override void Execute(TreeSerializationArgs args)
        {
            foreach (var node in TreeTraversalAlgorythm.GetAll(args.Tree))
            {
                args.TextWriter.WriteLine(args.Formatter.Format(node));
            }
        }

        public override bool CanExecute(TreeSerializationArgs args)
        {
            return base.CanExecute(args) && args.TextWriter != null && args.Tree != null && args.Formatter != null;
        }
    }
}