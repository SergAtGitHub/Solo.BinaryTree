using Solo.BinaryTree.Constructor.Infrastructure;

namespace Solo.BinaryTree.Constructor.Tests
{
    public static class TaskData
    {
        public static readonly string ExampleTreeInput =
            @"Fox, The, Lazy
Quick, Fox, Jumps
Jumps, Dog, #
Brown, #, Over
A, Quick, Brown";

        public static readonly Tree ExpectedTree = Tree.Create("A").Result.AddLeftAndNavigateToIt("Quick").AddLeftAndNavigateToIt("Fox")
            .AddLeftAndStay("The").AddRightAndNavigateBack("Lazy").AddRightAndNavigateToIt("Jumps")
            .AddLeftAndNavigateToRoot("Dog").AddRightAndNavigateToIt("Brown").AddRightAndNavigateToRoot("Over");

    }
}