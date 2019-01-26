using System.Collections.Generic;
using System.IO;

namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation.Actions
{
    public class DescribeTreeFromStreamContext : BinaryTreeParseAction
    {
        public override void Execute(BinaryTreeParseArguments args)
        {
            args.TextStrings = this.DeclareEnumerableStrings(args.TextReader);
        }

        public virtual IEnumerable<string> DeclareEnumerableStrings(TextReader streamReader)
        {
            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    yield return str;
                }
            }
        }

        public override bool CanExecute(BinaryTreeParseArguments args)
        {
            return args.TextReader != null && args.TextStrings == null;
        }
    }
}