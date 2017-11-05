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