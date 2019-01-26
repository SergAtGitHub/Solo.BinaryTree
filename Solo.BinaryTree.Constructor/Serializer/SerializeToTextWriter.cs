using System.Threading.Tasks;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public class SerializeToTextWriter : TreeSerializeAction
    {
        private ITreeTraversalAlgorythm treeTraversalAlgorythm;

        public ITreeTraversalAlgorythm TreeTraversalAlgorythm
        {
            get => treeTraversalAlgorythm ?? DepthTraverse.Instance;
            set => treeTraversalAlgorythm = value;
        }

        public override Task SafeExecute(TreeSerializationArgs args)
        {
            var skipEmpty = args.SkipEmptyLines.HasValue && args.SkipEmptyLines.Value;

            foreach (var node in TreeTraversalAlgorythm.GetAll(args.Tree))
            {
                var formattedString = args.Formatter.Format(node);
                if (skipEmpty && string.IsNullOrWhiteSpace(formattedString))
                {
                    continue;
                }

                args.TextWriter.WriteLine(formattedString);
            }

            return Done;
        }

        public override bool SafeCondition(TreeSerializationArgs args)
        {
            return base.SafeCondition(args) && args.TextWriter != null && args.Tree != null && args.Formatter != null;
        }
    }
}