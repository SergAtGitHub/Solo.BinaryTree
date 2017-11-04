using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;

namespace Solo.BinaryTree.Constructor.Tests
{
    [TestClass]
    public class ApiIntegrationTests
    {
        [TestMethod]
        public void ExampleFromTask_ShouldReturnAnExpectedTree()
        {
            string input = 
@"Fox, The, Lazy
Quick, Fox, Jumps
Jumps, Dog, #
Brown, #, Over
A, Quick, Brown";
            
            var expectedResult =
                Tree.Create("A").Result.AddLeftAndNavigateToIt("Quick").AddLeftAndNavigateToIt("Fox")
                    .AddLeftAndStay("The").AddRightAndNavigateBack("Lazy").AddRightAndNavigateToIt("Jumps")
                    .AddLeftAndNavigateToRoot("Dog").AddRightAndNavigateToIt("Brown").AddRightAndNavigateToRoot("Over");
            
                Tree actualResult = Api.BuildTreeByStringInput(input);

                Assert.IsTrue(TreeComparer.Instance.Equals(actualResult, expectedResult));
        }

        [TestMethod]
        public void WhenDuplicatingRootsInInput_AlgorythmShouldComplain()
        {
            string input =
                @"Fox, The, Lazy
Fox, Not, Active";

            string expectedError = string.Format(ChainedBinaryTreeMessages.DuplicatingEntryWasFound,
                "Fox", "Not", "Active", "The", "Lazy");

            try
            {
                Api.BuildTreeByStringInput(input);
                Assert.Fail("Algorythm should not pass.");
            }
            catch (Exception e)
            {
                Assert.AreEqual(expectedError, e.Message);
            }

            
        }
    }
}