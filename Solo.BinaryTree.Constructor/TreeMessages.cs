namespace Solo.BinaryTree.Constructor
{
    public static class TreeMessages
    {
        public static readonly string CannotSpecifyChildBecauseItAlreadyHasParent =
            "The node with data: [{0}] cannot override a [{1}] node with data [{2}], because it already has a parent with data: [{3}].";

        public static readonly string CannotAddReferenceTwice = "You're trying to add a chilren twice.";
        public static string CanotCreateTreeWithoutData = "Cannot create a node without a data.";
    }
}