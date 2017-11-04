using System.Collections.Generic;
using System.IO;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class DescribeTreeFromStreamContext : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.TextStrings = this.DeclareEnumerableStrings(args.StreamReader);
        }

        public virtual IEnumerable<string> DeclareEnumerableStrings(StreamReader streamReader)
        {
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                yield return str;
            }
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return base.CanExecute(args) && args.StreamReader != null && args.TextStrings == null;
        }
    }
}