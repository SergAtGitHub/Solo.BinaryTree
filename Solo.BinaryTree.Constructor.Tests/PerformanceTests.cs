using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solo.BinaryTree.Constructor.Infrastructure.Builders;
using Solo.BinaryTree.Constructor.Serializer;

namespace Solo.BinaryTree.Constructor.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void Parser_ShouldPerformFast_WhenAmountOfDataIsQuiteBig()
        {
            var data = Enumerable.Range(0, int.MaxValue / 1000).Select(x => UpperLettersAndNumericCharacters.Instance.GetRandom(16)).Distinct().ToArray();

            var builder = new LineByLineTreeBuilder();
            builder.AddData(data);
            string input = Api.Serialize(builder.Root, InlineTreeFormatter.WellKnownFormats.CommaAndSpaceSeparated);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Api.BuildTreeByStringInput(input);

            sw.Stop();

            var acceptanceTimeInSeconds = 6;
            Trace.WriteLine(string.Format("Elapsed={0}", sw.Elapsed.Seconds));
            Assert.IsTrue(sw.Elapsed.Seconds <= acceptanceTimeInSeconds);
        }
    }

    public interface IRandomStringProvider
    {
        string GetRandom(int length);
    }

    public class UpperLettersAndNumericCharacters : IRandomStringProvider
    {
        public static readonly UpperLettersAndNumericCharacters Instance = new UpperLettersAndNumericCharacters();
        private static Random random = new Random();

        public string GetRandom(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
