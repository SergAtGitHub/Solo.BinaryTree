namespace Solo.BinaryTree.Constructor.Serializer
{
    public class MakeDescisionAboutEmptyStrings : TreeSerializeAction
    {
        public bool SkipEmpty { get; }
        
        public MakeDescisionAboutEmptyStrings(bool skipEmpty)
        {
            SkipEmpty = skipEmpty;
        }

        public override void Execute(TreeSerializationArgs args)
        {
            args.SkipEmptyLines = SkipEmpty;
        }

        public override bool CanExecute(TreeSerializationArgs args)
        {
            return base.CanExecute(args) && !args.SkipEmptyLines.HasValue;
        }
    }
}