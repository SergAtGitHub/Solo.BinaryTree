using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            using (StreamReader streamReader = new StreamReader(input))
            {
                var actualResult = Api.BuildTree(streamReader);

                Assert.IsTrue(TreeComparer.Instance.Equals(actualResult, expectedResult));
            }
        }
    }
}