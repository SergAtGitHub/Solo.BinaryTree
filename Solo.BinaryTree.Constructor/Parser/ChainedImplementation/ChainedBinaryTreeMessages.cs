namespace Solo.BinaryTree.Constructor.Parser.ChainedImplementation
{
    public static class ChainedBinaryTreeMessages
    {
        public static readonly string DuplicatingEntryWasFound =
                "Duplicating entry with key [{0}] was found. Algorythm was supposed to create create children: [{1}] and [{2}], but instead found [{3}] and [{4}].";

        public static readonly string TheStringWasNotInExpectedFormat =
                "A line should contain 3 members in format similar to: 'Root, LeftNode, RightNode', but actual result was [{0}].";
    }
}