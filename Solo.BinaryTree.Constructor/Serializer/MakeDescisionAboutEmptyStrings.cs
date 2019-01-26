using System.Threading.Tasks;

namespace Solo.BinaryTree.Constructor.Serializer
{
    public class MakeDescisionAboutEmptyStrings : TreeSerializeAction
    {
        public bool SkipEmpty { get; }
        
        public MakeDescisionAboutEmptyStrings(bool skipEmpty)
        {
            SkipEmpty = skipEmpty;
        }

        public override Task SafeExecute(TreeSerializationArgs args)
        {
            args.SkipEmptyLines = SkipEmpty;
            return Done;
        }

        public override bool SafeCondition(TreeSerializationArgs args)
        {
            return base.SafeCondition(args) && !args.SkipEmptyLines.HasValue;
        }
    }
}