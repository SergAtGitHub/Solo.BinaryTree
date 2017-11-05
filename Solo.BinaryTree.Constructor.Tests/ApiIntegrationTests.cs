using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solo.BinaryTree.Constructor.Infrastructure;
using Solo.BinaryTree.Constructor.Infrastructure.Builders;
using Solo.BinaryTree.Constructor.Infrastructure.Traverse;
using Solo.BinaryTree.Constructor.Parser.ChainedImplementation;
using Solo.BinaryTree.Constructor.Serializer;

namespace Solo.BinaryTree.Constructor.Tests
{
    [TestClass]
    public class ApiIntegrationTests
    {
        [TestMethod]
        public void ExampleFromTask_ShouldReturnAnExpectedTree()
        {
            string input = TaskData.ExampleTreeInput;

            
            var expectedResult = TaskData.ExpectedTree;

            
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

        [TestMethod]
        public void TreeInputGenerator_ShouldGenerateData()
        {
            string expectedResult = TaskData.ExampleTreeInput;

            var tree = TaskData.ExpectedTree;

            string actualResult = Api.Serialize(tree, InlineTreeFormatter.WellKnownFormats.CommaAndSpaceSeparated);

            CollectionAssert.AreEquivalent(
                expectedResult.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries),
                actualResult.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
        }

        [TestMethod]
        public void WhenEmptyLinesAreComing_TreeBuilderShouldIgnoreThem()
        {
            var input = @"A, Quick, Brown
Quick, Fox, Jumps
Fox, The, Lazy


Jumps, Dog, #

Brown, #, Over
";


            var expectedResult = TaskData.ExpectedTree;

            Tree actualResult = Api.BuildTreeByStringInput(input);

            Assert.IsTrue(TreeComparer.Instance.Equals(actualResult, expectedResult));
        }

        [TestMethod]
        public void WhenTwoWordsInString_AlgorythmShouldComplain()
        {
            var input = @"A, Quick, Brown
Quick, Fox, Jumps
Fox, The
";

            string expectedError = string.Format(ChainedBinaryTreeMessages.TheStringWasNotInExpectedFormat,
                "Fox, The");

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

        [TestMethod]
        public void SerializedTree_ShouldBeWellUnderstoodByParser()
        {
            var builder = new LineByLineTreeBuilder();

            builder.AddData(Enumerable.Range(1, 1024).Select(x => x.ToString()).ToArray());
            string input = Api.Serialize(builder.Root);


            var actualTree = Api.BuildTreeByStringInput(input);
            var expectedTree = builder.Root;

            Assert.IsTrue(TreeComparer.Instance.Equals(expectedTree, actualTree));
        }

        [TestMethod]
        public void DeserializedTree_ShouldNotBeTheSameAsSerialized()
        {
            var builder = new LineByLineTreeBuilder();

            builder.AddData(Enumerable.Range(1, 1024).Select(x => x.ToString()).ToArray());
            string input = Api.Serialize(builder.Root);


            var actualTree = Api.BuildTreeByStringInput(input);
            var expectedTree = builder.Root;

            var traverseAlgorythm = DepthTraverse.Instance;

            using (var actualEnumerator = traverseAlgorythm.GetAll(actualTree).GetEnumerator())
            {
                using (var expectedEnumerator = traverseAlgorythm.GetAll(expectedTree).GetEnumerator())
                {
                    bool actualMove, expectedMove;
                    do
                    {
                        actualMove = actualEnumerator.MoveNext();
                        expectedMove = expectedEnumerator.MoveNext();

                        Assert.AreEqual(actualMove, expectedMove);

                        var actualNode = actualEnumerator.Current;
                        var expectedNode = expectedEnumerator.Current;

                        Assert.AreNotSame(actualNode, expectedNode);
                    } while (actualMove && expectedMove);
                }
            }

            
        }

        [TestMethod]
        public void Serializer_ShouldProduceDataFromTheTask_WhenReadableTextIsComing()
        {
            var readableText = "Quick Brown Fox Jumps # Over The Lazy Dog";

            var builder = new LineByLineTreeBuilder(Tree.Create("A").Result);
            builder.AddData(readableText.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries));

            string actualResult = Api.Serialize(builder.Root, InlineTreeFormatter.WellKnownFormats.CommaAndSpaceSeparated);
            string expectedResult = TaskData.ExampleTreeInput;

            CollectionAssert.AreEquivalent(
                expectedResult.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
                actualResult.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}